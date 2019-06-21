using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebCore.Model;

namespace WebCore.Core
{
    public class Core
    {
        private const string Appid = "RBP7Kxvtch7yaDLFnnQIAEfUFqAa";
        private const string AppPwd = "m2_tk7noSfAEhoijHMnQ1OU782oa";

        private static NASDK currsdk = null;
        private static DateTime TokenTime;
        private static Timer TokenTimer;

        private static string TxtToken = null;

        private static readonly string PLACE_ORDER = "PLACE_ORDER";
        private static readonly string CANCEL_ORDER = "CANCEL_ORDER";
        private static readonly string QUERY_INFO = "QUERY_INFO";
        private static readonly string FORCED_END = "FORCED_END";

        private static readonly string SERVICEID = "SteamCarWash";

        private static string CALLBACK_URL = "";

        public static void InitToken()
        {
            currsdk = new NASDK("180.101.147.89", 8743, Appid, AppPwd, "iot3rd.p12", "IoM@1234");
            TokenResult token = currsdk.getToken();
            if (token == null)
            {
                //MessageBox.Show("获取失败，请看日志");
            }
            else
            {
                TxtToken = token.accessToken;
                TokenTime = DateTime.Now;
                if (TokenTimer == null)
                {
                    TokenTimer = new Timer((x) => { });
                }
            }
        }

        private static bool CheckToken()
        {
            if (TxtToken == null)
                return false;
            return true;
        }

        private static void ResetToken()
        {
            InitToken();
        }

        //PLACE_ORDER
        public static string PlaceOrder(string deviceId, string orderID, string carNo, string parkNo)
        {
            SetAssigning(deviceId, orderID, carNo, parkNo);
            //if (!CheckToken())
            //{
            //    ResetToken();
            //}
            InitToken();

            List<CommandPara> lsCmdPars = new List<CommandPara>();

            lsCmdPars.Add(new CommandPara() { isNum = false, paraName = "orderID", paraValue = orderID });
            lsCmdPars.Add(new CommandPara() { isNum = false, paraName = "carNo", paraValue = carNo });
            lsCmdPars.Add(new CommandPara() { isNum = false, paraName = "parkNo", paraValue = parkNo });

            string result = currsdk.sendCommand(TxtToken, deviceId, CALLBACK_URL, SERVICEID, PLACE_ORDER, lsCmdPars);
            if (result == null)
            {
                Console.WriteLine("PlaceOrder 获取失败，请看日志");
                return "获取失败，请看日志";
            }
            Console.WriteLine($"下发订单命令 {DateTime.Now.ToString("hh:mm;ss,fff")} " + result);
            Console.WriteLine($"deviceId:{deviceId}");
            Console.WriteLine($"orderID:{orderID}");
            Console.WriteLine("---------------" + System.Environment.NewLine);
            return result;
        }

        public static string CancelOrder(string deviceId, string orderID)
        {
            if (!CheckToken())
            {
                ResetToken();
            }
            //移除存在的OrderConf消息确认列表
            DeleteOrderConfirm(deviceId, orderID);
            List<CommandPara> lsCmdPars = new List<CommandPara>();

            lsCmdPars.Add(new CommandPara() { isNum = false, paraName = "orderID", paraValue = orderID });

            string result = currsdk.sendCommand(TxtToken, deviceId, CALLBACK_URL, SERVICEID, CANCEL_ORDER, lsCmdPars);
            if (result == null)
            {
                Console.WriteLine("CancelOrder 获取失败，请看日志");
                return "获取失败，请看日志";
            }
            Console.WriteLine($"取消订单命令 {DateTime.Now.ToString("hh:mm:ss,fff")} " + result);
            Console.WriteLine($"deviceId:{deviceId}");
            Console.WriteLine($"orderID:{orderID}");
            Console.WriteLine("---------------" + System.Environment.NewLine);
            return result;
        }

        public static string QueryInfo(string deviceId, string state)
        {
            if (!CheckToken())
            {
                ResetToken();
            }

            List<CommandPara> lsCmdPars = new List<CommandPara>();

            lsCmdPars.Add(new CommandPara() { isNum = true, paraName = "State", paraValue = state });

            string result = currsdk.sendCommand(TxtToken, deviceId, CALLBACK_URL, SERVICEID, QUERY_INFO, lsCmdPars);
            if (result == null)
            {
                Console.WriteLine("QueryInfo 获取失败，请看日志");
                return "获取失败，请看日志";
            }
            Console.WriteLine($"设备查询命令 {DateTime.Now.ToString("hh:mm;ss,fff")} {System.Environment.NewLine} QueryInfo:{result}");
            return result;
        }

        public static string ForcedOrder(string deviceId, string orderID, string check_order)
        {
            if (!CheckToken())
            {
                ResetToken();
            }

            List<CommandPara> lsCmdPars = new List<CommandPara>();

            lsCmdPars.Add(new CommandPara() { isNum = false, paraName = "orderID", paraValue = orderID });
            lsCmdPars.Add(new CommandPara() { isNum = true, paraName = "Check_order", paraValue = check_order });
            //FORCED_END
            string result = currsdk.sendCommand(TxtToken, deviceId, CALLBACK_URL, SERVICEID, FORCED_END, lsCmdPars);
            if (result == null)
            {
                Console.WriteLine("ForcedOrder 获取失败，请看日志");
                return "获取失败，请看日志";
            }
            Console.WriteLine($"强制结单命令 {DateTime.Now.ToString("hh:mm;ss,fff")} {System.Environment.NewLine} ForcedOrder:{result}");
            return result;
        }

        /// <summary>
        /// 等待指派
        /// </summary>
        private void SetWaitAssign()
        {
        }

        /// <summary>
        /// 指派中
        /// 等待服务返回消息送达标识
        /// 30秒未确认或拒绝订单这发送取消此设备订单
        /// </summary>
        public static void SetAssigning(string deviceId, string orderID, string carNo, string parkNo)
        {
            Task task = new TaskFactory().StartNew(() =>
            {
                var dev = deviceId;
                var order = orderID;

                ///
                ///1.等待消息送达
                /// N 秒后如果未收到响应数据则再次发送,
                /// M 秒后如果再次未收到,则发送取消命令,并向服务器汇报
                ///2.等到按钮响应
                /// N 秒后如未收到响应数据,则发送取消命令,并向服务器汇报
                ///
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (true)
                {
                    Thread.Sleep(1000);
                    if (OrderConf.Where(x => x.Key == dev && x.Value == order).Count() > 0)
                    {
                        //N 秒未接收则告知服务器
                        if (sw.ElapsedMilliseconds > 30000)
                        {
                            //告知后推出循环
                            {
                                var url = "https://www.chupiao.xyz/api/Servermessage";
                                Dictionary<string, string> dic = new Dictionary<string, string>();
                                //dic.Add("deviceid", "e33aa4cc-f96a-4ec3-a3bd-a126a5be54a5");
                                //dic.Add("order_number", "TPYDSD2019062048549750");
                                dic.Add("deviceid", deviceId);
                                dic.Add("order_number", orderID);
                                //var res = HttpPost(url, dic, Encoding.UTF8);
                                //Console.WriteLine(res);
                                var res = Helper.HttpClientHelper.PostResponse(url, dic);
                                Console.WriteLine(res);
                            }
                            Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss,fff")}告知后推出循环");
                            sw.Restart();
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            });
        }

        /// <summary>
        /// POST 同步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string HttpPost(string url, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 10000)
        {
            HttpClientHandler handler = new HttpClientHandler();

            HttpClient client = new HttpClient(handler);
            MemoryStream ms = new MemoryStream();
            FillFormDataStream(formData, ms);

            HttpContent hc = new StreamContent(ms);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
            hc.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var t = client.PostAsync(url, hc);
            t.Wait();
            var t2 = t.Result.Content.ReadAsByteArrayAsync();
            return encoding.GetString(t2.Result);
        }

        /// <summary>
        /// 组装QueryString的方法
        /// 参数之间用&连接，首位没有符号，如：a=1&b=2&c=3
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        public static string GetQueryString(Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            var i = 0;
            foreach (var kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                if (i < formData.Count)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 填充表单信息的Stream
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="stream"></param>
        public static void FillFormDataStream(Dictionary<string, string> formData, Stream stream)
        {
            string dataString = GetQueryString(formData);
            var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);
            stream.Write(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);//设置指针读取位置
        }

        private static Dictionary<string, string> MessagArr = new Dictionary<string, string>();
        private static Dictionary<string, string> OrderConf = new Dictionary<string, string>();

        /// <summary>
        /// 消息送达
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="orderID"></param>
        public static void SetMessageArrivals(string deviceId, string orderID)
        {
            //do something
            //MessagArr.Add(deviceId, orderID);
        }

        /// <summary>
        /// 加入到待订单确认列表
        /// 如果列表中存在相同设备并且订单号一致则返回true
        /// 如果列表中存在相同设备则返回false
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="orderID"></param>
        /// <param name="isConfirm"></param>
        public static bool GetOrderConfirm(string deviceId, string orderID)
        {
            if (OrderConf.Where(x => x.Key == deviceId).Count() > 0)
            {
                if (OrderConf.Where(x => x.Key == deviceId && x.Value == orderID).Count() > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                OrderConf.Add(deviceId, orderID);
                return true;
            }
        }

        /// <summary>
        /// 接收到订单确认/拒单
        /// 移除列表
        /// </summary>
        /// <param name="orderConf"></param>
        public static void DeleteOrderConfirm(string deviceId, string orderID)
        {
            if (OrderConf.Where(x => x.Key == deviceId).Count() > 0)
            {
                OrderConf.Remove(deviceId);
                Console.WriteLine($"订单确认列表将订单移除:{deviceId} ");
            }
            else
            {
                Console.WriteLine($"未查询到订单确认列表的设备{deviceId},执行移除操作无效");
            }
        }

        /// <summary>
        /// 心跳
        /// </summary>
        public static void Heartbeat()
        {
            Console.WriteLine("Heartbeat");
            new TaskFactory().StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(10000);

                    Console.WriteLine($"{DateTime.Now.ToString("hh:mm;ss,fff")} Heartbeat");
                }
            });
        }
    }
}
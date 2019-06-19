using System;
using System.Collections.Generic;
using System.Linq;
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
                Console.WriteLine("获取失败，请看日志");
                return "获取失败，请看日志";
            }
            Console.WriteLine($"下发订单命令 {DateTime.Now.ToString("hh:mm;ss,fff")} PlaceOrder:{orderID} " + result);
            return result;
        }

        public static string CancelOrder(string deviceId, string orderID)
        {
            if (!CheckToken())
            {
                ResetToken();
            }

            List<CommandPara> lsCmdPars = new List<CommandPara>();

            lsCmdPars.Add(new CommandPara() { isNum = false, paraName = "orderID", paraValue = orderID });

            string result = currsdk.sendCommand(TxtToken, deviceId, CALLBACK_URL, SERVICEID, CANCEL_ORDER, lsCmdPars);
            if (result == null)
            {
                Console.WriteLine("获取失败，请看日志");
                return "获取失败，请看日志";
            }
            Console.WriteLine($"取消订单命令 {DateTime.Now.ToString("hh:mm:ss,fff")} CancelOrder:{orderID} " + result);
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
                Console.WriteLine("获取失败，请看日志");
                return "获取失败，请看日志";
            }
            Console.WriteLine($"设备查询命令 {DateTime.Now.ToString("hh:mm;ss,fff")} QueryInfo:{result}");
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
                Console.WriteLine("获取失败，请看日志");
                return "获取失败，请看日志";
            }
            Console.WriteLine($"强制结单命令 {DateTime.Now.ToString("hh:mm;ss,fff")} ForcedOrder:{result}");
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
                ///
                ///1.等待消息送达
                /// N 秒后如果未收到响应数据则再次发送,
                /// M 秒后如果再次未收到,则发送取消命令,并向服务器汇报
                ///2.等到按钮响应
                /// N 秒后如未收到响应数据,则发送取消命令,并向服务器汇报
                ///
            });
        }

        /// <summary>
        /// 消息送达
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="orderID"></param>
        public static void SetMessageArrivals(string deviceId, string orderID)
        {
            //do something
        }

        /// <summary>
        /// 接收到订单确认/拒单
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="orderID"></param>
        /// <param name="isConfirm"></param>
        public static void GetOrderConfirm(OrderConfirm orderConf)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        private static readonly string FORCED_END = "FORCED_END ";

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
            return true;
        }

        private static void ResetToken()
        {
        }

        //PLACE_ORDER
        public static string PlaceOrder(string deviceId, string orderID, string carNo, string parkNo)
        {
            if (!CheckToken())
            {
                ResetToken();
            }

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
            Console.WriteLine(result);
            return result;
        }
    }
}
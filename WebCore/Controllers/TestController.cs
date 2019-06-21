using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Post()
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //int reCount = 0;
            //while (true)
            //{
            //    Thread.Sleep(1000);
            //    if (MessagArr.Where(x => x.Key == dev && x.Value == order).Count() == 0)
            //    {
            //        if (sw.ElapsedMilliseconds > 30000)
            //        {
            //            if (reCount < 1)
            //            {
            //                reCount++;
            //                //再次发送
            //                Console.WriteLine("再次发送下单命令");
            //                var place = PlaceOrder(deviceId, orderID, carNo, parkNo);
            //                if (place == "201")
            //                {
            //                }
            //                else
            //                {
            //                    Console.WriteLine("再次发送下单命令失败");
            //                }
            //            }
            //            else
            //            {
            //                //真正的取消 并告知
            //                Console.WriteLine("取消订单");
            //                //CancelOrder(deviceId, orderID);
            //                break;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            return "";
        }

        private static Dictionary<string, string> MessagArr = new Dictionary<string, string>();
        private static Dictionary<string, string> OrderConf = new Dictionary<string, string>();
    }
}
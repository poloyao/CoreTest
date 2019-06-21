using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCore.Model;

namespace WebCore.Controllers
{
    /// <summary>
    /// 消息送达
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MessageArrivalsController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Post([FromBody] MessageArrivals mess)
        {
            //Console.WriteLine($"接收到消息送达 {DateTime.Now.ToString("hh:mm:ss,fff")} message confirm:{mess.orderID}");
            //Core.Core.SetMessageArrivals(mess.deviceId, mess.orderID);

            AssignResult r = new AssignResult();

            if (mess == null)
            {
                r.code = 200;
                r.Des = "传入的数据异常";
                Console.WriteLine($"接收到消息送达 {DateTime.Now.ToString("hh:mm:ss,fff")} message confirm:code:{r.code} Des:{r.code}");
            }
            else if (mess.deviceId == null || mess.deviceId == "")
            {
                r.code = 200;
                r.Des = "传入的deviceId数据异常";
                Console.WriteLine($"接收到消息送达 {DateTime.Now.ToString("hh:mm:ss,fff")} message confirm:code:{r.code} Des:{r.code}");
            }
            else if (mess.orderID == null || mess.orderID == "")
            {
                r.code = 200;
                r.Des = "传入的orderID数据异常";
                Console.WriteLine($"接收到消息送达 {DateTime.Now.ToString("hh:mm:ss,fff")} message confirm:code:{r.code} Des:{r.code}");
            }
            else
            {
                r.code = 200;
                r.Des = "MessageArrivals ok";
                Console.WriteLine($"接收到消息送达 {DateTime.Now.ToString("hh:mm:ss,fff")} message confirm:{mess.orderID}");
                Core.Core.SetMessageArrivals(mess.deviceId, mess.orderID);
            }
            return Ok(r);
        }
    }
}
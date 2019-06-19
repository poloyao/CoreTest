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
            Console.WriteLine($"接收到消息送达 {DateTime.Now.ToString("hh:mm:ss,fff")} message confirm:{mess.orderID}");
            Core.Core.SetMessageArrivals(mess.deviceId, mess.orderID);
            AssignResult r = new AssignResult();
            r.code = 200;
            r.Des = "MessageArrivals ok";
            return Ok(r);
        }
    }
}
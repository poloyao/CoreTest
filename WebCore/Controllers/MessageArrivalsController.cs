using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageArrivalsController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Post([FromBody] MessageArrivals mess)
        {
            Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss,fff")} message confirm:{mess.orderID}");
            AssignResult r = new AssignResult();
            r.code = 200;
            r.Des = "MessageArrivals ok";
            return Ok(r);
        }
    }

    public class MessageArrivals
    {
        public string deviceId { get; set; }

        public string orderID { get; set; }
    }
}
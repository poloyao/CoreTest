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
    public class OrderConfirmController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Post([FromBody] OrderConfirm order)
        {
            Console.WriteLine("order confirm");
            AssignResult r = new AssignResult();
            r.code = 200;
            return Ok(r);
        }
    }

    public class OrderConfirm
    {
        public string deviceId { get; set; }

        public string orderID { get; set; }

        public int isConfirm { get; set; }
    }
}
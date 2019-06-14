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
    public class AssignOrderController : ControllerBase
    {
        // GET api/AssignOrder
        [HttpGet]
        public ActionResult<string> Get(string deviceId, string orderID, string carNo, string parkNo)
        {
            //return $"收到Get订单{orderID} 等待分配中";
            Core.Core.InitToken();
            return $"收到Get订单{orderID}处理结果:" + Core.Core.PlaceOrder(deviceId, orderID, carNo, parkNo);
        }

        [HttpPost]
        public ActionResult<string> Post(string deviceId, string orderID, string carNo, string parkNo)
        {
            Core.Core.InitToken();
            return $"收到Post订单{orderID}处理结果:" + Core.Core.PlaceOrder(deviceId, orderID, carNo, parkNo);

            //return $"收到Post订单{orderID} 等待分配中";
        }
    }
}
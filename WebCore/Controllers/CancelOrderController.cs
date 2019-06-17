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
    public class CancelOrderController : ControllerBase
    {
        public ActionResult<string> Post(string deviceId, string orderID)
        {
            string errMess = "";
            if (deviceId.Trim() == "")
            {
                errMess += "deviceId不完整";
            }
            if (orderID.Length != 22)
            {
                errMess += "orderID 不完整";
            }

            if (errMess != "")
                return errMess;

            Core.Core.InitToken();
            return $"CancelOrder 收到订单{orderID}处理结果:" + Core.Core.CancelOrder(deviceId, orderID);
        }
    }
}
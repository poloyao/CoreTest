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
    public class ForcedEndController : ControllerBase
    {
        public ActionResult<string> Post(string deviceId, string orderID, string check_order)
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

            if (check_order == "")
            {
                errMess += "check_order 不完整";
            }

            if (errMess != "")
                return errMess;

            Core.Core.InitToken();
            return $"ForcedEnd 收到订单{orderID}处理结果:" + Core.Core.ForcedOrder(deviceId, orderID, check_order);
        }
    }
}
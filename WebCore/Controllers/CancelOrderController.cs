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
    /// 取消订单
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CancelOrderController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Post([FromBody] Assign assign)
        {
            string errMess = "";
            if (assign.deviceId != null && assign.deviceId.Trim() == "")
            {
                errMess += "deviceId不完整";
            }
            if (assign.orderID != null && assign.orderID.Length != 22)
            {
                errMess += "orderID 不完整";
            }
            AssignResult r = new AssignResult();
            if (errMess != "")
            {
                r.code = 400;
                r.Des = errMess;
                //r.Des = System.Web.HttpUtility.UrlEncode(r.Des, System.Text.Encoding.GetEncoding("UTF-8"));
                return Ok(r);
            }

            var result = Core.Core.CancelOrder(assign.deviceId, assign.orderID);

            if (result == "201")
                r.code = 200;
            else
            {
                r.code = 400;
                r.Des = result;
            }

            return Ok(r);

            //return $"CancelOrder 收到订单{assign.orderID}处理结果:" + Core.Core.CancelOrder(assign.deviceId, assign.orderID);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCore.Helper;
using WebCore.Model;

namespace WebCore.Controllers
{
    /// <summary>
    /// 订单确认/拒单
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderConfirmController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Post([FromBody] OrderConfirm order)
        {
            //ZHHelper.ConsoleOut($"接收到订单确认 {DateTime.Now.ToString("hh:mm:ss,fff")} order confirm:{order.orderID}");
            //Core.Core.GetOrderConfirm(order);
            //AssignResult r = new AssignResult();
            //r.code = 200;
            //return Ok(r);

            AssignResult r = new AssignResult();

            if (order == null)
            {
                r.code = 200;
                r.Des = "传入的数据异常";
                ZHHelper.ConsoleOut($"接收到订单确认 {DateTime.Now.ToString("hh:mm:ss,fff")} message confirm:code:{r.code} Des:{r.code}");
            }
            else if (order.deviceId == null || order.deviceId == "")
            {
                r.code = 200;
                r.Des = "传入的deviceId数据异常";
                ZHHelper.ConsoleOut($"接收到订单确认 {DateTime.Now.ToString("hh:mm:ss,fff")} message confirm:code:{r.code} Des:{r.code}");
            }
            else if (order.orderID == null || order.orderID == "")
            {
                r.code = 200;
                r.Des = "传入的orderID数据异常";
                ZHHelper.ConsoleOut($"接收到订单确认 {DateTime.Now.ToString("hh:mm:ss,fff")} message confirm:code:{r.code} Des:{r.code}");
            }
            else
            {
                r.code = 200;
                ZHHelper.ConsoleOut($"接收到订单确认 {DateTime.Now.ToString("hh:mm:ss,fff")} message confirm:{order.orderID}");
                Core.Core.DeleteOrderConfirm(order.deviceId, order.orderID);
            }
            return Ok(r);
        }
    }
}
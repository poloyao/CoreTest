using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCore.Helper;
using WebCore.Model;

namespace WebCore.Controllers
{
    /// <summary>
    /// 下单,派单
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AssignOrderController : ControllerBase
    {
        // GET api/AssignOrder
        [HttpGet]
        public ActionResult<string> Get(string deviceId, string orderID, string carNo, string parkNo)
        {
            //var aop = AssignOrderPost(deviceId, orderID, carNo, parkNo);
            //return Ok(aop);

            return "get error";
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody]Assign assign)
        {
            //Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss,fff")}  AssignOrder:" + assign.deviceId + "---" + assign.orderID);

            var aop = AssignOrderPost(assign.deviceId, assign.orderID, assign.carNo, assign.parkNo);
            return Ok(aop);
        }

        private AssignResult AssignOrderPost(string deviceId, string orderID, string carNo, string parkNo)
        {
            string errMess = "";
            if (deviceId == null && deviceId.Trim() == "")
            {
                errMess += "deviceId不完整";
            }
            if (orderID == null && orderID.Length != 22)
            {
                errMess += "orderID 不完整";
            }
            if (carNo == null && carNo.Trim() == "" && ZHHelper.CheckZhLength(carNo) > 20)
            {
                errMess += "carNo 长度异常";
            }
            else
            {
                carNo = ZHHelper.ZH_Fill(carNo, 20);
            }
            if (parkNo.Trim() == "" && ZHHelper.CheckZhLength(parkNo) > 50)
            {
                errMess += "parkNo 长度异常";
            }
            else
            {
                parkNo = ZHHelper.ZH_Fill(parkNo, 50);
            }
            AssignResult r = new AssignResult();
            if (errMess != "")
            {
                r.code = 400;
                r.Des = errMess;
                return r;
            }
            if (Core.Core.GetOrderConfirm(deviceId, orderID))
            {
                var asss = Core.Core.PlaceOrder(deviceId, orderID, carNo, parkNo);
                if (asss == "201")
                {
                    r.code = 200;
                }
                else
                {
                    r.code = 400;
                    r.Des = asss;
                }
            }
            else
            {
                // ZHHelper.ConsoleOut($"AssignOrderPost 当前设备已再下发订单列表中{deviceId},orderID:{orderID}");
                Console.WriteLine($"AssignOrderPost list existed {deviceId},orderID:{orderID}");
                r.code = 400;
                r.Des = $"当前设备已在下发订单列表中{deviceId},orderID:{orderID}";
            }
            return r;
        }
    }
}
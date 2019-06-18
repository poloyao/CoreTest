using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCore.Helper;

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
            if (deviceId.Trim() == "")
            {
                errMess += "deviceId不完整";
            }
            if (orderID.Length != 22)
            {
                errMess += "orderID 不完整";
            }
            if (carNo.Trim() == "" && ZHHelper.CheckZhLength(carNo) > 20)
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
            var asss = Core.Core.PlaceOrder(deviceId, orderID, carNo, parkNo);

            if (asss == "201")
                r.code = 200;
            else
            {
                r.code = 400;
                r.Des = asss;
            }
            return r;
        }
    }

    public class AssignResult
    {
        public int code { get; set; }
        public string Des { get; set; }
    }

    public class Assign
    {
        public string deviceId { get; set; }

        public string orderID { get; set; }

        public string carNo { get; set; }

        public string parkNo { get; set; }
    }
}
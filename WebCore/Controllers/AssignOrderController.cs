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
            return AssignOrderPost(deviceId, orderID, carNo, parkNo);
        }

        //[HttpPost]
        //public ActionResult<string> Post(string deviceId, string orderID, string carNo, string parkNo)
        //{
        //    return AssignOrderPost(deviceId, orderID, carNo, parkNo);
        //}


        [HttpPost]
        public ActionResult<string> Post([FromBody]Assign assign)
        {
            return AssignOrderPost(assign.deviceId, assign.orderID, assign.carNo, assign.parkNo);
        }

        private string AssignOrderPost(string deviceId, string orderID, string carNo, string parkNo)
        {
            Console.WriteLine("go on");
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

            if (errMess != "")
                return errMess;

            //Core.Core.InitToken();
            return $"收到Get订单{orderID}处理结果:" + Core.Core.PlaceOrder(deviceId, orderID, carNo, parkNo);
        }
    }

    public class Assign
    {
        public string deviceId { get; set; }

        public string orderID { get; set; }

        public string carNo { get; set; }

        public string parkNo { get; set; }
    }
}
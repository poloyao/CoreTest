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
    public class QueryController : ControllerBase
    {
        public ActionResult<string> Post(string deviceId, string state)
        {
            string errMess = "";
            if (deviceId.Trim() == "")
            {
                errMess += "deviceId不完整";
            }

            if (errMess != "")
                return errMess;

            Core.Core.InitToken();
            return $" Query 处理结果:" + Core.Core.CancelOrder(deviceId, state);
        }
    }
}
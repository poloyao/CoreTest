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
    /// 查询
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Post([FromBody] Assign assign)
        {
            string errMess = "";
            if (assign.deviceId.Trim() == "")
            {
                errMess += "deviceId不完整";
            }
            AssignResult r = new AssignResult();
            if (errMess != "")
            {
                r.code = 400;
                r.Des = errMess;
                return Ok(r);
            }

            var result = Core.Core.QueryInfo(assign.deviceId, "1");

            if (result == "201")
                r.code = 200;
            else
            {
                r.code = 400;
                r.Des = result;
            }

            return Ok(r);
        }
    }
}
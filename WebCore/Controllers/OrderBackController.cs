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
    public class OrderBackController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Post([FromBody] object assign)
        {
            return Ok("234");
        }
    }
}
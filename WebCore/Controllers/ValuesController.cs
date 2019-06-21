using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Console.WriteLine("Version V1.0.0.1");
            //Core.Core.SetAssigning("123", "456", "678", "901"); ;
            return new string[] { "Version", "V1.0.0.1" };
        }
    }
}
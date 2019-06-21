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
            Console.OutputEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Console.WriteLine("1 版本 V1.0.0.1");
            Helper.ZHHelper.ConsoleOut("2 版本 V1.0.0.1");
            //Core.Core.SetAssigning("123", "456", "678", "901"); ;
            try
            {
                Console.WriteLine(1111111111);
                var url = "http://www.chupiao.xyz/api/Servermessage";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("deviceid", "123");
                dic.Add("order_number", "234");
                var res = Helper.HttpClientHelper.PostResponse(url, dic);
                Console.WriteLine(res);
            }
            catch (Exception)
            {
            }

            //try
            //{
            //    Console.WriteLine(22222222);
            //    var url = "https://www.baidu.com/";
            //    Dictionary<string, string> dic = new Dictionary<string, string>();
            //    dic.Add("deviceid", "123");
            //    dic.Add("order_number", "234");
            //    var res = Helper.HttpClientHelper.PostResponse(url, dic);
            //    Console.WriteLine(res);
            //}
            //catch (Exception)
            //{
            //}

            return new string[] { "Version", "V1.0.0.1" };
        }
    }
}
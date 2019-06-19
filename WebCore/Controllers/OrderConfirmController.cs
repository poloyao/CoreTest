﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            Console.WriteLine("order confirm");
            Core.Core.GetOrderConfirm(order);
            AssignResult r = new AssignResult();
            r.code = 200;
            return Ok(r);
        }
    }
}
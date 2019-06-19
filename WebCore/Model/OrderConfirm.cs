using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Model
{
    /// <summary>
    /// 订单确认
    /// </summary>
    public class OrderConfirm
    {
        public string deviceId { get; set; }

        public string orderID { get; set; }

        public int isConfirm { get; set; }
    }
}
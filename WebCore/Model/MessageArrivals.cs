using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Model
{
    /// <summary>
    /// 消息确认
    /// </summary>
    public class MessageArrivals
    {
        public string deviceId { get; set; }

        public string orderID { get; set; }
    }
}
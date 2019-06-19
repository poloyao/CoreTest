using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Model
{
    /// <summary>
    /// 下单信息
    /// </summary>
    public class Assign
    {
        public string deviceId { get; set; }

        public string orderID { get; set; }

        public string carNo { get; set; }

        public string parkNo { get; set; }
    }
}
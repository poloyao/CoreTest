using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Model
{
    /// <summary>
    /// 派单等信息接口返回值
    /// </summary>
    public class AssignResult
    {
        /// <summary>
        /// 200 正常,400 异常
        /// </summary>
        public int code { get; set; }

        public string Des { get; set; }
    }
}
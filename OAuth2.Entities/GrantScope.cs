using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Entities
{
    public class GrantScope
    {
        public int SCOPE_ID { get; set; }
        public string SCOPE_CODE { get; set; }
        public string SCOPE_NAME { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string REMARKS { get; set; }
        /// <summary>
        /// 是否需要显式授权
        /// </summary>
        public bool IS_EXPLLICIT { get; set; }

    }
}

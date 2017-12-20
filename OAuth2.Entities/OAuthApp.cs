using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Entities
{
    public class OAuthApp
    {
        public int APP_ID { get; set; }
        public string APP_CODE { get; set; }
        public string APP_NAME { get; set; }
        public string APP_HOST { get; set; }
        public string SECRET_KEY { get; set; }
        public string UID_ENCRYPT_KEY { get; set; }
        public string ACCESS_TOKEN { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string REMARKS { get; set; }
        public int STATUS { get; set; }
        /// <summary>
        /// 是否英雄会内部应用
        /// </summary>
        public bool IS_INTERNAL { get; set; }
        public string LOGO_URL { get; set; }
    }
}

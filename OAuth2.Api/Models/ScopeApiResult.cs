using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.Api.Models
{
    public class ScopeApiResult
    {
        public string Api_Url { get; set; }
        public string Api_Name { get; set; }
        public int App_Type { get; set; }
        public DateTime Create_Time { get; set; }
        public int Status { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.Api.Areas.Api.Models
{
    public class SendValidCodeArgs
    {
        public string UserCode { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.Api.Areas.Api.Models
{
    public class RegisterArgs
    {
        public string RefereeCode { get; set; }
        public string SmsCode { get; set; }
        public string Password { get; set; }
    }
}
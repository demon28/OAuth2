using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.Api.Areas.Api.Models
{
    public class PasswordResetModel
    {
        public string New_Pwd { get; set; }
        public int ValidateType { get; set; }
        public string ValidateCode { get; set; }
        public int PwdType { get; set; }
    }
}
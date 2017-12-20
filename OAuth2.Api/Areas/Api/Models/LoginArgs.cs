using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OAuth2.Api.Areas.Api.Models
{
    public class LoginArgs
    {
        [Required(ErrorMessage = "{0}不能为空"), Display(Name = "登录密码")]
        public string Password { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Data.Validation;

namespace OAuth2.Entities
{
    public class ThirdPartyBindingModel
    {
        /// <summary>
        /// 第三方类型
        /// </summary>
        [EnumDefine(typeof(ThirdPartyLogin), ErrorMessage = "未定义的第三方登录方式")]
        public int ThirdParty { get; set; }
        [Required(ErrorMessage = "{0}不能为空"), Display(Name = "第三方会员开放ID")]
        public string OpenID { get; set; }
        [Required(ErrorMessage = "{0}不能为空"), Display(Name = "绑定手机号")]
        public string UserCode { get; set; }
        [Required(ErrorMessage = "{0}不能为空"), Display(Name = "短信验证码")]
        public string ValidateCode { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public string RefereeCode { get; set; }
    }
}

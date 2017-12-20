using OAuth2.Api.Models;
using OAuth2.Entities;
using OAuth2.Facade;
using OAuth2.Facade.ThirdPartySignIn;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Winner.Data.Validation;
using Winner.Framework.Utils;
using Winner.Framework.Utils.Model;
using Winner.User.Interface;
using Winner.WebApi.Contract;

namespace OAuth2.Api.Areas.Api.Controllers
{
    /// <summary>
    /// 第三方登录控制器
    /// </summary>
    [IgnoreUserToken]
    public class ThirdpartyController : ApiControllerBase
    {
        public ActionResult Login([EnumDefine(typeof(ThirdPartyLogin))]int ThirdParty,
            [Required(ErrorMessage = "{0}不能为空"), Display(Name = "第三方会员ID")] string OpenID)
        {
            var app = Facade.Caches.OAuthAppCache.Instance.Find(it => string.Equals(Package.MerchantNo, it.APP_CODE, StringComparison.OrdinalIgnoreCase));
            if (app == null)
            {
                return FailResult("商户不存在", (int)ApiStatusCode.DATA_NOT_FOUND);
            }
            string plainText;
            if (!xUtils.RsaDecryptPayPwd(OpenID, out plainText))
            {
                return FailResult("OpenID解密失败", (int)ApiStatusCode.DECRYPT_PASSWORD_FAIL);
            }
            int pos = plainText.IndexOf('_');
            string[] array = new string[2];
            array[0] = plainText.Substring(0, pos);
            array[1] = plainText.Substring(pos + 1);
            long timestamp;
            if (!long.TryParse(array[0], out timestamp))
            {
                return FailResult("OpenID解密失败", (int)ApiStatusCode.DECRYPT_PASSWORD_FAIL);
            }
            long currentTime = xUtils.GetCurrentTimeStamp();
            if (currentTime - timestamp > 120)
            {
                return FailResult("请求已过期", (int)ApiStatusCode.BAD_REQUEST);
            }
            string trueOpenID = array[1];
            var thirdLogin = new ThirdPartyLoginProvider((ThirdPartyLogin)ThirdParty, trueOpenID);
            var result = thirdLogin.Login(this.Package, Request.UserHostAddress, Session.SessionID, app.APP_ID);
            return Json(result);
        }

        public ActionResult Bind(ThirdPartyBindingModel model)
        {
            var app = Facade.Caches.OAuthAppCache.Instance.Find(it => string.Equals(Package.MerchantNo, it.APP_CODE, StringComparison.OrdinalIgnoreCase));
            if (app == null)
            {
                return FailResult("商户不存在", (int)ApiStatusCode.DATA_NOT_FOUND);
            }
            //先绑定手机号
            string plainText;
            if (!xUtils.RsaDecryptPayPwd(model.OpenID, out plainText))
            {
                return FailResult("OpenID解密失败", (int)ApiStatusCode.DECRYPT_PASSWORD_FAIL);
            }
            int pos = plainText.IndexOf('_');
            string[] array = new string[2];
            array[0] = plainText.Substring(0, pos);
            array[1] = plainText.Substring(pos + 1);
            long timestamp;
            if (!long.TryParse(array[0], out timestamp))
            {
                return FailResult("OpenID解密失败", (int)ApiStatusCode.DECRYPT_PASSWORD_FAIL);
            }
            long currentTime = xUtils.GetCurrentTimeStamp();
            if (currentTime - timestamp > 120)
            {
                return FailResult("请求已过期", (int)ApiStatusCode.BAD_REQUEST);
            }
            ThirdPartyBindingProvider bindingProvider = new ThirdPartyBindingProvider(model);
            if (!bindingProvider.Register())
            {
                return FailResult(bindingProvider.PromptInfo.CustomMessage);
            }
            //再调用登录
            var thirdLogin = new ThirdPartyLoginProvider((ThirdPartyLogin)model.ThirdParty, array[1]);
            var result = thirdLogin.Login(this.Package, Request.UserHostAddress, Session.SessionID, app.APP_ID);
            return Json(result);
        }

        public ActionResult SendValidateCode()
        {
            //发送绑定手机验证码
            SmsValidateProvider smsProvider = new SmsValidateProvider(Package.UserCode, SmsValidateType.绑定手机号);
            if (!smsProvider.SendCode())
            {
                return FailResult(smsProvider.PromptInfo.CustomMessage);
            }
            var data = new
            {
                Exist = smsProvider.User != null
            };
            return SuccessResult(data);
        }
        /// <summary>
        /// 检查推荐人账号是否存在
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckUserCode()
        {
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser user = fac?.GetUserByCode(Package.UserCode);
            if (user == null)
            {
                return FailResult("推荐人账号未注册");
            }
            else
            {
                string userName = string.Concat("*", user.UserName.Substring(1));
                if (Regex.IsMatch(user.UserName, "^\\d+$"))
                {
                    userName = Regex.Replace(user.UserName, "^(\\d{2})(\\d{5})(\\d{4})$", "$1*****$3");
                }
                var data = new
                {
                    UserName = userName
                };
                return SuccessResult(data);
            }
        }
    }
}
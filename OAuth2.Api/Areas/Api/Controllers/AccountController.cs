using OAuth2.Api.Areas.Api.Models;
using OAuth2.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.WebApi.Contract;

namespace OAuth2.Api.Areas.Api.Controllers
{
    [IgnoreUserToken]
    public class AccountController : ApiControllerBase
    {

        public ActionResult Index()
        {
            return View();
        }
        // GET: api/account/login

        public ActionResult Login(LoginArgs arg)
        {
            var app = Facade.Caches.OAuthAppCache.Instance.Find(it => string.Equals(Package.MerchantNo, it.APP_CODE, StringComparison.OrdinalIgnoreCase));
            if (app == null)
            {
                return FailResult("商户不存在", (int)ApiStatusCode.DATA_NOT_FOUND);
            }
            LoginProvider loginProvider = new LoginProvider(Package.UserCode, arg.Password);
            if (!loginProvider.Login(Package.ClientSource, Package.ClientSystem, Package.Device_Id, Request.UserHostAddress, Session.SessionID, Package.ClientVersion, app.APP_ID))
            {
                return FailResult(loginProvider.PromptInfo.CustomMessage, (int)loginProvider.PromptInfo.ResultType);
            }
            var data = new
            {
                Token = loginProvider.Token
            };
            return SuccessResult(data);
        }

        public ActionResult Register(RegisterArgs arg)
        {
            var app = Facade.Caches.OAuthAppCache.Instance.Find(it => string.Equals(Package.MerchantNo, it.APP_CODE, StringComparison.OrdinalIgnoreCase));
            if (app == null)
            {
                return FailResult("商户不存在", (int)ApiStatusCode.DATA_NOT_FOUND);
            }
            //先注册，再登录发放TOKEN
            RegisterProvider provider = new RegisterProvider(Package.UserCode, arg.Password, arg.SmsCode, arg.RefereeCode);
            if (!provider.Register())
            {
                return FailResult(provider.PromptInfo.CustomMessage, (int)provider.PromptInfo.ResultType);
            }
            object data = null;
            LoginProvider loginProvider = new LoginProvider(Package.UserCode, arg.Password);
            if (loginProvider.Login(Package.ClientSource, Package.ClientSystem, Package.Device_Id, Request.UserHostAddress, Session.SessionID, Package.ClientVersion, app.APP_ID))
            {
                data = new
                {
                    Token = loginProvider.Token
                };
            }
            return SuccessResult(data);
        }

        public ActionResult SendValidCode()
        {
            SmsValidateProvider smsProvider = new SmsValidateProvider(Package.UserCode, Entities.SmsValidateType.注册);
            if (!smsProvider.SendCode())
            {
                return FailResult(smsProvider.PromptInfo.CustomMessage, (int)smsProvider.PromptInfo.ResultType);
            }
            return SuccessResult();
        }
    }
}
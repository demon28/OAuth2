using OAuth2.DataAccess;
using OAuth2.Facade;
using OAuth2.Facade.Caches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Winner.Framework.MVC.Controllers;
using Winner.User.Interface;

namespace OAuth2.Api.Models.Mvc
{
    public class OAuthControllerBase : TopControllerBase
    {
        public const string COOKIE_NAME = "oauth_installer_id";
        public OAuthContext OAuthContext { get; set; }
        protected virtual bool UserLogin(string user_code, string password, int appId, out string message)
        {
            var cookie = Request.Cookies[COOKIE_NAME];
            int client_source = 1;
            string client_system = string.Concat(Request.Browser.Platform, " ", Request.Browser.Browser, " ", Request.Browser.Version);
            string device_id = cookie != null ? cookie.Value : "unknown device";
            string ip_address = Request.UserHostAddress;
            string session_id = Session.SessionID;
            string clientVersion = Request.Headers["client-version"];
            LoginProvider loginProvider = new LoginProvider(user_code, password);
            loginProvider.OnLogined += (user) =>
            {
                Session[OAuthContext._USER_LOGIN_SESSION_NAME] = user;
            };
            bool res = loginProvider.Login(client_source, client_system, device_id, ip_address, session_id, clientVersion, appId);
            message = loginProvider.PromptInfo.CustomMessage;
            return res;
        }
        protected virtual bool LoginByToken(string rsaToken, string device_id, string appVersion, out string message)
        {
            string com_token;
            if (!xUtils.RsaDecryptPayPwd(rsaToken, out com_token))
            {
                message = "无效的登录会话，请重新登录";
                return false;
            }
            if (!com_token.Contains("_"))
            {
                message = "无效的请求";
                return false;
            }
            string[] array = com_token.Split('_');
            string token = array[1];
            long requestTimestamp = Convert.ToInt64(array[0]);
            long timestamp = (long)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds;
            if (timestamp - requestTimestamp > 120)//请求有效期2分钟
            {
                message = "会话超时，请重新登录";
                return false;
            }
            Token.UserToken userToken = Token.UserToken.FromCipherToken(token);
            if (userToken == null)
            {
                message = "无效的登录会话，请重新登录";
                return false;
            }
            if (userToken.Expire_Time < DateTime.Now)
            {
                message = "登录会话已失效，请重新登录";
                return false;
            }

            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser user = fac?.GetUserByCode(userToken.UserCode);
            if (user == null)
            {
                message = "账号未注册";
                return false;
            }
            Session[OAuthContext._USER_LOGIN_SESSION_NAME] = user;
            message = null;
            return true;
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.OAuthContext = new OAuthContext();
            var cookie = Request.Cookies[COOKIE_NAME];
            string installer_id = cookie?.Value ?? Guid.NewGuid().ToString();
            if (cookie == null || (cookie.Expires - DateTime.Now).TotalDays < 30)
            {
                HttpCookie instrallerCookie = new HttpCookie(COOKIE_NAME);
                instrallerCookie.Expires = DateTime.Now.AddDays(365);
                instrallerCookie.Value = installer_id;
                Response.AppendCookie(instrallerCookie);
            }
        }
    }
}
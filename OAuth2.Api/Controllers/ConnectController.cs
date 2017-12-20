using OAuth2.Api.Models.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAuth2.Facade.Caches;
using OAuth2.Entities;
using OAuth2.Facade;
using Winner.User.Interface;
using Winner.Framework.Utils.Model;
using Winner.Framework.Utils;
using OAuth2.Api.Models;
using OAuth2.DataAccess;

namespace OAuth2.Api.Controllers
{
    public class ConnectController : OAuthControllerBase
    {
        // GET: Connect
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取用户授权码
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="scope"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Authorize(string appid, string scope, string state, string redirect_uri)
        {
            string authHeader = Request.Headers["auth"];
            string device_id = Request.Headers["device-id"];
            string appVersion = Request.Headers["app-version"];
            Log.Info("HTTP HEADER: auth={0}&device_id={1}&app-version={2}", authHeader, device_id, appVersion);
            if (!string.IsNullOrEmpty(authHeader))
            {
                scope = string.IsNullOrEmpty(scope) ? "basic_api" : scope;
                string message;
                try
                {
                    if (!this.LoginByToken(authHeader, device_id, appVersion, out message))
                    {
                        return View("fatal", message);
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    Log.Info("APP登录失败", ex);
                    return View("fatal", new { Message = message });
                }
            }

            OAuthApp app = OAuthAppCache.Instance.Find(it => it.APP_CODE.Equals(appid));
            //var scopeModel = ScopeCache.Instance.Find(it => it.SCOPE_CODE.Equals(scope));
            GrantScope[] scopeModel = ScopeCache.Instance.FindAll(scope);
            if (scopeModel == null || scopeModel.Length <= 0)
            {
                return View("fatal", FuncResult.FailResult("无效的授权范围"));
            }
            var scopeids = scopeModel.Select(it => it.SCOPE_ID);
            var scopeRights = ScopeRightProvider.GetScopeRights(scopeids.ToArray());
            ViewBag.ScopeRights = scopeRights;
            if (app == null)
            {
                return View("fatal", FuncResult.FailResult("未注册的应用"));
            }
            this.OAuthContext.CurrentApp = app;

            if (string.IsNullOrEmpty(redirect_uri))
            {
                return View("fatal", FuncResult.FailResult("redirect_uri不能为空"));
            }
            if (this.OAuthContext.IsLogined)//已登录
            {
                bool isAlreadyAuthorized = xUtils.IsAlreayAuthorized(app.APP_ID, this.OAuthContext.UserInfo.UserId, scope);//是否已授权

                if (app.IS_INTERNAL || !scopeModel.HasExpllicit() || isAlreadyAuthorized)//内部应用、隐式授权作用域以及已经授权过
                {
                    GrantProvider provider = new GrantProvider(appid, this.OAuthContext.UserInfo.UserCode, scope, device_id ?? this.OAuthContext.Device_Id);
                    if (!provider.Grant(!isAlreadyAuthorized, null))//获取授权作用范围内所有权限
                    {
                        return View("fatal", FuncResult.FailResult("授权失败"));
                    }
                    string auth_code = provider.Auth_Code;
                    string return_url = xUtils.CombinaRedirectUri(redirect_uri, state, auth_code);
                    return Redirect(return_url);
                }
                else
                {
                    //显式授权                    
                    return View();
                }
            }
            else//未登录
            {
                if (app.IS_INTERNAL)
                {
                    return View("Internal_Login");
                }
                //登录后授权
                return View();
            }
        }
        /// <summary>
        /// 显式授权
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="scope"></param>
        /// <param name="state"></param>
        /// <param name="redirect_uri"></param>
        /// <param name="user_code"></param>
        /// <param name="login_pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Authorize(string appid, string scope, string state, string redirect_uri, string user_code, string login_pwd, GrantCodeRight[] grants, int takeAll)
        {
            string device_id = Request.Headers["device-id"];
            OAuthApp app = OAuthAppCache.Instance.Find(it => it.APP_CODE.Equals(appid));
            GrantScope scopeModel = ScopeCache.Instance.Find(it => it.SCOPE_CODE.Equals(scope));
            if (app == null)
            {
                return View("fatal", FuncResult.FailResult("未注册的应用"));
            }
            if (scopeModel == null)
            {
                return View("fatal", FuncResult.FailResult("无效的授权范围"));
            }
            if (!this.OAuthContext.IsLogined)
            {
                if (string.IsNullOrEmpty(user_code))
                {
                    return View("fatal", FuncResult.FailResult("必须输入账号"));
                }
                if (string.IsNullOrEmpty(login_pwd))
                {
                    return View("fatal", FuncResult.FailResult("必须输入密码"));
                }
                string message;
                if (!this.UserLogin(user_code, login_pwd, app.APP_ID, out message))
                {
                    return View("fatal", FuncResult.FailResult(message));
                }
            }
            user_code = this.OAuthContext.UserInfo.UserCode;
            GrantProvider grant = new GrantProvider(appid, user_code, scope, device_id ?? this.OAuthContext.Device_Id);
            if (!grant.Grant(takeAll == 1, grants))
            {
                return View("fatal", FuncResult.FailResult("授权失败，请重试"));
            }
            string return_url = xUtils.CombinaRedirectUri(redirect_uri, state, grant.Auth_Code);
            return Redirect(return_url);
        }
        /// <summary>
        /// 获取用户OPEN_ID
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="grant_type"></param>
        /// <returns></returns>
        public ActionResult AccessToken(string appid, string secret, string code, string state, string grant_type)
        {
            OpenOAuthProvider provider = new OpenOAuthProvider(appid, secret, code, grant_type);
            if (!provider.OAuthAccess())
            {
                var res = FailResult(provider.PromptInfo.CustomMessage);
                res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return res;
            }
            var suc = SuccessResult(provider.OAuthUser);
            suc.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return suc;
        }
        /// <summary>
        /// 刷新授权Token
        /// </summary>
        /// <param name="appid">应用账号</param>
        /// <param name="refresh_token">刷新令牌</param>
        /// <param name="grant_type">授权类型=refresh_token</param>
        /// <returns></returns>
        public ActionResult RefreshToken(string appid, string refresh_token, string grant_type)
        {
            JsonResult jsonRes = null;
            RefreshTokenProvider refresh = new RefreshTokenProvider(appid, refresh_token);
            if (!refresh.Refresh())
            {
                jsonRes = FailResult(refresh.PromptInfo.CustomMessage, (int)refresh.PromptInfo.ResultType);
                jsonRes.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return jsonRes;
            }
            jsonRes = SuccessResult(refresh.OAuthUser);
            jsonRes.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonRes;
        }
    }
}
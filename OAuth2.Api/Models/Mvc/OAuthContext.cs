using OAuth2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Winner.User.Interface;

namespace OAuth2.Api.Models.Mvc
{
    public class OAuthContext
    {
        public const string _USER_LOGIN_SESSION_NAME = "OAuth2.User.Login";
        public IUser UserInfo
        {
            get
            {
                var user = HttpContext.Current.Session[_USER_LOGIN_SESSION_NAME] as IUser;
                return user;
            }
        }
        public OAuthApp CurrentApp { get; set; }
        public bool IsLogined
        {
            get
            {
                return UserInfo != null;
            }
        }

        public string Device_Id
        {
            get
            {
                var installerCookie = HttpContext.Current.Request.Cookies[OAuthControllerBase.COOKIE_NAME];
                return installerCookie?.Value;
            }
        }
    }
}
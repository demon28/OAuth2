using OAuth2.Api.Models.Mvc;
using OAuth2.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuth2.Api.Controllers
{
    public class SnsController : OAuthControllerBase
    {
        // GET: Sns
        public ActionResult Index()
        {
            return View();
        }
        //拉取用户信息
        public ActionResult UserInfo(string access_token, string open_id)
        {
            int userId, appid;
            xUtils.DecryptOpenId(open_id, out userId, out appid);
            return View();
        }
    }
}
using OAuth2.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.WebApi.Contract;
using OAuth2.Facade.Caches;
using OAuth2.Entities;
using OAuth2.Api.Models;
using Winner.Framework.Utils;
using Winner.Framework.Utils.Model;

namespace OAuth2.Api.Controllers
{
    public class ScopeController : ApiControllerBase
    {
        public ActionResult Apis()
        {
            OAuth2.Token.UserToken userToken = Token.UserToken.FromCipherToken(Package.Token);
            Tauth_Token daToken = new Tauth_Token();
            if (!daToken.SelectByAppId_UserId(userToken.AppId, Package.UserId))
            {
                return FailResult("未找到授权访问令牌，Token无效", (int)ApiStatusCode.OPERATOR_FORBIDDEN);
            }
            Tauth_Token_RightCollection daRightCollection = new Tauth_Token_RightCollection();
            daRightCollection.ListEffectiveByTokenId(daToken.Token_Id);
            List<ScopeApiResult> list = MapProvider.Map<ScopeApiResult>(daRightCollection.DataTable);
            if (list == null || list.Count <= 0)
            {
                return Json(FuncResult.SuccessResult(list));
            }
            var apis = from scope in list where scope.Status == 1 select scope.Api_Url;
            return Json(FuncResult.SuccessResult(apis));
        }
    }
}
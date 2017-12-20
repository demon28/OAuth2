using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC.Models;
using Winner.Framework.Utils;
using Winner.Framework.Utils.Model;
using Winner.WebApi.Contract;

namespace OAuth2.Api.Models.Mvc
{
    public class ApiErrorHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Log.Error("控制器异常", filterContext.Exception);
            if (filterContext.Controller is ApiControllerBase)
            {
                var result = new JsonNetResult();
                FuncResult func = new FuncResult();
                func.Success = false;
                func.Message = "服务器异常";
                filterContext.ExceptionHandled = true;
                filterContext.Result = result;
            }
            base.OnException(filterContext);
        }
    }
}
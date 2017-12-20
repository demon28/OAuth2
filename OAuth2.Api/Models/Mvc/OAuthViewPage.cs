using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.Api.Models.Mvc
{
    //OAuth2.Api.Models.Mvc.OAuthViewPage
    public abstract class OAuthViewPage : OAuthViewPage<dynamic>
    {

    }
    public abstract class OAuthViewPage<T> : System.Web.Mvc.WebViewPage<T>
    {
        public OAuthContext OAuthContext { get; set; }
        public override void InitHelpers()
        {
            base.InitHelpers();
            var ctrl = this.ViewContext.Controller as OAuthControllerBase;
            if (ctrl == null || ctrl.OAuthContext == null)
            {
                this.OAuthContext = new OAuthContext();
            }
            else
            {
                this.OAuthContext = ctrl.OAuthContext;
            }
        }
    }
}
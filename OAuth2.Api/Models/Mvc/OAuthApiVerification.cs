using OAuth2.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Winner.Framework.Encrypt;
using Winner.Framework.Utils;
using Winner.WebApi.Contract;

namespace OAuth2.Api.Models.Mvc
{
    public class OAuthApiVerification : DefaultApiVerification
    {
        public override bool VerifySignature(ActionParameter para)
        {
            Merchant.OAuthApp app = Merchant.OAuthApp.GetOAuthApp(para.MerchantNo);
            if (app == null)
            {
                return false;
            }
            if (!app.IsValid())
            {
                return false;
            }
            string secretKey = app.Secret_Key;
            //签名数据
            string signValue = para.Data + secretKey;
            //签名结果
            string signResult = MD5Provider.Encode(signValue);

            //验证签名
            if (!signResult.Equals(para.Sign, StringComparison.CurrentCultureIgnoreCase))
            {
                Log.Info("签名错误：");
                Log.Info("签名数据：" + signValue);
                Log.Info("签名结果：" + signResult);
                return false;
            }
            return true;
        }
    }
}
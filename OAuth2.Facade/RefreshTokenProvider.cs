using OAuth2.DataAccess;
using OAuth2.Entities;
using OAuth2.Facade.Caches;
using OAuth2.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.User.Interface;

namespace OAuth2.Facade
{
    public class RefreshTokenProvider : FacadeBase
    {
        private string _refresh_token;
        private string _appid;
        public RefreshTokenProvider(string appid, string refresh_token)
        {
            this._refresh_token = refresh_token;
            this._appid = appid;
            this.OAuthUser = new UserOpenModel();
            this.OAuthUser.Expire_In = 7200;
        }
        public UserOpenModel OAuthUser { get; private set; }
        public bool Refresh()
        {
            var app = OAuthAppCache.Instance.Find(it => it.APP_CODE == this._appid);
            if (app == null)
            {
                Alert((ResultType)ResponseCode.应用ID无效, "未知的应用ID");
                return false;
            }
            var DecryptRes = xUtils.DecryptAccessToken(this._refresh_token);
            if (!DecryptRes.Success)
            {
                Alert((ResultType)ResponseCode.无效操作, DecryptRes.Message);
                return false;
            }
            UserToken token = DecryptRes.Content;
            if (token.Expire_Time < DateTime.Now)
            {
                Alert((ResultType)ResponseCode.令牌已过期, "令牌已过期，请重新发起用户授权");
                return false;
            }
            Tauth_Token daToken = new Tauth_Token();
            if (!daToken.SelectByAppId_UserId(app.APP_ID, token.UserId))
            {
                Alert((ResultType)ResponseCode.Token错误, "未找到授权记录，无效的刷新令牌");
                return false;
            }
            if (!daToken.Refresh_Token.Equals(this._refresh_token))
            {
                Alert((ResultType)ResponseCode.无效操作, "无效的刷新令牌");
                return false;
            }
            if (daToken.Refresh_Timeout < DateTime.Now)
            {
                Alert((ResultType)ResponseCode.令牌已过期, "令牌已过期，请重新发起用户授权");
                return false;
            }
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser user = fac?.GetUserByID(daToken.User_Id);
            if (user == null)
            {
                Alert("用户不存在");
                return false;
            }
            string newToken = xUtils.EncryptAccessToken(token.UserId, user.UserCode, app.APP_ID);
            daToken.Token_Code = newToken;
            daToken.Expire_Time = DateTime.Now.AddSeconds(this.OAuthUser.Expire_In);
            if (!daToken.Update())
            {
                Alert((ResultType)ResponseCode.服务器错误, "Token刷新失败，请重试");
                return false;
            }
            this.OAuthUser.Open_Id = xUtils.EncryptOpenId(app.APP_ID, token.UserId, app.UID_ENCRYPT_KEY);
            this.OAuthUser.Token = newToken;
            this.OAuthUser.Refresh_Token = this._refresh_token;
            this.OAuthUser.Refresh_Expire_In = (int)(daToken.Refresh_Timeout - DateTime.Now).TotalDays;
            return true;
        }
    }
}

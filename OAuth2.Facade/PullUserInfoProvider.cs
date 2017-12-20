using OAuth2.Entities;
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
    /// <summary>
    /// 拉取用户信息
    /// </summary>
    public class PullUserInfoProvider : FacadeBase
    {
        private string _access_token, _open_id;
        public PullUserInfoProvider(string access_token, string open_id)
        {
            this._access_token = access_token;
            this._open_id = open_id;
            this.UserInfoDictionary = new Dictionary<string, object>();
        }

        public bool GetUserInfo()
        {
            int appid, userId;
            if (!xUtils.DecryptOpenId(this._open_id, out userId, out appid))
            {
                Alert((ResultType)ResponseCode.Token错误, "open_id无效");
                return false;
            }
            UserToken token = UserToken.FromCipherToken(_access_token);
            if (token == null)
            {
                Alert("无效Token");
                return false;
            }
            if (token.Expire_Time < DateTime.Now)
            {
                Alert((ResultType)ResponseCode.令牌已过期, "Token已过期");
                return false;
            }
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser userInfo = fac?.GetUserByID(userId);
            if (userInfo == null)
            {
                Alert("用户已注销或不存在");
                return false;
            }
            UserInfoDictionary.Add("UserName", userInfo.UserName);
            UserInfoDictionary.Add("Avatar", userInfo.Avatar);
            return true;
        }

        public Dictionary<string, object> UserInfoDictionary { get; set; }
    }
}

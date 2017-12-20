using OAuth2.DataAccess;
using OAuth2.Entities;
using OAuth2.Facade.Caches;
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
    /// 获取 OPEN_ID
    /// </summary>
    public class OpenOAuthProvider : FacadeBase
    {
        private string _appid;
        private string _secret;
        private string _auth_code;
        private string _grant_type;
        public OpenOAuthProvider(string appid, string secret, string auth_code, string grant_type)
        {
            this._appid = appid;
            this._secret = secret;
            this._auth_code = auth_code;
            this._grant_type = grant_type;
            this.OAuthUser = new UserOpenModel();
            this.OAuthUser.Expire_In = 7200;
            this.OAuthUser.Refresh_Expire_In = 30;
        }
        public UserOpenModel OAuthUser { get; private set; }
        public bool OAuthAccess()
        {
            var app = OAuthAppCache.Instance.Find(it => it.APP_CODE.Equals(this._appid));
            if (app == null)
            {
                Alert("无效的应用编号");
                return false;
            }
            Tauth_Code daCode = new Tauth_Code();
            if (!daCode.SelectByAppId_GrantCode(app.APP_ID, this._auth_code))
            {
                Alert("无效的授权码");
                return false;
            }
            if (daCode.Status == 1)
            {
                Alert("该授权码已被使用，不能重复使用");
                return false;
            }
            if (daCode.Expire_Time < DateTime.Now)
            {
                Alert("授权码已过期");
                return false;
            }
            daCode.Status = 1;
            if (!daCode.Update())
            {
                Alert("授权码验证失败");
                return false;
            }
            int user_id = daCode.User_Id;
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser user = fac?.GetUserByID(user_id);
            if (user == null)
            {
                Alert("用户不存在");
                return false;
            }
            string open_id = xUtils.EncryptOpenId(app.APP_ID, user_id, app.UID_ENCRYPT_KEY);
            this.OAuthUser.Open_Id = open_id;
            this.OAuthUser.Token = xUtils.EncryptAccessToken(user_id, user.UserCode, app.APP_ID);
            this.OAuthUser.Refresh_Token = xUtils.EncryptAccessToken(user_id, user.UserCode, app.APP_ID, 2592000);
            BeginTransaction();
            Tauth_Token daToken = new Tauth_Token();
            daToken.ReferenceTransactionFrom(Transaction);
            bool exist = daToken.SelectByAppId_UserId(app.APP_ID, user_id);
            daToken.App_Id = app.APP_ID;
            daToken.Expire_Time = DateTime.Now.AddSeconds(this.OAuthUser.Expire_In);
            daToken.Refresh_Timeout = DateTime.Now.AddDays(this.OAuthUser.Refresh_Expire_In);
            daToken.Refresh_Token = this.OAuthUser.Refresh_Token;
            daToken.Token_Code = this.OAuthUser.Token;
            daToken.Scope_Id = daCode.Scope_Id;
            daToken.User_Id = user_id;
            daToken.Grant_Id = daCode.Auth_Id;
            if (exist)
            {
                if (!daToken.Update())
                {
                    Rollback();
                    Alert("TOKEN生成失败");
                    return false;
                }
            }
            else
            {
                if (!daToken.Insert())
                {
                    Rollback();
                    Alert("TOKEN生成失败");
                    return false;
                }
            }
            if (!UpdateTokenRights(daToken.Token_Id, daToken.Refresh_Timeout, daCode.Right_Json))
            {
                Rollback();
                return false;
            }
            Commit();
            return true;
        }
        private bool UpdateTokenRights(int tokenId, DateTime timeout, string rightJson)
        {
            if (string.IsNullOrEmpty(rightJson))
            {
                Log.Info("TokenId={0},无API授权信息", tokenId);
                return true;
            }
            try
            {
                List<GrantCodeRight> rights = Javirs.Common.Json.JsonSerializer.Deserializer<List<GrantCodeRight>>(rightJson);
                if (rights == null || rights.Count <= 0)
                {
                    return true;
                }
                List<int> apis = new List<int>();
                foreach (GrantCodeRight gcr in rights)
                {
                    if (gcr.RightType == 0)//Group
                    {
                        apis.AddRange(GetGroupApis(gcr.RightId));
                    }
                    else//api-info
                    {
                        apis.Add(gcr.RightId);
                    }
                }
                if (!AddOrUpdate(tokenId, timeout, apis))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("更新令牌权限失败", ex);
                Alert("更新令牌权限失败");
                return false;
            }
        }
        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="expireTime"></param>
        /// <param name="apis"></param>
        /// <returns></returns>
        public bool AddOrUpdate(int tokenId, DateTime expireTime, List<int> apis)
        {
            if (apis == null || apis.Count <= 0)
            {
                return true;
            }
            BeginTransaction();
            foreach (int api_id in apis)
            {
                Tauth_Token_Right daTRight = new Tauth_Token_Right();
                daTRight.ReferenceTransactionFrom(Transaction);
                if (daTRight.SelectByTokenId_ApiId(tokenId, api_id))
                {
                    if (daTRight.Have_Right == 0 || daTRight.Expire_Time < expireTime)
                    {
                        daTRight.Have_Right = 1;
                        daTRight.Expire_Time = expireTime;
                        if (!daTRight.Update())
                        {
                            Rollback();
                            return false;
                        }
                    }
                }
                else
                {
                    daTRight.Api_Id = api_id;
                    daTRight.Expire_Time = expireTime;
                    daTRight.Have_Right = 1;
                    daTRight.Last_Modify_Time = DateTime.Now;
                    daTRight.Token_Id = tokenId;
                    if (!daTRight.Insert())
                    {
                        Rollback();
                        return false;
                    }
                }
            }
            Commit();
            return true;
        }
        private List<int> GetGroupApis(int groupId)
        {
            Tauth_Group_RightCollection daRightsCollection = new Tauth_Group_RightCollection();
            daRightsCollection.ListByGroup_Id(groupId);
            List<int> result = new List<int>();
            foreach (Tauth_Group_Right right in daRightsCollection)
            {
                result.Add(right.Api_Id);
            }
            return result;
        }
    }
}

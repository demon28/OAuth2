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
    /// 用户授权登录提供程序
    /// </summary>
    public class GrantProvider : FacadeBase
    {
        private string _appCode, _userCode, _scope, _device_id;
        /// <summary>
        /// 授权登录提供程序
        /// </summary>
        /// <param name="appCode">app代码</param>
        /// <param name="secret">app密码</param>
        /// <param name="userCode">用户账号</param>
        /// <param name="scope">授权范围</param>
        public GrantProvider(string appCode, string userCode, string scope, string device_id)
        {
            this._appCode = appCode;
            this._userCode = userCode;
            this._scope = scope;
            this._device_id = device_id;
        }
        /// <summary>
        /// 执行授权
        /// </summary>
        /// <returns></returns>
        public bool Grant(bool takeAll, params GrantCodeRight[] rights)
        {
            OAuthApp app = OAuthAppCache.Instance.Find(it => it.APP_CODE.Equals(this._appCode));
            if (app == null)
            {
                Alert("未注册的应用");
                return false;
            }
            GrantScope[] scope = ScopeCache.Instance.FindAll(this._scope);
            if (scope == null || scope.Length <= 0)
            {
                Alert("未定义的授权类型");
                return false;
            }
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser user = fac?.GetUserByCode(this._userCode);
            if (user == null)
            {
                Alert("用户信息加载失败");
                return false;
            }
            if (CheckAlreadyAuth(app.APP_ID, user.UserId))
            {
                return true;
            }
            if (takeAll && (rights == null || rights.Length <= 0))
            {
                var temp = ScopeRightProvider.GetScopeRights(this._scope);
                rights = new GrantCodeRight[temp.Count];
                for (int i = 0; i < rights.Length; i++)
                {
                    rights[i] = new GrantCodeRight
                    {
                        RightId = temp[i].Right_Id,
                        RightType = temp[i].Right_Type
                    };
                }
            }
            this.Auth_Code = Guid.NewGuid().ToString("N");
            Tauth_Code daCode = new Tauth_Code();
            daCode.App_Id = app.APP_ID;
            daCode.Expire_Time = DateTime.Now.AddMinutes(5);
            daCode.Grant_Code = this.Auth_Code;
            daCode.Scope_Id = scope.FirstOrDefault().SCOPE_ID;
            daCode.User_Id = user.UserId;
            daCode.Device_Id = this._device_id;
            if (rights != null && rights.Length > 0)
            {
                daCode.Right_Json = Javirs.Common.Json.JsonSerializer.JsonSerialize(rights);
            }
            if (!daCode.Insert())
            {
                Alert("授权失败，请重试！");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检查是否已经授权了
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool CheckAlreadyAuth(int appid, int userId)
        {
            Tauth_Code daCode = new Tauth_Code();
            if (!daCode.SelectByAppid_UserId(appid, userId))
            {
                return false;
            }
            if (daCode.Expire_Time < DateTime.Now)
            {
                return false;
            }
            if (daCode.Status == 1)
            {
                return false;
            }
            this.Auth_Code = daCode.Grant_Code;
            return true;
        }
        /// <summary>
        /// 授权码
        /// </summary>
        public string Auth_Code { get; set; }
    }
}

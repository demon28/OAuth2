using OAuth2.DataAccess;
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
    public class LoginProvider : FacadeBase
    {
        private string _user_code;
        private string _password;
        public LoginProvider(string user_code, string password)
        {
            this._user_code = user_code;
            this._password = password;
        }
        /// <summary>
        /// 忽略密码检查
        /// </summary>
        internal bool IgnorePassword { get; set; }
        public bool Login(int client_source, string client_system, string device_id, string ip_address, string session_id, string clientVersion, int appid)
        {
            var fac = UserModuleFactory.GetUserModuleInstance();
            if (fac == null)
            {
                Alert("加载用户模块失败");
                return false;
            }

            this.User = fac.GetUserByCode(_user_code);
            if (this.User == null)
            {
                Alert("用户未注册");
                return false;
            }
            var lockResult = this.User.IsLocked(Winner.User.Interface.Lock.LockRight.登陆);
            if (lockResult.IsLocked)
            {
                Alert((ResultType)403, lockResult.Reason);
                return false;
            }
            if (!IgnorePassword && !this.User.CheckLoginPassword(_password))
            {
                Alert(this.User.PromptInfo.Message);
                return false;
            }
            this.Token = xUtils.EncryptAccessToken(this.User.UserId, this.User.UserCode, appid);
            Tauth_Session daSession = new Tauth_Session
            {
                Client_Source = client_source,
                Client_System = client_system,
                Device_Id = device_id,
                Ip_Address = ip_address,
                Session_Id = session_id,
                Status = 1,
                User_Id = this.User.UserId,
                Token = this.Token,
                Client_Version = clientVersion
            };
            if (!daSession.Insert())
            {
                Alert("保存登录会话失败");
                return false;
            }
            Logined();
            return true;
        }
        public IUser User { get; set; }
        public event Action<IUser> OnLogined;
        public string Token { get; private set; }
        private void Logined()
        {
            if (OnLogined != null)
            {
                var delegates = OnLogined.GetInvocationList();
                foreach (Delegate d in delegates)
                {
                    Action<IUser> act = d as Action<IUser>;
                    act?.Invoke(this.User);
                }
            }
        }
    }
}

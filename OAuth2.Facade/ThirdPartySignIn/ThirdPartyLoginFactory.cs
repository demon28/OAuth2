using OAuth2.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils.Model;
using Winner.User.Interface;

namespace OAuth2.Facade.ThirdPartySignIn
{
    public class ThirdPartyLoginResult
    {
        public string UserCode { get; set; }
        public string Token { get; set; }
    }
    public class ThirdPartyLoginProvider : FacadeBase
    {
        private string _openID;
        private Entities.ThirdPartyLogin _thirdParty;
        public ThirdPartyLoginProvider(Entities.ThirdPartyLogin thirdParty, string openID)
        {
            this._openID = openID;
            this._thirdParty = thirdParty;
        }

        public FuncResult<ThirdPartyLoginResult> Login(Winner.WebApi.Contract.ApiPackage package, string ipAddress, string session_id, int appId)
        {
            //ThirdParty、OpenId
            bool isExist = false;
            int userId = 0;
            Tnet_User_Auth daAuth = new Tnet_User_Auth();
            if (!(isExist = daAuth.SelectByThirdparty_OpenId((int)this._thirdParty, this._openID)))
            {
                //if not exist
                //add one
                userId = GetNewUserId();
                daAuth.Open_Id = this._openID;
                daAuth.Status = 1;
                daAuth.Thirdparty = (int)this._thirdParty;
                daAuth.User_Id = userId;
                if (!daAuth.Insert())
                {
                    //Alert("登录失败，保存登录信息异常");
                    return FuncResult.FailResult<ThirdPartyLoginResult>("登录失败，保存登录信息异常");
                }
            }
            else
            {
                userId = daAuth.User_Id;
            }
            if (!isExist)
            {
                return FuncResult.SuccessResult((ThirdPartyLoginResult)null);
            }
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser user = fac.GetUserByID(userId);
            if (user == null)
            {
                return FuncResult.SuccessResult((ThirdPartyLoginResult)null);
            }
            LoginProvider localLogin = new LoginProvider(user.UserCode, null);
            localLogin.IgnorePassword = true;
            if (!localLogin.Login(package.ClientSource, package.ClientSystem, package.Device_Id, ipAddress, session_id, package.ClientVersion, appId))
            {
                return FuncResult.FailResult<ThirdPartyLoginResult>(localLogin.PromptInfo.CustomMessage, (int)localLogin.PromptInfo.ResultType);
            }
            var data = new ThirdPartyLoginResult
            {
                Token = localLogin.Token,
                UserCode = user.UserCode
            };
            return FuncResult.SuccessResult(data);
        }
        private static int GetNewUserId()
        {
            Tnet_User daUser = new Tnet_User();
            return daUser.GetNewUserId();
        }
    }

    #region Plan
    public class ThirdPartyLoginFactory
    {
        public static IThirdPartyLoginBehavior GetLoginBehavior(Entities.ThirdPartyLogin thirdParty)
        {
            IThirdPartyLoginBehavior behavior = null;
            switch (thirdParty)
            {
                case Entities.ThirdPartyLogin.微信登录:
                    behavior = new WechatLoginProvider();
                    break;
                case Entities.ThirdPartyLogin.QQ登录:
                    behavior = new QQLoginProvider();
                    break;
                case Entities.ThirdPartyLogin.新郎微博登录:
                    behavior = new WeiboLoginProvider();
                    break;
            }
            return behavior;
        }
    }
    public interface IThirdPartyLoginBehavior
    {
    }



    public class QQLoginProvider : IThirdPartyLoginBehavior
    {

    }

    public class WechatLoginProvider : IThirdPartyLoginBehavior
    {

    }

    public class WeiboLoginProvider : IThirdPartyLoginBehavior
    {

    }
    #endregion
}

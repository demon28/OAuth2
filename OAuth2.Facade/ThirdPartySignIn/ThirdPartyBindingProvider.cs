using OAuth2.DataAccess;
using OAuth2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.User.Interface;

namespace OAuth2.Facade.ThirdPartySignIn
{
    /// <summary>
    /// 第三方登录绑定手机号业务
    /// </summary>
    public class ThirdPartyBindingProvider : FacadeBase
    {
        private ThirdPartyBindingModel _model;
        /// <summary>
        /// 第三方登录绑定手机号业务
        /// </summary>
        public ThirdPartyBindingProvider(ThirdPartyBindingModel model)
        {
            this._model = model;
        }

        public bool Register()
        {
            string[] array = new string[2];
            string plainText;
            if (!xUtils.RsaDecryptPayPwd(_model.OpenID, out plainText))
            {
                Alert("OpenID解密失败");
                return false;
            }
            int pos = plainText.IndexOf('_');
            array[0] = plainText.Substring(0, pos);
            array[1] = plainText.Substring(pos + 1);
            long timestamp;
            if (!long.TryParse(array[0], out timestamp))
            {
                Alert("OpenID解密失败");
                return false;
            }
            long currentTime = xUtils.GetCurrentTimeStamp();
            if (currentTime - timestamp > 120)
            {
                Alert("请求已过期");
                return false;
            }
            string openID = array[1];
            SmsValidateProvider smsValidate = new SmsValidateProvider(_model.UserCode, SmsValidateType.绑定手机号);
            if (!smsValidate.ValidateCode(_model.ValidateCode))
            {
                Alert(smsValidate.PromptInfo);
                return false;
            }
            BeginTransaction();
            Tnet_User_Auth daAuth = new Tnet_User_Auth();
            daAuth.ReferenceTransactionFrom(Transaction);
            if (!daAuth.SelectByThirdparty_OpenId(_model.ThirdParty, openID))
            {
                Rollback();
                Log.Info("未找到第三方账号注册信息");
                Alert("绑定手机号失败");
                return false;
            }
            var fac = UserModuleFactory.GetUserModuleInstance();
            if (fac == null)
            {
                Rollback();
                Alert("系统繁忙，请稍后重试");
                Log.Info("加载用户模块失败");
                return false;
            }
            IUser user = fac.GetUserByCode(_model.UserCode);
            if (user == null)//如果为空注册一个新会员
            {
                if (string.IsNullOrEmpty(_model.Password))
                {
                    Rollback();
                    Alert("对新会员登录密码是必须的");
                    return false;
                }
                UserCreationProvider userCreation = new UserCreationProvider(_model.UserCode, _model.Password, _model.RefereeCode, daAuth.User_Id);
                userCreation.ReferenceTransactionFrom(Transaction);
                if (!userCreation.AddUser(_model.NickName, _model.Avatar))
                {
                    Rollback();
                    Alert(userCreation.PromptInfo);
                    return false;
                }
            }
            bool needUpdate = false;
            if (user != null && daAuth.User_Id != user.UserId)
            {
                if (IsAlreadyBind(user.UserId, _model.ThirdParty))
                {
                    Rollback();
                    Alert($"您的账号已经绑定了{((ThirdPartyLogin)_model.ThirdParty).ToString()}");
                    return false;
                }
                needUpdate = true;
                daAuth.User_Id = user.UserId;
            }
            if (!string.IsNullOrEmpty(_model.Avatar))
            {
                needUpdate = true;
                daAuth.Avatar = _model.Avatar;
            }
            if (!string.IsNullOrEmpty(_model.NickName))
            {
                needUpdate = true;
                daAuth.User_Name = _model.NickName;
            }

            if (needUpdate && !daAuth.Update())
            {
                Rollback();
                Alert("绑定第三方登录账号失败");
                return false;
            }
            Commit();
            return true;
        }

        private static bool IsAlreadyBind(int userId, int thirdparty)
        {
            Tnet_User_Auth daAuth = new Tnet_User_Auth();
            return daAuth.SelectByUserId_Thirdparty(userId, thirdparty);
        }
    }
}

using OAuth2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Core.Facade;
using Winner.SmsCenter.Client;
using Winner.User.Interface;
using Winner.User.Interface.Enums;

namespace OAuth2.Facade
{
    public class SmsValidateProvider : FacadeBase
    {
        public const string SMS_ACCOUNT_SETTING_NAME = "SMS.ACCOUNT";
        public const string SMS_PWD_SETTING_NAME = "SMS.PASSWORD";
        private string GID;
        private string _userCode;
        private SmsValidateType _validationType;
        public SmsValidateProvider(string userCode, SmsValidateType validationType)
        {
            this._userCode = userCode;
            this._validationType = validationType;
            this.GID = GetSmsGID(validationType);
        }
        public IUser User { get; set; }
        public bool SendCode()
        {
            string smsAccount = Winner.ConfigurationManager.ConfigurationProvider.GetString(SMS_ACCOUNT_SETTING_NAME);
            string smsPassword = Winner.ConfigurationManager.ConfigurationProvider.GetString(SMS_PWD_SETTING_NAME);
            var fac = UserModuleFactory.GetUserModuleInstance();
            this.User = fac?.GetUserByCode(this._userCode);
            if (_validationType == SmsValidateType.注册)
            {
                if (User != null)
                {
                    Alert("手机号已被注册");
                    return false;
                }
            }
            else if (_validationType == SmsValidateType.重置支付密码 || _validationType == SmsValidateType.重置登录密码)
            {
                if (User == null)
                {
                    Alert("手机号未注册");
                    return false;
                }
            }
            SmsServiceClient client = new SmsServiceClient(smsAccount, smsPassword);
            if (!client.SendValidateCode(this._userCode, GID, null))
            {
                Alert(client.Message);
                return false;
            }
            return true;
        }
        public bool ValidateCode(string code)
        {
            string smsAccount = Winner.ConfigurationManager.ConfigurationProvider.GetString(SMS_ACCOUNT_SETTING_NAME);
            string smsPassword = Winner.ConfigurationManager.ConfigurationProvider.GetString(SMS_PWD_SETTING_NAME);

            SmsServiceClient client = new SmsServiceClient(smsAccount, smsPassword);
            if (!client.ValidateCode(_userCode, GID, code))
            {
                Alert(client.Message);
                return false;
            }
            return true;
        }

        private static string GetSmsGID(SmsValidateType validateType)
        {
            string gid = string.Empty;
            switch (validateType)
            {
                case SmsValidateType.注册:
                    gid = "78d281b57529e76803bc685e86797ccb";
                    break;
                case SmsValidateType.重置登录密码:
                    gid = "2F8DD4EBE71F4F2B831E894BC5B790E9";
                    break;
                case SmsValidateType.重置支付密码:
                    gid = "E894BC5B790E92F8DD4EBE71F4F2B831";
                    break;
                case SmsValidateType.绑定手机号:
                    gid = "1782eedab9234d81a6b6a9f94c61769e";
                    break;
            }
            return gid;
        }
    }
}

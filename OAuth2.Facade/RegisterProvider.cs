using OAuth2.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils.Model;
using Winner.User.Interface;
using Winner.User.Interface.Enums;

namespace OAuth2.Facade
{
    public class RegisterProvider : FacadeBase
    {
        private string _userCode, _password, _smsCode, _refereeCode;
        public RegisterProvider(string userCode, string password, string smsCode, string refereeCode)
        {
            this._userCode = userCode;
            this._password = password;
            this._smsCode = smsCode;
            this._refereeCode = refereeCode;
        }
        public bool Register(params Action<IUser>[] listeners)
        {
            SmsValidateProvider validate = new SmsValidateProvider(this._userCode, Entities.SmsValidateType.注册);
            if (!validate.ValidateCode(this._smsCode))
            {
                Alert(validate.PromptInfo);
                return false;
            }
            UserCreationProvider userCreation = new UserCreationProvider(this._userCode, this._password, this._refereeCode);
            if (listeners != null || listeners.Length > 0)
            {
                foreach (var lst in listeners)
                {
                    userCreation.Success += new Action<IUser>(lst);
                }
            }
            if (!userCreation.AddUser())
            {
                return false;
            }
            return true;
        }
    }
}

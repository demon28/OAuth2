using OAuth2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.User.Interface;
using Winner.User.Interface.Enums;

namespace OAuth2.Facade.Verifications
{
    public class PasswordValidVerification : IIdentityVerification
    {
        private IUser _user;
        private string _password;
        private PasswordType _passwordType;
        public PasswordValidVerification(IUser user, string password, PasswordType pwdType)
        {
            this._user = user;
            this._password = password;
            this._passwordType = pwdType;
        }
        public string ErrorMessage
        {
            get; protected set;
        }

        public bool Verify()
        {
            bool res = false;
            if (PasswordType.登录密码 == this._passwordType)
            {
                res = this._user.CheckLoginPassword(_password);
            }
            else
            {
                res = this._user.CheckPayPassword(_password);
            }
            ErrorMessage = res ? null : "旧密码不正确";
            return res;
        }

    }
}

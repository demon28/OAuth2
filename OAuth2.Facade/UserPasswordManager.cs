using OAuth2.DataAccess;
using OAuth2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;

namespace OAuth2.Facade
{
    public class UserPasswordManager : FacadeBase
    {
        private PasswordManagerArgs _arg;
        public UserPasswordManager(PasswordManagerArgs arg)
        {
            this._arg = arg;
        }

        public bool Alter()
        {
            if (this._arg == null)
            {
                Alert("系统错误，修改密码失败");
                return false;
            }
            if (this._arg.Pwd_Manager == null)
            {
                Alert("系统错误，修改密码失败");
                return false;
            }
            if (this._arg.Verification == null)
            {
                Alert("无法验证用户");
                return false;
            }
            if (!this._arg.Verification.Verify())
            {
                Alert(this._arg.Verification.ErrorMessage);
                return false;
            }
            BeginTransaction();
            Tnet_Pwd_Change_His daPwdChangehis = new Tnet_Pwd_Change_His();
            daPwdChangehis.ReferenceTransactionFrom(Transaction);
            daPwdChangehis.User_Id = this._arg.UserId;
            daPwdChangehis.Old_Pwd = this._arg.Pwd_Manager.GetOldPassword(this._arg.Pwd_Type);
            daPwdChangehis.New_Pwd = this._arg.Pwd_Manager.EncryptPassword(this._arg.NewPassword);
            daPwdChangehis.Pwd_Type = (int)this._arg.Pwd_Type;
            daPwdChangehis.Alter_Place = this._arg.Use_Place;
            daPwdChangehis.Remarks = this._arg.Remarks;
            daPwdChangehis.Alter_Source = this._arg.AlterSource;
            if (!daPwdChangehis.Insert())
            {
                Rollback();
                return false;
            }
            if (!UpdatePassword())
            {
                Rollback();
                return false;
            }
            Commit();
            return true;
        }
        private bool UpdatePassword()
        {
            try
            {
                return this._arg.Pwd_Manager.UpdatePassword(this._arg.Pwd_Type, this._arg.NewPassword);
            }
            catch (Exception ex)
            {
                Log.Error("修改密码失败", ex);
                return false;
            }
        }
    }
}

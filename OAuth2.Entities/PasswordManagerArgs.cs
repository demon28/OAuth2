using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.User.Interface;
using Winner.User.Interface.Enums;

namespace OAuth2.Entities
{
    public class PasswordManagerArgs
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
        public PasswordType Pwd_Type { get; set; }
        public IIdentityVerification Verification { get; set; }
        public IPasswordManager Pwd_Manager { get; set; }

        public string AlterSource { get; set; }
        public string Use_Place { get; set; }
        public string Remarks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.DataAccess
{
    public partial class Tnet_User
    {
        public int GetNewUserId()
        {
            string sql = "SELECT SEQ_TNET_USER.NEXTVAL FROM DUAL";
            return GetSequence(sql);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.DataAccess
{
    public partial class Tauth_Code
    {
        /// <summary>
        /// 查询有效的授权码
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool SelectByAppid_UserId(int appid, int userId)
        {
            string sql_condition = string.Format("{0}=:{0} AND {1}=:{1} AND {2}>SYSDATE", _APP_ID, _USER_ID, _EXPIRE_TIME);
            AddParameter(_APP_ID, appid);
            AddParameter(_USER_ID, userId);
            return SelectByCondition(sql_condition);
        }
    }
    public partial class Tauth_CodeCollection
    {

    }
}

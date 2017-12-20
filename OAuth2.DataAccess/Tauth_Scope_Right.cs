using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.DataAccess
{
    public partial class Tauth_Scope_Right
    {
    }
    public partial class Tauth_Scope_RightCollection
    {
        /// <summary>
        /// 查询作用域的权限列表
        /// </summary>
        /// <param name="scopeArray"></param>
        /// <returns></returns>
        public bool ListFKJoin(params int[] scopeArray)
        {
            if (scopeArray == null || scopeArray.Length <= 0)
            {
                return false;
            }
            string sql = @"SELECT * FROM (
SELECT TSR.*,TAI.API_NAME FROM TAUTH_SCOPE_RIGHT TSR JOIN TAUTH_API_INFO TAI ON TSR.REF_ID=TAI.API_ID AND TSR.REF_TYPE=1
UNION ALL
SELECT TSR.*,TAG.GROUP_NAME FROM TAUTH_SCOPE_RIGHT TSR JOIN TAUTH_API_GROUP TAG ON TSR.REF_ID=TAG.GROUP_ID AND TSR.REF_TYPE=0
) TMP WHERE 1=1";
            if (scopeArray != null && scopeArray.Length > 0)
            {
                string ids = string.Join<int>(",", scopeArray);
                sql += $" AND TMP.SCOPE_ID IN ({ids})";
            }
            return ListBySql(sql);
        }
    }
}

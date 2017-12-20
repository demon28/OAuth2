using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.DataAccess
{
    public partial class Tauth_Token_Right
    {
    }
    public partial class Tauth_Token_RightCollection
    {
        /// <summary>
        /// 查询指定Token的有效权限
        /// </summary>
        /// <returns></returns>
        public bool ListEffectiveByTokenId(int tokenId)
        {
            string sql = $"SELECT TTR.*,TAI.API_URL,TAI.API_NAME,TAI.STATUS FROM TAUTH_TOKEN_RIGHT TTR LEFT JOIN TAUTH_API_INFO TAI ON TTR.API_ID=TAI.API_ID WHERE TTR.{Tauth_Token_Right._EXPIRE_TIME}>SYSDATE AND TTR.{Tauth_Token_Right._TOKEN_ID}=:{Tauth_Token_Right._TOKEN_ID} AND TTR.{Tauth_Token_Right._HAVE_RIGHT}=1 AND TAI.STATUS=1";
            AddParameter(Tauth_Token_Right._TOKEN_ID, tokenId);
            return ListBySql(sql);

        }

        public bool ListAllByTokenId(int tokenId)
        {
            string sql_condition = $"{Tauth_Token_Right._TOKEN_ID}=:{Tauth_Token_Right._TOKEN_ID}";
            AddParameter(Tauth_Token_Right._TOKEN_ID, tokenId);
            return ListByCondition(sql_condition);
        }
    }
}

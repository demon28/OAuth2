using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Utils;

namespace OAuth2.DataAccess
{
    public partial class Tauth_Scope
    {
    }
    public partial class Tauth_ScopeCollection
    {
        public bool ListByScopes(params string[] scopes)
        {
            if (scopes == null || scopes.Length <= 0)
            {
                return true;
            }
            string sql = null;
            if (scopes != null && scopes.Length > 0)
            {
                for (int i = 0; i < scopes.Length; i++)
                {
                    if (i != 0)
                    {
                        sql += " OR ";
                    }
                    sql += $"{Tauth_Scope._SCOPE_CODE}=:{Tauth_Scope._SCOPE_CODE}_{i}";
                    AddParameter($"{Tauth_Scope._SCOPE_CODE}_{i}", scopes[i]);
                }
            }
            return ListByCondition(sql);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.DataAccess
{
    public partial class Vauth_Scope_Right
    {
    }
    public partial class Vauth_Scope_RightCollection
    {
        public bool ListByScope(params int[] scopeIds)
        {
            if (scopeIds == null || scopeIds.Length <= 0)
            {
                return true;
            }
            string condition = "";
            for (int i = 0; i < scopeIds.Length; i++)
            {
                if (i != 0)
                {
                    condition += " OR ";
                }
                condition += $" {Vauth_Scope_Right._SCOPE_ID}=:{Vauth_Scope_Right._SCOPE_ID}_{i}";
                AddParameter($"{Vauth_Scope_Right._SCOPE_ID}_{i}", scopeIds[i]);
            }
            return ListByCondition(condition);
        }
    }
}

using OAuth2.DataAccess;
using OAuth2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Utils;

namespace OAuth2.Facade.Caches
{
    public class ScopeCache : CacheContainer<GrantScope>
    {
        public static ScopeCache Instance
        {
            get
            {
                return new ScopeCache();
            }
        }
        protected override List<GrantScope> LoadData()
        {
            Tauth_ScopeCollection daScopeColl = new Tauth_ScopeCollection();
            daScopeColl.ListAll();
            var list = MapProvider.Map<GrantScope>(daScopeColl.DataTable);
            return list;
        }

        public GrantScope[] FindAll(string scopes)
        {
            List<GrantScope> result = new List<GrantScope>();
            string[] array = null;
            if (scopes.Contains(','))
            {
                array = scopes.Split(',');
            }
            else
            {
                array = new string[] { scopes };
            }
            foreach (string key in array)
            {
                var scopeCache = this.Find(it => string.Equals(it.SCOPE_CODE, key, StringComparison.OrdinalIgnoreCase));
                if (scopeCache != null)
                {
                    result.Add(scopeCache);
                }
            }
            return result.ToArray();
        }
    }
}

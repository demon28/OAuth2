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
    public class OAuthAppCache : CacheContainer<OAuthApp>
    {
        public static OAuthAppCache Instance
        {
            get
            {
                return new OAuthAppCache();
            }
        }
        protected override List<OAuthApp> LoadData()
        {
            Tauth_AppCollection daAppCollection = new Tauth_AppCollection();
            daAppCollection.ListAll();
            var list = MapProvider.Map<OAuthApp>(daAppCollection.DataTable);
            return list;
        }
    }
}

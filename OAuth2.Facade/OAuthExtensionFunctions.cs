using OAuth2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Facade
{
    public static class OAuthExtensionFunctions
    {
        /// <summary>
        /// 是否包含需要显示授权的授权域
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool HasExpllicit(this GrantScope[] array)
        {
            if (array == null)
            {
                return false;
            }
            int expllicit = 0;
            foreach (GrantScope item in array)
            {
                if (item.IS_EXPLLICIT)
                {
                    expllicit++;
                }
            }
            return expllicit > 0;
        }
    }
}

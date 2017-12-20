using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Entities
{
    public enum ResponseCode
    {

        令牌已过期 = 401,
        Token错误 = 402,
        无效操作 = 403,
        应用ID无效 = 405,
        服务器错误 = 500,
    }
}

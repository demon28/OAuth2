using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Entities
{
    public enum SmsValidateType
    {
        注册 = 0,
        重置登录密码 = 1,
        重置支付密码 = 2,
        绑定手机号 = 3
    }
}

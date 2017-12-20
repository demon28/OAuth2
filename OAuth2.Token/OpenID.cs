using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Token
{
    /// <summary>
    /// 开放会员ID
    /// </summary>
    public class OpenID
    {
        /// <summary>
        /// 接入APPID
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// OpenID的隐式转换
        /// </summary>
        /// <param name="openId"></param>
        public static implicit operator OpenID(string openId)
        {
            int appid, userId;
            if (!EncodeDecodeOpenID.DecryptOpenId(openId, out userId, out appid))
            {
                return null;
            }
            OpenID open = new OpenID
            {
                AppId = appid,
                UserId = userId
            };
            return open;
        }
    }
}

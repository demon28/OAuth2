using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Entities
{
    /// <summary>
    /// 用户开放信息
    /// </summary>
    public class UserOpenModel
    {
        /// <summary>
        /// 开放用户ID
        /// </summary>
        public string Open_Id { get; set; }
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 访问令牌过期时间（秒），默认7200秒
        /// </summary>
        public int Expire_In { get; set; }
        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string Refresh_Token { get; set; }
        /// <summary>
        /// 刷新令牌过期时间（天），默认30天
        /// </summary>
        public int Refresh_Expire_In { get; set; }
    }
}

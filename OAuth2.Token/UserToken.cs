using Javirs.Common;
using Javirs.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Token
{
    /// <summary>
    /// 用户票证
    /// </summary>
    public class UserToken
    {
        /// <summary>
        /// TOKEN加密密钥
        /// </summary>
        public const string SECRET = "8F718FD018F7186343A843A";
        /// <summary>
        /// 票证过期时间
        /// </summary>
        public DateTime Expire_Time { get; set; }
        /// <summary>
        /// 该票证对应的用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 票证对应的用户账号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// true可以对Token进行DB验证
        /// </summary>
        public bool Verifiable { get; set; }
        /// <summary>
        /// Token对应的APP
        /// </summary>
        public int AppId
        {
            get;
            set;
        }
        /// <summary>
        /// 从TOKEN中解密数据
        /// </summary>
        /// <param name="cipherToken"></param>
        /// <returns></returns>
        public static UserToken FromCipherToken(string cipherToken)
        {
            byte[] input = Base58.Decode(cipherToken);
            var des = new DesEncodeDecode(SECRET, System.Security.Cryptography.CipherMode.ECB, System.Security.Cryptography.PaddingMode.PKCS7, true);
            byte[] output = des.DesDecrypt(input);
            string plainText = Encoding.UTF8.GetString(output);
            string[] array = plainText.Split('|');
            int seconds;
            int user_id, appid;
            if (!int.TryParse(array[0], out seconds))
            {
                return null;
            }
            TimeStamp timestamp = seconds;
            if (!int.TryParse(array[1], out user_id))
            {
                return null;
            }
            if (!int.TryParse(array[3], out appid))
            {
                return null;
            }
            UserToken userToken = new UserToken
            {
                Expire_Time = timestamp.LocalDate,
                UserCode = array[2],
                UserId = user_id,
                AppId = appid,
                Verifiable = (array.Length > 3) ? (array[3] == "1") : false
            };
            return userToken;
        }
        /// <summary>
        /// 获取加密TOKEN
        /// </summary>
        /// <returns></returns>
        public string ToCipherToken()
        {
            Encoding utf8 = Encoding.UTF8;
            TimeStamp stamp = this.Expire_Time;
            string plainText = string.Format("{0}|{1}|{2}|{3}|{4}", (int)stamp.Seconds, this.UserId, this.UserCode,
                this.AppId.ToString().PadLeft(5, '0'), this.Verifiable ? 1 : 0);
            var des = new DesEncodeDecode(SECRET, System.Security.Cryptography.CipherMode.ECB, System.Security.Cryptography.PaddingMode.PKCS7, true, utf8);
            byte[] input = utf8.GetBytes(plainText);
            byte[] output = des.DesEncrypt(input);
            string base58 = Base58.Encode(output);
            return base58;
        }
    }
}

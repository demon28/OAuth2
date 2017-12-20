using Javirs.Common;
using Javirs.Common.Security;
using OAuth2.DataAccess;
using OAuth2.Entities;
using OAuth2.Facade.Caches;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Winner.Framework.Utils;
using Winner.Framework.Utils.Model;

namespace OAuth2.Facade
{
    public static class xUtils
    {
        //public const string _COMMON_KEY = "E39ACC95A2EA4806BD58FFC89181A568";
        public static string CombinaUrl(string host, string query)
        {
            if (host.IndexOf('?') > -1)
            {
                host += "&";
            }
            else
            {
                host += "?";
            }
            return host + query;
        }

        public static string CombinaRedirectUri(string host, string state, string auth_code)
        {
            string query = string.Format("code={0}&state={1}", auth_code, state);
            return CombinaUrl(host, query);
        }

        public static string EncryptOpenId(int appid, int userId, string key)
        {
            string enc_uuid;
            DesEncrypt(userId.ToString().PadLeft(8, '0'), key, out enc_uuid);
            string open_id;
            DesEncrypt(appid.ToString().PadLeft(8, '0') + "_" + enc_uuid, OAuth2.Token.UserToken.SECRET, out open_id);
            return open_id;
        }
        public static bool DecryptOpenId(string open_id, out int userId, out int appid)
        {
            string tokenSecret = OAuth2.Token.UserToken.SECRET;
            string enc_uuid;
            DesDecrypt(open_id, tokenSecret, out enc_uuid);
            var array = enc_uuid.Split('_');
            int lambdaAppid = appid = Convert.ToInt32(array[0]);
            var app = OAuthAppCache.Instance.Find(it => it.APP_ID == lambdaAppid);
            string plain_uid;
            DesDecrypt(array[1], app.UID_ENCRYPT_KEY, out plain_uid);
            userId = Convert.ToInt32(plain_uid);
            return true;
        }
        public static bool DesEncrypt(string plainText, string key, out string cipherText)
        {
            Encoding utf8 = Encoding.UTF8;
            try
            {
                DesEncodeDecode des = new DesEncodeDecode(key,
                        System.Security.Cryptography.CipherMode.ECB,
                        System.Security.Cryptography.PaddingMode.PKCS7,
                        true, utf8);
                byte[] input = utf8.GetBytes(plainText);
                //var base64 = des.DesEncrypt(plainText);
                //var bytes = Convert.FromBase64String(base64);
                //cipherText = bytes.Byte2HexString();
                byte[] output = des.DesEncrypt(input);
                cipherText = Base58.Encode(output);
                return true;
            }
            catch (Exception ex)
            {
                cipherText = ex.Message;
                return false;
            }
        }
        public static bool DesDecrypt(string cipherText, string key, out string plainText)
        {
            Encoding utf8 = Encoding.UTF8;
            try
            {
                DesEncodeDecode des = new DesEncodeDecode(key,
                        System.Security.Cryptography.CipherMode.ECB,
                        System.Security.Cryptography.PaddingMode.PKCS7,
                        true, utf8);
                //var bytes = cipherText.HexString2ByteArray();
                byte[] input = Base58.Decode(cipherText);
                //var base64 = Convert.ToBase64String(bytes);
                byte[] output = des.DesDecrypt(input);
                //plainText = des.DesDecrypt(base64);
                plainText = utf8.GetString(output);
                return true;
            }
            catch (Exception ex)
            {
                plainText = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 加密信息获得TOKEN
        /// </summary>
        /// <param name="userId">会员ID</param>
        /// <param name="userCode">会员账号</param>
        /// <param name="seconds">TOKEN有效期（秒），默认一天86400秒</param>
        /// <returns></returns>
        public static string EncryptAccessToken(int userId, string userCode, int appid, int seconds = 86400)
        {
            var token = new OAuth2.Token.UserToken
            {
                UserCode = userCode,
                UserId = userId,
                Expire_Time = DateTime.Now.AddSeconds(seconds),
                AppId = appid
            };
            string cipherText = token.ToCipherToken();
            return cipherText;
        }
        public static FuncResult<OAuth2.Token.UserToken> DecryptAccessToken(string access_token)
        {
            var token = OAuth2.Token.UserToken.FromCipherToken(access_token);
            return FuncResult.SuccessResult(token);
        }
        public static bool RsaDecryptPayPwd(string Hex_password, out string plainText)
        {
            try
            {
                string fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "yxhpaypwd_private_key.pem");
                Log.Debug("证书保存路径：" + fullpath);
                var rsa = PemCertificate.ReadFromPemFile(fullpath);
                byte[] cipherBytes = Hex_password.HexString2ByteArray();
                string base64 = Convert.ToBase64String(cipherBytes);
                plainText = rsa.Decrypt(base64);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("支付密码解密失败", ex);
                plainText = null;
                return false;
            }
        }
        public static string GetClientSource(int source)
        {
            string comsource = "UNKNOWN";
            switch (source)
            {
                case 1:
                    comsource = "Android";
                    break;
                case 2:
                    comsource = "iOS";
                    break;
                case 3:
                    comsource = "PC";
                    break;
            }
            return comsource;
        }

        public static long GetCurrentTimeStamp()
        {
            var initiateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime now = DateTime.Now;
            var ts = now - initiateTime;
            return (long)ts.TotalSeconds;
        }
        /// <summary>
        /// 对指定作用域是否已经授权过
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        /// <param name="scope"></param>
        /// <returns>authorized is true,otherwise false.</returns>
        public static bool IsAlreayAuthorized(int appId, int userId, string scope)
        {
            Log.Info("申请授权的APPID={0},USER_ID={1},SCOPE={2}", appId, userId, scope);
            string cacheKey = string.Concat(appId, "-", userId, "-", scope);
            IEnumerable<int>[] value = GetCache(cacheKey) as IEnumerable<int>[];
            if (value == null)
            {
                int res = ForceLoadUserGrantRight(appId, userId, scope, out value);
                if (res == -1)//从未授权过
                {
                    return false;
                }
                if (res == 0)//不需要检查value了
                {
                    return true;
                }
                #region obsolute code
                /*
                value = new List<int>[2];
                Tauth_Token daToken = new Tauth_Token();
                if (!daToken.SelectByAppId_UserId(appId, userId))
                {
                    Log.Info("未找到授权记录");
                    return false;
                }
                string[] scopeArray = null;
                if (scope.Contains(","))
                {
                    scopeArray = scope.Split(',');
                }
                else
                {
                    scopeArray = new string[] { scope };
                }
                var scopeRights = ScopeRightProvider.GetScopeApis(scopeArray);
                //如果作用域不包含任何权限（仅OpenID），返回已经授权过
                if (scopeRights == null || scopeRights.Count <= 0)
                {
                    Log.Info("授权作用域不包含任何权限");
                    return true;
                }
                Log.Info("授权作用域包含权限数量{0}", scopeRights.Count);
                var tmp = new List<int>();
                foreach (var sr in scopeRights)
                {
                    tmp.Add(sr.Api_Id);
                }
                value[0] = tmp;
                //value[0] = scopeRights.Select(it => it.Api_Id);
                Tauth_Token_RightCollection daRightCollection = new Tauth_Token_RightCollection();
                daRightCollection.ListEffectiveByTokenId(daToken.Token_Id);

                List<TokenRightApi> apis = MapProvider.Map<TokenRightApi>(daRightCollection.DataTable);
                Log.Info("已经获得的权限有{0}个", apis?.Count);
                var tmp2 = new List<int>();
                foreach (var a in apis)
                {
                    tmp2.Add(a.Api_Id);
                }
                value[1] = tmp2;
                */
                #endregion
                AddOrUpdate(cacheKey, value);
            }
            var scopeApiIds = value[0]; //from x in scopeRights select x.Api_Id;
            var tokenApis = value[1]; //from y in apis select y.Api_Id;
            var result = scopeApiIds.Intersect(tokenApis);//获取两个集合的交集
            Log.Info("交集数量:{0}", result?.Count());
            return result != null && result.Count() == scopeApiIds.Count();//交集不为空并且交集长度与授权API长度相等，认为已全部授权

        }
        public static void UpdateUserGrantRightCache(string key)
        {
            try
            {
                if (!key.Contains("-"))
                {
                    return;
                }
                string pattern = "^(\\d+)-(\\d+)-(.+)$";
                if (!Regex.IsMatch(key, pattern))
                {
                    return;
                }
                int appid = Convert.ToInt32(Regex.Replace(key, pattern, "$1"));
                int userid = Convert.ToInt32(Regex.Replace(key, pattern, "$2"));
                string scope = Regex.Replace(key, pattern, "$3");
                IEnumerable<int>[] value;
                int res = ForceLoadUserGrantRight(appid, userid, scope, out value);
                if (res == 1)
                {
                    AddOrUpdate(key, value);
                    return;
                }
            }
            catch
            {

            }
        }
        public static int ForceLoadUserGrantRight(int appId, int userId, string scope, out IEnumerable<int>[] value)
        {
            value = new List<int>[2];
            try
            {
                Tauth_Token daToken = new Tauth_Token();
                if (!daToken.SelectByAppId_UserId(appId, userId))
                {
                    Log.Info("未找到授权记录");
                    return -1;
                }
                string[] scopeArray = null;
                if (scope.Contains(","))
                {
                    scopeArray = scope.Split(',');
                }
                else
                {
                    scopeArray = new string[] { scope };
                }
                var scopeRights = ScopeRightProvider.GetScopeApis(scopeArray);
                //如果作用域不包含任何权限（仅OpenID），返回已经授权过
                if (scopeRights == null || scopeRights.Count <= 0)
                {
                    Log.Info("授权作用域不包含任何权限");
                    return 1;
                }
                Log.Info("授权作用域包含权限数量{0}", scopeRights.Count);
                var tmp = new List<int>();
                foreach (var sr in scopeRights)
                {
                    tmp.Add(sr.Api_Id);
                }
                value[0] = tmp;
                //value[0] = scopeRights.Select(it => it.Api_Id);
                Tauth_Token_RightCollection daRightCollection = new Tauth_Token_RightCollection();
                daRightCollection.ListEffectiveByTokenId(daToken.Token_Id);

                List<TokenRightApi> apis = MapProvider.Map<TokenRightApi>(daRightCollection.DataTable);
                Log.Info("已经获得的权限有{0}个", apis?.Count);
                var tmp2 = new List<int>();
                foreach (var a in apis)
                {
                    tmp2.Add(a.Api_Id);
                }
                value[1] = tmp2;
                return 0;
            }
            catch (Exception ex)
            {
                Log.Error("检查是否已授权出现错误", ex);
                return -1;
            }
        }
        public static object GetCache(string cacheKey)
        {
            return System.Runtime.Caching.MemoryCache.Default.Get(cacheKey);
        }

        public static object AddOrUpdate(string cacheKey, object value)
        {
            System.Runtime.Caching.CacheItemPolicy policy = new System.Runtime.Caching.CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddDays(1);
            object existing = System.Runtime.Caching.MemoryCache.Default.AddOrGetExisting(cacheKey, value, policy);
            if (existing != null)
            {
                System.Runtime.Caching.MemoryCache.Default[cacheKey] = value;
            }
            return value;
        }
    }
}

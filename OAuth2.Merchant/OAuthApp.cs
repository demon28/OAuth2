using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Utils;

namespace OAuth2.Merchant
{
    /// <summary>
    /// 授权登录APP
    /// </summary>
    public class OAuthApp
    {
        private static List<OAuthApp> apps = new List<OAuthApp>();
        /// <summary>
        /// 授权登录APP
        /// </summary>
        protected OAuthApp() { }
        /// <summary>
        /// 获取授权登录app信息
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public static OAuthApp GetOAuthApp(string appCode)
        {
            LoadData();
            OAuthApp app = apps.Find(it => string.Equals(appCode, it.App_Code, StringComparison.OrdinalIgnoreCase));
            if (app == null)
            {
                LoadData(true);
                app = apps.Find(it => string.Equals(appCode, it.App_Code, StringComparison.OrdinalIgnoreCase));
            }
            return app;
        }
        /// <summary>
        /// 获取授权登录app信息
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static OAuthApp GetOAuthApp(int appid)
        {
            LoadData();
            OAuthApp app = apps.Find(it => appid == it.App_Id);
            if (app == null)
            {
                LoadData(true);
                app = apps.Find(it => it.App_Id == appid);
            }
            return app;
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return this.Status == 1;
        }
        private static void LoadData(bool force = false)
        {
            if (apps == null || apps.Count <= 0 || force)
            {
                Tauth_AppCollection daAppCollection = new Tauth_AppCollection();
                daAppCollection.ListAll();
                apps.Clear();
                foreach (Tauth_App daApp in daAppCollection)
                {
                    OAuthApp app = new OAuthApp
                    {
                        App_Code = daApp.App_Code,
                        Access_Token = daApp.Access_Token,
                        App_Host = daApp.App_Host,
                        App_Id = daApp.App_Id,
                        App_Name = daApp.App_Name,
                        Create_Time = daApp.Create_Time,
                        Is_Internal = daApp.Is_Internal,
                        Logo_Url = daApp.Logo_Url,
                        Remarks = daApp.Remarks,
                        Secret_Key = daApp.Secret_Key,
                        Status = daApp.Status,
                        UID_Encrypt_Key = daApp.Uid_Encrypt_Key
                    };
                    apps.Add(app);
                }
            }
        }

        #region properties
        /// <summary>
        /// appid
        /// </summary>
        public int App_Id { get; set; }
        /// <summary>
        /// app代码，唯一
        /// </summary>
        public string App_Code { get; set; }
        /// <summary>
        /// app名称
        /// </summary>
        public string App_Name { get; set; }
        /// <summary>
        /// app主机域名
        /// </summary>
        public string App_Host { get; set; }
        /// <summary>
        /// 签名密钥
        /// </summary>
        public string Secret_Key { get; set; }
        /// <summary>
        /// uid加密密钥
        /// </summary>
        public string UID_Encrypt_Key { get; set; }
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string Access_Token { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Create_Time { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 是否内部应用
        /// </summary>
        public int Is_Internal { get; set; }
        /// <summary>
        /// 应用logo地址
        /// </summary>
        public string Logo_Url { get; set; }
        #endregion
    }
}

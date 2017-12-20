using OAuth2.DataAccess;
using OAuth2.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Utils;

namespace OAuth2.Facade
{
    /// <summary>
    /// 授权域权限提供者
    /// </summary>
    public class ScopeRightProvider
    {
        /// <summary>
        /// 获取作用域权限列表（API,GROUP）
        /// </summary>
        /// <param name="scopeArray"></param>
        /// <returns></returns>
        public static List<ScopeRightResult> GetScopeRights(params int[] scopeArray)
        {
            if (scopeArray == null || scopeArray.Length <= 0)
            {
                return new List<ScopeRightResult>();
            }
            if (scopeArray.Length > 20)
            {
                return null;
            }
            Tauth_Scope_RightCollection daRightsCollection = new Tauth_Scope_RightCollection();
            daRightsCollection.ListFKJoin(scopeArray);
            if (daRightsCollection.Count <= 0)
            {
                return new List<ScopeRightResult>();
            }
            List<ScopeRightResult> results = MapProvider.Map<ScopeRightResult>(daRightsCollection.DataTable);
            return results;
        }
        /// <summary>
        /// 获取作用域权限列表（API,GROUP）
        /// </summary>
        /// <param name="scopeCodes"></param>
        /// <returns></returns>
        public static List<ScopeRightResult> GetScopeRights(params string[] scopeCodes)
        {
            Tauth_ScopeCollection daScopeCollection = new Tauth_ScopeCollection();
            daScopeCollection.ListByScopes(scopeCodes);
            if (daScopeCollection.Count <= 0)
            {
                return null;
            }
            List<GrantScope> list = MapProvider.Map<GrantScope>(daScopeCollection.DataTable);
            var scopeids = list.Select(it => it.SCOPE_ID);
            return GetScopeRights(scopeids.ToArray());
        }
        /// <summary>
        /// 获取作用域权限列表，GROUP转换为API，仅返回API
        /// </summary>
        /// <param name="scopeCodes"></param>
        /// <returns></returns>
        public static List<ScopeApi> GetScopeApis(params string[] scopeCodes)
        {
            Tauth_ScopeCollection daScopeCollection = new Tauth_ScopeCollection();
            daScopeCollection.ListByScopes(scopeCodes);
            if (daScopeCollection.Count <= 0)
            {
                return null;
            }
            List<GrantScope> list = MapProvider.Map<GrantScope>(daScopeCollection.DataTable);
            var scopeids = list.Select(it => it.SCOPE_ID);
            return GetScopeApis(scopeids.ToArray());
        }
        /// <summary>
        /// 获取作用域权限列表，GROUP转换为API，仅返回API
        /// </summary>
        /// <param name="scopeIds"></param>
        /// <returns></returns>
        public static List<ScopeApi> GetScopeApis(params int[] scopeIds)
        {
            Vauth_Scope_RightCollection daRightCollection = new Vauth_Scope_RightCollection();
            daRightCollection.ListByScope(scopeIds);
            List<ScopeApi> list = MapProvider.Map<ScopeApi>(daRightCollection.DataTable);
            return list;
        }
    }
    public class ScopeApi
    {
        public int Api_Id { get; set; }
        public string Api_Name { get; set; }
        public string Api_Url { get; set; }
        public string Scope_Id { get; set; }
    }
    public class ScopeRightResult
    {
        [MapMember("REF_ID")]
        public int Right_Id { get; set; }
        [MapMember("REF_TYPE")]
        public int Right_Type { get; set; }
        public string Api_Name { get; set; }
    }
}

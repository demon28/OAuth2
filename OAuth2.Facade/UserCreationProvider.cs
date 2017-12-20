using OAuth2.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils.Model;
using Winner.User.Interface;
using Winner.User.Interface.Enums;

namespace OAuth2.Facade
{
    /// <summary>
    /// 创建会员账号业务
    /// </summary>
    public class UserCreationProvider : FacadeBase
    {
        private string _refereeCode, _password, _userCode;
        private int? _userId;
        /// <summary>
        /// 创建会员账号业务
        /// </summary>
        /// <param name="userCode">会员账号</param>
        /// <param name="password">指定登录密码</param>
        /// <param name="refereeCode">指定推荐人</param>
        /// <param name="userId">指定会员ID</param>
        public UserCreationProvider(string userCode, string password, string refereeCode, int? userId = null)
        {
            this._userCode = userCode;
            this._password = password;
            this._refereeCode = refereeCode;
            this._userId = userId;
        }
        /// <summary>
        /// 新增会员
        /// </summary>
        /// <returns></returns>
        public bool AddUser(string userName = null, string avatar = null)
        {
            var funcres = GetIntroducerId(this._refereeCode);
            if (!funcres.Success)
            {
                Alert(funcres.Message);
                return false;
            }
            int? refereeId = funcres.Content;
            int userId = this._userId.HasValue ? this._userId.Value : GetNewUserId();
            string loginPwd = EncodePwd(_password, userId, _userCode);
            string paypwd = null;
            BeginTransaction();
            Tnet_User daReg = new Tnet_User
            {
                User_Code = _userCode,
                Login_Password = loginPwd,
                Payment_Password = paypwd,
                User_Name = userName ?? _userCode,
                User_Status = (int)UserStatus.已激活,
                User_Level = 0,
                Father_Id = refereeId,
                Auth_Status = 0,
                Data_Source = 1,
                Mobile_No = _userCode,
                Photo_Url = avatar,
                User_Nickname = userName
            };
            daReg.ReferenceTransactionFrom(Transaction);
            if (!daReg.Insert(userId))
            {
                Rollback();
                Alert("注册新会员失败");
                return false;
            }
            Commit();
            return true;
        }
        private static string EncodePwd(string password, int userId, string user_code)
        {
            return Winner.Framework.Encrypt.SafePassword.Encode(userId, password);
        }
        private int GetNewUserId()
        {
            Tnet_User daReg = new Tnet_User();
            return daReg.GetNewUserId();
        }

        private static FuncResult<int?> GetIntroducerId(string refereeCode)
        {
            var result = new FuncResult<int?>();
            if (string.IsNullOrEmpty(refereeCode))
            {
                result.Success = true;
                result.Content = null;
                return result;
            }
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser refereeUser = fac?.GetUserByCode(refereeCode);
            int? refereeId = refereeUser?.UserId;
            if (!refereeId.HasValue)
            {
                result.Success = false;
                result.Message = "推荐人账号不存在";
                return result;
            }
            result.Success = true;
            result.Content = refereeId;
            return result;
        }

        public event Action<IUser> Success;
        private void OnSuccess(IUser user)
        {
            if (Success != null)
            {
                var delegates = Success.GetInvocationList();//获取委托链逐个调用，避免失败中断执行链表
                foreach (var del in delegates)
                {
                    try
                    {
                        Action<IUser> act = (Action<IUser>)del;
                        act.Invoke(user);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

    }
}

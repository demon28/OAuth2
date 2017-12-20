using OAuth2.Api.Areas.Api.Models;
using OAuth2.Entities;
using OAuth2.Entities.ViewModels;
using OAuth2.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.Utils;
using Winner.Framework.Utils.Model;
using Winner.User.Interface;
using Winner.WebApi.Contract;
using Javirs.Common;
using Winner.User.Interface.Enums;

namespace OAuth2.Api.Areas.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        public JsonResult Modify(UserModifyModel model)
        {
            FuncResult result = new FuncResult();
            UserProfileModifyProvider uf = new UserProfileModifyProvider();
            result.Success = uf.ModifyProfile(model);
            result.Message = result.Success ? null : uf.PromptInfo.CustomMessage;
            result.StatusCode = result.Success ? 1 : (int)uf.PromptInfo.ResultType;
            return Json(result);
        }
        [IgnoreUserToken]
        public JsonResult SendValidCode(int PwdType)
        {
            Log.Info("UserCode={0}&PWDTYPE={1}", Package.UserCode, PwdType);
            FuncResult result = new FuncResult();
            SmsValidateProvider valid = new SmsValidateProvider(Package.UserCode, (SmsValidateType)PwdType);
            result.Success = valid.SendCode();
            result.Message = result.Success ? null : valid.PromptInfo.CustomMessage;
            result.StatusCode = result.Success ? 1 : (int)valid.PromptInfo.ResultType;
            return Json(result);
        }
        [IgnoreUserToken]
        public JsonResult ResetPassword(PasswordResetModel model)
        {
            Log.Debug(model.ToLineText());
            var fac = UserModuleFactory.GetUserModuleInstance();
            if (fac == null)
            {
                return Json(FuncResult.FailResult("系统错误", 500));
            }
            string newPwd = model.New_Pwd;
            string validateCode = model.ValidateCode;
            if (model.PwdType == (int)PasswordType.支付密码)
            {
                if (!xUtils.RsaDecryptPayPwd(model.New_Pwd, out newPwd))
                {
                    return Json(FuncResult.FailResult("新密码解密失败", (int)ApiStatusCode.DECRYPT_PASSWORD_FAIL));
                }
                if (model.ValidateType == (int)IdentityValidateType.旧密码验证)
                {
                    if (!xUtils.RsaDecryptPayPwd(model.ValidateCode, out validateCode))
                    {
                        return Json(FuncResult.FailResult("旧密码解密失败", (int)ApiStatusCode.DECRYPT_PASSWORD_FAIL));
                    }
                }
            }
            IUser user = fac.GetUserByCode(Package.UserCode);
            if (user == null)
            {
                return FailResult("用户账号[" + Package.UserCode + "]不存在");
            }
            PasswordType passwordType = (PasswordType)model.PwdType;
            var validateType = (IdentityValidateType)model.ValidateType;
            IIdentityVerification verification = IdentityVerificationFactory.GetVerification(validateType, user, passwordType, validateCode);
            if (verification == null)
            {
                return Json(FuncResult.FailResult("指定的身份验证方式不正确", 409));
            }

            IPasswordManager pwdmgt = fac.GetPasswordManager(user);
            PasswordManagerArgs arg = new PasswordManagerArgs
            {
                AlterSource = xUtils.GetClientSource(this.Package.ClientSource),
                NewPassword = newPwd,
                Pwd_Manager = pwdmgt,
                Pwd_Type = passwordType,
                Remarks = string.Format("通过{0}修改", validateType.ToString()),
                UserId = user.UserId,
                Use_Place = this.Package.ClientSystem,
                Verification = verification
            };
            FuncResult result = new FuncResult();
            UserPasswordManager manager = new UserPasswordManager(arg);
            result.Success = manager.Alter();
            result.Message = result.Success ? null : manager.PromptInfo.CustomMessage;
            result.StatusCode = result.Success ? 1 : (int)manager.PromptInfo.ResultType;
            return Json(result);
        }
    }
}
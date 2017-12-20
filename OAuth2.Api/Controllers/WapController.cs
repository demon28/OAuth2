using Javirs.Common.Security;
using OAuth2.DataAccess;
using OAuth2.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC.Controllers;
using Winner.Framework.Utils.Model;
using Winner.User.Interface;
using Winner.User.Interface.Enums;

namespace OAuth2.Api.Controllers
{
    public class WapController : TopControllerBase
    {
        [HttpGet]
        public ActionResult Register(string id)
        {
            int? refer_id = null;
            if (!string.IsNullOrEmpty(id))
            {
                string plainText = Encoding.UTF8.GetString(Base58.Decode(id));
                int r_id;
                if (!int.TryParse(plainText, out r_id))
                {
                    return RedirectToAction("result", new { errMessage = "链接已失效" });
                }
                refer_id = r_id;
            }
            IUser referUser = null;
            ViewBag.Referee_Code = string.Empty;
            ViewBag.Referee_Name = string.Empty;
            if (refer_id.HasValue)
            {
                var fac = UserModuleFactory.GetUserModuleInstance();
                referUser = fac?.GetUserByID(refer_id.Value);
                if (referUser != null)
                {
                    ViewBag.Referee_Code = referUser.UserCode;
                    ViewBag.Referee_Name = referUser.UserName;
                }
            }
            return View();
        }
        public JsonResult CheckInviter(string refereeCode)
        {
            if (string.IsNullOrEmpty(refereeCode))
            {
                return FailResult("请输入邀请人账号");
            }
            Tnet_User daUser = new Tnet_User();
            if (!daUser.SelectByUserCode(refereeCode))
            {
                return FailResult("输入的邀请人账号未注册");
            }
            if (daUser.User_Status != (int)UserStatus.已激活)
            {
                return FailResult("邀请人账号状态异常");
            }
            var data = new
            {
                User_Code = daUser.User_Code,
                User_Name = daUser.User_Name
            };
            return SuccessResult(data);
        }

        public JsonResult SendValidCode(string mobileNo)
        {
            SmsValidateProvider registerFacade = new SmsValidateProvider(mobileNo, Entities.SmsValidateType.注册);
            if (!registerFacade.SendCode())
            {
                return FailResult(registerFacade.PromptInfo.CustomMessage);
            }
            return SuccessResult();
        }
        [HttpPost]
        public JsonResult Register(string sms_code, string mobileno, string loginpwd, string referee_code, string validate_code)
        {
            var registerFacade = new RegisterProvider(mobileno, loginpwd, sms_code, referee_code);
            if (!registerFacade.Register())
            {
                return FailResult(registerFacade.PromptInfo.CustomMessage, 400);
            }
            return SuccessResult();
        }
        public ActionResult Result(string id)
        {
            return View(id);
        }

        public ActionResult DownApp()
        {
            return View();
        }
    }
}
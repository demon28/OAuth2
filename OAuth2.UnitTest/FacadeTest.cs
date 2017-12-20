using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Javirs.Common.Net;
using System.Diagnostics;
using Javirs.Common.Json;
using OAuth2.Facade;
using System.Text.RegularExpressions;
using Javirs.Common;
using Javirs.Common.Security;
using System.Text;

namespace OAuth2.UnitTest
{
    [TestClass]
    [NUnit.Framework.TestFixture]
    public class FacadeTest
    {
        [TestMethod]
        [NUnit.Framework.Test(Description = "测试获取AccessToken功能")]
        public void GetAccessToken()
        {
            string uri = "http://localhost:3560/connect/accesstoken";
            //fulluri = http://localhost:3560/connect/accesstoken?appid=ckzg&secret=E0DF7C2EDF6D499D8A6E39644340F8AE&code=&grant_type=authorization_code
            string appid = "ckzg";
            string secret = "E0DF7C2EDF6D499D8A6E39644340F8AE";
            string code = "fe472b2fe19748a189de889a393cc6ec";
            string query = string.Format("appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
            HttpHelper http = new HttpHelper(uri);
            var response = http.Get(query);
            Debug.WriteLine(response);
            JsonObject Json = JsonObject.Parse(response);
            Assert.IsTrue(Json.GetBoolean("Success"));
        }

        [TestMethod]
        public void EncodeOpenID()
        {
            int userId = 4000000;
            int appid = 1;
            string uuid_key = "40FDD43F5A074FFE852E4A54DB5C05C2";
            string openId = xUtils.EncryptOpenId(appid, userId, uuid_key);
            Debug.WriteLine("openID=" + openId);
            Assert.IsFalse(string.IsNullOrEmpty(openId));
        }
        [TestMethod]
        public void DecodeOpenID()
        {
            string open_id = "APu8aK54gmm1pTT9V82jDg9xLJ9yq3gAzkVVrRer4HqE";
            //string open_id = "FceW4xrN7yjqcaTsfrakQekjfTFMx6gux";//Dj5hgj5ZiANukpwogjDTBfCESn39ikgSM
            int userid, appid;
            bool res = xUtils.DecryptOpenId(open_id, out userid, out appid);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void CleanUrl()
        {
            string service = "http://admin.ver.yxhv3.t01.pw:81/?code=3eea604b1a3b4a6f905f8654c1cb7524&ticket=abc&auth_code=1&state=framework&code=d0c21db39693429f97d526f6b4a2e439&state=framework&code=65ed1efaaacb482997485e6d1f33ae04&state=framework&code=cd82c126dc5349218136e0abdc1e62c8&state=framework&code=4538dcb96ebd45c9a229b376e2bc3785&state=framework&code=51463aae633f4ddaab71556e978bc4cf&state=framework&code=2869bd0e8e864ee4aa3b938e87edb6a6&state=framework&code=2869bd0e8e864ee4aa3b938e87edb6a6&state=framework&code=2869bd0e8e864ee4aa3b938e87edb6a6&state=framework&code=2869bd0e8e864ee4aa3b938e87edb6a6&state=framework&code=2869bd0e8e864ee4aa3b938e87edb6a6&state=framework";
            service = Regex.Replace(service, "(?<=\\?|&)(code|state)=[^&]+[&]?", "");
            //service = Regex.Replace(service, "(?<=\\?|&)state=[^&]+[&]?", "");
            service = service.TrimEnd('?', '&');
            Debug.WriteLine(service);
        }

        [TestMethod]
        public void OauthAppTest()
        {
            Merchant.OAuthApp app = Merchant.OAuthApp.GetOAuthApp("appversioncontrol");
            Assert.IsTrue(app != null);
        }
        [TestMethod]
        public void Base58Test()
        {
            string userCode = "3999999";
            string result = Base58.Encode(Encoding.UTF8.GetBytes(userCode));
            Debug.WriteLine(result);
        }
        [TestMethod]
        public void AnasysUriTest()
        {
            Uri uri = new Uri("http://api.user.yxhv3.t01.pw:81/user/info?debug=true");
            string lineText = uri.ToLineText();
            Debug.WriteLine(lineText);
        }

        [TestMethod]
        public void SubstringTest()
        {
            string s = "ssss_111232";
            int pos = s.IndexOf('_');
            string[] array = new string[2];
            array[0] = s.Substring(0, pos).PadLeft(2, '0');
            array[1] = s.Substring(pos + 1);
            Debug.WriteLine($"0:{array[0]},1:{array[1]}");
        }

        [TestMethod]
        public void IsAuthorizedAlreadyTest()
        {
            bool res = xUtils.IsAlreayAuthorized(2, 3434950, "userinfo_api");
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void DecodeAccessToken()
        {
            string token = "8biE2RkHfPidk2tMJut9TLS5yk1XgmBM53WS1LjdLz3oiQJsGJKY8NE";
            Token.UserToken userToken = Token.UserToken.FromCipherToken(token);
            Debug.WriteLine(userToken.Expire_Time);
            Assert.IsTrue(userToken != null);
        }
        [TestMethod]
        public void CreateAccessTokenTest()
        {
            Token.UserToken ut = new Token.UserToken
            {
                AppId = 10,
                Expire_Time = DateTime.Now.AddDays(1),
                UserCode = "18576687613",
                UserId = 3441217,
                Verifiable = true
            };
            string token = ut.ToCipherToken();
            Debug.WriteLine("新Token：" + token);
        }
    }
}

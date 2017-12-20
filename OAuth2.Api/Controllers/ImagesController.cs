using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuth2.Api.Controllers
{
    public class ImagesController : Controller
    {
        // GET: Validate
        public ActionResult ValidateCode()
        {
            string validateCode = CreateVildateString(4);
            Session["ValidateCode"] = validateCode;
            var img = CreateValidateCode(validateCode);
            return File(img, "image/jpg");
        }
        //生成随机验证码字符串
        public static string CreateVildateString(int length)
        {
            //设置允许出现的字符
            string chars = "2345689ABCDEFGHJKMNPRSUWXY";

            Random r = new Random(DateTime.Now.Millisecond);
            //随机字符串
            string ValidateString = "";
            for (int i = 0; i < length; i++)
            {
                ValidateString += chars[r.Next(chars.Length)];
            }
            return ValidateString;

        }

        public static byte[] CreateValidateCode(string validateCode)
        {
            //设置场景
            Bitmap bmp = new Bitmap(validateCode.Length * 15, 25);
            //获取绘图对象
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //设置字体
            Font f = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic);
            //设置渐变矩形
            Rectangle r = new Rectangle(0, 0, bmp.Width, bmp.Height);
            //设置渐变刷子
            LinearGradientBrush b = new LinearGradientBrush(r, Color.Red, Color.Blue, 1.2f, true);
            //绘制干扰线
            Random rd = new Random(DateTime.Now.Millisecond);
            Pen pen = new Pen(Color.Silver);
            for (int i = 0; i < 25; i++)
            {
                int StartX = rd.Next(bmp.Width);
                int StartY = rd.Next(bmp.Height);
                int EndX = rd.Next(bmp.Width);
                int EndY = rd.Next(bmp.Height);
                g.DrawLine(pen, StartX, StartY, EndX, EndY);
            }
            //绘制干扰点
            for (int i = 0; i < 100; i++)
            {
                int x = rd.Next(bmp.Width);
                int y = rd.Next(bmp.Height);
                int red = rd.Next(256);
                int green = rd.Next(256);
                int blue = rd.Next(256);
                bmp.SetPixel(x, y, Color.FromArgb(red, green, blue));
            }
            //绘制验证图片
            g.DrawString(validateCode, f, b, 3, 2);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            bmp.Dispose();
            return ms.ToArray();
        }
    }
}
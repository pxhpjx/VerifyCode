using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace VerifyCodeBase
{
    public class Create
    {
        /// <summary>
        /// 获得新验证码
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static ImageInfo GetVerifyCode(ImageCode img, string code, string imageGrade, string ip, string url, string urlrefer, string via)
        {
            ImageInfo imageInfo = new ImageInfo();
            imageInfo.Key = AddValue(code, ip);
            string outputvia = "";
            if (!string.IsNullOrEmpty(via))
                outputvia = via + "viacheck";
            imageInfo.Gps = ConfigurationManager.AppSettings["GPS"].Trim();
            imageInfo.Validate = SimpleEncrypt.Md5(imageInfo.Key, imageInfo.Gps);
            imageInfo.Key = SimpleEncrypt.EncryptString(imageInfo.Key, "yzm");
            imageInfo.Gps = SimpleEncrypt.EncryptString(imageInfo.Gps, "yzm");
            imageInfo.Image = img.CreateImageBytes(code);
            return imageInfo;
        }


        /// <summary>
        /// 插入新value值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        private static string AddValue(string value, string ip)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            string key = "VerifyCode" + DateTime.Now.ToString("HHmmss") + rand.Next(100000, 999999);
            value = SimpleEncrypt.EncryptString(value + "|" + ip + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "cache");
            Couch.Add(key, value, DateTime.Now.AddSeconds(600));
            return key;
        }















    }
}

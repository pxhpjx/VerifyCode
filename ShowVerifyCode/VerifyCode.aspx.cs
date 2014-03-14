using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerifyCodeBase;

namespace ShowVerifyCode
{
    public partial class VerifyCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string FreshKey = Request["v"] == null ? "" : Request["v"].ToString();
            if (HttpRuntime.Cache.Get(FreshKey) == null)
            {
                string ip = HttpContext.Current.Request.UserHostAddress;
                string url = HttpContext.Current.Request.Url.ToString();
                string urlRefer = (HttpContext.Current.Request.UrlReferrer == null) ? "" : HttpContext.Current.Request.UrlReferrer.ToString();
                string via = HttpContext.Current.Request.ServerVariables["HTTP_VIA"];
                ImageCode img = new ImageCode();
                string gradeName = "基础级别";
                string code = img.CreateVerifyCode();
                var imageInfo = Create.GetVerifyCode(img, code, gradeName, ip, url, urlRefer, via);

                HttpCookie cookie = new HttpCookie("VerifyCode");
                cookie.Domain = ConfigurationManager.AppSettings["Verify_Cookie_Domain"];
                cookie.Values.Add("key", imageInfo.Key);
                cookie.Values.Add("gps", imageInfo.Gps);
                cookie.Values.Add("validate", imageInfo.Validate);
                HttpContext.Current.Response.Cookies.Add(cookie);

                Response.ClearContent();
                Response.ContentType = "image/Jpeg";
                Response.BinaryWrite(imageInfo.Image);

                HttpRuntime.Cache.Add(FreshKey, 1111, null, DateTime.Now.AddSeconds(0.5), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }
        }
    }
}
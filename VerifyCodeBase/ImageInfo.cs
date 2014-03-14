using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VerifyCodeBase
{
    public  class ImageInfo
    {
        /// <summary>
        /// 验证码主键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 服务地址
        /// </summary>
        public string Gps { get; set; }
        /// <summary>
        /// 校验值
        /// </summary>
        public string Validate { get; set; }
        /// <summary>
        /// 图像字节流
        /// </summary>
        public byte[] Image { get; set; }
    }
}
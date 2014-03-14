using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace VerifyCodeBase
{
    public class ImageCode
    {
        #region 字符串旋转最大角度
        private float rotateAngle = 20;
        public float RotateAngle
        {
            get { return rotateAngle; }
            set { rotateAngle = value; }
        }
        #endregion

        #region 字符串随机平移长度
        private float transform = 6;
        public float Transform
        {
            get { return transform; }
            set { transform = value; }
        }
        #endregion

        #region 字符串扭曲度, 越大扭曲越厉害
        private float twistMultiValue = 5;
        public float TwistMultiValue
        {
            get { return twistMultiValue; }
            set { twistMultiValue = value; }
        }
        #endregion

        #region 干扰线的字符基点的水平浮动最大值
        private float curveFloatX = 6;
        public float CurveFloatX
        {
            get { return curveFloatX; }
            set { curveFloatX = value; }
        }
        #endregion

        #region 干扰线的字符基点的上下浮动范围
        private float minCouveFloatRate = 8;
        public float MinCouveFloatRate
        {
            get { return minCouveFloatRate; }
            set { minCouveFloatRate = value; }
        }

        private float maxCouveFloatRate = 6;
        public float MaxCouveFloatRate
        {
            get { return maxCouveFloatRate; }
            set { maxCouveFloatRate = value; }
        }
        #endregion

        #region 干扰线画笔范围(范围大于2)
        private float minCurvePen = 9;
        public float MinCurvePen
        {
            get { return minCurvePen; }
            set { minCurvePen = value; }
        }

        private float maxCurvePen = 12;
        public float MaxCurvePen
        {
            get { return maxCurvePen; }
            set { maxCurvePen = value; }
        }
        #endregion

        #region 验证码长度
        int length = 4;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        #endregion

        #region 验证码字体大小
        int fontSize = 25;
        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }
        #endregion

        #region 边框补(默认1像素)
        int padding = 0;
        public int Padding
        {
            get { return padding; }
            set { padding = value; }
        }
        #endregion

        #region 是否带横线干扰线(默认输出)
        bool hline = true;
        public bool Hline
        {
            get { return hline; }
            set { hline = value; }
        }
        #endregion

        #region 带竖干扰线条数
        int eline = 0;
        public int Eline
        {
            get { return eline; }
            set { eline = value; }
        }
        #endregion

        #region 是否带噪点(默认不输出)
        bool chaos = false;
        public bool Chaos
        {
            get { return chaos; }
            set { chaos = value; }
        }
        #endregion

        #region 输出燥点的颜色(默认灰色)
        Color chaosColor = Color.Black;
        public Color ChaosColor
        {
            get { return chaosColor; }
            set { chaosColor = value; }
        }
        #endregion

        #region 自定义背景色(默认白色)
        Color[] backgroundColors = { Color.White };
        //Color[] backgroundColors = { Color.White, Color.FromArgb(248, 248, 248), Color.FromArgb(237, 247, 255), Color.FromArgb(247, 254, 236) };
        public Color[] BackgroundColors
        {
            get { return backgroundColors; }
            set { backgroundColors = value; }
        }
        #endregion

        #region 自定义随机颜色数组
        //Color[] colors = { Color.Black };
        //Color[] colors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        Color[] colors = { Color.FromArgb(27, 104, 160), Color.FromArgb(7, 143, 42), Color.FromArgb(219, 49, 5) };
        public Color[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }
        #endregion

        #region 自定义字体数组
        string[] fonts = { "Arial" };
        //string[] fonts = {  "Arial", 
        //                    "Microsoft YaHei", 
        //                    "STXinwei",
        //                    "STXihei",
        //                    "FZZhunYuan-M02",
        //                    "FZZhunYuan-M02S",
        //                    "FZZhunYuan-M02T",
        //                    "FZYaoTi",
        //                    "FZYaoTi-M06S",
        //                    "FZMeiHei-M07S",
        //                    "SimHei"
        //                 };
        //string[] fonts = { "Arial", "Georgia", "Arial Unicode MS", "MS Mincho", "SimSun-ExtB", "Basemic Symbol", "黑体", "Franklin Gothic Heavy" };
        //string[] fonts = { 
        //                    "Agency FB",
        //                    "Arial",
        //                    "Arial Black",
        //                    "Arial Narrow",
        //                    "Arial Rounded MT Bold",
        //                    "Arial Unicode MS",
        //                    "Baskerville Old Face",
        //                    "Batang",
        //                    "BatangChe",
        //                    "Bodoni MT",
        //                    "Bodoni MT Condensed",
        //                    "Bodoni MT Poster Compressed",
        //                    "Book Antiqua",
        //                    "Bookman Old Style",
        //                    "Britannic Bold",
        //                    "Calibri",
        //                    "Californian FB",
        //                    "Calisto MT",
        //                    "Cambria",
        //                    "Candara",
        //                    "Century",
        //                    "Century Gothic",
        //                    "Century Schoolbook",
        //                    "Comic Sans MS",
        //                    "Consolas",
        //                    "Constantia",
        //                    "Copperplate Gothic Bold",
        //                    "Copperplate Gothic Light",
        //                    "Corbel",
        //                    "Courier New",
        //                    "Dotum",
        //                    "DotumChe",
        //                    "Engravers MT",
        //                    "Eras Bold ITC",
        //                    "Eras Demi ITC",
        //                    "Eras Light ITC",
        //                    "Eras Medium ITC",
        //                    "Estrangelo Edessa",
        //                    "Felix Titling",
        //                    "Franklin Gothic Book",
        //                    "Franklin Gothic Demi",
        //                    "Franklin Gothic Demi Cond",
        //                    "Franklin Gothic Heavy",
        //                    "Franklin Gothic Medium",
        //                    "Franklin Gothic Medium Cond",
        //                    "Garamond",
        //                    "Gautami",
        //                    "Georgia",
        //                    "Gill Sans MT",
        //                    "Gloucester MT Extra Condensed",
        //                    "Goudy Old Style",
        //                    "Gulim",
        //                    "GulimChe",
        //                    "High Tower Text",
        //                    "Impact",
        //                    "Latha",
        //                    "Lucida Bright",
        //                    "Lucida Console",
        //                    "Lucida Fax",
        //                    "Lucida Sans",
        //                    "Lucida Sans Typewriter",
        //                    "Lucida Sans Unicode",
        //                    "Mangal",
        //                    "Microsoft Sans Serif",
        //                    "MS Gothic",
        //                    "MS PGothic",
        //                    "MS Reference Sans Serif",
        //                    "MS UI Gothic",
        //                    "MV Boli",
        //                    "Nina",
        //                    "Palatino Linotype",
        //                    "Perpetua",
        //                    "Perpetua Titling MT",
        //                    "Raavi",
        //                    "Rockwell",
        //                    "Rockwell Condensed",
        //                    "Segoe Condensed",
        //                    "Segoe UI",
        //                    "Shruti",
        //                    "Sylfaen",
        //                    "Tahoma",
        //                    "Tempus Sans ITC",
        //                    "Times New Roman",
        //                    "Trebuchet MS",
        //                    "Tunga",
        //                    "Tw Cen MT",
        //                    "Tw Cen MT Condensed",
        //                    "Tw Cen MT Condensed Extra Bold",
        //                    "Verdana",
        //                    "STZhongsong",
        //                    "STFangsong",
        //                    "STSong",
        //                    "STXinwei",
        //                    "STKaiti",
        //                    "STXihei",
        //                    "SimSun",
        //                    "YouYuan",
        //                    "Microsoft YaHei",
        //                    "NSimSun",
        //                    "FZYaoTi",
        //                    "FZShuTi",
        //                    "LiSu",
        //                    "SimHei"
        //                    };
        //string[] fonts = { "Microsoft YaHei"
        //                 };
        //string[] fonts = {  "Agency FB",
        //                    "Arial",
        //                    "Arial Black",
        //                    "Arial Rounded MT Bold",
        //                    "Bookman Old Style",
        //                    "Britannic Bold",
        //                    "Calibri",
        //                    "Calisto MT",
        //                    "Cambria",
        //                    "Candara",
        //                    "Century Gothic",
        //                    "Century Schoolbook",
        //                    "Comic Sans MS",
        //                    "Copperplate Gothic Bold",
        //                    "Courier New",
        //                    "Corbel",
        //                    "Eras Bold ITC",
        //                    "Eras Demi ITC",
        //                    "Franklin Gothic Demi",
        //                    "Franklin Gothic Heavy",
        //                    "Franklin Gothic Medium",
        //                    "Georgia",
        //                    "Gill Sans MT",
        //                    "Impact",
        //                    "Lucida Bright",
        //                    "Lucida Fax",
        //                    "Lucida Sans",
        //                    "Lucida Sans Typewriter",
        //                    "Lucida Sans Unicode",
        //                    "Nina",
        //                    "Perpetua Titling MT",
        //                    "Rockwell",
        //                    "Segoe UI",
        //                    "Tahoma",
        //                    "Times New Roman",
        //                    "Trebuchet MS",
        //                    "Tw Cen MT",
        //                    "Verdana",
        //                    "Microsoft YaHei",
        //                    "SimHei"
        //                    };
        //string[] fonts = {  "Agency FB",
        //                    "Arial",
        //                    "Arial Rounded MT Bold",
        //                    "Bookman Old Style",
        //                    "Britannic Bold",
        //                    "Calibri",
        //                    "Calisto MT",
        //                    "Cambria",
        //                    "Candara",
        //                    "Century Gothic",
        //                    "Century Schoolbook",
        //                    "Comic Sans MS",
        //                    "Eras Demi ITC",
        //                    "Franklin Gothic Demi",
        //                    "Franklin Gothic Medium",
        //                    "Georgia",
        //                    "Gill Sans MT",
        //                    "Lucida Bright",
        //                    "Lucida Fax",
        //                    "Lucida Sans",
        //                    "Lucida Sans Typewriter",
        //                    "Lucida Sans Unicode",
        //                    "Nina",
        //                    "Rockwell",
        //                    "Segoe UI",
        //                    "Tahoma",
        //                    "Times New Roman",
        //                    "Trebuchet MS",
        //                    "Tw Cen MT",
        //                    "Verdana",
        //                    "Microsoft YaHei"
        //                    };
        public string[] Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }
        #endregion

        #region 自定义随机码字符串序列
        //string codeSerial = "3,4,5,6,7,8,a,b,c,d,e,f,g,h,k,m,n,p,r,s,t,u,v,w,x,y,z,A,B,C,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y";
        //string codeSerial = "a,b,c,d,e,g,h,k,m,n,p,s,u,w,x,y,A,B,C,E,F,G,H,K,M,N,P,Q,R,S,U,V,W,X,Y";
        string codeSerial = "2,3,4,5,6,7,8,9";
        //string codeSerial = "被,美,国,陆,军,研,究,实,验,室,用,于,即,时,模,拟,俄,罗,斯,制,反,导,弹,战";
        public string CodeSerial
        {
            get { return codeSerial; }
            set { codeSerial = value; }
        }
        #endregion

        #region 验证码之间的间隙
        int fpadding = 14;
        public int Fpadding
        {
            get { return fpadding; }
            set { fpadding = value; }
        }
        #endregion

        #region 生成校验码图片

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="code">生成字符串</param>
        /// <returns></returns>
        private Bitmap CreateImageCode(string code)
        {
            Random rand = new Random();
            //放大图片
            int fSize = 100; //字体大小
            int fWidth = 300; // 单个字符串的最大长度
            int leftPadding = rand.Next(0, 40);
            int imageWidth = (int)(code.Length * (fSize + Padding - Fpadding)) + 4 + Padding * 2 + 50;//这里是让图片更长点
            int imageHeight = fSize * 2 + Padding;
            //需要保证以这个字体大小绘图能在这个长宽的图片中完全画出
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight);

            //设置图片绘制方式
            Graphics g = Graphics.FromImage(image);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            //画笔颜色
            int cindex = rand.Next(Colors.Length);
            //增加颜色随机值
            Color selectcolor = colors[cindex];
            int randR = rand.Next(0, 10);
            int randG = rand.Next(0, 10);
            int randB = rand.Next(0, 10);
            Color generatecolor = Color.FromArgb(selectcolor.R + randR > 255 ? selectcolor.R : selectcolor.R + randR, selectcolor.G + randG > 255 ? selectcolor.G : selectcolor.G + randG, selectcolor.B + randB > 255 ? selectcolor.B : selectcolor.B + randB);

            //背景颜色
            int bcindex = rand.Next(backgroundColors.Length);
            Color backgroundcolor = backgroundColors[bcindex];
            g.Clear(backgroundcolor);
            drawString(g, rand, code, fWidth, imageHeight, fSize, generatecolor);

            //画干扰点
            drawPoint(g, rand, image);
            //画干扰线
            drawLine(g, rand, generatecolor);
            g.Dispose();

            //压缩图片
            fSize = fontSize;
            fWidth = 150;
            imageWidth = (int)(code.Length * (fSize + Padding - Fpadding)) + 4 + Padding * 2 + 50;//这里是让图片更长点
            imageHeight = fSize * 2 + Padding;
            Bitmap smallimage = new Bitmap(image, imageWidth, imageHeight);
            return smallimage;
        }

        private void drawString(Graphics g, Random rand, string code, int fWidth, int imageHeight, int fSize, Color generatecolor)
        {
            //画字符串
            Bitmap[] images = getCodeBitmaps(code, fWidth, imageHeight, fSize, generatecolor);
            // 当前正在处理的图片
            int index = 0;
            // 绘制下一个图片时的位置
            int drawX = 0;
            if (images != null && images.Length != 0)
            {
                int[] measurelength = measureBitmap(images[index]);
                drawX = -measurelength[2];
                g.DrawImage(images[index], drawX, 0, images[index].Width, images[index].Height);
                drawX += fSize;
                index++;
            }
            while (index < images.Length)
            {
                g.DrawImage(images[index], drawX, 0, images[index].Width, images[index].Height);
                drawX += fSize;
                index++;
            }
        }

        /// <summary>
        /// 画字符串
        /// </summary>
        /// <param name="code">字符串</param>
        /// <param name="fWidth">图长</param>
        /// <param name="imageHeight">图高</param>
        /// <param name="fSize">字体大小</param>
        /// <param name="color">字体颜色</param>
        /// <returns>每一个字符对应一个Bitmap图,返回一个Bitmap数组</returns>
        private Bitmap[] getCodeBitmaps(string code, int fWidth, int imageHeight, int fSize, Color color)
        {
            Random rand = new Random();
            //画字符串
            Bitmap[] charmaps = new Bitmap[code.Length];
            colors = new Color[code.Length];
            SolidBrush b = new System.Drawing.SolidBrush(color);
            //随机字体和颜色的验证码字符
            for (int i = 0; i < code.Length; i++)
            {
                charmaps[i] = new Bitmap(fWidth, imageHeight);
                Graphics gchar = Graphics.FromImage(charmaps[i]);
                //随机字体
                int findex = rand.Next(Fonts.Length);
                Font f = new System.Drawing.Font(Fonts[findex], fSize - rand.Next(fSize / 10), System.Drawing.FontStyle.Bold);
                gchar.DrawString(code.Substring(i, 1), f, b, 0, 0);
                gchar.Dispose();
            }

            //随机旋转
            for (int i = 0; i < charmaps.Length; i++)
            {
                charmaps[i] = linearImage(charmaps[i], rand.Next(100) % 2 == 0 ? (float)rand.NextDouble() * rotateAngle : -(float)rand.NextDouble() * rotateAngle, rand.Next(100) % 2 == 0 ? rand.Next((int)transform) : -rand.Next((int)transform));
            }

            //随机扭曲
            for (int i = 0; i < charmaps.Length; i++)
            {
                charmaps[i] = TwistImage(charmaps[i], true, TwistMultiValue, rand.NextDouble() * 2 * 3.14);
            }

            return charmaps;
        }

        #endregion

        #region 线性转化图片(平移,旋转)

        /// <summary>
        /// 线性转化图片(平移,旋转)
        /// </summary>
        /// <param name="image"></param>
        /// <param name="rotateangle">旋转角度</param>
        /// <param name="translate">水平位移</param>
        /// <returns></returns>
        private Bitmap linearImage(Bitmap image, float rotateangle, float translate)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);
            Graphics gchar = Graphics.FromImage(result);
            int[] measuresize = measureBitmap(image);
            Point charcenter = new Point(image.Width / 2 - (measuresize[2] + measuresize[3]) / 2, image.Height / 2 - (measuresize[0] + measuresize[1]) / 2);
            Matrix matrix = new Matrix();
            matrix.RotateAt(rotateangle, new Point(image.Width / 2, image.Height / 2));
            matrix.Translate(0, translate);
            gchar.Transform = matrix;
            gchar.DrawImage(image, charcenter);
            gchar.ResetTransform();
            gchar.Dispose();
            return result;
        }

        /// <summary>
        /// 测量一个字符图的左右最小空白和上下最小空白
        /// </summary>
        private int[] measureBitmap(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;

            int[] result = new int[4];
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData bmpData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);
            int[] mem_data = new int[bmpData.Stride * bmpData.Height / 4];

            Marshal.Copy(bmpData.Scan0, mem_data, 0, bmpData.Stride * bmpData.Height / 4);
            Color colorpixel;

            int starti = image.Height;
            int endi = 0;
            int startj = image.Width;
            int endj = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    colorpixel = Color.FromArgb(mem_data[i * bmpData.Stride / 4 + j]);
                    if (!checkColored(colorpixel))
                    {
                        if (i < starti)
                            starti = i;
                        if (i > endi)
                            endi = i;
                        if (j < startj)
                            startj = j;
                        if (j > endj)
                            endj = j;
                        continue;
                    }
                }
            }
            result[0] = starti;
            result[1] = endi;
            result[2] = startj;
            result[3] = endj;
            image.UnlockBits(bmpData);
            return result;
        }

        #endregion

        #region 扭曲图片

        private const double PI = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559;

        /// <summary>
        /// 正弦曲线Wave扭曲图片 
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            System.Drawing.Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;
            Rectangle rect = new Rectangle(0, 0, srcBmp.Width, srcBmp.Height);
            BitmapData bmpData = srcBmp.LockBits(rect, ImageLockMode.ReadWrite, srcBmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int[] mem_data = new int[bmpData.Stride * bmpData.Height / 4];
            Marshal.Copy(ptr, mem_data, 0, bmpData.Stride * bmpData.Height / 4);

            BitmapData destData = destBmp.LockBits(new Rectangle(0, 0, destBmp.Width, destBmp.Height), ImageLockMode.ReadWrite, destBmp.PixelFormat);
            int[] dest_mem_data = new int[destData.Stride * destData.Height / 4];
            Marshal.Copy(destData.Scan0, dest_mem_data, 0, destData.Stride * destData.Height / 4);

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    System.Drawing.Color color = Color.FromArgb(mem_data[j * bmpData.Stride / 4 + i]);
                    if (!checkColored(color))
                    {
                        double dx = 0;
                        dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                        dx += dPhase;
                        double dy = Math.Sin(dx);

                        // 取得当前点的颜色
                        int nOldX = 0, nOldY = 0;
                        nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                        nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                        if (nOldX >= 0 && nOldX < destBmp.Width
                         && nOldY >= 0 && nOldY < destBmp.Height)
                        {
                            dest_mem_data[nOldY * destData.Stride / 4 + nOldX] = mem_data[j * bmpData.Stride / 4 + i];
                        }
                    }
                }
            }
            Marshal.Copy(dest_mem_data, 0, destData.Scan0, destData.Stride * destData.Height / 4);
            destBmp.UnlockBits(destData);
            srcBmp.UnlockBits(bmpData);
            return destBmp;
        }

        /// <summary>
        /// 检查一个像素的颜色是否和目标颜色相同
        /// </summary>
        /// <param name="colorpixel"></param>
        /// <returns></returns>
        private bool checkColored(Color colorpixel)
        {
            if (colorpixel.A == 0 && colorpixel.R == 0 & colorpixel.G == 0 & colorpixel.B == 0)
                return true;
            return false;
        }

        #endregion

        #region 画干扰线
        private void drawLine(Graphics g, Random rand, Color generatecolor)
        {
            float lastpen = (float)(rand.NextDouble() * (MaxCurvePen - MinCurvePen) + MinCurvePen);
            float randpen = (float)(rand.NextDouble() * 2) - 1;
            float thispen = lastpen + randpen;
            Pen drawpen = new Pen(generatecolor, thispen);
            g.DrawCurve(drawpen, new PointF[] { new PointF(0, 100), new PointF(100, 80), new PointF(200, 120), new PointF(300, 80), new PointF(400, 100) }, 1.0f);
        }
        #endregion

        #region 画干扰点
        private void drawPoint(Graphics g, Random rand, Bitmap image)
        {
            //给背景添加随机生成的燥点
            if (this.Chaos)
            {
                Pen pen = new Pen(ChaosColor, 1);
                int c = Length * 10;

                for (int i = 0; i < c; i++)
                {
                    int x = rand.Next(image.Width);
                    int y = rand.Next(image.Height);

                    g.DrawRectangle(pen, x, y, 1, 1);
                }
            }
        }
        #endregion

        #region 将创建好的图片输出到页面

        public byte[] CreateImageBytes(string code)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            Bitmap image = this.CreateImageCode(code);
            //修改图片保存质量
            ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");
            Encoder myEncoder = Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            //图片质量等级
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            image.Save(ms, myImageCodecInfo, myEncoderParameters);
            byte[] imageBytes = ms.GetBuffer();
            ms.Close();
            ms = null;
            image.Dispose();
            image = null;
            return imageBytes;
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType) return encoders[j];
            }
            return null;
        }

        #endregion

        #region 生成随机字符码

        /// <summary>
        /// 生成随机字符码
        /// </summary>
        /// <param name="codeLen"></param>
        /// <returns></returns>
        public string CreateVerifyCode(int codeLen)
        {
            if (codeLen == 0)
            {
                codeLen = Length;
            }

            string[] arr = CodeSerial.Split(',');

            string code = "";

            int randValue = -1;

            Random rand = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);

                code += arr[randValue];
            }

            return code;
        }
        public string CreateVerifyCode()
        {
            return CreateVerifyCode(0);
        }

        #endregion
    }
}
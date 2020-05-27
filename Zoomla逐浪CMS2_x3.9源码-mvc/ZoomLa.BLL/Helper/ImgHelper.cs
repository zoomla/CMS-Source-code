using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using ZoomLa.Common;
using ZoomLa.Components;

/*
 * 图片相关处理,压缩,Base64转换等,前台的见ImgHelper.js
 */ 
namespace ZoomLa.BLL.Helper
{
    //GDI+中发生一般性错误 的解决办法
    //答:Save()时,保存的图片文件已经被打开且未被释放
    public class ImgHelper
    {
        public string ImgToBase64(string vpath)
        {
            string ppath=SafeSC.PathDeal(vpath);
            if (!File.Exists(ppath)) throw new Exception(vpath + "文件不存在");
            Bitmap bmp = new Bitmap(vpath);
            return ImgToBase64ByImage(bmp);
        }
        //image对象直接转base64
        public string ImgToBase64ByImage(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            return Convert.ToBase64String(arr);
        }
       /// <summary>
       /// 将Base64转为图片
       /// </summary>
       /// <param name="vpath">带图片名的路径</param>
       /// <param name="imgtxt">Base64字符串</param>
        public void Base64ToImg(string vpath, string base64)
        {
            //data:image/png;base64,
            if (base64.Contains("base64,"))
            {
                base64 = Regex.Split(base64, Regex.Unescape("base64,"))[1];
            }
            string ppath = function.VToP(SafeSC.PathDeal(vpath));
            SafeSC.CreateDir(ppath);
            byte[] arr = Convert.FromBase64String(base64);
            MemoryStream ms = new MemoryStream(arr);
            Bitmap bmp = new Bitmap(ms);
            bmp.Save(ppath, System.Drawing.Imaging.ImageFormat.Jpeg);
            //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
            //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
            //bmp.Save(txtFileName + ".png", ImageFormat.Png);
            ms.Close();
        }
        /// <summary>
        /// 对图片进行等比例压缩,一直压到指定大小之下
        /// </summary>
        /// <param name="file">需压缩的文件</param>
        /// <param name="tsize">大于多少的图片才压</param>
        /// <param name="vpath">虚拟路径</param>
        public void CompressImg(HttpPostedFile file, int tsize, string vpath)
        {
            int imgSize = tsize * 1024;
            vpath = SafeSC.PathDeal(vpath);
            Stream imgStream = file.InputStream;
            System.Drawing.Image img = System.Drawing.Image.FromStream(imgStream);
            Bitmap bmp = new Bitmap(img);
            if (file.ContentLength > imgSize)//开始压缩,计算大小和比率
            {
                double scale = (double)imgSize / (double)file.ContentLength;
                int width = (int)(img.Width * scale);
                int height = (int)(img.Height * scale);
                bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                Graphics grap = Graphics.FromImage(bmp);
                grap.Clear(Color.Transparent);
                grap.DrawImage(img, new Rectangle(0, 0, width, height));
                grap.Dispose();
            }
            SaveImg(vpath, bmp);
            bmp.Dispose();
            imgStream.Close();
        }
        // Bitmap bt = imgHelper.Rotate("/test/a1.jpg", 90);
        public Bitmap Rotate(string vpath, int angle)
        {
            string ppath = function.VToP(vpath);
            if (!File.Exists(ppath)) { throw new Exception("图片文件不存在"); }
            System.Drawing.Image img = System.Drawing.Image.FromFile(ppath);
            System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
            img.Dispose();
            return Rotate(bmp, angle);
        }
        /// <summary>
        /// 以逆时针为方向对图像进行旋转
        /// </summary>
        /// <param name="b">位图流</param>
        /// <param name="angle">旋转角度[0,360](前台给的)</param>
        /// <returns></returns>
        public Bitmap Rotate(Image img, int angle)
        {
            angle = angle % 360;
            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高
            int w = img.Width;
            int h = img.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            //目标位图
            Bitmap dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);
            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(img, rect);
            //重至绘图的所有变换
            g.ResetTransform();
            g.Save();
            g.Dispose();
            //dsImage.Save("yuancd.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return dsImage;
        }
        /// <summary>
        ///  图片缩放(用于缩略图等)
        /// </summary>
        /// <param name="vpath">图片虚拟路径</param>
        /// <returns></returns>
        public Bitmap ZoomImg(string vpath,int height,int width)
        {
            string ppath = function.VToP(vpath);
            if (!File.Exists(ppath)) { throw new Exception("图片文件不存在"); }
            System.Drawing.Image img = System.Drawing.Image.FromFile(ppath);//避免占用资源
            System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
            img.Dispose();
            return ZoomImg(bmp, height, width);
        }
        public Bitmap ZoomImg(Image img, int destHeight, int destWidth)
        {
            System.Drawing.Imaging.ImageFormat thisFormat = img.RawFormat;
            int width = img.Width, height = img.Height;
            if (destWidth > 0 && width > destWidth)
            {
                double ImgPercent = (double)destWidth / (double)width;
                width = (int)(width * ImgPercent);
                height = (int)(height * ImgPercent);
            }
            if (destHeight > 0 && height > destHeight)
            {
                double ImgPercent = (double)destHeight / (double)height;
                width = (int)(width * ImgPercent);
                height = (int)(height * ImgPercent);
            }
            Bitmap outBmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.White);//画布颜色,缩小图片后,指定用该色填充
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, new Rectangle(0, 0, width, height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量     
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            img.Dispose();
            return outBmp;
        }
        /// <summary>
        /// 根据后缀名,按指定格式保存图片
        /// </summary>
        /// <param name="vpath">带文件名的路径</param>
        public string SaveImg(string vpath, Bitmap bmp)
        {
            return SaveImage(vpath, bmp);
        }
        public static string SaveImage(string vpath, Image img)
        {
            Bitmap bmp = new Bitmap(img);
            return SaveImage(vpath, bmp);
        }
        public static string SaveImage(string vpath, Bitmap bmp)
        {
            string ppath = function.VToP(vpath);
            if (!Directory.Exists(Path.GetDirectoryName(ppath))) { SafeSC.CreateDir(vpath); }
            string ext = Path.GetExtension(vpath).ToLower();
            switch (ext)
            {
                case ".jpg":
                    bmp.Save(ppath, ImageFormat.Jpeg);
                    break;
                case ".png":
                    bmp.Save(ppath, ImageFormat.Png);
                    break;
                case ".gif":
                    bmp.Save(ppath, ImageFormat.Gif);
                    break;
                default:
                    bmp.Save(ppath, ImageFormat.Jpeg);
                    break;
            }
            return vpath;
        }

        /*---------------从配置文件中增加水印--------------------*/
        /// <summary>
        /// 读取配置文件,增加水印
        /// </summary>
        /// <vpath>需水印图片虚拟路径</vpath>
        /// <returns>水印后的路径</returns>
        public static string AddWater(string svpath)
        {
            if (!SafeSC.IsImage(svpath) || !WaterModuleConfig.WaterConfig.IsUsed) { return svpath; }
            string sppath = function.VToP(svpath);
            string vdir = Path.GetDirectoryName(sppath) + "\\";
            string fname = "wd_" + Path.GetFileName(svpath);
            string savePath = function.PToV(vdir + fname);
            System.Drawing.Image img = Image.FromFile(sppath);
            img = AddWater(img);
            Bitmap bmp = new Bitmap(img);
            savePath = new ImgHelper().SaveImg(savePath, bmp);
            img.Dispose(); bmp.Dispose();
            return savePath;
        }
        public static Image AddWater(Image img)
        {
            if (!WaterModuleConfig.WaterConfig.IsUsed) return img;
            if (IsPixelFormatIndexed(img.PixelFormat))
            {
                Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.DrawImage(img, 0, 0);
                }
                img = bmp;
            }
            //-----开始处理水印
            WaterImages water = new WaterImages();
            if (WaterModuleConfig.WaterConfig.WaterClass.Equals("1"))
            {
                string waterPath = function.VToP(WaterModuleConfig.WaterConfig.imgLogo);
                if (File.Exists(waterPath))
                {
                    img = water.DrawImage(img, waterPath);
                }
                else { ZLLog.L("[" + WaterModuleConfig.WaterConfig.imgLogo + "]水印图片不存在"); }
            }
            else
            {
                img = water.DrawFont(img);
            }
            return img;
        }
        /*---------------从配置文件中增加水印--------------------*/
        /// <summary>
        /// 将图片加载入内存
        /// </summary>
        public static Image ReadImgToMS(string vpath)
        {
            string ppath = function.VToP(vpath);
            if (!File.Exists(ppath)) { throw new Exception("图片文件不存在"); }
            System.Drawing.Image imgDisk = System.Drawing.Image.FromFile(ppath);//避免占用资源
            System.Drawing.Image imgMS = new System.Drawing.Bitmap(imgDisk);
            imgDisk.Dispose();
            return imgMS;
        }
        //---------------------Tools
        private static PixelFormat[] indexedPixelFormats = { PixelFormat.Undefined, PixelFormat.DontCare,
        PixelFormat.Format16bppArgb1555, PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed,
        PixelFormat.Format8bppIndexed
            };
        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// 一些图片经过处理会带索引,直接添加水印会报:无法从带有索引像素格式的图像创建graphics对象
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        /// <returns></returns>
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }
            return false;
        }
    }
}

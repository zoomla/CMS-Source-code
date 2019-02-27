using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using ZoomLa.Safe;
using ZoomLa.Common;

namespace ZoomLa.BLL.Helper
{
    public class ImageDealLib
    {
        #region 公用函数与枚举
        /// <summary>
        /// 根据文件路径判断文件是否存在
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="model">返回模式,m:返回map地址不检查文件是否存在,c:检测文件是否存在,并返回map地址</param>
        /// <param name="mappath">map路径</param>
        /// <returns></returns>
        public static bool FileExistMapPath(string filepath, FileCheckModel model, out string mappath)
        {
            bool checkresult = false;
            switch (model)
            {
                case FileCheckModel.M:
                    mappath = HttpContext.Current.Server.MapPath(filepath);
                    checkresult = true;
                    break;
                case FileCheckModel.C:
                    if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(filepath)))
                    {
                        mappath = HttpContext.Current.Server.MapPath(filepath);
                        checkresult = true;
                    }
                    else
                    {
                        mappath = null;
                        checkresult = false;
                    }
                    break;
                default:
                    mappath = "";
                    checkresult = false;
                    break;
            }
            return checkresult;
        }

        /// <summary>
        /// 图片保存类型
        /// JPEG:.jpg格式;
        /// GIF:.gif格式;
        /// PNG:.png格式;
        /// </summary>
        public enum ImageType
        {
            JPEG,
            GIF,
            PNG
        }

        /// <summary>
        /// 水印模式
        /// Center:中间;
        /// CenterUp:中上;
        /// CenterDown:中下;
        /// LeftUp:左上;
        /// LeftDown:左下;
        /// RightUp:右上;
        /// RightDown:右下;
        /// Random:随机;
        /// </summary>
        public enum WaterType
        {
            Center,
            CenterUp,
            CenterDown,
            LeftUp,
            LeftDown,
            RightUp,
            RightDown,
            Random
        }

        /// <summary>
        /// 缩略模式
        /// X--按宽度缩放,高着宽比例;
        /// Y--按高度缩放,宽着宽比例;
        /// XY--按给定mwidth,mheight(此模式mwidth,mheight为必须值)进行缩略;
        /// </summary>
        public enum ResizeType
        {
            X,
            Y,
            XY
        }

        /// <summary>
        /// 文件检测模式
        /// M:不检测文件是否存在,返回ServerMapPath;
        /// C:检测文件是否存在,返回ServerMapPath;
        /// </summary>
        public enum FileCheckModel
        {
            M,
            C
        }

        /// <summary>
        /// 原图文件是否保存
        /// Delete:保存
        /// Save:不保存,删除
        /// </summary>
        public enum FileCache
        {
            Save,
            Delete
        }
        #endregion
        public static string lastcroppic = "";//上一张已剪切生成的文件名
        public static string diffpicpath = "";//上一张要被剪切的原图地址
        /// <summary>
        /// 图片剪切
        /// </summary>
        /// <param name="picpath">源图片文件地址</param>
        /// <param name="spath">剪切临时文件地址</param>
        /// <param name="x1">x起始坐标</param>
        /// <param name="y1">y起始坐标</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="filecache">源文件处理方式</param>
        /// <param name="warning">处理警告信息</param>
        /// <returns>剪切图片地址</returns>
        public static string imgcrop(string picpath, string spath, int x1, int y1, int width, int height, FileCache filecache, out string warning)
        {
            //反馈信息
            System.Text.StringBuilder checkmessage = new System.Text.StringBuilder();
            //从指定源图片,创建image对象
            string _sourceimg_common_mappath = "";
            //检测源文件
            bool checkfile = false;
            checkfile = FileExistMapPath(picpath, FileCheckModel.M, out _sourceimg_common_mappath);

            System.Drawing.Image _sourceimg_common = null;
            System.Drawing.Bitmap _currimg_common = null;
            System.Drawing.Graphics _g_common = null;

            if (checkfile == true)
            {
                //从源文件创建imgage
                System.Drawing.Image temp = System.Drawing.Image.FromFile(_sourceimg_common_mappath);
                _sourceimg_common = new System.Drawing.Bitmap(temp);
                temp.Dispose();

                //从指定width、height创建bitmap对象
                _currimg_common = new System.Drawing.Bitmap(width, height);

                //从_currimg_common创建画笔
                _g_common = Graphics.FromImage(_currimg_common);

                //设置画笔
                _g_common.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                _g_common.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                _g_common.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                //绘制图片
                _g_common.DrawImage(_sourceimg_common, new Rectangle(0, 0, width, height), new Rectangle(x1, y1, width, height), GraphicsUnit.Pixel);

                //保存图片
                string _spath_common_mappath = "";

                //判断是否是对同一张图片进行剪切
                //判断是否，已更新剪切图片,防止覆盖上一张已完成剪切的图片
                //spath = string.IsNullOrEmpty(lastcroppic) ? spath + DateTime.Now.ToString("MMddhhmmss") + ".jpg" : (diffpicpath == picpath ? lastcroppic : spath + Guid.NewGuid().ToString() + ".jpg");

                lastcroppic = spath;
                diffpicpath = picpath;

                FileExistMapPath(spath, FileCheckModel.M, out _spath_common_mappath);

                _currimg_common.Save(_spath_common_mappath, System.Drawing.Imaging.ImageFormat.Jpeg);

                //释放
                _sourceimg_common.Dispose();
                _currimg_common.Dispose();
                _g_common.Dispose();

                //处理原文件
                int filecachecode = filecache.GetHashCode();

                //文件缓存方式:Delete,删除原文件
                if (filecachecode == 1)
                {
                    System.IO.File.Delete(_sourceimg_common_mappath);
                }

                //返回相对虚拟路径
                warning = "";
                return spath;
            }

            checkmessage.Append("error:未能找到剪切原图片;");

            warning = checkmessage.ToString().TrimEnd(';');

            return "";
        }
        //string waterpic = ImageDealLib.makewatermark(suoluepic, "/images/w.png", ImageDealLib.WaterType.Random, "/images/", ImageDealLib.ImageType.JPEG, ImageDealLib.FileCache.Save, out warning2);
        public static string makewatermark(string picpath, string waterspath, WaterType watermodel, string spath, ImageType imgtype, FileCache filecache, out string warning, int trans = 100)
        {
            System.Text.StringBuilder checkmessage = new System.Text.StringBuilder();
            System.Drawing.Image _sourceimg_water = null;
            //检测水印源文件
            string _sourceimg_water_mappath = "";
            bool checkfilewater = false;
            checkfilewater = FileExistMapPath(waterspath, FileCheckModel.C, out _sourceimg_water_mappath);
            if (checkfilewater == true)
            {
                //从指定源文件,创建image对象
                _sourceimg_water = System.Drawing.Image.FromFile(_sourceimg_water_mappath);
            }
            else
            {
                checkmessage.Append("error:找不到需要水印图片!" + waterspath + ";");
            }
            return makewatermark(picpath, _sourceimg_water, watermodel, spath, imgtype, filecache, out warning, trans);
        }
        /// <summary>
        /// 水印图片
        /// 【如果图片需要缩略,请使用skeletonize.Resizepic()方法对图片进行缩略】
        /// 返回图片虚拟路径,和一个警告信息,可根据此信息获取图片合成信息
        /// </summary>
        /// <param name="picpath">需要水印的图片路径</param>
        /// <param name="waterspath">水印图片(Image)</param>
        /// <param name="watermodel">水印模式</param>
        /// <param name="spath">文件保存路径,带文件名</param>
        /// <param name="imgtype">保存文件类型</param>
        /// <param name="filecache">原文件处理方式</param>
        /// <param name="warning">处理警告信息</param>
        /// <returns>错误,返回错误信息;成功,返回图片路径</returns>
        public static string makewatermark(string picpath, System.Drawing.Image water, WaterType watermodel, string spath, ImageType imgtype, FileCache filecache, out string warning, int trans = 100)
        {
            #region
            //反馈信息
            System.Text.StringBuilder checkmessage = new System.Text.StringBuilder();

            //检测源文件
            string _sourceimg_common_mappath = "";
            bool checkfile = false;
            checkfile = FileExistMapPath(picpath, FileCheckModel.C, out _sourceimg_common_mappath);

            System.Drawing.Image _sourceimg_common = null;

            if (checkfile == true)
            {
                //从指定源文件,创建image对象
                _sourceimg_common = System.Drawing.Image.FromFile(_sourceimg_common_mappath);
            }
            else
            {
                checkmessage.Append("error:找不到需要的水印图片!" + picpath + ";");
            }
            #endregion

            #region
            if (string.IsNullOrEmpty(checkmessage.ToString()))
            {
                //源图宽、高
                int _sourceimg_common_width = _sourceimg_common.Width;
                int _sourceimg_common_height = _sourceimg_common.Height;

                //水印图片宽、高
                int _sourceimg_water_width = water.Width;
                int _sourceimg_water_height = water.Height;

                #region 水印坐标
                //水印坐标
                int _sourceimg_water_point_x = 0;
                int _sourceimg_water_point_y = 0;

                switch (watermodel)
                {
                    case WaterType.Center:
                        _sourceimg_water_point_x = (_sourceimg_common_width - _sourceimg_water_width) / 2;
                        _sourceimg_water_point_y = (_sourceimg_common_height - _sourceimg_water_height) / 2;
                        ; break;
                    case WaterType.CenterDown:
                        _sourceimg_water_point_x = (_sourceimg_common_width - _sourceimg_water_width) / 2;
                        _sourceimg_water_point_y = _sourceimg_common_height - _sourceimg_water_height;
                        ; break;
                    case WaterType.CenterUp:
                        _sourceimg_water_point_x = (_sourceimg_common_width - _sourceimg_water_width) / 2;
                        _sourceimg_water_point_y = 0;
                        ; break;
                    case WaterType.LeftDown:
                        _sourceimg_water_point_x = 0;
                        _sourceimg_water_point_y = _sourceimg_common_height - _sourceimg_water_height;
                        ; break;
                    case WaterType.LeftUp:
                        ; break;
                    case WaterType.Random:
                        Random r = new Random();
                        int x_random = r.Next(0, _sourceimg_common_width);
                        int y_random = r.Next(0, _sourceimg_common_height);

                        _sourceimg_water_point_x = x_random > (_sourceimg_common_width - _sourceimg_water_width)
                            ? _sourceimg_common_width - _sourceimg_water_width : x_random;

                        _sourceimg_water_point_y = y_random > (_sourceimg_common_height - _sourceimg_water_height)
                            ? _sourceimg_common_height - _sourceimg_water_height : y_random;

                        ; break;
                    case WaterType.RightDown:
                        _sourceimg_water_point_x = _sourceimg_common_width - _sourceimg_water_width;
                        _sourceimg_water_point_y = _sourceimg_common_height - _sourceimg_water_height;
                        ; break;
                    case WaterType.RightUp:
                        _sourceimg_water_point_x = _sourceimg_common_width - _sourceimg_water_width;
                        _sourceimg_water_point_y = 0;
                        ; break;
                }
                #endregion

                //从源图创建画板
                System.Drawing.Graphics _g_common = Graphics.FromImage(_sourceimg_common);

                //设置画笔
                _g_common.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                _g_common.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                _g_common.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                #region  设置透明
                int ap = trans;
                if (ap > 100 || ap < 0) { ap = 100; }
                float alpha = (float)ap / (float)100;
                //包含有关在呈现时如何操作位图和图元文件颜色的信息。
                ImageAttributes imageAttributes = new ImageAttributes();
                //Colormap: 定义转换颜色的映射
                ColorMap colorMap = new ColorMap();
                //我的水印图被定义成拥有绿色背景色的图片被替换成透明
                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = { colorMap };
                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float[][] colorMatrixElements = { 
                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f}, // red红色
                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f}, //green绿色
                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f}, //blue蓝色       
                new float[] {0.0f,  0.0f,  0.0f,  alpha, 0.0f}, //透明度     
                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};//
                //  ColorMatrix:定义包含 RGBA 空间坐标的 5 x 5 矩阵。
                //  ImageAttributes 类的若干方法通过使用颜色矩阵调整图像颜色。
                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                #endregion

                //绘制水印图片
                // g.DrawImage(copyImage, new Rectangle(postx, posty, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel, imageAttributes);
                //new Rectangle(0, 0, _sourceimg_water_width, _sourceimg_water_height)
                _g_common.DrawImage(water, new Rectangle(_sourceimg_water_point_x, _sourceimg_water_point_y, _sourceimg_water_width, _sourceimg_water_height), 0, 0, _sourceimg_water_width, _sourceimg_water_height, GraphicsUnit.Pixel, imageAttributes);

                //保存图片
                string _spath_common_mappath = "";
                //全局文件名

                //获取图片类型的hashcode值,生成图片后缀名
                //int extro = imgtype.GetHashCode();
                //string extend = extro == 0 ? ".jpg" : (extro == 1 ? ".gif" : (extro == 2 ? ".png" : ".jpg"));
                //spath = spath + Guid.NewGuid().ToString() + extend;

                FileExistMapPath(spath, FileCheckModel.M, out _spath_common_mappath);

                switch (imgtype)
                {
                    case ImageType.JPEG: _sourceimg_common.Save(_spath_common_mappath, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case ImageType.GIF: _sourceimg_common.Save(_spath_common_mappath, System.Drawing.Imaging.ImageFormat.Gif); break;
                    case ImageType.PNG: _sourceimg_common.Save(_spath_common_mappath, System.Drawing.Imaging.ImageFormat.Png); break;
                }


                //释放
                _sourceimg_common.Dispose();
                water.Dispose();
                _g_common.Dispose();

                //处理原文件
                int filecachecode = filecache.GetHashCode();
                //删除原文件
                if (filecachecode == 1)
                {
                    System.IO.File.Delete(_sourceimg_common_mappath);
                }

                warning = "";
                return spath;

            }
            #endregion
            //释放
            _sourceimg_common.Dispose();
            water.Dispose();
            warning = checkmessage.ToString().TrimEnd(';');
            return "";
        }
        /// <summary>
        /// 根据指定：缩略宽、高,缩略图片并保存
        /// 返回图片虚拟路径,和一个警告信息,可根据此信息获取图片合成信息
        /// </summary>
        /// <param name="picpath">原图路径</param>
        /// <param name="model">缩略模式[X,Y,XY](默认XY模式)</param>
        /// <param name="spath">文件保存路径(默认跟路径)</param>
        /// <param name="imgtype">图片保存类型</param>
        /// <param name="mwidth">缩略宽度(默认原图高度)</param>
        /// <param name="mheight">缩略高度(默认原图高度)</param>
        /// <param name="filecache">原文件处理方式</param>
        /// <param name="warning">处理警告信息</param>
        /// <returns>错误,返回错误信息;成功,返回图片路径</returns>
        public static string Resizepic(string picpath, ResizeType model, string spath, ImageType imgtype, double? mwidth, double? mheight, FileCache filecache, out string warning)
        {
            //反馈信息
            System.Text.StringBuilder checkmessage = new System.Text.StringBuilder();

            //文件保存路径
            spath = string.IsNullOrEmpty(spath) ? "/" : spath;

            //缩略宽度
            double swidth = mwidth.HasValue ? double.Parse(mwidth.ToString()) : 0;

            //缩略高度
            double sheight = mheight.HasValue ? double.Parse(mheight.ToString()) : 0;

            //从指定源图片,创建image对象
            string _sourceimg_common_mappath = "";

            //检测源文件
            bool checkfile = false;
            checkfile = FileExistMapPath(picpath, FileCheckModel.C, out _sourceimg_common_mappath);

            System.Drawing.Image _sourceimg_common = null;
            System.Drawing.Bitmap _currimg_common = null;
            System.Drawing.Graphics _g_common = null;

            if (checkfile == true)
            {
                //从源文件创建imgage
                _sourceimg_common = System.Drawing.Image.FromFile(_sourceimg_common_mappath);

                #region 缩略模式
                //缩略模式
                switch (model)
                {
                    case ResizeType.X:

                        #region X模式

                        //根据给定尺寸,获取绘制比例
                        double _width_scale = swidth / _sourceimg_common.Width;
                        //高着比例
                        sheight = _sourceimg_common.Height * _width_scale;

                        #endregion
                        ; break;
                    case ResizeType.Y:
                        #region Y模式

                        //根据给定尺寸,获取绘制比例
                        double _height_scale = sheight / _sourceimg_common.Height;
                        //宽着比例
                        swidth = _sourceimg_common.Width * _height_scale;

                        #endregion
                        ; break;
                    case ResizeType.XY:
                        #region XY模式

                        //当选择XY模式时,mwidth,mheight为必须值
                        if (swidth < 0 || sheight < 0)
                        {
                            checkmessage.Append("error:XY模式,mwidth,mheight为必须值;");
                        }

                        #endregion
                        ; break;
                    default:

                        #region 默认XY模式

                        //当默认XY模式时,mwidth,mheight为必须值
                        if (swidth < 0 || sheight < 0)
                        {
                            checkmessage.Append("error:你当前未选择缩略模式,系统默认XY模式,mwidth,mheight为必须值;");
                        }

                        ; break;
                        #endregion
                }
                #endregion
            }
            else
            {
                checkmessage.Append("error:未能找到缩略原图片," + picpath + ";");
            }

            if (string.IsNullOrEmpty(checkmessage.ToString()))
            {
                //创建bitmap对象
                _currimg_common = new System.Drawing.Bitmap((int)swidth, (int)sheight);

                _g_common = Graphics.FromImage(_currimg_common);

                //设置画笔
                _g_common.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                _g_common.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                _g_common.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                //绘制图片
                _g_common.DrawImage(_sourceimg_common, new Rectangle(0, 0, (int)swidth, (int)sheight), new Rectangle(0, 0, _sourceimg_common.Width, _sourceimg_common.Height), GraphicsUnit.Pixel);

                //保存图片
                string _spath_common_mappath = "";

                //获取图片类型的hashcode值,生成图片后缀名
                int extro = imgtype.GetHashCode();

                string extend = extro == 0 ? ".jpg" : (extro == 1 ? ".gif" : (extro == 2 ? ".png" : ".jpg"));

                //全局文件名
                spath = spath + Guid.NewGuid().ToString() + extend;

                FileExistMapPath(spath, FileCheckModel.M, out _spath_common_mappath);

                switch (imgtype)
                {
                    case ImageType.JPEG: _currimg_common.Save(_spath_common_mappath, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case ImageType.GIF: _currimg_common.Save(_spath_common_mappath, System.Drawing.Imaging.ImageFormat.Gif); break;
                    case ImageType.PNG: _currimg_common.Save(_spath_common_mappath, System.Drawing.Imaging.ImageFormat.Png); break;
                }

                //释放
                _sourceimg_common.Dispose();
                _currimg_common.Dispose();
                _g_common.Dispose();

                //处理原文件
                int filecachecode = filecache.GetHashCode();

                //文件缓存方式:Delete,删除原文件
                if (filecachecode == 1)
                {
                    System.IO.File.Delete(_sourceimg_common_mappath);
                }

                //返回相对虚拟路径
                warning = "";
                return spath;
            }

            //释放
            if (_sourceimg_common != null)
            {
                _sourceimg_common.Dispose();
            }
            if (_currimg_common != null)
            {
                _currimg_common.Dispose();
            }
            if (_g_common != null)
            {
                _g_common.Dispose();
            }

            warning = checkmessage.ToString().TrimEnd(';');

            return "";
        }
        /// <summary>
        /// 将字符串转换为图片,用于水印
        /// </summary>
        /// <imgvpath>生成的文字水印图片存储位置</imgvpath>
        /// <FontModel>字体模型,包含文本,字体颜色,大小等</imgvpath>
        /// <returns></returns>
        public static System.Drawing.Image ConverFontToImg(string imgvpath, FontModel model)
        {
            int width = model.intsize * model.text.Length * 2;
            int height = model.intsize * 2;
            //-----------根据指定宽高,生成图片
            string imgppath = function.VToP(imgvpath);
            SafeC.CreateDir(imgvpath);
            //System.Drawing.Image image, image2 = System.Drawing.Image.FromFile(imgppath);
            System.Drawing.Image image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            //g.FillRectangle(new SolidBrush(Color.FromArgb(model.GetRGB(model.background, "red"), model.GetRGB(model.background, "yellow"), model.GetRGB(model.background, "blue"))), 0, 0, width, height); //背景色
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            //------------指定文字与字体样式
            FontStyle fs = FontStyle.Regular;
            Font f = new Font(model.family, model.intsize, fs);
            SizeF crSize = g.MeasureString(model.text, f);
            //string color = "0x" + "#333333".Replace("#", "");
            //SolidBrush b = new SolidBrush(Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(color, 16))));//定义字体画笔
            SolidBrush b = new SolidBrush(Color.FromArgb(model.GetRGB(model.color, "red"), model.GetRGB(model.color, "yellow"), model.GetRGB(model.color, "blue")));
            StringFormat StrFormat = new StringFormat();
            int ap = 40;//透明度
            if (ap > 100 || ap < 0) { ap = 100; }
            int m_alpha = 255 * (ap / 100);
            int posx = 3, posy = 3;//文字的起始位置
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(m_alpha, 0, 0, 0));
            g.DrawString(model.text, f, semiTransBrush2, new PointF(posx + 1, posy + 1), StrFormat);//?无用
            g.DrawString(model.text, f, b, new PointF(posx, posy), StrFormat);
            g.Dispose();
            return image;
        }
    }
    public class FontModel
    {
        public string text;
        public string family;
        public string size;
        public string weight;
        public string style;
        public string decoration;
        public string color;
        public string background;//rgb(1,1,1)
        //---------------------
        public int intsize { get { double _size = DataConverter.CDouble(size.Replace("px", "").Replace("pt", "")); return Convert.ToInt32(_size); } }
        public int GetRGB(string rgb, string color)
        {
            string _color = rgb.Replace("rgb(", "").Replace(")", "");
            switch (color)
            {
                case "red":
                    return Convert.ToInt32(_color.Split(',')[0]);
                case "yellow":
                    return Convert.ToInt32(_color.Split(',')[1]);
                case "blue":
                    return Convert.ToInt32(_color.Split(',')[2]);
                default:
                    return 0;
            }
        }
    }
}

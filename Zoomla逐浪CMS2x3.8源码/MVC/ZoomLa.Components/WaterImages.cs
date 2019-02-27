using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace ZoomLa.Components
{
    public class WaterImages
    {
        public Image DrawImage(string sppath,string waterurl)
        {
            System.Drawing.Image image = Image.FromFile(sppath);
            return DrawImage(image, waterurl);
        }
        /// <summary>
        /// 在图片上添加水印
        /// </summary>
        /// <param name="image">需要添加水印的图片</param>
        /// <param name="waterurl">水印图片</param>
        /// <param name="filename">保存路径,物理路径</param>
        public Image DrawImage(System.Drawing.Image image, string waterurl)
        {
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(waterurl);
            Graphics g = Graphics.FromImage(image);
            //左上1   上中4   右上7
            //左中2   中  5   右中8
            //左下3   下中6   右下9
            //  手动设置10
            #region 图片位置
            int postx = 0;
            int posty = 0;

            switch (Convert.ToInt16(WaterModuleConfig.WaterConfig.lopostion))
            {
                case 1:
                    postx = 10;
                    posty = 10;
                    break;
                case 2:
                    postx = 10;
                    posty = (image.Height - copyImage.Height) / 2 + 10;
                    break;
                case 3:
                    postx = 10;
                    posty = image.Height - copyImage.Height - 10;
                    break;
                case 4:
                    postx = (image.Width - copyImage.Width) / 2 + 10;
                    posty = 10;

                    break;
                case 5:
                    postx = (image.Width - copyImage.Width) / 2 + 10;
                    posty = (image.Height - copyImage.Height) / 2 + 10;
                    break;
                case 6:
                    postx = (image.Width - copyImage.Width) / 2 + 10;
                    posty = image.Height - copyImage.Height - 10;
                    break;
                case 7:
                    postx = image.Width - copyImage.Width - 10;
                    posty = 10;
                    break;
                case 8:
                    postx = image.Width - copyImage.Width - 10;
                    posty = (image.Height - copyImage.Height) / 2 + 10;
                    break;
                case 9:
                    postx = image.Width - copyImage.Width - 10;
                    posty = image.Height - copyImage.Height - 10;
                    break;
                case 10:
                    postx = WaterModuleConfig.WaterConfig.loX;
                    posty = WaterModuleConfig.WaterConfig.loY;
                    break;
                default:
                    postx = 10;
                    posty = 10;
                    break;
            }
            if (image.Width < postx + copyImage.Width)
                postx = image.Width - copyImage.Width;
            if (image.Height < posty + copyImage.Height)
                posty = image.Height - copyImage.Height;
            #endregion
            #region  透明
            int ap = WaterModuleConfig.WaterConfig.imgAlph;
            if (ap > 100 || ap < 0) { ap = 100; }
            float alpha = (float)ap / (float)100;
            //ImageAttributes 对象包含有关在呈现时如何操作位图和图元文件颜色的信息。
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
            g.DrawImage(copyImage, new Rectangle(postx, posty, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel, imageAttributes);
            g.Dispose();
            copyImage.Dispose();
            return image;
        }
        public Image DrawFont(string sppath)
        {
            System.Drawing.Image image = Image.FromFile(sppath);
            return DrawFont(image);
        }
        public Image DrawFont(System.Drawing.Image image)
        {
            //System.Drawing.Image image = System.Drawing.Image.FromFile(Testfilename);
            //Graphics g = Graphics.FromImage(image);
            Graphics g = Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);

            FontStyle fs = new FontStyle();
            switch (Convert.ToInt16(WaterModuleConfig.WaterConfig.WaterWordStyle))
            {
                case 1:
                    fs = FontStyle.Regular;
                    break;
                case 2:
                    fs = FontStyle.Italic;
                    break;
                case 3:
                    fs = FontStyle.Bold;
                    break;
                case 4:
                    fs = FontStyle.Strikeout;
                    break;
                case 5:
                    fs = FontStyle.Underline;
                    break;
                default:
                    fs = FontStyle.Regular;
                    break;
            }
            Font f = new Font(WaterModuleConfig.WaterConfig.WaterWordType, WaterModuleConfig.WaterConfig.WaterWordSize, fs);
            SizeF crSize = new SizeF();
            crSize = g.MeasureString(WaterModuleConfig.WaterConfig.WaterWord, f);
            int ap = WaterModuleConfig.WaterConfig.WaterWordAlph;
            if (ap > 100 || ap < 0) { ap = 100; }
            int m_alpha = 255 * (ap / 100);
            // SolidBrush b = new SolidBrush(Color.FromArgb(255, Color.FromArgb(0xE6BD1A)));
            string yanse = WaterModuleConfig.WaterConfig.WaterWordColor;
            yanse = "0x" + yanse.Replace("#", "");
            int ddc = Convert.ToInt32(yanse, 16);
            SolidBrush b = new SolidBrush(Color.FromArgb(255, Color.FromArgb(ddc)));
            string addText = WaterModuleConfig.WaterConfig.WaterWord;
            //图片位置

            int postx = 0;
            int posty = 0;
            switch (Convert.ToInt16(WaterModuleConfig.WaterConfig.lopostion))
            {
                case 1:
                    postx = 10;
                    posty = 10;
                    break;
                case 2:
                    postx = image.Width - (ushort)crSize.Width - 10;
                    posty = 10;
                    break;
                case 3:
                    postx = image.Width - (ushort)crSize.Width - 10;
                    posty = image.Height - (ushort)crSize.Height - 10;
                    break;
                case 4:
                    postx = 10;
                    posty = image.Height - (ushort)crSize.Height - 10;
                    break;
                case 5:
                    postx = WaterModuleConfig.WaterConfig.loX;
                    posty = WaterModuleConfig.WaterConfig.loY;
                    break;
                default:
                    postx = 10;
                    posty = 10;
                    break;
            }
            if (image.Width < postx + crSize.Width)
                postx = image.Width - (ushort)crSize.Width;
            if (image.Height < posty + crSize.Height)
                posty = image.Height - (ushort)crSize.Height;
            StringFormat StrFormat = new StringFormat();
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(m_alpha, 0, 0, 0));
            g.DrawString(addText, f, semiTransBrush2, new PointF(postx + 1, posty + 1), StrFormat);
            g.DrawString(addText, f, b, new PointF(postx, posty), StrFormat);
            g.Dispose();
            return image;
        }
    }
}
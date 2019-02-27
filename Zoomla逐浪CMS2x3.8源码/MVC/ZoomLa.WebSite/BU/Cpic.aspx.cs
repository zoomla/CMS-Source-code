namespace ZoomLaCMS.BU
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;
    using System.IO;
    public partial class Cpic : System.Web.UI.Page
    {
        /*
            * 实际显示效果与在浏览器中输出并不一致
            * background-url:并不会输出图片 
            * 默认不支持JS
            * 给前端控件赋值无用,静态变量等可以如Call.SiteName
            * 解决:因为Cookies的关系，没有取到值
            */
        private B_User buser = new B_User();
        public string uname = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void CreateImage(string btext)
        {
            System.Drawing.Image img = null;
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            System.Drawing.Image im = System.Drawing.Image.FromFile(Server.MapPath("/App_Themes/User/pinimage.jpg"));
            Bitmap bt = new Bitmap(im.Width, im.Height);
            bt.SetResolution(im.HorizontalResolution, im.VerticalResolution);
            Graphics grimage = Graphics.FromImage(bt);
            grimage.SmoothingMode = SmoothingMode.AntiAlias;
            grimage.DrawImage(im, new Rectangle(0, 0, im.Width, im.Height), 0, 0, im.Width, im.Height, GraphicsUnit.Pixel);
            if (img != null)
            {
                DrawWImage(grimage, img, new Point(600, 500));
            }
            DrawWord(grimage, bt, date, 40, FontStyle.Regular, Color.FromArgb(50, 0, 0, 0), 110, 70);
            DrawRoteWord(grimage, bt, btext, 14);
            bt.Save(Server.MapPath(uname + ".jpg"), ImageFormat.Jpeg);
            bgimg.ImageUrl = uname + ".jpg";
            grimage.Dispose();
            bt.Dispose();
            im.Dispose();
        }
        void DrawWImage(Graphics gr, System.Drawing.Image image, Point pt)
        {
            gr.DrawImage(image, pt);
        }
        void DrawRoteWord(Graphics grimage, Bitmap bt, string Word, int fontsize)
        {
            grimage.TranslateTransform(bt.Width / 2 + 100, bt.Height / 2);
            grimage.RotateTransform(-40);
            grimage.TranslateTransform(-(bt.Width / 2 + 80), -(bt.Height / 2));
            int offwidth = 0;
            int offheight = 850;
            for (int i = 0; i < 12; i++)
            {
                SizeF sf;
                for (int j = 0; j < 8; j++)
                {
                    sf = DrawWord(grimage, bt, Word, fontsize, FontStyle.Regular, Color.FromArgb(60, 0, 0, 0), offwidth, offheight);
                    offwidth = offwidth + ((int)sf.Width) + 50;
                }
                offwidth = 0;
                offheight = offheight - 100;
            }
        }
        SizeF DrawWord(Graphics grimage, Bitmap bt, string Word, int fontsize, FontStyle fs, Color alpha, int x, int y)
        {
            Font ft = new Font("arial", fontsize, fs);
            SizeF sf = grimage.MeasureString(Word, ft);
            grimage.DrawString(Word, ft, new SolidBrush(alpha), new PointF(x, bt.Height - sf.Height - y));
            return sf;
        }
    }
}
namespace ZoomLa.Web.UI
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;
    using System.ComponentModel;
    using System.Web.UI;
    using System;
    using System.Drawing.Drawing2D;


    public partial class ValidateCode : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Cache.SetNoStore();
            Bitmap image = new Bitmap(70, 20);
            Graphics graphics = Graphics.FromImage(image);
            Color[] colorArray2 = new Color[] { Color.AliceBlue, Color.Aqua, Color.Black, Color.Brown, Color.DarkRed, Color.SkyBlue, Color.Silver, Color.Tan, Color.Violet, Color.SpringGreen };
            try
            {
                Random random = new Random();
                graphics.Clear(Color.White);
                for (int i = 0; i < 5; i++)
                {
                    int num2 = random.Next(image.Width);
                    int num3 = random.Next(image.Width);
                    int num4 = random.Next(image.Height);
                    int num5 = random.Next(image.Height);
                    graphics.DrawLine(new Pen(Color.Silver), num2, num4, num3, num5);
                }
                string[] strArray = "2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z".Split(new char[] { ',' });
                string s = string.Empty;
                for (int j = 0; j < 6; j++)
                {
                    s = s + strArray[random.Next(strArray.Length)];
                }
                Font font = new Font("Arial", 11f, FontStyle.Bold);
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                graphics.DrawString(s, font, brush, (float)random.Next(image.Width - 70), (float)random.Next(image.Height - 20));
                for (int k = 0; k < 100; k++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                graphics.DrawRectangle(new Pen(colorArray2[random.Next(colorArray2.Length)]), 0, 0, image.Width - 1, image.Height - 1);
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(stream.ToArray());
                this.Session["ValidateCode"] = s.ToLower();
            }
            finally
            {
                graphics.Dispose();
                image.Dispose();
            }
        }

    }
}
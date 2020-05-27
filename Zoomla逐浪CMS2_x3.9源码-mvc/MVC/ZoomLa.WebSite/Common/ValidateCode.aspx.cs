using System;
using System.Collections.Generic;
using System.Drawing;
using ZoomLa.Common;

namespace ZoomLaCMS.Common
{
    public partial class ValidateCode : System.Web.UI.Page
    {
        public Dictionary<string, string> CodeDic
        {
            get
            {
                if (Session["codeDic"] == null)
                {
                    Dictionary<string, string> codeDic = new Dictionary<string, string>();
                    Session["codeDic"] = codeDic;
                }
                return (Dictionary<string, string>)Session["codeDic"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { return; }
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string value = Request.Form["value"];
                string result = "0";
                switch (action)
                {
                    case "checkcode":
                        if (value.ToLower().Equals(CodeDic[key])) result = "1";
                        break;
                    default:
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
            }
            else
            {
                string randomcode = function.GetRandomString(6, 4);
                if (CodeDic.ContainsKey(key)) { CodeDic[key] = randomcode.ToLower(); }
                else { CodeDic.Add(key, randomcode.ToLower()); }
                CreateImage(randomcode);
            }
        }
        private void CreateImage(string randomcode)
        {
            Color[] colorArr = new Color[] { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            Random rand = new Random();
            //int randAngle = 45; //随机转动角度
            int mapwidth = (int)(randomcode.Length * 16);
            Bitmap map = new Bitmap(mapwidth, 22);//创建图片背景
            Graphics graph = Graphics.FromImage(map);
            graph.Clear(Color.AliceBlue);//清除画面，填充背景
            graph.DrawRectangle(new Pen(colorArr[rand.Next(7)], 0), 0, 0, map.Width - 1, map.Height - 1);//画一个边框
            //graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//模式
            try
            {
                //背景噪点生成
                Pen blackPen = new Pen(Color.LightGray, 0);
                for (int i = 0; i < 50; i++)
                {
                    int x = rand.Next(0, map.Width);
                    int y = rand.Next(0, map.Height);
                    graph.DrawRectangle(blackPen, x, y, 1, 1);
                }
                //绘制干扰线
                for (int i = 0; i < 1; i++)
                {
                    int num2 = rand.Next(map.Width);
                    int num3 = rand.Next(map.Width);
                    int num4 = rand.Next(map.Height);
                    int num5 = rand.Next(map.Height);
                    graph.DrawLine(new Pen(colorArr[rand.Next(7)]), num2, num4, num3, num5);
                }
                char[] chars = randomcode.ToCharArray();//拆散字符串成单字符数组
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                //定义字体
                string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
                for (int i = 0; i < chars.Length; i++)
                {
                    int findex = rand.Next(5);
                    Font f = new System.Drawing.Font(font[findex], 14, System.Drawing.FontStyle.Bold);//字体样式(参数2为字体大小)
                    Brush b = new System.Drawing.SolidBrush(colorArr[rand.Next(7)]);

                    Point dot = new Point(14, 14);
                    //float angle = rand.Next(-randAngle, randAngle);//转动的度数

                    graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                    //graph.RotateTransform(angle);
                    graph.DrawString(chars[i].ToString(), f, b, 1, -3, format);
                    //graph.RotateTransform(-angle);//转回去
                    graph.TranslateTransform(0, -dot.Y);//移动光标到指定位置，每个字符紧凑显示，避免被软件识别
                }
                //生成图片
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.Cache.SetNoStore();
                Response.ClearContent();
                Response.ContentType = "image/gif";
                Response.BinaryWrite(ms.ToArray());
            }
            catch { }
            finally { graph.Dispose(); map.Dispose(); }
        }
    }
}
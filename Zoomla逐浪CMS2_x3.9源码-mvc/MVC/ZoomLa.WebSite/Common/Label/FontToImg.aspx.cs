namespace ZoomLaCMS.Common.Label
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL.Helper;
    using ZoomLa.Common;
    public partial class FontToImg : System.Web.UI.Page
    {
        //1,生成的图像背景不纯色,有点状
        //2,暂只取前十字显示,后期完善换行
        public string txt { get { return HttpUtility.UrlDecode(Request.QueryString["txt"] ?? ""); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txt) || txt.Length > 500) { }
            FontConfig cfg = new FontConfig();
            cfg.text = txt.Length > 25 ? txt.Substring(0, 25) : txt;
            cfg.size = Convert.ToInt32(Req("size", cfg.size.ToString()));
            cfg.family = Req("family", cfg.family);
            cfg.color = Req("color", cfg.color);
            //仅接受16位传参
            cfg.bkcolor = Req("bkcolor", cfg.bkcolor);
            cfg.width = DataConverter.CLng(Req("width", cfg.width.ToString()));
            if (!cfg.bkcolor.StartsWith("#")) { cfg.bkcolor = "#" + cfg.bkcolor; }
            if (!cfg.color.StartsWith("#")) { cfg.color = "#" + cfg.color; }
            CreateImage(cfg);
        }
        public string Req(string name, string def)
        {
            string value = Request[name];
            if (string.IsNullOrEmpty(value)) { value = def; }
            return value;
        }
        private void CreateImage(FontConfig cfg)
        {
            int margin = cfg.size / 4;//每个字之间的间距
            int mapwidth = (int)(cfg.text.Length * (cfg.size + (cfg.size / 4) + margin));//必须加大些宽高,否则文字显示不全
            Bitmap map = new Bitmap(mapwidth, cfg.size + (cfg.size / 2));//创建图片背景
            Graphics graph = Graphics.FromImage(map);
            graph.Clear(colorHx16toRGB(cfg.bkcolor));//清除画面，填充背景
            try
            {
                char[] chars = cfg.text.ToCharArray();//拆散字符串成单字符数组
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                //定义字体
                for (int i = 0; i < chars.Length; i++)
                {
                    Font f = new System.Drawing.Font(cfg.family, cfg.size, System.Drawing.FontStyle.Regular);//字体样式(参数2为字体大小)
                    Brush b = new System.Drawing.SolidBrush(colorHx16toRGB(cfg.color));

                    Point dot = new Point(cfg.size, cfg.size);
                    graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                    graph.DrawString(chars[i].ToString(), f, b, 1, -3, format);
                    graph.TranslateTransform(margin, -dot.Y);//移动光标到指定位置，每个字符紧凑显示，避免被软件识别
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

        //-------------------------------Tools
        /// <summary>
        /// 一次只允许传一个字符
        /// num|eng|chinese
        /// </summary>
        public string GetCharType(string str)
        {
            Regex regChina = new Regex("^[^\x00-\xFF]");
            Regex regEnglish = new Regex("^[a-zA-Z]");
            Regex regNum = new Regex("[0-9]");
            char s = str.ToCharArray()[0];
            if (regEnglish.IsMatch(str)) { return "eng"; }
            else if (regChina.IsMatch(str)) { return "china"; }
            else if (regNum.IsMatch(str)) { return "num"; }
            else { return "other"; }
        }
        /// <summary>
        /// [颜色：16进制转成RGB],不能简写,如#ffffff不能为#fff
        /// </summary>
        public static System.Drawing.Color colorHx16toRGB(string strHxColor)
        {
            if (string.IsNullOrEmpty(strHxColor))
            {
                return System.Drawing.Color.FromArgb(0, 0, 0);//设为黑色
            }
            else
            {
                System.Drawing.Color color = System.Drawing.Color.FromArgb(System.Int32.Parse(strHxColor.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier), System.Int32.Parse(strHxColor.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier), System.Int32.Parse(strHxColor.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
                return color;
            }
        }
        /// <summary>
        /// [颜色：RGB转成16进制]
        /// </summary>
        /// <param name="R">红 int</param>
        /// <param name="G">绿 int</param>
        /// <param name="B">蓝 int</param>
        /// <returns></returns>
        public static string colorRGBtoHx16(int R, int G, int B)
        {
            return System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(R, G, B));
        }
    }
    public class FontConfig
    {
        //宽度与高度,根据所设定的size自动计算出来
        public string family = "Microsoft YaHei";
        public int size = 12;
        public string text = "";
        public string color = "#000000";
        public string bkcolor = "#ffffff";
        //超过多少则换行
        public int width = 0;
    }
}
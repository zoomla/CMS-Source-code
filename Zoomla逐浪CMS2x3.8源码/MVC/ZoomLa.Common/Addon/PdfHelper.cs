using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZoomLa.Common.Addon
{
    /// <summary>
    /// 对IText的封装
    /// </summary>
    public class PdfHelper
    {
        /// <summary>
        /// Html生成PDF,图片以http形式保存(保存后其会存在PDF中)
        /// </summary>
        /// <param name="css">CSS文本</param>
        /// <param name="destPath">PDF保存虚拟路径</param>
        public static void HtmlToPdf(string html, string css, string destPath)
        {
            //1,mThe document has no pages.
            //答:html的样式关闭有问题,如<body>无关闭标签,或列表无法合并
            Document doc = new Document();
            FileStream fs = null;
            try
            {
                fs = new FileStream(function.VToP(destPath), FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();
                //string html = SafeSC.ReadFileStr("/test/test.html"), css = SafeSC.ReadFileStr("/test/test.css");
                using (var htmlMS = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                {
                    using (var cssMS = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)))
                    {
                        var xmlWorker = XMLWorkerHelper.GetInstance();
                        xmlWorker.ParseXHtml(writer, doc, htmlMS, cssMS, System.Text.Encoding.UTF8, new UnicodeFontFactory());
                    }
                }
                doc.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { if (fs != null) { doc.Dispose(); fs.Dispose(); } }
        }
    }
    internal class UnicodeFontFactory : FontFactoryImp
    {
        private static readonly string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
          "arialuni.ttf");//arial unicode MS是完整的unicode字型。
        private static readonly string 标楷体Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
          "KAIU.TTF");//标楷体


        public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color,
          bool cached)
        {
            //可用Arial或标楷体，自己选一个
            BaseFont baseFont = BaseFont.CreateFont(标楷体Path, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            return new Font(baseFont, size, style, color);
        }
    }
}
//C#示例,https://sourceforge.net/p/itextsharp/code/HEAD/tree/book/iTextExamplesWeb/iTextExamplesWeb/iTextInAction2Ed/Chapter09/
//将Html转成PDF，使用Pechkin或iTextSharp各有优缺点
//画面呈现当然是Pechkin比较忠於原始网页(因为采用Webkit引擎)，如果没要进一步控制PDF档案功能的话，可使用Pechkin
//如果要进一步控制PDF功能而且画面只是单纯的白纸黑字，就可以改用iTextSharp套件

//※补充说明，以上最外层的table容器宽度如果是固定宽度而非100%宽，Document物件又设为PageSize.A4的话，输出成PDF可能会仅输出部分

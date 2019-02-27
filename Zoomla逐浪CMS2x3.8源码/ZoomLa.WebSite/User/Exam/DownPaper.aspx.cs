using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using NotesFor.HtmlToOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Visitors;
using ZoomLa.Safe;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
//用于下载试卷,后期扩展增加收费和选择打印纸等功能
public partial class User_Exam_DownPaper : System.Web.UI.Page
{
    /*
     * 1,如何找到筛选到需要替换的图片(含base64)
     * 2,如何将其替换掉
     */ 
    B_User buser = new B_User();
    B_TempUser userBll = new B_TempUser();
    B_Exam_Sys_Papers paperBll = new B_Exam_Sys_Papers();
    private int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
    private string Qids { get { return Request.QueryString["Qids"]; } }
    public string PaperSize { get { return Request.QueryString["PaperSize"] ?? "A4"; } }
    public bool Orient
    {
        get
        {
            bool _isvertical = true;
            if (!string.IsNullOrEmpty(Request.QueryString["Orient"]))
            {
                _isvertical = DataConvert.CBool(Request.QueryString["Orient"]);
            }
            return _isvertical;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo mu = userBll.GetLogin();
            M_Exam_Sys_Papers paperMod = paperBll.SelReturnModel(Mid);
            if (paperMod == null) { function.WriteErrMsg("试卷不存在"); }
            //function.WriteErrMsg("未开放试卷下载功能,请联系管理员");
            HtmlHelper htmlHelp = new HtmlHelper();
            //Document doc = new Document(Server.MapPath("paperTlp.docx"));
            //DocumentBuilder builder = new DocumentBuilder(doc);
            StringWriter sw = new StringWriter();
            //也可不传,将openid=sessionid传上即可获取当前用户
            Server.Execute("/User/Exam/Paper.aspx?id=" + paperMod.id, sw);
            string html = sw.ToString();
            HtmlPage page = htmlHelp.GetPage(html);
            html = page.Body.ExtractAllNodesThatMatch(new HasAttributeFilter("id", "paper"), true).ToHtml();
            //builder.MoveToBookmark("content");//Aspose
            //builder.InsertHtml(html);
            //var docStream = new MemoryStream();
            //doc.Save(docStream, SaveFormat.Docx);
            //SafeSC.DownFile(docStream.ToArray(), paperMod.p_name + ".docx");
            //docStream.Dispose();
            string wordDir = "/Log/Storage/Exam/Paper/";
            string wordPath = wordDir + paperMod.id + ".docx";
            string ppath = Server.MapPath(wordPath);
            //string htmlPath = "/test/123.html";
            if (!Directory.Exists(Server.MapPath(wordDir))) { Directory.CreateDirectory(Server.MapPath(wordDir)); }
            byte[] array = Encoding.UTF8.GetBytes(sw.ToString());
            MemoryStream stream = new MemoryStream(array);             //convert stream 2 string      
            //StreamReader reader = new StreamReader(stream);
            //string text = reader.ReadToEnd();
            using (MemoryStream generatedDocument = new MemoryStream())
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Create(generatedDocument, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = doc.MainDocumentPart;
                    if (mainPart == null)
                    {
                        mainPart = doc.AddMainDocumentPart();
                        new Document(new Body()).Save(mainPart);
                    }
                    HtmlConverter converter = new HtmlConverter(mainPart);

                    //生成格式A4,A3
                    Body docBody = mainPart.Document.Body;
                    SectionProperties sectionProperties = new SectionProperties();
                    PageSize pageSize = new PageSize();
                    PageMargin pageMargin = new PageMargin();
                    //默认为16k大小
                    Columns columns = new Columns() { Space = "220" };//720
                    DocGrid docGrid = new DocGrid() { LinePitch = 100 };//360
                    GetPageSetting(ref pageSize, ref pageMargin, PaperSize, Orient);
                    sectionProperties.Append(pageSize, pageMargin, columns, docGrid);
                    docBody.Append(sectionProperties);

                    var paragraphs = converter.Parse(html);
                    for (int i = 0; i < paragraphs.Count; i++)
                    {
                        docBody.Append(paragraphs[i]);
                    }
                    mainPart.Document.Save();
                }
                SafeC.SaveFile(wordDir, paperMod.id + ".docx", generatedDocument.ToArray());
            }
            #region 旧版生成Word
            //using (WordprocessingDocument myDoc = WordprocessingDocument.Create(ppath, WordprocessingDocumentType.Document))
            //{


            //    string altChunkId = "AltChunkId";
            //    MainDocumentPart mainPart = myDoc.AddMainDocumentPart();
            //    mainPart.Document = new Document();
            //    mainPart.Document.Body = new Body();
            //    var chunk = mainPart.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.Html, altChunkId);

            //    //using (FileStream fileStream =
            //    //        File.Open(Server.MapPath(htmlPath), FileMode.Open))
            //    //{
            //        chunk.FeedData(stream);
            //    //}
            //    AltChunk altChunk = new AltChunk() { Id = altChunkId };
            //    mainPart.Document.Append(altChunk);
            //    function.WriteErrMsg(chunk.);
            //    mainPart.Document.Save();

            //    stream.Dispose();
            //}
            #endregion
            SafeSC.DownFile(wordPath, paperMod.p_name + ".docx");
        }
    }
    /// <summary>
    /// 设置页面大小
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageMargin"></param>
    /// <param name="paperSize">页面大小</param>
    /// <param name="direct">true:横向,false纵向</param>
    public void GetPageSetting(ref PageSize pageSize, ref PageMargin pageMargin, string paperSize = "A4", bool isVertical = true)
    {
        //string str_paperSize = "Letter";//A4,B4
        UInt32Value width = 15840U;
        UInt32Value height = 12240U;
        int top = 1440;
        UInt32Value left = 1440U;
        switch (paperSize)
        {
            case "A4":// 210mm×297mm
                width = 16840U;//80
                height = 11905U;//40
                break;
            case "A3"://297mm×420mm
                width = 23760U;
                height = 16800U;
                break;
            case "B4"://257mm×364mm
                width = 20636U;
                height = 14570U;
                break;
            case "16k":
            default:
                break;
        }
        if (!isVertical)
        {
            UInt32Value sweep = width;
            width = height;
            height = sweep;
            int top_sweep = top;
            top = (int)left.Value;
            left = (uint)top_sweep;
        }

        pageSize.Width = width;
        pageSize.Height = height;
        pageSize.Orient = new EnumValue<PageOrientationValues>(isVertical ? PageOrientationValues.Landscape : PageOrientationValues.Portrait);

        pageMargin.Top = top;
        pageMargin.Bottom = top;
        pageMargin.Left = left;
        pageMargin.Right = left;
        pageMargin.Header = (UInt32Value)720U;
        pageMargin.Footer = (UInt32Value)720U;
        pageMargin.Gutter = (UInt32Value)0U;
    }
}
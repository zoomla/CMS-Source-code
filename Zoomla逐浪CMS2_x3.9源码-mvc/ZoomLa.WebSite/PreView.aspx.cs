using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using Aspose.Words;
using System.Text;
using System.Text.RegularExpressions;
using ZoomLa.Model.Plat;
using ZoomLa.BLL.Plat;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL.Helper;

namespace ZoomLaCMS
{
    public partial class PreView : System.Web.UI.Page
    {
        /*
     * 提供预览支持
     * generalDiv:PDF,图片
     */
        private B_Admin badmin = new B_Admin();
        private B_User buser = new B_User();
        private string[] WordEx = new string[] { "doc", "docx", "rtf" };
        private string[] ExcelEx = new string[] { "xls", "xlsx" };
        private string[] ImgEx = new string[] { "csv", "jpg", "bmp", "png", "gif", "swf" };
        private string[] VideoEx = new string[] { "mp3", "mp4", "flv" };
        //----------
        public string pdfUrl, imgUrl;
        public int viewWidth = 1024, viewHeight = 600;
        public string vpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["vpath"]))
            {
                vpath = Server.UrlDecode(Request["vpath"]);
                while (vpath.Contains("//")) { vpath = vpath.Replace("//", "/"); }
            }
            else if (!string.IsNullOrEmpty(Request["File"]))//Guid,Plat_Doc
            {
                //检测公司等信息
                M_User_Plat upMod = B_User_Plat.GetLogin();
                B_Plat_File fileBll = new B_Plat_File();
                M_Plat_File fileMod = fileBll.SelReturnModel(Request["File"]);
                if (upMod == null || upMod.UserID == 0) { function.WriteErrMsg("你尚未登录能力中心,请先登录!"); }
                if (fileMod == null) { function.WriteErrMsg("文件不存在!"); }
                if (upMod.CompID != fileMod.CompID) { function.WriteErrMsg("你没有该文档的访问权限!"); }
                vpath = fileMod.VPath + fileMod.SFileName;
            }
            else if (!string.IsNullOrEmpty(Request["CloudFile"]))
            {
                B_User_Cloud cloudBll = new B_User_Cloud();
                M_User_Cloud cloudMod = cloudBll.SelReturnModel(Request["CloudFile"]);
                if (cloudMod == null) { function.WriteErrMsg("云端文件不存在!"); }
                if (cloudMod.UserID != buser.GetLogin().UserID) { function.WriteErrMsg("你无权访问该文件!"); }
                vpath = cloudMod.VPath + cloudMod.SFileName;
            }
            else
            {
                function.WriteErrMsg("你没有指定文件");
            }
            vpath = vpath.ToLower().Replace("/uploadfiles/", "");
            vpath = "/uploadfiles/" + SafeSC.PathDeal(vpath);
            string ppath = Server.MapPath(vpath);
            string exName = Path.GetExtension(vpath).ToLower().Replace(".", "");//doc
            if (!File.Exists(function.VToP(vpath))) function.WriteErrMsg(Path.GetFileName(Request.QueryString["vpath"]) + "文件不存在!");
            if (!badmin.CheckLogin() && !buser.CheckLogin())
            {
                function.WriteErrMsg("请先登录,再使用该页面!!!");
            }
            else if (SafeSC.FileNameCheck(vpath))
            {
                function.WriteErrMsg("文件名异常,请去除特殊符号，或更换后缀名");
            }
            else if (Path.GetExtension(vpath).Equals(".config"))
            {
                function.WriteErrMsg("为安全考虑,该类型文件不提供预览服务!!");
            }
            /*------------*/
            if (!string.IsNullOrEmpty(Request.QueryString["width"]))
            {
                viewWidth = Convert.ToInt32(Request.QueryString["width"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["height"]))
            {
                viewHeight = Convert.ToInt32(Request.QueryString["height"]);
            }
            /*------------------------------------------------------------------*/
            if (WordEx.Contains(exName))//Office类预览
            {
                generalDiv.Visible = true;
                string pdfPath = vpath + ".pdf";
                if (!File.Exists(Server.MapPath(pdfPath)))
                {
                    Document doc = new Document(ppath);
                    doc.Save(Server.MapPath(pdfPath), SaveFormat.Pdf);
                }
                pdfDiv.Visible = true;
                pdfUrl = VpathToUrl(pdfPath);
                //if (DeviceHelper.GetBrower() == DeviceHelper.Brower.IE)
                //{
                //    function.WriteErrMsg("IE不支持Word预览,请使用Chrome,Edge或其他浏览器");
                //}
            }
            else if (ExcelEx.Contains(exName))
            {
                //generalDiv.Visible = true;
                //string pdfPath = vpath + ".pdf";
                //if (!File.Exists(Server.MapPath(pdfPath)))
                //{
                //    Aspose.Cells.Workbook excel = new Aspose.Cells.Workbook(Server.MapPath(vpath));
                //    excel.Save(Server.MapPath(pdfPath), Aspose.Cells.SaveFormat.Pdf);
                //}
                //pdfDiv.Visible = true;
                //pdfUrl = VpathToUrl(pdfPath);
                function.WriteErrMsg("暂未开放Excel预览,请联系官方技术支持!");
            }
            else if (exName.Equals("pdf"))
            {
                //if (DeviceHelper.GetBrower() == DeviceHelper.Brower.IE)
                //{
                //    function.WriteErrMsg("IE不支持PDF预览,请使用Chrome,Edge或其他浏览器");
                //}
                pdfDiv.Visible = true;
                pdfUrl = VpathToUrl(vpath);
            }
            else if (ImgEx.Contains(exName))
            {
                imgDiv.Visible = true;
                imgUrl = vpath;
            }
            else if (exName.Equals("html") || exName.Equals("htm"))
            {
                Server.Transfer(vpath);
            }
            else if (exName.Equals("txt"))
            {
                ViewTxt.Visible = true;
                ViewTxt.Text = SafeSC.ReadFileStr(vpath);
                ViewTxt.Width = viewWidth;
                ViewTxt.Height = viewHeight;
            }
            else if (VideoEx.Contains(exName))
            {
                videoDiv.Visible = true;
                function.Script(this, "PlayVideo();");
            }
            else
            {
                function.WriteErrMsg("暂不支持<span style='color:#0066cc;'>[" + exName + "]</span>格式文件预览,请联系管理员提供支持!!");
            }
        }
        // 虚拟路径转Url路径
        public string VpathToUrl(string vpath)
        {
            string url = Regex.Split(Request.Url.OriginalString.Trim(), "/PreView.aspx", RegexOptions.IgnoreCase)[0];
            url += vpath;
            return url;
        }
    }
}
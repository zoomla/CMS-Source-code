using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;

public partial class Common_Upload : System.Web.UI.Page
{
    string m_ShowPath, m_FileExtArr;
    public string Cid { get { return Request.QueryString["Cid"] ?? ""; } }
    public int FType { get { return DataConverter.CLng(Request.QueryString["ftype"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged();
    }
    private string FileSavePath()
    {
        string vpath = SiteConfig.SiteOption.UploadDir + SiteConfig.SiteOption.AdvertisementDir + "/" + DateTime.Now.ToString("yyyyMMdd") + "/";
        vpath = vpath.Replace("//", "/");
        m_ShowPath = vpath;
        return vpath;
    }
    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        if (FType == 1 || FType == 4)
        {
            this.m_FileExtArr = "gif|jpg|png|swf";
        }
        else
        {
            this.m_FileExtArr = "gif|jpg|png|docx|rar|doc";
        }
        string str2 = Path.GetExtension(File_UP.FileName).ToLower();
        if (!this.CheckFilePostfix(str2.Replace(".", "")))
        {
            this.ReturnManage("上传的文件不是符合扩展名" + this.m_FileExtArr + "的文件");
        }
        else
        {
            string fname = DateTime.Now.ToString("yyyyMMddHHmmss") + function.GetRandomString(4) + Path.GetExtension(File_UP.FileName);
            string savePath = "";
            if (SafeSC.IsImage(File_UP.FileName))
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(File_UP.PostedFile.InputStream);
                img = ImgHelper.AddWater(img);
                savePath = ImgHelper.SaveImage(FileSavePath() + fname, img);
            }
            else
            {
                savePath = SafeSC.SaveFile(savePath, File_UP, fname);
            }
            this.GetScriptByModuleName(savePath);
            this.ReturnManage("文件上传成功");
        }
    }
    private void GetScriptByModuleName(string thumbnailPath)
    {
        function.Script(this, "parent.DealwithUploadPic(\"" + thumbnailPath + "\",\"txt" + Cid + "\");");
    }
    private bool CheckFilePostfix(string fileExtension)
    {
        return StringHelper.FoundCharInArr(this.m_FileExtArr.ToLower(), fileExtension.ToLower(), "|");
    }
    private void RepStr(string msg) { Response.Clear(); Response.Write(msg); Response.Flush(); Response.End(); }
    private void ReturnManage(string msg)
    {
        LblMsg_L.Text = msg;
        //if (!string.IsNullOrEmpty(msg))
        //{
        //    function.Script(this, "parent.DealwithUploadErrMessage(\"" + msg + "\");");
        //}
    }
}
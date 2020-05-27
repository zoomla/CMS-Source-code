using System;
using System.IO;
using System.Text;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

namespace ZoomLaCMS.Common
{
    public partial class FileUpload : System.Web.UI.Page
    {
        private string m_FileExtArr;
        protected int m_MaxFileSize;
        protected string m_FileSizeField;
        private string m_ShowPath;
        protected M_ModelField m_ModelField;
        protected M_Node m_NodeInfo;
        protected B_Node bnode = new B_Node();
        protected B_ModelField bfield = new B_ModelField();
        protected B_User buser = new B_User();
        B_Admin admin = new B_Admin();

        public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        public int ModelID { get { return DataConverter.CLng(Request.QueryString["ModelID"]); } }
        //控件ID
        public string FieldName { get { return Request.QueryString["FieldName"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!buser.CheckLogin() && !admin.CheckLogin()) || (string.IsNullOrEmpty(FieldName)))
            {
                Response.Write("非法请求不能访问!"); Response.Flush(); Response.End();
                return;
            }

            this.InitFileExtArr();
        }

        private void ReturnManage(string msg)
        {
            LblMessage.Text = msg;
            //if (!string.IsNullOrEmpty(manage))
            //{
            //    StringBuilder builder = new StringBuilder();
            //    builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
            //    builder.Append("   parent.DealwithUploadErrMessage(\"" + manage + "\");");
            //    builder.Append("</script>");
            //    this.Page.ClientScript.RegisterStartupScript(base.GetType(), "script", builder.ToString());
            //}
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            long size = FupFile.FileContent.Length;

            if (!this.FupFile.HasFile)
            {
                this.ReturnManage("请指定一个上传文件");
            }
            else
            {
                if (!SiteConfig.SiteOption.EnableUploadFiles)
                {
                    this.ReturnManage("本站不允许上传文件！");
                }
                else
                {
                    string str2 = Path.GetExtension(this.FupFile.FileName).ToLower();

                    this.m_FileExtArr = SiteConfig.SiteOption.UploadFileExts;
                    this.m_MaxFileSize = SiteConfig.SiteOption.UploadPicMaxSize;//DataConverter.CLng(this.ViewState["MaxFileSize"].ToString());

                    if (((int)this.FupFile.FileContent.Length) > (this.m_MaxFileSize * 0x400))
                    {
                        this.ReturnManage("上传的文件超过限制的" + this.m_MaxFileSize + "KB大小");
                        return;
                    }
                    string str3 = function.GetRandomString(6);
                    string foldername = SiteConfig.SiteOption.UploadDir + this.FileSavePath();
                    string filename = FileSystemObject.CreateFileFolder(foldername, HttpContext.Current) + str3 + str2;//文件名+后缀名

                    if (WaterModuleConfig.WaterConfig.IsUsed && (str2 == ".jpg" || str2 == ".png") && HiddenNodeDir.Value != null && HiddenNodeDir.Value != "")
                    {
                        string allvpath = FileSystemObject.CreateFileFolder(foldername, HttpContext.Current) + str3 + str2;
                        allvpath = SafeSC.SaveFile(function.PToV(foldername), FupFile, str3 + str2);
                        allvpath = ImgHelper.AddWater(allvpath);
                        this.m_ShowPath = allvpath.Replace(SiteConfig.SiteOption.UploadDir, "");
                    }
                    else
                    {
                        string allvpath = SafeSC.SaveFile(function.PToV(filename), FupFile);
                        this.m_ShowPath = allvpath.Replace(SiteConfig.SiteOption.UploadDir, "");
                    }
                    int sizes = (int)this.FupFile.FileContent.Length;
                    string thumbnailPath = this.m_ShowPath;//fsxm/jpg/2015/5/:OPjUWd:.jpg
                    this.GetScriptByModuleName(thumbnailPath, sizes);
                    this.ReturnManage("文件上传成功");
                }
            }
        }

        private string FileSavePath()
        {
            string str = "";
            if (SiteConfig.SiteOption.EnableUploadFiles)
            {
                if (NodeID <= 0)
                {
                    str = "UserUpload/{$FileType}/{$Year}/{$Month}/";
                }
                else
                {
                    this.m_NodeInfo = bnode.GetNodeXML(NodeID);
                    str = this.m_NodeInfo.NodeDir + "/{$FileType}/{$Year}/{$Month}/";
                    // str = "/UploadFiles" + "/{$FileType}/{$Year}/{$Month}/";
                    HiddenNodeDir.Value = this.m_NodeInfo.NodeDir;
                }
                str = str.Replace("{$FileType}", Path.GetExtension(this.FupFile.FileName).ToLower().Replace(".", "")).Replace("{$Year}", DateTime.Now.Year.ToString()).Replace("{$Month}", DateTime.Now.Month.ToString()).Replace("\\", "/");
                this.m_ShowPath = str;
            }
            return str;
        }

        private void GetScriptByModuleName(string thumbnailPath, int size)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<script language=\"javascript\" type=\"text/javascript\">");

            if (this.ViewState["UploadType"] != null)
            {
                if (this.ViewState["UploadType"].ToString() == "FileType")
                {
                    string sizeid = this.ViewState["SizeField"].ToString();
                    builder.Append("  parent.DealwithUpload(\"" + thumbnailPath + "\",\"" + size + "\",\"sel_" + FieldName + "\",\"txt_" + FieldName + "\",\"txt_" + sizeid + "\");");
                }

                if (this.ViewState["UploadType"].ToString() == "PicType" || this.ViewState["UploadType"].ToString() == "SmallFileType")
                {

                    builder.Append("  parent.DealwithUploadPic(\"" + thumbnailPath + "\",\"txt_" + FieldName + "\");parent.DealwithUploadImg(\"" + SiteConfig.SiteOption.UploadDir + "/" + thumbnailPath + "\",\"Img_" + FieldName + "\");");
                }
            }
            else
            {
                builder.Append("  parent.DealwithUploadPic(\"" + thumbnailPath + "\",\"txt_" + FieldName + "\");parent.DealwithUploadImg(\"" + SiteConfig.SiteOption.UploadDir + thumbnailPath + "/" + "\",\"Img_" + FieldName + "\");");
            }
            builder.Append("</script>");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
        }

        private void InitFileExtArr()
        {
            string content = "";
            string fieldType = "";

            if (ModelID > 0)
            {
                B_ModelField bmf = new B_ModelField();
                M_ModelField mmf = bmf.GetModelByFieldName(ModelID, FieldName);
                mmf = (mmf == null) ? new M_ModelField() : mmf;
                content = mmf.Content;
                fieldType = mmf.FieldType;
            }
            else
            {
                B_UserBaseField bubf = new B_UserBaseField();
                M_UserBaseField mubf = bubf.getUserBaseFieldByFieldName(FieldName);
                mubf = (mubf == null) ? new M_UserBaseField() : mubf;
                content = mubf.Content;
                fieldType = mubf.FieldType;
            }
            if (content != null)
            {
                string[] Setting = content.Split(new char[] { ',' });
                if (fieldType == "PicType")
                {
                    this.m_MaxFileSize = DataConverter.CLng(Setting[1].Split(new char[] { '=' })[1]);
                    this.ViewState["MaxFileSize"] = Setting[1].Split(new char[] { '=' })[1];
                    this.m_FileExtArr = Setting[2].Split(new char[] { '=' })[1];
                    this.ViewState["FileExtArr"] = Setting[2].Split(new char[] { '=' })[1];
                    this.ViewState["UploadType"] = "PicType";
                }
                if (fieldType == "FileType")
                {
                    string chkSize = Setting[0].Split(new char[] { '=' })[1];
                    string SizeField = Setting[1].Split(new char[] { '=' })[1];
                    this.ViewState["SizeField"] = SizeField;
                    this.m_MaxFileSize = DataConverter.CLng(Setting[2].Split(new char[] { '=' })[1]);
                    this.ViewState["MaxFileSize"] = Setting[2].Split(new char[] { '=' })[1];
                    this.m_FileExtArr = Setting[3].Split(new char[] { '=' })[1];
                    this.ViewState["FileExtArr"] = Setting[3].Split(new char[] { '=' })[1];
                    this.ViewState["UploadType"] = "FileType";
                }

                if (fieldType == "SmallFileType")
                {
                    string chkSize = Setting[0].Split(new char[] { '=' })[1];
                    string SizeField = Setting[1].Split(new char[] { '=' })[1];
                    this.ViewState["SizeField"] = SizeField;
                    this.m_MaxFileSize = DataConverter.CLng(chkSize);
                    this.ViewState["MaxFileSize"] = chkSize;
                    this.m_FileExtArr = SizeField;
                    this.ViewState["FileExtArr"] = SizeField;
                    this.ViewState["UploadType"] = "SmallFileType";
                }
            }
        }
    }
}
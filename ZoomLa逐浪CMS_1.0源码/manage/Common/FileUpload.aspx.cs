using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;
namespace ZoomLa.WebSite.Manage.Common
{
    public partial class FileUpload : System.Web.UI.Page
    {
        private string m_FieldName;
        private string m_FileExtArr;
        protected int m_MaxFileSize;
        protected string m_FileSizeField;
        private int m_ModelId;        
        private string m_ShowPath;
        protected M_ModelField m_ModelField;
        protected M_Node m_NodeInfo;        
        protected B_Node bnode=new B_Node();
        protected B_ModelField bfield=new B_ModelField();
        protected B_Admin badmin = new B_Admin();

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (!this.FupFile.HasFile)
            {
                this.ReturnManage("请指定一个上传文件");
            }
            else
            {
                
                this.m_FileExtArr = this.ViewState["FileExtArr"].ToString();
                this.m_MaxFileSize = DataConverter.CLng(this.ViewState["MaxFileSize"].ToString());
                if (!SiteConfig.SiteOption.EnableUploadFiles)
                {
                    this.ReturnManage("本站不允许上传文件！");
                }
                else
                {
                    string str2 = Path.GetExtension(this.FupFile.FileName).ToLower();
                    if (string.IsNullOrEmpty(this.m_FileExtArr))
                    {
                        this.ReturnManage("要上传文件的字段没有指定上传文件类型");
                    }
                    else if (!this.CheckFilePostfix(str2.Replace(".", "")))
                    {
                        this.ReturnManage("上传的文件不是符合扩展名" + this.m_FileExtArr + "的文件");
                    }
                    else
                    {
                        if (((int) this.FupFile.FileContent.Length) > (this.m_MaxFileSize * 0x400))
                        {
                            this.ReturnManage("上传的文件超过限制的" + this.m_MaxFileSize + "KB大小");
                            return;
                        }                        
                        string str3 = DataSecurity.MakeFileRndName();
                        string foldername=base.Request.PhysicalApplicationPath+(VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + this.FileSavePath()).Replace("/", "\\");
                        string filename = FileSystemObject.CreateFileFolder(foldername, HttpContext.Current) + str3 + str2;
                        int sizes=(int)this.FupFile.FileContent.Length;
                        this.FupFile.SaveAs(filename);
                        string thumbnailPath = "";
                        
                        thumbnailPath = this.m_ShowPath + str3 + str2;

                        this.GetScriptByModuleName(thumbnailPath, sizes);
                        
                        this.ReturnManage("文件上传成功");
                    }
                }
            }
        }

        private bool CheckFilePostfix(string fileExtension)
        {
            return StringHelper.FoundCharInArr(this.m_FileExtArr.ToLower(), fileExtension.ToLower(), "|");
        }

        private string FileSavePath()
        {
            string str = "";
            if (SiteConfig.SiteOption.EnableUploadFiles)
            {
                this.m_NodeInfo = bnode.GetNode(DataConverter.CLng(this.ViewState["NodeId"].ToString()));
                str = this.m_NodeInfo.NodeDir + "/{$FileType}/{$Year}/{$Month}/";                
                str=str.Replace("{$FileType}", Path.GetExtension(this.FupFile.FileName).ToLower().Replace(".", "")).Replace("{$Year}", DateTime.Now.Year.ToString()).Replace("{$Month}", DateTime.Now.Month.ToString()).Replace("\\", "/");
                this.m_ShowPath = str;
            }
            return str;                   
        }

        private void GetScriptByModuleName(string thumbnailPath,int size)
        {
            string id = this.ViewState["FieldName"].ToString();
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
            if (this.ViewState["UploadType"].ToString() == "FileType")
            {
                string sizeid = this.ViewState["SizeField"].ToString();
                builder.Append("  parent.DealwithUpload(\"" + thumbnailPath + "\",\"" + size + "\",\"sel_" + id + "\",\"txt_" + id + "\",\"txt_" + sizeid + "\");");
            }
            if (this.ViewState["UploadType"].ToString() == "PicType")
            {
                builder.Append("  parent.DealwithUploadPic(\"" + thumbnailPath + "\",\"txt_" + id + "\");");
            }
            builder.Append("</script>");
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
        }

        private void InitFileExtArr()
        {
            this.m_ModelId = DataConverter.CLng(this.ViewState["ModelId"].ToString());
            this.m_FieldName=this.ViewState["FieldName"].ToString();
            this.m_ModelField = bfield.GetModelByFieldName(this.m_ModelId, this.m_FieldName);
            string[] Setting=this.m_ModelField.Content.Split(new char[]{','});
            if (this.m_ModelField.FieldType == "PicType")
            {
                this.m_MaxFileSize = DataConverter.CLng(Setting[1].Split(new char[] { '=' })[1]);
                this.ViewState["MaxFileSize"] = Setting[1].Split(new char[] { '=' })[1];
                this.m_FileExtArr = Setting[2].Split(new char[] { '=' })[1];
                this.ViewState["FileExtArr"] = Setting[2].Split(new char[] { '=' })[1];
                this.ViewState["UploadType"] = "PicType";
            }
            if (this.m_ModelField.FieldType == "FileType")
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.badmin.CheckMulitLogin();
            this.ViewState["NodeId"] = base.Request.QueryString["NodeId"];
            this.ViewState["ModelId"] = base.Request.QueryString["ModelID"];
            this.ViewState["FieldName"] = base.Request.QueryString["FieldName"];            
            this.InitFileExtArr();
        }

        private void ReturnManage(string manage)
        {
            if (!string.IsNullOrEmpty(manage))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
                builder.Append("   parent.DealwithUploadErrMessage(\"" + manage + "\");");
                builder.Append("</script>");
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
            }
        }
    }
}
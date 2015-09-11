using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Components;
namespace ZoomLa.WebSite.Manage.Common
{
    public partial class MultiPicUpload : System.Web.UI.Page
    {
        private string m_FieldName;
        private string m_FileExtArr;
        protected int m_MaxFileSize;
        protected string m_FileSizeField;
        private int m_ModelId;
        private string m_ShowPath;
        protected M_ModelField m_ModelField;
        protected M_Node m_NodeInfo;        
        protected B_Node bnode = new B_Node();
        protected B_ModelField bfield = new B_ModelField();
        protected B_Admin badmin = new B_Admin();

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            
            this.m_FileExtArr = this.ViewState["FileExtArr"].ToString();
            this.m_MaxFileSize = DataConverter.CLng(this.ViewState["MaxFileSize"].ToString());
            if (!SiteConfig.SiteOption.EnableUploadFiles)
            {
                this.ReturnManage("本站不允许上传文件！");
            }
            else
            {
                if (string.IsNullOrEmpty(this.m_FileExtArr))
                {
                    this.ReturnManage("要上传文件的字段没有指定上传文件类型");
                }
                else
                {
                    int num = DataConverter.CLng(base.Request.Form["ThumbIndex"]);
                    string id = this.ViewState["FieldName"].ToString();
                    string thumbid = this.ViewState["ThumbField"].ToString();
                    StringBuilder builder = new StringBuilder();
                    builder.Append("<script language=\"javascript\" type=\"text/javascript\">");
                    int num2 = 0;
                    StringBuilder builder2 = new StringBuilder();
                    for (int i = 0; i < 10; i++)
                    {
                        num2++;
                        System.Web.UI.WebControls.FileUpload upload = (System.Web.UI.WebControls.FileUpload)this.FindControl("FileUpload" + i.ToString());
                        if (upload.HasFile)
                        {
                            string str2 = Path.GetExtension(upload.FileName).ToLower();
                            if (!this.CheckFilePostfix(str2.Replace(".", "")))
                            {
                                builder2.Append("文件" + upload.FileName + "不符合扩展名规则" + this.m_FileExtArr + @"\r\n");
                            }
                            else
                            {
                                if (((int)upload.FileContent.Length) > (this.m_MaxFileSize * 0x400))
                                {
                                    builder2.Append("文件" + upload.FileName + "大小超过" + this.m_MaxFileSize + @"KB\r\n");
                                }
                                else
                                {
                                    string str3 = DataSecurity.MakeFileRndName() + i.ToString();
                                    string foldername = base.Request.PhysicalApplicationPath + (VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + this.FileSavePath(upload.FileName)).Replace("/", "\\");
                                    string filename = FileSystemObject.CreateFileFolder(foldername, HttpContext.Current) + str3 + str2;
                                    int sizes = (int)upload.FileContent.Length;
                                    upload.SaveAs(filename);
                                    string thumbnailPath = "";

                                    thumbnailPath = this.m_ShowPath + str3 + str2;

                                    if (i == num)
                                    {
                                        builder.Append("parent.ChangeThumbField(\"" + thumbnailPath + "\",\"sel_" + id + "\",\"txt_" + id + "\",\"txt_" + thumbid + "\");");
                                    }
                                    else
                                    {
                                        builder.Append("parent.DealwithPhotoUpload(\"" + thumbnailPath + "\",\"sel_" + id + "\",\"txt_" + id + "\");");
                                    }
                                    builder2.Append("文件" + upload.FileName + @"上传成功\r\n");
                                }  
                            }
                        }
                    }
                    if (builder2.Length > 0)
                    {
                        builder.Append("parent.DealwithUploadErrMessage(\"" + builder2.ToString() + "\");");
                    }
                    builder.Append("</script>");
                    this.Page.ClientScript.RegisterStartupScript(base.GetType(), "UpdateParent", builder.ToString());
                }
            }
        }

        private bool CheckFilePostfix(string fileExtension)
        {
            return StringHelper.FoundCharInArr(this.m_FileExtArr.ToLower(), fileExtension.ToLower(), "|");
        }

        private string FileSavePath(string fileName)
        {
            string str = "";
            if (SiteConfig.SiteOption.EnableUploadFiles)
            {
                this.m_NodeInfo = bnode.GetNode(DataConverter.CLng(this.ViewState["NodeId"].ToString()));
                str = this.m_NodeInfo.NodeDir + "/{$FileType}/{$Year}/{$Month}/";
                str = str.Replace("{$FileType}", Path.GetExtension(fileName).ToLower().Replace(".", "")).Replace("{$Year}", DateTime.Now.Year.ToString()).Replace("{$Month}", DateTime.Now.Month.ToString()).Replace("\\", "/");
                this.m_ShowPath = str;
            }
            return str;
        }        

        private void InitFileExtArr()
        {
            this.m_ModelId = DataConverter.CLng(this.ViewState["ModelId"].ToString());
            this.m_FieldName = this.ViewState["FieldName"].ToString();
            this.m_ModelField = bfield.GetModelByFieldName(this.m_ModelId, this.m_FieldName);
            string[] Setting = this.m_ModelField.Content.Split(new char[] { ',' });
            string ChkThumb = Setting[0].Split(new char[] { '=' })[1];

            string ThumbField = Setting[1].Split(new char[] { '=' })[1];
            this.ViewState["ThumbField"] = ThumbField;
            this.m_MaxFileSize = DataConverter.CLng(Setting[2].Split(new char[] { '=' })[1]);
            this.ViewState["MaxFileSize"] = Setting[3].Split(new char[] { '=' })[1];
            this.m_FileExtArr = Setting[4].Split(new char[] { '=' })[1];
            this.ViewState["FileExtArr"] = Setting[4].Split(new char[] { '=' })[1];
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
                this.LblMessage.Text = manage;                
            }
        }
    }
}
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Text;
using System.IO;
using ZoomLa.Common;
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class ProjectColumnAdd : System.Web.UI.Page
    {
        private string m_FileExtArr;
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>项目管理</li><li>字段添加</li>");
            if (!Page.IsPostBack)
            {
                InitPage();
                MyBind();
            }
        }
        private void MyBind()
        {
            if (Request.QueryString["Pid"] != null)
            {
                //this.Egv.DataSource = this.bprojectfield.GetProjectFieldByid(Convert.ToInt32(Request.QueryString["Pid"].Trim()));
                //this.Egv.DataBind();
            }
        }
        private void InitPage()
        {
            int pid = 0;
            int PFid = 0;
            if (Request.QueryString["Pid"] != null)
            {
                pid = Convert.ToInt32(Request.QueryString["Pid"].Trim());
                //M_Project mproject = bproject.GetProjectByid(pid);
                //TxtProjectName.Text = mproject.ProjectName;
            }
            if (Request.QueryString["PFId"] != null)
            {
                PFid = Convert.ToInt32(Request.QueryString["PFid"].Trim());
                //M_ProjectField mprojectfield = this.bprojectfield.GetProjectFieldByPFid(PFid);
                //TxtProjectConlumn.Text = mprojectfield.FieldName;
                //TxtAlias.Text = mprojectfield.Alias;
                //TxtColumnDefault.Text = mprojectfield.FieldValue;
                //for (int i = 0; i < DDLFieldType.Items.Count; i++)
                //{
                //    if (DDLFieldType.Items[i].Value == mprojectfield.Type)
                //    {
                //        DDLFieldType.SelectedIndex = i;
                //    }
                //}

            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            //bool address = false;//局域网 //win04/d/web和http:// 有地址两字 就设为true
            int pid = Convert.ToInt32(Request.QueryString["Pid"].Trim());
            string wordpath = string.Empty;
            string FieldName = string.Empty;
            //mprojectfield.ProjectID = pid;
            FieldName = TxtProjectConlumn.Text.Trim();
            //mprojectfield.FieldName = FieldName;
            //if (CheckField(FieldName))
            //    address = true;
            //mprojectfield.Alias = TxtAlias.Text.Trim();
            //mprojectfield.Type = DDLFieldType.SelectedValue;
            wordpath = TxtColumnDefault.Text.Trim();
            //if ((wordpath.IndexOf("http://") > 0)||(wordpath.IndexOf("//") > 0)||(wordpath.IndexOf("\\") > 0))
            //    address = true;
            //if (address)
            //    wordpath = "<a href='" + wordpath + "' target='_blank'>" + wordpath + "</a>&nbsp;&nbsp;&nbsp;&nbsp<a href='" + wordpath + "' target='_blank'>点击查看</a>";
            //if (CBdoc.Checked)//字段值是url 相对或绝对
            //{
            //    mprojectfield.FieldValue = "<a href='../../" + wordpath + "' target='_blank'>点击查看</a>";

            //}
            //else
            //{
            // mprojectfield.FieldValue = wordpath;
            //}
            if (Request.QueryString["PFId"] != null)
            {
                //mprojectfield.ID = Convert.ToInt32(Request.QueryString["PFId"].Trim());
                //bool flag = this.bprojectfield.Update(mprojectfield);
                //if (flag)
                //    Response.Write("<script language=javascript> alert('项目字段修改成功！');window.document.location.href='ProjectColumnAdd.aspx?Pid=" + Request.QueryString["Pid"] + "';</script>");
            }
            else
            {
                //if (this.bprojectfield.Add(mprojectfield))//'ProjectColumnAdd.aspx?Pid='"+Request.QueryString["Pid"]+"
                //    Response.Write("<script language=javascript> alert('项目字段添加成功！');window.document.location.href=window.document.location.href;</script>");
            }
        }
        private bool CheckField(string FieldName)
        {

            if ((FieldName.IndexOf("地址") > 0) || (FieldName.IndexOf("文档") > 0))
                return true;
            else
                return false;
        }
        protected void EBtnModify_Click(object sender, EventArgs e)
        {

        }
        protected void CBdoc_CheckedChanged(object sender, EventArgs e)
        {
            if (CBdoc.Checked)
            {
                FileUpload1.Visible = true;
                Button1.Visible = true;
            }
            else
            {
                FileUpload1.Visible = false;
                Button1.Visible = false;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            this.m_FileExtArr = "doc|docx|txt";
            string str3 = "";
            string foldername = "";
            string filename = "";
            string str2 = "";
            System.Web.UI.WebControls.FileUpload upload = (System.Web.UI.WebControls.FileUpload)this.FindControl("FileUpload1");
            StringBuilder builder2 = new StringBuilder();

            string uploadFile;
            if (SiteConfig.SiteOption.UploadDir == null)
            {
                uploadFile = "UploadFiles";
            }
            else
            {
                uploadFile = SiteConfig.SiteOption.UploadDir;
            }

            if (upload.HasFile)
            {
                str2 = Path.GetExtension(upload.FileName).ToLower();
                if (!this.CheckFilePostfix(str2.Replace(".", "")))
                {
                    builder2.Append("文档" + upload.FileName + "不符合图片扩展名规则" + this.m_FileExtArr + @"\r\n");
                }
                else
                {
                    if (((int)upload.FileContent.Length) > (100 * 0x400))
                    {
                        builder2.Append("文档" + upload.FileName + "大小超过100" + @"KB\r\n");
                    }
                    else
                    {
                        str3 = DataSecurity.MakeFileRndName();
                        foldername = base.Request.PhysicalApplicationPath + (VirtualPathUtility.AppendTrailingSlash("/" + uploadFile + "/Project")).Replace("/", "\\");// + this.FileSavePath()
                        filename = FileSystemObject.CreateFileFolder(foldername, HttpContext.Current) + str3 + str2;
                        //SafeSC.SaveFile(filename,upload);
                        if (!FileUpload1.SaveAs(filename)) { function.WriteErrMsg(FileUpload1.ErrorMsg); }
                        builder2.Append("文档" + upload.FileName + @"上传成功\r\n");
                        this.TxtColumnDefault.Text = uploadFile + "/Project/" + str3 + str2;
                    }
                }
            }
            if (builder2.Length > 0)
            {
                string message = "<script language=\"javascript\" type=\"text/javascript\">alert('" + builder2.ToString() + "');</script>";
                function.Script(Page, "alert(message)");
            }

        }
        private bool CheckFilePostfix(string fileExtension)
        {
            return StringHelper.FoundCharInArr(this.m_FileExtArr.ToLower(), fileExtension.ToLower(), "|");
        }
        protected void CanCel_Click(object sender, EventArgs e)
        {
            string str = Request.QueryString["Pid"];
            Page.Response.Redirect("WorkManage.aspx?Pid=" + str);
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DelField":
                    int Id = Convert.ToInt32(e.CommandArgument.ToString());
                    //this.bprojectfield.DeleteByFID(Id);
                    MyBind();
                    break;
                case "ModifyField":
                    int PFId = Convert.ToInt32(e.CommandArgument.ToString());
                    Page.Response.Redirect("ProjectColumnAdd.aspx?Pid=" + Request.QueryString["Pid"] + "&PFId=" + PFId);
                    break;

            }
        }
        protected void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                cbox.Checked = cbAll.Checked;
            }
        }
        //批量删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            int flag = 0;
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    btnDel.Attributes.Add("OnClientClick", "return confirm('你确定要删除吗？');");
                    int PF = DataConverter.CLng(Egv.DataKeys[i].Value);
                    //if (this.bprojectfield.DeleteByFID(PF))
                    //    flag++;
                }
            }
            if (flag > 0)
                MyBind();
        }
    }
}
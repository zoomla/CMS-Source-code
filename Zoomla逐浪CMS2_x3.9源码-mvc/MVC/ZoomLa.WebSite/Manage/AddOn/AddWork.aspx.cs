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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
namespace ZoomLaCMS.Manage.AddOn
{
    public partial class AddWork : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitPage();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Projects.aspx'>项目管理</a></li><li><a href='WorkManage.aspx'>流程管理</a></li><li class='active'>添加流程</li>" + Call.GetHelp(62));
            }

        }
        public void InitPage()
        {
            if (Request.QueryString["Pid"] != null)//这项目添加内容
            {
                this.Literal1.Text = "添加流程";
                LblTitleAdd.Visible = true;
                LblTitleModify.Visible = false;
            }
            if (Request.QueryString["Wid"] != null)//修改内容
            {
                this.Literal1.Text = "修改流程";
                LblTitleAdd.Visible = false;
                LblTitleModify.Visible = true;
                HFWid.Value = Request.QueryString["Wid"].Trim();
                //mprojectwork = bprojectwork.SelectWorkByWID(DataConverter.CLng(Request.QueryString["Wid"].Trim()));
                //TxtWorkName.Text = mprojectwork.WorkName;
                //TxtWorkIntro.Text = mprojectwork.WorkIntro;
                EBtnSubmit.Visible = false;
                EBtnModify.Visible = true;
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {

        }
        protected void EBtnModify_Click(object sender, EventArgs e)
        {

        }
    }
}
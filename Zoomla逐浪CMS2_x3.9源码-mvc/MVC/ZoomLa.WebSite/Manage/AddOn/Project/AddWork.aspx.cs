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
using ZoomLa.BLL.Project;
using ZoomLa.Model.Project;


namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class AddWork : System.Web.UI.Page
    {

        B_Pro_Flow flowBll = new B_Pro_Flow();
        public int Fid { get { return Request.QueryString["id"] == null ? 0 : DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Projects.aspx'>项目管理</a></li><li><a href='WorkManage.aspx'>流程管理</a></li><li class='active'>编辑流程</li>" + Call.GetHelp(62));
            }

        }
        public void MyBind()
        {
            if (Fid > 0)
            {
                M_Pro_Flow flowMod = flowBll.SelReturnModel(Fid);
                if (flowMod != null)
                {
                    LblTitle.Text = "修改流程";
                    EBtnSubmit.Text = "修改";
                    TxtWorkName.Text = flowMod.WorkName;
                    TxtWorkIntro.Text = flowMod.WorkIntro;
                }
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_Pro_Flow flowMod = new M_Pro_Flow();
            if (Fid > 0)
            {
                flowMod = flowBll.SelReturnModel(Fid);
                flowMod.WorkName = TxtWorkName.Text;
                flowMod.WorkIntro = TxtWorkIntro.Text;
                flowBll.UpdateByID(flowMod);
            }
            else
            {
                flowMod.WorkName = TxtWorkName.Text;
                flowMod.WorkIntro = TxtWorkIntro.Text;
                flowMod.ProjectID = 0;
                flowMod.Status = 0;
                flowBll.Insert(flowMod);
            }
            function.WriteSuccessMsg("操作成功!", "WorkManage.aspx");
        }
    }
}
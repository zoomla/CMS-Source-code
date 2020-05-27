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
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class AddProjectCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li><a href='ProjectManage.aspx'>项目管理</a></li>");
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            //this.mpro.ProjectCategoryName = TxtProCateName.Text.Trim();
            //this.mpro.ProjectCategoryIntro = TxtProCateIntro.Text.Trim();
            //if(this.bpro.Add(this.mpro))
            //    Response.Write("<script language=javascript> alert('项目分类添加成功！');window.document.location.href='ProjectCategoryManage.aspx';</script>");
        }
        protected void EBtnModify_Click(object sender, EventArgs e)
        {

        }
    }
}
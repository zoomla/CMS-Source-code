using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;

namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class ProjectsModelField : System.Web.UI.Page
    {
        M_ModelField mmf = new M_ModelField();
        B_ModelField bll = new B_ModelField();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.model, "ShopModelManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                Bind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Projects.aspx'>项目管理</a></li><li class='active'>字段列表<a href='../../Content/AddModelField.aspx?ModelType=10&ModelID=1'>[添加字段]</a></li>" + Call.GetHelp(46));
        }
        protected void Bind()
        {
            this.RepSystemModel.DataSource = bll.GetProjectsStore();
            this.RepSystemModel.DataBind();
            //this.RepModelField.DataSource = bpf.Select_All();
            this.RepModelField.DataBind();
        }
        protected string GetStyleTrue(string isnotnull)
        {
            if (DataConverter.CBool(isnotnull))
                return "<font color=green>√</font>";
            else
                return "<font color=red>×</font>";
        }
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpMove")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                //mpf = this.bpf.GetSelect(Id);
                //M_ProjectBaseField FieldPre = this.bpf.GetPreField(mpf.OrderId);
                //if (mpf.OrderId != FieldPre.OrderId)
                //{
                //    int CurrOrder = mpf.OrderId;
                //    mpf.OrderId = FieldPre.OrderId;
                //    FieldPre.OrderId = CurrOrder;
                //    this.bpf.GetUpdate(mpf);
                //    this.bpf.GetUpdate(FieldPre);
                //}
            }
            if (e.CommandName == "DownMove")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                //mpf = this.bpf.GetSelect(Id);
                //M_ProjectBaseField FieldNext = this.bpf.GetNextField(mpf.OrderId);
                //if (mpf.OrderId != FieldNext.OrderId)
                //{
                //    int CurrOrder = mpf.OrderId;
                //    mpf.OrderId = FieldNext.OrderId;
                //    FieldNext.OrderId = CurrOrder;
                //    this.bpf.GetUpdate(mpf);
                //    this.bpf.GetUpdate(FieldNext);
                //}
            }
            if (e.CommandName == "Delete")
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                // bpf.DeleteByGroupID(Id);
            }
            Bind();

        }
    }
}
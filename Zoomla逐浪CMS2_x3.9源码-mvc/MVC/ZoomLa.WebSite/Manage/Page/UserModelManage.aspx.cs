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
namespace ZoomLaCMS.Manage.Page
{
    public partial class UserModelManage : CustomerPageAction
    {
        private B_Model bll = new B_Model();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZLEnum.Auth.page, "PageModelManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                DataBaseList();
            }
            string menu = base.Request.QueryString["menu"];
            if (menu == "del")
            {
                int modeid = DataConverter.CLng(Request.QueryString["id"]);
                if (bll.DelModel(modeid))
                {
                    Response.Write("<script language=javascript>alert('删除成功!');location.href='UserModelManage.aspx';</script>");
                }
                else {
                    Response.Write("<script language=javascript>alert('删除失败!');</script>");
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li><li class='active'>黄页申请设置<a href='UserModel.aspx'>[添加申请模型]</a></li>" + Call.GetHelp(85));
        }
        private void DataBaseList()
        {
            B_ModelField mdll = new B_ModelField();
            this.Repeater1.DataSource = this.bll.GetListPage();
            this.Repeater1.DataBind();
        }
        public string showdelbotton(string id)
        {

            int tempid = DataConverter.CLng(id);
            string r1 = "<a href=\"?menu=del&id=" + tempid + "\" OnClick=\"return confirm('确实要删除此会员模型吗？');\" class='option_style'><i class='fa fa-trash-o' title='删除'></i></a> ";
            return r1;
        }
        protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string Id = e.CommandArgument.ToString();
                Response.Redirect("UserModel.aspx?ModelID=" + Id);
            }
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                this.bll.DelModel(DataConverter.CLng(Id));
                Response.Write("<script language=javascript>alert('删除成功!');location.href='UserModelManage.aspx';</script>");


            }
            if (e.CommandName == "Field")
            {
                string Id = e.CommandArgument.ToString();

                Response.Redirect("../Content/ModelField.aspx?ModelID=" + Id + "&ModelType=4");
            }
        }
    }
}
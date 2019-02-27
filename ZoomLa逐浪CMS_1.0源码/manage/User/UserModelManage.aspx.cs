namespace Zoomla.WebSite.Manage.User
{
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

    public partial class UserModelManage : System.Web.UI.Page
    {
        private B_Model bll = new B_Model();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("UserModel"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                DataBaseList();
            }
        }
        private void DataBaseList()
        {
            this.Repeater1.DataSource = this.bll.GetListUser();
            this.Repeater1.DataBind();
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
                DataBaseList();
            }
            if (e.CommandName == "Field")
            {
                string Id = e.CommandArgument.ToString();
                Response.Redirect("UserModelField.aspx?ModelID=" + Id);
            }
        }
    }
}
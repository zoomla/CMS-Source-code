namespace ZoomLa.WebSite.Manage
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
    using ZoomLa.Web;
    using ZoomLa.Common;

    public partial class ModelManage : System.Web.UI.Page
    {
        private B_Model bll = new B_Model();

        private void DataBaseList()
        {
            this.Repeater1.DataSource = this.bll.GetList();
            this.Repeater1.DataBind();
        }
        public string GetIcon(string IconPath)
        {
            return "../../Images/ModelIcon/" + (string.IsNullOrEmpty(IconPath) ? "Default.gif" : IconPath);
        }
        protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string Id = e.CommandArgument.ToString();
                Response.Redirect("AddEditModel.aspx?ModelID=" + Id);
            }
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                this.bll.DelModel(int.Parse(Id));
                DataBaseList();
            }
            if (e.CommandName == "Field")
            {
                string Id = e.CommandArgument.ToString();
                Response.Redirect("ModelField.aspx?ModelID=" + Id);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ModelManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                DataBaseList();
            }
        }
    }
}
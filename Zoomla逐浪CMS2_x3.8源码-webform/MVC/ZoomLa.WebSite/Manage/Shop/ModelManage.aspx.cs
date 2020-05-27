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
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class ModelManage :CustomerPageAction
    {
        private B_Model bll = new B_Model();
        protected int Stocktype;
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "ShopModelManage")) { function.WriteErrMsg("没有权限进行此项操作"); }
                MyBind();
            }
        }
        public void MyBind()
        {
            Model_RPT.DataSource= bll.GetListShop();
            Model_RPT.DataBind();
        }
        public string GetIcon(string IconPath)
        {
            return "../../Images/ModelIcon/" + (string.IsNullOrEmpty(IconPath) ? "Default.gif" : IconPath);
        }
        protected void Model_RPT_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string Id = e.CommandArgument.ToString();
                Response.Redirect("Model.aspx?ModelID=" + Id);
            }
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                this.bll.DelModel(int.Parse(Id));
                MyBind();
            }
            if (e.CommandName == "Field")
            {
                string Id = e.CommandArgument.ToString();
                Response.Redirect("ModelField.aspx?ModelID=" + Id);
            }
        }
    }
}
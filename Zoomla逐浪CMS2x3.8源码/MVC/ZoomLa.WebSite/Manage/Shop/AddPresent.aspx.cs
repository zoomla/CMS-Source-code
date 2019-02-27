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
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class AddPresent : CustomerPageAction
    {
        protected B_Model bmode = new B_Model();
        protected B_ShowField bshow = new B_ShowField();
        protected B_ModelField bfield = new B_ModelField();
        protected int NodeID;
        protected int ModelID;
        B_Admin badmin = new B_Admin();

        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();

            string menu = Request.QueryString["menu"];
            if (!IsPostBack)
            {
                if (menu == "edit")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    this.Label1.Text = "修改商品";
                    this.uptype.Value = "update";
                }
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string adminname = badmin.GetAdminLogin().AdminName;
            }
        }
    }
}
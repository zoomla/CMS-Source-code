using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Shop.Order;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class ExpSenderManage : System.Web.UI.Page
    {
        B_Order_ExpSender esBll = new B_Order_ExpSender();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            EGV.DataSource = esBll.Sel(Skey_T.Text.Trim());
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }

        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "del":
                    esBll.Del(id);
                    break;
                case "setdef":
                    esBll.SetDefault(id);
                    break;
            }
            MyBind();
        }
        protected string GetIsDefault()
        {
            int isdef = DataConverter.CLng(Eval("IsDefault"));
            return isdef == 1 ? "是" : "否";
        }

        protected void souchok_Click(object sender, EventArgs e)
        {
            MyBind();
        }

        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='AddExpSender.aspx?ID=" + dr["ID"] + "'");
            }
        }
    }
}
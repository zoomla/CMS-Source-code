using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;

namespace ZoomLaCMS.Manage.User
{
    public partial class CapitalLog : CustomerPageAction
    {
        B_History hisBll = new B_History();
        B_OrderList orderBll = new B_OrderList();
        public int type { get { return DataConverter.CLng(Request.QueryString["type"]) > 0 ? DataConverter.CLng(Request.QueryString["type"]) : 1; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind(string keyword = "")
        {
            DataTable dt = null;
            if (type == 7)
            { dt = orderBll.SelByHistory(DataConverter.CLng(Search_Drop.SelectedValue), keyword); }
            else
            { dt = hisBll.SelByType((M_UserExpHis.SType)type, DataConverter.CLng(Search_Drop.SelectedValue), keyword); }
            EGV.DataSource = dt;
            EGV.DataBind();
            function.Script(this, "CheckType(" + type + ");");
        }

        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }

        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            MyBind(Search_T.Text);
        }

        protected void Dels_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                if (type < 7)
                { hisBll.DelByIDS((M_UserExpHis.SType)type, Request.Form["idchk"]); }
            }
            MyBind();
        }


        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                //String id = EGV.DataKeys[e.Row.RowIndex].Value.ToString();
                e.Row.Attributes.Add("ondblclick", "window.location.href = 'Userinfo.aspx?id=" + (e.Row.DataItem as DataRowView)["UserID"] + "';");
            }
        }
    }
}
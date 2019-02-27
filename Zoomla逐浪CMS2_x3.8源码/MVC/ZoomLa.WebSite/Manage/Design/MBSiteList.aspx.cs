using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;


namespace ZoomLaCMS.Manage
{
    public partial class MBSiteList : CustomerPageAction
    {
        B_Design_MBSite msBll = new B_Design_MBSite();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>动力模块</a></li><li class='active'>微建站</li>");
            }
        }

        private void MyBind(string skey = "")
        {
            DataTable dt = msBll.Sel(skey);
            EGV.DataSource = dt;
            EGV.DataBind();
        }

        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }

        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='EditMBSite.aspx?ID=" + dr["ID"] + "'");
            }
        }

        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    msBll.Del(id);
                    break;
            }
            MyBind();
        }
        protected void Search_B_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Skey_T.Text)) { sel_box.Attributes.Add("style", "display:inline;"); template.Attributes.Add("style", "margin-top:44px;"); }
            else { sel_box.Attributes.Add("style", "display:none;"); }
            MyBind(Skey_T.Text);
        }
    }
}
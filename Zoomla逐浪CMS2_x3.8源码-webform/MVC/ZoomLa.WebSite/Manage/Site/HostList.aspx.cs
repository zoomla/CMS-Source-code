using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.CreateJS;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Site
{
    public partial class HostList : System.Web.UI.Page
    {
        B_CodeModel hostBll=new B_CodeModel("ZL_FZ_User");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>站群管理</a></li><li><a href='" + Request.RawUrl + "'>主机管理</a></li>");
            }
        }
        private void MyBind()
        {
            EGV.DataSource = hostBll.Sel();
            EGV.DataBind();
        }
                protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='HostAdd.aspx?name=" + HttpUtility.UrlEncode(DataConvert.CStr(dr["name"])) + "'");
            }
        }
    }
}
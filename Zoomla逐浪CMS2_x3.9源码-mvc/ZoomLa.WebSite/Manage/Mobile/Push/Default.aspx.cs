using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Mobile;

namespace ZoomLaCMS.Manage.Mobile.Push
{
    public partial class Default : System.Web.UI.Page
    {
        B_Mobile_PushMsg msgBll = new B_Mobile_PushMsg();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + Request.RawUrl + "'>消息推送</a></li><li class='active'>历史推送</li>");
            }
        }
        public void MyBind()
        {
            EGV.DataSource = msgBll.Sel();
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DataRowView dr = e.Row.DataItem as DataRowView;
            //    e.Row.Attributes.Add("ondblclick", "location='AddEnglishQuestion.aspx?ID=" + dr["ID"] + "'");
            //}
        }
        public string GetResult()
        {
            string result = Eval("Result", "");
            if (result.Equals("成功")) { return "<span style='color:green;'>" + result + "</span>"; }
            else { return "<span style='color:red;'>" + result + "</span>"; }
        }
    }
}
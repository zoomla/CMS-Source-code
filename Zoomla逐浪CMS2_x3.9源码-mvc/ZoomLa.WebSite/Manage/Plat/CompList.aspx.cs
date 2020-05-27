using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model.Plat;

namespace ZoomLaCMS.Manage.Plat
{
    public partial class CompList : System.Web.UI.Page
    {
        B_Plat_Comp compBll = new B_Plat_Comp();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                //Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + Request.RawUrl + "'>能力中心</a></li><li><a href='PlatInfoManage.aspx'>信息管理</a> [<a href='AddComp.aspx'>添加公司</a>]</li>");
            }
        }
        private void MyBind(string key = "")
        {
            DataTable dt = compBll.SelForList(key);
            EGV.DataSource = dt;
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
                    compBll.Del(DataConverter.CLng(e.CommandArgument));
                    break;
            }
            MyBind();
        }

        protected void Dels_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                compBll.DelByIDS(Request.Form["idchk"]);
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ondblclick", "javascript:location.href='AddComp.aspx?id=" + (e.Row.DataItem as DataRowView)["ID"] + "';");
            }
        }
        protected void Search_B_Click(object sender, EventArgs e)
        {
            string skey = Skey_T.Text;
            if (!string.IsNullOrEmpty(skey))
            {
                sel_box.Attributes.Add("style", "display:inline;");
                EGV.Attributes.Add("style", "margin-top:44px;");
            }
            else
            {
                sel_box.Attributes.Add("style", "display:none;");
                EGV.Attributes.Add("style", "margin-top:0px;");
            }
            MyBind(skey);
        }
    }
}
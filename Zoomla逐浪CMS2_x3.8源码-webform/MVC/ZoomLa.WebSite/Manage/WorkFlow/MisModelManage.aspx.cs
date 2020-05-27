using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.WorkFlow
{
    public partial class MisModelManage : System.Web.UI.Page
    {
        protected B_Mis_Model bmis = new B_Mis_Model();
        protected M_Mis_Model mmis = new M_Mis_Model();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li class='active'>模板管理<a href='AddMisModel.aspx'>[添加套红模板]</a></li>");
            }
        }
        private void MyBind(string key = "")
        {
            DataTable dt = bmis.Sel(-100, searchText.Text);
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
            switch (e.CommandName.ToLower())
            {
                case "del":
                    bmis.DelByModelID(DataConvert.CLng(e.CommandArgument.ToString()));
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes["class"] = "tdbg";
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='AddMisModel.aspx?&ID={0}'", this.EGV.DataKeys[e.Row.RowIndex].Value.ToString());
                e.Row.Attributes["onmouseover"] = "this.className='tdbgmouseover'";
                e.Row.Attributes["onmouseout"] = "this.className='tdbg'";
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.Attributes["title"] = "双击修改";
            }
        }
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}
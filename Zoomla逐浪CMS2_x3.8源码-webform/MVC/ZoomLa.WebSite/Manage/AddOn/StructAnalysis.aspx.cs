using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.AddOn
{
    public partial class StructAnalysis : System.Web.UI.Page
    {
        B_Structure bll = new B_Structure();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='StructMenber.aspx'>组织结构</a></li><li class='active'>组织结构</li>");
                MyBind();
            }

        }
        public void MyBind()
        {
            EGV.DataSource = bll.Sel();
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
                case "del2":
                    //删除记录，同时删除目标数据库
                    break;
            }
        }
        protected int GetNums(object id)
        {
            int sid = Convert.ToInt32(id);
            return bll.GetCount(sid);
        }
    }
}
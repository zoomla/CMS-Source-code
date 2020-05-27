using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL.User;

namespace ZoomLaCMS.Manage.Counter
{
    public partial class Ip : CustomerPageAction
    {
        //public B_Counter counterabout = new B_Counter();
        public int allcount;
        B_Com_VisitCount visitBll = new B_Com_VisitCount();

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MyBind();
            }
        }

        public void MyBind(string username = "", string source = "")
        {
            SumCount_L.Text = visitBll.CountByIP().Rows.Count.ToString();
            DataTable dt = visitBll.SelectAll(0, 0, username, source);
            EGV.DataSource = dt;
            EGV.DataBind();
        }

        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            if (FilterType_Drop.SelectedValue.Equals("UserName"))
            {
                MyBind(IDName.Text);
            }
            else
            {
                MyBind("", IDName.Text);
            }
        }

        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            if (FilterType_Drop.SelectedValue.Equals("UserName"))
            {
                MyBind(IDName.Text);
            }
            else
            {
                MyBind("", IDName.Text);
            }
        }
    }
}
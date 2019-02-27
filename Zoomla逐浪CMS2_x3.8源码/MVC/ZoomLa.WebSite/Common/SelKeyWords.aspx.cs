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

namespace ZoomLaCMS.Common
{
    public partial class SelKeyWords : System.Web.UI.Page
    {
        B_KeyWord keyBll = new B_KeyWord();
        B_User buser = new B_User();
        B_Admin badmin = new B_Admin();

        public int KeyType { get { return DataConverter.CLng(Request.QueryString["type"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!buser.CheckLogin() && !badmin.CheckLogin())
            {
                function.WriteErrMsg("您还没有登录!请点击[<a href='/User/Login'>登录</a>]", "/User/Login");
            }
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind(string search = "")
        {
            DataTable dt = keyBll.SelAll(search);
            EGV.DataSource = dt;
            EGV.DataBind();
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
    }
}
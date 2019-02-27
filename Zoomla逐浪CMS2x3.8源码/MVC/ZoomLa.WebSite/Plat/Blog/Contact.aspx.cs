using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Plat.Blog
{
    public partial class Contact : System.Web.UI.Page
    {
        B_User bllUser = new B_User();
        B_User_Plat upBll = new B_User_Plat();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MyBind();
            }
        }
        private void MyBind()
        {
            DataTable dt = upBll.SelByCompany(B_User_Plat.GetLogin().CompID);
            if (!string.IsNullOrEmpty(SearchKey))
            {
                string key = "'%" + SearchKey + "%'";
                dt.DefaultView.RowFilter = "UserName like " + key + "Or GroupName like " + key;
                dt = dt.DefaultView.ToTable();
            }
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        private string SearchKey
        {
            get
            {
                return ViewState["SearchKey"] as string;
            }
            set
            {
                ViewState["SearchKey"] = value;
            }
        }
        public string GetGN(object o)
        {
            return string.IsNullOrEmpty(o.ToString()) ? "未设置" : o.ToString();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            this.MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void Skey_Btn_Click(object sender, EventArgs e)
        {
            SearchKey = skey_t.Value;
            MyBind();
        }
        protected void BtnSetTopPosation_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idChk"];
            Response.Redirect("/Plat/Mail/MessageSend.aspx?uid=" + ids);
        }
    }
}
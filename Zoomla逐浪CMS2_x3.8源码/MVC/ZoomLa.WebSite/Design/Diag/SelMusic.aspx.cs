namespace ZoomLaCMS.Design.Diag
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL.Design;
    public partial class SelMusic : System.Web.UI.Page
    {

        B_Design_RES resBll = new B_Design_RES();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind(string skey = "")
        {
            EGV.DataSource = resBll.Search(skey, "", "music");
            EGV.DataBind();
        }


        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }

        protected void Search_B_Click(object sender, EventArgs e)
        {
            MyBind(Skey_T.Text);
        }
    }
}
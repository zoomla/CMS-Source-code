using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Model;

namespace ZoomLaCMS.Design.User.mbsite
{
    public partial class Default : System.Web.UI.Page
    {
        B_Design_MBSite mbBll = new B_Design_MBSite();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            RPT.DataSource = mbBll.Sel(mu.UserID);
            RPT.DataBind();
        }
        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del":
                    int id = Convert.ToInt32(e.CommandArgument);
                    mbBll.DelSite(id);//后期增加删除数据功能
                    break;
            }
            MyBind();
        }
        public string GetImg()
        {
            string siteimg = Eval("SiteImg", "");
            return string.IsNullOrEmpty(siteimg) ? "/design/mobile/tlp/" + Eval("TlpID") + "/view.jpg" : siteimg;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;


namespace ZoomLaCMS.Guest.Ask
{
    public partial class Ask : System.Web.UI.MasterPage
    {
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.user.Text = buser.GetLogin().UserName;
                M_UserInfo mu = buser.GetLogin(false);
                if (!string.IsNullOrEmpty(GuestConfig.GuestOption.WDOption.selGroup))
                {//用户组查看权限
                    string groups = "," + GuestConfig.GuestOption.WDOption.selGroup + ",";
                    if (!groups.Contains("," + mu.GroupID.ToString() + ","))
                        function.WriteErrMsg("您没有查看问答中心的权限!", "/");
                }
            }
        }
        public string getstyles()
        {
            if (buser.CheckLogin())
            {
                return "display:none";
            }
            else return "display:inline-table";
        }
        protected string getstyle()
        {
            if (buser.CheckLogin())
            {
                return "display:inline-table";
            }
            else return "display:none";
        }
    }
}
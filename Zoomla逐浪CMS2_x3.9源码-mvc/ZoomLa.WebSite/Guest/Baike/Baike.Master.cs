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
using ZoomLa.Components;

namespace ZoomLaCMS.Guest.Baike
{
    public partial class Baike : System.Web.UI.MasterPage
    {
        B_User buser = new B_User();
        B_Baike b_Baike = new B_Baike();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_UserInfo m_UserInfo = buser.GetLogin();
                user.Text = m_UserInfo.UserName;
                if (!string.IsNullOrEmpty(GuestConfig.GuestOption.BKOption.selGroup))
                {//用户组查看权限
                    string groups = "," + GuestConfig.GuestOption.BKOption.selGroup + ",";
                    if (!groups.Contains("," + m_UserInfo.GroupID.ToString() + ","))
                        function.WriteErrMsg("您没有查看百科的权限!");
                }
            }
        }
        protected string getstyle()
        {
            if (buser.CheckLogin())
            {
                return "display:inline-table";
            }
            else return "display:none";
        }
        protected string getstyles()
        {
            if (buser.CheckLogin())
            {
                return "display:none";
            }
            else return "display:inline-table";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Guest/Baike/Search.aspx?tittle=" + txtAsk.Text.Trim());
        }
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            string tittle = txtAsk.Text.Trim();
            if (tittle != "")
            {
                DataTable dt = b_Baike.SelBy(tittle, 1);
                if (dt.Rows.Count > 0)
                {
                    Response.Redirect("/Guest/Baike/Details.aspx?action=new&soure=user&tittle=" + tittle);
                }
                else
                {

                    Response.Redirect("/Guest/Baike/Search.aspx?tittle=" + tittle);
                }
            }
        }
    }
}
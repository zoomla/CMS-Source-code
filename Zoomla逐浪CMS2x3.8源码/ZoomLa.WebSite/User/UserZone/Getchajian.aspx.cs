using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_UserZone_Getchajian : System.Web.UI.Page
{
    private B_User buser = new B_User();
    B_MTit BM = new B_MTit();
    B_MiUserInfo mm = new B_MiUserInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin();
        M_UserInfo uinfo = buser.GetLogin();
        M_MiUserInfo mi = mm.GetSelectUserID(uinfo.UserID);
        int type = 0;
        if (mi.type == 0)
            type = 0;
        else
            type = 1;
        DataTable dd = BM.Select_All(type);
        string ss = "";
        foreach (DataRow dr in dd.Rows)
        {
            ss += "<li><a href=" + dr["Iurl"].ToString() + ">" + dr["Iinfo"].ToString() + "</a></li>";
        }
        base.Response.Write("document.write('" + ss + "');");
    }
}

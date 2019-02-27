using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
public partial class User_UserZone_Kiss_ViewSkins : System.Web.UI.Page
{
    private B_User ull = new B_User();
    private B_Sns_Kiss kll = new B_Sns_Kiss();
    protected void Page_Load(object sender, EventArgs e)
    {
        ull.CheckIsLogin();
        M_UserInfo info = ull.GetLogin();

        int id = DataConverter.CLng(Request.QueryString["id"]);

        M_Sns_Kiss kissinfo = kll.GetSelect(id);

        kissinfo.IsRead = 1;
        kissinfo.ReadTime = DateTime.Now;
        kll.GetUpdate(kissinfo);


        DataTable kinfo = kll.SelectTable(id);

        this.Repeater1.DataSource = kinfo;
        this.Repeater1.DataBind();
    }
    protected string getuserpic(string SendID)
    {
        int userid = DataConverter.CLng(SendID);
        return ull.GetUserBaseByuserid(userid).UserFace;

    }

    protected string getusername(string SendID)
    {

        int userid = DataConverter.CLng(SendID);
        return ull.GetUserByUserID(userid).UserName;
    }
}

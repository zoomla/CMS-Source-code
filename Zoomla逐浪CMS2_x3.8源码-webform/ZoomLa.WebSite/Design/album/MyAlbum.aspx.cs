using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Model;

public partial class Design_album_MyAlbum : System.Web.UI.Page
{
    B_Design_Album aumBll = new B_Design_Album();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        WxAPI.AutoSync(Request.RawUrl);
        B_User.CheckIsLogged(Request.RawUrl);
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        RPT.DataSource = aumBll.Sel(mu.UserID,"");
        RPT.DataBind();
    }

    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del2":
                aumBll.Del(id);
                break;
        }
        MyBind();
    }
}
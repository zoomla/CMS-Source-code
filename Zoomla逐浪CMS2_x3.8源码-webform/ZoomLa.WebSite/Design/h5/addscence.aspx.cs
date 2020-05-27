using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;

public partial class Design_scence_addscence : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Design_Scence pageBll = new B_Design_Scence();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged(Request.RawUrl);
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_Design_Page pageMod = new M_Design_Page();
        pageMod.guid = System.Guid.NewGuid().ToString();
        pageMod.Title = Title_T.Text;
        pageMod.CUser = mu.UserID;
        pageMod.UserID = mu.UserID;
        pageMod.UserName = mu.UserName;
        pageMod.ZType = 0;
        pageBll.Insert(pageMod);
        function.Script(this, "top.location='/design/h5/default.aspx?id=" + pageMod.guid + "';");
    }
}
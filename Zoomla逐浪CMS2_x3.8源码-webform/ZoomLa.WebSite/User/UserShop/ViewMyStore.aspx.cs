using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class User_UserShop_ViewMyStore : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Content conBll = new B_Content();
    protected void Page_Load(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_CommonData storeMod = conBll.SelMyStore_Ex();
        Response.Redirect("/Store/StoreIndex.aspx?id=" + storeMod.GeneralID);

    }
}
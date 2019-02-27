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

public partial class Design_Diag_se_conf : System.Web.UI.Page
{
    B_Design_Scence seBll=new B_Design_Scence();
    B_User buser=new B_User();
    public string Mid { get { return Request["id"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_Design_Page seMod = seBll.SelModelByGuid(Mid);
        if (seMod.UserID != mu.UserID) { return; }
        if (function.isAjax())
        {
            string action = Request.Form["action"] ?? "";
            string result = "";
            switch (action)
            {
                case "update":
                    seMod.AccessPwd = Request["pwd"];
                    seBll.UpdateByID(seMod);
                    break;
                default:
                    throw new Exception(action + "不存在");
            }
            Response.Clear(); Response.Write(result); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            pwd_t.Attributes["value"] = seMod.AccessPwd;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Promo : System.Web.UI.Page
{
    B_User buser = new B_User();
    private string PStr { get { return Request.QueryString["p"]; } }
    private string PUName { get { return Request.QueryString["pu"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        int PUserID = DataConvert.CLng(EncryptHelper.DesDecrypt(PStr));
        M_UserInfo mu = new M_UserInfo();
        if (PUserID < 1 && string.IsNullOrEmpty(PUName)) { function.WriteErrMsg("无用户信息"); }
        if (PUserID > 0)
        {
            mu = buser.SelReturnModel(PUserID);
        }
        else if (!string.IsNullOrEmpty(PUName))
        {
            mu = buser.GetUserByName(PUName);
        }
        //if (mu.UserID < 1) { function.WriteErrMsg("推广用户不存在"); }
        //if (mu.UserID > 1) { Response.Cookies["UserState2"]["ParentUserID"] = mu.UserID.ToString(); }
        Response.Redirect("/User/Register.aspx?ParentUserID=" + mu.UserID);
    }
}
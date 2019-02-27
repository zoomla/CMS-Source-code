using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class User_Guest_BaikeFavorite : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Favorite favBll = new B_Favorite();
    B_Baike bkBll = new B_Baike();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    protected void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        DataTable dt = favBll.SelByType(mu.UserID, 5);
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    public string GetTitle()
    {
        int infoID = DataConvert.CLng(Eval("infoID"));
        return bkBll.SelReturnModel(infoID).Tittle;
    }
}
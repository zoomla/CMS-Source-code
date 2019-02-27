using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class User_Guest_AskComment : System.Web.UI.Page
{
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        //ZL_Ask AskID Qcontent|ZL_GuestAnswer AswID Content
        string mtable = "(SELECT A.*,B.QContent FROM ZL_AskCommon A LEFT JOIN ZL_Ask B ON A.AskID=B.ID WHERE A.Type=1 AND A.UserID=" + mu.UserID + ")";
        EGV.DataSource = SqlHelper.JoinQuery("A.*,B.Content AS AnswerContent", mtable, "ZL_GuestAnswer", "A.AswID=B.ID", "", "A.AddTime DESC");
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}
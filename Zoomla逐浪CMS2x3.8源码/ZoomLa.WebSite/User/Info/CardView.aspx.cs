using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data;

public partial class User_Info_CardView : System.Web.UI.Page
{

    B_User buser = new B_User();
    B_Group bgp = new B_Group();
    B_Card bc = new B_Card();
    B_CardType bcType = new B_CardType();
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        M_UserInfo uinfo = buser.GetLogin();
        WebUserControlTop1.GroupID = uinfo.GroupID;
        DataTable dt = bc.SelCarByUserID(uinfo.UserID);
        M_Card card = bc.GetSelect(Mid);
        Label1.Text = card.CardNum.ToString();
        if (card.AssociateUserID != 0)
            Label2.Text = buser.SeachByID(card.AssociateUserID).UserName.ToString();
        else
            Label2.Text = "没有激活";
        Label3.Text = card.CardPwd.ToString();
        Label4.Text = buser.SeachByID(card.PutUserID).UserName.ToString();
        if (card.CardState == 1)
            Label5.Text = "开启";
        else
            Label5.Text = "关闭";
        Label6.Text = card.CircumscribeTime.ToShortDateString();
    }
}

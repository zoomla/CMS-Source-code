using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using ZoomLa.Components;

public partial class User_Info_UserRecei : System.Web.UI.Page
{
    B_UserRecei buserRecei = new B_UserRecei();
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
        RPT.DataSource = buserRecei.SelByUID(mu.UserID);
        RPT.DataBind();
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "def":
                buserRecei.SetDef(DataConverter.CLng(e.CommandArgument));
                break;
            case "del":
                buserRecei.DeleteByGroupID(DataConverter.CLng(e.CommandArgument));
                break;
            default:
                break;
        }
        MyBind();
    }
    protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = e.Item.DataItem as DataRowView;
            if (drv["IsDefault"].ToString().Equals("1")) { e.Item.FindControl("Def_Btn").Visible = false; }
        }
    }
}
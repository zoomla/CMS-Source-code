using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class Plat_Addon_NotifyList : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Common_Notify comBll = new B_Common_Notify();
    public int MsgID { get { return string.IsNullOrEmpty(Request.QueryString["msgid"]) ? -100 : DataConverter.CLng(Request.QueryString["msgid"]); } }
    public string Skey { get { return Request.QueryString["skey"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind(Skey);
        }
    }
    public void MyBind(string skey = "")
    {
        RPT.DataSource = comBll.Sel("2", -100, MsgID, "", skey);
        RPT.DataBind();
    }

    public string GetUser()
    {
        return buser.GetUserNameByIDS(Eval("ReceOrgin", ""));
    }

    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del":
                comBll.DelByIDS(e.CommandArgument.ToString(), buser.GetLogin().UserID);
                break;
        }
        MyBind();
    }

    protected void Search_B_Click(object sender, EventArgs e)
    {
        string skey = Skey_T.Text.Trim(' ');
        MyBind(skey);
    }
}
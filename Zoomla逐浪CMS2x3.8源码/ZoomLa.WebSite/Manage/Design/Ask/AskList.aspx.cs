using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;

public partial class Manage_Design_Ask_AskList : CustomerPageAction
{
    B_Design_Ask askBll = new B_Design_Ask();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind(string skey = "")
    {
        EGV.DataSource = askBll.Sel(skey);
        EGV.DataBind();
    }
    public string GetUserName()
    {
        int userid = DataConverter.CLng(Eval("CUser", ""));
        return buser.SelReturnModel(userid).UserName;
    }
    public string GetZType()
    {
        return Eval("ZType", "");
    }
    public string GetStatus()
    {
        return Eval("ZStatus", "");
    }

    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = DataConverter.CLng(e.CommandArgument);
        switch (e.CommandName.ToLower())
        {
            case "del":
                {
                    askBll.Del(id);
                }
                break;
        }
        MyBind();
    }

    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='EditAsk.aspx?ID=" + dr["ID"] + "'");
        }
    }

    protected void Search_B_Click(object sender, EventArgs e)
    {
        string Skey = Skey_T.Text.Trim();
        if (!string.IsNullOrEmpty(Skey)) { sel_box.Attributes.Add("style", "display:inline;"); template.Attributes.Add("style", "margin-top:44px;"); }
        else { sel_box.Attributes.Add("style", "display:none;"); template.Attributes.Add("style", "margin-top:5px;"); }
        MyBind(Skey);
    }
}
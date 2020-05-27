using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Text;


public partial class User_Pub : System.Web.UI.Page
{
    B_Content conBll = new B_Content();
    B_User buser = new B_User();
    B_Pub pubBll = new B_Pub();
    B_Node nodeBll = new B_Node();
    B_Model modBll = new B_Model();
    public int NodeID
    {
        get { return DataConverter.CLng(ViewState["nodeid"]); }
        set { ViewState["nodeid"] = value; }
    }
    public string Mode { get { return DataConverter.CStr(Request.QueryString["mode"]); } }
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
        string SearchTitle = this.TxtSearchTitle.Text.Trim();
        DataTable dt = new DataTable();
        dt = conBll.ContentListUser(NodeID, "", mu.UserName, SearchTitle, 0, "");
        EGV.DataSource = dt;
        EGV.DataKeyNames = new string[] { "GeneralID" };
        EGV.DataBind();
    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}
 
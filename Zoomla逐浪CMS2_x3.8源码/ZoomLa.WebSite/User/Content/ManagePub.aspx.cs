using System;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
public partial class User_ManagePub : System.Web.UI.Page
{
    /*
     * 管理我的互动信息
     */ 
    B_User buser = new B_User();
    B_Pub pubBll = new B_Pub();
    public int PubID { get { return Convert.ToInt32(Request.QueryString["PubID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        DataTable nodeDT = pubBll.Sel();
        M_Pub pubMod = pubBll.SelReturnModel(PubID);
        if (nodeDT.Rows.Count < 1) { function.WriteErrMsg("互动无节点信息"); }
        if (PubID < 1) { Response.Redirect("ManagePub.aspx?PubID=" + nodeDT.Rows[0]["PubID"]); }
        Node_RPT.DataSource = nodeDT;
        Node_RPT.DataBind();
        RPT.DataSource = pubBll.SelInfoPage(1,int.MaxValue,pubMod.PubTableName, 0,mu.UserID);
        RPT.DataBind();
    }
    protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Del":
                int id = Convert.ToInt32(e.CommandArgument.ToString().Split(':')[0]);
                int pid = Convert.ToInt32(e.CommandArgument.ToString().Split(':')[1]);
                M_Pub pinfos = pubBll.GetSelect(pid);
                pubBll.DeleteModel(pinfos.PubTableName, "ID='" + id + "'");
                break;
        }
        MyBind();
    }
}
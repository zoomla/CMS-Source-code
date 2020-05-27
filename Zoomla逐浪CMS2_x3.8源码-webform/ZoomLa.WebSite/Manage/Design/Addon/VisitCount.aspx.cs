using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL.SQL;

public partial class Manage_Design_Addon_VisitCount : CustomerPageAction
{
    B_Com_VisitCount visitBll = new B_Com_VisitCount();
    B_User buser = new B_User();
    public string ZType { get { return Request.QueryString["type"] ?? "h5,mbh5"; } }
    public string Skey { get { return Request.QueryString["skey"] ?? ""; } }
    public string InfoID { get { return Request.QueryString["infoid"] ?? ""; } }
    public string view { get { return Request.QueryString["view"] ?? "detail"; } }
    public int UserID { get { return DataConverter.CLng(Request.QueryString["userid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Detail_RPT.SPage = Detail_SelPage;
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public DataTable Detail_SelPage(int pageSize, int pageIndex)
    {
        PageSetting config = new PageSetting();
        config = visitBll.SelPage(pageIndex, pageSize, ZType, UserID, Skey, InfoID);
        Detail_RPT.ItemCount = config.itemCount;
        if (config.itemCount < 1) { Empty_Detail.Visible = true; }
        return config.dt;
    }
    public void MyBind()
    {
        switch (view)
        {
            case "detail":
                {
                    Detail_RPT.DataBind();
                }
                break;
            case "overview":
                {
                    Detail_Div.Visible = false;
                    OView_Div.Visible = true;
                    DataTable dt = visitBll.SelForSum("h5,mbh5", Skey);
                    if (dt.Rows.Count < 1) { Empty_OView.Visible = true; }
                    OView_RPT.DataSource = dt;
                    OView_RPT.DataBind();
                }
                break;
        }
    }

    public string GetUser()
    {
        int uid = DataConverter.CLng(Eval("UserID", ""));
        if (uid <= 0) { return "游客"; }
        else
        {
            M_UserInfo mu = buser.SelReturnModel(uid);
            return "<a href='VisitCount.aspx?userid=" + mu.UserID + "' >" + mu.UserName + "</a>";
        }
    }
    public string GetIpLocation()
    {
        return IPScaner.IPLocation(Eval("IP", ""), "@province|@city", true);
    }
    public string GetSEType()
    {
        string ztype = Eval("ztype", "");
        switch (ztype)
        {
            case "mbh5":
                return "<a href='VisitCount.aspx?type=mbh5'>微场景</a>";
            case "h5":
                return "<a href='VisitCount.aspx?type=h5'>PC场景</a>";
            default:
                return "";
        }
    }
    protected void souchok_Click(object sender, EventArgs e)
    {
        Response.Redirect("VisitCount.aspx?type=" + ZType + "&skey=" + souchkey.Text + "&view=" + view);
    }


    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='VisitCount.aspx?view=detail&skey=" + dr["InfoTitle"] + "'");
        }
    }

    protected void Detail_RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del":
                visitBll.Del(DataConverter.CLng(e.CommandArgument));
                break;
        }
        Response.Redirect("VisitCount.aspx?type=" + ZType + "&skey=" + Skey + "&view=" + view);
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.BLL.User;
using ZoomLa.SQLDAL;

public partial class Manage_Design_SceneList : CustomerPageAction
{
    B_Design_Scence scenceBll = new B_Design_Scence();
    B_Com_VisitCount visitBll = new B_Com_VisitCount();
    public string Skey
    {
        get
        {
            if (ViewState["skey"] == null || string.IsNullOrEmpty(ViewState["skey"].ToString()))
            {
                ViewState["skey"] = Request.QueryString["skey"] ?? "";
            }
            return ViewState["skey"].ToString();
        }
        set
        {
            ViewState["skey"] = value;
        }
    }
    public string keyword { get { return Request.QueryString["KeyWord"] ?? ""; } }
    public int Status { get { return string.IsNullOrEmpty(Request.QueryString["status"]) ? -100 : DataConvert.CLng(Request.QueryString["status"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Status == (int)ZoomLa.Model.ZLEnum.ConStatus.Recycle)
            {
                reclink.Visible = false;
                recli.Visible = true;
            }
            Skey = keyword;
            MyBind();
        }
    }
    public void MyBind()
    {
        DataTable dt = scenceBll.SelModelByStatus(Skey, Status);
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    public string GetStatus()
    {
        int status = DataConvert.CLng(Eval("Status"));
        switch (status)
        {
            case 0: return "<a href='SceneList.aspx?Status=0&skey=" + Skey + "'><span style='color:blue;'>正常</span></a>";
            case 1: return "<a href='SceneList.aspx?Status=1&skey=" + Skey + "'><span style='color:green;'>荐</span></a>";
            case 2: return "<a href='SceneList.aspx?Status=2&skey=" + Skey + "'><span style='color:red;'>停</span></a>";
            default: return "";
        }
    }

    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='SceneScore.aspx?ID=" + dr["ID"] + "'");
        }
    }

    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del":
                var pageMod = scenceBll.SelReturnModel(id);
                pageMod.Status = (int)ZoomLa.Model.ZLEnum.ConStatus.Recycle;
                scenceBll.UpdateByID(pageMod);
                break;
            case "del2":
                scenceBll.Del(id);
                break;
            case "rec":
                var pageMod2 = scenceBll.SelReturnModel(id);
                pageMod2.Status = 0;
                scenceBll.UpdateByID(pageMod2);
                break;
        }
        MyBind();
    }

    protected void Search_B_Click(object sender, EventArgs e)
    {
        Skey = Skey_T.Text;
        if (!string.IsNullOrEmpty(Skey_T.Text)) { sel_box.Attributes.Add("style", "display:inline;"); template.Attributes.Add("style", "margin-top:44px;"); }
        else { sel_box.Attributes.Add("style", "display:none;"); }
        MyBind();
    }
    public int GetVisitCount()
    {
        return visitBll.GetVisitCount(Eval("ID",""));
    }
}
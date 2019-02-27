using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Message;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_Guest_BKList : System.Web.UI.Page
{
    B_BaikeEdit editBll = new B_BaikeEdit();
    B_Baike bkBll = new B_Baike();
    public string Flow { get { return Request.QueryString["Flow"] ?? ""; } }
    public string VerStr { get { return DataConvert.CStr(ViewState["VerStr"]); } set { ViewState["VerStr"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.HideBread(Master);
            MyBind();
        }
    }
    private void MyBind()
    {
        M_Baike bkMod = bkBll.SelModelByFlow(Flow);
        VerStr = bkMod.VerStr;
        EGV.DataSource = editBll.SelBy(-100, Flow, "");
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del2":
                editBll.Del(id);
                break;
            case "apply":
                editBll.Apply(id);
                function.WriteSuccessMsg("应用成功");
                break;
        }
        MyBind();
    }
    public string GetStatus()
    {
        int status = DataConvert.CLng(Eval("Status"));
        return editBll.GetStatus(status);
    }
}
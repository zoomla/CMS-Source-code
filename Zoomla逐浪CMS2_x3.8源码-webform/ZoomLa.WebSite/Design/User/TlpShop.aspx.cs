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
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Design_User_TlpShop : System.Web.UI.Page
{
    B_Design_Tlp tlpBll = new B_Design_Tlp();
    B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
    B_User buser = new B_User();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["mode"]))
        {
            this.MasterPageFile = "~/Common/Master/Empty.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { MyBind(); }
    }
    private void MyBind()
    {
        DataTable dt = tlpBll.SelWith(0,0,17);
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    public string GetPrice()
    {
        double price = DataConvert.CDouble(Eval("Price"));
        if (price == 0) { return "免费"; }
        else { return price.ToString("f2"); }
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int tlpID = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del2":
                tlpBll.Del(tlpID);
                break;
            case "apply":
                M_UserInfo mu = buser.GetLogin();
                string guid = tlpBll.CopyTlp(tlpID, sfBll.SelReturnModel(mu.SiteID));
                function.Script(this, "top.location='/design/?ID=" + guid + "';");
                break;
        }
        MyBind();
    }
}
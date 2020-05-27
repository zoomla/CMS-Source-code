using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL;
using ZoomLa.Model.Plat;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class test_MailConfig : System.Web.UI.Page
{
    B_Plat_MailConfig configBll = new B_Plat_MailConfig();
    M_Plat_MailConfig configMod = new M_Plat_MailConfig();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        EGV.DataSource = configBll.SelByUid(buser.GetLogin().UserID);
        EGV.DataBind();
    }
    protected void Dels_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            configBll.DelByIDS(Request.Form["idchk"],buser.GetLogin().UserID);
            MyBind();
        }
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName.ToLower())
        {
            case "receive":
                M_Plat_MailConfig configMod = new M_Plat_MailConfig();
                break;
            case "del2":
                configBll.Del(id);
                break;
        }
        MyBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}
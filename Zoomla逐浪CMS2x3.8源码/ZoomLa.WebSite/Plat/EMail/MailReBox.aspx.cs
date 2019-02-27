using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model.Plat;
using ZoomLa.Model;

public partial class Plat_Mail_MailReBox : System.Web.UI.Page
{
    B_Plat_Mail mailBll = new B_Plat_Mail();
    B_Plat_MailConfig configBll = new B_Plat_MailConfig();
    //M_Plat_Mail mailMod = null;
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
        DataTable configDT = configBll.SelByUid(mu.UserID);
        MailList_DP.DataSource = configDT;
        MailList_DP.DataBind();
        MailList_DP.Items.Insert(0, new ListItem("全部", ""));
        DataTable dt = mailBll.Sel(50000, mu.UserID, -100, (int)ZLEnum.ConStatus.Recycle, MailList_DP.SelectedValue,"");
        EGV.DataSource = dt;
        EGV.DataBind();
        count_sp.InnerText = dt.Rows.Count.ToString();
    }

    public string GetUserName(string str)
    {
        return str;
    }
    protected void Dels_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            mailBll.DelByIds(Request.Form["idchk"]);
            MyBind();
        }
    }
    protected void ReBox_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            mailBll.ReBox(Request.Form["idchk"]);
            MyBind();
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        EGV.DataBind();
    }
    protected void MailList_DP_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyBind();
    }
}
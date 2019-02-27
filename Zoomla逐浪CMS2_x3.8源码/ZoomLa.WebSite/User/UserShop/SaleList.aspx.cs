using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;

public partial class User_UserShop_SaleList : Page
{
    protected B_Payment payll = new B_Payment();
    protected B_User ull = new B_User();
    protected B_PayPlat prell = new B_PayPlat();
    protected B_ModelField mll = new B_ModelField();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        DataTable Cll = payll.GetPayList();
        Cll.DefaultView.RowFilter = "UserID=" + ull.GetLogin().UserID;
        Cll = Cll.DefaultView.ToTable();
        EGV.DataSource = Cll;
        EGV.DataBind();
        object allmoney, MoneyTrue;
        allmoney = Cll.Compute("Sum(MoneyPay)", "");
        MoneyTrue = Cll.Compute("Sum(MoneyTrue)", "");
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected string getusername(string userid)
    {
        M_UserInfo uinfo = ull.GetUserByUserID(DataConverter.CLng(userid));
        return uinfo.UserName;
    }

    protected string GetStatus(string status)
    {
        if (status == "3")
            return "<font color=green>√</font>";
        else
            return "<font color=red>×</font>";
    }
    protected string GetCStatus(string cstatus)
    {
        if (DataConverter.CBool(cstatus))
            return "<font color=green>√</font>";
        else
            return "<font color=red>×</font>";
    }
    protected string getPayPlat(string id)
    {
        string result = "";
        M_PayPlat platMod = prell.GetPayPlatByid(DataConverter.CLng(id));
        if (platMod != null)
        {
            result = platMod.PayPlatName;
        }
        return result;
    }
}

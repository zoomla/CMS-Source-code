using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class User_CashCoupon_GetArrive : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Arrive avBll = new B_Arrive();
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
        DataTable dt = avBll.U_SelForGet(mu.UserID);
        if (dt.Rows.Count > 0) { data_div.Visible = true; }
        else { empty_div.Visible = true; }
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    public string GetMoneyRegion()
    {
        double min = DataConvert.CDouble(Eval("MinAmount"));
        double max = DataConvert.CDouble(Eval("MaxAmount"));
        return avBll.GetMoneyRegion(min, max);
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        string flow = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "get":
                int avid = avBll.U_GetArrive(mu.UserID, flow);
                if (avid > 0) { function.WriteSuccessMsg("领取成功"); }
                else { function.WriteErrMsg("领取失败"); }
                break;
        }
        MyBind();
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Data;
using ZoomLa.SQLDAL;

public partial class User_CashCoupon_ArriveManage : Page
{
    B_User buser = new B_User();
    B_Arrive avBll = new B_Arrive();
    public int State { get { return DataConvert.CLng(Request.QueryString["state"],1); } }
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
        DataTable dt = avBll.U_Sel(mu.UserID, -100, State);
        if (dt.Rows.Count < 1) { empty_div.Visible = true; }
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

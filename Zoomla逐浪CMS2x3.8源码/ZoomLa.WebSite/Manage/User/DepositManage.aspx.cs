using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Model.User;
using ZoomLa.Common;
using System.Data;
using ZoomLa.Model;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

public partial class Manage_User_DepositManage : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Cash cashBll = new B_Cash();
    public int State { get { return DataConverter.CLng(Request.QueryString["state"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='../Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li><a href='" + Request.RawUrl + "'>用户出金</a></li>");
            MyBind();
        }
    }
    private void MyBind(string username = "")
    {
        Deposit_RPT.DataSource = cashBll.SelByState(State, username);
        Deposit_RPT.DataBind();
        function.Script(this, "checkState(" + State + ")");
    }
    public string GetStatus()
    {
        string status = Eval("YState").ToString();
        switch (status)
        {
            case "1":
                return "<span>等待确认</span>";
            case "99":
                return "<span style='color:green'>成功出金</span>";
            case "-1":
                return "<span style='color:red'>拒绝出金</span>";
            default:
                return "等待确认";
        }
    }
    //批量确认
    protected void CheckDepos_B_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            cashBll.UpdateState(Request.Form["idchk"],ZLEnum.WDState.Dealed);
            MyBind();
        }
    }
    protected void Deposit_RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Check":
                cashBll.UpdateState(e.CommandArgument.ToString(),ZLEnum.WDState.Dealed);
                break;
        }
        MyBind();
    }
    public string GetOP()
    {
        string opstr = "";
        if (Convert.ToInt32(Eval("YState"))==(int)ZLEnum.WDState.WaitDeal)
        {
            opstr+= "<a href='javascript:;' onclick='checkFunc(this)'>确认</a> <a href='javascript:;' onclick='ShowBack("+Eval("Y_ID")+",this)'>拒绝</a>";
        }
        return string.IsNullOrEmpty(opstr)?"<span style='color:gray'>已完结</span>":opstr;
    }
    //拒绝提现,金额不返还
    protected void BackDe_B_Click(object sender, EventArgs e)
    {
        M_Cash cashMod = cashBll.SelReturnModel(Convert.ToInt32(backID_Hid.Value));
        cashMod.yState = (int)ZLEnum.WDState.Rejected;
        cashMod.Result = BackDecs_T.Text;
        //double price = Convert.ToDouble(model.Str1);
        //buser.ChangeVirtualMoney(model.UserID, new M_UserExpHis() { UserID = model.UserID, score = (int)(price + (price * 0.1)), ScoreType = (int)M_UserExpHis.SType.Purse, detail = "管理员拒绝提现申请,余额返还" });
        cashBll.UpdateByID(cashMod);
        Response.Redirect(Request.RawUrl);
    }
    protected void Search_Btn_Click(object sender, EventArgs e)
    {
        MyBind(Search_T.Text);
    }
    protected string GetUser()
    {
        int uid = DataConvert.CLng(Eval("UserID"));
        M_UserInfo mu = buser.SelReturnModel(uid);
        return mu.UserName + "(" + mu.HoneyName + ")";
    }
}
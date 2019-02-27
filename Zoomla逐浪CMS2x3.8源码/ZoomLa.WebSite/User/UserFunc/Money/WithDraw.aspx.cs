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

public partial class User_UserFunc_WithDraw : System.Web.UI.Page
{
    B_Cash cashBll = new B_Cash();
    B_User buser = new B_User();
    private int SType { get { return DataConvert.CLng(Request.QueryString["SType"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin(false);
            NowMoney_L.Text = mu.Purse.ToString("f2").Replace(',','.');
            //CDate_L.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }
    }
    protected void Sure_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin(false);
        double money = DataConverter.CDouble(Money_T.Text.Trim());
        if (money <= 0) { function.WriteErrMsg("提现金额错误,不能小于或等于0"); }
        if (mu.Purse < money) { function.WriteErrMsg("你只有[" + mu.Purse.ToString("f2").Replace(',', '.') + "]不够提现"); }
        //生成提现单据
        M_Cash cashMod = new M_Cash();
        cashMod.UserID = mu.UserID;
        cashMod.money = money;
        cashMod.YName = mu.UserName;
        cashMod.Account = Account_T.Text;
        cashMod.Bank = Bank_T.Text;
        cashMod.PeopleName = PName_T.Text;
        //cashMod.Remark = mu.UserName + "发起提现,金额:" + cashMod.money.ToString("f2");
        cashMod.yState = 0;
        cashMod.Classes = 0;
        cashMod.Remark = Remark_T.Text;
        buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis()
        {
            score = -money,
            ScoreType = (int)M_UserExpHis.SType.Purse,
            detail = cashMod.Remark
        });
        cashBll.insert(cashMod);
        function.WriteSuccessMsg("提现申请成功,请等待管理员审核", "WithDrawLog.aspx");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class User_UserFunc_Money_GiveMoney : System.Web.UI.Page
{
    B_User buser = new B_User();
    //用于将自己的虚拟币赠送给其他用户,默认关闭
    public int SType { get { return DataConverter.CLng(Request.QueryString["stype"]); } }
    public string TypeName { get { return buser.GetVirtualMoneyName(SType); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        if (!IsPostBack)
        {
            if (SType <= 0) { function.WriteErrMsg("参数错误!"); }
            M_UserInfo usermod = buser.GetLogin();
            double score = GetUserScore(usermod);
            if (score <= 0) { function.WriteErrMsg("您的" + TypeName + "为0!"); }
            UserScore_L.Text = score.ToString();
        }
    }

    protected void SendScore_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo usermod = buser.GetLogin();
        int score = DataConverter.CLng(Score_T.Text);
        if (score < 1) { function.WriteErrMsg(TypeName + "不能小于1"); }
        if (GetUserScore(usermod) < score) { function.WriteErrMsg("您的" + TypeName + "不足!!"); }
        M_UserInfo touser= buser.GetUserByName(UserName_T.Text);
        if (touser.UserID <= 0) { function.WriteErrMsg("赠送用户不存在!"); }
        if (touser.UserID == usermod.UserID) { function.WriteErrMsg("不能给自己充值!"); }
        buser.ChangeVirtualMoney(usermod.UserID, new M_UserExpHis() {
            ScoreType=SType,
            score=-score,
            detail="赠送给"+touser.UserName+score+ TypeName
        });
        string remark = Remark_T.Text;
        if (string.IsNullOrEmpty(remark)) { remark = usermod.UserName + "赠送了" + score + TypeName + "给您!"; }
        buser.ChangeVirtualMoney(touser.UserID, new M_UserExpHis() {
            ScoreType = SType,
            score = score,
            detail = remark
        });
        function.Script(this,"parent.location=parent.window.location;");
    }
    //获得用户的虚拟币
    private double GetUserScore(M_UserInfo mu)
    {
        M_UserExpHis.SType ExpType = (M_UserExpHis.SType)SType;
        switch (ExpType)
        {
            case M_UserExpHis.SType.Purse:
                return mu.Purse;
            case M_UserExpHis.SType.SIcon:
                return mu.SilverCoin;
            case M_UserExpHis.SType.Point:
                return mu.UserExp;
            case M_UserExpHis.SType.UserPoint:
                return mu.UserPoint;
            case M_UserExpHis.SType.DummyPoint:
                return mu.DummyPurse;
            case M_UserExpHis.SType.Credit:
                return mu.UserCreit;
            default:
                return mu.UserExp;
        }
    }
}
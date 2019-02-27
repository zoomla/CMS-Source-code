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

public partial class User_Info_DbCardActivate : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Card cardBll = new B_Card();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        DataTable dt = cardBll.SelActivateRcords(buser.GetLogin().UserID);
        if (dt != null && dt.Rows.Count > 0)
        {
            EGV.Visible = true;
            EGV.DataSource = dt;
            EGV.DataBind();
        }
    }
    protected void Activate_B_Click(object sender, EventArgs e)
    {

        string card1 = Card1_T.Text.Trim();
        string card2 = Card2_T.Text.Trim();
        if (card1 == card2) { function.WriteErrMsg("不能输入同一个卡号"); }
        M_Card cardMod1 = cardBll.SelectNum(card1);
        M_Card cardMod2 = cardBll.SelectNum(card2);
        if (cardMod1.CardNum == string.Empty || cardMod2.CardNum == string.Empty) { function.WriteErrMsg("卡号有误，请重新输入。"); }
        if (cardMod1.ActivateState == 1 || cardMod2.ActivateState == 1) { function.WriteErrMsg("已使用的卡不能再次使用，请再次重试。"); }
        if (cardBll.DoubleActivation(card1, card2, buser.GetLogin().UserID)) { function.WriteSuccessMsg("激活成功"); }
        else { function.WriteErrMsg("激活失败!"); }
    }
    protected string GetUserName(string uid)
    {
        if (DataConverter.CLng(uid) == 0)
        {
            return "暂无用户";
        }
        else
        {
            return buser.GetUserByUserID(int.Parse(uid)).UserName;
        }
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}
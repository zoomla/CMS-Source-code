using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data;

public partial class User_Info_DredgeVip : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Group bgp = new B_Group();
    B_Card bc = new B_Card();
    B_CardType bcType = new B_CardType();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();
            DataTable dt = bc.SelCarByUserID(mu.UserID);
            if (dt != null && dt.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 2;
                Card_RPT.DataSource = dt;
                Card_RPT.DataBind();
            }
            else
            {
                M_Card aa = bc.SelectUser(mu.UserID);
                if (aa.AssociateUserID == mu.UserID)
                {
                    this.MultiView1.ActiveViewIndex = 1;
                    this.Label1.Text = aa.CardNum;
                    if (aa.CardState == 2)
                    {
                        M_CardType mt = bcType.SelectType(aa.VIPType);
                        this.Label2.Text = "你的VIP卡优惠为:" + mt.typename + "   <br />有效期限到" + aa.CircumscribeTime + "为止。";
                    }
                    else
                        this.Label2.Text = "此卡以过期或停止服务";
                }
                else
                {
                    this.MultiView1.ActiveViewIndex = 0;
                }
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string cardnum = txtVIP.Text.Trim();
        string cardpas = txtPas.Text.Trim();
        M_Card aa = bc.SelectNum(cardnum);
        if (aa.CardNum == null) { function.WriteErrMsg("没有此卡ID"); }
        if (string.IsNullOrEmpty(cardnum) || string.IsNullOrEmpty(cardpas)) { function.WriteErrMsg("卡号与密码不能为空"); }
        if (!bc.SelectPas(cardnum, cardpas)) { function.WriteErrMsg("密码不正确"); }
        if (bc.GetUpdatePas(cardnum, cardpas, buser.GetLogin().UserID))
        { function.WriteSuccessMsg("验证成功"); }
        else
        { function.WriteErrMsg("验证失败"); }
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
    protected string GetState(string str)
    {
        string state = "";
        switch (str)
        {
            case "1":
                state = "未启用";
                break;
            case "2":
                state = "启用";
                break;
            case "3":
                state = "停用";
                break;
        }
        return state;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class Manage_WeiXin_WxConfig : CustomerPageAction
{
    B_WX_APPID wxbll = new B_WX_APPID();
    public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "WeiXin/WxAppManage.aspx'>公众号管理</a></li><li class='active'>微信配置</li>");
            MyBind();
        }
    }
    public void MyBind()
    {
        if (Mid > 0)
        {
            M_WX_APPID wxmod = wxbll.SelReturnModel(Mid);
            Alias_T.Text = wxmod.Alias;
            AppID_T.Text = wxmod.APPID;
            Secret_T.Text = wxmod.Secret;
            Token_L.Text = wxmod.Token;
            WxNo_T.Text = wxmod.WxNo;
            OrginID_T.Text = wxmod.OrginID;
        }
        if (string.IsNullOrEmpty(Token_L.Text))
            Token_L.Text = "<span style='color:#999'>系统自动获取</span>";
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_WX_APPID wxmod = new M_WX_APPID();
        if (Mid > 0) wxmod = wxbll.SelReturnModel(Mid);
        wxmod.Alias = Alias_T.Text;
        wxmod.APPID = AppID_T.Text;
        wxmod.Secret = Secret_T.Text;
        wxmod.WxNo = WxNo_T.Text;
        wxmod.Status = 1;
        wxmod.OrginID = OrginID_T.Text;
        if (Mid > 0) wxbll.UpdateByID(wxmod);
        else { wxmod.ID = wxbll.Insert(wxmod); }
        function.WriteSuccessMsg("保存成功!", "WxAppManage.aspx?action=add&id=" + wxmod.ID + "&alias=" + wxmod.Alias);
    }
    protected void ReToken_Btn_Click(object sender, EventArgs e)
    {
        WxAPI api = WxAPI.Code_Get(Mid);
        api.GetToken();
        function.WriteSuccessMsg("已重新获取Token");
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
using ZoomLa.BLL.Helper;

public partial class manage_APP_AddUcenter : CustomerPageAction
{
    B_Ucenter ucBll = new B_Ucenter();
    B_Admin badmin = new B_Admin();
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.IsSuperManage();
        if (!IsPostBack)
        {
            if (Mid > 0)
            {
                M_Ucenter ucMod = ucBll.Select(Mid);
                Alias_T.Text = ucMod.Alias;
                TxtWebSite.Text = ucMod.WebSite;
                DBUName_T.Text =ucMod.DBUName;
                Key_L.Text = ucMod.Key;
                Status_Chk.Checked = ucMod.Status == 1 ? true : false;
                function.Script(this, "SetChkVal('userauth','" + ucMod.UserAuth + "');");
                function.Script(this, "SetChkVal('askauth','" + ucMod.AskAuth + "');");
                lblText.Text = "修改";
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "user/UserManage.aspx'>用户管理</a><li><a href='WsApi.aspx'>跨站接入</a></li><li>添加授权网站</li>");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        M_Ucenter ucMod = new M_Ucenter();
        if (Mid > 0)
        {
            ucMod = ucBll.Select(Mid);
        }
        else
        {
            ucMod.Key = function.GetRandomString(20);
            ucMod.AddTime = DateTime.Now;
        }
        ucMod.Alias = Alias_T.Text.Trim();
        ucMod.WebSite =StrHelper.UrlDeal(TxtWebSite.Text);
        if (!string.IsNullOrEmpty(DBUName_T.Text) && !string.IsNullOrEmpty(DBPwd_T.Text))
        {
            ucMod.DBUName = DBUName_T.Text;
            ucMod.DBPwd = DBPwd_T.Text;
        }
        ucMod.UserAuth = Request.Form["userauth"];
        ucMod.AskAuth = Request.Form["askauth"];
        //ucMod.ConAuth = "";
        //ucMod.OtherAuth = "";
        ucMod.Status = Status_Chk.Checked ? 1 : 0;
        if (Mid > 0)
        {
            ucBll.Update(ucMod);
        }
        else
        {
            ucBll.Insert(ucMod);
        }
        function.WriteSuccessMsg("操作成功","WsApi.aspx");
    }
}
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.User;

public partial class User_UserZone_StyleSet : Page
{
    B_User buser = new B_User();
    B_User_BlogStyle bsBll = new B_User_BlogStyle();
    M_User_BlogStyle bsMod = null;
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
        if (mu.State != 2)
        {
            main_div.InnerHtml = "<div class='alert alert-info'>未通过聚合认证会员无法开启个人主页!</div>"; return;
        }
        if (bsBll.Sel().Rows.Count < 1)//提示系统未提供可用模板！
        {
            main_div.InnerHtml = "<div class='alert alert-info'><i class='fa fa-exclamation-triangle'></i>&nbsp系统未提供可用模板!</div>"; return;
        }
        else
        {
            if (mu.PageID < 1) { StyleNameLB.Text = "还没有选定模板"; }
            else
            {
                bsMod = bsBll.SelReturnModel(mu.PageID);
                if (bsMod != null)
                {
                    StyleNameLB.Text = bsMod.StyleName;
                    StyleNameLB.Text += " <a href='/Space/Default.aspx?ID=" + mu.UserID + "' target='_blank' class='btn btn-xs btn-info'>访问页面</a>";
                }
            }
        }
        RPT.DataSource = bsBll.Sel();
        RPT.DataBind();
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "apply":
                M_UserInfo mu = buser.GetLogin();
                B_User.UpdateField("PageID", e.CommandArgument.ToString(), mu.UserID);
                break;
        }
        function.WriteSuccessMsg("操作成功");
    }
}

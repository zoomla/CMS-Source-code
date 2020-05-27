using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Sns;
using ZoomLa.Model;

public partial class User_UserZone_ZoneEditApply : Page
{
    #region 业务对象
    B_User ubll = new B_User();
    blogTableBLL btbll = new blogTableBLL();
    #endregion
    int currentUser = 0;

    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = ubll.GetLogin().UserID;
        ubll.CheckIsLogin();
        ViewState["action"] = Request.QueryString["action"];
        if (!IsPostBack)
        {

            GetInit();
        }
    }
    #endregion

    #region 页面方法
    private void GetInit()
    {
        M_UserInfo uinfo = ubll.GetUserByUserID(currentUser);

        blogTable bt = btbll.GetUserBlog(currentUser);
        if (ViewState["action"] == null)//
        {
            switch (bt.BlogState.ToString())
            {
                case "0": this.Label2.Text = "正在审核中......! 请等待管理员开通您的空间功能!"; break;
                case "2": this.Label2.Text = "你提交的信息审核未通过,请修改后重新提交!"; break;
                default: break;
            }
            this.add.Visible = false;
            this.Auditing.Visible = true;

        }
        else
        {
            this.add.Visible = true;
            this.Auditing.Visible = false;

            this.Nametxt.Text = bt.BlogName;
            this.textareacontent.Value = bt.BlogContent;
        }

    }

    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        blogTable bt = btbll.GetUserBlog(currentUser);
        bt.BlogName = this.Nametxt.Text;
        bt.BlogContent = this.textareacontent.Value;
        btbll.UpdateBlogtable(bt);
        base.Response.Redirect("ZoneEditApply.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ZoneEditApply.aspx?action=Show");
    }
    #endregion
}

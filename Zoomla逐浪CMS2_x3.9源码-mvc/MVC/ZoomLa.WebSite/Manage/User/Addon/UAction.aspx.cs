using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Auth;
using ZoomLa.BLL.Chat;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Chat;


namespace ZoomLaCMS.Manage.User.Addon
{
    public partial class UAction : CustomerPageAction
    {/*
  * 1,管理员进去后,可能用户还未进入页面,需要刷新下才可改变用户
  * 2,如果用户登录了,也是以游客身份,需要处理
  */
        B_UAction uaBll = new B_UAction();
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li><a href='UserManage.aspx'>会员管理</a></li><li>会员行为记录</li>");
            }
        }
        private void MyBind(string search = "")
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(search))
            {
                dt = uaBll.SelBySearch(search, DataConverter.CLng(SearchType_Drop.SelectedValue));
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["searchkey"]))
            {
                dt = uaBll.SelBySearch(Request.QueryString["searchkey"], DataConverter.CLng(Request.QueryString["type"]));
            }
            else
                dt = uaBll.Sel();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                uaBll.DelByIDS(Request.Form["idchk"]);
                MyBind();
            }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            MyBind(Search_T.Text);
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "event":
                    B_User buser = new B_User();
                    M_UserInfo mu = buser.GetLogin();
                    if (mu == null || mu.UserID < 1)
                    {
                        mu = buser.GetUserByName(badmin.GetAdminLogin().UserName);
                        buser.SetLoginState(mu);
                    }
                    if (mu == null || mu.UserID < 1)
                    {
                        function.WriteErrMsg("你还没有绑定用户,请绑定用户后再发起聊天");
                    }
                    ZLEvent.AddEvent(new M_ZLEvent()
                    {
                        MyType = ZLEvent.EventT.UAction,
                        Name = e.CommandArgument.ToString(),
                        Value = "{\"action\":\"chat\",\"uid\":\"" + mu.UserID + "\"}"
                    });
                    function.Script(this, "GetTo('" + e.CommandArgument + "');");
                    break;
            }
        }
        public string GetIpLocation(string ip)
        {
            return IPScaner.IPLocation(ip);
        }
    }
}
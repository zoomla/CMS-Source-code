using Newtonsoft.Json;
using System;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.User
{
    public partial class GroupManage : CustomerPageAction
    {
        B_Group bll = new B_Group();
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        public string Action { get { return Request.QueryString["action"] ?? ""; } }
        public int GID { get { return DataConvert.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {

                int pid = DataConvert.CLng(Request.Form["pid"]);
                string json = "";
                DataTable dt = bll.GetChildGroup(pid);
                json = JsonConvert.SerializeObject(dt);
                Response.Clear();
                Response.Write(json);
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "UserGroup"))
                {
                    function.WriteErrMsg(Resources.L.没有权限进行此项操作);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["action"]) && !string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (Action == "default")
                    {
                        bll.SetDefaultGroup(GID);
                    }
                    else if (Action == "del")
                    {
                        this.bll.Del(GID);
                    }
                    Response.Redirect("GroupManage.aspx");
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='UserManage.aspx'>" + Resources.L.用户管理 + "</a></li> <li class='active'>" + Resources.L.会员组管理 + "<a href=\"Group.aspx\">[" + Resources.L.添加会员组 + "]</a></li>" + Call.GetHelp(38) + "<div class='pull-right'><a href='javascript:;' onclick='location.reload();'><i class='fa fa-refresh'></i></a></div>");
        }
    }
}
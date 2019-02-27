using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.User;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.User.Promo
{
    public partial class InviteCodeList : System.Web.UI.Page
    {
        B_User_InviteCode inviteBll = new B_User_InviteCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "user/UserManage.aspx'>用户管理</a></li><li>邀请码列表 [<a href='InviteCodeAdd.aspx'>添加邀请码</a>]</li>");
            }
        }
        private void MyBind()
        {
            EGV.DataSource = inviteBll.Sel();
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    inviteBll.Del(id);
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                //e.Row.Attributes.Add("ondblclick", "location='InviteCodeAdd.aspx?ID=" + dr["ID"] + "'");
            }
        }
        public string GetCUser()
        {
            int cadmin = DataConverter.CLng(Eval("CAdmin"));
            if (cadmin < 1) { return "<a href='javascript:;' onclick='user.showuinfo('" + Eval("UserID") + "');'>" + Eval("UserName", "") + "</a>"; }
            else { return "管理员"; }
        }
        public string GetGroupName()
        {
            string name = DataConverter.CStr(Eval("GroupName"));
            return string.IsNullOrEmpty(name) ? "默认会员组" : name;
        }
        public string GetUsedInfo() 
        {
            if(string.IsNullOrEmpty(DataConverter.CStr(Eval("UsedUserName")))){return "";}
            return "<a href=\"javascript:;\" onclick=\"user.showuinfo('" + Eval("UsedUserID") + "');\">" + Eval("UsedUserName") + "(" + Eval("UsedUserID") + ")</a>";
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids=Request.Form["idchk"];
            if(!string.IsNullOrEmpty(ids))
            {
                inviteBll.DelByIDS(ids);
                function.WriteSuccessMsg("删除成功");
            }
            MyBind();
        }
    }
}
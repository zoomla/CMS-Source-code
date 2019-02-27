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
using ZoomLa.Common;
using ZoomLa.Web;
using ZoomLa.BLL;
using System.Collections.Generic;
using ZoomLa.Model;
using ZoomLa.BLL.User;

namespace ZoomLaCMS.Manage.Zone
{
    public partial class ZoneStyleManage :CustomerPageAction
    {
        B_User_BlogStyle bsBll = new B_User_BlogStyle();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "ZoneStyleManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li class='active'>空间模板管理<a href='ZoneStyleAdd.aspx'>[添加空间模板]</a></li>");
        }
        private void MyBind()
        {
            RPT.DataSource = bsBll.Sel();
            RPT.DataBind();
        }
        protected string GetState(string gid)
        {
            return "启用";
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (string.IsNullOrWhiteSpace(ids)) { function.WriteErrMsg("未指定需要删除数据"); }
            bsBll.DelByIDS(ids);
            MyBind();
        }
        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    bsBll.Del(id);
                    break;
            }
            MyBind();
        }
    }
}
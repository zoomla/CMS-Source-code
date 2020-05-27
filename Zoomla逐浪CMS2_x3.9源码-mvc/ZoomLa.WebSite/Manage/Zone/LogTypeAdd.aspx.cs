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
using BDUBLL;
using BDUModel;
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Zone
{
    public partial class LogTypeAdd : CustomerPageAction
    {
        //GSManageBLL gsbll = new GSManageBLL();
        B_Admin ubll = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li><a href='GSManage.aspx'>空间族群列表</a></li><li><a href='LogTypeManage.aspx'>族群类型管理</a></li><li class='active'>添加族群类型</a></li>");
        }
        protected void addbtn_Click(object sender, EventArgs e)
        {
            GSType gs = new GSType();
            gs.GSTypeName = this.Nametxt.Text;
            //gsbll.InsertType(gs);
            Response.Redirect("LogTypeManage.aspx");
        }
    }
}
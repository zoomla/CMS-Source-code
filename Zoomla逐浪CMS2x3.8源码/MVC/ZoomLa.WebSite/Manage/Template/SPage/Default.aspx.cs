using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;

namespace ZoomLaCMS.Manage.Design.SPage
{
    public partial class Default : CustomerPageAction
    {
        B_SPage_Page pageBll = new B_SPage_Page();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>可视设计</a></li><li class='active'>页面列表 <a href='AddPage.aspx'>[添加页面]</a></li><span class='pull-right'><a href='javascript:;' onclick='ShowColor(this);'><i class='fa fa-list-ul'></i></a></span>");
            }
        }
        private void MyBind()
        {
            RPT.DataSource = pageBll.Sel();
            RPT.DataBind();
        }

        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            M_AdminInfo adminMod = B_Admin.GetLogin();
            switch (e.CommandName)
            {
                case "del2":
                    pageBll.Del(Convert.ToInt32(e.CommandArgument));
                    break;
                case "copy":
                    M_SPage_Page pageMod = pageBll.SelReturnModel(Convert.ToInt32(e.CommandArgument));
                    pageMod.CDate = DateTime.Now;
                    pageMod.UserID = adminMod.AdminId;
                    pageBll.Insert(pageMod);
                    function.WriteSuccessMsg("复制成功");
                    break;
            }
            MyBind();
        }
    }
}
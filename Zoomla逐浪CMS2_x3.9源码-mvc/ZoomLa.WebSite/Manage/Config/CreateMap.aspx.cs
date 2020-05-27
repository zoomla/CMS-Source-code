using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ZoomLa.Common;
using System.Text.RegularExpressions;
using System.Data;
using ZoomLa.Components;
using System.Reflection;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.AppCode;

/*
 * 重新生成SiteMap，用于支持全站搜索
 */

namespace ZoomLaCMS.Manage.Config
{
    public partial class CreateMap : System.Web.UI.Page
    {
        private string xmlPath = "/Config/SiteMap.config";
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged();
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class='active'>页面索引(用于主页右上方的快速搜索功能)</li>");
            }
        }
        protected void Sure_Btn_Click(object sender, EventArgs e)
        {
            function.CreateSiteMap(Server.MapPath("/Manage"), Server.MapPath(xmlPath));
            function.WriteSuccessMsg("生成后台索引成功");
        }
        protected void SureUser_Btn_Click(object sender, EventArgs e)
        {
            function.CreateSiteMap(Server.MapPath("/User"), Server.MapPath("/Config/UserMap.config"));
            function.WriteSuccessMsg("生成用户索引成功");
        }
    }
}
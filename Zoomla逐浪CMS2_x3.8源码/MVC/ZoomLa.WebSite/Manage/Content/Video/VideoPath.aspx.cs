using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;


namespace ZoomLaCMS.Manage.Content.Video
{
    public partial class VideoPath : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                function.Script(this, "SetRadVal('openftp_rad','" + SiteConfig.SiteOption.OpenFTP + "');");
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../ContentManage.aspx'>内容管理</a></li><li class='active'>视频路径</li>");
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            SiteConfig.SiteOption.OpenFTP = Request.Form["openftp_rad"];
            SiteConfig.Update();
            function.WriteSuccessMsg("配置保存成功");
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class Manage_Content_Video_VideoConfig : System.Web.UI.Page
{
    private delegate void DownFileDel(string url, string vpath, ProgMod model);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            //返回进度
            Response.Write(((ProgMod)Session["ProgMod"]).ProgStatus);
            Response.Flush();
            Response.End();
        }
        else if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../ContentManage.aspx'>内容管理</a></li><li class='active'>视频配置</li>");
            if (File.Exists(Server.MapPath("/Tools/ffmpeg.exe")))
            {
                remind_sp.InnerHtml = "你已下载视频转换工具,可以正常使用视频管理功能";
                getLoad.Text = "重新下载";
            }
            videoPath.Text = SiteConfig.SiteOption.Videourl;
        }
    }
    protected void BDown_Click(object sender, EventArgs e)
    {
        DownFileDel down = FileUDHelper.DownFile;
        ProgMod prog = new ProgMod();
        Session["ProgMod"] = prog; 
        IAsyncResult result = down.BeginInvoke("http://code.z01.com/ffmpeg.exe", "/Tools/ffmpeg.exe", prog, null, null);
        function.Script(this, "beginCheck('getProgress');");
    }

    protected void SaveConfig_Click(object sender, EventArgs e)
    {
        SiteConfig.SiteOption.Videourl = videoPath.Text.Trim();
        SiteConfig.Update();
        Response.Redirect("VideoList.aspx");
    }
}
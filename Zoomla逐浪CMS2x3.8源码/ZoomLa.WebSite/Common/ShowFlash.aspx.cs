using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Components;
using System.IO;

public partial class Common_ShowFlash : System.Web.UI.Page
{
    public string swfurl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["type"]))
            swfurl = SiteConfig.SiteOption.UploadDir + "read/swf/" + Request.QueryString["path"];
        else
        {
            string path = Server.UrlDecode(Request.QueryString["path"]);
            string fileMapth = SiteConfig.SiteMapath() + path.Replace(@"/", @"\");
            string swffile = Path.ChangeExtension(path, ".swf");
            string swfMapath = SiteConfig.SiteMapath() + swffile.Replace(@"/", @"\"); //转换成绝对路径 
            if (!File.Exists(swfMapath))
            {
                String fs_filename = fileMapth;
                String fs_convertedfilename = swfMapath;
                Print2Flash3.Server2 p2fServer = new Print2Flash3.Server2();
                p2fServer.ConvertFile(fs_filename, fs_convertedfilename, null, null, null);
                swfurl = swffile;
            }
            else
                swfurl = swffile;
        }
    }
}
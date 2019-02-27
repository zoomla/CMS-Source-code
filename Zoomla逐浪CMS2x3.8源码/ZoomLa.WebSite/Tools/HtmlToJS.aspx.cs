using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class Tools_HtmlToJS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged();
    }
    protected void BeginUP_Btn_Click(object sender, EventArgs e)
    {
        //string[] exts="txt,html,htm,shtml".Split(',');
        //string ext = Path.GetFileName(Html_UP.FileName).ToLower().Replace(".", "");
        //if (!exts.Contains(ext) || string.IsNullOrEmpty(ext)) { function.WriteErrMsg("请上传文本或Html文件"); }
        if (!Html_UP.HasFile) return;
        string vpath = "/UploadFiles/temp.txt";
        Html_UP.SaveAs(vpath);
        Html_T.Text = SafeSC.ReadFileStr(vpath);
        //读取Html中的内容
    }
}
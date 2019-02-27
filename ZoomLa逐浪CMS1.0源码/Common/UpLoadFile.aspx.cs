using ASP;
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
using ZoomLa.BLL;
using ZoomLa.Common;


public partial class Common_UpLoadFile : System.Web.UI.Page
{
    private B_UpLoadFile BUpLoadFile = new B_UpLoadFile();

    protected void Page_Load(object sender, EventArgs e)
    {
        B_User buser = new B_User();
        buser.CheckIsLogin();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string[] strArray = this.File_PicPath.Text.Split(new char[] { '|' });
        string str2 = base.Request.QueryString["ControlId"];
        string str3 = this.BUpLoadFile.GetUpLoadFile(this.File1, "" + function.ApplicationRootPath + "/UploadFiles/" + strArray[0] + "/", int.Parse(strArray[1]));
        base.Response.Write("<script>window.dialogArguments.$('" + str2 + "').value='" + str3 + "';window.close();</script>");
        base.Response.End();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Design_Diag_Image_edit : System.Web.UI.Page
{
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
 
        }
    }
    protected void SFile_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        string fpath = B_Design_SiteInfo.GetSiteUpDir(mu.SiteID);
        fpath = SafeSC.SaveFile(fpath,SFile_UP, function.GetFileName() + Path.GetExtension(SFile_UP.FileName));
        function.Script(this, "setTimeout(function () { updateiurl('"+fpath+"');},50);");
    }
}
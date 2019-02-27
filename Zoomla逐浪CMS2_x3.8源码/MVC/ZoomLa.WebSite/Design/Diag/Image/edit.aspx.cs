namespace ZoomLaCMS.Design.Diag.Image
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Design;
    using ZoomLa.BLL.Helper;
    using ZoomLa.Common;
    using ZoomLa.Model;
    public partial class edit : System.Web.UI.Page
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
            ImgHelper imghelp = new ImgHelper();
            string fpath = B_Design_SiteInfo.GetSiteUpDir(mu.SiteID);
            if (!SafeSC.IsImage(SFile_UP.FileName)) { function.WriteErrMsg("上传的图片后缀名不允许"); }
            if (Path.GetExtension(SFile_UP.FileName).Equals(".gif"))
            {
                fpath = SafeSC.SaveFile(fpath, SFile_UP, function.GetFileName() + Path.GetExtension(SFile_UP.FileName));
            }
            else
            {
                fpath += function.GetFileName() + Path.GetExtension(SFile_UP.FileName);
                imghelp.CompressImg(SFile_UP.PostedFile, 1024, fpath);
            }
            function.Script(this, "setTimeout(function () { updateiurl('" + fpath + "');},50);");
        }
    }
}
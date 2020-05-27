using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;


namespace ZoomLaCMS.dai
{
    public partial class DownImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string base64 = Request.Form["img_hid"];
            if (string.IsNullOrEmpty(base64)) { Response.Write("数据为空,无法生成图片"); Response.End(); }
            if (base64.Contains(",")) { base64 = base64.Split(',')[1]; }
            ImgHelper imgHelper = new ImgHelper();
            string vpath = "/UploadFiles/dai/" + function.GetRandomString(6) + ".jpg";
            imgHelper.Base64ToImg(vpath, base64);
            SafeSC.DownFile(vpath, "glassed.jpg");
        }
    }
}
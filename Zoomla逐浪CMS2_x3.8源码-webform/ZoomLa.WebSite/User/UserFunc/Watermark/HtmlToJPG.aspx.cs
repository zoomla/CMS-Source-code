using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class User_UserFunc_Watermark_HtmlToJPG : System.Web.UI.Page
{
    IEBrowHelper ieHelp = new IEBrowHelper();
    //需优化,下载时才生成图片,其他时刻不生成
    protected void Page_Load(object sender, EventArgs e)
    {
        string vpath = SiteConfig.SiteOption.UploadDir + "/User/UserFunc/";
        string url = SiteConfig.SiteInfo.SiteUrl.TrimEnd('/');
        Bitmap m_Bitmap = ieHelp.GetWebSiteThumbnail(Request.Url.Scheme + "://" + Request.Url.Authority + "/User/UserFunc/Watermark/Cpic.aspx" + Request.Url.Query, 1024, 723, 1024, 723);
        MemoryStream ms = new MemoryStream();
        m_Bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);//JPG、GIF、PNG等均可 
        byte[] buff = ms.ToArray();
        //Response.BinaryWrite(buff);
        string fname = function.GetRandomString(3) + ".jpg";
        SafeSC.SaveFile(vpath, fname, buff);
        CerPic_Img.Src = vpath + fname;
        Title_T.InnerHtml = Request.QueryString["Name"] + "的证书_" + Call.SiteName;
        CerPic_Img.Attributes.Add("alt", Request.QueryString["Name"] + "的证书");
    }
    protected void SaveImage_Click(object sender, EventArgs e)
    {
        SafeSC.DownFile(CerPic_Img.Src);
    }
}
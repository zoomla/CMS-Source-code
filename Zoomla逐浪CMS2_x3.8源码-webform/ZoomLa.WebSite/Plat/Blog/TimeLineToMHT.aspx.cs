using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Common.Addon;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Plat_Blog_TimeLineToHMT : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_User_Plat upBll = new B_User_Plat();
    private int uid { get { return DataConvert.CLng(Request.QueryString["uid"]); } }
    private string type { get { return Request.QueryString["type"] ?? ""; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_User_Plat upMod = B_User_Plat.GetLogin();
            if (upMod == null) { function.WriteErrMsg("您还没有登录，不能使用该功能！"); }
            M_UserInfo info = buser.SelReturnModel(uid);
            if (info.IsNull) { function.WriteErrMsg("用户不存在!"); }
            M_User_Plat upMod2 = upBll.SelReturnModel(uid);
            if (upMod2.CompID != upMod.CompID) { function.WriteErrMsg("您没有权限下载该用户的时间线记录！"); }
            if (type.Equals("mht"))
            {
                HtmlHelper htmlHelp = new HtmlHelper();
                string url = SiteConfig.SiteInfo.SiteUrl + "/Plat/Blog/Timeline.aspx?uids=" + uid + "&uname=" + info.UserName + "&upwd=" + info.UserPwd;
                string vpath = htmlHelp.DownToMHT(url, "/UploadFiles/" + info.UserName + "的时间线.mht");
                SafeSC.DownFile(vpath);
            }
            else if (type.Equals("pdf"))
            {
                StringWriter sw = new StringWriter();
                string url ="/Plat/Blog/TimelineToPDF.aspx?uids=" + uid + "&uname=" + info.UserName + "&upwd=" + info.UserPwd;
                Server.Execute(url, sw);
                string vpath = "/UploadFiles/" + info.UserName + "的时间线.pdf";
                PdfHelper.HtmlToPdf(sw.ToString(), "", vpath);
                SafeSC.DownFile(vpath);
            }
        }
    }
}
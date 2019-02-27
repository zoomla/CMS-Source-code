using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Design;

public partial class Design_mbh5_EditScence : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Design_Scence pageBll = new B_Design_Scence();
    B_Design_Tlp tlpBll = new B_Design_Tlp();
    B_Design_Scence scenceBll = new B_Design_Scence();
    public string Guid { get { return Request.QueryString["id"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_Design_Page pageMod = pageBll.SelModelByGuid(Guid);
        M_UserInfo mu = buser.GetLogin();
        if (pageMod == null) { function.WriteErrMsg("指定的场景不存在"); }
        if (pageMod.UserID != mu.UserID) { function.WriteErrMsg("你无权修改该场景"); }
        Title_T.Text = pageMod.Title;
        SFile_Up.FileUrl = pageMod.PreviewImg;
    }

    protected void Edit_B_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_Design_Page pageMod = pageBll.SelModelByGuid(Guid);
        pageMod.Title = Title_T.Text;
        SFile_Up.SaveUrl = SiteConfig.SiteOption.UploadDir + "User/" + mu.UserName + mu.UserID + "/";
        pageMod.PreviewImg = SFile_Up.SaveFile();
        pageBll.UpdateByID(pageMod);
        function.Script(this, "parent.refresh('" + pageMod.Title + "-来自[" + mu.TrueName + "]的手机创作','" + pageMod.PreviewImg + "');");
    }
}
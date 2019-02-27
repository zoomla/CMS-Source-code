namespace ZoomLaCMS.Design.Diag
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Design;
    using ZoomLa.BLL.Helper;
    using ZoomLa.Common;
    using ZoomLa.Model.Design;

    public partial class se_page : System.Web.UI.Page
    {
        M_Design_Page pageMod = null;
        B_Design_Scence pageBll = new B_Design_Scence();
        public string Mid { get { return Request.QueryString["ID"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            if (string.IsNullOrEmpty(Mid)) { function.WriteErrMsg("错误,未传入页面ID"); }
            pageMod = pageBll.SelModelByGuid(Mid);
            Title_T.Text = pageMod.Title;
            Meta_T.Text = pageMod.Meta;
            Remind_T.Text = pageMod.Remind;

            PreviewImg_UP.FileUrl = pageMod.PreviewImg;
            Thumb_UP.FileUrl = pageMod.ThumbImg;
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            pageMod = pageBll.SelModelByGuid(Mid);
            pageMod.Title = Title_T.Text;
            pageMod.Meta = Meta_T.Text;
            pageMod.Remind = Remind_T.Text;
            err_sp.InnerHtml = "";
            if (Thumb_UP.HasFile)
            {
                Thumb_UP.SaveFile();
                pageMod.ThumbImg = Thumb_UP.FileUrl;
            }
            else //未指定文件,则以文本框中的为准
            {
                pageMod.ThumbImg = Thumb_UP.FVPath;
            }
            string oldimg = pageMod.PreviewImg;
            if (PreviewImg_UP.HasFile)
            {
                HttpPostedFile file = PreviewImg_UP.Request.Files[0];
                var image = System.Drawing.Image.FromStream(file.InputStream);
                PreviewImg_UP.SaveFile();
                pageMod.PreviewImg = PreviewImg_UP.FileUrl;
            }
            else
            {
                pageMod.PreviewImg = PreviewImg_UP.FVPath;
            }
            if (pageMod.TlpID > 0 && !pageMod.PreviewImg.Equals(oldimg))
            {
                B_Design_Tlp tlpBll = new B_Design_Tlp();
                M_Design_Tlp tlpMod = tlpBll.SelReturnModel(pageMod.TlpID);
                tlpMod.PreviewImg = pageMod.PreviewImg;
                tlpBll.UpdateByID(tlpMod);
            }
            pageBll.UpdateByID(pageMod);
            function.Script(this, "top.settitle('" + pageMod.Title + "');top.CloseDiag();");
        }
    }
}
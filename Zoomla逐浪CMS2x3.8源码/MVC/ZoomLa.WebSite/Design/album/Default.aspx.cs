namespace ZoomLaCMS.Design.album
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Design;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Model.Design;
    public partial class Default : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Design_Album aumBll = new B_Design_Album();
        B_Design_RES resBll = new B_Design_RES();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            WxAPI.AutoSync(Request.RawUrl);
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                M_Design_Album aumMod = aumBll.SelReturnModel(Mid);
                AlbumName_T.Text = aumMod.AlbumName;
                Photos_Hid.Value = aumMod.Photos;
                UseTlp_Hid.Value = aumMod.UseTlp.ToString();
                function.Script(this, "setMusic('" + aumMod.Music + "');");
            }
            Music_RPT.DataSource = resBll.Search("", "", "music");
            Music_RPT.DataBind();
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            M_Design_Album aumMod = new M_Design_Album();
            if (Mid > 0) { aumMod = aumBll.SelReturnModel(Mid); }
            aumMod.AlbumName = AlbumName_T.Text.Trim();
            //albumMod.AlbumDesc = AlbumDesc_T.Text;
            aumMod.UseTlp = DataConverter.CLng(UseTlp_Hid.Value);
            aumMod.Music = Request.Form["music_rad"];
            aumMod.Photos = Photos_Hid.Value.Trim('|');
            if (string.IsNullOrEmpty(aumMod.Photos)) { function.WriteErrMsg("未指定相册图片"); }
            if (aumMod.ID > 0) { aumBll.UpdateByID(aumMod); }
            else
            {
                aumMod.UserID = mu.UserID;
                aumMod.ID = aumBll.Insert(aumMod);
            }
            Response.Redirect("mbview.aspx?id=" + aumMod.ID);
        }
    }
}
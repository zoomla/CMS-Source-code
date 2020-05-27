namespace ZoomLaCMS.Design.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Design;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Model.Design;
    using ZoomLa.SQLDAL;

    public partial class PageInfo : System.Web.UI.Page
    {
        B_Design_Page pageBll = new B_Design_Page();
        B_User buser = new B_User();
        public string Mid { get { return Request.QueryString["ID"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            M_Design_Page pageMod = pageBll.SelModelByGuid(Mid);
            if (pageMod.UserID != mu.UserID) { function.WriteErrMsg("你无权修改该页面"); }
            Title_T.Text = pageMod.Title;
            Path_T.Text = pageMod.Path;
            CDate_L.Text = pageMod.CDate.ToString("yyyy年MM月dd日 hh:mm");
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_Design_Page pageMod = pageBll.SelModelByGuid(Mid);
            pageMod.Title = Title_T.Text;
            pageMod.Path = Path_T.Text;
            pageBll.UpdateByID(pageMod);
            function.WriteSuccessMsg("修改配置成功");
        }
    }
}
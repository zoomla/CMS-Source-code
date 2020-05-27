using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;
//页面的扩展属性配置,AJAX保存时不提交
namespace ZoomLaCMS.Design.Diag
{
    public partial class Page : System.Web.UI.Page
    {
        M_Design_Page pageMod = null;
        B_Design_Page pageBll = new B_Design_Page();
        public string Mid { get { return Request.QueryString["ID"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            if (!IsPostBack)
            {
                //Call.HideBread(Master);
                MyBind();
            }
        }
        private void MyBind()
        {
            if (string.IsNullOrEmpty(Mid)) { function.WriteErrMsg("错误,未传入页面ID"); }
            pageMod = pageBll.SelModelByGuid(Mid);
            Path_T.Text = pageMod.Path;
            pageMod.Path = Path_T.Text;
            Title_T.Text = pageMod.Title;
            Meta_T.Text = pageMod.Meta;
            Resource_T.Text = pageMod.Resource;
            Remind_T.Text = pageMod.Remind;
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            pageMod = pageBll.SelModelByGuid(Mid);
            pageMod.Title = Title_T.Text;
            pageMod.Meta = Meta_T.Text;
            pageMod.Resource = Resource_T.Text;
            pageMod.Remind = Remind_T.Text;
            pageMod.Path = Path_T.Text.Replace(" ", "");
            pageBll.UpdateByID(pageMod);
            function.Script(this, "CloseDiag();");
        }
    }
}
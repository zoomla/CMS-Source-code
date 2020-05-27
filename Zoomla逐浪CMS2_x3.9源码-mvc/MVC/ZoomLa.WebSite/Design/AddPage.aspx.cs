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
namespace ZoomLaCMS.Design
{
    public partial class AddPage : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Design_Page pageBll = new B_Design_Page();
        B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                if (mu.SiteID < 1) { function.WriteErrMsg("用户未正确关联站点"); }
                M_Design_SiteInfo sfMod = sfBll.SelReturnModel(mu.SiteID);
                if (sfMod == null || sfMod.UserID != mu.UserID.ToString()) { function.WriteErrMsg("你无权修改该站点"); }
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            M_Design_Page pageMod = new M_Design_Page();
            //路径是否存在
            string path = Path_T.Text.Replace(" ", "");
            if (pageBll.IsExist(mu.SiteID, path)) { function.WriteErrMsg("[" + path + "]已存在,请修改"); }
            pageMod.guid = System.Guid.NewGuid().ToString();
            pageMod.Path = path;
            pageMod.Title = Title_T.Text;
            pageMod.UserID = mu.UserID;
            pageMod.UserName = mu.UserName;
            pageMod.SiteID = mu.SiteID;
            pageMod.ZType = DataConverter.CLng(Request.Form["type_rad"]);
            pageBll.Insert(pageMod);
            function.Script(this, "top.location='/Design/?ID=" + pageMod.guid + "';");
        }
    }
}
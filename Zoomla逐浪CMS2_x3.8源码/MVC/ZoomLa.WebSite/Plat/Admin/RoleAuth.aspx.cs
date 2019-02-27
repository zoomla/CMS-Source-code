using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Plat.Admin
{
    public partial class RoleAuth : System.Web.UI.Page
    {
        /*
    * 权限请以P_开头
    */
        B_Plat_UserRole urBll = new B_Plat_UserRole();
        M_Plat_UserRole urMod = new M_Plat_UserRole();
        public int RoleID
        {
            get { return DataConvert.CLng(Request.QueryString["ID"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_User_Plat.IsAdmin()) function.WriteErrMsg("无权访问该页面!!");
            if (!IsPostBack && RoleID > 0)//并且只能为该公司的用户权限组
            {
                urMod = urBll.SelReturnModel(RoleID);//后期像角色ID这些应该是Guid码,避免其能猜到
                if (urMod.CompID != B_User_Plat.GetLogin().CompID) function.WriteErrMsg("该信息无权访问!!");
                RoleName_L.Text = urMod.RoleName;
                RoleDesc_L.Text = urMod.RoleDesc;
                function.Script(this, "InitValue('" + urMod.RoleAuth + "');");
            }
        }

        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            urMod = urBll.SelReturnModel(RoleID);
            string userAuth = Request.Form["UserAuth"];
            urMod.RoleAuth = userAuth;
            urBll.UpdateByID(urMod);
            Response.Redirect(Request.RawUrl);
        }
    }
}
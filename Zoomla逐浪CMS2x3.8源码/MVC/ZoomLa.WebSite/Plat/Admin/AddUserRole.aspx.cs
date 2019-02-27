using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;

namespace ZoomLaCMS.Plat.Admin
{
    public partial class AddUserRole : System.Web.UI.Page
    {
        B_Plat_UserRole urBll = new B_Plat_UserRole();
        M_Plat_UserRole urMod = new M_Plat_UserRole();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_User_Plat.IsAdmin())
            {
                function.WriteErrMsg("非管理员,无权限访问该页!!");
            }
            if (!IsPostBack)
            {
                int id = DataConverter.CLng(Request.QueryString["ID"]);
                if (id > 0)
                {
                    Save_Btn.Text = "确认修改";
                    M_Plat_UserRole urMod = new M_Plat_UserRole();
                    urMod = urBll.SelReturnModel(id);
                    RoleName_T.Text = urMod.RoleName;
                    RoleDesc_T.Text = urMod.RoleDesc;
                }
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            string info = "";
            int id = Convert.ToInt32(string.IsNullOrEmpty(Request.QueryString["ID"]) ? "0" : Request.QueryString["ID"].ToString());
            if (id > 0)
            {
                urMod = urBll.SelReturnModel(id);
                urMod.RoleName = RoleName_T.Text;
                urMod.RoleDesc = RoleDesc_T.Text;
                info = urBll.UpdateByID(urMod) ? "修改成功！" : "修改失败！";
            }
            else
            {
                M_User_Plat upMod = B_User_Plat.GetLogin();
                urMod.RoleName = RoleName_T.Text;
                urMod.RoleDesc = RoleDesc_T.Text;
                urMod.RoleAuth = "";
                urMod.UserID = upMod.UserID;
                urMod.CompID = upMod.CompID;
                urMod.CreateTime = DateTime.Now;
                info = urBll.Insert(urMod) > 0 ? "添加成功！" : "添加失败！";
            }
            function.Script(this, "alert('" + info + "');location='UserRole.aspx';");
        }
    }
}
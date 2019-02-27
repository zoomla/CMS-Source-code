using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_I_ASCX_SPwd : System.Web.UI.UserControl
{
    /*
     * 二级密码校验
     */
    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        M_AdminInfo admin=badmin.GetAdminLogin();
        if (string.IsNullOrEmpty(admin.RandNumber.Replace(" ","")))
        {
            nospwd_div.Visible = true;
        }
        else
        {
            spwd_div.Visible = true;
        }
    }
    protected void Sure_Btn_Click(object sender, EventArgs e)
    {
        //检测二级密码,如果为空,则输入密码
        B_Admin badmin = new B_Admin();
        M_AdminInfo admin = badmin.GetAdminLogin();
        if (badmin.CheckSPwd(admin, SPwd_T.Text))
        {
            Session["Spwd"] = SPwd_T.Text;
            Response.Redirect(Request.RawUrl);
        }
        else
        {
            function.Script(this.Page,"alert('二级密码错误');");
        }

    }
}
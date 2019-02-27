using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;
using System.Data;
using ZoomLa.Common;
using ZoomLa.BLL;

public partial class Plat_Admin_UserRole : System.Web.UI.Page
{
    M_Plat_UserRole urMod = new M_Plat_UserRole();
    B_Plat_UserRole urBll = new B_Plat_UserRole();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!B_User_Plat.IsAdmin())
        {
            function.WriteErrMsg("非管理员,无权限访问该页!!");
        }
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        DataTable dt = urBll.SelByCompID(B_User_Plat.GetLogin().CompID);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del2"://网络管理员不允许删除
                int ID = Convert.ToInt32(e.CommandArgument);
                urBll.Del(ID);
                MyBind();
                break;
        }
    }
    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idChk"]))
        {
            urBll.DelByIDS(Request.Form["idChk"]);
            MyBind();
            function.Script(this, "alert('删除成功!!');");
        }
        else { function.Script(this, "alert('未选中任何成员!!');"); }
    }
    protected void EGV_RowDataBoundDataRow(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = e.Row.DataItem as DataRowView;
        if (dr["IsSuper"].ToString().Equals("1")) { (e.Row.FindControl("DelBtn") as LinkButton).Visible = false; }
    }
}
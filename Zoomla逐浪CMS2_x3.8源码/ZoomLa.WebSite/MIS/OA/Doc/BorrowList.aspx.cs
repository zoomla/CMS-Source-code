using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.MIS;
using ZoomLa.Common;

public partial class MIS_OA_Doc_BorrowList : System.Web.UI.Page
{
    B_OA_Borrow borBll = new B_OA_Borrow();
    B_Permission perBll = new B_Permission();
    //借阅列表
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!perBll.CheckAuth(new B_User().GetLogin().UserRole, "oa_pro_file")) { function.WriteErrMsg("你没有访问该页面的权限"); }
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        DataTable dt = borBll.Sel();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del2":
                int id = Convert.ToInt32(e.CommandArgument);
                borBll.Del(id);
                break;
        }
        MyBind();
    }
    public string SubStr(string str, int len = 50)
    {
        return StringHelper.SubStr(str, len);
    }
}
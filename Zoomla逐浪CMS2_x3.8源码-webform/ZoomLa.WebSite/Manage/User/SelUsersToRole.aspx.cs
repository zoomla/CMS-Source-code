using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_User_SelUsersToRoleUsers : System.Web.UI.Page
{ 
    B_Permission perBll = new B_Permission();
    B_User buser = new B_User();
    B_Structure strBll = new B_Structure();
    private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Mid < 1) { function.WriteErrMsg("未指定角色"); }
            M_Permission perMod = perBll.SelReturnModel(Mid);
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='AdminManage.aspx'>用户管理</a></li><li><a href='PermissionInfo.aspx'>角色管理</a></li><li class='active'>" + perMod.RoleName + "</li>  [<a href='javascript:;' id='selUsers'>新增会员</a>]");
        }
    }
    public void MyBind()
    {
        if (Mid > 0)
        {
            DataTable dt = buser.SelUserByRole(Mid);
            if (dt.Rows.Count < 1)
                DelBtn.Visible = false;
            EGV.DataSource = dt;
            EGV.DataBind();
        }
    }
 
    //批量删除用户
    protected void BtnDelAll_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        string[] uid = ids.Split(',');
        for (int i = 0; i < uid.Length; i++)
        {
            M_UserInfo info = buser.SeachByID(DataConverter.CLng(uid[i]));
            info.UserRole = StrHelper.RemoveToIDS(info.UserRole, Mid.ToString());
            buser.UpDateUser(info);
        }
        MyBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    //批量绑定
    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        string ids = RoleUsers_Hid.Value;
        if (string.IsNullOrEmpty(ids))
        {
            function.WriteMsgTime("未选择会员", "PermissionInfo.aspx");
        }
        else
        {
            string[] uid = ids.Split(',');
            for (int i = 0; i < uid.Length; i++)
            {
                M_UserInfo info = buser.SeachByID(DataConverter.CLng(uid[i]));
                if (!info.UserRole.Contains("," + Mid + ","))
                {
                    info.UserRole += "," + Mid + ",";
                    buser.UpDateUser(info);
                }
            }
        }
        MyBind();
    }
}
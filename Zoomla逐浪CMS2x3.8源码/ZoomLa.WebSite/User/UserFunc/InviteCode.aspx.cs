using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_UserFunc_InviteCode : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_User_Temp utBll = new B_User_Temp();
    public int maxcount = SiteConfig.UserConfig.InviteCodeCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        DataTable dt = new DataTable();
        dt = utBll.Code_Sel(buser.GetLogin().UserID);
        CodeCount_L.Text = dt.Rows.Count.ToString();
        if (!string.IsNullOrEmpty(SearchKey))
        {
            dt.DefaultView.RowFilter = "Str1 Like '%" + SearchKey + "%'";
            dt = dt.DefaultView.ToTable();
        }
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    //Str3作为状态
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del2":
                int id = Convert.ToInt32(e.CommandArgument);
                utBll.DelByUid(id.ToString(),buser.GetLogin().UserID);
                break;
        }
        MyBind();
    }
    private string SearchKey
    {
        get
        {
            return ViewState["SearchKey"] as string;
        }
        set
        {
            ViewState["SearchKey"] = value.Trim();
        }

    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        SearchKey = searchText.Text.Trim();
        MyBind();
    }
    public string CodeStatus()
    {
        string result = "";
        switch (Eval("Str3").ToString())
        {
            case "1":
                result = "<span style='color:green;'>未使用</span>";
                break;
            case "2":
                result = "<span style='color:red;'>已使用</span>";
                break;
            default:
                break;
        }
        return result;
    }
    protected void Create_Link_Click(object sender, EventArgs e)
    {
        int count = maxcount - utBll.Code_Sel(buser.GetLogin().UserID).Rows.Count;
        if (count > 0)
        {
            utBll.Code_Create(buser.GetLogin().UserID, count);
            function.Script(this, "alert('生成完成');");
            MyBind();
        }
        else
        {
            function.Script(this, "alert('生成取消 ,因为你已经有了" + maxcount + "个未使用的邀请码！');");
        }
    }
}
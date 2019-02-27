using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;

public partial class User_PubManage : System.Web.UI.Page
{
    protected B_User buser = new B_User();
    protected M_UserInfo info = new M_UserInfo();
    protected M_Uinfo binfo = new M_Uinfo();
    protected B_Pub bpub = new B_Pub();
    protected M_Pub mpub = new M_Pub();
    protected void Page_Load(object sender, EventArgs e)
    {
        EGV.txtFunc = txtPageFunc;
        if (!IsPostBack)
        {
            DataBind();
        }
    }
    private void DataBind(string key = "")
    {
        DataTable dt = bpub.Select_NotDel();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    //分页
    public void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
        {
            pageSize = EGV.PageSize;
        }
        else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
        {
            pageSize = EGV.PageSize;
        }
        EGV.PageSize = pageSize;
        EGV.PageIndex = 0;//改变后回到首页
        size = pageSize.ToString();
        DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "manage":
                mpub = bpub.SelReturnModel(DataConvert.CLng(e.CommandArgument.ToString()));
                if (mpub.PubPermissions.IndexOf("Look") == -1)
                {
                    function.WriteErrMsg("后台未赋予查看权限");
                }
                else
                    Response.Redirect("PubInfo.aspx?PubID=" + e.CommandArgument.ToString());
                break;
        }
    }
}
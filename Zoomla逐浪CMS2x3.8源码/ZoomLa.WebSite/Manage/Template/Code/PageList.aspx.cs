using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Other;
using ZoomLa.Common;
//隐式搜索,删除对应页面
public partial class Manage_Template_Code_PageList :CustomerPageAction
{
    B_Code_Page codeBll = new B_Code_Page();
    ZipClass zipBll = new ZipClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li><a href=\"PageList.aspx\">页面列表</a><a href='AddPage.aspx'>[添加页面]</a></li>");
        }
    }
    private void MyBind() 
    {
        DataTable dt = new DataTable();
        EGV.DataSource = codeBll.Sel();
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
                codeBll.Del(id);
                break;
        }
        MyBind();
    }
    protected void Down_Btn_Click(object sender, EventArgs e)
    {

    }
}
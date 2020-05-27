using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Model.Site;

public partial class Plugins_Domain_MySiteManage : System.Web.UI.Page
{
    protected B_Admin badmin = new B_Admin();
    protected B_IDC_DBList dbBll = new B_IDC_DBList();
    protected M_IDC_DBList dbMod = new M_IDC_DBList();
    protected B_User buser = new B_User();
    private int userID;
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        userID = buser.GetLogin().UserID;
        if (!IsPostBack)
        {
            DataBind();
        }
    }
    private void DataBind(string key = "")
    {
        DataTable dt = new DataTable();
        if (string.IsNullOrEmpty(key.Trim()))
            dt = dbBll.SelByUserID(userID);
        else
            dt = dbBll.SelByUserID(userID,key);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    //处理页码
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
            case "del2":
                //删除记录，同时删除目标数据库
                break;
        }
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        DataBind(searchText.Text.Trim());
    }
}
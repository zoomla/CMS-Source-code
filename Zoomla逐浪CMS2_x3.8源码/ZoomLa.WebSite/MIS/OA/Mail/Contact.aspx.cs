using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

/*
 *联系人管理查看 
 */ 
public partial class MIS_Mail_Contact : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Structure stuBll = new B_Structure();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        DataTable dt = new DataTable();
        dt = buser.SelAll();
        if (!string.IsNullOrEmpty(SearchKey))
        {
            if (function.IsNumeric(SearchKey))
            {
                dt.DefaultView.RowFilter = "WorkNum in('" + SearchKey + "')";
            }
            else
            {
                string key = "'%" + SearchKey + "%'";
                dt.DefaultView.RowFilter = "HoneyName Like " + key ;
            }
            dt = dt.DefaultView.ToTable();
        }
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    public string GetStuName()
    {
        DataTable dt= stuBll.SelByUid(DataConverter.CLng(Eval("UserID")));
        string stuName = "";
        foreach (DataRow item in dt.Rows)
        {
            stuName += item["Name"]+",";
        }
        return stuName.Trim(',');
    }
    private string SearchKey 
    {
        get
        {
            return ViewState["SearchKey"] as string;
        }
        set 
        {
            ViewState["SearchKey"] = value;
        }

    }
    //处理页码
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
                //删除记录，同时删除目标数据库
                break;
        }
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        SearchKey = searchText.Text.Trim();
        MyBind();
    }
    //-----Tool
    public string GetGN(object o) 
    {
      return string.IsNullOrEmpty(o.ToString()) ? "未设置" : o.ToString();
    }
    protected void batMsgBtn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.Form["idChk"]))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先选定收件人!!!');", true);
        }
        else
        {
            Response.Redirect("MessageSend.aspx?uid="+Request.Form["idChk"]);
        }
    }
}
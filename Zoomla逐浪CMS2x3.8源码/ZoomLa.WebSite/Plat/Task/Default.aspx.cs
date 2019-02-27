using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Plat;

public partial class Plat_Task_Default : System.Web.UI.Page
{
    B_Plat_Task taskBll = new B_Plat_Task();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string value=Request.Form["value"];
            switch (action)
            {
                case "ChangeColor":
                    {
                        int id = Convert.ToInt32(value.Split(':')[0]);
                        string color =value.Split(':')[1];
                        taskBll.ChangeColor(id,color);
                    }
                    break;
            }
            Response.Write(1); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            MyBind();
        }

    }
    private void MyBind()
    {
        DataTable dt = new DataTable();
        dt = taskBll.SelHasAuth(buser.GetLogin().UserID);
        EGV.DataSource = dt;
        EGV.DataBind();
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
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Del":
                taskBll.Del(id);
                break;
            case "Edit":
                Response.Redirect("AddTask.aspx?ID=" + id);
                break;
        }
        MyBind();
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        SearchKey = searchText.Text.Trim();
        MyBind();
    }
   
    //根据IDS获取用户名
    public string GetUserName(string ids)
    {
        string unames = buser.GetUserNameByIDS(ids);
        return unames.Length > 30 ? unames.Substring(0, 30) + "..." : unames;
    }
    protected void DelBtn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idChk"]))
        {
            taskBll.DelByIDS(Request.Form["idChk"]);
        }
        MyBind();

    }
}
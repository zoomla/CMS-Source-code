using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.SQLDAL;

public partial class Manage_Site_VirtualDirectory : System.Web.UI.Page
{
    B_Admin badmin = new B_Admin();
    IISHelper iisHelper = new IISHelper();
    string siteName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        IdentityAnalogue ia = new IdentityAnalogue();
        ia.CheckEnableSA();
        siteName = Request["siteName"];
        if (!IsPostBack)
        {
            DataBind(siteName);
        }
    }
    public void DataBind(string siteName)
    {
        DataTable dt = iisHelper.GetVDList(siteName);
        vdEGV.DataSource = dt;
        vdEGV.DataBind();
    }
    protected void vdBtn_Click(object sender, EventArgs e)//添加
    {
        string alias = vdVPath.Text.Trim();
        string ppath = vdPPath.Text.Trim();
        if (string.IsNullOrEmpty(alias) || string.IsNullOrEmpty(ppath))//not allowed to add
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('信息不完整或包含非法字符');", true);//AJAX或UpdatePanel中无用
        }
        else if (!Directory.Exists(ppath))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('目录不存在');", true);
        }
        else
        {
            iisHelper.AddVD(siteName, alias, ppath);
            DataBind(siteName);
        }
    }
    protected void vdEGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Edit2":
                vdEGV.EditIndex = Convert.ToInt32(e.CommandArgument as string);
                DataBind(siteName);
                break;
            case "Save":
                string[] s = e.CommandArgument.ToString().Split(':');
                UpdatePath(DataConvert.CLng(s[0]), s[1], (DataConvert.CLng(s[2]) - 1));
                vdEGV.EditIndex = -1;
                DataBind(siteName);
                break;
            case "Cancel2":
                vdEGV.EditIndex = -1;
                DataBind(siteName);
                break;
            default: break;
        }
    }
    protected void UpdatePath(int rowNum, string siteName, int index)//Update VD's physicalPath
    {
        GridViewRow gr = vdEGV.Rows[rowNum];
        //string vpath = ((TextBox)gr.FindControl("Path")).Text.Trim();
        string spath = ((TextBox)gr.FindControl("EditPhySicalPath")).Text.Trim();
        iisHelper.ChangePhysicalPath(siteName, spath, index);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改完成');location=location;", true);
    }
    protected void vdEGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        vdEGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
}
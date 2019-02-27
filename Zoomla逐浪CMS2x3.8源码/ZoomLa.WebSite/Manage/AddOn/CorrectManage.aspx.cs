using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;

using ZoomLa.Model;

public partial class Manage_I_Content_CorrectManage : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'><a href='"+Request.RawUrl+"'>纠错管理</a></li>");
    }
    public void DataBind(string key = "")
    {

    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }
    public string GetCTitle(string tType)
    {
        int t = DataConverter.CLng(tType);
        string re = "";
        switch (t)
        {
            case 0:
                re = "内容错误";
                break;
            case 1:
                re = "错别字";
                break;
            case 2:
                re = "图片错误";
                break;
            case 3:
                re = "链接错误";
                break;
        }
        return re;
    }
    protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
       
    }
    private string[] GetChecked()
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            string[] chkArr = Request.Form["idchk"].Split(',');
            return chkArr;
        }
        else
            return null;
    }
}
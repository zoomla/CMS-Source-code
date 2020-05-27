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

public partial class Manage_I_Content_AuthorManage : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string AUId = "",AuthorName="";
        EGV.txtFunc = txtPageFunc;
        if (!Page.IsPostBack)
        {
            AuthorName = Request.QueryString["AuthorName"];
            MyBind(Request.QueryString["key"]);

            if (Request.QueryString["AUId"] != null)
            {
                AUId = Request.QueryString["AUId"].Trim();
                DeleteSource(AUId);
            }
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'><a href='"+Request.RawUrl+"'>作者管理</a></li>");
    }
    public void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))
        {
            pageSize = EGV.PageSize;
        }
        else if (pageSize < 1)
        {
            pageSize = EGV.PageSize;
        }
        EGV.PageSize = pageSize;
        EGV.PageIndex = 0;
        size = pageSize.ToString();
        MyBind();
    }
    private void DeleteSource(string SId)
    {
        B_Author bauthor = new B_Author();
        if (bauthor.DeleteByID(SId))
        {
            HttpContext.Current.Response.Write("<script language=javascript> alert('删除成功！');window.document.location.href='AuthorManage.aspx';</script>");
        }
    }
    public void MyBind(string key="")
    {
        B_Author bauthor=new B_Author();
        if (string.IsNullOrEmpty(key))
        {
            EGV.DataSource = bauthor.GetSourceAll();
        }
        else
        {
            EGV.DataSource = bauthor.GetAuthorByName(key);
        }
        EGV.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        this.MyBind();
    }  
    protected void btndelete_Click(object sender, EventArgs e)
    {
        B_Author bauthor = new B_Author();
        int flag = 0; string sid = "";
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            for (int i = 0; i < chkArr.Length; i++)
            {
                sid = chkArr[i];
                if (bauthor.DeleteByID(sid))
                    flag++;
            }
        }
        if (flag > 0)
        {
            Response.Write("<script language=javascript> alert('批量删除成功！');window.document.location.href='AuthorManage.aspx';</script>");
        }    
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

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

public partial class manage_Plus_AuthorManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string AUId = "";
        if (!Page.IsPostBack)
        {
            MyBind();
            if (Request.QueryString["AUId"] != null)
            {
                AUId = Request.QueryString["AUId"].Trim();
                DeleteSource(AUId);
            }

        }
    }
    private void DeleteSource(string SId)
    {
        B_Author bauthor = new B_Author();
        if (bauthor.DeleteByID(SId))
        {
            HttpContext.Current.Response.Write("<script language=javascript> alert('删除成功！');window.document.location.href='AuthorManage.aspx';</script>");
        }
    }
    public void MyBind()
    {
        B_Author bauthor=new B_Author();
        GridView1.DataSource = bauthor.GetSourceAll();// GetAuthorPage(0, 0, 10);
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.MyBind();
    }  
    protected void btndelete_Click(object sender, EventArgs e)
    {
        B_Author bauthor = new B_Author();
        int i = 0, flag = 0; string sid = "";     
        for (i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("SelectCheckBox")).Checked)
            {       
                sid = GridView1.DataKeys[GridView1.Rows[i].RowIndex].Value.ToString();
                if (bauthor.DeleteByID(sid))
                    flag++;
               
            }
        }
        // GridView1.DataKeys[GridView1.Rows[i].RowIndex]//而不是.DataItemIndex
        if (flag > 0)
        {
            Response.Write("<script language=javascript> alert('批量删除成功！');window.document.location.href='AuthorManage.aspx';</script>");

        }        
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("SelectCheckBox");
            cbox.Checked = this.CheckBox1.Checked;
        }
    }
}

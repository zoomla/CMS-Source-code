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
public partial class manage_Plus_KeyWordManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string KWId = "";
        if (!Page.IsPostBack)
        {
            MyBind();
            if (Request.QueryString["KWId"] != null)
            {
                KWId = Request.QueryString["KWId"].Trim();
                DeleteKeyWords(KWId);
            }
        }
    }
    private void DeleteKeyWords(string KWId)
    {
        B_KeyWord bkeywords = new B_KeyWord();
        if (bkeywords.DeleteByID(KWId))
        {
            HttpContext.Current.Response.Write("<script language=javascript> alert('删除成功！');window.document.location.href='KeyWordManage.aspx';</script>");
        }
    }
    public void MyBind()
    {
        B_KeyWord bkeyword = new B_KeyWord();
        GridView1.DataSource = bkeyword.GetKeyWordAll();// GetAuthorPage(0, 0, 10);
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.MyBind();
    }     
    protected void btndelete_Click(object sender, EventArgs e)
    {
        B_KeyWord bkeyword =new B_KeyWord();
        int i = 0, flag = 0; string sid = "";
        for (i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("SelectCheckBox")).Checked)
            {
                sid = GridView1.DataKeys[GridView1.Rows[i].RowIndex].Value.ToString();
                if (bkeyword.DeleteByID(sid))
                    flag++;

            }
        }      
        if (flag > 0)
        {
            Response.Write("<script language=javascript> alert('批量删除成功！');window.document.location.href='KeyWordManage.aspx';</script>");

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

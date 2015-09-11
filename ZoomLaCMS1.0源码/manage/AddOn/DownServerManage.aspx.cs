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
public partial class manage_Plus_DownServerManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string SId = "";
        if (!Page.IsPostBack)
        {
            MyBind();
            if (Request.QueryString["SId"] != null)
            {
                SId = Request.QueryString["SId"].Trim();
                DeleteKeyWords(SId);
            }
        }
      
    }
    private void DeleteKeyWords(string SId)
    {
        B_DownServer bdownserver = new B_DownServer();       
        if (bdownserver.DeleteByID(SId))
        {
            HttpContext.Current.Response.Write("<script language=javascript> alert('删除成功！');window.document.location.href='DownServerManage.aspx';</script>");
        }
    }
    public void MyBind()
    {
        B_DownServer bdownserver = new B_DownServer();
        GridView1.DataSource = bdownserver.GetDownServerAll();// GetAuthorPage(0, 0, 10);
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.MyBind();
    }  
    protected void btndelete_Click(object sender, EventArgs e)
    {
        B_DownServer bdownserver = new B_DownServer();
        int i = 0, flag = 0; string sid = "";
        //int f = GridView1.Rows.Count;//测试用OnClientClick="return confirm('确定要删除选中的服务器吗？')"
        for (i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox cbox=(CheckBox)GridView1.Rows[i].Cells[0].FindControl("SelectCheckBox");
            bool check = cbox.Checked;
            if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("SelectCheckBox")).Checked)//check
            {
                sid = GridView1.DataKeys[GridView1.Rows[i].RowIndex].Value.ToString();
                if (bdownserver.DeleteByID(sid))
                    flag++;
            }
        }
        if (flag > 0)
        {
            Response.Write("<script language=javascript> alert('批量删除成功！');window.document.location.href='DownServerManage.aspx';</script>");
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

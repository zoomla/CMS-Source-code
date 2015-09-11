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
public partial class manage_Plus_SourceManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string SId ="";
        if (!Page.IsPostBack)
        {
            MyBind();
            if (Request.QueryString["SId"]!=null)
            {
                SId = Request.QueryString["SId"].Trim();
                DeleteSource(SId);
            }
        }
    }
    private void DeleteSource(string SId)
    {
        B_Source bsource = new B_Source();
        if (bsource.DeleteByID(SId))
        {
            HttpContext.Current.Response.Write("<script language=javascript> alert('删除成功！');window.document.location.href='SourceManage.aspx';</script>");
        }
    }
    public void MyBind()
    {
        B_Source bsource = new B_Source();
        GridView1.DataSource =bsource.GetSourceAll();// GetAuthorPage(0, 0, 10);
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.MyBind();
    }
    /// <summary>
    /// 根据来源名搜索,好像没用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void Search_Click(object sender, EventArgs e)
    //{
    //    string strkey = sourcekey.Text.ToString();
    //     B_Source bsource = new B_Source();
    //     GridView1.DataSource =bsource.SearchSource(strkey);
    //     GridView1.DataBind();
    //}
    protected void btndelete_Click(object sender, EventArgs e)
    {       
        int i = 0,flag=0;string sid ="";
        for (i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("SelectCheckBox")).Checked)
            {
                flag++;
                sid= GridView1.DataKeys[GridView1.Rows[i].RowIndex].Value.ToString();
                 DeleteSource(sid);
                //logid = int.Parse(((Label)GridView1.Rows[i].Cells[0].FindControl("Label1")).Text);         
            }
        } 
      
        if (flag > 0)
        {            
            Response.Write("<script language=javascript> alert('批量删除成功！');window.document.location.href='SourceManage.aspx';</script>");

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

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
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class manage_AddOn_ProjectRequireList : System.Web.UI.Page
{
    private B_ClientRequire bll = new B_ClientRequire();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            Bind();
        }
    }
    private void Bind()
    {
        B_ClientRequire bll = new B_ClientRequire();
        DataView dv = bll.GetClientRequireAll();
        this.Egv.DataSource = dv;
        this.Egv.DataKeyNames = new string[] { "RequireID" };
        this.Egv.DataBind();
    }
    //绑定分页
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        Bind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {      
        switch (e.CommandName)
        {
            case "CreateProject":
                Response.Redirect("AddProject.aspx?rid=" + Convert.ToInt32(e.CommandArgument) + "");
                break; 
            case"DeleteRequest":
                string Id = e.CommandArgument.ToString();
                this.bll.DeleteByRID(DataConverter.CLng(Id));
                Bind();
                break;
        }
    }   
    protected void cbAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i <= Egv.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
            if (cbAll.Checked == true)
            {
                cbox.Checked = true;
            }
            else
            {
                cbox.Checked = false;
            }
        }
    }
    //批量删除
    protected void btnDel_Click(object sender, EventArgs e)
    {
        B_ClientRequire bll = new B_ClientRequire();
        for (int i = 0; i <= Egv.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
            if (cbox.Checked == true)
            {
                btnDel.Attributes.Add("OnClientClick", "return confirm('你确定要删除吗？');");
                bll.DeleteByRID(DataConverter.CLng(Egv.DataKeys[i].Value));
            }
        }
        Bind();
    }
    public int CountProjectNumByRid(int rid)
    {
        B_Project bproject = new B_Project();
        return bproject.CountProjectNumByRid(rid);
    }
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

public partial class manage_AddOn_ProjectAudit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        Call.SetBreadCrumb(Master,"<li>后台管理</li><li>CRM应用</li><li>项目管理</li><li>审核项目</li>");
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            //dt = bps.GetNoAudit();
            if (dt!=null)
                AuditBind(dt);
            if (dt != null)
            {
                dt.Dispose();
            }
        }
    }
    protected void AuditBind(DataTable dt)
    {
        dt.DefaultView.Sort = "id desc";
        Page_list(dt);
    }

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        if (Cll != null)
        {
            RPT.DataSource = Cll;
            RPT.DataBind();
        }
    }
    #endregion


    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ritem in RPT.Items)
        {
            CheckBox cb = ritem.FindControl("ChBx") as CheckBox;
            if (cb.Checked)
            {
                Label lbl = ritem.FindControl("Label1") as Label;
                int id = Convert.ToInt32(lbl.Text.ToString());
               // bps.DeleteByGroupID(id);
            }
        }
        Response.Write("<script language=javascript>alert('批量删除成功!');location.href='ProjectAudit.aspx'</script>");
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Del":
                //bps.DeleteByGroupID(DataConverter.CLng(e.CommandArgument));
                Response.Redirect("ProjectAudit.aspx");
                break;
            case "Audit":
                //bps.AuditByID(DataConverter.CLng(e.CommandArgument));
                Response.Redirect("ProjectAudit.aspx");
                break;
            default:
                break;
        }
    }
    protected void BtnAudit_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ritem in RPT.Items)
        {
            CheckBox cb = ritem.FindControl("ChBx") as CheckBox;
            if (cb.Checked)
            {
                Label lbl = ritem.FindControl("Label1") as Label;
                int id = Convert.ToInt32(lbl.Text.ToString());
               // bps.AuditByID(id);
            }
        }
        Response.Redirect("ProjectAudit.aspx");
    }
}

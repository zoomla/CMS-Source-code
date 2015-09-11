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

public partial class manage_Content_SpecList : System.Web.UI.Page
{
    private int m_CateID;
    protected B_Spec bll = new B_Spec();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("SpecManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (string.IsNullOrEmpty(base.Request.QueryString["CateID"]))
            {
                function.WriteErrMsg("没有指定专题类别ID");
            }
            else
            {
                this.m_CateID = DataConverter.CLng(base.Request.QueryString["CateID"]);
            }
            //绑定数据
            bind();
        }
    }
    /// <summary>
    /// 绑定数据
    /// </summary>
    private void bind()
    {
        this.GV.DataSource = this.bll.GetSpecList(this.m_CateID);
        this.GV.DataKeyNames = new string[] { "SpecID" };
        this.GV.DataBind();
    }
    protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
            Page.Response.Redirect("AddSpec.aspx?Action=Modify&SpecID=" + e.CommandArgument.ToString());
        if (e.CommandName == "Delete")
        {
            this.bll.DelSpec(DataConverter.CLng(e.CommandArgument.ToString()));
            bind();
        }
    }
    protected void GV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GV.PageIndex = e.NewPageIndex;
        this.bind();
    }
    public string GetOpenType(string otype)
    {
        string re = "";
        if (DataConverter.CBool(otype))
            re = "新窗口";
        else
            re = "原窗口";
        return re;
    }
}

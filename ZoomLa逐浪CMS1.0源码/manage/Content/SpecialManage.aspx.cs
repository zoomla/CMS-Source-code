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
namespace ZoomLa.WebSite.Manage
{
    public partial class SpecialManage : System.Web.UI.Page
    {
        protected B_SpecCate bll = new B_SpecCate();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("SpecCateManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
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
            this.GV.DataSource = this.bll.GetCateLidt().DefaultView;
            //this.GV.DataKeyNames = new string[] { "SpecCateID" };
            this.GV.DataBind();
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddSpec")
                Response.Redirect("AddSpec.aspx?Action=Add&CateID=" + e.CommandArgument.ToString());
            if (e.CommandName == "SpecList")
                Response.Redirect("SpecList.aspx?CateID=" + e.CommandArgument.ToString());
            if (e.CommandName == "Modify")
                Page.Response.Redirect("AddSpecCate.aspx?id=" + e.CommandArgument.ToString());
            if (e.CommandName == "Delete")
            {
                this.bll.DelCate(DataConverter.CLng(e.CommandArgument.ToString()));
                bind();
            }
        }
        protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.GV.EditIndex = -1;
        }
        /// <summary>
        /// 获取选中记录，并绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GV.EditIndex = e.NewEditIndex;
            //this.HdnRoleId.Value = (Egv.DataKeys[e.NewEditIndex].Value).ToString();
            bind();
        }
        /// <summary>
        /// 绑定的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GV_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";
            }
        }
        protected void GV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GV.PageIndex = e.NewPageIndex;
            this.bind();
        }
    }
}
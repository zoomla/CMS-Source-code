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
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Web;
using ZoomLa.Common;
using ZoomLa.BLL;


namespace ZoomLaManage.WebSite.Manage.User
{
    public partial class AdminManage : System.Web.UI.Page
    {

        M_AdminInfo manager = new M_AdminInfo();
        /// <summary>
        /// 页面初次加载,绑定数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("AdminManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                //绑定数据
                bind();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 变量说明：flag判断记录是否选种，chkCount选种记录数量
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ModifyAdmin")
                Page.Response.Redirect("AddManage.aspx?id=" + e.CommandArgument.ToString());
            if (e.CommandName == "DeleteAdmin")
            {
                B_Admin.DelAdminById(DataConverter.CLng(e.CommandArgument.ToString()));
                bind();
            }
        }
        /// <summary>
        /// 全部选择控件设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (CheckBox2.Checked == true)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    int adminID = Convert.ToInt32(Egv.DataKeys[i].Value);
                    B_Admin.DelAdminById(adminID);
                }
            }
            bind();
        }
        /// <summary>
        /// 取消选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            CheckBox2.Checked = false;
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                cbox.Checked = false;
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void bind()
        {
            Egv.DataSource = B_Admin.GetAdminInfo();
            Egv.DataKeyNames = new string[] { "AdminID" };
            Egv.DataBind();
        }
        /// <summary>
        /// 取消修改状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LnkCancel_Click(object sender, EventArgs e)
        {
            this.Egv.EditIndex = -1;
            bind();
        }
        /// <summary>
        /// 获取选中记录，并绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Egv.EditIndex = e.NewEditIndex;
            //this.HdnRoleId.Value = (Egv.DataKeys[e.NewEditIndex].Value).ToString();
            bind();
        }
        /// <summary>
        /// 绑定的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Display the company name in italics.
                e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";

            }
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            this.bind();
        }


    }
}
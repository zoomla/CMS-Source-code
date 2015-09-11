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
using ZoomLa.Model;
namespace ZoomLa.WebSite.Manage.AddOn
{


    public partial class ADManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                RepNodeBind();
            }
        }
        private void RepNodeBind()
        {
            DataTable da = B_Advertisement.GetAllAdvertisementList();
            if (da.Rows.Count > 0)
            {
                this.nocontent.Style["display"] = "none";
                this.GridView1.DataSource = da;
                this.GridView1.DataKeyNames = new string[] { "ADID" };
                this.GridView1.DataBind();
                this.GridView1.Visible = true;
            }
            else
            {
                this.nocontent.Style["display"] = "";
                this.GridView1.Visible = false;
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndelete_Click(object sender, EventArgs e)
        {
            string str = "";            
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("chkSel");
                if (cbox.Checked)
                {
                    str = GridView1.DataKeys[i].Value.ToString();
                    B_Advertisement.Advertisement_Delete(str);
                }
            }
            this.RepNodeBind();
        }
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsetpassed_Click(object sender, EventArgs e)
        {
            string str = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("chkSel");
                if (cbox.Checked)
                {                    
                    str = GridView1.DataKeys[i].Value.ToString();
                    B_Advertisement.Advertisement_SetPassed(str);                     
                }
            }
            this.RepNodeBind();
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
                Page.Response.Redirect("Advertisement.aspx?ADId=" + e.CommandArgument.ToString());
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                if (B_Advertisement.Advertisement_Delete(Id))
                    Response.Write("<script>alert('删除成功！')</script>");
                this.RepNodeBind();
            }           
            if (e.CommandName == "Copy")
            {
                string Id = e.CommandArgument.ToString();
                if (B_Advertisement.Advertisement_Copy(DataConverter.CLng(Id)))
                    Response.Write("<script>alert('复制成功！')</script>");
                this.RepNodeBind();
            }            
            if (e.CommandName == "Pass")
            {
                string Id = e.CommandArgument.ToString();
                if (!B_Advertisement.Advertisement_GetAdvertisementByid(DataConverter.CLng(Id)).Passed)
                    B_Advertisement.Advertisement_SetPassed(Id);
                else
                    B_Advertisement.Advertisement_CancelPassed(Id);
                this.RepNodeBind();
            }
        }

        public string GetADType(string typeid)
        {
            switch (DataConverter.CLng(typeid))
            {
                case 1:
                    return "图片";
                case 2:
                    return "动画";
                case 3:
                    return "文本";
                case 4:
                    return "代码";
                case 5:
                    return "页面";
            }
            return "";
        }
        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.RepNodeBind();
        }
        /// <summary>
        /// 批量取消审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancelpassed_Click(object sender, EventArgs e)
        {
            string str = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("chkSel");
                if (cbox.Checked)
                {
                    str = GridView1.DataKeys[i].Value.ToString();
                    B_Advertisement.Advertisement_CancelPassed(str);
                }
            }
            this.RepNodeBind();
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("chkSel");
                if (CheckSelectAll.Checked == true)
                {
                    cbox.Checked = true;
                }
                else
                {
                    cbox.Checked = false;
                }
            }
        }
    }
}

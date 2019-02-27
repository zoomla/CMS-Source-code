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
using ZoomLa.Components;
namespace ZoomLa.WebSite.User
{
    public partial class Message : System.Web.UI.Page
    {
        protected B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            buser.CheckIsLogin();
            this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }
        private void Bind()
        {
            string UserName = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
            this.GridView1.DataSource = B_Message.SeachByUserName(UserName).DefaultView;
            GridView1.DataKeyNames = new string[] { "MsgID" };
            this.GridView1.DataBind();
        }
        //绑定分页
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Bind();
        }
        /// <summary>
        /// 获取选中记录，并绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            //this.HdnRoleId.Value = (GridView1.DataKeys[e.NewEditIndex].Value).ToString();
            Bind();
        }
        /// <summary>
        /// 全部选择控件设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("cheCk");
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
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("cheCk");
                if (cbox.Checked == true)
                {
                    int MsgID = Convert.ToInt32(GridView1.DataKeys[i].Value);
                    B_Message.DelteById(MsgID);
                }
            }
            Bind();
        }
        /// <summary>
        /// 取消选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            CheckBox2.Checked = false;
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("cheCk");
                cbox.Checked = false;
            }
        }
        protected void Row_Command(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteMsg")
            {
                int MsgID = DataConverter.CLng(e.CommandArgument.ToString());
                B_Message.DelteById(MsgID);
                Bind();
            }
            if (e.CommandName == "ReadMsg")
            {
                Response.Redirect("MessageRead.aspx?id=" + e.CommandArgument.ToString());
            }
        }
    }
}
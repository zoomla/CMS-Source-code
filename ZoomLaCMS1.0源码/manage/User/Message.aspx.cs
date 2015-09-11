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
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace User
{
    public partial class Message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("MessManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                Bind();
            }
        }
        private void Bind()
        {
            this.GridView1.DataSource = B_Message.GetMessAll();//这样取得数据库中的数据，似乎写的不对
            GridView1.DataKeyNames = new string[] { "MsgID" };
            this.GridView1.DataBind();
        }
        //绑定分页
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Bind();
        }
        //删除多少日前的已读短消息
        protected void BtnDelDate_Click(object sender, EventArgs e)
        {
            B_Message.DeleteByDate(DataConverter.CLng(this.DropDelDate.SelectedValue));
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
        //英文状态下的逗号将用户名隔开实现多会员同时删除
        protected void BtnDelSender_Click(object sender, EventArgs e)
        {
            string incept = this.TxtSender.Text;
            if (!string.IsNullOrEmpty(incept))
            {
                string[] inarr = incept.Split(new char[] { ',' });
                for (int i = 0; i < inarr.Length; i++)
                {
                    B_Message.DeleteByUser(inarr[i]);
                }
            }
            Bind();
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
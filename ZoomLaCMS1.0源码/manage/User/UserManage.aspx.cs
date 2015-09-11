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
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;

namespace User
{
    public partial class UserManage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("UserManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                Bind();
            }
        }
        private void Bind()
        {
            B_User bll = new B_User();
            DataView dv = bll.GetUserInfo();
            this.Egv.DataSource = dv;
            this.Egv.DataKeyNames = new string[] { "UserID" };
            this.Egv.DataBind();
        }
        //绑定分页
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            Bind();
        }
        //批量认证
        protected void Button1_Click(object sender, EventArgs e)
        {
            B_User bll = new B_User();
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    //判断管理员＼邮件认证？
                    bll.UpUserUnLock(DataConverter.CLng(this.Egv.DataKeys[i].Value));
                }
            }
            Bind();
        }
        //删除会员
        protected void delete_Click(object sender, EventArgs e)
        {
            B_User bll = new B_User();
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked)
                {
                    bll.DelUserById(DataConverter.CLng(Egv.DataKeys[i].Value));
                }
            }
            Bind();
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            
                if (e.CommandName == "ChgPsw")
                    Page.Response.Redirect("UserPassModify.aspx?UserID=" + e.CommandArgument.ToString());
                if (e.CommandName == "Del")
                {
                    B_User bll = new B_User();
                    string Id = e.CommandArgument.ToString();
                    bll.DelUserById(DataConverter.CLng(Id));
                    Bind();
                }
            
        }
        //选中状态处理：
        //全部选中
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
        //锁定会员
        protected void btnLock_Click(object sender, EventArgs e)
        {
            B_User bll = new B_User();
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    bll.UpUserLock(Convert.ToInt32(Egv.DataKeys[i].Value), DataConverter.CDate(DateTime.Now.ToLongDateString()));
                    //将表中状态该为锁定．
                }
            }
            Bind();
        }
        //批量删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            B_User bll = new B_User();
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    btnDel.Attributes.Add("OnClientClick", "return confirm('你确定要删除吗？');");
                    bll.DelUserById(DataConverter.CLng(Egv.DataKeys[i].Value));
                }
            }
            Bind();
        }
        //置为正常
        protected void btnNormal_Click(object sender, EventArgs e)
        {
            B_User bll = new B_User();
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    bll.UpUserUnLock(DataConverter.CLng(Egv.DataKeys[i].Value));
                }
            }
            Bind();
        }
        /// <summary>
        /// 获取选中记录，并绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Egv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Egv.EditIndex = e.NewEditIndex;
            Bind();
        }
    }
}

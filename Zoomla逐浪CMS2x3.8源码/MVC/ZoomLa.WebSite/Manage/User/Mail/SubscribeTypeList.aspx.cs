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

namespace ZoomLaCMS.Manage.User.Mail
{
    public partial class SubscribeTypeList : CustomerPageAction
    {
        M_Subscribe ms = new M_Subscribe();
        B_Subscribe bs = new B_Subscribe();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            if (!IsPostBack)
            {
                Bind();
            }

            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>附件管理</li><li>邮件订阅</li><li>订阅管理</li>");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            ms.SubscribeName = txtTepy.Text;
            bs.GetInsert(ms);
            Response.Write("<script>location.href='SubscribeTypeList.aspx'</script>");
        }
        //分页绑定数据
        private void Bind()
        {
            DataTable dt = bs.Select_All();
            this.EGV.DataSource = dt;
            this.EGV.DataBind();
        }


        protected string GetCode(string str)
        {
            return "";
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bs.GetDelete(int.Parse(EGV.DataKeys[e.RowIndex].Value.ToString()));
            Bind();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EGV.EditIndex = -1;
            Bind();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ms.SubscribeName = ((TextBox)EGV.Rows[e.RowIndex].FindControl("TextBox1")).Text;
            ms.ID = int.Parse(EGV.DataKeys[e.RowIndex].Value.ToString());
            bs.GetUpdate(ms);
            EGV.EditIndex = -1;
            Bind();
        }
    }
}
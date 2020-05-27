using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Web.UI.WebControls;
using ZoomLa.Common;
namespace ZoomLaCMS.Manage.Iplook
{
    public partial class AlterClass : CustomerPageAction
    {
        B_IPOperation b_IPOperation = new B_IPOperation();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            if (!IsPostBack)
            {
                DataTable datatable = b_IPOperation.searchAllClass();
                leadto_ID.DataSource = datatable;
                leadto_ID.DataBind();
                leadto_ID.Items.Insert(0, new ListItem("最高分类", "0"));
                M_IP_class m_ip_class = new M_IP_class();
                if (Request.QueryString["class_ID"] != null)
                {
                    int n_class_ID = Convert.ToInt32(Request.QueryString["class_ID"].ToString());
                    m_ip_class = b_IPOperation.searchClass(n_class_ID);
                }
                class_ID.Text = m_ip_class.class_ID.ToString();
                class_name.Text = m_ip_class.class_name;
                leadto_ID.SelectedValue = m_ip_class.leadto_ID.ToString();
            }
        }

        protected void EBtnModify_Click(object sender, EventArgs e)
        {
            b_IPOperation.updateClass(Convert.ToInt32(class_ID.Text), Convert.ToInt32(leadto_ID.SelectedValue), class_name.Text);
            function.WriteSuccessMsg("修改成功!", "IPManage.aspx");
        }
    }
}
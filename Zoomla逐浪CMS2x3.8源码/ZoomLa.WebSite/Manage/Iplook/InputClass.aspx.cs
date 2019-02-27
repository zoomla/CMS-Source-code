using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;


public partial class InputClass : CustomerPageAction
{
    string IP_ID = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["leadto_IP_ID"] != null)
            {
                IP_ID = Request.QueryString["leadto_IP_ID"].ToString();
            }
            B_IPOperation b_ipoperation = new B_IPOperation();
            DataTable datatable = b_ipoperation.searchIP_pro_Class();
            leadto_ID.DataSource = datatable;
            leadto_ID.DataBind();
            leadto_ID.Items.Insert(0, new ListItem("最高所属分类", "0"));
            leadto_ID.SelectedValue = IP_ID;
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='IPManage.aspx'>其他功能</a></li><li><a href='InputClass.aspx'>添加分类</a></li>");
        }
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        B_IPOperation b_IPOperation = new B_IPOperation();
        b_IPOperation.insertClass(class_name.Text, Convert.ToInt32(leadto_ID.SelectedValue));
        function.WriteSuccessMsg("添加分类成功!", "IPManage.aspx");
    }
    protected void submit0_Click(object sender, EventArgs e)
    {
        Response.Redirect("IPManage.aspx");
    }
}
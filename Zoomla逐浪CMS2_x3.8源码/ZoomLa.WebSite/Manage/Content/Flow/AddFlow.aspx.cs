namespace ZoomLa.WebSite.Manage.Content
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;

    public partial class AddFlow : CustomerPageAction
    {
        B_Flow bf = new B_Flow();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
 
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='FlowManager.aspx'>流程管理</a></li><li class='active'>添加流程</li>");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = this.txtName.Value.Trim();
            string depict = this.txtFlowDepict.Value.Trim();
            if (name != "" && !"".Equals(depict))
            {
                if (bf.AddFlow(name, depict))
                {
                    Response.Write("<script>parent.document.frames('left').location.reload();</script>");
                    function.WriteSuccessMsg("流程添加成功！", customPath2 + "Content/FlowManager.aspx");
                }
            }
        }
    }
}
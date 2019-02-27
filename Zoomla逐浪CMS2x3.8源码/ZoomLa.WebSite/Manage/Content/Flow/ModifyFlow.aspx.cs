namespace ZoomLa.WebSite.Manage.Content
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.BLL;

    public partial class ModifyFlow : CustomerPageAction
    {
        B_Flow bf = new B_Flow();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li>系统设置</li><li><a href='FlowManager.aspx'>流程管理</a></li><li>修改审核状态</a></li>");
                int ids = Convert.ToInt32(Request.QueryString["id"]);
                BindDatas(ids);
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='FlowManager.aspx'>流程管理</a></li><li class='active'>修改流程</li>");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            string id = Request.QueryString["id"];
            string name = this.txtName.Value.Trim();
            string depict = this.txtFlowDepict.Value.Trim();
            if (name != "" && !"".Equals(depict))
            {
                if (id != null && !"".Equals(id))
                {
                    M_Flow flow = new M_Flow();
                    flow.Id = Convert.ToInt32(id);
                    flow.FlowName = name;
                    flow.FlowDepict = depict;
                    if (bf.ModifyFlowById(flow))
                    {
                        function.WriteSuccessMsg("流程修改成功！", customPath2 + "Content/FlowManager.aspx");
                    }
                }
            }
        }
        private void BindDatas(int id)
        {
            M_Flow mf = bf.GetFlowById(Convert.ToInt32(id));
            this.txtName.Value = mf.FlowName.ToString();
            this.txtFlowDepict.Value = mf.FlowDepict.ToString();
        }
    }
}
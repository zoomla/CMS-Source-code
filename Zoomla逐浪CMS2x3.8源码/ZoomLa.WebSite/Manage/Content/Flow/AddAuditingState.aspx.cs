namespace ZoomLa.WebSite.Manage.Content
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using ZoomLa.Common;

    public partial class AddAuditingState : CustomerPageAction
    {
        B_AuditingState bas = new B_AuditingState();
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='AuditingState.aspx'>状态码管理</a></li><li class='active'>添加状态码</li>");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string stateCode = this.ddlStateCode.SelectedValue.Trim();          
            string stateName = this.stateName.Value.Trim();
            if (stateName != null && !"".Equals(stateName))
            {
                M_AuditingState mas = new M_AuditingState();
                mas.StateCode = Convert.ToInt32(stateCode);
                mas.StateName = stateName;
                mas.StateType = "自定义";
                if (bas.AddAudtingState(mas))
                {
                    function.WriteSuccessMsg("保存稿件状态码成功！", CustomerPageAction.customPath2+"Content/AddAuditingState.aspx");
                }
            }
        }
    }
}
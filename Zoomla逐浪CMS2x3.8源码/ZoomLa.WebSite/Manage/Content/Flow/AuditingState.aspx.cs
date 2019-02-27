namespace ZoomLa.WebSite.Manage.Content
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using System.Drawing;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;

    public partial class AuditingState : CustomerPageAction
    {
        string StateName, StateType, StateCode;
        B_AuditingState asb = new B_AuditingState();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li class='active'>状态编码管理<a href='AddAuditingState.aspx'>[添加状态编码]</a></li>");
        }
        public string GetStateName(string stateCode, string stateName)
        {
            if (stateName != null && !"".Equals(stateName))
            {
                return stateName;
            }
            else
            {
                int s = Convert.ToInt32(stateCode);
                switch (s)
                {
                    case 0:  return "待审核";
                    case 99: return "已审核";
                    case -2: return "回收站";
                    default: return "退档";
                }
            }
        }
        public string GetStateType(string stateType)
        {
            if (stateType != "系统" && !"系统".Equals(stateType))
            {
                return stateType;
            }
            else
            {
                return "<label style='color:Green' id='xt'>系统</label>";
            }
        }
        public bool IsEnabled(string stateType)
        {
            if (stateType != "系统" && !"系统".Equals(stateType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void gvAuditingState_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int id = DataConvert.CLng(e.CommandArgument as string);
                if (asb.DelBystateCode(id))
                {
                    function.WriteSuccessMsg("删除成功!");
                }
            }
            if (e.CommandName == "Update")
            {
                string[] s = e.CommandArgument.ToString().Split(':');
                StateCode=s[1];
                GridViewRow gr = EGV.Rows[DataConvert.CLng(s[0])];
                StateName = ((TextBox)gr.FindControl("TextBox2")).Text.Trim();
                StateType = ((TextBox)gr.FindControl("TextBox1")).Text.Trim();
                asb.Update(StateName, StateType, StateCode);
            }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
    }
}
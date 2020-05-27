namespace ZoomLaCMS.Manage.Guest
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using System.Data;
    using ZoomLa.SQLDAL;
    using ZoomLa.Common;
    public partial class WdAlter : System.Web.UI.Page
    {
        protected B_User b_User = new B_User();//基本用户BLl
        protected M_UserInfo m_UserInfo = new M_UserInfo();
        protected B_Ask b_Ask = new B_Ask();//问题BLL
        protected M_Ask m_Ask = new M_Ask();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["ID"]))
                {
                    int id = Convert.ToInt32(Request["ID"]);
                    m_Ask = b_Ask.SelReturnModel(id);
                }
                txtContent.Text = m_Ask.Qcontent;
                txtSupplyment.Text = m_Ask.Supplyment;
                ddlCate.SelectedItem.Text = m_Ask.QueType;
                ddlScore.SelectedItem.Text = (m_Ask.Score).ToString();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='WdCheck.aspx'>百科问答</a></li><li class='active'>修改问题</li>");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request["ID"]);
            m_Ask = b_Ask.SelReturnModel(id);
            // throw new Exception(this.txtSupplyment.Text);
            m_Ask.Qcontent = this.txtContent.Text;
            m_Ask.Supplyment = this.txtSupplyment.Text;
            m_Ask.QueType = ddlCate.SelectedItem.Text;
            m_Ask.Score = Convert.ToInt32(ddlScore.SelectedItem.Text);
            m_Ask.ID = Convert.ToInt32(Request["ID"]);
            // throw new Exception(this.txtContent.Text);
            //throw new Exception(m_Ask.Supplyment);
            // throw new Exception(this.txtSupplyment.Text);
            b_Ask.UpdateByID(m_Ask);
            function.WriteSuccessMsg("修改成功!", "WdCheck.aspx?");
        }
        protected void Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("WdCheck.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class manage_Config_AddDevelop : CustomerPageAction
{
    protected B_Plan bll = new B_Plan();
    M_Plan info = new M_Plan();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRule();
            BindData();
            int id = Convert.ToInt32(base.Request.QueryString["DevID"]);
            if (id != 0)
            {
                info = bll.SelReturnModel(id);
                this.TxtPlanName.Text = info.PlanName;
                this.TxtPlanRule.SelectedValue = info.PlanRule;
                this.DR_Data.SelectedValue = info.DataSet;
                this.TxtExecutionTime.Text = Convert.ToString(info.ExecutionTime);
                this.TxtDescription.Text = info.Description;
            }
            Call.SetBreadCrumb(Master, "<li>扩展功能</li><li><a href='RunSql.aspx'>开发中心</a></li><li>快速建表</li>");
        }
    }

    protected void BindRule()
    {
        this.TxtPlanRule.Items.Insert(0, "手动执行");
        this.TxtPlanRule.Items[0].Value = "0";
        this.TxtPlanRule.Items.Insert(1, "每周执行一次");
        this.TxtPlanRule.Items[1].Value = "1";
        this.TxtPlanRule.Items.Insert(2, "每月执行一次");
        this.TxtPlanRule.Items[2].Value = "2";
    }
    protected void BindData()
    {
        this.DR_Data.Items.Insert(0,"主数据库");
        this.DR_Data.Items[0].Value = "0";
        this.DR_Data.Items.Insert(1,"从数据库");
        this.DR_Data.Items[1].Value = "1";
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        if (TxtExecutionTime.Text == "")
        {
            function.WriteErrMsg("启用时间不能为空");
        }
        int id =Convert.ToInt32(base.Request.QueryString["DevID"]);
        //this.HdnDevId.Value = id;
        if (id == 0)
        {
            info.ID = DataConverter.CLng(this.HdnDevId.Value);
            info.PlanName = this.TxtPlanName.Text;
            info.PlanRule = this.TxtPlanRule.SelectedItem.Value;
            info.DataSet = this.DR_Data.SelectedItem.Value;
            info.ExecutionTime = Convert.ToDateTime(this.TxtExecutionTime.Text);
            info.Description = this.TxtDescription.Text;
            bll.insert(info);
            function.WriteSuccessMsg("添加成功!", "DevelopmentCenter.aspx");
        }
        else
        {
            info = bll.SelReturnModel(id);
            info.PlanName = this.TxtPlanName.Text;
            info.PlanRule = this.TxtPlanRule.SelectedItem.Value;
            info.DataSet = this.DR_Data.SelectedItem.Value;
            info.ExecutionTime = Convert.ToDateTime(this.TxtExecutionTime.Text);
            info.Description = this.TxtDescription.Text;
            bll.UpdateByID(info);
            function.WriteSuccessMsg("修改成功!", "DevelopmentCenter.aspx");
        }
    }
}
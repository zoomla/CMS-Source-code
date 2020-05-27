using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.SQLDAL;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class manage_Config_CreateSQLmod : CustomerPageAction
{
    B_SQL bll = new B_SQL();
    M_SQL model = new M_SQL();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        if (!IsPostBack)
        {
              Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>扩展功能</a></li><li><a href='DatalistProfile.aspx'>开发中心</a></li><li class='active'>添加插件</li>");
            if (!string.IsNullOrEmpty(Request["ID"]))
            {
                int id = DataConvert.CLng(Request["ID"]);
                model = bll.SelReturnModel(id);
                txtName.Text = model.TagName;
                txtTableName.Text = model.TableName.Replace("ZL_my_","") ;
                txtUnit.Text = model.Unit;
                txtIcon.Text = model.Icon;
                txtExplain.Text = model.Explain;
                txtRunNum.Text = model.RunNum.ToString();
                TxtUserID.Value = model.UserID.ToString();
                txtUserName.Text = model.UserID.ToString();
                txtBtnName.Text = model.BtnName;
                txtSqlUrl.Text = model.SqlUrl;
                txtRunTime.Text = model.RunTime;
            }
        }
        if(Convert.ToInt32(Request["type"])==2)
        {
            this.CreateBtn.Text = "修改";
        }
    }

    protected void CreateBtn_Click(object sender, EventArgs e)
    {
        model.TagName = txtName.Text;
        model.TableName = "ZL_my_" + txtTableName.Text;
        model.Unit = txtUnit.Text;
        model.Icon = txtIcon.Text;
        model.Explain = txtExplain.Text;
        model.UserID = txtUserName.Text;
        model.BtnName = txtBtnName.Text;
        model.SqlUrl = txtSqlUrl.Text;
        model.RunTime = txtRunTime.Text;

        if (!IsNum(txtRunNum.Text))
        {
            function.WriteErrMsg("输入的格式不正确");
        }
        else {
            model.RunNum = Convert.ToInt32(txtRunNum.Text);
        }

        if (bll.SelByTName("ZL_my_" + txtTableName.Text).Rows.Count > 0)
        {
            function.WriteErrMsg("插入失败！数据库中已存在该表，请重新添加");
            return;
        }
        if ( Convert.ToInt32(Request["type"]) == 1&& bll.insert(model) > 0)
        {
            function.WriteSuccessMsg("添加成功!", "SQLManage.aspx");

        }
        if (Convert.ToInt32(Request["type"]) == 2)
        {
            model.ID =Convert.ToInt32( Request["ID"]);
            if (bll.UpdateByID(model))
            {
                function.WriteSuccessMsg("修改成功！", "SQLManage.aspx");
            }
          
        }
    }
    private bool IsNum(string Value)//判断文本框是否是数字
    {
        try
        {
            int i = int.Parse(Value);
            return true;
        }
        catch
        {
            return false;
        }
    }

 
}
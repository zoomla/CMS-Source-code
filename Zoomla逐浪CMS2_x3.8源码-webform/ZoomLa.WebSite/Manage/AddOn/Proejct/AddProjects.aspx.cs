using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Common;
using ZoomLa.BLL.Project;
using ZoomLa.Model.Project;

public partial class manage_AddOn_AddProjects : CustomerPageAction
{
    B_Admin badmin = new B_Admin();
    B_Sensitivity sll = new B_Sensitivity();
    B_Pro_Type ptBll = new B_Pro_Type();
    B_Pro_Project ppBll = new B_Pro_Project();

    //页面加载
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = ptBll.Sel();
            DDList.DataSource = dt;
            DDList.DataTextField = "TName";
            DDList.DataValueField = "ID";
            DDList.DataBind();
            this.Leader.Text = badmin.GetAdminLogin().AdminTrueName;
            //this.ProManageerID_H.Value = badmin.GetAdminLogin().AdminId.ToString();
          
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Projects.aspx'>项目管理</a></li><li class='active'>新增项目</a></li>");
    }

    //提交
    protected void Commit_Click(object sender, EventArgs e)
    {
        M_Pro_Project proMod = new M_Pro_Project();
        proMod.ProName = TxtProjectName.Text.Trim(' ');
        proMod.ZType = DataConverter.CLng(DDList.SelectedValue);
        proMod.ProPrice = DataConverter.CDouble(TxtProjectPrice.Text.Trim(' '));
        //proMod.ProManageer = DataConverter.CLng(ProManageerID_H.Value); 
        proMod.TecDirector = WebCoding.Text.Trim(' ');
        //proMod.CustomerID = DataConverter.CLng(CustomerID_H.Value);
        proMod.CustomerCompany = TxtUserCompany.Text.Trim(' ');
        proMod.CustomerTele = TxtUserTel.Text.Trim(' ');
        proMod.CustomerMobile = TxtUserMobile.Text.Trim(' ');
        proMod.CustomerQQ = TxtUserQQ.Text.Trim(' ');
        proMod.CustomerMSN = TxtUserMSN.Text.Trim(' ');
        proMod.CustomerAddress = TxtUserAddress.Text.Trim(' ');
        proMod.CustomerEmail = TxtUserEmail.Text.Trim(' ');
        proMod.Requirements = TxtProjectRequire.Value;
        proMod.ZStatus = 0;
        ppBll.Insert(proMod);
        function.WriteSuccessMsg("操作成功!", "Projects.aspx");
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        foreach (Control ctr in this.Page.Controls)
        {
            if (ctr is TextBox)
            {
                (ctr as TextBox).Text = "";
            }
        }
    }
}

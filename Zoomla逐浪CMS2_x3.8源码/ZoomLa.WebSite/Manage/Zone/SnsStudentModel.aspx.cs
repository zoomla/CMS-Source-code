using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Web;
using ZoomLa.Common;

public partial class manage_Zone_SnsStudentModel : CustomerPageAction
{
    private B_Model bll = new B_Model();

    private void DataBaseList()
    {
        B_ModelField mfll = new B_ModelField();

        this.Repeater1.DataSource = mfll.SelectTableName("ZL_Model", "TableName like 'ZL_School_%'");
        this.Repeater1.DataBind();
    }
    public string GetIcon(string IconPath)
    {
        return "../../Images/ModelIcon/" + (string.IsNullOrEmpty(IconPath) ? "Default.gif" : IconPath);
    }
    protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string Id = e.CommandArgument.ToString();
            Response.Redirect("SnsModel.aspx?ModelID=" + Id);
        }
        if (e.CommandName == "Del")
        {
            string Id = e.CommandArgument.ToString();
            this.bll.DelModel(int.Parse(Id));
            DataBaseList();
        }
        if (e.CommandName == "Field")
        {
            string Id = e.CommandArgument.ToString();
            Response.Redirect("SnsModelField.aspx?ModelID=" + Id);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        if (!this.Page.IsPostBack)
        {
            if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.model, "ShopModelManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            DataBaseList();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li class='active'>学校会员模型管理</li>");
        }
    }
}

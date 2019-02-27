using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;

public partial class test_AddRegular : System.Web.UI.Page
{
    B_Shop_MoneyRegular regularBll = new B_Shop_MoneyRegular();
    M_Shop_MoneyRegular regularMod = new M_Shop_MoneyRegular();
    private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../User/UserManage.aspx'>用户管理</a></li><li><a href='RegularList.aspx'>规则列表</a></li><li class='active'>规则管理</li>");
        }
    }
    private void MyBind()
    {
        if (Mid > 0)
        {
            regularMod = regularBll.SelReturnModel(Mid);
            Min_T.Text = regularMod.Min.ToString("f2");
            Purse_T.Text = regularMod.Purse.ToString("f2");
            SIcon_T.Text = regularMod.Sicon.ToString("f2");
            Point_T.Text = regularMod.Point.ToString("f2");
            UserRemind_T.Text = regularMod.UserRemind;
            AdminRemind_T.Text = regularMod.AdminRemind;
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_AdminInfo adminMod = B_Admin.GetLogin();
        if (Mid > 0) { regularMod = regularBll.SelReturnModel(Mid); }
        regularMod.Min = DataConvert.CDouble(Min_T.Text);
        if (regularMod.Min <= 0) { function.WriteErrMsg("充值金额不能为0"); }
        regularMod.Purse = DataConvert.CDouble(Purse_T.Text);
        regularMod.Sicon = DataConvert.CDouble(SIcon_T.Text);
        regularMod.Point = DataConvert.CDouble(Point_T.Text);
        regularMod.UserRemind = UserRemind_T.Text.Trim();
        regularMod.AdminRemind = AdminRemind_T.Text.Trim();
        regularMod.AdminID = regularMod.AdminID;
        if (Mid > 0) { regularMod.EditDate = DateTime.Now; regularBll.UpdateByID(regularMod); }
        else
        {
            if (regularBll.IsExist(regularMod.Min)) { function.WriteErrMsg("充值金额[" + regularMod.Min.ToString("f2") + "]的规则已存在,不可重复添加"); }
            regularBll.Insert(regularMod);
        }
        function.WriteSuccessMsg("保存成功","RegularList.aspx");
    }
}
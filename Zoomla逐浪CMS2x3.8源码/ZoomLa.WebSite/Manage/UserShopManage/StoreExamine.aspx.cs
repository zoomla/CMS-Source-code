using System;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
public partial class manage_UserShopManage_StoreExamine : CustomerPageAction
{
    B_UserStoreTable ustbll = new B_UserStoreTable();
    B_Content conBll = new B_Content();
    B_User bubll = new B_User();
    B_Content cbll = new B_Content();
    DataTable modeinfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreExamine"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='StoreManage.aspx'>店铺管理</a></li><li>商家店铺管理</li>");
        }
    }
    private void MyBind() 
    {
        EGV.DataSource = DBCenter.Sel("ZL_CommonModel", "Tablename like 'ZL_Store_%' and Status<>99");
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = DataConvert.CLng(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del2":
                conBll.Del(id);
                break;
            case "audit":
                conBll.SetAudit(id,(int)ZLEnum.ConStatus.Audited);
                break;
        }
        MyBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    public string GetStatus() { return B_Content.GetStatusStr(Convert.ToInt32(Eval("Status", ""))); }
    protected void BatDel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            conBll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }
    protected void BatAudit_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            conBll.SetAuditByIDS(Request.Form["idchk"], (int)ZLEnum.ConStatus.Audited);
        }
        MyBind();
    }
}
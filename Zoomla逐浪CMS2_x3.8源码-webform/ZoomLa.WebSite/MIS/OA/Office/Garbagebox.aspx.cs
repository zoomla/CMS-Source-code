using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;

/*
 * 公文回收站表页
 */

public partial class MIS_OA_Office_Garbagebox : System.Web.UI.Page
{
    B_Mis_Model modelBll = new B_Mis_Model();
    B_OA_Document oaBll = new B_OA_Document();
    B_Mis_AppProg proBll = new B_Mis_AppProg();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        EGV.txtFunc = txtPageFunc;
        if (!IsPostBack)
        {
            DataBind();
        }
    }
    private void DataBind(string key = "")
    {
        DataTable dt = new DataTable();
        dt = oaBll.SelByUserID(buser.GetLogin().UserID,-99);
        if (!string.IsNullOrEmpty(key.Trim()))
        {
            dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
            dt = dt.DefaultView.ToTable();
        }
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    //处理页码
    public void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
        {
            pageSize = EGV.PageSize;
        }
        else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
        {
            pageSize = EGV.PageSize;
        }
        EGV.PageSize = pageSize;
        EGV.PageIndex = 0;//改变后回到首页
        size = pageSize.ToString();
        DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = DataConverter.CLng(e.CommandArgument.ToString());
        switch (e.CommandName.ToLower())
        {
            case "del2":
                oaBll.Del(id);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功');", true);
                break;
            case "restore":
                oaBll.UpdateStatus(id,0);
                break;
        }
        DataBind();
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        DataBind(searchText.Text.Trim());
    }
    public string GetSecret(string Secret)
    {
        return OAConfig.StrToDic(OAConfig.Secret)[Secret];
    }
    public string GetImport(string importance)
    {
        return OAConfig.StrToDic(OAConfig.Importance)[importance];
    }
    public string GetUrgency(string Urgency)
    {
        return OAConfig.StrToDic(OAConfig.Urgency)[Urgency];
    }
    public string GetStatus(string Status)
    {
        switch (Status)
        {
            case "-99":
                return "<span style='color:red;'>回收站</span>";
            default:
                return "";
        }
    }

    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rv = (DataRowView)e.Row.DataItem;
            if (oaBll.IsApproving(Convert.ToInt32(rv["ID"])))
            {
                e.Row.FindControl("del").Visible = false;
                e.Row.FindControl("edit").Visible = false;
            }
        }
    }
    public DataTable MiaModelDT
    {
        get
        {
            if (Session["MiaModelDT"] == null)
                Session["MiaModelDT"] = modelBll.Sel();
            return Session["MiaModelDT"] as DataTable;
        }
        set
        {
            Session["MiaModelDT"] = value;
        }
    }
}
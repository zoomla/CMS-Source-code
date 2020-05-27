using System;
using System.Collections.Generic;
using System.Data;
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

public partial class User_Shop312_DeliverType : System.Web.UI.Page
{
    B_Shop_FareTlp fareBll = new B_Shop_FareTlp();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        DataTable dt = fareBll.U_SelByUid(mu.UserID);
        Egv.DataSource = dt;
        Egv.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            M_UserInfo mu=buser.GetLogin();
            DataRowView dr=e.Row.DataItem as DataRowView;
            var editbtn = e.Row.FindControl("edit_btn");
            if (mu.UserID != DataConvert.CLng(dr["EMS"])) { editbtn.Visible = false; }
        }
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = DataConverter.CLng(e.CommandArgument);
        M_Shop_FareTlp fareMod = fareBll.SelReturnModel(id);
        switch (e.CommandName.ToLower())
        {
            case "del":
                fareBll.Del(id);
                break;
        }
        MyBind();
    }
    public string GetMode()
    {
        switch (Eval("PriceMode").ToString())
        {
            case "2":
                return "按重量";
            default:
                return "按件数";
        }
    }
    
}
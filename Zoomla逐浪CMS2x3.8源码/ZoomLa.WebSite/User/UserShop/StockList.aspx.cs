using System;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class User_UserShop_StockList : System.Web.UI.Page
{
    B_Node bNode = new B_Node();
    B_Model bmode = new B_Model();
    B_User buser = new B_User();

    protected int Stocktype;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind() 
    {
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("del2"))
        {
            int id = Convert.ToInt32(e.CommandArgument);
        }
        MyBind();
    }
    public string stocktype(string cc)
    {
        return cc.Equals("0") ? "入库" : "出库";
    }
    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
        }
        
    }
}

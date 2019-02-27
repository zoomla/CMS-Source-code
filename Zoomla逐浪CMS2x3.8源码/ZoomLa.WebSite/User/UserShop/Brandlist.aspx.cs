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
using System.Collections.Generic;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Data.SqlClient;

public partial class manage_Shop_Brandlist : System.Web.UI.Page
{
    protected int i;
    protected string m_BrandInput;
    protected int PageSize;
    protected int CurrentPageIndex;
    private B_Trademark bll = new B_Trademark();
    protected B_Manufacturers mll = new B_Manufacturers();
    protected int id;
    private B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["producer"]))
        {
            DataTable rd = mll.GetManufacturersAll(" where Producername=@panme ", new SqlParameter[] { new SqlParameter("panme", Request.QueryString["producer"]) });
            this.id = DataConverter.CLng(rd.Rows[0]["id"]);
        }
        else {
            this.id = 0;
        }

  
        this.PageSize = 20;
        if (string.IsNullOrEmpty(base.Request.QueryString["p"]))
        {
            this.CurrentPageIndex = 1;
        }
        else
        {
            this.CurrentPageIndex = DataConverter.CLng(base.Request.QueryString["p"]);
        }
        this.CheckKeyword();
    }

    private void BindData(int keyword)
    {
        DataTable list = bll.GetTrademarkproducter(keyword);
        if (list != null)
        {
            if (list.Rows.Count == 0)
            {
                this.DivUserName.Visible = true;
            }
            else
            {
                this.DivUserName.Visible = false;
            }
            this.RepUser.DataSource = list.DefaultView;
            int RecordCount = list.Rows.Count;
            this.RepUser.DataBind();
        }
    }

    private void CheckKeyword()
    {
        //string str = DataSecurity.FilterBadChar(sss);
        this.BindData(this.id);
    }

    protected void RepUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
        }
    }
}

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
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class manage_Shop_projectSelect : CustomerPageAction
{
    B_Promotions pll = new B_Promotions();
    B_BindPro bll = new B_BindPro();

    protected void Page_Load(object sender, EventArgs e)
    {
        string KeyWord = Request.Form["TxtKeyWord"];
        string KeyWords = Request.QueryString["KeyWord"];
        int id = DataConverter.CLng(Request.QueryString["ID"]);
        if (string.IsNullOrEmpty(KeyWord) && !string.IsNullOrEmpty(KeyWords)) { KeyWord = KeyWords; }

        DataTable perminfo = pll.GetPromotionsAll();

        if (!string.IsNullOrEmpty(KeyWord))
        {
            perminfo = pll.PromotionsSearch(KeyWord);
        }

        Page_list(perminfo);
        Call.SetBreadCrumb(Master, "<li>商城管理</li><li><a href='projectSelect.aspx'>选择商品</a></li>");
    }

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        Pagetable.DataSource = Cll;
        Pagetable.DataBind();
    }
    #endregion

    public string getproimg(string type)
    {
        string restring;
        restring = "";

        if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
        if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
        {
            restring = "<img src=../../" + type + " border=0 width=60 height=45>";
        }
        else
        {
            restring = "<img src=../../UploadFiles/nopic.gif border=0 width=60 height=45>";
        }
        return restring;

    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Write("<script language=javascript>window.close();</script>");
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        string KeyWord = Request.Form["TxtKeyWord"];
        DataTable perminfo = pll.PromotionsSearch(KeyWord);
        Page_list(perminfo);
    }
}

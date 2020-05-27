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
//会员中心促销商品选择
public partial class manage_Shop_ProductsSelect : CustomerPageAction
{
    B_Product proBll = new B_Product();
    B_User buser = new B_User();
    B_Content conBll = new B_Content();
    private string KeyWord { get { return TxtKeyWord.Text; } set { TxtKeyWord.Text = value; } }
    private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        M_CommonData storeMod = conBll.SelMyStore(mu.UserName);
        if (storeMod == null) { function.WriteErrMsg("你还未开设自己的店铺,无法设定促销商品"); }
        DataTable dt = new DataTable();
        if (!string.IsNullOrEmpty(KeyWord))
        {
            dt = proBll.ProductSearch(0, KeyWord,storeMod.GeneralID);
        }
        else { dt = proBll.GetProductAll(0, storeMod.GeneralID); }
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    public string getproimg()
    {
        return function.GetImgUrl(Eval("Thumbnails"));

        //string restring;
        //restring = "";

        //if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
        //if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
        //{
        //    restring = "<img src=/" + type + " border=0 width=60 height=45>";
        //}
        //else
        //{
        //    restring = "<img src=/UploadFiles/nopic.gif border=0 width=60 height=45>";
        //}
        //return restring;

    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Write("<script language=javascript>window.close();</script>");
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        KeyWord = "";
        MyBind();
    }
}

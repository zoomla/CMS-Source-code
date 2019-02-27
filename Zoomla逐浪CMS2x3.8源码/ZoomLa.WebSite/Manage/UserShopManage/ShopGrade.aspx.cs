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
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;
using ZoomLa.Web;

public partial class manage_UserShopManage_ShopGrade : System.Web.UI.Page
{
    #region 业务对象
    B_ShopGrade shopGrade = new B_ShopGrade();
    B_Model mbll = new B_Model();
    B_Admin badmin = new B_Admin();
    B_Content cbll = new B_Content();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        
        if (!B_ARoleAuth.Check(ZLEnum.Auth.store, "StoreinfoManage"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        if (!IsPostBack)
        {
            GetInit();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='StoreManage.aspx'>店铺管理</a><li class='active'>店铺等级管理<a href='AddShopGrades.aspx'>[添加等级]</a></li>");
    }
    #region 页面方法
    private void GetInit()
    {
        int CPage;
        int temppage;
        if (!string.IsNullOrEmpty(DropDownList1.SelectedValue))
        {
            temppage = Convert.ToInt32(DropDownList1.SelectedValue);
        }
        else
        {
            temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
        }
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }
        DataTable dt = shopGrade.GetShopGradeinfo();
        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = dt.DefaultView;
        cc.AllowPaging = true;
        cc.PageSize = 1;
        cc.CurrentPageIndex = CPage - 1;
        GradeList.DataSource = cc;
        GradeList.DataBind();
        if (dt != null)
        {
            dt.Dispose();
        }
       
        Allnum.Text = dt.DefaultView.Count.ToString();
        int thispagenull = cc.PageCount;//总页数
        int CurrentPage = cc.CurrentPageIndex;
        int nextpagenum = CPage - 1;//上一页
        int downpagenum = CPage + 1;//下一页
        int Endpagenum = thispagenull;
        if (thispagenull <= CPage)
        {
            downpagenum = thispagenull;
            Downpage.Enabled = false;
        }
        else
        {
            Downpage.Enabled = true;
        }
        if (nextpagenum <= 0)
        {
            nextpagenum = 0;
            Nextpage.Enabled = false;
        }
        else
        {
            Nextpage.Enabled = true;
        }

        Toppage.Text = "<a href=?Currentpage=0>首页</a>";
        Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
        Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
        Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
        PageSize.Text = thispagenull.ToString();
        Nowpage.Text = CPage.ToString();
        pagess.Text = cc.PageSize.ToString();

        if (!this.Page.IsPostBack)
        {
            for (int i = 1; i <= thispagenull; i++)
            {
                DropDownList1.Items.Add(i.ToString());
            }
        }
        int id = DataConverter.CLng(Request.QueryString["id"]);
        if (Request.QueryString["menu"] == "del")
        {
            if (shopGrade.DelShopGrade(id))
            {
                Response.Write("<script language=javascript>alert('删除成功');location.href='ShopGrade.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('删除失败');location.href='ShopGrade.aspx'</script>");
            }
        }
    }
    protected string Gettpye(string type)
    {
        if (type == "0")
        {
            return "购物等级";
        }
        else if (type == "1")
        {
            return "卖家等级";
        }
        else if(type=="2")
        {
            return "商户等级";
        }
        else
        {
        return "";
        }
    }

    protected string Getture(string str)
    {
        if (str == "True")
        { return "<font color=green>启用</font>"; }
        else if (str == "False")
        { return "<font color=red>停用</font>"; }
        else { return ""; }
    }
    public string GetIcon(string IconPath, string picnum)
    {
        int pcnum = DataConverter.CLng(picnum);
        string cc= "/Images/levelIcon/" + (string.IsNullOrEmpty(IconPath) ? "m_1.gif" : IconPath);
        string ee = "<img src=" + cc + " style=\"border-width:0px;\" />";
        string dd = "";
        for (int i = 0; i < pcnum; i++)
        {
            dd = dd + ee;
        }
        return dd;
    }
    protected void Button1_Click(object sender, EventArgs e)
{
    string item = Request.Form["Item"];
    shopGrade.ShopGradTrue(item, 1);
    Response.Write("<script language=javascript>alert('批量启用成功');location.href='ShopGrade.aspx'</script>");
}
    protected void Button2_Click(object sender, EventArgs e)
    {
        string item = Request.Form["Item"];
        shopGrade.ShopGradTrue(item, 2);
        Response.Write("<script language=javascript>alert('批量停用成功');location.href='ShopGrade.aspx'</script>");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string item = Request.Form["Item"];
        shopGrade.ShopGradTrue(item, 3);
        Response.Write("<script language=javascript>alert('批量删除成功');location.href='ShopGrade.aspx'</script>");
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetInit();
    }
}
    #endregion 

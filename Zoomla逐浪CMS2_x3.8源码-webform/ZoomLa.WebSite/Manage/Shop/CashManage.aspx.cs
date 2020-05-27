using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;
public partial class manage_Shop_CashManage : CustomerPageAction
{
    B_Cash bc = new B_Cash();
    B_User bu = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        B_Admin badmin = new B_Admin();
        

        string menu = Request.QueryString["menu"];
        int pageid = DataConverter.CLng(Request.QueryString["id"]);

        switch (menu)
        {
            case "del":
                if (bc.DelCashid(pageid))
                {
                    Response.Write("<script language=javascript>alert('删除成功!');location.href='CashManage.aspx';</script>");
                }
                else
                {
                    Response.Write("<script language=javascript>alert('删除失败!请选择您要删除的数据');location.href='CCashManage.aspx';</script>");
                }
                break;
            case "stop":
                M_Cash mc = new M_Cash();
                mc.eTime = DateTime.Now;
                mc.Y_ID = pageid;
                mc.yState = 1;
                bc.UpCash(mc);
                break;
        }
        Bind();
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li>处理VIP卡提现金</li><li><a href='AddCard.aspx'>[ 生成VIP卡 ]</a>&nbsp;&nbsp;&nbsp;<a href='CardTypeManage.aspx'>[ 卡类型管理 ]</a>&nbsp;&nbsp;&nbsp;<a href='Addcardtype.aspx'>[ 添加卡类型 ]</a></li>");
    }

    private void Bind()
    {
        int CPage, temppage;

        if (Request.Form["DropDownList1"] != null)
        {
            temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
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

        DataTable Cll = bc.SelectAll();

        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = Cll.DefaultView;
        cc.AllowPaging = true;
        cc.PageSize = 12;
        cc.CurrentPageIndex = CPage - 1;
        gvCard.DataSource = cc;
        gvCard.DataBind();

        Allnum.Text = Cll.DefaultView.Count.ToString();
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
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();


        if (!this.Page.IsPostBack)
        {
            for (int i = 1; i <= thispagenull; i++)
            {
                DropDownList1.Items.Add(i.ToString());
            }
        }

    }

    public string showMoney(string id)
    {
        double sid = DataConverter.CDouble(id);

        return sid.ToString("f2");
    }

    public string shoyState(string id)
    {
        int sid = DataConverter.CLng(id);
        string str = "";

        if (sid == 1)
        {
            str = "<span style='color:red;'>以处理</span>";
        }
        else
        {
            str = "待处理";
        }
        return str;
    }

    public string showuse(string id)
    {
        int sid = DataConverter.CLng(id);
        string str = "";
        M_Cash tp = bc.SelReturnModel(sid);
        if (tp.yState == 0)
        {
            str = "<a href=?menu=stop&id=" + sid + ">提交处理</a>";
        }
        else
        {
            str = "<span style='color:red;'>完成操作</span>";
        }
        return str;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
}

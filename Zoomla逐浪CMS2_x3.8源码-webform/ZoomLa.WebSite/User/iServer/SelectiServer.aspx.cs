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
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;

public partial class manage_iServer_SelectiServer : System.Web.UI.Page
{
    B_IServer Serverbll = new B_IServer();
    string[] typeArray ={"","咨询","投诉","建议","要求","界面使用", "bug报告", "订单", "财务", "域名", "主机" , "邮局" , "DNS", "MSSQL"
                            ,"MySQL", "IDC", "网站推广", "网站制作", "其它"};
    string menu = "";
    string orderId = "";
    int type = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        string strTitle = Request.QueryString["strTitle"] == null ? "" : Request.QueryString["strTitle"];
        B_User buser = new B_User();
        M_UserInfo info = buser.GetLogin();
        DataTable table = new DataTable();
        string state = "";
        string num = Request.QueryString["num"] == null ? "" : Request.QueryString["num"];
        switch (num)
        {
            case "1":
                state = "未解决";
                break;
            case "2":
                state = "处理中";
                break;
            case "3":
                state = "已解决";
                break;
            case "4":
                state = "已锁定";
                break;
            default:
                state = "";
                break;
        }
        GetQueryString();
        table = Serverbll.SeachLikeTitle(strTitle, state, info.UserID, menu, typeArray[type], DataConvert.CLng(orderId));
        int CPage;
        int temppage;

        if (Request.Form["DropDownList1"] != null)
        {
            temppage = DataConverter.CLng(Request.Form["DropDownList1"]);
        }
        else
        {
            temppage = DataConverter.CLng(Request.QueryString["CurrentPage"]);
        }
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }
        if (table != null && table.Rows.Count > 0)
        {
            this.nocontent.Style["display"] = "none";
            this.resultsRepeater_w.Visible = true;

            PagedDataSource dd = new PagedDataSource();
            dd.DataSource = table.DefaultView;
            if (Request.QueryString["txtPage"] != null && Request.QueryString["txtPage"] != "")
            {
                dd.PageSize = DataConverter.CLng(Request.QueryString["txtPage"]);
            }
            if (Request.Form["txtPage"] != null && Request.Form["txtPage"] != "")
            {
                dd.PageSize = DataConverter.CLng(Request.Form["txtPage"]);
            }
            dd.AllowPaging = true;
            dd.CurrentPageIndex = CPage - 1;
            if (table != null && dd.PageSize >= table.Rows.Count)
            {
                dd.CurrentPageIndex = 0;
                CPage = 1;
            }
            this.resultsRepeater_w.DataSource = dd;
            this.resultsRepeater_w.DataBind();

            if (table != null && table.Rows.Count > 0)
            {
                Allnum.Text = table.Rows.Count.ToString();
            }
            else
            {
                Allnum.Text = "0";
            }
            int thispagenull = dd.PageCount;//总页数
            int CurrentPage = dd.CurrentPageIndex;
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
            txtPage.Text = dd.PageSize.ToString();
            Toppage.Text = "<a href=?txtPage=" + txtPage.Text + "&menu=" + Request.QueryString["menu"] + "&OrderID=" + Request.QueryString["OrderID"] + "&num=" + num + "&strTitle=" + strTitle + "&Currentpage=0>首页</a>";
            Nextpage.Text = "<a href=?txtPage=" + txtPage.Text + "&menu=" + Request.QueryString["menu"] + "&OrderID=" + Request.QueryString["OrderID"] + "&num=" + num + "&strTitle=" + strTitle + "&Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
            Downpage.Text = "<a href=?txtPage=" + txtPage.Text + "&menu=" + Request.QueryString["menu"] + "&OrderID=" + Request.QueryString["OrderID"] + "&num=" + num + "&strTitle=" + strTitle + "&Currentpage=" + downpagenum.ToString() + ">下一页</a>";
            Endpage.Text = "<a href=?txtPage=" + txtPage.Text + "&menu=" + Request.QueryString["menu"] + "&OrderID=" + Request.QueryString["OrderID"] + "&num=" + num + "&strTitle=" + strTitle + "&Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
            PageSize.Text = thispagenull.ToString();
            Nowpage.Text = CPage.ToString();
            DropDownList1.Items.Clear();
            for (int i = 1; i <= thispagenull; i++)
            {
                DropDownList1.Items.Add(i.ToString());
            }
            for (int j = 0; j < DropDownList1.Items.Count; j++)
            {
                if (DropDownList1.Items[j].Value == Nowpage.Text)
                {
                    DropDownList1.SelectedValue = Nowpage.Text;
                    break;
                }
            }
        }
        else
        {
            this.nocontent.Style["display"] = "";
            this.resultsRepeater_w.Visible = false;
        }
        //读取用户提交的问题分类
        repSeachBtn.DataSource = Serverbll.GetSeachUserIdType(info.UserID);
        repSeachBtn.DataBind();
    }
    public string GetUserName(string UserId)
    {
        B_User buser = new B_User();
        return buser.GetUserByUserID(DataConverter.CLng(UserId)).UserName;
    }
    public string GetGroupName()
    {
        B_User buser = new B_User();
        string GroupID = buser.GetLogin().GroupID.ToString();
        B_Group bgp = new B_Group();
        return bgp.GetByID(DataConverter.CLng(GroupID)).GroupName;
    }
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        MyBind();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyBind();
    }
    private void GetQueryString()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["menu"]))
        {
            menu = Request.QueryString["menu"];
        }
        if (!string.IsNullOrEmpty(Request.QueryString["OrderID"]))
        {
            orderId = Request.QueryString["OrderID"];
        }
        if (!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            type = DataConverter.CLng(Request.QueryString["type"]);
        }
    }
    protected void btnSeach_Click(object sender, EventArgs e)
    {
        GetQueryString();
        string strTitle = txtTitle.Text;
        if (menu != "")
            Response.Redirect("SelectiServer.aspx?num=-1&menu=" + menu + "&strTitle=" + strTitle);
        else
            Response.Redirect("SelectiServer.aspx?num=-1&strTitle=" + strTitle);
    }
    protected int returnType(object typeName)
    {
        int index = 0;
        for (int i = 0; i < typeArray.Length; i++)
        {
            if (typeName.ToString().Trim() == typeArray[i])
            {
                index = i;
                break;
            }
        }
        return index;
    }
    protected string retuenMapNav()
    {
        string mapNav = "所有问题";
        if(!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            mapNav = typeArray[DataConverter.CLng(Request.QueryString["type"])];
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["num"]))
        {
            string[] NN = { "", "未解决的问题", "处理中的问题", "已解决的问题" };
            int number = DataConverter.CLng(Request.QueryString["num"]);
            if (number > 0)
                mapNav = NN[DataConverter.CLng(Request.QueryString["num"])];
        }
        return mapNav;
    }
}

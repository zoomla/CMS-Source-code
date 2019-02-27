using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Sns.BLL;
using ZoomLa.Sns.Model;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Components;

public partial class BookList : Page
{
    #region 业务对象
    BookBLL bbll = new BookBLL();
    B_User ubll = new B_User();
    #endregion
    int currentUser = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = ubll.GetLogin().UserID;
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ubll.GetUserByUserID(currentUser);
            ViewState["title"] = this.Searchtxt.Text;
            GetBooktable(null);
        }
        this.WebUserControlLabel1.Allstype = 0;
    }

    private Dictionary<string, string> Order
    {
        get
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("BookAddtime", "0");
            return ht;
        }
    }

    #region 页面方法
    //绑定书籍
    private void GetBooktable(string search)
    {
        int CPage;
        int temppage;
        List<BookTable> list = bbll.GetBookLike(search, 0);
        if (list.Count < 1)
        {
            this.AddBook.Visible = true;
            this.AddBook.Text = "没有记录" + search + " <a href=\"BooktableAdd.aspx?BookName=" + search + "\">添加书籍" + search + "</a>";
            this.DataList1.Visible = false;
        }
        else
        {
            this.AddBook.Visible = false;
            this.DataList1.Visible = true;
            temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
            CPage = temppage;
            if (CPage <= 0)
            {
                CPage = 1;
            }
            PagedDataSource cc = new PagedDataSource();
            cc.DataSource = list;
            cc.AllowPaging = true;
            cc.PageSize = 10;
            cc.CurrentPageIndex = CPage - 1;
            DataList1.DataSource = cc;
            DataList1.DataBind();

            Allnum.Text = list.Count.ToString();
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

            Toppage.Text = "<a href=BookList.aspx?Currentpage=0>首页</a>";
            Nextpage.Text = "<a href=BookList.aspx?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
            Downpage.Text = "<a href=BookList.aspx?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
            Endpage.Text = "<a href=BookList.aspx?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
            Nowpage.Text = CPage.ToString();
            PageSize.Text = thispagenull.ToString();
            pagess.Text = cc.PageSize.ToString();
        }
   

    }

    public string GetStr(string str)
    {
        if (str.Length > 5)
            return str.Substring(0, 5);
        else
            return str;
    }
    

    protected void sBtn_Click(object sender, EventArgs e)
    {
         ViewState["title"] = this.Searchtxt.Text;

         GetBooktable(ViewState["title"].ToString());
    }

    
    #endregion
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        DataListItem di = lb.Parent as DataListItem;
        bbll.del(new Guid(this.DataList1.DataKeys[di.ItemIndex].ToString()));
        GetBooktable(null);
    }

    protected bool GetV(string str)
    {
        bool b;
        if (str == currentUser.ToString())
        {
            b = true;
        }
        else
        {
            b = false;
        }
        return b;
    }
}


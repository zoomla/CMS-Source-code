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

public partial class WebUserControlMy : System.Web.UI.UserControl
{

    #region 业务对象
    CollectTableBLL ctbll = new CollectTableBLL();
    BookBLL bbll = new BookBLL();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCollect();
            GetBook();
        }
        this.WebUserControlLabel1.Allstype = stype;
        td1.InnerHtml = GetAddUrl();
    }

    private string GetAddUrl()
    {
        switch (stype)
        {
            case 0:
                return "<a href='" + ResolveUrl("~/User/UserZone/Book/BooktableAdd.aspx") + "'>我要介绍</a>";
            case 1:
                return "<a href='" + ResolveUrl("~/User/UserZone/Book/MovieAdd.aspx") + "'>我要介绍</a>";
            case 2:
                return "<a href='" + ResolveUrl("~/User/UserZone/Book/MusicAdd.aspx") + "'>我要介绍</a>";
            case 3:
                return "<a href='" + ResolveUrl("~/User/UserZone/Book/ItineraryAdd.aspx") + "'>我要介绍</a>";
            default :
                return null;
        }
    }

    public int stype
    {
        get
        {
            if (ViewState["stype"] != null)
                return int.Parse(ViewState["stype"].ToString());
            else return 100;
        }
        set
        {
            ViewState["stype"] = value;
        }
    }

    private int state
    {
        get
        {
            if (ViewState["state"] != null)
                return int.Parse(ViewState["state"].ToString());
            else return 100;
        }
        set
        {
            ViewState["state"] = value;
        }
    }

    public int UserID
    {
        get
        {
            if (ViewState["UserID"] != null)
                return int.Parse(ViewState["UserID"].ToString());
            else return 0;
        }
        set
        {
            ViewState["UserID"] = value;
        }
    }

    public int CurrentPage
    {
        get
        {
            if (ViewState["CurrentPage"] != null)
                return int.Parse (ViewState["CurrentPage"].ToString());
            else return 0;
        }
        set
        {
            ViewState["CurrentPage"] = value;
        }
    }

    public string PageName
    {
        get
        {
            if (ViewState["PageName"] != null)
                return ViewState["PageName"].ToString();
            else return string.Empty;
        }
        set
        {
            ViewState["PageName"] = value;
        }
    }

    //读取收藏状态
    private void GetCollect()
    {

        string[] bstate ={ "想读", "在读", "读过", "想看", "在看", "看过", "想听", "在听", "听过", "想去", "在途中", "去过" };
       
        int j = 0;
        j = stype * 3;
        this.LinkButton1.Text = bstate[j];
        this.LinkButton1.CommandName = j.ToString();
        this.LinkButton2.Text = bstate[j+1];
        this.LinkButton2.CommandName = (j + 1).ToString();
        this.LinkButton3.Text = bstate[j + 2];
        this.LinkButton3.CommandName = (j + 2).ToString();

    }

    private Dictionary<string, string> Order
    {
        get
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("CAddtime", "0");
            return ht;
        }
    }
    //绑定数据
    private void GetBook()
    {
        int CPage;
        int temppage;
        List<CollectTable> list = ctbll.GetcollectByUserID(UserID, stype, state);
        temppage = CurrentPage;
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }
        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = list;
        cc.AllowPaging = true;
        cc.PageSize = 2;
        cc.CurrentPageIndex = CPage - 1;
        DataList1.DataSource = cc;
        DataList1.DataBind();

        Allnum.Text = list.Count.ToString();
        int thispagenull = cc.PageCount;//总页数
        int CurrentPage2 = cc.CurrentPageIndex;
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

        Toppage.Text = "<a href=" + PageName + ".aspx?type=" + stype + "&Currentpage=0>首页</a>";
        Nextpage.Text = "<a href=" + PageName + ".aspx?type=" + stype + "&Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
        Downpage.Text = "<a href=" + PageName + ".aspx?type=" + stype + "&Currentpage=" + downpagenum.ToString() + ">下一页</a>";
        Endpage.Text = "<a href=" + PageName + ".aspx?type=" + stype + "&Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();
        
    }

    protected string fotmatString(string uID, string stype)
    {
        Guid sid = new Guid(uID.ToString());
        BookTable bt = bbll.GetBooktableByID(sid);
        int st = Convert.ToInt32(stype);

        if (st == 0)
        {

            return "(<a href='"+ResolveUrl("~/User/UserZone/Book/Bookshow.aspx?bID=" + sid.ToString()) + "'>" + bt.BookTitle + "</a>)";
        }
        else if (st == 1)
        {
            return "(<a href='" + ResolveUrl("~/User/UserZone/Book/Movieshow.aspx?bID=" + sid.ToString()) + "'>" + bt.BookTitle + "</a>)";
        }
        else
        {
            return "(<a href='" + ResolveUrl("~/User/UserZone/Book/Musicshow.aspx?bID=" + sid.ToString()) + "'>" + bt.BookTitle + "</a>)";
        }
    }

    protected string geturl(string uID)
    {
        Guid sid = new Guid(uID.ToString());
        BookTable bt = bbll.GetBooktableByID(sid);
        return bt.Bookurl;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        ViewState["state"] = int.Parse(lb.CommandName.ToString());
        GetBook();
    }

    protected void DelCommant_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        DataListItem dl = lb.Parent as DataListItem;
        ctbll.DelCollect(new Guid(this.DataList1.DataKeys[dl.ItemIndex].ToString()));
        GetBook();
    }
}

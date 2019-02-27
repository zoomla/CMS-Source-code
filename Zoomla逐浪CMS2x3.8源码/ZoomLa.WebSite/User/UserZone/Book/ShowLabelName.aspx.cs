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
using ZoomLa.BLL;
using ZoomLa.Sns.BLL;
using ZoomLa.Model;
using ZoomLa.Components;


public partial class ShowLabelName : Page
{
    #region 业务对象
    BookBLL bbll = new BookBLL();
    CollectTableBLL ctbll = new CollectTableBLL();
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
            ViewState["slabelname"] = Request.QueryString["labelname"];
            ViewState["sstype"] = int.Parse(Request.QueryString["stype"]);
            GetBooktable();
        }
        this.WebUserControlLabel1.Allstype = sstype;
        this.Label1.Text = slabelname;
    }

    #region 页面方法

    private string slabelname
    {
        get
        {
            if (ViewState["slabelname"] != null)
                return ViewState["slabelname"].ToString();
            else return string.Empty;

        }
        set
        {
            ViewState["slabelname"] = value;
        }
    }

    private int sstype
    {
        get
        {
            if (ViewState["sstype"] != null)
                return int.Parse(ViewState["sstype"].ToString());
            else return 100;
        }
        set
        {
            ViewState["sstype"] = value;
        }
    }


    protected string Geturl(Guid slnid)
    {
        return bbll.GetBooktableByID(slnid).Bookurl;
    }

    protected string Gettitle(Guid slnid)
    {
        return bbll.GetBooktableByID(slnid).BookTitle;
    }

    protected string Getcontent(Guid slnid)
    {
        string con = bbll.GetBooktableByID(slnid).BookContent;
        if (con.Length > 20)
            return con.Substring(0, 20) + "...";
        else
            return con;
    }


    protected void GetBooktable()
    {
        int CPage;
        int temppage;

        List<Guid> list = ctbll.GetBookBystate(slabelname, sstype);

        temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
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

        Toppage.Text = "<a href=ShowLabelName.aspx?Currentpage=0>首页</a>";
        Nextpage.Text = "<a href=ShowLabelName.aspx?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
        Downpage.Text = "<a href=ShowLabelName.aspx?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
        Endpage.Text = "<a href=ShowLabelName.aspx?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();

    }

    #endregion
    protected void WebUserControlLabel1_Load(object sender, EventArgs e)
    {

    }
}

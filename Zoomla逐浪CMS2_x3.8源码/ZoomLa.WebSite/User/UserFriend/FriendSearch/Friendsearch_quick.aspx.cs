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
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL;
using FreeHome.common;
using ZoomLa.Sns.BLL;
using ZoomLa.Sns.Model;
using ZoomLa.Common;

public partial class Friendsearch_quick : Page
{
    #region 业务实体
    B_User ut = new B_User();
    UserTableBLL utbll = new UserTableBLL();
    int currentUser = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = ut.GetLogin().UserID;
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ut.GetUserByUserID(currentUser);
            trSearchFriendMarry.Visible = true;
        }
    }

    private string Xmlurl
    {
        get
        {
            return Server.MapPath(@"~/User/Command/UserRegXml.xml");
        }
        set
        {
            Xmlurl = value;
        }
    }
    private void GetPage()
    {
        List<string> list = null;
        //绑定省
        //List<province> list2 = ct.readProvince(Server.MapPath(@"~/User/Command/SystemData.xml"));
        //DropDownList3.DataSource = list2;
        //DropDownList3.DataTextField = "name";
        //DropDownList3.DataValueField = "code";
        //DropDownList3.DataBind();


        ListItem li3 = new ListItem();
        li3.Value = "";
        li3.Text = "不限";
        li3.Selected = true;
        DropDownList3.Items.Add(li3);

        //绑定婚姻

        list = UserRegConfig.GetInitUserReg(Xmlurl, "UserMarry");
        marryDropDownList.DataSource = list;
        marryDropDownList.DataBind();
        marryDropDownList.Items.Add(li3);

        ////绑定月收入
        //list = UserRegConfig.GetInitUserReg(Xmlurl, "UserMonthIn");
        //monthDropDownList.DataSource = list;
        //monthDropDownList.DataBind();
        //monthDropDownList.Items.Add(li3);

        ////绑定住房条件
        //list = UserRegConfig.GetInitUserReg(Xmlurl, "UserHome");
        //homeDropDownList.DataSource = list;
        //homeDropDownList.DataBind();
        //homeDropDownList.Items.Add(li3);
    }
    //绑定城市
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pro = DropDownList3.SelectedValue;
        if (pro != "")
        {
            //this.DropDownList4.Visible = true;
            //List<Pcity> listc = ct.ReadCity(Server.MapPath(@"~/User/Command/SystemData.xml"), pro);
            //DropDownList4.DataSource = listc;
            //DropDownList4.DataTextField = "name";
            //DropDownList4.DataValueField = "code";
            //DropDownList4.DataBind();
        }
    }

    protected string GetPic(string id)
    {
        return ut.GetUserBaseByuserid(int.Parse(id)).UserFace;
    }
    protected void quick2btn_Click(object sender, EventArgs e)
    {
        string age1 = this.TextBox1.Text;
        string age2 = this.TextBox2.Text;
        string sextext = this.RadioButtonList1.Text;
        string pro = this.DropDownList3.Text;
        string citys = this.DropDownList4.Text;
        string marry = this.marryDropDownList.Text;
        //string monthin = this.monthDropDownList.Text;
        //string home = this.homeDropDownList.Text;

        IList<UserMoreinfo> umlist = utbll.GetSearch2(currentUser, age1, age2, sextext, pro, citys, marry);



        //.GetUserFriend(list);
        if (umlist.Count < 1)
        {
            quickresultPanel.Visible = false;
            this.Label1.Text = "暂时没有符合你要求的结果！";
        }
        else
        {
            quickresultPanel.Visible = true;
            this.Label1.Text = "";
        }
        DataList1.DataSource = umlist;
        DataList1.DataBind();
    }

    //绑定好友
    //private void GetUserFriend(List<UserMoreinfo> list)
    //{
    //    this.quickPanel.Visible = false;
    //    this.quickresultPanel.Visible = true;
    //    int CPage;
    //    int temppage;
    //    if (Request.Form["DropDownList1"] != null)
    //    {
    //        temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
    //    }
    //    else
    //    {
    //        temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
    //    }

    //    CPage = temppage;
    //    if (CPage <= 0)
    //    {
    //        CPage = 1;
    //    }
    //    PagedDataSource cc = new PagedDataSource();
    //    cc.DataSource = list;
    //    cc.AllowPaging = true;
    //    cc.PageSize = 5;
    //    cc.CurrentPageIndex = CPage - 1;
    //    DataList1.DataSource = cc;
    //    DataList1.DataBind();

    //    Allnum.Text = list.Count.ToString();
    //    int thispagenull = cc.PageCount;//总页数
    //    int CurrentPage = cc.CurrentPageIndex;
    //    int nextpagenum = CPage - 1;//上一页
    //    int downpagenum = CPage + 1;//下一页
    //    int Endpagenum = thispagenull;
    //    if (thispagenull <= CPage)
    //    {
    //        downpagenum = thispagenull;
    //        Downpage.Enabled = false;
    //    }
    //    else
    //    {
    //        Downpage.Enabled = true;
    //    }
    //    if (nextpagenum <= 0)
    //    {
    //        nextpagenum = 0;
    //        Nextpage.Enabled = false;
    //    }
    //    else
    //    {
    //        Nextpage.Enabled = true;
    //    }

    //    Toppage.Text = "<a href=?Currentpage=0>首页</a>";
    //    Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
    //    Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
    //    Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
    //    Nowpage.Text = CPage.ToString();
    //    PageSize.Text = thispagenull.ToString();
    //    pagess.Text = cc.PageSize.ToString();
    //    for (int i = 1; i <= thispagenull; i++)
    //    {
    //        DropDownList1.Items.Add(i.ToString());
    //    }
    //}

    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Response.Redirect("?Currentpage=" + this.DropDownList1.Text);
    //}

    //private void GetRequest(string minage, string maxage, string sex, string province,string city)
    //{
    //    this.quickPanel.Visible = false;
    //    this.quickresultPanel.Visible = true;
    //    List<UserMoreinfo> list = utbll.SearchQuickUser(currentUser, minage, maxage, sex, province, city);
    //    DataList1.DataSource = list;
    //    DataList1.DataBind();
    //}

    //private void GetSearchName(string username)
    //{
    //    this.quickPanel.Visible = false;
    //    this.quickresultPanel.Visible = true;
    //    List<UserMoreinfo> list = utbll.GetSearchQuickUserName(currentUser,username,"");
    //    DataList1.DataSource = list;
    //    DataList1.DataBind();
    //}

    //private void GetSearchHome(string home)
    //{
    //    this.quickPanel.Visible = false;
    //    this.quickresultPanel.Visible = true;
    //    List<UserMoreinfo> list = utbll.GetSearchQuickUserName(currentUser,"",home );
    //    DataList1.DataSource = list;
    //    DataList1.DataBind();
    //}
}
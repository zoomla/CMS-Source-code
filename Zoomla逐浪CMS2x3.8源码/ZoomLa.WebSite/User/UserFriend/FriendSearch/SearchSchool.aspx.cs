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
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Sns.BLL;
using ZoomLa.Sns.Model;

public partial class SearchSchool : Page 
{
    B_User ut = new B_User();
    UserTableBLL utbll = new UserTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ut.GetUserByUserID(ut.GetLogin().UserID);
            GetPage();
        }
    }

    private void GetPage()
    {
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string sex = this.RadioButtonList1.SelectedValue;
        string pro = this.DropDownList3.Text ;
        string city = this.DropDownList4.Text ;
        string school = this.TextBox1.Text;
        this.quickPanel.Visible = false;
        this.quickresultPanel.Visible = true;
        Bind();
        if (DataList1.Items.Count < 1)
            this.Label1.Text = "暂时没有符合你要求的结果！";
    }

    private void Bind()
    {
        string sex = this.RadioButtonList1.SelectedValue;
        string pro = this.DropDownList3.Text;
        string city = this.DropDownList4.Text;
        string school = this.TextBox1.Text;
        this.quickPanel.Visible = false;
        this.quickresultPanel.Visible = true;
        int CPage;
        int temppage;
        List<UserMoreinfo> list = new List<UserMoreinfo>();
        list = utbll.GetSchool(ut.GetLogin().UserID, sex, pro, city, school);
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

        Toppage.Text = "<a href=SearchSchool.aspx?Currentpage=0>首页</a>";
        Nextpage.Text = "<a href=SearchSchool.aspx?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
        Downpage.Text = "<a href=SearchSchool.aspx?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
        Endpage.Text = "<a href=SearchSchool.aspx?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();
        for (int i = 1; i <= thispagenull; i++)
        {
            DropDownList1.Items.Add(i.ToString());
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("SearchSchool.aspx?Currentpage=" + this.DropDownList1.Text);
    }

}


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
using BDUBLL;
using System.Collections.Generic;
using BDUModel;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;


public partial class CategList : Page
{
    public string username;
    public  int  intervieweeID;
    private List<PicCateg> categ = new List<PicCateg>();
    protected string content; 
    B_User ubll = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
            intervieweeID = ubll.GetLogin().UserID;
            Bind();
        }
    }
    private void Bind()
    {
        int CPage;
        int temppage;
        PicCateg_BLL CategBll = new PicCateg_BLL();
        categ = CategBll.GetPicCategList(ubll.GetLogin().UserID, ubll.GetLogin().UserID, null);
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
        cc.DataSource = categ;
        cc.AllowPaging = true;
        cc.PageSize = 12;
        cc.CurrentPageIndex = CPage - 1;
        dltCategList.DataSource = cc;
        dltCategList.DataBind();

        Allnum.Text = categ.Count.ToString();
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

        Toppage.Text = "<a href=CategList.aspx?Currentpage=0>首页</a>";
        Nextpage.Text = "<a href=CategList.aspx?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
        Downpage.Text = "<a href=CategList.aspx?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
        Endpage.Text = "<a href=CategList.aspx?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();
        for (int i = 1; i <= thispagenull; i++)
        {
            DropDownList1.Items.Add(i.ToString());
        }

        if (dltCategList.Items.Count <= 0)
            content = "你还没有建立自己的相册！";
    }
    protected string GetUrl(Guid id)
    {
        if (id != null)
        {
            PicTure_BLL TureBll = new PicTure_BLL();
            return TureBll.GetPic(id).PicUrl;
        }
        else
        {
            return "../Images/PicIndex.GIF";
        }
    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("CategList.aspx?Currentpage=" + this.DropDownList1.Text);
    }
}

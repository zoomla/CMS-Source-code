using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Data;

public partial class manage_Question_ApplicationManage : CustomerPageAction
{
    protected B_User ull = new B_User();
    protected B_Recruitment rll = new B_Recruitment();
    protected B_EnrollList ell = new B_EnrollList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                ell.DeleteByGroupID(id);
            }
            DataTable tablelist = rll.GetRecruintmentall();
            if (tablelist != null)
            {
                Page_list(tablelist);
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li>培训资源库<a href='AddEnroll.aspx'>[添加招生信息]</a> <a href='javascript:void(0)' data-toggle=\"modal\" data-target=\"#TechUser_div\" onclick='open_window()'>[导入招生资料]</a></li>");
        }
    }

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        int CPage, temppage = 0;

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
        //Cll.DefaultView.Sort = "PubCreateTime desc";
        cc.DataSource = Cll.DefaultView;
        cc.AllowPaging = true;
        if (Request.QueryString["txtPage"] != null && Request.QueryString["txtPage"] != "")
        {
            cc.PageSize = DataConverter.CLng(Request.QueryString["txtPage"]);
        }
        if (Request.Form["txtPage"] != null && Request.Form["txtPage"] != "")
        {
            cc.PageSize = DataConverter.CLng(Request.Form["txtPage"]);
        }
        cc.CurrentPageIndex = CPage - 1;
        Repeater1.DataSource = cc;
        Repeater1.DataBind();

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

        this.Toppage.Text = "<a href=?Currentpage=0&txtPage=" + cc.PageSize.ToString() + ">首页</a>";
        this.Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + "&txtPage=" + cc.PageSize.ToString() + " >上一页</a>";
        this.Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + "&txtPage=" + cc.PageSize.ToString() + " >下一页</a>";
        this.Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + "&txtPage=" + cc.PageSize.ToString() + " >尾页</a>";
        this.Nowpage.Text = CPage.ToString();
        this.PageSize.Text = thispagenull.ToString();
        txtPage.Text = cc.PageSize.ToString();
        if (!this.Page.IsPostBack)
        {
            for (int i = 1; i <= thispagenull; i++)
            {
                this.DropDownList1.Items.Add(i.ToString());
            }
        }

    }
    #endregion 

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = rll.GetRecruintmentall();
        if (dt != null)
        {
            Page_list(dt);
        }
    }

    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        DataTable dt = rll.GetRecruintmentall();
        if (dt != null)
        {
            Page_list(dt);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string item = Request.Form["item"];
        if (item != null && item != "")
        {
            if (item.IndexOf(',') > -1)
            {
                string[] itemarr = item.Split(',');
                for (int i = 0; i < itemarr.Length; i++)
                {
                    rll.DeleteByGroupID(DataConverter.CLng(itemarr[i]));
                }
            }
            else
            {
                rll.DeleteByGroupID(DataConverter.CLng(item));
            }
        }
        function.WriteSuccessMsg("操作成功!", "ApplicationManage.aspx");
    }

    protected string GetUserName(string UserID)
    {
        M_UserInfo uinfo = ull.GetUserByUserID(DataConverter.CLng(UserID));
        M_Uinfo ubaseinfo = ull.GetUserBaseByuserid(DataConverter.CLng(UserID));
        return uinfo.UserName;
    }
    protected string getusercount(string userid)
    {
        return rll.GetRencount(DataConverter.CLng(userid)).Rows.Count.ToString();
    }

}
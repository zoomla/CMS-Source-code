using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Data;

public partial class manage_Question_QuestionTypeManage : CustomerPageAction
{
    private B_Exam_Type bqt = new B_Exam_Type ();
    private B_Admin badmin = new B_Admin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                bqt.GetDelete(id);
            }
            List<M_Exam_Type> dt = bqt.SelectAll();
            if (dt != null)
            {
                Page_list(dt);
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='QuestionManage.aspx'>考试管理</a></li> <li>题型管理</li>" + Call.GetHelp(78));
        }
    }

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(List<M_Exam_Type> Cll)
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
        cc.DataSource = Cll;
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
        if (CPage > cc.PageCount)
        {
            cc.CurrentPageIndex = 0;
            CPage = cc.PageCount;
        }
        Repeater1.DataSource = cc;
        Repeater1.DataBind();

        Allnum.Text = Cll.Count.ToString();
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
        this.DropDownList1.Items.Clear();
        for (int i = 1; i <= thispagenull; i++)
        {
            this.DropDownList1.Items.Add(i.ToString());
        }
        this.DropDownList1.SelectedValue = this.Nowpage.Text;
    }
    #endregion 

    public string GetType(string typeid)
    {
        //单选,多选,判断，填空,问答,组合
        switch (typeid)
        {
            case "1":
                return "单选题";
            case "2":
                return "多选题";
            case "3":
                return "判断题";
            case "4":
                return "填空题";
            case "5":
                return "问答题";
            case "6":
                return "组合题";
            default:
                return "";
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
                    bqt.GetDelete(DataConverter.CLng(itemarr[i]));
                }
            }
            else
            {
                bqt.GetDelete(DataConverter.CLng(item));
            }
        }
        function.WriteSuccessMsg("操作成功!", "QuestionTypeManage.aspx");
    }
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        List<M_Exam_Type> dt = bqt.SelectAll();
        if (dt != null)
        {
            Page_list(dt);
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<M_Exam_Type> dt = bqt.SelectAll();
        if (dt != null)
        {
            Page_list(dt);
        }
    }
}
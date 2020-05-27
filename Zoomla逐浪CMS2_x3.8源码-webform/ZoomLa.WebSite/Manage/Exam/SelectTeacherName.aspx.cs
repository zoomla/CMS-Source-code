using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data;

public partial class manage_Question_SelectTeacherName : CustomerPageAction
{
    private B_ExTeacher bet = new B_ExTeacher();
    private B_Exam_Class bqc = new B_Exam_Class ();

    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();       
        if (!IsPostBack)
        {
            DataTable dt = bet.Select_All();
            if (dt != null)
            {
                Page_list(dt);
            }
        }
        if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "select")
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            string name = Request.QueryString["name"];
            string scripttxt = "setvalue('TextBox1','" + name + "');";
            string scriptid = "setvalue('hfid','" + id + "');";
            function.Script(this,scriptid + scripttxt + ";parent.Dialog.close();");
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

    public string GetTeachClass(string classid)
    {
        M_Exam_Class mqc = bqc.GetSelect(DataConverter.CLng(classid));
        if (mqc != null && mqc.C_id > 0)
        {
            return mqc.C_ClassName;
        }
        else
        {
            return "";
        }
    }
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        DataTable dt = bet.Select_All();
        if (dt != null)
        {
            Page_list(dt);
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = bet.Select_All();
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
                    bet.DeleteByGroupID(DataConverter.CLng(itemarr[i]));
                }
            }
            else
            {
                bet.DeleteByGroupID(DataConverter.CLng(item));
            }
        }
        function.WriteSuccessMsg("操作成功!", "ExTeacherManage.aspx");
    }

    public string GetRemark(string remark)
    {
        if (remark.Length > 25)
        {
            return remark.Substring(0, 25) + "...";
        }
        else
        {
            return remark;
        }
    }
}
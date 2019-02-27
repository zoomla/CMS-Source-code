using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class manage_Exam_StudioInfoListByTech : CustomerPageAction
{
    protected B_Recruitment rll = new B_Recruitment();
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["id"] = Request.QueryString["id"]; 
        if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "delete")
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            int eid = rll.GetSelect(id).EnrolllistID;

            rll.DeleteByGroupID(id);
            Response.Redirect("StudioInfoListByTech.aspx?id=" + eid.ToString());
        }
        if (ViewState["id"] != null)
        {
            ViewState["page"] = "1";
            DataTable dt = rll.GetRecruintment(DataConverter.CLng(ViewState["id"]));
            if (dt != null)
            {
                Page_list(dt);
            }
        }
        Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>培训资料库<a href='AddStudioInfo.aspx?id=" + Request.QueryString["id"] + "'>[添加学员信息]</a> <a href='javascript:void(0)' data-toggle=\"modal\" data-target=\"#InputUser_div\" onclick='open_window()'>[导入招生资料]</a></li>");
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["id"] != null)
        {
            ViewState["page"] = "1";
            DataTable dt = rll.GetRecruintment(DataConverter.CLng(ViewState["id"]));
            if (dt != null)
            {
                Page_list(dt);
            }
        }
    }

    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        if (ViewState["id"] != null)
        {
            ViewState["page"] = "1";
            DataTable dt = rll.GetRecruintment(DataConverter.CLng(ViewState["id"]));
            if (dt != null)
            {
                Page_list(dt);
            }
        }
    }

    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        ViewState["tableinfo"] = Cll;
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ViewState["tableinfo"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page page = new Page();
            HtmlForm form = new HtmlForm();
            string titles = "学员姓名,预设用户名,预设登录密码,联系电话,联系地址,身份证号码,登记时间,备注";
            string[] usertit = titles.Split(',');
            DataTable viewtable = (DataTable)ViewState["tableinfo"];
            sb.Append(titles.Replace(",", "\t"));
            sb.Append("\n");
            for (int ii = 0; ii < viewtable.Rows.Count; ii++)
            {
                if (viewtable != null && viewtable.Rows.Count > 0)
                {
                    int sssid = DataConverter.CLng(viewtable.Rows[0]["ssid"]);
                    M_Recruitment rinfo = rll.GetSelect(sssid);
                    sb.Append(rinfo.Studioname + "\t");
                    sb.Append(rinfo.PriorUserName + "\t");
                    sb.Append(rinfo.LogPassWord + "\t");
                    sb.Append(rinfo.Tel + "\t");
                    sb.Append(rinfo.Addinfo + "\t");
                    sb.Append(rinfo.CradNo + "\t");
                    sb.Append(rinfo.WriteTime + "\t");
                    sb.Append(rinfo.Remark + "\t");     
                }
                sb.Append("\n");
            }
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Recruitment(" + DateTime.Now.ToString() + ").xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
    }
}
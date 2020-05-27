using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.SQLDAL;
using System.Data;
using ZoomLa.Common;
using System.Xml;

public partial class Manage_I_Main : CustomerPageAction
{
    private B_Search searchBll = new B_Search();
    private int pageSize = 12;
    private string[] colorArr = new string[] { "#852b99", "#4B7F8C", "#1E86EA", "#ffb848", "#00CCFF", "#FE7906", "#004B9B", "#74B512", "#A43AE3", "#22AFC2", "#808081", "#F874A4" };
    private string[] sizeArr = new string[] { "col-lg-2 col-md-3", "col-lg-1 col-md-1", "col-lg-2 col-md-3", "col-lg-4 col-md-4" };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["key"]))
            { 
                DataBind();
            }
            else
            {
                navDiv.Visible = false;
                pageDiv.Visible = true;
                DataBind2();
            }
        }
    }
    private void DataBind(string key = "")
    {
        string div = " <div class='{0} padding5 {1}'><div style='background:{2};'>{3}</div></div>";
        string href = "<a href='{0}' target='{1}'><span class='{2}'></span>{3}</a>";
        string hid = "hidden-xs hidden-sm";
        DataTable dt = searchBll.SelByType(1, customPath2, 1);
        int pageCount = PageCommon.GetPageCount(pageSize, dt.Rows.Count);
        for (int i = 0; i < pageSize; i++)
        {
            try
            {
                int index = (CPage - 1) * pageSize + i;
                string labelID = "Label" + (i + 1);
                DataRow dr = dt.Rows[index];

                string name = dr["name"].ToString();
                name = name.Length > 4 ? name.Substring(0, 3) : name;
                string openType = dr["OpenType"].ToString().Equals("0") ? "_self" : "_blank";
                string value = string.Format(href, dr["FileUrl"], openType, dr["ico"], dr["name"]);//链接字符串
                string size = sizeArr[DataConvert.CLng(dr["size"])];//默认为中
                string mobile = dr["Mobile"].ToString().Equals("1") ? "" : hid;//是否在小屏中显示
                model_Lit.Text += string.Format(div, size, mobile, colorArr[i], value);
            }
            catch { break; }
        }
        page_Lit.Text = CreatePaging(pageCount, CPage);
    }
    private void DataBind2()
    {
        EGV.DataSource = PageDT;
        EGV.DataBind();
    }
    /// <summary>
    /// 返回分页Html,从1开始计数
    /// </summary>
    /// <param name="pageCount">共有多少页</param>
    /// <param name="cPage">当前页</param>
    public string CreatePaging(int pageCount, int cPage)
    {
        if (pageCount<2) return"";
        #region 前台最终Html
        //<ul class="pagination">
        //<li class="disabled"><a href="?page=1">&laquo;</a></li>
        //<li class="active"><a href="?page=1">1 <span class="sr-only">(current)</span></a></li>
        //<li><a href="?page=2">2 <span class="sr-only"></span></a></li>
        //<li><a href="?page=last">&raquo;</a></li>
        //</ul>
        #endregion
        pageCount = pageCount < 1 ? 1 : pageCount;
        cPage = cPage < 1 ? 1 : cPage;
        string pageHtml = "<ul class='pagination'>";
        pageHtml += "<li " + (cPage > 1 ? "" : "class='disabled'") + "><a href='?page=1'>&laquo;</a></li>";
        for (int i = 1; i <= pageCount; i++)
        {
            pageHtml += "<li " + (cPage != i ? "" : "class='active'") + "><a href='?page=" + i + "'>" + i + " <span class='sr-only'>(current)</span></a></li>";
        }
        pageHtml += "<li><a href='?page=" + pageCount + "'>&raquo;</a></li></ul>";
        return pageHtml;
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind2();
    }
    public DataTable DecreateDT(DataTable dt,string key) 
    {
        dt.Columns.Add(new DataColumn("Index",typeof(int)));
        dt.Columns.Add(new DataColumn("DTitle",typeof(string)));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["Index"] = (i + 1);
            dt.Rows[i]["Url"]=dt.Rows[i]["Url"].ToString().Replace("/Manage/", customPath2);
            string title=dt.Rows[i]["Title"].ToString();
            dt.Rows[i]["DTitle"] = title.Replace(key, "<span style='color:red;'>" + key + "</span>");
        }
        return dt;
    }
    public DataTable PageDT 
    {
        get 
        {
            if (ViewState["PageDT"] == null)
            {
                string key = Request.QueryString["key"].Trim();
                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath("/Config/SiteMap.config"));
                DataTable dt = ds.Tables[0];
                dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
                dt = dt.DefaultView.ToTable();
                ViewState["PageDT"] = DecreateDT(dt,key);
            }
            return ViewState["PageDT"] as DataTable;
        }
    }
    public int CPage 
    {
        get 
        {
            return PageCommon.GetCPage();
        }
    }
}
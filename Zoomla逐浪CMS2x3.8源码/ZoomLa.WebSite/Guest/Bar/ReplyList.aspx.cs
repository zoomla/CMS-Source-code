using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.SQLDAL;

public partial class Plat_Blog_ReplyList : System.Web.UI.Page
{
    B_Guest_Bar barBll = new B_Guest_Bar();
    B_TempUser utBll = new B_TempUser();
    int replyPageSize = 5;
    int pid, pageSize, pageIndex;
    int UserID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserID = utBll.GetLogin().UserID;
            MyBind();
        }
    }
    public void MyBind() 
    {
        pid = Convert.ToInt32(Request.QueryString["Pid"]);
        pageSize = Convert.ToInt32(Request.QueryString["PageSize"]);
        pageIndex = Convert.ToInt32(Request.QueryString["PageIndex"]);
        DataTable dt = barBll.SelByPid(pid);
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows.Count > replyPageSize)
            {
                Literal pageHtml = new Literal(); string html = "";
                pageHtml.EnableViewState = false;
                ReplyList_RPT.DataSource = GetPageDT(replyPageSize, pageIndex, dt, out html);
                ReplyList_RPT.DataBind();
                pageHtml.Text = html;
                ReplyList_RPT.Items[(ReplyList_RPT.Items.Count - 1)].Controls.Add(pageHtml);
            }
            else
            {
                ReplyList_RPT.DataSource = dt;
                ReplyList_RPT.DataBind();
            }
        }//dt bind end;

    }
    public string GetMSG()
    {
        return StrHelper.DecompressString(Eval("MsgContent").ToString());
    }
    /// <summary>
    /// 从1开始
    /// </summary>
    public object GetPageDT(int pageSize, int pageIndex, DataTable dt, out string pageHtml)
    {
        //先临时实现，后再切换为直接取对应数据的
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.DataSource = dt.DefaultView;
        pds.PageSize = pageSize;
        pds.CurrentPageIndex = (pageIndex - 1);
        pageHtml = CreatePageHtml(GetPageCount(pageSize, dt.Rows.Count), pageIndex);
        return pds;
    }
    public int GetPageCount(int pageSize, int itemCount)
    {
        int pageCount = 0;
        if (pageSize > 0 && itemCount > 0)
            pageCount = itemCount / pageSize + ((itemCount % pageSize > 0) ? 1 : 0);
        return pageCount;
    }
    public string CreatePageHtml(int pageCount, int cPage)
    {
        #region 前台最终Html
        //<li class="disabled"><a href="?page=1">&laquo;</a></li>
        //<li class="active"><a href="?page=1">1 <span class="sr-only">(current)</span></a></li>
        //<li><a href="?page=2">2 <span class="sr-only"></span></a></li>
        //<li><a href="?page=last">&raquo;</a></li>
        //</ul>
        #endregion
        string pageHtml = "<ul class='pagination'>";
        pageHtml += "<li " + (cPage > 1 ? "" : "class='disabled'") + "><a href='javascript:;' onclick='LoadReply(" + pid + "," + pageSize + "," + 1 + ");'>&laquo;</a></li>";
        for (int i = 1; i <= pageCount; i++)
        {
            pageHtml += "<li " + (cPage != i ? "" : "class='active'") + "><a href='javascript:;' onclick='LoadReply(" + pid + "," + pageSize + "," + i + ")';>"+i+"<span class='sr-only'>(current)</span></a></li>";
        }
        //pageHtml += "<li><a href='?page=" + pageCount + "' onclick='LoadReply("+pid+","+pageSize+","+pageIndex+");'>&raquo;</a></li></ul>";
        pageHtml += "<li><a href='javascript:;' onclick='LoadReply(" + pid + "," + pageSize + "," + pageCount + ");'>&raquo;</a></li></ul>";
        return pageHtml;
    }
    public string GetDel()
    {
        int uid = DataConvert.CLng(Eval("CUser"));
        if (UserID == uid)
        {
            return " <a href='javascript:;' onclick='PostDelMsg(" + Eval("ID") + ")' style='color:#999;'>删除|</a>";
        }
        else return "";
    }
    public string GetUserFace()
    {
        return Eval("UserFace") == DBNull.Value ? "" : Eval("UserFace").ToString();
    }
    public string GetUName()
    {
        return barBll.GetUName(Eval("HoneyName"),Eval("CUName"));
    }
}
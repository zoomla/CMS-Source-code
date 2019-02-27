using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;


public partial class User_Order_ShareReply : System.Web.UI.Page
{
    /*
     * 1,回复时匿名失效(不需要,回复主信息本就不显示名称,回复不可匿名)
     */
    B_Order_Share shareBll = new B_Order_Share();
    int pid, psize, cpage;
    public string Mode { get { return Request.QueryString["mode"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        pid = Convert.ToInt32(Request.QueryString["Pid"]);
        psize = Convert.ToInt32(Request.QueryString["psize"]);
        cpage = Convert.ToInt32(Request.QueryString["page"]);
        int itemCount = 0;
        DataTable dt = shareBll.SelByPid(psize, cpage, out itemCount, pid,0);
        int pageCount = PageHelper.GetPageCount(itemCount, psize);
        if (pageCount > 1)
        {
            Literal pageHtml = new Literal();
            pageHtml.EnableViewState = false;
            pageHtml.Text = CreatePageHtml(pageCount, cpage);
            ReplyList.DataSource = dt;
            ReplyList.DataBind();
            ReplyList.Items[(ReplyList.Items.Count - 1)].Controls.Add(pageHtml);
        }
        else
        {
            ReplyList.DataSource = dt;
            ReplyList.DataBind();
        }
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
        string pageHtml = "<ul class='pagination'>";
        pageHtml += "<li " + (cPage > 1 ? "" : "class='disabled'") + "><a href='javascript:;' onclick='LoadReply(" + pid + "," + psize + "," + 1 + ");'>&laquo;</a></li>";
        for (int i = 1; i <= pageCount; i++)
        {
            pageHtml += "<li " + (cPage != i ? "" : "class='active'") + "><a href='javascript:;' onclick='LoadReply(" + pid + "," + psize + "," + i + ")';>" + i + "<span class='sr-only'>(current)</span></a></li>";
        }
        pageHtml += "<li><a href='javascript:;' onclick='LoadReply(" + pid + "," + psize + "," + pageCount + ");'>&raquo;</a></li></ul>";
        return pageHtml;
    }
    public string GetReplyStr()
    {
        string result = " ：";
        if (DataConvert.CLng(Eval("ReplyID")) != DataConvert.CLng(Eval("Pid")))
        {
            string uname = B_User.GetUserName(Eval("RHoney"), Eval("RUName"));
            result = " 回复 " + uname + "：";
        }
        return result;
    }
    public string GetUserName()
    {
        return B_User.GetUserName(Eval("CHoney"),Eval("CUName"));
    }

    protected void MsgRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = e.Item.DataItem as DataRowView;
            int pid= Convert.ToInt32(dr["ID"]);
            if (Mode.ToLower().Equals("admin"))
            {
                HtmlButton btnhtml = e.Item.FindControl("Del_Btn") as HtmlButton;
                btnhtml.Attributes.Add("data-id", pid.ToString());
                btnhtml.Visible = true;
                HtmlAnchor ahtml = e.Item.FindControl("Edit_A") as HtmlAnchor;
                ahtml.HRef = "/" + SiteConfig.SiteOption.ManageDir + "/Shop/EditShare.aspx?id=" + pid;
                ahtml.Visible = true;
            }
        }
    }
}
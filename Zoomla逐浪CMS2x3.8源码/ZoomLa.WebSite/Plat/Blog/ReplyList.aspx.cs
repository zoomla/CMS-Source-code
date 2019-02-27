using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;

public partial class Plat_Blog_ReplyList : System.Web.UI.Page
{
    B_Blog_Msg msgBll = new B_Blog_Msg();
    //int replyPageSize = 5;
    int pid, pageSize, pageIndex;
    public int UserID
    {
        get
        {
            if (Session["Plat_Default_UserID"] == null || Convert.ToInt32(Session["Plat_Default_UserID"]) == 0)
            {
                Session["Plat_Default_UserID"] = B_User_Plat.GetLogin().UserID;
            }
            return Convert.ToInt32(Session["Plat_Default_UserID"]);
        }
    }
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
        pageSize = 20;// Convert.ToInt32(Request.QueryString["PageSize"]);
        pageIndex = Convert.ToInt32(Request.QueryString["PageIndex"]);
        int pageCount = 0;
        //不限,内部分
        DataTable dt = msgBll.SelByPid(1000, pageIndex, out pageCount, pid);
        dt.DefaultView.RowFilter = "Source='bar'";
        DataTable barDT = dt.DefaultView.ToTable();
        dt.DefaultView.RowFilter = "Source='plat'";
        dt.DefaultView.Sort = "CDate asc";
        DataTable platDT = dt.DefaultView.ToTable();
        PagedDataSource ds = (PagedDataSource)PageHelper.GetPageDT(pageSize, pageIndex, platDT, out pageCount);
        if (barDT.Rows.Count > 0)
        {
            bar_div.Visible = true;
            Bar_RPT.DataSource = barDT;
            Bar_RPT.DataBind();
        }
        if (pageCount > 1)
        {
            Literal pageHtml = new Literal();
            pageHtml.EnableViewState = false;
            pageHtml.Text = CreatePageHtml(pageCount, pageIndex);
            ReplyList_RPT.DataSource = ds;
            ReplyList_RPT.DataBind();
            ReplyList_RPT.Items[(ReplyList_RPT.Items.Count - 1)].Controls.Add(pageHtml);
        }
        else
        {
            ReplyList_RPT.DataSource = ds;
            ReplyList_RPT.DataBind();
        }
    }
    public string GetReplyStr(object rid)
    {
        string msg = Eval("MsgContent").ToString();
        switch (Eval("Source").ToString())
        {
            case "bar":
                msg = StrHelper.DecompressString(msg);
                break;
        }
        string result = "<a href='/Plat/Blog/?Uids=" + Eval("CUser") + "' title='查看他的工作流'>" + GetUName() + "</a>";
        if (Convert.ToInt32(rid) != 0)
        {
            result += "回复" + Eval("ReplyUName") + "<span class='fa fa-comment'></span>：" + msg;
        }
        else
        {
            result += "<span class='fa fa-comment'></span>：" + msg;
        }
        return result;
    }
    public string CreatePageHtml(int pageCount, int cPage)
    {
        string pageHtml = "<ul class='pagination'>";
        pageHtml += "<li " + (cPage > 1 ? "" : "class='disabled'") + "><a href='javascript:;' onclick='LoadReply(" + pid + "," + pageSize + "," + 1 + ");'>&laquo;</a></li>";
        for (int i = 1; i <= pageCount; i++)
        {
            pageHtml += "<li " + (cPage != i ? "" : "class='active'") + "><a href='javascript:;' onclick='LoadReply(" + pid + "," + pageSize + "," + i + ")';>"+i+"<span class='sr-only'>(current)</span></a></li>";
        }
        pageHtml += "<li><a href='javascript:;' onclick='LoadReply(" + pid + "," + pageSize + "," + pageCount + ");'>&raquo;</a></li></ul>";
        return pageHtml;
    }
    public string GetUName()
    {
        return string.IsNullOrEmpty(Eval("HoneyName", "")) ? Eval("UserName", "") : Eval("HoneyName", "");
    }
    string attachTlp = "<div class='imgview' onclick='PreView(\"{0}\");'><div class='ext {1}'></div><div class='fname'>{2}</div></div>";
    string imgTlp = "<a class='athumbnail_img' rel='group_{0}' href='{1}'><img src='{1}' class='thumbnail_img'/></a>";
    public string GetAttach()
    {
        string imgresult = "", attachresult = "";
        string attach = Eval("Attach").ToString();
        int max = 3;
        foreach (string file in attach.Split('|'))
        {
            if (string.IsNullOrEmpty(file)) continue;
            if (ZoomLa.Safe.SafeC.IsImage(file) && max > 0)//jpg,png,gif图片显示预览,只显示前三张
            {
                imgresult += string.Format(imgTlp, Eval("ID"), file); max--;
            }
            else
            {
                string fname = Path.GetFileName(file); fname = fname.Length > 6 ? fname.Substring(0, 6) + ".." : fname;
                attachresult += string.Format(attachTlp, file, Path.GetExtension(file).ToLower().Replace(".", ""), fname);
            }
        }
        imgresult = string.IsNullOrEmpty(imgresult) ? "" : "<div class='thumbnail_div'>" + imgresult + "</div>";
        return imgresult + attachresult;
    }

}
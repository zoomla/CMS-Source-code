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
using BDUModel;
using System.Collections.Generic;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class MemoContext : Page
{
    Memo_BLL memobll = new Memo_BLL();
    List<UserMemo> usermemo = new List<UserMemo>();
    B_User ubll = new B_User();
    private Dictionary<string, string> Order
    {
        get
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("MemoTime", "0");
            return ht;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    { 
        ubll.CheckIsLogin(); 
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
            ViewState["id"] = Request.QueryString["ID"].ToString();
            PagePagination pagepag = new PagePagination();
            pagepag.PageIndex = 1;
            pagepag.PageOrder = Order;
            pagepag.PageSize = memobll.GetMemoList(ubll.GetLogin().UserID, null).Count;
            //Response.Write(SQLstr + ubll.GetLogin().UserID); Response.End();
            usermemo = memobll.GetMemoList(ubll.GetLogin().UserID, pagepag);
 
            this.labCount.Text = "共" + pagepag.PageSize + "条记录";
            int i = -1;
            foreach (UserMemo um in usermemo)
            {
                i++;
                if (um.ID.ToString() == ViewState["id"].ToString())
                    break;
            }
            tdTitle.InnerHtml = "<FONT size='4' color='#000000'><STRONG>" + usermemo[i].MemoTitle.ToString() + "</STRONG></FONT>";
            tdPostTime.InnerHtml = "<FONT color='#cc0000'>时间：" + usermemo[i].MemoTime.ToShortDateString().ToString() + "</FONT>";
            tdContent.InnerHtml = usermemo[i].MemoContext.ToString();
            if ((i - 1) >= 0)
                this.tdUp.InnerHtml = "上一篇：<a href='MemoContext.aspx?ID=" + usermemo[i - 1].ID.ToString() + "'>" + usermemo[i - 1].MemoTitle.ToString() + "</a>";
            else
                this.tdUp.InnerHtml = "上一篇：没有备忘录";
            if ((i + 1) < usermemo.Count)
                this.tdDown.InnerHtml = "下一篇：<a href='MemoContext.aspx?ID=" + usermemo[i + 1].ID.ToString() + "'>" + usermemo[i + 1].MemoTitle.ToString() + "</a>";
            else
                this.tdDown.InnerHtml = "下一篇：没有备忘录";
        }

    }
}


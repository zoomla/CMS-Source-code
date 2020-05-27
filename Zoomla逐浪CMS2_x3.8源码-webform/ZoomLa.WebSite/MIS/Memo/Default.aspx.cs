using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Data.SqlClient;

public partial class MIS_Memo : System.Web.UI.Page
{
    protected B_User buser = new B_User();
    B_MisInfo bll = new B_MisInfo();
    M_MisInfo model = new M_MisInfo();
    B_Comment Bcom = new B_Comment();
    M_Comment Mcom = new M_Comment();
    B_User user = new B_User();
    M_UserInfo ui = new M_UserInfo();
    DataTable dt=new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    protected List<M_MisInfo> GetDataSource()
    {
        List<M_MisInfo> memos = new List<M_MisInfo>();
        List<M_Comment> Mcomment = new List<M_Comment>();
        if (Request.QueryString["UName"] != null)
        {
            ltlTitle.Text = "我的备忘";
            memos = bll.GetUserMemos(Request.QueryString["UName"]);
        }
        else if (Request.QueryString["Sname"] != null)
        {
            ltlTitle.Text = "共享给我";
            memos = bll.GetSharedMemos(Request.QueryString["Sname"]);
        }
        else if (Request.QueryString["Mname"] != null)
        {
            ltlTitle.Text = "我的评论";
            this.rptMemos.Visible = false;
            this.rptComm.Visible = true;
            this.rptComm.DataSource = DataBindComment();
            this.rptComm.DataBind();
        }
        else
        {
            ltlTitle.Text = "全部备忘";
            memos = bll.GetAllLastestMemos();
        }
        return memos;
    }

    protected void DateBindRepToday()
    {
       
    }

    protected DataTable DataBindComment()
    {
        DataTable dt = new DataTable();
        ui=buser.GetUserIDByUserName(buser.GetLogin().UserName);
        dt = Bcom.SearchByUser(ui.UserID,0);
        return dt;
    }

    //分页数据源的绑定
    protected void PagedSourceBind()
    {
        int pageSize = Convert.ToInt32(ViewState["PageSize"]);
        int curntNum = Convert.ToInt32(ViewState["CurntPage"]);
        List<M_MisInfo> lstMemos = GetDataSource();
        DataTable dt = DataBindComment();
        if (lstMemos.Count <= 0&&dt.Rows.Count<=0)
        {
            divEmpty.Style["display"] = "block";
            divPager.Style["display"] = "none";
            return;
        }
        else
        {
            divPager.Style["display"] = "block";
            divEmpty.Style["display"] = "none";
        }
        PagedDataSource pdsMemos = new PagedDataSource();
        pdsMemos.AllowPaging = true;
        pdsMemos.DataSource = lstMemos;
        pdsMemos.PageSize = pageSize;

        pdsMemos.CurrentPageIndex = curntNum - 1;
        int totalCnt = pdsMemos.DataSourceCount;
        ViewState["TotalCnt"] = totalCnt;
        if (totalCnt <= 0)
        {
            divPager.Style["display"] = "none";
            return;
        }
        int pages = pdsMemos.PageCount;
        ViewState["Pages"] = pages;
        ListDataBind();
        lblTotalCnts.Text = totalCnt.ToString();

        // 分页链接
        if (pdsMemos.IsFirstPage)
            lbtnPrePage.Enabled = false;
        else
            lbtnPrePage.Enabled = true;
        if (pdsMemos.IsLastPage)
            lbtnNextPage.Enabled = false;
        else
            lbtnNextPage.Enabled = true;
        rptMemos.DataSource = pdsMemos;
        rptMemos.DataBind();
    }
    //截取长字符串
    protected string CutTitle(string title)
    {
        if (title.Length > 30)
            return title.Substring(0, 30) + "....";
        return title;
    }
    protected void rptMemos_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //if (e.Item.ItemType != ListItemType.Item)
        //    return;
        int mid = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Detail":
                Response.Redirect("MemoDetail.aspx?MID=" + mid);
                break;
            case "Edit":
                Response.Redirect("AddMemo.aspx?ID=" + mid);
                break;
            case "Delete":
                bll.Del(mid);
                Response.Redirect(Request.Url.AbsolutePath);
                break;
            case "Comment":
                break;
            default:
                break;
        }
    }

    protected void RepToday_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int mid = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Detail":
                Response.Redirect("MemoDetail.aspx?MID=" + mid);
                break;
        }
    }

    // 下拉页码的绑定
    public void ListDataBind()
    {
        int curntNum = Convert.ToInt32(ViewState["CurntPage"]);
        int pages = Convert.ToInt32(ViewState["Pages"]);
        List<int> lstNums = new List<int>();
        for (int i = 1; i <= pages; i++)
        {
            lstNums.Add(i);
        }
        ddlPages.DataSource = lstNums;
        ddlPages.DataBind();
        ddlPages.SelectedIndex = curntNum - 1;
    }
    //底部分页导航
    protected void LbtnAlterPage_Click(object sender, EventArgs e)
    {
        int curntNum = Convert.ToInt32(ViewState["CurntPage"]);
        int pages = Convert.ToInt32(ViewState["Pages"]);
        LinkButton lbtn = sender as LinkButton;
        switch (lbtn.CommandName)
        {
            case "First":
                ViewState["CurntPage"] = 1;
                break;
            case "Previous":
                curntNum--;
                if (curntNum < 1)
                    curntNum = 1;
                ViewState["CurntPage"] = curntNum;
                break;
            case "Next":
                curntNum++;
                if (curntNum > pages)
                    curntNum = pages;
                ViewState["CurntPage"] = curntNum;
                break;
            case "Last":
                ViewState["CurntPage"] = pages;
                break;
        }
        PagedSourceBind();
    }
    protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["CurntPage"] = (sender as DropDownList).SelectedValue;
        PagedSourceBind();
    }
    protected void BtnComment_Click(object sender, EventArgs e)
    { 
        int id = Convert.ToInt32(HidCommTxt.Value); 
        model = bll.SelReturnModel(id);
        ui = user.GetUserIDByUserName(buser.GetLogin().UserName);
        Mcom.Title = model.Title;
        Mcom.GeneralID = model.ID;
        Mcom.CommentTime = DateTime.Now;
        Mcom.Contents = this.TxtComment.Text;
        Mcom.UserID = ui.UserID;
        Mcom.Type = 3;
        if (Bcom.insert(Mcom) > 0)
        {
            Response.Write("<script>alert('评论成功');location.href='Default.aspx'</script>");
        }
        else
        {
            Response.Write("<script>alert('评论失败');location.href='Default.aspx'</script>");
        }
    }

    protected string getcout(int pid)
    {
        return "";
    }
    protected void BtnSub_Click(object sender, EventArgs e)
    {
        
    }

    protected void BindRep()
    {
      
    }

    protected string getUserInfo(string uid)
    {
        B_User buser = new B_User();
        M_UserInfo mu = buser.GetSelect(DataConverter.CLng(uid));
        string str = "";
        if (mu != null && !mu.IsNull)
            str = mu.UserName;
        return str;
    }

    protected string getUserface(string uid)
    {
        B_User buser = new B_User();
        return buser.GetSelect(DataConverter.CLng(uid)).UserFace;
    }

    protected void Repeater1_ItemCommmand(object sender, RepeaterCommandEventArgs e)
    {
       
    }

    protected string SelectNameByID(string id)
    {
      Response.Write("<script>alert('"+id+"');</script>");
      DataTable dtss = buser.SelectUserByIds(id);
      return dtss.Rows[0]["UserName"].ToString();
    }

    protected void LinkMore_Click(object sender, EventArgs e)
    {
       
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(this.HidCommTxt.Value);
        Response.Write("<script>alert('"+id+"');</script>");
        Response.Redirect("MemoDetail.aspx?MID="+id+"");
    }
}
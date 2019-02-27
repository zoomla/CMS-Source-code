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
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Collections.Generic;
using ZoomLa.BLL.Message;
using ZoomLa.Model.Message;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

public partial class Manage_I_Guest_WdCheck : CustomerPageAction
{
    B_Ask askBll = new B_Ask();
    B_GuestAnswer answerBll = new B_GuestAnswer();
    B_Favorite favBll = new B_Favorite();
    B_GuestBookCate cateBll = new B_GuestBookCate();
    public int Status { get { return DataConvert.CLng(Request.QueryString["Status"]); } }
    public int QueType { get { return DataConvert.CLng(Request.QueryString["QueType"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "AskManage");
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Content/ContentManage.aspx'>内容管理</a></li><li class='active'><a href='" + Request.RawUrl + "'>问答管理</a></li>");
        }
    }
    public void MyBind()
    {
        Egv.DataSource = askBll.Search(QueType,Status,0,Key_T.Text);
        Egv.DataBind();
    }
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    public string getcommend(object aa)
    {
        string aaa = aa.ToString();
        switch (aaa)
        {
            case "1":
                return "已审核";

            case "0":
                return "待审核";
             case "2":
                return "已解决";
            //case "3":
            //    return "新版本待审";
            default:
                return "待审核";
        }
    }
    protected string gettj(string v)
    {
        string a = v.ToString();
        switch (a)
        { 
            case "0":
                return "未推荐";
            case "1":
                return "推荐";
            default:
                return "未推荐";
        }
    }
    //删除选定的问题 
    protected void BtnSubmit1_Click(object sender, EventArgs e)
    {
        string[] idArr = Request.Form["idchk"].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < idArr.Length; i++)
        {
            int itemID = Convert.ToInt32(idArr[i]);
            askBll.Del(itemID);
            answerBll.DelByQueID(itemID);
        }
        MyBind();
    }
    // 审核通过选定的问题
    protected void BtnSubmit2_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (SafeSC.CheckIDS(ids)) { askBll.UpdateByField("Status", "1", ids); }
        MyBind();
    }
    //取消审核选定的词条
    protected void BtnSubmit3_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (SafeSC.CheckIDS(ids)) { askBll.UpdateByField("Status", "0", ids); }
        MyBind();
    }
    // 批量推荐问题
    protected void BtnSubmit4_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (SafeSC.CheckIDS(ids)) { askBll.UpdateByField("Elite", "1", ids); }
        MyBind();
    }
    // 取消推荐问题
    protected void BtnSubmit5_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (SafeSC.CheckIDS(ids)) { askBll.UpdateByField("Elite", "0", ids); }
        MyBind();
    }
    protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
    {
        int Id = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Audit")
        {
            M_Ask askMod = askBll.SelReturnModel(Id);
            if (askMod.Status == 0)
            {
                askBll.UpdateByField("Status", "1", Id);
            }
            else
            {
                askBll.UpdateByField("Status", "0", Id);
            }
        }
        if (e.CommandName == "Del")
        {
            askBll.Del(Id);
            DataTable dt = answerBll.Sel(" QueId=" + Id, "", null);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    answerBll.Del(" ID=" + ID);
                }
            }
        }
        MyBind();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvr = (GridViewRow)lb.NamingContainer;
        Egv.EditIndex = gvr.RowIndex;
        MyBind();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvr = (GridViewRow)lb.NamingContainer;
        string Id = Egv.DataKeys[gvr.RowIndex].Value.ToString();
        cateBll.Del(DataConverter.CLng(Id));
        MyBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        MyBind();
    }    
    /// <summary>
    /// 绑定的行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='tdbgmouseover';");
            e.Row.Attributes.Add("onmouseout", "this.className='tdbg';");
            e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";
        }
    }
    protected string gettype(string id)
    {
        DataTable dt = Sql.Sel("zl_grade", " GradeID=" + id, "");
        if (dt.Rows.Count > 0)
        {
            string name;
            name = (dt.Rows[0]["GradeName"]).ToString();
            return name;
        }
        else
            return "";
    }
    public string GetContent(string s) 
    {
        return s.Length > 100 ? s.Substring(0, 20) + "..." : s;
    }
    protected void SetLike_B_Click(object sender, EventArgs e)
    {
        AddLikeQues(QuesId_Hid.Value);
        function.WriteSuccessMsg("收藏成功");
    }
    //批量添加问题收藏
    private void AddLikeQues(string qids)
    {
        DataTable userDt = JsonHelper.JsonToDT(CurUser_Hid.Value);
        string[] qidattr = qids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (DataRow item in userDt.Rows)
        {
            for (int i = 0; i < qidattr.Length; i++)
            {
                int userid = Convert.ToInt32(item["UserID"]);
                int qid = Convert.ToInt32(qidattr[i]);
                M_Favorite favMod = new M_Favorite();
                favMod.InfoID = qid;
                favMod.Owner = userid;
                favMod.Title = askBll.SelReturnModel(qid).Qcontent;
                favMod.FavoriType = 4;
                favMod.FavUrl = "/Guest/Ask/SearchDetails.aspx?soure=manager&ID=" + qid;
                favMod.AddDate = DateTime.Now;
                favBll.insertQues(favMod);
            }
        }
    }
    protected void BtnQuest_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            AddLikeQues(Request.Form["idchk"]);
            function.WriteSuccessMsg("批量收藏成功!");
        }
    }
}
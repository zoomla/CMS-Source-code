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
using System.Data.SqlClient;

public partial class Manage_I_Guest_BkCheck : CustomerPageAction
{
    B_Content bll = new B_Content();
    B_User buser = new B_User();
    B_Admin badmin = new B_Admin();
    B_Baike bkBll = new B_Baike();
    M_Baike m_Baike = new M_Baike();
    B_BaikeEdit editBll = new B_BaikeEdit();
    B_GuestBookCate cateBll = new B_GuestBookCate();
    public int type { get { return string.IsNullOrEmpty(Request.QueryString["type"]) ? -100 : DataConverter.CLng(Request.QueryString["type"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "BaikeManage");
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Content/ContentManage.aspx'>内容管理</a></li><li><a href='WdCheck.aspx'>百科问答</a></li><li class='active'><a href='" + Request.RawUrl + "'>百科词条管理</a></li>");
            MyBind();
        }
    }
    private void MyBind(string key = "")
    {
        DataTable dts = bkBll.SelectAll(type,key);
        Egv.DataSource = dts;
        Egv.DataBind();
    }
    public string getcommend()
    {
        return editBll.GetStatus(DataConverter.CLng(Eval("Status")));
    }
    public string getElite(object aa)
    {
        string aaa = aa.ToString();
        switch (aaa)
        {
            case "0":
                return "未推荐";
            case "1":
                return "推荐";
            case "2":
                return "特色";
            case "3":
                return "每日图片";
            default:
                return "未推荐";
        }
    }
    protected void BtnSubmit1_Click(object sender, EventArgs e)
    {
        //删除选定的评论     
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            bkBll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }
    protected void BtnSubmit2_Click(object sender, EventArgs e)
    {
        //审核通过选定的词条
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            for (int i = 0; i < chkArr.Length; i++)
            {
                int itemID = Convert.ToInt32(chkArr[i]);
                ChangeUserExp(bkBll.SelReturnModel(itemID));
                bkBll.UpdateStatus(1, itemID);
            }
            MyBind();
        }
    }
    //给创建用户增加分数
    public void ChangeUserExp(M_Baike model)
    {
        if (model.Status != 1 && GuestConfig.GuestOption.BKOption.CreatePoint > 0)
        {
            M_UserInfo mu = buser.SelReturnModel(model.UserId);
            int point = GuestConfig.GuestOption.BKOption.CreatePoint;
            buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis()
            {
                score = point,
                ScoreType = (int)((M_UserExpHis.SType)(Enum.Parse(typeof(M_UserExpHis.SType), GuestConfig.GuestOption.BKOption.PointType))),
                detail = mu.UserName + "创建了词条[" + model.Tittle + "],增加奖励:" + point
            });
        }
    }
    //推荐用户增加分数
    public void ChangeUserElid(M_Baike model)
    {
        if (model.Elite != 1 && GuestConfig.GuestOption.BKOption.RemmPoint > 0)
        {
            M_UserInfo mu = buser.SelReturnModel(model.UserId);
            int point = GuestConfig.GuestOption.BKOption.RemmPoint;
            buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis()
            {
                score = point,
                ScoreType = (int)((M_UserExpHis.SType)(Enum.Parse(typeof(M_UserExpHis.SType), GuestConfig.GuestOption.BKOption.PointType))),
                detail = mu.UserName + "创建的词条[" + model.Tittle + "]被管理员设为推荐,增加奖励:" + point
            });
        }
    }
    protected void BtnSubmit3_Click(object sender, EventArgs e)
    {
        //取消审核选定的词条
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            for (int i = 0; i < chkArr.Length; i++)
            {
                int itemID = Convert.ToInt32(chkArr[i]);
                bkBll.UpdateStatus(0, itemID);
            }
            MyBind();
        }
    }
    protected void BtnSubmit4_Click(object sender, EventArgs e)
    {
        //推荐词条
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            for (int i = 0; i < chkArr.Length; i++)
            {
                int itemID = Convert.ToInt32(chkArr[i]);
                ChangeUserElid(bkBll.SelReturnModel(itemID));
                bkBll.UpdateElite(1, itemID);
            }
            MyBind();
        }
    }
    protected void BtnSubmit5_Click(object sender, EventArgs e)
    {
        //取消推荐词条
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            for (int i = 0; i < chkArr.Length; i++)
            {
                int itemID = Convert.ToInt32(chkArr[i]);
                bkBll.UpdateElite(0, itemID);
            }
            MyBind();
        }
    }
    protected void BtnSubmit6_Click(object sender, EventArgs e)
    {
        //每日图片
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            for (int i = 0; i < chkArr.Length; i++)
            {
                int itemID = Convert.ToInt32(chkArr[i]);
                bkBll.UpdateElite(3, itemID);
            }
            MyBind();
        }
    }
    protected void BtnSubmit7_Click(object sender, EventArgs e)
    {
        //取消每日图片
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            for (int i = 0; i < chkArr.Length; i++)
            {
                int itemID = Convert.ToInt32(chkArr[i]);
                bkBll.UpdateElite(0, itemID);
            }
            MyBind();
        }
    }
    protected void BtnSubmit8_Click(object sender, EventArgs e)
    {
        //特色
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            for (int i = 0; i < chkArr.Length; i++)
            {
                int itemID = Convert.ToInt32(chkArr[i]);
                bkBll.UpdateElite(2, itemID);
            }
            MyBind();
        }
    }
    protected void BtnSubmit9_Click(object sender, EventArgs e)
    {
        //取消特色
        string[] chkArr = GetChecked();
        if (chkArr != null)
        {
            for (int i = 0; i < chkArr.Length; i++)
            {
                int itemID = Convert.ToInt32(chkArr[i]);
                bkBll.UpdateElite(0, itemID);
            }
            MyBind();
        }
    }
    protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
    {
        int id = DataConverter.CLng(e.CommandArgument.ToString());
        if (e.CommandName == "Audit")
        {
            bkBll.UpdateStatus(1, id);
            //Response.Redirect("BaikeCheck.aspx?action=manage&id="+id);
        }
        if (e.CommandName == "Del")
        {
            bkBll.Del(id);
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
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.MyBind();
    }
    /// <summary>
    /// 绑定的行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";
        }
    }
    protected string getsta(string i)
    {
        if (i != "")
        {
            return "已合并";
        }
        else
        {
            return "未合并";
        }
    }
    private string[] GetChecked()
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            string[] chkArr = Request.Form["idchk"].Split(',');
            return chkArr;
        }
        else
            return null;
    }
    //protected void BtnSearch_Click(object sender, EventArgs e)
    //{
    //    string LabelName = this.TxtLableName.Text.Trim();
    //    Response.Redirect("BKCheck.aspx?lblkey=" + LabelName);
    //}
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        string LabelName = this.Key_T.Text.Trim();
        MyBind(LabelName);
    }
}
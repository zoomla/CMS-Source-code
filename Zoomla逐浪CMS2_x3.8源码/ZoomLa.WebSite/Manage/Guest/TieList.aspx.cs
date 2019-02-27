using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Model.Message;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.Message;

public partial class Manage_I_Guest_TieList : System.Web.UI.Page
{
    B_Guest_Bar barBll = new B_Guest_Bar();
    B_User buser = new B_User();
    B_Blog_Msg msgBll = new B_Blog_Msg();
    B_GuestBookCate cateBll = new B_GuestBookCate();
    B_Guest_Medals medalbll = new B_Guest_Medals();

    public int CateID
    {
        get
        {
            return string.IsNullOrEmpty(Request.QueryString["CateID"]) ? 0 : DataConverter.CLng(Request.QueryString["CateID"]);
        }
    }
    public int BarStatus
    {
        get
        {
            return string.IsNullOrEmpty(Request.QueryString["status"]) ? -10 : DataConverter.CLng(Request.QueryString["status"]);
        }
    }
    public string SKey
    {
        get
        {
            return ViewState["Skey"] as string;
        }
        set { ViewState["Skey"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.HdnCateID.Value = CateID.ToString();
            if (!string.IsNullOrEmpty(Request.QueryString["type"]) && Request.QueryString["type"].Equals("del"))
            {
                barBll.DelByCid(CateID);
            }
            SKey = Request.QueryString["Skey"];
            MyBind();
            M_GuestBookCate cateMod = cateBll.SelReturnModel(CateID);
            string catename = cateMod == null ? "全部帖子" : cateMod.CateName;
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx" + "'>"
                                + "工作台</a></li><li><a href='GuestCateMana.aspx?Type=1'>贴吧版面</a></li><li class='active'><a href='TieList.aspx?CateID=" + CateID + "'>贴子列表：</a>"
                                + catename + "[<a href='TieList.aspx?" + (BarStatus != (int)ZLEnum.ConStatus.Recycle ? "&status=" + (int)ZLEnum.ConStatus.Recycle : "") + "'>" + (BarStatus != (int)ZLEnum.ConStatus.Recycle ? "回收站" : "返回") + "</a>]</li>");
        }
    }
    public void MyBind()
    {
        DataTable dt = cateBll.Cate_SelByType(M_GuestBookCate.TypeEnum.PostBar);
        string ids = "";
        if (CateID > 0)
        {
            BindItem(dt, CateID, ref ids);
            BindDrown(dt);
            ids = ids.TrimEnd(',');
        }
        //----绑定数据
        DataTable item = new DataTable();
        if (BarStatus == (int)ZLEnum.ConStatus.Recycle)
        {
            Rels.Visible = true;
            DelAll.Visible = true;
            item = barBll.SelByStatus(BarStatus);
        }
        else
        {
            Opiton_Span.Visible = true;
            item = barBll.SelByCateIDS(ids, SKey, BarStatus);
        }
        Egv1.DataSource = item;
        Egv1.DataBind();
    }
    void BindItem(DataTable dt, int cateid, ref string ids)
    {
        ids += cateid.ToString() + ",";
        DataRow[] drs = dt.Select("ParentID=" + cateid);
        foreach (DataRow dr in drs)
        {
            BindItem(dt, Convert.ToInt32(dr["CateID"]), ref ids);
        }
    }
    public void BindDrown(DataTable dt, int pid = 0, int layer = 0)
    {
        DataRow[] drs = dt.Select("ParentID=" + pid);
        string pre = layer > 0 ? "{0}<img src='/Images/TreeLineImages/t.gif' />" : "";
        string nbsp = "";
        for (int i = 0; i < layer; i++)
        {
            nbsp += "&nbsp;&nbsp;&nbsp;";
        }
        pre = string.Format(pre, nbsp);
        foreach (DataRow dr in drs)
        {
            PCate_ul.InnerHtml += string.Format("<li class='barli' data-barid='{1}'><a role='menuitem' tabindex='1' href='javascript:;'>{0}</a></li>", pre + dr["CateName"].ToString(), dr["CateID"].ToString());
            BindDrown(dt, Convert.ToInt32(dr["CateID"]), (layer + 1));
        }
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv1.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
    {
        if (this.Page.IsValid)
        {
            if (e.CommandName == "QList")
            {
                Response.Redirect("GuestTieShow.aspx?GID=" + e.CommandArgument.ToString());
            }
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            barBll.DelByIDS(ids, BarStatus == (int)ZLEnum.ConStatus.Recycle);
        }
        MyBind();
    }
    public string GetMsgPosition()
    {
        string postflag = Eval("PostFlag").ToString();
        string result = "";
        switch (Convert.ToInt32(Eval("OrderFlag")))
        {
            case 1:
                result += "<span style='color:orange;'>版面置顶</span>  ";
                break;
            case 2:
                result += "<span style='color:orange;'>全局置顶</span>  ";
                break;
            case -1:
                result += "<span style='color:red;'>沉底</span>  ";
                break;
        }
        if (postflag.Contains("Recommend"))
            result += "<span style='color:green;'>精华</span>  ";
        return result;
    }
    public string GetUserName(string UserID)
    {
        B_User buser = new B_User();
        return buser.SeachByID(DataConverter.CLng(UserID)).UserName;
    }
    public string GetStatus()
    {
        int status = Eval("status") == DBNull.Value ? 0 : Convert.ToInt32(Eval("status"));
        string result = "";
        switch (status)
        {
            case (int)ZLEnum.ConStatus.Recycle:
                result = "<span style='color:red;'>回收站</span>";
                break;
            case (int)ZLEnum.ConStatus.UnAudit:
                result = "<span style='color:red;'>未审核</span>";
                break;
            case (int)ZLEnum.ConStatus.Audited:
                result = "<span style='color:green;'>已审核</span>";
                break;
        }
        return result;
    }
    protected void BtnSetTopPosation_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            barBll.UpdateOrderFlag(Request.Form["idchk"].ToString(), 1);
            MyBind();
        }
    }
    protected void BtnSetRecomPosation_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            barBll.UpdateRecommend(Request.Form["idchk"].ToString(), true);
            MyBind();
        }
    }
    protected void BtnClosTopPosation_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            barBll.UpdateOrderFlag(Request.Form["idchk"].ToString(), 0);
            MyBind();
        }

    }
    protected void BtnCloseRecomPosation_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            barBll.UpdateRecommend(Request.Form["idchk"].ToString(), false);
            MyBind();
        }

    }
    protected void BtnSetDown_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            barBll.UpdateOrderFlag(Request.Form["idchk"], -1);
            MyBind();
        }
    }
    protected void BtnCloseDown_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            barBll.UpdateOrderFlag(Request.Form["idchk"], 0);
            MyBind();
        }
    }
    public int GetHitCount()
    {
        return DataConverter.CLng(Eval("HitCount") == DBNull.Value ? "" : Eval("HitCount").ToString());
    }
    protected void Rels_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            
            barBll.RelByIDS(Request.Form["idchk"]);
            MyBind();
        }
    }
    protected void MoveBar_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            barBll.ShiftPost(Request.Form["idchk"], Convert.ToInt32(selected_Hid.Value));
            MyBind();
        }
    }
    protected void UnCheck_B_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            barBll.UnCheckByIDS(Request.Form["idchk"]);
            MyBind();
        }
    }
    protected void Check_B_Click(object sender, EventArgs e)
    {
        M_GuestBookCate catemod = cateBll.SelReturnModel(CateID);
        //避免重复加分操作
        DataTable dt = barBll.SelByIDS(Request.Form["idchk"]);
        foreach (DataRow item in dt.Rows)
        {
            if (DataConverter.CLng(item["Status"]) != (int)ZLEnum.ConStatus.Audited && DataConverter.CLng(item["CUser"]) > 0)
            {
                //if (catemod.IsPlat == 1)
                //{
                //    string siteurl = "http://" + Request.Url.Authority + "/";
                //    string url = B_Guest_Bar.CreateUrl(2, Convert.ToInt32(item["ID"]));
                //    string cateurl = B_Guest_Bar.CreateUrl(1, catemod.CateID);
                //    msgBll.InsertMsg(string.Format(forwardTlp, item["Title"], siteurl + url, siteurl + url, catemod.CateName, siteurl+cateurl));
                //}
                buser.ChangeVirtualMoney(DataConverter.CLng(item["CUser"]), new M_UserExpHis()
                {
                    score = DataConverter.CLng(item["SendScore"]),
                    detail=string.Format("{0} {1}在版面:{2}发表主题:{3},赠送{4}分", DateTime.Now, item["CUName"], item["Catename"], item["Title"], DataConverter.CLng(item["SendScore"])),
                    ScoreType=(int)M_UserExpHis.SType.Point
                });
            }
        }
        barBll.CheckByIDS(Request.Form["idchk"]);
        MyBind();
    }

    protected void BtnSetAllTopPosation_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            barBll.UpdateOrderFlag(Request.Form["idchk"], 2);
            MyBind();
        }
    }

    protected void BtnAddMedalPosation_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            string[] idsarr = Request.Form["idchk"].Split(',');
            foreach (string barid in idsarr)
            {
                int id = DataConverter.CLng(barid);
                if (!medalbll.CheckMedalDiff(id, -1))//检测是否重复颁发
                {
                    M_Guest_Bar barmod = barBll.SelReturnModel(id);
                    medalbll.Insert(new M_Guest_Medals() { UserID = barmod.CUser,BarID=id, Sender = -1, MedalID = 3 });
                }
            }
            function.WriteSuccessMsg("颁发成功!");
        }
    }
}
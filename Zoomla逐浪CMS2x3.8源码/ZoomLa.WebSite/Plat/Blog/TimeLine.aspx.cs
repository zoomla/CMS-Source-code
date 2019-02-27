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
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Plat_Blog_TimeLine : System.Web.UI.Page
{
    B_Blog_Msg msgBll = new B_Blog_Msg();
    B_Plat_Group groupBll = new B_Plat_Group();
    B_Plat_Pro proBll = new B_Plat_Pro();
    B_User_Plat upBll = new B_User_Plat();
    B_Plat_Like likeBll = new B_Plat_Like();
    B_User buser = new B_User();
    protected int CPage { get { return PageCommon.GetCPage(); } }
    protected int psize = 30;
    protected int replyPageSize = 5;
    /*----------------------------------------------------------------------------*/
    public int Uid { get { return DataConvert.CLng(Request.QueryString["uids"]); } }
    public string Filter { get { return string.IsNullOrEmpty(Request.QueryString["Filter"]) ? "" : Request.QueryString["Filter"]; } }
    public string MsgType { get { return Request.QueryString["MsgType"]; } }
    public string Skey { get { return Request.QueryString["Skey"]; } }
    private string DateStr { get { return Request.QueryString["date"] ?? ""; } }
    private int Mid { get { return DataConvert.CLng(Request["ID"]); } }
    private string uname { get { return Request.QueryString["uname"] ?? ""; } }
    private string upwd { get { return Request.QueryString["upwd"] ?? ""; } }
    /*----------------------------------------------------------------------------*/
    public DataTable GroupDT { get { return (DataTable)ViewState["Plat_GroupDT"]; } set { ViewState["Plat_GroupDT"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        MyBind();
    }
    private void MyBind()
    {
        M_User_Plat upMod = upBll.SelByNameAndPwd(uname,upwd,true);
        if (upMod == null) { function.WriteErrMsg("您没有权限下载该用户的时间线记录！"); }
        GroupDT = groupBll.SelByCompID(upMod.CompID);
        int pageCount = 0;
        DataTable dt = msgBll.SelByPid(50000, 1, out pageCount, 0, upMod, upMod.Gid, 0, Filter, MsgType, Skey, upMod.UserID.ToString(), DateStr, Mid);
        //-------------------针对时间线进行处理
        if (CPage > 1) { Page.FindControl("ttitle_div").Visible = false; }
        DateTime TimeLine = DateTime.MinValue;
        dt.Columns.Add("timeline", typeof(string));
        foreach (DataRow dr in dt.Rows)
        {
            if (TimeLine == DateTime.MinValue || DateHelper.IsMoreThanOne(TimeLine, Convert.ToDateTime(dr["cdate"])))
            {
                TimeLine = Convert.ToDateTime(dr["cdate"]);
                dr["timeline"] = "date";
            }
            else { dr["timeline"] = "time"; }
        }
        MsgRepeater.DataSource = dt;
        MsgRepeater.DataBind();
    }
    protected void MsgRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = e.Item.DataItem as DataRowView;
            //判断信息类型
            switch (dr["MsgType"].ToString())
            {
                case "1":

                    e.Item.FindControl("normal").Visible = true;
                    break;
                case "2":
                    e.Item.FindControl("vote").Visible = true;
                    //用户投过票,或投票已到期,则直接显示结果页
                    if (HasVote(dr["VoteResult"].ToString(), buser.GetLogin().UserID) || Convert.ToDateTime(dr["EndTime"]) < DateTime.Now)
                    {
                        e.Item.FindControl("vote_user_div").Visible = false;
                        e.Item.FindControl("msg_op_btn_div").Visible = false;
                        (e.Item.FindControl("vote_result_div") as System.Web.UI.HtmlControls.HtmlGenericControl).Style.Add("display", "block");
                    }
                    Repeater rep = e.Item.FindControl("VoteResultRep") as Repeater;
                    DataTable dt = msgBll.GetVoteResultDT(dr["VoteOP"].ToString(), dr["VoteResult"].ToString());
                    rep.DataSource = dt;
                    rep.DataBind();
                    break;
                case "3":
                    e.Item.FindControl("longarticle").Visible = true;
                    break;
            }
        }
    }
    public bool HasVote(string voteResult, int uid)
    {
        bool flag = false;
        foreach (string s in voteResult.Split(','))
        {
            if (s.Split(':').Length > 1 && s.Split(':')[1].Equals(uid.ToString())) { flag = true; break; }
        }
        return flag;
    }
    public string GetForward()
    {
        if (DataConverter.CLng(Eval("ForwardID")) > 0)
        {
            string result = "<div class='msg_content_forward_div'>{0}";
            DataTable dt = msgBll.GetContentByID(Convert.ToInt32(Eval("ForwardID")));
            if (dt == null || dt.Rows.Count < 1 || string.IsNullOrEmpty(dt.Rows[0]["MsgContent"].ToString())) return "";
            result = string.Format(result, dt.Rows[0]["MsgContent"]);
            result += "<div style='margin-top:5px;'>" + Convert.ToDateTime(dt.Rows[0]["CDate"]).ToString("yyyy年MM月dd日 HH:mm");
            result += "</div></div>";
            return result;
        }
        return "";
    }
    public string GetVoteLI()
    {
        string result = "", id = Eval("ID").ToString(), li = "<li onclick='$(this).find(\"input:radio\")[0].checked=true;'><input type='radio' name='vote_" + id + "' value='{0}' />{1}</li>";
        string[] oparr = Eval("VoteOP").ToString().Split(',');
        for (int i = 0; i < oparr.Length; i++)
        {
            result += string.Format(li, i, oparr[i]);
        }
        return result;
    }
    //投票底部统计
    public string GetVoteBottom()
    {
        int count = Eval("VoteResult").ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length;
        string html = " 总计 " + count + " 票 " + Convert.ToDateTime(Eval("EndTime")).ToString("yyyy年MM月dd日 HH:mm") + "到期";
        return html;
    }
    public string GetWhoCanSee()
    {
        string result = "", ids = "";
        if (buser.GetLogin().UserID == Convert.ToInt32(Eval("CUser")))
        {
            ids = Eval("GroupIDS").ToString().Trim(',');
            result = " <i class='fa fa-bookmark'></i>";
            if (string.IsNullOrEmpty(ids))
            {
                result += "所有人";
            }
            else if (ids.Equals("0"))
            {
                result += "仅自己";
            }
            else
            {
                GroupDT.DefaultView.RowFilter = "ID IN (" + ids + ")";
                foreach (DataRow dr in GroupDT.DefaultView.ToTable().Rows)
                {
                    result += dr["GroupName"] + ",";
                }
                result = result.TrimEnd(',');
            }
        }
        return result;
    }
    public M_UserInfo GetUser()
    {
        return buser.SelReturnModel(Uid);
    }
}
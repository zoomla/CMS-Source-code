using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Plat.Blog
{
    public partial class MsgListBody : System.Web.UI.Page
    {
        B_Blog_Msg msgBll = new B_Blog_Msg();
        B_Plat_Group groupBll = new B_Plat_Group();
        B_Plat_Pro proBll = new B_Plat_Pro();
        B_User_Plat upBll = new B_User_Plat();
        B_Plat_Like likeBll = new B_Plat_Like();
        B_User buser = new B_User();
        protected int CPage { get { return PageCommon.GetCPage(); } }
        protected int psize = 10;
        protected int replyPageSize = 5;
        /*----------------------------------------------------------------------------*/
        public int CurProID { get { return DataConvert.CLng(Request.QueryString["Pro"]); } }
        public string Filter { get { return string.IsNullOrEmpty(Request.QueryString["Filter"]) ? "" : Request.QueryString["Filter"]; } }
        public string MsgType { get { return Request.QueryString["MsgType"]; } }
        public string Skey { get { return Request.QueryString["Skey"]; } }
        public string Uids { get { return Request.QueryString["Uids"]; } }
        private string DateStr { get { return Request.QueryString["date"] ?? ""; } }
        private int Mid { get { return DataConvert.CLng(Request["ID"]); } }
        private string LView { get { return Request.QueryString["view"] ?? ""; } }
        /*----------------------------------------------------------------------------*/
        protected void Page_Load(object sender, EventArgs e)
        {
            MyBind();
        }
        private void MyBind()
        {
            M_User_Plat upMod = B_User_Plat.GetLogin();
            GroupDT = groupBll.SelByCompID(upMod.CompID);
            //-----------------权限校验
            if (CurProID > 0)
            {
                if (!proBll.HasAuth(upMod.UserID, CurProID)) function.WriteErrMsg("你没有权限访问该项目!!");
            }
            int pageCount = 0;
            DataTable dt = msgBll.SelByPid(psize, CPage, out pageCount, 0, upMod, upMod.Gid, CurProID, Filter, MsgType, Skey, Uids, DateStr, Mid);
            //LikesDt
            string msgids = "";
            foreach (DataRow dr in dt.Rows) { msgids += dr["ID"].ToString().Trim('-') + ","; }
            if (!string.IsNullOrEmpty(msgids)) { LikesDt = likeBll.SelByMsgIDS(msgids.Trim(','), "plat"); }
            if (Filter.Contains("atuser"))//移除@
            {
                upBll.RemoveAtCount(upMod.UserID);
            }
            //-------------------针对时间线进行处理
            if (LView.Equals("timeline") || LView.Equals("tomht"))
            {
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
            }
            Msg_RPT.DataSource = dt;
            Msg_RPT.DataBind();
        }
        protected void MsgRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //加载用户回复列表
                Literal replyList = e.Item.FindControl("ReplyList_L") as Literal;
                DataRowView dr = e.Item.DataItem as DataRowView;
                int pid = Convert.ToInt32(dr["ID"]);
                StringWriter sw = new StringWriter();
                Server.Execute("/Plat/Blog/ReplyList.aspx?pid=" + pid + "&PageSize=" + replyPageSize + "&PageIndex=1", sw);
                if (!LView.Equals("timeline") && !LView.Equals("tomht"))
                {
                    replyList.Text = Regex.Match(sw.ToString(), "(?<=(start>))[.\\s\\S]*?(?=(</start))", RegexOptions.IgnoreCase).Value;
                }
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
        #region ItemGet
        //true已经投票,false未投票
        public bool HasVote(string voteResult, int uid)
        {
            bool flag = false;
            foreach (string s in voteResult.Split(','))
            {
                if (s.Split(':').Length > 1 && s.Split(':')[1].Equals(uid.ToString())) { flag = true; break; }
            }
            return flag;
        }
        public string GetContent()
        {
            string result = "";
            string content = Eval("MsgContent") as string;
            switch (Eval("Source").ToString())
            {
                case "plat":
                    if (content.Length > 300 && !content.Contains("</"))//带Html则不收缩
                    {
                        result = string.Format(ltlp, content.Substring(0, 300) + "...", content);
                    }
                    else
                    {
                        result = content;
                    }
                    break;
                case "bar":
                    string forwardTlp = "<i class='fa fa-share-alt'></i> 来自社区[<a href='/PClass?ID={0}' target='_blank'>{1}</a>]的分享：<strong>{3}</strong><a href='/PItem?ID={2}' title='点击浏览' target='_blank'> http://www.1th.cn/PItem?id={2}</a>";
                    //result = StrHelper.DecompressString(Eval("MsgContent").ToString());
                    result = string.Format(forwardTlp, Eval("CateID"), Eval("CateName"), -Convert.ToInt32(Eval("ID")), Eval("Title")) + result;
                    break;
            }
            return result;
        }
        string attachTlp = "<div class='imgview' onclick='PreView(\"{0}\");'><div class='ext {1}'></div><div class='fname'>{2}</div></div>";
        string imgTlp = "<a class='athumbnail_img' rel='group_{0}' href='{1}'><img src='{1}' class='thumbnail_img'/></a>";
        //-----Html Tlp
        private string ltlp = "<div class='synposis_div'>{0}<div class='aritcle_op_div '><a onclick='ArtColl();' href='javascript:;'>展开</a></div></div>"
                     + "<div class='detail_div'>{1}<div class='aritcle_op_div'><a onclick='ArtUnfold();' href='javascript:;'>折叠</a></div></div>";
        //显示附件,图片则直接展示,其他仍以附件形式
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
        //是否收藏
        public string GetColled()
        {
            string ids = Eval("ColledIDS").ToString();
            int msgID = Convert.ToInt32(Eval("ID"));
            string result = "";
            if (ids.Contains("," + buser.GetLogin().UserID + ","))
                result = "<span class='fa fa-heart colled' title='取消收藏' onclick='CollFunc(this," + msgID + ");'></span>";
            else
                result = "<span class='fa fa-heart-o nocolled' title='收藏' onclick='CollFunc(this," + msgID + ");'></span>";
            return result;
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
        //点赞--用户列表
        public string ShowLikeUser()
        {
            string result = "";
            if (LikesDt != null)
            {
                DataTable dt = LikesDt;
                dt.DefaultView.RowFilter = "MsgID=" + Eval("ID").ToString().Trim('-') + " AND Source='" + Eval("Source") + "'";
                foreach (DataRow dr in dt.DefaultView.ToTable().Rows)
                {
                    string name = B_User.GetUserName(dr["HoneyName"], dr["UserName"]);
                    result += "<li title='" + name + "' data-uid='" + dr["CUser"] + "' class='likeids_li'><a href='javascript:ShowUser(" + dr["CUser"] + ");'><div class=\"uword uword_xs\">" + name.Substring(0, 1) + "</div><img src='" + dr["salt"] + "' class=\"uimg img_xs\" onerror=\"showword(this);\" data-uid=\"" + Eval("CUser") + "\"/></a></li>";
                }
            }
            return result;
        }
        // 控制转发,点赞等效果
        public string GetReplyOP()
        {
            int id = Convert.ToInt32(Eval("ID"));
            int msgtype = Convert.ToInt32(Eval("MsgType"));
            int uid = Convert.ToInt32(Eval("CUser"));
            string suid = "," + uid + ",";
            string ids = Eval("LikeIDS").ToString();
            string result = "";
            string clockHtml = "<span title='定时提示' class='fa fa-clock-o' onclick=\"location='/Plat/Addon/SetPrompt.aspx?MsgID=" + id + "';\"></span>";
            string delHtml = "<span class='fa fa-trash-o' title='删除' onclick='PostDelMsg(" + id + ");'></span>";
            string forHtml = "<span class='fa fa-send' title='转发' onclick='ShowForWard(" + id + ");'></span>";
            string likeHtml = "<span class='fa fa-thumbs-up' title='{0}' onclick='PostLike(" + id + ");'></span>";
            if (buser.GetLogin().UserID == uid)//非本人不允许删除
            {
                result += clockHtml;
                result += delHtml;
            }
            if (msgtype != 2 && id > 0)//投票类型不转发,贴吧不允许转发
            {
                result += forHtml;
            }
            result += string.Format(likeHtml, ids.Contains(suid) ? "取消赞" : "点赞");
            return result;
        }
        public string IsShowLike()
        {
            return LikesDt.Select("MsgID=" + Eval("ID").ToString().Trim('-') + " AND Source='" + Eval("Source") + "'").Length > 0 ? "" : "display:none;";
        }
        public int GetLikeNum()
        {
            return LikesDt.Select("MsgID=" + Eval("ID").ToString().Trim('-') + " AND Source='" + Eval("Source") + "'").Length;
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
        public string GetUName()
        {
            return string.IsNullOrEmpty(Eval("HoneyName", "")) ? Eval("UserName", "") : Eval("HoneyName", "");
        }
        #endregion
        public DataTable LikesDt
        {
            get
            {
                return ViewState["Plat_Default_LikesDt"] as DataTable;
            }
            set { ViewState["Plat_Default_LikesDt"] = value; }
        }
        public DataTable GroupDT { get { return (DataTable)ViewState["Plat_GroupDT"]; } set { ViewState["Plat_GroupDT"] = value; } }
        public DataTable GetUserDT() { if (Session["Plat_Default_UserFace"] == null) { Session["Plat_Default_UserFace"] = upBll.SelUserFaceDT(B_User_Plat.GetLogin().CompID); } return Session["Plat_Default_UserFace"] as DataTable; }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["Plat_Default_UserFace"] = null;
            LikesDt = null;
            GroupDT = null;
        }
        public M_UserInfo GetUser()
        {
            int uid = DataConvert.CLng(Uids.Replace("\"", "").Replace(" ", "").Split(',')[0]);
            return buser.SelReturnModel(uid);
        }
    }
}
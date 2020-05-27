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
using ZoomLa.Model.Plat;
using ZoomLa.Safe;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Plat.Blog
{
    public partial class item : System.Web.UI.Page
    {
        B_Blog_Msg msgBll = new B_Blog_Msg();
        B_User_Plat upBll = new B_User_Plat();
        RegexHelper regHelper = new RegexHelper();
        B_Plat_Like likeBll = new B_Plat_Like();
        B_User buser = new B_User();
        public int Mid
        {
            get
            {
                return DataConverter.CLng(Request.QueryString["id"]);
            }
        }
        public int Uid { get { return DataConvert.CLng(Request.QueryString["uid"]); } }
        public int CPage
        {
            get
            {
                return PageCommon.GetCPage();
            }
        }
        public string Mode { get { return Request.QueryString["mode"] ?? ""; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Mode))
            {
                this.MasterPageFile = "~/Plat/Empty.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string result = "";
                switch (action)
                {
                    case "likelist":
                        DataTable dt = likeBll.SelLikeUsers(DataConvert.CLng(Request.Form["msgid"]), "plat");
                        result = JsonHelper.JsonSerialDataTable(dt);
                        break;
                    default:
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
            }
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            if (Mid < 0) { Response.Redirect("/PItem?ID=" + (-Mid)); return; }
            M_Blog_Msg msgMod = msgBll.SelReturnModel(Mid);
            M_User_Plat user = B_User_Plat.GetLogin();
            //if (user.CompID != msgModel.CompID){function.WriteErrMsg("此文章您没有权限访问！");}  
            CDate_L.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh:mm");
            Content_Lit.Text = msgMod.MsgContent;
            ids_Hid.Value = msgMod.LikeIDS;
            Attach_Lit.Text = GetAttach(msgMod.Attach);
            if (msgMod.MsgType == 2) { LoadVote(); }
            //---------------------------------
            DataTable dt = null;
            int pageCount = 0;
            dt = likeBll.SelLikeUsers(Mid, "plat");
            //Like_RPT.DataSource = dt;
            //Like_RPT.DataBind();
            Empty_Span_Like.Visible = dt.Rows.Count <= 0;
            dt = msgBll.SelByPid(5, CPage, out pageCount, Mid);
            MsgRepeater.DataSource = dt;
            MsgRepeater.DataBind();
            Empty_Span_Comm.Visible = dt.Rows.Count <= 0;
            commCount_L.Text = msgBll.GetSumCount(Mid).ToString();
            UserInfo_Hid.Value = user.TrueName + ":" + user.UserFace;
            likeCount_L.Text = msgMod.LikeIDS.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length.ToString();
            MsgPage_L.Text = PageCommon.CreatePageHtml(pageCount, CPage);
        }
        public M_Blog_Msg GetMsgMod() { return msgBll.SelReturnModel(Mid); }
        public void LoadVote()
        {
            M_Blog_Msg msgMod = GetMsgMod();
            vote.Visible = true;
            if (HasVote(msgMod.VoteResult, buser.GetLogin().UserID) || msgMod.EndTime < DateTime.Now)
            {
                vote_user_div.Visible = false;
                msg_op_btn_div.Visible = false;
                (vote_result_div as System.Web.UI.HtmlControls.HtmlGenericControl).Style.Add("display", "block");
            }
            Repeater rep = VoteResultRep as Repeater;
            DataTable dt = msgBll.GetVoteResultDT(msgMod.VoteOP, msgMod.VoteResult);
            rep.DataSource = dt;
            rep.DataBind();
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
        public string GetVoteLI()
        {
            M_Blog_Msg msgMod = GetMsgMod();
            string result = "", li = "<li onclick='$(this).find(\"input:radio\")[0].checked=true;'><input type='radio' name='vote_" + msgMod.ID + "' value='{0}' />{1}</li>";
            string[] oparr = msgMod.VoteOP.Split(',');
            for (int i = 0; i < oparr.Length; i++)
            {
                result += string.Format(li, i, oparr[i]);
            }
            return result;
        }
        public string GetVoteBottom()
        {
            M_Blog_Msg msgMod = GetMsgMod();
            int count = msgMod.VoteResult.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length;
            string html = " 总计 " + count + " 票 " + msgMod.EndTime.ToString("yyyy年MM月dd日 HH:mm") + "到期";
            return html;
        }

        string attachTlp = "<div class='imgview' onclick='PreView(\"{0}\");'><div class='ext {1}'></div><div class='fname'>{2}</div></div>";
        string imgTlp = "<img src='{1}' class='thumbnail_img'/>";
        public string GetAttach(string attach)
        {
            string imgresult = "", attachresult = "";
            int max = 3;
            foreach (string file in attach.Split('|'))
            {
                if (string.IsNullOrEmpty(file)) continue;
                if (SafeC.IsImage(file) && max > 0)//jpg,png,gif图片显示预览,只显示前三张
                {
                    imgresult += string.Format(imgTlp, Mid, file); max--;
                }
                else
                {
                    string fname = Path.GetFileName(file); fname = fname.Length > 6 ? fname.Substring(0, 6) + ".." : fname;
                    attachresult += string.Format(attachTlp, file, Path.GetExtension(file).ToLower().Replace(".", ""), fname);
                }
            }
            imgresult = string.IsNullOrEmpty(imgresult) ? "" : "<div class='thumbnail_div'>" + imgresult + "</div>";
            return imgresult + attachresult + "<div class='clearfix'></div>";
        }
        protected void MsgRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        /*------------------------------------------------------------------------------*/
        public string GetUserLike()
        {
            string ids = string.IsNullOrEmpty(ids_Hid.Value.Trim(',')) ? "0" : ids_Hid.Value.Trim(',');
            string uid = B_User_Plat.GetLogin().UserID.ToString();
            string result = "";
            result = "<span id=\"showlike_span\" data-init=\"" + (ids.Contains(uid) ? "1" : "0") + "\" style=\"margin-left:-5px;" + (ids.Contains(uid) ? "display:inline;" : "display:none;") + "\">1</span>";
            return result;
        }
        //public string GetUserFace()
        //{
        //    string ids = string.IsNullOrEmpty(ids_Hid.Value.Trim(',')) ? "0" : ids_Hid.Value.Trim(',');
        //    string result = "";
        //    if (!string.IsNullOrEmpty(ids))
        //    {
        //        DataTable dt = GetUserDT();
        //        dt.DefaultView.RowFilter = "UserID IN(" + ids + ")";
        //        foreach (DataRow dr in dt.DefaultView.ToTable().Rows)
        //        {
        //            result += "<li title='" + dr["TrueName"] + "' class='likeids_li'><a href='javascript:;'><img class='imgface_mid' src='" + dr["UserFace"] + "' onerror=\"this.src='/Images/userface/noface.png';\"/></a></li>";
        //        }
        //    }
        //    return result;
        //}
        public DataTable GetUserDT()
        {
            if (Session["Plat_Default_UserFace"] == null)
            {
                Session["Plat_Default_UserFace"] = upBll.SelByCompany(B_User_Plat.GetLogin().CompID);
            }
            return Session["Plat_Default_UserFace"] as DataTable;
        }
        public string IsShowLike()
        {
            return ids_Hid.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length > 0 ? "" : "display:none;";
        }
        protected void sendButton_Click(object sender, EventArgs e)
        {
            M_Blog_Msg model = FillMsg(MsgContent_T.Value, Mid);
            msgBll.Insert(model);
            MsgContent_T.Value = "";
            MyBind();
        }
        protected void Froward_Btn_Click(object sender, EventArgs e)
        {
            M_Blog_Msg msgMod = new M_Blog_Msg();
            int forid = Convert.ToInt32(Forward_ID_Hid.Value);
            M_Blog_Msg model = msgBll.SelReturnModel(forid);
            if (model.MsgType != 2)
            {
                msgMod = FillMsg(ForMsg_Text.Text);
                msgMod.ForWardID = forid;
                msgBll.Insert(msgMod);
                MyBind();
            }
            else { function.Script(this, "alert('该类型不允许转发');"); }
        }
        public M_Blog_Msg FillMsg(string msg, int pid = 0, int rid = 0)
        {
            M_User_Plat upMod = B_User_Plat.GetLogin();
            M_Blog_Msg model = new M_Blog_Msg();
            model.MsgType = 1;
            model.Status = 1;
            model.CUser = upMod.UserID;
            model.CUName = upMod.TrueName;
            #region 信息内容处理
            msg = Server.HtmlEncode(msg);//避免写入js,后面可插入Html
                                         //------处理@功能
                                         //@功能
            {
                MatchCollection mc = regHelper.GetValuesBySE(msg, "@", "]");
                int id = 0;
                string atuser = "", atgroup = "", name = "";
                string uTlp = "<a href='javascript:;' onclick='ShowUser({0});'>{1}</a>";
                string gTlp = "<a href='javascript:;' onclick='ShowGroup({0});'>{1}</a>";
                foreach (Match m in mc)
                {
                    if (string.IsNullOrEmpty(m.Value)) continue;
                    if (m.Value.Contains("uid:"))
                    {
                        id = DataConvert.CLng(regHelper.GetValueBySE(m.Value, "uid:", "]", false));
                        name = regHelper.GetValueBySE(m.Value, "@", @"\[").Replace("[", "");
                        atuser += id + ",";
                        msg = msg.Replace(m.Value, string.Format(uTlp, id, name));
                    }
                    else if (m.Value.Contains("gid:"))
                    {
                        id = DataConvert.CLng(regHelper.GetValueBySE(m.Value, "gid:", "]", false));
                        name = regHelper.GetValueBySE(m.Value, "@", @"\[").Replace("[", "");
                        atgroup += id + ",";
                        msg = msg.Replace(m.Value, string.Format(gTlp, id, name));
                    }
                    msg = msg.Replace(m.Value, "");
                }
                if (!string.IsNullOrEmpty(atuser) || !string.IsNullOrEmpty(atgroup))
                {
                    atuser += upBll.SelByGIDS(atgroup);
                    if (!string.IsNullOrEmpty(atuser.Replace(",", "")))
                    {
                        model.ATUser = StrHelper.IdsFormat(atuser);
                        //model.ATUser = model.ATUser.Replace("," + upMod.UserID, "");//过滤自己
                        //提示被@人
                        M_Notify notifyMod = new M_Notify();
                        notifyMod.CUName = upMod.UserName;
                        notifyMod.Title = "Hi,有人@你了,点击查看详情";
                        notifyMod.Content = msg.Length > 30 ? msg.Substring(0, 30) : msg;
                        notifyMod.ReceUsers = model.ATUser;
                        B_Notify.NotifyList.Add(notifyMod);
                    }
                }
            }
            #endregion
            model.MsgContent = msg;
            model.pid = pid;
            model.ReplyID = rid;
            if (rid > 0)
            {
                M_Blog_Msg msgMod = msgBll.SelReturnModel(model.ReplyID);
                model.ReplyUserID = msgMod.CUser;
                model.ReplyUName = msgMod.CUName;
            }
            if (!string.IsNullOrEmpty(Request.Form["Attach_Hid"]))//为安全，不允许全路径，必须后台对路径处理
            {
                string uppath = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Blog);
                M_UserInfo bus = buser.GetLogin();
                string files = SafeSC.PathDeal(Request.Form["Attach_Hid"].Trim());
                foreach (string file in files.Split('|'))
                {
                    if (string.IsNullOrEmpty(file)) continue;
                    model.Attach += uppath + file + "|";
                }
            }
            model.GroupIDS = Request.Form["GroupIDS_Chk"];//后期需加入检测,避免前台伪造
            model.ColledIDS = "";
            //model.CompID = upMod.CompID;
            return model;
        }
        public M_UserInfo GetUser()
        {
            return buser.SelReturnModel(Uid);
        }


        protected void UserVote_B_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["vote_" + Mid])) { return; }
            int opid = DataConvert.CLng(Request.Form["vote_" + Mid]);
            msgBll.AddUserVote(Mid, opid, buser.GetLogin().UserID);
            LoadVote();
        }
    }
}
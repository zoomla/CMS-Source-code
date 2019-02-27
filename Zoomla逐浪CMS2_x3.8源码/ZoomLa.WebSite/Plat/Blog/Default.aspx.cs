using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Message;
using ZoomLa.Model.Plat;
using ZoomLa.PdoApi.SinaWeiBo;
using ZoomLa.SQLDAL;

/*
 * WhoCanSee, 为空则所有人可见,否则加入部门机制，OnlyMe,则为自己日记，不要此功能
 */ 
public partial class Plat_Task_Default : System.Web.UI.Page
{
    M_Blog_Msg msgMod = new M_Blog_Msg();
    B_Blog_Msg msgBll = new B_Blog_Msg();
    B_Blog_Sdl sdlBll = new B_Blog_Sdl();
    B_Plat_Pro proBll = new B_Plat_Pro();
    B_Plat_Group groupBll = new B_Plat_Group();
    B_Plat_Topic topicBll = new B_Plat_Topic();
    B_Plat_Comp compBll = new B_Plat_Comp();
    B_Plat_Like likeBll = new B_Plat_Like();
    B_User_Plat upBll = new B_User_Plat();
    B_User_Token tokenBll = new B_User_Token();
    B_User buser = new B_User();
    B_Guest_Bar barBll = new B_Guest_Bar();

    RegexHelper regHelper = new RegexHelper();
    SinaHelper sinaBll = null;
    QQHelper qqBll = null;
    public int replyPageSize = 5;
    public string deftopic = "#插入话题#";
    /*----------------------------------------------------------------------------*/
    public int CurProID { get { return DataConvert.CLng(Request.QueryString["Pro"]); } }
    public string Filter { get { return string.IsNullOrEmpty(Request.QueryString["Filter"]) ? "" : Request.QueryString["Filter"]; } }
    public string MsgType { get { return Request.QueryString["MsgType"]; } }
    public string Skey { get { return Request.QueryString["Skey"]; } }
    public string Uids { get { return Request.QueryString["Uids"]; } }
    public string LView { get { return Request.QueryString["view"]; } }
    public int PSize = 10;
    /*----------------------------------------------------------------------------*/
    public string Write { get { return HttpUtility.UrlDecode(Request.QueryString["write"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AJAX请求
        //正数为消息,负数为贴吧
        if (function.isAjax())
        {
            string action = Request["action"];
            string result = "";
            int id = DataConvert.CLng(Regex.Split(Request["value"] ?? "", ":::")[0]);
            //思路,信息都存在贴吧中,ID为负数
            switch (action)
            {
                case "add":
                    {
                        string puremsg = "";
                        msgMod = FillMsg(Request.Form["MsgContent_T"], out puremsg);
                        result = msgBll.Insert(msgMod).ToString();
                        #region 同步至微博
                        if (!string.IsNullOrWhiteSpace(Request.Form["sync_chk"]))
                        {
                            puremsg = StringHelper.SubStr(puremsg, 140, ""); 
                            string sync = Request.Form["sync_chk"];
                            M_UserInfo mu=buser.GetLogin();
                            M_User_Token tokenMod = tokenBll.SelModelByUid(mu.UserID);
                            if (tokenMod != null)
                            {
                                try
                                {
                                    if (sync.Contains("qqblog") && !string.IsNullOrWhiteSpace(tokenMod.QQToken))
                                    {
                                        qqBll = new QQHelper(tokenMod.QQToken, tokenMod.QQOpenID);
                                        if (!string.IsNullOrEmpty(msgMod.Attach)) { qqBll.AddBlog(puremsg, msgMod.Attach.Split('|')[0]); }
                                        else { qqBll.AddBlog(puremsg); }
                                    }
                                    if (sync.Contains("sina") && !string.IsNullOrWhiteSpace(tokenMod.SinaToken))
                                    {
                                        sinaBll = new SinaHelper(tokenMod.SinaToken);
                                        string err = sinaBll.PostStatus(puremsg, (msgMod.Attach ?? "").Split('|')[0]);
                                        ZLLog.L(err);
                                    }
                                }
                                catch (Exception ex) { ZLLog.L("[" + sync + "]同步失败,用户[" + mu.UserName + "]原因:" + ex.Message); }
                            }
                        }
                        #endregion
                    }
                    break;
                case "addvote":
                    {
                        msgMod = FillMsg(Request.Form["MsgContent_T"]);
                        msgMod.MsgType = 2;
                        msgMod.Title = HttpUtility.HtmlEncode(VoteTitle_T.Text);
                        msgMod.VoteOP = Request.Form["VoteOption_T1"] + "," + Request.Form["VoteOption_T"];//为Jquery验证
                        msgMod.VoteOP = HttpUtility.HtmlEncode(msgMod.VoteOP);
                        msgMod.VoteResult = "";
                        msgMod.EndTime = DateTime.Parse(EndDate_T.Text);
                        result = msgBll.Insert(msgMod).ToString();
                    }
                    break;
                case "addarticle"://里面包含html|图片|附件,暂不同步微博
                    {
                        string msg = Request.Form["msg"];
                        UEHelper ueHelper = new UEHelper();
                        msgMod = FillMsg("");
                        msgMod.MsgContent = msg;
                        msgMod.MsgType = 3;
                        msgMod.Title = ueHelper.GetSubTitle(msgMod.MsgContent);
                        result = msgBll.Insert(msgMod).ToString();
                    }
                    break;
                case "AddReply":
                case "AddReply2":
                    if (id == 0) throw new Exception("传入的ID不正确");
                    result = PlatAJAX();
                    break;
                case "UserVote":
                    result = PlatAJAX();
                    break;
                default:
                    if (id == 0) throw new Exception("信息ID不正确");
                    if (id > 0) { result = PlatAJAX(); }
                    else { result = BarAJAX(); }
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        #endregion
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    #region AJAX请求
    public string PlatAJAX() 
    {
        int uid = buser.GetLogin().UserID;
        string action = Request.Form["action"];
        string value = Request.Form["value"];
        string msg = "", files = ""; int pid = 0, rid = 0, id = 0;
        switch (action)
        {
            case "DeleteMsg"://删除
                id = Convert.ToInt32(value);
                msgBll.DelByUID(id, uid);
                break;
            case "AddReply"://回复
                pid = Convert.ToInt32(Regex.Split(value, ":::")[0]);
                rid = Convert.ToInt32(Regex.Split(value, ":::")[1]);
                msg = Regex.Split(value, ":::")[2];
                files = Regex.Split(value, ":::")[3];
                msgBll.Insert(FillMsg(msg, pid, rid, files));
                break;
            case "AddReply2"://对回复中的某人进行回复
                pid = Convert.ToInt32(Regex.Split(value, ":::")[0]);
                rid = Convert.ToInt32(Regex.Split(value, ":::")[2]);
                msg = Regex.Split(value, ":::")[2];
                files = Regex.Split(value, ":::")[3];
                msgBll.Insert(FillMsg(msg, pid, rid, files));
                break;
            case "AddColl"://收藏
                msgBll.UpdateColled(uid, Convert.ToInt32(value), 1);
                break;
            case "ReColl"://后期加入部门与公司校验
                msgBll.UpdateColled(uid, Convert.ToInt32(value), 2);
                break;
            case "AddLike":
                id = Convert.ToInt32(value);
                //msgBll.AddLike(id, UserID);
                likeBll.AddLike(uid, id, "plat");
                break;
            case "ReLike":
                id = Convert.ToInt32(value);
                //msgBll.RemoveLike(id, UserID);
                likeBll.DelLike(uid, id,"plat");
                break;
            case "UserVote"://用户投票
                id = Convert.ToInt32(value.Split(':')[0]);
                int opid = Convert.ToInt32(value.Split(':')[1]);
                msgBll.AddUserVote(id, opid, uid);
                break;
        }
        return M_APIResult.Success.ToString();
    }
    public string BarAJAX()
    {
        string action = Request.Form["action"];
        string value = Request.Form["value"];
        int pid = 0;
        string result = "";
        M_UserInfo user = buser.GetLogin();
        pid = (-Convert.ToInt32(Regex.Split(value, ":::")[0]));
        switch (action)
        {
            case "DeleteMsg"://删除
                result = barBll.UpdateStatus(barBll.SelReturnModel(pid).CateID, pid.ToString(), (int)ZLEnum.ConStatus.Recycle) ? M_APIResult.Success.ToString() : M_APIResult.Failed.ToString();
                break;
            //case "AddReply"://回复
            //    //pid = Convert.ToInt32(Regex.Split(value, ":::")[0]);
            //    rid = (-Convert.ToInt32(Regex.Split(value, ":::")[1]));
            //    msg = Regex.Split(value, ":::")[2];
            //    barBll.Insert(FillBarMsg(msg, pid, rid));
            //    break;
            //case "AddReply2"://回复用户,需要切换为Json
            //    //pid = Convert.ToInt32(Regex.Split(value, ":::")[0]);
            //    rid = (-Convert.ToInt32(Regex.Split(value, ":::")[1]));
            //    msg = Regex.Split(value, ":::")[2];
            //    barBll.Insert(FillBarMsg(msg, pid, rid));
            //    break;
            case "AddColl":
                result = barBll.LikeTie(pid, user.UserID, 1, "ColledIDS") ? "1" : "0";
                break;
            case "ReColl":
                result = barBll.LikeTie(pid, user.UserID, 2, "ColledIDS") ? "1" : "0";
                break;
            case "AddLike":
                result = likeBll.AddLike(user.UserID, pid, "bar") ? M_APIResult.Success.ToString() : M_APIResult.Failed.ToString();// barBll.LikeTie(pid, user.UserID, 1) ? "1" : "0";
                break;
            case "ReLike":
                result = likeBll.DelLike(user.UserID,pid,"bar") ? M_APIResult.Success.ToString() : M_APIResult.Failed.ToString(); ;// barBll.LikeTie(pid, user.UserID, 2) ? "1" : "0";
                break;
        }
        return result;
    }
    public M_Guest_Bar FillBarMsg(string msg, int pid, int rid = 0)
    {
        string base64 = StrHelper.CompressString(msg);
        if (base64.Length > 40000) function.WriteErrMsg("发贴失败,原因:内容过长,请减少内容文字");
        M_UserInfo mu = buser.GetLogin();
        M_Guest_Bar parent = barBll.SelReturnModel(pid);
        M_Guest_Bar model = new M_Guest_Bar();
        model.MsgType = 1;
        model.Status = (int)ZLEnum.ConStatus.Audited;
        model.CUser = mu.UserID;
        model.CUName = mu.HoneyName;
        model.R_CUName = mu.HoneyName;
        model.IDCode = mu.UserID <= 0 ? mu.WorkNum : mu.UserID.ToString();
        model.MsgContent = base64;
        model.Pid = pid;
        model.ReplyID = rid;
        model.CateID = parent.CateID;
        model.IP = EnviorHelper.GetUserIP();
        string ipadd = IPScaner.IPLocation(model.IP);
        ipadd = ipadd.IndexOf("本地") > 0 ? "未知地址" : ipadd;
        model.IP = model.IP + "|" + ipadd;
        model.ColledIDS = "";
        //AddUserExp(mu, parent.CateID, parent.Title);
        return model;
    }
    #endregion
    private void MyBind()
    {
        M_User_Plat upMod = B_User_Plat.GetLogin();
        M_Plat_Comp compMod = compBll.SelReturnModel(upMod.CompID);
        Title_T.Text = compMod.CompName + "办公平台";
        //--------------获取本月日程数据,并转为JSON,方便绑定
        myplan_hid.Value = sdlBll.SelMonthToJson(DateTime.Now, upMod.UserID);
        //--------------平台
        M_User_Token tokenMod = tokenBll.SelModelByUid(upMod.UserID);
        bool flag = false;
        if (tokenMod != null)
        {
            if (!string.IsNullOrWhiteSpace(tokenMod.SinaToken))
            {
                sinaBll = new SinaHelper(tokenMod.SinaToken);
                sina_li.Visible = true; flag = true;
                if (!sinaBll.CheckToken())//Token有效
                {
                    sina_li.InnerHtml = sina_li.InnerHtml + "<span class='r_red'>(已失效)</span>"; sina_li.Attributes.Add("title", "点击重新绑定"); sina_li.Attributes.Add("onclick", "OpenWin(2);");
                }
            }
            if (!string.IsNullOrWhiteSpace(tokenMod.QQToken))
            {
                qqBll = new QQHelper(tokenMod.QQToken, tokenMod.QQOpenID);
                qqblog_li.Visible = true; flag = true;
                if (!qqBll.TokenIsValid())
                {
                    qqblog_li.InnerHtml = qqblog_li.InnerHtml + "<span class='r_red'>(已失效)</span>"; qqblog_li.Attributes.Add("title", "点击重新绑定"); qqblog_li.Attributes.Add("onclick", "OpenWin(2);");
                }
            }
        }
        bloglist.Visible = flag; noplat_div.Visible = !flag;
        //-----------
        GroupRpt.DataSource = groupBll.SelGroupByAuth(upMod.UserID);
        GroupRpt.DataBind();
        DataTable cateDT = barBll.SelBlogCate(upMod.CompID);
        if (cateDT.Rows.Count > 0)
        {
            Cate_RPT.DataSource = cateDT;
            Cate_RPT.DataBind();
        }
        else { Cart_RPT_Empty.Visible = true; }

        EndDate_T.Text = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd HH:mm");
        UserInfo_Hid.Value = upMod.TrueName + ":" + upMod.UserFace + ":" + upMod.UserID;
        ////--最近的投票
        //newvote_Rep.DataSource = (from t in dt.AsEnumerable()
        //                          where t.Field<int>("MsgType") == 2
        //                          orderby t.Field<DateTime>("CDate")
        //                          select new { Title = t.Field<string>("Title"), CDate = t.Field<DateTime>("CDate"), UserFace = t.Field<string>("UserFace") }).Take(5);
        //newvote_Rep.DataBind();
    }
    //转发
    protected void Froward_Btn_Click(object sender, EventArgs e)
    {
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
    //-----Tools
    private M_Blog_Msg FillMsg(string msg, int pid = 0, int rid = 0, string files = "")
    {
        string puremsg = "";
        return FillMsg(msg, out puremsg, pid, rid, files);
    }
    private M_Blog_Msg FillMsg(string msg, out string puremsg, int pid = 0, int rid = 0, string files = "")
    {
        puremsg = msg;
        M_User_Plat upMod = B_User_Plat.GetLogin();
        M_Blog_Msg model = new M_Blog_Msg();
        model.MsgType = 1;
        model.Status = 1;
        model.CUser = upMod.UserID;
        model.CUName = upMod.TrueName;
        model.ProID = CurProID;
        model.CompID = upMod.CompID;
        model.pid = pid;
        model.ReplyID = rid;
        #region 信息内容处理
        //#话题(转码后会带有#符号,所以需要转码前处理完成)
        if (msg.Contains("#"))
        {
            msg = msg.Replace(deftopic, "");
            string tlp = "<a href='/Plat/Blog?Skey={0}' title='话题浏览'>{1}</a>";
            Dictionary<string, string> itemDic = new Dictionary<string, string>();
            for (int i = 0; !string.IsNullOrEmpty(regHelper.GetValueBySE(msg, "#", "#", false)) && i < 5; i++)//最多不能超过5个话题
            {
                string topic = "#" + regHelper.GetValueBySE(msg, "#", "#", false) + "#";
                msg = msg.Replace(topic, "{" + i + "}");
                topic = topic.Replace(" ", "").Replace(",", "");
                itemDic.Add("{" + i + "}", string.Format(tlp, Server.UrlEncode(topic), topic));
                model.Topic += topic + ",";
            }
            msg = HttpUtility.HtmlEncode(msg);
            foreach (var item in itemDic)
            {
                msg = msg.Replace(item.Key, item.Value);
            }
        }
        else { msg = HttpUtility.HtmlEncode(msg); }
        //URL转链接
        {
            string tlp = "<a href='{0}' target='_blank'>{0}</a>";
            MatchCollection mcs = regHelper.GetUrlsByStr(msg);
            foreach (Match m in mcs)
            {
                //同网址,信息替换多次会产生Bug,如多个www.baidu.com
                string url = m.Value.IndexOf("://") < 0 ? "http://" + m.Value : m.Value;
                msg = msg.Replace(m.Value, string.Format(tlp, url));
            }
        }
        //表情
        {
            if (!string.IsNullOrEmpty(ImgFace_Hid.Value))
            {
                string imgHtml = "<img src='/Plugins/Ueditor/dialogs/emotion/{0}' class='imgface_img' />";
                DataTable imgDT = JsonHelper.JsonToDT(ImgFace_Hid.Value);
                foreach (DataRow dr in imgDT.Rows)
                {
                    msg = msg.Replace(dr["title"].ToString(), string.Format(imgHtml, dr["realurl"].ToString()));
                    puremsg = puremsg.Replace(dr["title"].ToString(), "");
                }
            }
        }
        //@功能
        {
            MatchCollection mc = regHelper.GetValuesBySE(msg, "@", "]");
            int id = 0;
            string atuser = "", atgroup = "",name="";
            string uTlp = "<a href='javascript:;' onclick='ShowUser({0});'>{1}</a>";
            string gTlp = "<a href='javascript:;' onclick='ShowGroup({0});'>{1}</a>";
            foreach (Match m in mc)
            {
                //@what130[uid:19],名字替换为超链接,之后的取值取入数据库
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
                puremsg = puremsg.Replace(m.Value, "");
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
                    notifyMod.Title = "Hi,[" + B_User.GetUserName(upMod.TrueName, upMod.UserName) + "]@你了,点击查看详情";
                    notifyMod.Content = puremsg.Length > 30 ? puremsg.Substring(0, 30) : puremsg;
                    notifyMod.ReceUsers = model.ATUser;
                    B_Notify.NotifyList.Add(notifyMod);
                }
            }
        }
        //--------------------------------
        msg = msg.Replace("\r","").Replace("\n", "<br/>");//替换换行标识
        #endregion
        //msg = msg.Replace("&#39;", "\'");
        model.MsgContent = msg;
        if (rid > 0)
        {
            M_Blog_Msg msgMod = msgBll.SelReturnModel(model.ReplyID);
            model.ReplyUserID = msgMod.CUser;
            model.ReplyUName = msgMod.CUName;
        }
        if (string.IsNullOrEmpty(files) && !string.IsNullOrEmpty(Request.Form["Attach_Hid"]))
        {
            files = SafeSC.PathDeal(Request.Form["Attach_Hid"].Trim());
        }
        if (!string.IsNullOrEmpty(files))//为安全，不允许全路径，必须后台对路径处理
        {
            string uppath = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Blog);
            foreach (string file in files.Split('|'))
            {
                if (string.IsNullOrEmpty(file)) continue;
                model.Attach += uppath + file + "|";
            }
        }
        if (!string.IsNullOrEmpty(Request.Form["GOnlyMe_Chk"]))
        {
            model.GroupIDS = "0";
        }
        else
        {
            model.GroupIDS = string.IsNullOrEmpty(Request.Form["GroupIDS_Chk"]) ? "" : Request.Form["GroupIDS_Chk"];//后期需加入检测,避免前台伪造
        }
        model.ColledIDS = "";
        return model;
    }
    //传入基础路径,拼接为完整路径,用于需要保持状态的情况下Url链接,如分页等
    public string GetUrl(string url) 
    {
        if (CurProID != 0)
            url += "&Pro=" + CurProID;
            return url;
    }
    //----------------------------------------公告
    public string getTitle()
    {
        string title = Eval("Title").ToString();
        title = title.Length > 14 ? title.Substring(0, 14) + "..." : title;
        return "<a target='_blank' href='/PItem?id=" + Eval("ID") + "'>" + title + " </a>" + " [" + barBll.GetUName(Eval("HoneyName"), Eval("CUName")) + "]";
    }
    protected void Cate_RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView drv = e.Item.DataItem as DataRowView;
        Repeater bar_rpt = e.Item.FindControl("Bar_RPT") as Repeater;
        if (bar_rpt != null && drv != null)
        {
            DataTable dt = barBll.SelBlogBar(Convert.ToInt32(drv["Cateid"]));
            bar_rpt.DataSource = dt;
            bar_rpt.DataBind();
        }
    }
}
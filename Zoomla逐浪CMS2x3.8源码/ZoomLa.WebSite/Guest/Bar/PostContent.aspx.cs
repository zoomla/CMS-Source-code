using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Message;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Message;
using ZoomLa.SQLDAL;

public partial class Guest_Bar_PostContent : CustomerPageAction
{
    M_Guest_Bar barMod = new M_Guest_Bar();
    B_Guest_Bar barBll = new B_Guest_Bar();
    B_Guest_BarAuth authBll = new B_Guest_BarAuth();
    B_User buser = new B_User();
    RegexHelper regHelper = new RegexHelper();
    B_Group bgp = new B_Group();
    M_Uinfo users = new M_Uinfo();
    M_UserInfo userinfo = new M_UserInfo();
    public int replyPageSize = 5;
    public int curPageSize = 10;
    B_User ubll = new B_User();
    B_User_Friend ufbll = new B_User_Friend();
    B_TempUser tpuserBll = new B_TempUser();
    B_Guest_Medals medalsBll = new B_Guest_Medals();
    B_Plat_Like likeBll = new B_Plat_Like();
    B_GuestBookCate cateBll = new B_GuestBookCate();
    public string Filter
    {
        get { return string.IsNullOrEmpty(Request.QueryString["Filter"]) ? "" : Request.QueryString["Filter"]; }
    }
    //贴子ID
    public int Pid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
    public int Cid { get { return ViewState["cid"] == null ? 0 : Convert.ToInt32(ViewState["cid"]); } set { ViewState["cid"] = value; } }
    //未登录用户则隐藏贴子
    public bool HidePost
    {
        get
        {
            if (ViewState["HidePost"] == null)
                ViewState["HidePost"] = false;
            return (bool)ViewState["HidePost"];
        }
        set { ViewState["HidePost"] = value; }
    }
    private string HideTlp = "";
    private string AlertTlp = "<div class='alert alert-danger' role='alert'><span class='fa fa-exclamation-circle' aria-hidden='true'></span><span class='sr-only'>提示:</span>该贴子内容已被屏蔽！您拥有管理权限，以下是贴子内容</div>";
    private string UserAlertTlp = "<div class='alert alert-danger' role='alert'><span class='fa fa-exclamation-circle' aria-hidden='true'></span><span class='sr-only'>提示:</span>该贴子内容已被屏蔽！</div>";
    public bool IsBarOwner = false;
    public int UserID
    {
        get
        {
            if (ViewState["UserID"] == null)
            {
                ViewState["UserID"] = tpuserBll.GetLogin().UserID;
            }
            return Convert.ToInt32(ViewState["UserID"]);
        }
    }
    public DataTable MedalDT { get { return ViewState["Bar_MedalDT"] as DataTable; } set { ViewState["Bar_MedalDT"] = value; } }
    public string BarIDS="";//贴吧ids
    public int CPage
    {
        get
        {
            return PageCommon.GetCPage();
        }
    }
    public DataTable LikeDt
    {
        get { return ViewState["Guest_Content_Like"] as DataTable; }
        set { ViewState["Guest_Content_Like"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string result = BarAJAX();
            Response.Write(result); Response.Flush(); Response.End();
        }
        HideTlp = "你当前没有登录,请<a href='/User/Login.aspx?returnUrl=/PItem?id=" + Pid + "'>登录</a>后查看该贴";
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public string BarAJAX()
    {
        //Pid贴子ID
        string action = Request.Form["action"];
        string value = Request.Form["value"];
        string msg = ""; int pid = 0;
        string result = "1" + ":" + Pid;
        M_UserInfo user = buser.GetLogin();
        pid = DataConvert.CLng(Regex.Split(value, ":::")[0]);
        switch (action)
        {
            case "DeleteMsg"://删除
                result = barBll.UpdateStatus(barBll.SelReturnModel(pid).CateID, pid.ToString(), (int)ZLEnum.ConStatus.Recycle) ? M_APIResult.Success.ToString() : M_APIResult.Failed.ToString();
                break;
            case "AddReply"://回复
                msg = Regex.Split(value, ":::")[1];
                barBll.Insert(FillMsg(msg, pid, Pid));
                break;
            case "AddReply2"://回复用户,需要切换为Json
                msg = Regex.Split(value, ":::")[1];
                barBll.Insert(FillMsg(msg, pid, Pid));
                break;
            case "AddColl":
                result = barBll.LikeTie(pid, user.UserID, 1, "ColledIDS") ? "1" : "0";
                break;
            case "ReColl":
                result = barBll.LikeTie(pid, user.UserID, 2, "ColledIDS") ? "1" : "0";
                break;
            case "AddLike":
                result = likeBll.AddLike(user.UserID,pid,"bar") ? "1" : "0";// barBll.LikeTie(pid, user.UserID, 1) ? "1" : "0";
                break;
            case "ReLike":
                result = likeBll.DelLike(user.UserID,pid,"bar") ? "1" : "0";// barBll.LikeTie(pid, user.UserID, 2) ? "1" : "0";
                break;
            case "AddMedal"://添加勋章
                result = medalsBll.AddMedal_U(pid,user.UserID).ToString();
                break;
            case "GetMedalNum"://得到用户勋章数量
                result = medalsBll.SelByUid(pid).Rows.Count.ToString();
                break;
            case "GetUserMedal"://获取用户的勋章
                result = JsonConvert.SerializeObject(medalsBll.SelByUid(pid));
                break;
        }
        return result;
    }
    public void MyBind()
    {
        int pageCount = 0;
        DataTable dt = new DataTable();
        barMod = barBll.SelReturnModel(Pid);
        if (barMod == null) function.WriteErrMsg("该贴子不存在!!");
        M_GuestBookCate cateMod = cateBll.SelReturnModel(barMod.CateID);
        M_UserInfo mu = tpuserBll.GetLogin();
        if (barMod.Status!=(int)ZLEnum.ConStatus.Audited&&cateMod.Status!=1)
        {
            function.WriteErrMsg("该贴子需要审核通过才能浏览！");
        }
        if (cateMod.IsBarOwner(mu.UserID))//吧主
        {
            barowner_div.Visible = true;
            IsBarOwner = true;
        }
        else
        {
            if (!authBll.AuthCheck(cateMod, mu))
            {
                function.WriteErrMsg("你没有访问权限或未登录,请<a href='/User/Login.aspx?Returnurl=/PItem?id=" + Pid+ "&cpage="+ CPage + "'>登录</a>后查看");
                //if (cateMod.NeedLog == 1 && mu.UserID == 0) function.WriteErrMsg("该栏目必须<a href='/User/Login.aspx?returnUrl=/Post" + Pid + "/Default_1.aspx'>登录</a>后才能访问");
            }
            if ((!authBll.AuthCheck(cateMod, mu, "send")))
            {
                send_div.Visible = false;
                noauth_div.Visible = true;
            }
        }
        dt = barBll.SelByID(Pid);
        barBll.AddHitCount(Pid);
        hitcount_span.InnerText = (barMod.HitCount+1).ToString();
        dt.Columns.Add("Layer", typeof(int));
        string msgids = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["Layer"] = (i + 1);
            msgids += dt.Rows[i]["ID"].ToString() + ",";
        }
        LikeDt = likeBll.SelByMsgIDS(msgids.Trim(','),"bar");
        if (Filter.Contains("OnlyLayer"))
        {
            dt.DefaultView.RowFilter = "CUser = " + barMod.CUser;
            dt = dt.DefaultView.ToTable();
        }

        //---------
        MsgRepeater.DataSource = PageCommon.GetPageDT(curPageSize, CPage, dt, out pageCount);
        MsgRepeater.DataBind();
        BindMedalDT();
        MsgPage_L.Text = PageCommon.CreatePageHtml(pageCount, CPage);
        replynum_span1.InnerText = (dt.Rows.Count - 1).ToString();
        pagenum_span1.InnerText = pageCount.ToString();
        Span1.InnerText = (dt.Rows.Count - 1).ToString();
        Span2.InnerText = pageCount.ToString();
        Title_L.Text = barMod.Title;
        PostName_L.Text = barMod.Title;
        Cid = barMod.CateID;
        if (barMod.Status < 0 && barMod.Status == (int)ZLEnum.ConStatus.Recycle) function.WriteErrMsg("该帖子已删除!!", "/PClass?id=" + Cid);
        function.Script(this, "SetImg('" + cateMod.BarImage + "');");
        ReturnBar_a.Text = "<i class='fa fa-arrow-circle-left'></i>返回" + cateMod.CateName;
        ReturnBar_a.NavigateUrl = "/PClass?id="+ Cid;
        int mcount = 0, rcount = 0;
        barBll.GetCount(Cid, out mcount, out rcount);
        if (!barMod.ColledIDS.Contains("," + mu.UserID + ","))
            LikeBtn_Li.Text = string.Format("<input type='button' value='收藏' id='liketie' onclick='ColledBtn(this,{0})'class='btn btn-xs btn-primary' />", Pid);
        else
            LikeBtn_Li.Text = "<input type='button' value='取消收藏' id='liketie' onclick='ColledBtn(this," + Pid + ")'class='btn btn-xs btn-primary collbtn' />";
        //判断是否显示编辑按钮
        if (UserID == barMod.CUser)
            EditBtn_DIV.Visible = true;
    }
    public void BindMedalDT()
    {
        if (!string.IsNullOrEmpty(BarIDS))
        {
            MedalDT = medalsBll.SelByBarIDS(BarIDS.Trim(','));
            MsgRepeater.DataBind();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        LikeDt = null;
    }
    protected void MsgRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (HidePost) return;
            //加载用户回复列表
            DataRowView dr = e.Item.DataItem as DataRowView;
            if (Convert.ToInt32(dr["Pid"]) > 0)
            {
                int pid = Convert.ToInt32(dr["ID"]);
                Literal replyList = e.Item.FindControl("ReplyList_L") as Literal;
                StringWriter sw = new StringWriter();
                Server.Execute("/Guest/Bar/ReplyList.aspx?pid=" + pid + "&PageSize=" + replyPageSize + "&PageIndex=1", sw);
                replyList.Text = Regex.Match(sw.ToString(), "(?<=(start>))[.\\s\\S]*?(?=(</start))", RegexOptions.IgnoreCase).Value;
            }
            else  //如果是第一楼,加载能力中心信息
            {
               
                Repeater platRPT = e.Item.FindControl("Plat_RPT") as Repeater;
                int pid = -Convert.ToInt32(dr["ID"]);
                B_Blog_Msg msgBll = new B_Blog_Msg();
                DataTable dt= msgBll.Sel(pid, "plat");
                if (dt.Rows.Count > 0)
                {
                    e.Item.FindControl("plat_div").Visible = true;
                    platRPT.DataSource = dt;
                    platRPT.DataBind();
                }
            }
            BarIDS += dr["ID"] + ",";
        }
    }
    //添加一条分享
    public M_Guest_Bar FillMsg(string msg, int pid,int rid=0)
    {
        string base64 = StrHelper.CompressString(msg);
        if (base64.Length > 40000) function.WriteErrMsg("发贴失败,原因:内容过长,请减少内容文字");
        M_UserInfo mu = tpuserBll.GetLogin("匿名用户");
        M_Guest_Bar parent=barBll.SelReturnModel(pid);
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
        AddUserExp(mu, parent.CateID, parent.Title);
        return model;
    }
    protected void PostMsg_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = tpuserBll.GetLogin();
        if (mu.Status != 0)
            function.WriteErrMsg("您的账户已被锁定，无法进行发帖、回复等操作！");
        M_Guest_Bar lastMod = barBll.SelLastModByUid(mu, false);
        M_GuestBookCate catemod = cateBll.SelReturnModel(Cid);
        BarOption baroption = GuestConfig.GuestOption.BarOption.Find(v => v.CateID == Cid);
        int usertime = baroption == null ? 120 : baroption.UserTime;
        int sendtime = baroption == null ? 5 : baroption.SendTime;
        if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], VCode.Text.Trim()))
        {
            function.WriteErrMsg("验证码不正确", "/PItem?id=" +Pid+"&cpage="+ CPage);
        }
        else if (catemod.IsBarOwner(mu.UserID))
        {

        }
        else if (mu.UserID > 0 && (DateTime.Now - mu.RegTime).TotalMinutes < usertime)//匿名用户不受此限
        {
            int minute = usertime - (int)(DateTime.Now - mu.RegTime).TotalMinutes;
            function.WriteErrMsg("新注册用户" + usertime + "分钟内不能发贴,你还需要" + minute + "分钟", "javascript:history.go(-1);");
        }
        else if (lastMod != null && ((int)(DateTime.Now - lastMod.CDate).TotalMinutes) < sendtime)
        {
            int second = sendtime - (int)(DateTime.Now - lastMod.CDate).TotalMinutes;
            function.WriteErrMsg("你回复太快了," + second + "分钟后才能再次回复", "javascript:history.go(-1);");
        }
       barMod = FillMsg(MsgContent_T.Text, Pid);
       barBll.Insert(barMod);
       Response.Redirect("/PItem?id="+Pid+"&page="+CPage);
    }
    //回复加积分操作
    private void AddUserExp(M_UserInfo mu, int cateid,string title)
    {
       
        if (!mu.IsNull)//是否匿名
        {
            M_GuestBookCate catemod = cateBll.SelReturnModel(cateid);
            buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis() 
            { 
                score=catemod.ReplyScore,
                detail=string.Format("{0}在版面:{1}回复主题:{2},赠送{3}分", mu.UserName, catemod.CateName, title, catemod.ReplyScore),
                ScoreType=(int)M_UserExpHis.SType.Point
            });
        }
    }
    //------------
    public string GetDel(int type=0)
    {
        int uid = DataConvert.CLng(Eval("CUser"));
        int isfirst = Convert.ToInt32(Eval("Pid")) == 0 ? 1 : 2;
        if (((UserID == uid&&uid!=0) || IsBarOwner)&&type==0)
        {
            return "<a href='/EditContent?ID=" + Eval("ID") + "' title='编辑' style='margin-right:5px;'><span class='fa fa-pencil'></span></a> <a href='javascript:;' onclick='PostDelMsg(" + Eval("ID") + "," + isfirst + ")' title='删除'><span class='fa fa-trash-o'></span> </a>"+(IsBarOwner?"<input type='checkbox' name='idchk' value='" + Eval("ID") + "'/>":"");
        }
        if (((UserID == uid && uid != 0) || IsBarOwner) && type == 1)
            return "<a href='/EditContent?ID=" + Eval("ID") + "'>编辑</a><a href='javascript:;' onclick='PostDelMsg(" + Eval("ID") + "," + isfirst + ")'>删除</a>";
        return "";
    }
    public string GetReply()
    {
        return Convert.ToInt32(Eval("Pid")) > 0 ? "<a id='msg_toggle_a_" + Eval("ID") + "' onclick='DisReply(" + Eval("ID") + ");' style='cursor:pointer;'>收起回复</a>" : "";
    }
    public string GetUrl(string url)
    {
        return url+"&Pid="+Pid;
    }
    public string GetMsg()
    {
        string result = "";
        if (HidePost)
        {
            result = HideTlp;
        }
        else if (DataConvert.CLng(Eval("Status")) == -2)
        {
            if (IsBarOwner)
            {
                result = AlertTlp + StrHelper.DecompressString(Eval("MsgContent").ToString());
            }
            else
            {
                result = UserAlertTlp;
            }
        }
        else
        {
            result =StrHelper.DecompressString(Eval("MsgContent").ToString());
        }
        return result;
    }
    public string GetUserInfo(string str)
    {
        int id = DataConvert.CLng(Eval("CUser"));
        if (str.Equals("groupName"))
        {
            userinfo = buser.GetUserByUserID(id); 
            return bgp.GetByID(DataConverter.CLng(userinfo.GroupID)).GroupName;
        }
        else if (str.Equals("count"))
        {
            DataTable dt = new DataTable();
            dt = barBll.SelByCateID(id.ToString(), 2);
            return dt.Rows.Count + "";
        }
        else if (str.Equals("userBirth"))
        {
            users = buser.GetUserBaseByuserid(id);
            return users.BirthDay;
        }
        else if (str.Equals("userExp"))
        {
            userinfo = buser.GetUserByUserID(id);
            return userinfo.UserExp+"";             
        }
        else if (str.Equals("userSex"))
        {
            users = buser.GetUserBaseByuserid(id);
            if ((users.UserSex + "") == "False")
            {
                return "女";
            }
            else
            {
                return "男";
            }

        }
        else if (str.Equals("regTime"))
        {
            DateTime datetime =tpuserBll.GetLogin().RegTime;
            return datetime.Year + "-" + datetime.Month + "-" + datetime.Day;
        }
        else
        {
            return "";
        }

    }
    public string GetUserFace() 
    {
        return Eval("UserFace") == DBNull.Value ? "" : Eval("UserFace").ToString();
    }
    public string GetHref() 
    {
        int uid = Convert.ToInt32(Eval("CUser") == DBNull.Value ? 0 : Eval("CUser"));
        string result="";
        if (uid > 0)
            result = "href='PostSearch?uid="+uid+"'";
        return result;
    }
    public string GetUName()
    {
        return barBll.GetUName(Eval("HoneyName"), Eval("CUName"));
    }
    protected void Bar_Btn_Click(object sender, EventArgs e)
    {
        M_GuestBookCate cateMod = cateBll.SelReturnModel(Cid);
        int uid = buser.GetLogin().UserID;
        string ids = Request.Form["idchk"];
        if (cateMod.IsBarOwner(uid) && !string.IsNullOrWhiteSpace(ids))
        {
            switch ((sender as LinkButton).CommandArgument)
            {
                case "Del":
                    barBll.UpdateStatus(Cid, ids, (int)ZLEnum.ConStatus.Recycle);
                    if (ids.Contains(Pid.ToString())) Response.Redirect("/PClass?id=" +Cid);
                    break;
                case "AddTop":
                    barBll.UpdateOrderFlag(ids, 1);
                    break;
                case "RemoveTop":
                    barBll.UpdateOrderFlag(ids, 0);
                    break;
                case "AddRecom":
                    barBll.UpdateRecommend(ids, true);
                    break;
                case "RemoveRecom":
                    barBll.UpdateRecommend(ids, false);
                    break;
                case "AddBottom":
                    barBll.UpdateOrderFlag(ids, -1);
                    break;
                case "AddAllTop":
                    barBll.UpdateOrderFlag(ids, 2);
                    break;
                case "RemoveBottom":
                    barBll.UpdateOrderFlag(ids, 0);
                    break;
                case "Hidden":
                    barBll.UpdateStatus(Cid, ids, -2);
                    break;
                case "CancelHidden":
                    barBll.UpdateStatus(Cid, ids, (int)ZLEnum.ConStatus.Audited);
                    break;
                 
            }
        }
        MyBind();
    }
    //--------------------Like
    public string GetCurUser()
    {
        M_UserInfo mu = buser.GetLogin();
        return "{ UserID: \"" + mu.UserID + "\", UserName: \"" + mu.HoneyName + "\", UserFace: \"" + mu.UserFace + "\" }";
    }
    public int GetLikeNum()
    {
        return LikeDt.Select("MsgID=" + Eval("ID") + " AND Source='bar'").Length;
    }
    public string ShowLikeUser()
    {
        //------------------
        DataRow[] drs= LikeDt.Select("MsgID="+Eval("ID")+" AND Source='bar'");
        string result = "";
        foreach (DataRow dr in drs)
        {
            string uname = string.IsNullOrEmpty(DataConvert.CStr(dr["UserName"])) ? dr["UserName"].ToString() : dr["UserName"].ToString();
            result += "<li title='" + uname + "' data-uid='" + dr["CUser"] + "' class='likeids_li'><a href='javascript:;'><img src='" + dr["salt"] + "' onerror=\"this.src='/Images/userface/noface.png';\"/></a></li>";
        }
        return result;
    }
    //--------------------Medals
    public string GetMedalBtn()
    {
        M_UserInfo mu = buser.GetLogin();
        if (mu==null||mu.UserID<=0) { return ""; }//匿名用户与回复贴不能颁发勋章
        int medalnum = 0;
        string disclass = "";//禁用样式
        if (MedalDT != null)
        {
            MedalDT.DefaultView.RowFilter = "BarID="+ DataConvert.CLng(Eval("ID"));
            DataTable curDt = MedalDT.DefaultView.ToTable();
            medalnum = curDt.Rows.Count;
            curDt.DefaultView.RowFilter = "Sender="+mu.UserID;
            disclass = curDt.DefaultView.Count > 0 ? "medal_btn_dis" : "";
        }
       return "<li style='width:auto;'><a title='颁发勋章' onclick='AddMedal(this,"+DataConvert.CLng(Eval("ID"))+")' href='javascript:;' class='likeids_div_a " + disclass + "'><i class='fa fa-sun-o'></i><span class='likenum_span medalnum_btn'>("+ medalnum + ")</span></a></li>";
    }
    public string ShowMedalList()
    {
        if (MedalDT!=null)
        {
            MedalDT.DefaultView.RowFilter = "BarID=" + DataConvert.CLng(Eval("ID"));
            DataTable curdt = MedalDT.DefaultView.ToTable();
            return medalsBll.GetMedalIcon(curdt);
        }
        return "";
    }
    
}
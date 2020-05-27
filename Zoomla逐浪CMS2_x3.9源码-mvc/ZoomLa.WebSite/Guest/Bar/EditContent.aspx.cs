using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Model.Message;
using System.Text.RegularExpressions;
using System.IO;
using ZoomLa.BLL.Message;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Components;
using ZoomLa.BLL.User;


namespace ZoomLaCMS.Guest.Bar
{
    public partial class EditContent :CustomerPageAction
    {
        M_Guest_Bar barMod = new M_Guest_Bar();
        B_Guest_Bar barBll = new B_Guest_Bar();
        B_Guest_BarAuth authBll = new B_Guest_BarAuth();
        B_User buser = new B_User();
        M_GuestBookCate cateMod = new M_GuestBookCate();
        B_GuestBookCate cateBll = new B_GuestBookCate();
        B_TempUser tpuserBll = new B_TempUser();
        public int Pid
        {
            get { return DataConverter.CLng(Request.QueryString["ID"]); }
        }
        public int Cid { get { return DataConverter.CLng(Request.QueryString["Cid"]); } }//CateID
                                                                                         //需要返回的贴子ID
        public int PostID
        {
            get { return DataConverter.CLng(ViewState["PostID"]); }
            set { ViewState["PostID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Pid >= 0)
                {
                    MyBind();
                }
                else
                {
                    MsgTitle_T.Visible = true;
                    Valid_S.Visible = true;
                    M_UserInfo mu = buser.GetLogin();
                    cateMod = cateBll.SelReturnModel(Cid);
                    Tip_T.Text = "发表贴子至" + "<a href='/PClass?id=" + Cid + "' title='返回'>" + cateMod.CateName + "</a>";
                    string errtitle = "<h3 class='panel-title'><span class='fa fa-exclamation-circle'></span> 系统提示</h3>";
                    if (!authBll.AuthCheck(cateMod, mu, "send"))//验证发贴权限
                    {
                        string url = "/User/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.RawUrl);
                        function.WriteErrMsg(errtitle, "你没有登录,请[<a href='" + url + "'>登录</a>]后再发贴!", url);
                    }
                    else if (!authBll.AuthCheck(cateMod, mu, "send"))
                    {
                        function.WriteErrMsg(errtitle, "您没有权限在此发帖,请[<a href='/Index'>返回</a>]论坛主页!", "/Index");
                    }
                }
                ReturnBar_a.Text = "<i class='fa fa-arrow-circle-left'></i>返回" + cateMod.CateName;
                ReturnBar_a.NavigateUrl = "/PClass?id=" + cateMod.CateID;
            }
        }
        void MyBind()
        {
            barMod = barBll.SelReturnModel(Pid);
            if (barMod == null) function.WriteErrMsg("该贴子不存在！");
            PostID = barMod.Pid == 0 ? Pid : barMod.Pid;
            M_UserInfo mu = tpuserBll.GetLogin();// buser.GetLogin();
            cateMod = cateBll.SelReturnModel(barMod.CateID);
            PostName_L.Text = barMod.Title;
            if ((cateMod.IsBarOwner(mu.UserID) || barMod.CUser == mu.UserID) && barMod.ReplyID == 0)
            {
                if (barMod.Pid == 0)
                {
                    MsgTitle_T.Visible = true;
                    MsgTitle_T.Text = barMod.Title;
                }
                MsgContent_T.Text = StrHelper.DecompressString(barMod.MsgContent);
            }
            else
            {
                function.WriteErrMsg("您没有权限修改此贴！");
            }

        }
        protected void PostMsg_Btn_Click(object sender, EventArgs e)
        {
            string msg = MsgContent_T.Text;
            string base64Msg = StrHelper.CompressString(msg);
            if (Pid > 0)
            {
                barMod = barBll.SelReturnModel(Pid);
                barMod.Title = MsgTitle_T.Text;
                barMod.SubTitle = GetSubTitle(ref msg);
                barMod.MsgContent = base64Msg;
                barBll.UpdateByID(barMod);
                Response.Redirect("/PItem?id=" + PostID);
            }
            else
            {
                if (string.IsNullOrEmpty(MsgTitle_T.Text))
                { function.WriteErrMsg("贴子标题不能为空!"); }
                if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], VCode.Text.Trim()))
                {
                    function.WriteErrMsg("验证码不正确");
                }
                GetSubTitle(ref msg);
                M_UserInfo mu = tpuserBll.GetLogin(); //buser.GetLogin();
                if (mu.Status != 0)
                    function.WriteErrMsg("您的账户已被锁定，无法进行发帖、回复等操作！");
                M_Guest_Bar lastMod = barBll.SelLastModByUid(mu);
                BarOption baroption = GuestConfig.GuestOption.BarOption.Find(v => v.CateID == Cid);
                int usertime = baroption == null ? 120 : baroption.UserTime;
                int sendtime = baroption == null ? 5 : baroption.SendTime;
                if (mu.UserID > 0 && (DateTime.Now - mu.RegTime).TotalMinutes < usertime)//匿名用户不受此限
                {
                    int minute = usertime - (int)(DateTime.Now - mu.RegTime).TotalMinutes;
                    function.WriteErrMsg("新注册用户" + usertime + "分钟内不能发贴,你还需要" + minute + "分钟", "javascript:history.go(-1);");
                }
                else if (lastMod != null && (DateTime.Now - lastMod.CDate).TotalMinutes < sendtime)
                {
                    int minute = sendtime - (int)(DateTime.Now - lastMod.CDate).TotalMinutes;
                    function.WriteErrMsg("你发贴太快了," + minute + "分钟后才能再次发贴", "javascript:history.go(-1);");
                }

                M_GuestBookCate catemod = cateBll.SelReturnModel(Cid);
                barMod = FillMsg(MsgTitle_T.Text, msg, catemod);
                int id = barBll.Insert(barMod);
                if (catemod.Status == 1 && mu.UserID != 0)//是否需审核
                {
                    buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis()
                    {
                        score = catemod.SendScore,
                        ScoreType = (int)M_UserExpHis.SType.Point,
                        detail = string.Format("{0} {1}在版面:{2}发表主题:{3},赠送{4}分", DateTime.Now, mu.UserName, catemod.CateName, MsgTitle_T.Text.Trim(), catemod.SendScore)
                    });
                    Response.Redirect("/PClass?id=" + Cid);
                }
                else
                    Response.Redirect("/PClass?id=" + Cid);
            }

        }
        protected void DelMsg_Btn_Click(object sender, EventArgs e)
        {
            if (Pid > 0)
            {
                M_UserInfo mu = tpuserBll.GetLogin();
                barMod = barBll.SelReturnModel(Pid);
                int cateId = barMod.CateID;
                string result = barBll.UpdateStatus(barBll.SelReturnModel(Pid).CateID, Pid.ToString(), -1) ? "ok" : "failed";
                if (result == "ok")
                {
                    function.Script(this, "<script>alert('删除成功！')</script>");
                    Response.Redirect("/PClass?uid=" + mu.UserID);
                }
                else {
                    function.Script(this, "<script>alert('删除失败！')</script>");
                    Response.Redirect("/PItem?id=" + Pid);
                }
            }

        }
        protected void CancelMsg_Btn_Click(object sender, EventArgs e)
        {
            MsgContent_T.Text = "";
        }
        public string GetSubTitle(ref string msg)
        {
            string text = StringHelper.StripHtml(msg, 500).Replace(" ", "");
            string result = (text.Length > 50 ? text.Substring(0, 50) + "..." : text) + "<br/><ul class='thumbul'>";
            RegexHelper regHelper = new RegexHelper();
            int need = 3;
            int curCount = 0;
            if (msg.Contains("edui-faked-video"))//在线视频,如不以swf结尾,则直接显示链接
            {
                string qvtlp = "<li class='thumbli'><img src='/App_Themes/Guest/images/Bar/videologo.png' data-type='quotevideo' data-content='{0}'/></li>";
                //只取其引用,不存实体
                MatchCollection mcs = regHelper.GetValuesBySE(msg, "<embed", "/>");
                for (int i = 0; i < need && i < mcs.Count; i++)
                {
                    string src = regHelper.GetHtmlAttr(mcs[i].Value, "src");//引用区分大小写
                    if (Path.GetExtension(src).Equals(".swf"))
                    {
                        result += string.Format(qvtlp, src);
                        curCount++;
                    }
                    else
                    {
                        msg = msg.Replace(mcs[i].Value, string.Format("<a href='{0}'>{0}</a>", src));
                    }
                }
            }
            if (msg.Contains("<video ") && curCount < need)//上传的视频文件
            {
                string videotlp = "<li class='thumbli'><img src='/App_Themes/Guest/images/Bar/videologo.png' data-type='video' data-content='{0}'/></li>";
                msg = msg.Replace("<video", " <video");
                MatchCollection mcs = regHelper.GetValuesBySE(msg, "<video", ">");
                for (int i = 0; i < need && i < mcs.Count && curCount < need; i++)
                {
                    string src = regHelper.GetHtmlAttr(mcs[i].Value, "src");
                    result += string.Format(videotlp, src);
                    curCount++;
                }
            }
            if (msg.Contains("<img ") && curCount < need)//图片
            {
                MatchCollection mcs = regHelper.GetValuesBySE(msg, "<img", "/>");//匹配图片文件
                for (int i = 0; i < need && i < mcs.Count && curCount < need; i++)
                {
                    if (mcs[i].Value.Contains("/Ueditor")) { continue; }//不存表情
                    result += "<li class='thumbli'>" + mcs[i].Value + "</li>";
                    curCount++;
                }
            }
            return result += "</ul>";

        }
        public M_Guest_Bar FillMsg(string title, string msg, M_GuestBookCate catemod)
        {
            string base64 = StrHelper.CompressString(msg);
            if (base64.Length > 40000) function.WriteErrMsg("取消修改,原因:内容过长,请减少内容");
            M_UserInfo mu = tpuserBll.GetLogin("匿名用户"); //barBll.GetUser();
            M_Guest_Bar model = new M_Guest_Bar();
            model.MsgType = 1;
            model.Status = catemod.Status > 1 ? (int)ZLEnum.ConStatus.UnAudit : (int)ZLEnum.ConStatus.Audited;//判断贴吧是否开启审核，如果是就默认设置为未审核
            model.CUser = mu.UserID;
            model.CUName = mu.HoneyName;
            model.R_CUName = mu.HoneyName;
            model.Title = title;
            model.SubTitle = GetSubTitle(ref msg);
            model.MsgContent = base64;
            model.CateID = catemod.CateID;
            model.IP = EnviorHelper.GetUserIP();
            string ipadd = IPScaner.IPLocation(model.IP);
            ipadd = ipadd.IndexOf("本地") > 0 ? "未知地址" : ipadd;
            model.IP = model.IP + "|" + ipadd;
            model.Pid = 0;
            model.ReplyID = 0;
            model.ColledIDS = "";
            model.IDCode = mu.UserID == 0 ? mu.WorkNum : mu.UserID.ToString();
            model.CDate = DateTime.Now;
            return model;
        }
        public int GetBarPid()
        {
            return Pid;
        }
    }
}
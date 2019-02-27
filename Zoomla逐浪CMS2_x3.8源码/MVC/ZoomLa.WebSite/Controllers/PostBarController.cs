using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
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
using ZoomLa.SQLDAL.SQL;
using ZoomLaCMS.Models.Bar;

namespace ZoomLaCMS.Controllers
{
    public class PostBarController : Ctrl_Guest
    {
        B_Guest_Bar barBll = new B_Guest_Bar();
        M_Guest_Bar barMod = new M_Guest_Bar();
        B_Guest_BarAuth authBll = new B_Guest_BarAuth();
        B_GuestBookCate cateBll = new B_GuestBookCate();
        B_TempUser tuBll=new B_TempUser();
        B_Plat_Like likeBll = new B_Plat_Like();
        B_Guest_Medals medalBll=new B_Guest_Medals();
        public int CateID
        {
            get
            {
                if (ViewBag.CartID == null) { ViewBag.CartID = DataConvert.CLng(Request.QueryString["CateID"]); }
                return ViewBag.CartID;
            }
            set { ViewBag.CartID = value; }
        }
        public ActionResult Index()
        {
            ViewBag.tycount = barBll.SelYTCount();//发贴统计
            ViewBag.rpt_focus = barBll.SelFocus();//热点
            ViewBag.rpt_week = barBll.SelTop(15);//每周热门
            ViewBag.rpt_top = GetBarImg();
            ViewBag.rpt_cart = cateBll.Cate_SelByType(M_GuestBookCate.TypeEnum.PostBar, 0);//第一级贴吧
            ViewBag.cartdt = cateBll.GetCateList();
            return View();
        }
        public ActionResult PClass()
        {
            VM_PClass model = new VM_PClass(Request,CPage);
            model.setting.url = MVCHelper.GetAction("PClass", Request);
            if (function.isAjax())
            {
                return PartialView("PClass_PostList",model);
            }
            model.GetTieCount();
            return View(model);
        }
                //贴子搜索
        public ActionResult PostSearch()
        {
            M_UserInfo mu = tuBll.GetLogin();
            int uid = DataConvert.CLng(Request.QueryString["uid"]);
            string skey = DataConvert.CStr(Request.QueryString["skey"]).Trim();
            bool sellike = DataConvert.CLng(Request.QueryString["islike"]) > 0 ? true : false;
            string skeyTlp = "\"<span style='color:#ff6a00;'>{0}</span>\"";
            VM_PClass model = new VM_PClass();
            model.cateMod = new M_GuestBookCate();
            model.cateMod.BarImage = "/UploadFiles/timg.jpg";
            model.setting = barBll.SelPage(CPage, PSize, 0, uid, skey, true);
            model.setting.url = MVCHelper.GetAction("PostSearch", Request);
            if (function.isAjax())
            {
                return PartialView("PClass_PostList", model);
            }
            if (!string.IsNullOrEmpty(skey))
            {
                model.cateMod.Desc = "相关" + string.Format(skeyTlp, skey.Replace("|", "、")) + "的贴子";
                model.cateMod.CateName = skey + "\"的贴子";
            }
            if (uid > 0)
            {
                M_UserInfo smu = buser.GetUserByUserID(uid);
                model.cateMod.BarImage = smu.UserFace;
                model.cateMod.CateName = smu.HoneyName + "的贴子";
                model.cateMod.Desc = string.Format(skeyTlp, smu.HoneyName) + "的贴子";
            }
            if (sellike)
            {
                model.cateMod.CateName = "我的收藏";
                model.cateMod.Desc = string.Format(skeyTlp, "我的收藏");
            }
            return View(model);
        }
        //贴子内容
        public ActionResult PItem()
        {
            VM_PItem model = null;
            model = new VM_PItem(Request, CPage, PSize);
            if (Request.IsAjaxRequest()) { return PartialView("PItem_List", model); }
            else { return View(model); }
        }
        //贴子的回复
        public PartialViewResult PostReply(int pid)
        {
            PageSetting setting = barBll.SelByPid(CPage, 5, pid);
            setting.url = "/PostBar/PostReply?pid=" + pid;
            return PartialView("PItem_List_Reply", setting);
        }
        //贴子编辑|新建
        public ActionResult EditContent()
        {
            M_UserInfo mu = tuBll.GetLogin();// buser.GetLogin();
            M_Guest_Bar barMod = new M_Guest_Bar();
            M_GuestBookCate cateMod = null;
            if (Mid >= 0)
            {
                barMod = barBll.SelReturnModel(Mid);
                if (barMod == null) {function.WriteErrMsg("该贴子不存在！");return Content(""); }
                cateMod = cateBll.SelReturnModel(barMod.CateID);
                if ((cateMod.IsBarOwner(mu.UserID) || barMod.CUser == mu.UserID) && barMod.ReplyID == 0)
                {
                    barMod.MsgContent = StrHelper.DecompressString(barMod.MsgContent);
                }
                else
                {
                    function.WriteErrMsg("您没有权限修改此贴！");return Content("");
                }

            }
            else
            {
                cateMod = cateBll.SelReturnModel(CateID);
                string errtitle = "<h3 class='panel-title'><span class='fa fa-exclamation-circle'></span> 系统提示</h3>";
                if (!authBll.AuthCheck(cateMod, mu, "send"))//验证发贴权限
                {
                    string url = "/User/Login?ReturnUrl=" + HttpUtility.UrlEncode(Request.RawUrl);
                    function.WriteErrMsg(errtitle, "你没有登录,请[<a href='" + url + "'>登录</a>]后再发贴!", url);return Content("");
                }
                else if (!authBll.AuthCheck(cateMod, mu, "send"))
                {
                    function.WriteErrMsg(errtitle, "您没有权限在此发帖,请[<a href='/Index'>返回</a>]论坛主页!", "/Index");return Content("");
                }
            }
            ViewBag.cateMod=cateMod;
            return View(barMod);
        }
        [HttpPost]
        [ValidateInput(false)]
        public void Post_Add()
        {
            //Mid,CateID
            M_UserInfo mu = tuBll.GetLogin(); //buser.GetLogin();
            if (mu.Status != 0) { function.WriteErrMsg("您的账户已被锁定,无法进行发帖、回复等操作!");return; }
            int pid = DataConvert.CLng(Request.QueryString["pid"]);
            string title = Request.Form["MsgTitle_T"];
            string msg = Request.Form["MsgContent_T"];
            string base64Msg = StrHelper.CompressString(msg);
            string rurl = Request["rurl"];//操作完成后返回哪个链接
            bool auth_barowner = false;
            if (pid > 0)//回复主贴
            {
                M_Guest_Bar pmod = barBll.SelReturnModel(pid);
                CateID = pmod.CateID;
            }
            if (Mid > 0)//编辑贴子
            {
                barMod = barBll.SelReturnModel(Mid);
                CateID = barMod.CateID;
            }
            M_GuestBookCate cateMod = cateBll.SelReturnModel(CateID);
            if (cateMod == null) { function.WriteErrMsg("栏目不存在");return; }
            rurl = string.IsNullOrEmpty(rurl) ? "/PClass?id=" + cateMod.CateID : rurl;
            auth_barowner = cateMod.IsBarOwner(mu.UserID);
            if (Mid > 0)//编辑
            {
                if (barMod.CUser != mu.UserID && !auth_barowner) { function.WriteErrMsg("你无权修改该内容");return; }
                barMod.Title = title;
                barMod.SubTitle = GetSubTitle(ref msg);
                barMod.MsgContent = base64Msg;
                barBll.UpdateByID(barMod);
            }
            else
            {
                if (pid < 1 && string.IsNullOrEmpty(title)) { function.WriteErrMsg("贴子标题不能为空!");return; }
                if (!ZoomlaSecurityCenter.VCodeCheck(Request["VCode_hid"], Request["VCode"])) { function.WriteErrMsg("验证码不正确");return; }
                if (!auth_barowner)//非管理员需要检测权限和接受限制
                {
                    //是否可在该版块发贴子
                    if (!authBll.AuthCheck(cateMod, mu, "send"))
                    {
                        function.WriteErrMsg("你无权在[" + cateMod.CateName + "]版块发布贴子");return;
                    }
                    //是否有时间限制
                    M_Guest_Bar lastMod = barBll.SelLastModByUid(mu);
                    BarOption baroption = GuestConfig.GuestOption.BarOption.Find(v => v.CateID == CateID);
                    int usertime = baroption == null ? 120 : baroption.UserTime;
                    int sendtime = baroption == null ? 5 : baroption.SendTime;
                    if (mu.UserID > 0 && (DateTime.Now - mu.RegTime).TotalMinutes < usertime)//匿名用户不受此限
                    {
                        int minute = usertime - (int)(DateTime.Now - mu.RegTime).TotalMinutes;
                        function.WriteErrMsg("新注册用户" + usertime + "分钟内不能发贴,你还需要" + minute + "分钟", "javascript:history.go(-1);");return;
                    }
                    else if (lastMod != null && (DateTime.Now - lastMod.CDate).TotalMinutes < sendtime)
                    {
                        int minute = sendtime - (int)(DateTime.Now - lastMod.CDate).TotalMinutes;
                        function.WriteErrMsg("你发贴太快了," + minute + "分钟后才能再次发贴", "javascript:history.go(-1);");return;
                    }
                }
                barMod = FillMsg(title, msg, pid, 0, cateMod);
                barBll.Insert(barMod);
                if (cateMod.Status == 1 && mu.UserID > 0 && cateMod.SendScore > 0)//是否需审核
                {
                    buser.AddMoney(mu.UserID, cateMod.SendScore, M_UserExpHis.SType.Point, string.Format("{0} {1}在版面:{2}发表主题:{3},赠送{4}分", DateTime.Now, mu.UserName, cateMod.CateName, barMod.Title, cateMod.SendScore));
                }
            }
            Response.Redirect(rurl);return;
        }
        [HttpPost]
        public int Post_Del(int cateid, string ids)
        {
            if (cateid < 1 || string.IsNullOrEmpty(ids) || mu.UserID < 1) { return Failed; }
            M_GuestBookCate cateMod = cateBll.SelReturnModel(cateid);
            if (cateMod.IsBarOwner(mu.UserID))
            {
                barBll.UpdateStatus(cateid, ids, (int)ZLEnum.ConStatus.Recycle);
            }
            else
            {
                barBll.UpdateStatus(cateid, ids, (int)ZLEnum.ConStatus.Recycle, mu.UserID);
            }
            return Success;
        }
        [HttpPost]
        //管理员对贴子的操作,需要验证管理员权限
        public int Post_OP(int cateid, string ids)
        {
            if (cateid < 1) { return Failed; }
            M_GuestBookCate cateMod = cateBll.SelReturnModel(cateid);
            if (!cateMod.IsBarOwner(mu.UserID)) { return Failed; }
            cateMod = cateBll.SelReturnModel(cateid);
            if (string.IsNullOrEmpty(ids)) { return Failed; }
            switch (Request["action"])
            {
                case "Del":
                    barBll.UpdateStatus(cateid, ids, (int)ZLEnum.ConStatus.Recycle);
                    break;
                case "AddTop":
                    barBll.UpdateOrderFlag(ids, 1);
                    break;
                case "AddAllTop":
                    barBll.UpdateOrderFlag(ids, 2);
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
                case "Checked"://审核
                    barBll.CheckByIDS(ids);
                    break;
                case "UnCheck":
                    barBll.UnCheckByIDS(ids);
                    break;
                case "Hidden":
                    barBll.UpdateStatus(cateid, ids,(int)ZLEnum.ConStatus.Recycle);
                    break;
                case "CancelHidden":
                    barBll.UpdateStatus(cateid, ids, (int)ZLEnum.ConStatus.Audited);
                    break;
                default:
                    throw new Exception(Request["action"]+"不匹配");
            }
            return Success;
        }
        [HttpPost]
        [ValidateInput(false)]
        public string Post_API()
        {
            //Pid贴子ID
            int rid = DataConvert.CLng(Request.QueryString["id"]);
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            string msg = ""; int pid = 0;
            string result = "1" + ":" + Mid;
            M_UserInfo user = buser.GetLogin();
            pid = DataConvert.CLng(Regex.Split(value, ":::")[0]);
            switch (action)
            {
                case "DeleteMsg"://删除
                    result = barBll.UpdateStatus(barBll.SelReturnModel(pid).CateID, pid.ToString(), (int)ZLEnum.ConStatus.Recycle) ? M_APIResult.Success.ToString() : M_APIResult.Failed.ToString();
                    break;
                case "AddReply"://回复
                    msg = Regex.Split(value, ":::")[1];
                    barBll.Insert(FillMsg("", msg, pid, rid));
                    break;
                case "AddReply2"://回复用户,需要切换为Json
                    msg = Regex.Split(value, ":::")[1];
                    barBll.Insert(FillMsg("", msg, pid, rid));
                    break;
                case "AddColl":
                    result = barBll.LikeTie(pid, user.UserID, 1, "ColledIDS") ? "1" : "0";
                    break;
                case "ReColl":
                    result = barBll.LikeTie(pid, user.UserID, 2, "ColledIDS") ? "1" : "0";
                    break;
                case "AddLike":
                    result = likeBll.AddLike(user.UserID, pid, "bar") ? "1" : "0";// barBll.LikeTie(pid, user.UserID, 1) ? "1" : "0";
                    break;
                case "ReLike":
                    result = likeBll.DelLike(user.UserID, pid, "bar") ? "1" : "0";// barBll.LikeTie(pid, user.UserID, 2) ? "1" : "0";
                    break;
                case "AddMedal"://添加勋章
                    result = medalBll.AddMedal_U(pid, user.UserID).ToString();
                    break;
                case "GetMedalNum"://得到用户勋章数量
                    result = medalBll.SelByUid(pid).Rows.Count.ToString();
                    break;
                case "GetUserMedal"://获取用户的勋章
                    result = JsonConvert.SerializeObject(medalBll.SelByUid(pid));
                    break;
            }
            return result;
        }
        //------------------Tools
        private DataTable GetBarImg()
        {
            RegexHelper regexHelper = new RegexHelper();
            DataTable dt = barBll.SelTop1(4);
            dt.Columns.Add(new DataColumn("TopImg", typeof(string)));
            dt.Columns.Add(new DataColumn("Index", typeof(int)));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                dr["Index"] = i;
                string value = regexHelper.GetValueBySE(dr["SubTitle"].ToString(), "<img", "/>");
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace("'", "\"");
                    dr["TopImg"] = regexHelper.GetHtmlAttr(value, "src");
                }
            }
            if (dt.Rows.Count < 1)
            {
                DataRow dr = dt.NewRow();
                dr["Index"] = 0;
                dr["ID"] = 0;
                dr["Title"] = "暂无数据";
                dr["TopImg"] = "/Images/nopic.gif";
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private string GetSubTitle(ref string msg)
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
        private M_Guest_Bar FillMsg(string title, string msg, int pid, int rid = 0, M_GuestBookCate catemod = null)
        {
            if (pid > 0)
            {
                M_Guest_Bar pmod = barBll.SelReturnModel(pid);
                catemod = cateBll.SelReturnModel(pmod.CateID);
            }
            string base64 = StrHelper.CompressString(msg);
            if (base64.Length > 40000) {throw new Exception("取消修改,原因:内容过长,请减少内容"); }
            M_UserInfo mu = tuBll.GetLogin("匿名用户"); //barBll.GetUser();
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
            model.Pid = pid;
            model.ReplyID = rid;
            model.ColledIDS = "";
            model.IDCode = mu.UserID == 0 ? mu.WorkNum : mu.UserID.ToString();
            model.CDate = DateTime.Now;
            return model;
        }
    }
}

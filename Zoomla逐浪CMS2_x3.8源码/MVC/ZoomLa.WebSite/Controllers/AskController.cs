using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Controllers
{
    public class AskController : Controller
    {
        B_Ask askBll = new B_Ask();
        B_GuestAnswer ansBll = new B_GuestAnswer();
        B_AskCommon askComBll = new B_AskCommon();
        B_TempUser b_tuser = new B_TempUser();
        B_User buser = new B_User();
        public int PSize
        {
            get
            {
                return DataConverter.CLng(Request["psize"]);
            }
        }
        public int CPage
        {
            get { int _cpage = DataConverter.CLng(Request["cpage"]); if (_cpage < 1) { _cpage = 1; } return _cpage; }
        }
        //取得最佳回答采纳率
        public string adoption
        {
            get
            {
                double existNum = ansBll.IsExistInt(1);
                double sum = ansBll.getnum();
                return sum == 0 ? "0.00%" : (existNum / sum * 100).ToString("0.00") + "%";
            }
        }
        //取已解决问题总数
        public int solvedcount { get { return askBll.IsExistInt("Status=2"); } }
        //取待解决问题总数
        public int solvingcount { get { return askBll.IsExistInt("Status=1"); } }
        //取得用户总数
        public int usercount { get { return buser.GetUserNameListTotal(""); } }
        private void AskAuth()
        {
            M_UserInfo mu = buser.GetLogin(false);
            //用户组查看权限
            if (!string.IsNullOrEmpty(GuestConfig.GuestOption.WDOption.selGroup))
            {
                string groups = "," + GuestConfig.GuestOption.WDOption.selGroup + ",";
                if (!groups.Contains("," + mu.GroupID.ToString() + ","))
                {
                    function.WriteErrMsg("您没有查看问答中心的权限!", "/");return;
                }
            }
        }
        public ActionResult Default()
        {
            AskAuth();
            ViewBag.solveDt = askBll.Sel("Status=2", "AddTime desc", null);
            ViewBag.hotDt = askBll.SelfieldOrd("QueType", 10);//根据被采纳问题数取知道之星
            return View();
        }
        //问题库
        public ActionResult List()
        {
            AskAuth();
            int type = DataConverter.CLng(Request["type"]);
            PageSetting setting = askBll.SelWaitQuest_SPage(CPage, 10, Request["quetype"], type);
            return View(setting);
        }
        public PartialViewResult List_Data()
        {
            int type = DataConverter.CLng(Request["type"]);
            return PartialView("List_List", askBll.SelWaitQuest_SPage(CPage, 10, Request["quetype"], type));
        }
        //分类大全
        public ActionResult Classification()
        {
            AskAuth();
            //string type = "0";
            //if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            //{
            //    type = Server.HtmlEncode(Request.QueryString["type"]);
            //}
            //if (!string.IsNullOrEmpty(Request.QueryString["ParentID"]))  //点击的是小类
            //{
            //    cateid = Server.HtmlEncode(Request.QueryString["ParentID"]);
            //    M_Grade grademod = B_GradeOption.GetGradeOption(DataConvert.CLng(Request.QueryString["ParentID"]));
            //    catename = grademod.GradeName;
            //    DataTable dt2 = B_GradeOption.GetGradeList(2, DataConvert.CLng(Request.QueryString["ParentID"]));
            //    Repeater1.DataSource = dt2;
            //    Repeater1.DataBind();
            //    if (!string.IsNullOrEmpty(Request.QueryString["GradeID"]))
            //    {
            //        gradeid = Server.HtmlEncode(Request.QueryString["GradeID"]);
            //    }

            //    SqlParameter[] sp = new SqlParameter[]
            //    {
            //    new SqlParameter("@gradeid",gradeid)
            //    };
            //    //dt3 = b_Ask.SelPage(type, pageSize, pageIndex, out itemCount, QueType: gradeid);
            //}
            //else if (!string.IsNullOrEmpty(Request.QueryString["GradeID"]))   //点击的是大类
            //{
            //    cateid = Server.HtmlEncode(Request.QueryString["GradeID"]);
            //    M_Grade grademod = B_GradeOption.GetGradeOption(DataConvert.CLng(Request.QueryString["GradeID"]));
            //    catename = grademod.GradeName;
            //    DataTable dt2 = B_GradeOption.GetGradeList(2, DataConvert.CLng(Request.QueryString["GradeID"]));
            //    Repeater1.DataSource = dt2;
            //    Repeater1.DataBind();
            //    DataTable dtChild = B_GradeOption.GetGradeList(2, DataConvert.CLng(cateid));
            //    List<int> cateids = new List<int>();
            //    cateids.Add(DataConvert.CLng(cateid));
            //    foreach (DataRow dr in dtChild.Rows)
            //    {
            //        cateids.Add(DataConvert.CLng(dr["GradeID"]));
            //    }
            //    //dt3 = b_Ask.Sel(strWhere + " and " + str, " AddTime desc", sp);//str与strwhere未污染
            //    //dt3 = b_Ask.SelPage(type, pageSize, pageIndex, out itemCount,Cateids:cateids);
            //}
            //else
            //{
            //    catename = "全部分类";
            //    DataTable dt = B_GradeOption.GetGradeList(2, 0);
            //    Repeater1.DataSource = dt;
            //    Repeater1.DataBind();
            //    //dt3 = b_Ask.Sel(str, " AddTime desc", null);
            //    //dt3 = b_Ask.SelPage(type, pageSize, pageIndex, out itemCount);
            //}
            ViewBag.askDt = B_GradeOption.GetGradeList(2, DataConverter.CLng(Request.QueryString["ParentID"]));
            return View(new PageSetting() { itemCount = 0 });
        }
        public PartialViewResult Classification_Data()
        {
            return PartialView("Classification_List", new PageSetting() { itemCount = 0 });
        }
        //在线提问
        public ActionResult Add()
        {
            AskAuth();
            ViewBag.title = Server.HtmlEncode(Request["strWhere"]);
            ViewBag.typeDt = B_GradeOption.GetGradeList(2, 0);
            ViewBag.islogin = buser.CheckLogin();
            ViewBag.needlogin = GuestConfig.GuestOption.WDOption.IsLogin;
            return View();
        }
        [ValidateInput(false)]
        public void Add_Submit()
        {
            AskAuth();
            M_UserInfo mu = buser.GetLogin();
            int score = DataConverter.CLng(Request.Form["ddlScore"]);
            if (mu.UserID > 0)
            {
                if (mu.UserExp < score)
                {
                    function.WriteErrMsg("积分不足");return;
                }
            }
            else if (!GuestConfig.GuestOption.WDOption.IsLogin)
            {
                if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], Request.Form["VCode"].Trim()))
                {
                    function.WriteErrMsg("验证码不正确!");return;
                }
            }
            else
            {
                Response.Redirect("/User/Login?ReturnUrl=/Ask/Add");return;
            }
            M_Ask askMod = new M_Ask();
            askMod.Qcontent = Request.Form["txtContent"].Trim();
            askMod.Supplyment = Request.Form["txtSupplyment"].Trim();
            askMod.AddTime = DateTime.Now;
            askMod.UserId = mu.UserID;
            askMod.UserName = mu.UserID > 0 ? mu.UserName : mu.UserName + "[" + mu.WorkNum + "]";
            askMod.Score = score;
            askMod.IsNi = DataConverter.CBool(Request.Form["isNi"]) ? 1 : 0;
            askMod.QueType = Request.Form["subgrade"];
            if (string.IsNullOrEmpty(askMod.QueType)) { askMod.QueType = Request.Form["ddlCate"]; }
            askMod.Elite = 0;
            askMod.Status = 1;
            int flag = askBll.insert(askMod);
            DataTable dts = askBll.Sel();
            if (score > 0 && buser.CheckLogin())
            {
                //悬赏积分
                buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis()
                {
                    score = 0 - score,
                    ScoreType = (int)M_UserExpHis.SType.Point,
                    detail = mu.UserName + "提交问题[" + askMod.Qcontent + "],扣除悬赏积分-" + score
                });
            }
            //string fix = Request["fix"];
            //if (!string.IsNullOrEmpty(fix))//提交时，若求助对象可见，则向求助对象发送一条短信息
            //{
            //    B_Message message = new B_Message();
            //    M_Message messInfo = new M_Message();
            //    messInfo.Incept = fix;
            //    string UserName = mu.UserName;
            //    messInfo.Sender = mu.UserID.ToString();
            //    messInfo.Title = "来自" + mu.UserName + "的问答求助";
            //    messInfo.PostDate = DateTime.Now;
            //    messInfo.Content = "<a href=\"/Guest/Question/MyAnswer?ID=" + (dts.Rows[0]["ID"]).ToString() + "\" target=\"_blank\">" + askMod.Qcontent + "</a>";
            //    messInfo.Savedata = 0;
            //    messInfo.Receipt = "";
            //    int i = message.GetInsert(messInfo);
            //}
            if (flag > 0 && mu.UserID > 0)
            {
                buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis()
                {
                    score = GuestConfig.GuestOption.WDOption.QuestPoint,
                    ScoreType = (int)((M_UserExpHis.SType)(Enum.Parse(typeof(M_UserExpHis.SType), GuestConfig.GuestOption.WDOption.PointType))),
                    detail = mu.UserName + "提交问题[" + askMod.Qcontent + "],增加问答积分" + GuestConfig.GuestOption.WDOption.QuestPoint
                });
            }
            Response.Redirect("AddSuccess"); return;
        }
        public string GetGrade(string value)
        {
            DataTable dt = B_GradeOption.GetGradeList(2, DataConvert.CLng(value));
            return JsonConvert.SerializeObject(dt);
        }
        //问答专家
        public ActionResult Star()
        {
            AskAuth();
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "select top 10 a.* from (select a.UserID as cuid,count(a.UserID) as ccount from ZL_User a,ZL_GuestAnswer b where b.Status=1 and a.UserID=b.UserID group by(a.UserID)) c,ZL_User a where c.cuid=a.UserID order by c.ccount desc", null);//取被采纳问题数前十为知道之星
            return View(dt);
        }
        //我的提问
        public ActionResult MyAskList()
        {
            AskAuth();
            string quetype = Request["quetype"] ?? "";
            PageSetting setting = askBll.SelMyAsk_SPage(CPage, 10, buser.GetLogin().UserID, quetype);
            if (Request.IsAjaxRequest())
            {
                return PartialView("MyAskList_List", setting);
            }
            return View(setting);
        }
        //我的回答
        public ActionResult MyAnswerlist()
        {
            AskAuth();
            M_UserInfo mu = buser.GetLogin();
            string qids = ansBll.GetUserAnswerIDS(mu.UserID);
            if (string.IsNullOrEmpty(qids)) { function.WriteErrMsg("你没有对任何问题进行回答！请回答后在进行查看！", "/Ask/list");return Content(""); }
            PageSetting setting = askBll.SelByIDS(CPage, 10, qids);
            if (Request.IsAjaxRequest()) { return PartialView("MyAnswerlist_List", setting); }
            ViewBag.islogin = buser.CheckLogin();
            return View(setting);
        }
        public ActionResult SearchDetails()
        {
            AskAuth();
            int Mid = DataConvert.CLng(Request["ID"]);
            M_Ask askMod = askBll.SelReturnModel(Mid);
            if (askMod == null) { function.WriteErrMsg("问题不存在"); return Content("");}
            ViewBag.dt1 = ansBll.Sel(" QueId=" + Mid + " And Status=1", " Status desc", null);
            ViewBag.dt2 = ansBll.Sel(" QueId=" + Mid + " And Status=0", " AddTime desc", null);
            ViewBag.dt3 = askBll.Sel("Status=1", "Addtime desc", null);
            return View(askMod);
        }
        //赞同
        public int Approval(string id)
        {
            M_UserInfo mu = buser.GetLogin();
            int Mid = DataConverter.CLng(id);
            DataTable dt = askComBll.U_SelByAnswer(Mid, mu.UserID, 0);
            if (dt.Rows.Count > 0) { return 1; }
            else
            {
                DataTable dt2 = ansBll.Sel("ID=" + id, "", null);
                M_AskCommon askcomMod = new M_AskCommon();
                askcomMod.AskID = DataConverter.CLng(dt2.Rows[0]["QueID"]);
                askcomMod.AswID = Mid;
                askcomMod.UserID = mu.UserID;
                askcomMod.Content = "赞同";
                askcomMod.AddTime = DateTime.Now;
                askcomMod.Type = 0;
                int flag = askComBll.insert(askcomMod);
                if (flag == 1) { return 2; }
                else { return 3; }
            }
        }
        //评论
        public void Comment()
        {
            M_UserInfo mu = buser.GetLogin();
            int Mid = DataConverter.CLng(Request.Form["mid"]);
            DataTable dt = ansBll.Sel("ID=" + Mid, "", null);
            M_AskCommon askcomMod = new M_AskCommon();
            askcomMod.AskID = DataConverter.CLng(dt.Rows[0]["QueID"]);
            askcomMod.AswID = Mid;
            askcomMod.UserID = mu.UserID;
            askcomMod.Content = Request.Form["txtSupplyment"];
            askcomMod.AddTime = DateTime.Now;
            askcomMod.Type = 1;
            if (askComBll.insert(askcomMod) == 1)
            {
                function.WriteSuccessMsg("评论成功", "SearchDetails?ID=" + Request["ID"]);return;
            }
            else
            {
                function.WriteErrMsg("评论失败", "SearchDetails?ID=" + Request["ID"]);return;
            }
        }
        public ActionResult MyAnswer()
        {
            AskAuth();
            M_UserInfo mu = b_tuser.GetLogin();
            if (!string.IsNullOrEmpty(GuestConfig.GuestOption.WDOption.ReplyGroup))
            {
                //用户组回复权限
                string groups = "," + GuestConfig.GuestOption.WDOption.ReplyGroup + ",";
                if (!groups.Contains("," + mu.GroupID.ToString() + ","))
                { function.WriteErrMsg("您没有回复问题的权限!");return Content(""); }
            }
            ViewBag.adoption = adoption;
            ViewBag.solvedcount = solvedcount;
            ViewBag.solvingcount = solvingcount;
            ViewBag.usercount = usercount;
            int Mid = DataConverter.CLng(Request["ID"]);
            M_Ask askMod = askBll.SelReturnModel(Mid);
            if (askMod == null) { function.WriteErrMsg("问题不存在!");return Content(""); }
            if (askMod.Status == 0) { function.WriteErrMsg("该问题未经过审核,无法对其答复!", "/Guest/Ask/List");return Content(""); }
            if (askMod.UserId == buser.GetLogin().UserID) { Response.Redirect("Interactive?ID=" + Mid);return Content(""); }
            ViewBag.ansDt = ansBll.Sel(" QueId=" + Mid + " And supplymentid=0 AND status<>0", "", null);
            return View(askMod);
        }
        public void Answer()
        {
            M_UserInfo tmu = b_tuser.GetLogin();
            if (GuestConfig.GuestOption.WDOption.IsReply && tmu.UserID <= 0)
            {
                Response.Redirect("/User/Login?ReturnUrl=/Guest/Ask/MyAnswer");return;
            }
            M_GuestAnswer ansMod = new M_GuestAnswer();
            ansMod.UserId = tmu.UserID;
            ansMod.Content = Server.HtmlEncode(Request.Form["txtContent"]);
            ansMod.QueId = DataConverter.CLng(Request["ID"]);
            ansMod.AddTime = DateTime.Now;
            ansMod.Status = 0;
            ansMod.UserName = tmu.UserID > 0 ? tmu.UserName : tmu.UserName + "[" + tmu.WorkNum + "]";
            ansMod.supplymentid = 0;
            ansMod.audit = 0;
            ansMod.IsNi = DataConverter.CBool(Request.Form["CkIsNi"]) ? 1 : 0;
            ansBll.insert(ansMod);
            if (tmu.UserID > 0)
            {
                M_Ask askMod = askBll.SelReturnModel(ansMod.QueId);
                string questname = buser.SelReturnModel(askMod.UserId).UserName;
                if (string.IsNullOrEmpty(questname)) { questname = "匿名用户"; }
                buser.ChangeVirtualMoney(tmu.UserID, new M_UserExpHis()
                {
                    score = GuestConfig.GuestOption.WDOption.WDPoint,
                    ScoreType = (int)((M_UserExpHis.SType)(Enum.Parse(typeof(M_UserExpHis.SType), GuestConfig.GuestOption.WDOption.PointType))),
                    detail = string.Format("{0} {1}在问答中心回答了{2}的问题,赠送{3}分", DateTime.Now, tmu.UserName, questname, GuestConfig.GuestOption.WDOption.WDPoint)
                });
            }
            function.WriteSuccessMsg("回答成功", "List");return;
        }
        //我的提问
        public ActionResult Interactive()
        {
            AskAuth();
            int Mid = DataConverter.CLng(Request["ID"]);
            M_Ask askMod = askBll.SelReturnModel(Mid);
            ViewBag.ansDt = ansBll.Sel(" QueId=" + Mid + " And supplymentid=0", " Status desc", null);
            return View(askMod);
        }
        //补充问题
        [HttpPost]
        [ValidateInput(false)]
        public void Supple()
        {
            askBll.UpdateByField("Supplyment", Request.Form["Txtsupment"], DataConverter.CLng(Request["ID"]));
            function.WriteSuccessMsg("提交成功", "Interactive?ID" + Request["ID"]);return;
        }
        //提交追问
        [HttpPost]
        [ValidateInput(false)]
        public void SuppleAsk()
        {
            M_UserInfo mu = buser.GetLogin();
            M_GuestAnswer ansMod = new M_GuestAnswer();
            ansMod.QueId = DataConverter.CLng(Request["ID"]);
            ansMod.Content = this.Request.Form["txtSupplyment"];
            ansMod.AddTime = DateTime.Now;
            ansMod.UserId = buser.GetLogin().UserID;
            ansMod.UserName = buser.GetLogin().UserName;
            ansMod.Status = 0;
            ansMod.audit = 0;
            ansMod.supplymentid = DataConverter.CLng(Request.Form["Rid"]);
            ansBll.insert(ansMod);
            function.WriteSuccessMsg("追问成功!", "Interactive?ID=" + Request["ID"]); return;
        }
        //推荐为满意答案
        public void Recomand()
        {
            int Mid = DataConvert.CLng(Request["ID"]);
            M_GuestAnswer ansMod = ansBll.SelReturnModel(DataConverter.CLng(Request["aid"]));
            M_UserInfo answeuser = buser.SelReturnModel(ansMod.UserId);//回答人用户模型
            ansMod.Status = 1;
            ansBll.UpdateByID(ansMod);
            M_Ask mask = askBll.SelReturnModel(Mid);
            mask.Status = 2;      //问题状态设置为已解决
            if (askBll.SelReturnModel(Mid).Score > 0)
            {
                buser.ChangeVirtualMoney(ansMod.UserId, new M_UserExpHis()//悬赏积分
                {
                    score = mask.Score,
                    ScoreType = (int)M_UserExpHis.SType.Point,
                    detail = answeuser.UserName + "对问题[" + mask.Qcontent + "]的回答被推荐为满意答案,增加悬赏积分+" + mask.Score.ToString()
                });
            }
            buser.ChangeVirtualMoney(ansMod.UserId, new M_UserExpHis()//问答积分
            {
                score = GuestConfig.GuestOption.WDOption.WDRecomPoint,
                ScoreType = (int)((M_UserExpHis.SType)(Enum.Parse(typeof(M_UserExpHis.SType), GuestConfig.GuestOption.WDOption.PointType))),
                detail = answeuser.UserName + "对问题[" + mask.Qcontent + "]的回答被推荐为满意答案,增加问答积分+" + GuestConfig.GuestOption.WDOption.WDRecomPoint
            });
            askBll.UpdateByID(mask);
            function.WriteSuccessMsg("推荐成功！", "Interactive?ID=" + Mid);return;
        }
        public ActionResult MoreProblems()
        {
            AskAuth();
            if (!buser.CheckLogin()) { Response.Redirect("/User/Login?ReturnUrl=/Ask/Add");return Content(""); }
            string type = Request["type"] ?? "";
            PageSetting setting = askBll.SelPage(CPage, 10, type);
            if (Request.IsAjaxRequest()) { return PartialView("AskController", setting); }
            ViewBag.adoption = adoption;
            ViewBag.solvedcount = solvedcount;
            ViewBag.solvingcount = solvingcount;
            ViewBag.usercount = usercount;
            return View(setting);
        }
        public ActionResult AddSuccess()
        {
            AskAuth();
            ViewBag.conflogin = GuestConfig.GuestOption.WDOption.IsLogin;
            ViewBag.adoption = adoption;
            ViewBag.solvedcount = solvedcount;
            ViewBag.solvingcount = solvingcount;
            ViewBag.usercount = usercount;
            return View("AddSuccess");
        }
        public ActionResult SearchList()
        {
            PageSetting setting = askBll.SelWaitQuest_SPage(CPage, 10, Server.HtmlEncode(Request["quetype"]), 2, Server.HtmlEncode(Request["strWhere"]));
            if (Request.IsAjaxRequest()) { return PartialView("SearchList_List", setting); }
            ViewBag.islogin = buser.CheckLogin();
            return View(setting);
        }
        [ChildActionOnly]
        public ActionResult CommonView()
        {
            ViewBag.adoption = adoption;
            ViewBag.solvedcount = solvedcount;
            ViewBag.solvingcount = solvingcount;
            ViewBag.usercount = usercount;
            return View();
        }
    }
}

using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.AdSystem;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.AdSystem;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class UserFuncController : ZLCtrl
    {
        B_User_InviteCode utBll = new B_User_InviteCode();
        B_UserDay dayBll = new B_UserDay();
        B_Client_Basic basBll = new B_Client_Basic();
        B_User_Signin signBll = new B_User_Signin();
        B_ADZone adzBll = new B_ADZone();
        B_Adbuy adbBll = new B_Adbuy();
        B_ChatMsg msgbll = new B_ChatMsg();
        B_Group gpBll = new B_Group();
        B_Temp tpBll = new B_Temp();
        public ActionResult InviteCode()
        {
            PageSetting setting = utBll.Code_SelPage(CPage, PSize, mu.UserID, Request["skey"]);
            if (Request.IsAjaxRequest()) { return PartialView("InviteCode_List", setting); }
            int maxcount = SiteConfig.UserConfig.InviteCodeCount;
            ViewBag.maxcount = maxcount;
            ViewBag.codecount = utBll.Code_Sel(mu.UserID).Rows.Count;
            return View(setting);
        }
        //根据配置生成指定数量的邀请码
        public void InviteCode_Add()
        {
            int maxcount = SiteConfig.UserConfig.InviteCodeCount;
            if (maxcount < 1) { function.WriteErrMsg("未开启邀请码功能"); return; }
            int count = maxcount - utBll.Code_Sel(buser.GetLogin().UserID).Rows.Count;
            if (count > 0)
            {
                utBll.Code_Create(count, mu);
                function.WriteSuccessMsg("生成完成", "InviteCode"); return;
            }
            else
            {
                function.WriteErrMsg("生成取消 ,因为你已经有了" + maxcount + "个未使用的邀请码!", "/User/UserFunc/InviteCode");
            }
        }
        public int Code_Del(string ids)
        {
            utBll.DelByIDS(ids, mu.UserID);
            return 1;
        }
        public int Code_Create()
        {
            int maxcount = SiteConfig.UserConfig.InviteCodeCount;
            int count = maxcount - utBll.Code_Sel(mu.UserID).Rows.Count;
            if (count > 0)
            {
                utBll.Code_Create(count,buser.GetLogin());
                return -1;
            }
            else
            {
                return maxcount;
            }
        }
        public ActionResult SetSecondPwd()
        {
            mu = buser.GetLogin(false);
            ViewBag.isfirst = string.IsNullOrEmpty(mu.PayPassWord);
            return View();
        }
        [HttpPost]
        public void SecondPwd_Set()
        {
            mu=buser.GetLogin(false);
            string action = (Request["action"] ?? "").Trim(' ');
            string pwd = (Request.Form["pwd_t"] ?? "").Trim(' ');
            string oldpwd = (Request.Form["oldpwd_t"] ?? "").Trim(' ');
            if (action.Equals("update"))
            {
                if (mu.PayPassWord != StringHelper.MD5(oldpwd))
                {
                    function.WriteErrMsg("原密码错误,请重新输入！"); return;
                }
            }
            if (string.IsNullOrEmpty(pwd)) { function.WriteErrMsg("二级密码不能为空"); }
            if (StringHelper.MD5(pwd).Equals(mu.PayPassWord)) { function.WriteErrMsg("新密码和原密码不能相同"); return; }
            mu.PayPassWord = StringHelper.MD5(pwd);
            buser.UpDateUser(mu);
            function.WriteSuccessMsg("操作成功", "/User/Info/Index");
        }
        public ActionResult UserDay()
        {
            DataTable dt = dayBll.Select_All(mu.UserID);
            int Mid = DataConverter.CLng(Request["ID"]);
            M_UserDay dayMod = dayBll.GetSelect(Mid);
            ViewBag.dayMod = dayMod;
            return View(dt);
        }
        public void UserDay_Add()
        {
            int Mid = DataConverter.CLng(Request["ID"]);
            M_UserDay dayMod = dayBll.GetSelect(Mid);
            dayMod.D_name = Request.Form["D_name"];
            dayMod.D_date = DataConverter.CDate(Request.Form["D_date"]);
            dayMod.D_Content = Request.Form["D_Content"];
            if (dayMod.id > 0)
            {
                dayBll.GetUpdate(dayMod);
                function.WriteSuccessMsg("修改成功","UserDay?id="+dayMod.id);
            }
            else
            {
                dayMod.D_UserID = mu.UserID;
                dayBll.GetInsert(dayMod);
                function.WriteSuccessMsg("添加成功", "UserDay");
            }
        }
        public ActionResult ConstPassen()
        {
            int type = DataConverter.CLng(Request["type"]);
            string group = Request["group"];
            PageSetting setting = basBll.SelByType_SPage(CPage, PSize, mu.UserID, type, group);
            return View(setting);
        }
        public int ConstPassen_Del(string code)
        {
            basBll.GetDeleteByCode(code);
            return 1;
        }
        public ActionResult UserSignin()
        {
            ViewBag.issign = signBll.IsSignToday(mu.UserID);
            ViewBag.signdays = signBll.GetHasSignDays(mu.UserID);
            return View();
        }
        public int SignIn()
        {
            if (!signBll.IsSignToday(mu.UserID))
            {
                M_User_Signin signMod = new M_User_Signin();
                signMod.CreateTime = DateTime.Now;
                signMod.UserID = mu.UserID;
                signMod.Status = 1;
                signMod.Remind = mu.UserName + "签到";
                signBll.Insert(signMod);
                Response.Redirect("UserSignin");return 1;
            }
            else { return -1; }
        }
        public ActionResult AdPlan()
        {
            PageSetting setting = adbBll.SelPage(CPage, PSize);
            if (Request.IsAjaxRequest()) { return PartialView("AdPlan_List", setting); }
            return View(setting);
        }
        public ActionResult AdPlanAdd()
        {
            DataTable dt = B_ADZone.ADZone_ID();
            ViewBag.noad = dt == null || dt.Rows.Count < 1;
            int id = DataConverter.CLng(Request["ID"]);
            M_Adbuy adbMod = adbBll.SelectId(id);
            ViewBag.ADID_DT = dt;
            return View(adbMod);
        }
        public void AdPlan_Add()
        {
            int Mid = DataConverter.CLng(Request["id"]);
            M_Adbuy adMod = adbBll.SelectId(Mid);
            adMod.ADID = DataConverter.CLng(Request.Form["ADID"]);
            adMod.Ptime = DataConverter.CDate(Request.Form["PTime"]);
            adMod.ShowTime = DataConverter.CLng(Request.Form["ShowTime"]);
            adMod.Scale = DataConverter.CLng(Request.Form["scale"]);
            adMod.Price = DataConverter.CDecimal(Request.Form["price"]);
            adMod.Content = Request.Form["content"];
            if (adMod.ID > 0)
            {
                adbBll.Update(adMod);
                function.WriteSuccessMsg("修改成功!!","AdPlan");
            }
            else
            {
                adMod.UID = mu.UserID;
                adMod.Time = DateTime.Now;
                adbBll.Add(adMod);
                function.WriteSuccessMsg("恭喜，您的广告计划提交成功，请尽快付款完成购买!!","AdPlan");
            }
        }
        public int AdPlan_Del(int id)
        {
            adbBll.Delete(id);
            return Success;
        }
        public ActionResult TalkLog()
        {
            string reuser = Request["reuser"];
            int reuid = buser.GetUserByName(reuser).UserID;
            PageSetting setting = msgbll.SelPage(CPage,PSize,mu.UserID, reuid, Request["sdate"], Request["edate"]);
            if (Request.IsAjaxRequest()) { return PartialView("TalkLog_List",setting); }
            return View(setting);
        }
        public void TalkLog_Down()
        {
            DataTable dt = msgbll.SelByWhere(mu.UserID, buser.GetUserByName(Request.Form["reuser"]).UserID, Request.Form["sdate"], Request.Form["edate"]);
            if (dt.Rows.Count < 1) { function.WriteErrMsg("没有聊天记录,无法导出"); }
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append(dr["UserName"] + dr["CDate"].ToString() + ":\r\n");
                sb.Append(dr["Content"].ToString() + "\r\n");
                sb.Append("---------------------------------------------------------------\r\n");
            }
            string vpath = "/Temp/ChatHis/";
            string filename = function.GetRandomString(8) + ".txt";
            SafeSC.WriteFile(vpath + filename, sb.ToString());
            SafeSC.DownFile(vpath + filename);
            SafeSC.DelFile(vpath + filename);
            Response.End();
        }
        public ActionResult PromotUnion()
        {
            return View();
        }
        public ActionResult Watermark()
        {
            ViewBag.username = mu.UserName;
            return View();
        }
        public ActionResult HtmlToJPG()
        {
            IEBrowHelper ieHelp = new IEBrowHelper();
            string vpath = SiteConfig.SiteOption.UploadDir + "/User/UserFunc/";
            string url = SiteConfig.SiteInfo.SiteUrl.TrimEnd('/');
            Bitmap m_Bitmap = ieHelp.GetWebSiteThumbnail(Request.Url.Scheme + "://" + Request.Url.Authority + "/BU/Cpic.aspx" + Request.Url.Query, 1024, 723, 1024, 723);
            MemoryStream ms = new MemoryStream();
            m_Bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);//JPG、GIF、PNG等均可 
            byte[] buff = ms.ToArray();
            string fname = function.GetRandomString(3) + ".jpg";
            SafeSC.SaveFile(vpath, fname, buff);
            ViewBag.imgsrc = vpath + fname;
            ViewBag.title = Request.QueryString["Name"] + "的证书_" + Call.SiteName;
            ViewBag.imgalt = Request.QueryString["Name"] + "的证书";
            return View();
        }
        //public void DownFile(string url)
        //{
        //    SafeSC.DownFile(url);
        //}
        //用户可自由变换所属会员组,仅VIP组可访问该页面(前端判断)
        public ActionResult ChangeGroup()
        {
            M_Group gpMod = gpBll.SelReturnModel(mu.GroupID);
            M_Temp tpMod = tpBll.SelModelByUid(mu.UserID, 13);
            if (gpMod.VIPGroup != 1 && tpMod == null) { function.WriteErrMsg("你所在的会员组无权使用该功能页"); return null; }
            ViewBag.gpMod = gpMod;
            ViewBag.gpdt = gpBll.Sel();
            return View();
        }
        [HttpPost]
        public void Group_Change()
        {
            M_Temp tpMod = tpBll.SelModelByUid(mu.UserID, 13);
            M_Group gpMod = gpBll.SelReturnModel(mu.GroupID);
            string action = Request.Form["action_hid"];
            if (gpMod.VIPGroup != 1 && tpMod == null) { function.WriteErrMsg("你所在的会员组无权使用该功能页","ChangeGroup"); return; }
            switch (action)
            {
                case "change":
                    int gid = Convert.ToInt32(Request.Form["Group_DP"]);
                    if (tpMod == null)
                    {
                        tpMod = new M_Temp();
                        tpMod.UserID = mu.UserID;
                        tpMod.Str1 = mu.GroupID.ToString();
                        tpMod.UseType = 13;
                        tpBll.Insert(tpMod);
                    }
                    buser.UpdateGroupId(mu.UserID.ToString(), gid);
                    function.WriteSuccessMsg("切换会员组成功", "ChangeGroup");
                    break;
                case "recover":
                    if (tpMod == null) { function.WriteErrMsg("未找到切换前的会员组记录"); return; }
                    buser.UpdateGroupId(mu.UserID.ToString(), Convert.ToInt32(tpMod.Str1));
                    tpBll.Del(tpMod.ID);
                    function.WriteSuccessMsg("恢复会员组成功", "ChangeGroup");
                    break;
            }
        }
    }
}

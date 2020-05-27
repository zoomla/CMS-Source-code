using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class UserZoneController : ZLCtrl
    {
        B_User_Follow fwBll = new B_User_Follow();
        B_User_FriendApply faBll = new B_User_FriendApply();
        B_User_Friend friendBll = new B_User_Friend();
        B_User_BlogStyle bsBll = new B_User_BlogStyle();
        public void Index()
        {
            Response.Redirect("Structure"); return;
        }
        public void Default() { Response.Redirect("Structure"); return; }
        public ActionResult Structure() { return View(); }
        public ActionResult MySubscription()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult FollowList()
        {
            int ztype = DataConverter.CLng(Request["type"]);
            PageSetting setting = null;
            if (ztype == 1)//关注我的
            {
                setting = fwBll.SelByTUser_SPage(CPage, PSize, mu.UserID, Request["skey"]);
            }
            else
            {
                setting = fwBll.SelByUser_SPage(CPage, PSize, mu.UserID, Request["skey"]);
            }
            ViewBag.ztype = ztype;
            return View(setting);
        }
        //取消关注
        public int Follow_Del(string id)
        {
            fwBll.DelByIDS(id);
            return 1;
        }
        #region 好友
        public ActionResult FriendApply()
        {
            string action = Request["action"] ?? "";
            PageSetting setting = null;
            switch (action)
            {
                case "send"://我发送的好友申请
                    setting = faBll.SelMySendApply_SPage(CPage, PSize, mu.UserID);
                    break;
                default://我收到的未处理的好友申请
                    setting = faBll.SelMyReceApply_SPage(CPage, PSize, mu.UserID);
                    break;
            }
            ViewBag.action = action;
            return View(setting);
        }
        public ActionResult FriendList()
        {
            PageSetting setting = friendBll.SelMyFriend_SPage(CPage, PSize, mu.UserID, Request["skey"]);
            return View(setting);
        }
        public PartialViewResult Friend_Data()
        {
            PageSetting setting = friendBll.SelMyFriend_SPage(CPage, PSize, mu.UserID, Request["skey"]);
            return PartialView("FriendList_List", setting);
        }
        public ActionResult UserQuestFriend() { return View(); }
        public ActionResult QueryUser()
        {
            string action = Request["action"] ?? "";
            int sex = 0;
            string username = "";
            int userid = 0;
            switch (action)
            {
                case "vague":
                    sex = DataConverter.CLng(Request["sex"]);
                    ViewBag.where = "sex=" + sex;
                    break;
                case "username":
                    username = Request["username"];
                    ViewBag.where = "username=" + username;
                    break;
                case "uid":
                    userid = DataConverter.CLng(Request["userid"]);
                    ViewBag.where = "userid=" + userid;
                    break;
            }
            PageSetting setting = buser.QueryUser(CPage, PSize, sex, username, userid);
            return View(setting);
        }
        public PartialViewResult QueryUser_Data()
        {
            int sex = DataConverter.CLng(Request["sex"]);
            string username = Request["username"];
            int userid = DataConverter.CLng(Request["userid"]);
            PageSetting setting = buser.QueryUser(CPage, PSize, sex, username, userid);
            return PartialView("QueryUser_List", setting);
        }
        //删除好友
        public int Friend_Del(string id)
        {
            int tuid = DataConverter.CLng(id);
            friendBll.DelFriend(mu.UserID, tuid);
            return 1;
        }
        //同意好友申请
        public int FriendApply_Agree(string id)
        {
            int mid = DataConverter.CLng(id);
            M_User_FriendApply faMod = faBll.SelReturnModel(mid);
            faMod.ZStatus = (int)ZLEnum.ConStatus.Audited;
            faBll.UpdateByID(faMod);
            friendBll.AddFriendByApply(faMod);
            return Success;
        }
        //拒绝好友申请
        public int FriendApply_Reject(string id)
        {
            int mid = DataConverter.CLng(id);
            M_User_FriendApply faMod = faBll.SelReturnModel(mid);
            faMod.ZStatus = (int)ZLEnum.ConStatus.Reject;
            faBll.UpdateByID(faMod);
            return 1;
        }
        public PartialViewResult FriendApplyRece_Data()
        {
            return PartialView("FriendApply_ReceList", faBll.SelMyReceApply_SPage(CPage, PSize, mu.UserID));
        }
        public PartialViewResult FriendApplySend_Data()
        {
            return PartialView("FriendApply_SendList", faBll.SelMySendApply_SPage(CPage, PSize, mu.UserID));
        }
        #endregion
        public ActionResult MyZonePage()
        {
            PageSetting setting = bsBll.SelPage(CPage, PSize);
            if (Request.IsAjaxRequest()) { return PartialView("MyZonePage_List", setting); }
            if (mu.State != 2) { ViewBag.showtype = 1; }
            else if (setting.itemCount < 1) { ViewBag.showtype = 2; }
            else
            {
                if (mu.PageID < 1)
                {
                    ViewBag.sname = "还没有选定模板";
                }
                else
                {
                    M_User_BlogStyle bsMod = bsBll.SelReturnModel(mu.PageID);
                    if (bsMod != null)
                    {
                        ViewBag.sname = bsMod.StyleName + " <a href='/User/Space/SpaceManage?ID=" + mu.UserID + "' target='_blank' class='btn btn-xs btn-info'>访问页面</a>";
                    }
                }
            }
            return View(setting);
        }
        public void MyZonePage_Apply()
        {
            B_User.UpdateField("PageID", Request["ID"], mu.UserID);
            function.WriteSuccessMsg("操作成功", "MyZonePage");
        }
    }
}

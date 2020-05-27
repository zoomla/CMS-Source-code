using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Message;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Controllers
{
    public class FrontBaikeController : Ctrl_Guest
    {
        B_Baike bkBll = new B_Baike();
        B_BaikeEdit editBll = new B_BaikeEdit();
        private int EditID { get { return DataConvert.CLng(Request.QueryString["editid"]); } }
        private string BType { get { return Server.UrlDecode(Request.QueryString["BType"]); } }
        private string Tittle { get { return Server.UrlDecode(Request.QueryString["tittle"]); } }
        //admin
        private string Mode { get { return Request.QueryString["mode"] ?? ""; } }
        public ActionResult Default()
        {
            ViewBag.mu = mu;
            return View();
        }
        public ActionResult Details()
        {
            M_Baike bkMod = GetModel();
            ViewBag.Mid = Mid;
            ViewBag.EditID = EditID;
            return View(bkMod);
        }
        public ActionResult Search()
        {
            int UserID = DataConvert.CLng(Request.QueryString["UserID"]);
            string result = "";
            PageSetting setting = bkBll.SelByInfo(CPage, 20, Tittle, BType, UserID);
            if (function.isAjax()) {return PartialView("Search_List",setting); }
            if (string.IsNullOrEmpty(Tittle + BType) && UserID == 0) { result = "<div class='alert alert-warning'>未输入查询条件</div>"; }
            if (setting.itemCount < 1)
            {
                if (!string.IsNullOrEmpty(Tittle))
                {
                    result = "<div class='alert alert-info'>" + Call.SiteName + "百科尚未收录该词条\"<font color='red'>" + Tittle + "</font>\"，也未找到相关词条。<br/>欢迎您来创建，与广大网友分享关于该词条的信息。<a href='BKEditor?tittle=" + Tittle + "'><font color='blue'>我来创建</font></a></div>";
                }
                else
                {
                    result = "<div class='alert alert-info'>" + Call.SiteName + "百科尚未收录该词条<br/>欢迎您来创建,与广大网友分享各种词条的信息</div>";
                }
            }
            ViewBag.result = result;
            ViewBag.BType = BType;
            return View(setting);
        }
        public ActionResult CreateBaike()
        {
            if (!B_User.CheckIsLogged(Request.RawUrl)) { return null; }
            if (!string.IsNullOrEmpty(GuestConfig.GuestOption.BKOption.CreateBKGroup))
            {
                //用户组编辑权限
                string groups = "," + GuestConfig.GuestOption.BKOption.CreateBKGroup + ",";
                if (!groups.Contains("," + mu.GroupID.ToString() + ","))
                { function.WriteErrMsg("您没有创建词条的权限!"); return null; }
            }
            return View();
        }
        public ActionResult CompBaike()
        {
            M_BaikeEdit editMod = editBll.SelReturnModel(EditID);
            M_Baike bkMod = bkBll.SelModelByFlow(editMod.Flow);
            if (bkMod == null) { bkMod = new M_Baike(); }
            if (editMod == null) { editMod = new M_BaikeEdit(); }
            ViewBag.editMod = editMod;
            return View(bkMod);
        }
        public ActionResult BKClass()
        {
            if (!CheckAuth("view")) {function.WriteErrMsg("您没有查看百科的权限"); }
            //B_GradeOption.GetGradeList(3, 0);
            return View();
        }
        //传入ID才可修改最新的词条
        public ActionResult BKEditor()
        {
            switch (Mode)
            {
                case "admin":
                    if (!B_Admin.CheckIsLogged(Request.RawUrl)) { return null; }
                    break;
                case "user":
                default://是否限定创建权限,用户所在组是否拥有创建权限
                    B_User.CheckIsLogged(Request.RawUrl);
                    if (!bkBll.AuthCheck(GuestConfig.GuestOption.BKOption.CreateBKGroup, mu.GroupID)) { function.WriteErrMsg("你没有创建或编辑词条的权限"); return null; }
                    break;
            }
            M_Baike bkMod = GetModel_Editor(ref err);
            if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg(err); return null; }
            if (bkMod == null) { bkMod = new M_Baike(); }
            return View(bkMod);
        }
        //-----Dialog
        public ActionResult AddRef() {return View("Diag/AddRef"); }
        public ActionResult SelClass()
        {
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "SELECT *,(SELECT Count(GradeID) FROM ZL_Grade WHERE ParentID=A.GradeID) AS ChildCount FROM ZL_Grade A WHERE Cate=3");
            dt.DefaultView.RowFilter = "ParentID>0";
            ViewBag.json = JsonConvert.SerializeObject(dt.DefaultView.ToTable());
            dt.DefaultView.RowFilter = "ParentID=0";
            ViewBag.dt = dt;
            return View("Diag/SelClass");
        }
        //-----
        [HttpPost]
        public void Baike_Create()
        {
            string baike = Server.HtmlEncode(Request.Form["creatbai"]);
            if (mu.IsNull || string.IsNullOrEmpty(baike)) { return; }
            DataTable dt = bkBll.SelBy(baike, 1);
            if (dt.Rows.Count > 0)
            {
                Response.Redirect("Details?action=new&soure=user&tittle=" + baike);
            }
            else
            {
                Response.Redirect("BKEditor?tittle=" + baike);
            }
        }
        //通过百科编辑器更新词条
        [ValidateInput(false)]
        [HttpPost]
        public void BKEditor_Add()
        {
            M_Baike bkMod = GetModel_Editor(ref err);
            M_BaikeEdit editMod = new M_BaikeEdit();
            if (!string.IsNullOrEmpty(err)) { function.WriteErrMsg(err); return; }
            bkMod.Contents = Request.Form["Contents_T"];
            bkMod.Brief = Request.Form["Brief_T"];
            bkMod.BriefImg = Request.Form["pic_hid"];
            bkMod.Extend = Request.Form["info_hid"];
            bkMod.Reference = Request.Form["refence_hid.Value"];
            bkMod.UpdateTime = DateTime.Now;
            bkMod.Classification = Request.Form["class_hid"];
            //bkMod.Editnumb++;
            bkMod.Btype = Request.Form["BType_T"].Replace(" ", "");
            if (bkMod.ID < 1)//新建百科(新百科也需要管理员审核)
            {
                bkMod.Status = (int)ZLEnum.ConStatus.UnAudit;
                bkMod.UserId = mu.UserID;
                bkMod.UserName = mu.UserName;
                bkMod.Tittle = Tittle;
                bkMod.ID = bkBll.insert(bkMod);
                editBll.ConverToEdit(editMod, bkMod, "all");
                editBll.Insert(editMod);
                //function.WriteSuccessMsg("创建百科成功", "/Baike/Details?ID=" + bkMod.ID);
            }
            else if (EditID > 0) //修改自己填的未审核百科
            {
                editMod = editBll.SelReturnModel(EditID);
                editBll.ConverToEdit(editMod, bkMod, "all");
                editBll.UpdateByID(editMod);
            }
            else if (Mid > 0) //存为新的版本,待审核,并跳至用户中心处
            {
                editBll.ConverToEdit(editMod, bkMod);
                editMod.Status = (int)ZLEnum.ConStatus.UnAudit;
                editMod.UserId = mu.UserID;
                editMod.UserName = mu.UserName;
                editMod.OldID = Mid;
                editBll.Insert(editMod);
            }
            else { function.WriteErrMsg("保存条件不正确"); return; }
            function.WriteSuccessMsg("操作成功,请等待管理员审核", "/User/Guest/BaikeContribution");
        }
        //-------------------------------------Tools
        private M_Baike GetModel_Editor(ref string err)
        {
            M_Baike bkMod = null;
            if (Mid < 1 && EditID < 1)
            {
                if (string.IsNullOrEmpty(Tittle)) { err = "未指定词条标题"; return null; }
                bkMod = new M_Baike();
            }
            else if (Mid > 0) { bkMod = bkBll.SelReturnModel(Mid); }
            else if (EditID > 0)
            {
                bkMod = editBll.SelReturnModel(EditID);
                if (mu.UserID != bkMod.UserId) { err = "你无权修改该版本词条"; return null; }
                if (Mode.Equals("admin")) //管理员可不限制操作
                {

                }
                else
                {
                    if (bkMod.Status == 1) { err = "该版本已审核,无法再次修改,<a href='BKEditor?ID=" + bkBll.SelModelByFlow(bkMod.Flow).ID + "'>创建新的版本</a>"; return null; }
                }
            }
            else if (Mid > 0 && EditID > 0) { err = "传参错误,指向不明确"; return null; }
            else { err = "错误,未匹配的版本"; return null; }
            return bkMod;
        }
        private M_Baike GetModel()
        {
            M_UserInfo mu = buser.GetLogin();
            M_Baike model = null;
            if (Mid > 0)
            {
                model = bkBll.SelReturnModel(Mid);
                if (model.Status == (int)ZLEnum.ConStatus.UnAudit) { function.WriteErrMsg("该信息尚未审核"); }
            }
            else if (EditID > 0)
            {
                model = editBll.SelReturnModel(EditID);
                //if (model.UserId != mu.UserID && model.Status == (int)ZLEnum.ConStatus.UnAudit) { function.WriteErrMsg("非创建人无权预览该词条"); }
            }
            if (model == null) { function.WriteErrMsg("词条不存在"); }
            return model;
        }
        private bool CheckAuth(string auth)
        {
            if (!string.IsNullOrEmpty(GuestConfig.GuestOption.BKOption.selGroup))
            {
                //用户组查看权限
                string groups = "," + GuestConfig.GuestOption.BKOption.selGroup + ",";
                if (!groups.Contains("," + mu.GroupID.ToString() + ",")) { return false; }
            }
            return true;
        }
    }
}

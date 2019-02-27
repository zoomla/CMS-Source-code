using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class CashCouponController : ZLCtrl
    {
        B_Arrive avBll = new B_Arrive();
        public void Index()
        {
           Response.Redirect("ArriveManage");
        }
        public ActionResult ArriveManage()
        {
            PageSetting setting = avBll.SelPage(CPage, PSize, mu.UserID, DataConvert.CLng(Request["Type"], -100), DataConvert.CLng(Request["State"], -100));
            return View(setting);
        }
        public PartialViewResult Arrive_Data()
        {
            PageSetting setting = avBll.SelPage(CPage, PSize, mu.UserID, DataConvert.CLng(Request["Type"], -100), DataConvert.CLng(Request["State"], -100));
            return PartialView(setting);
        }
        //用户领取优惠卷
        public ActionResult GetArrive() {
            DataTable dt = avBll.U_SelForGet(mu.UserID);
            return View(dt);
        }
        public int Arrive_Get(string ids)
        {
            int avid = avBll.U_GetArrive(mu.UserID, ids);
            if (avid > 0) return Success;
            return Failed;
        }
        public ActionResult ArriveJihuo()
        {
            return View();
        }
        public void Arrive_Act()
        {
            string ANo = Request.Form["ANo"];
            string APwd = Request.Form["APwd"];

            int uid = avBll.GetUserid(ANo);//uid=0
            decimal mianzhi = avBll.GetOtherArrive(mu.UserID, ANo, APwd);
            //获得用户基本信息
            M_Uinfo muinfo = buser.GetUserBaseByuserid(uid);
            avBll.UpdateState(ANo);
            avBll.UpdateUseTime(ANo);

            //优惠券的实例
            M_Arrive avMod = avBll.SelReturnModel(ANo, APwd);
            if (avMod == null) { function.WriteErrMsg("优惠券不存在"); return; }
            string str = "优惠券激活成功" + "！此优惠券的面值为[" + avMod.Amount + "]";
            function.WriteSuccessMsg(str,"ArriveJiHuo");
        }
    }
}

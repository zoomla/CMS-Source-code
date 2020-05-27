using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Other;
using ZoomLa.BLL.User.Addon;
using ZoomLa.BLL.WxPayAPI;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Other;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class MoneyController : ZLCtrl
    {
        B_Cash cashBll = new B_Cash();
        B_UserAPP uaBll = new B_UserAPP();
        B_WX_RedPacket redBll = new B_WX_RedPacket();
        B_WX_RedDetail detBll = new B_WX_RedDetail();
        //----------------------------------提现
        public ActionResult WithDraw()
        {
            ViewBag.mu = mu;
            return View();
        }
        public ActionResult WithDrawLog()
        {
            PageSetting setting = cashBll.SelPage(CPage, PSize, mu.UserID, Request.Form["STime_T"], Request.Form["ETime_T"]);
            if (Request.IsAjaxRequest())
            {
                return PartialView("WithDrawLog_List", setting);
            }
            return View(setting);
        }
        [HttpPost]
        public void WithDraw_Add()
        {
            M_UserInfo mu = buser.GetLogin(false);
            double money = Convert.ToDouble(Request.Form["Money_T"]);
            double fee = SiteConfig.SiteOption.MastMoney > 0 ? (money * (SiteConfig.SiteOption.MastMoney / 100)) : 0;
            if (money < 1) { function.WriteErrMsg("提现金额错误,不能小于1"); return; }
            if (mu.Purse < (money + fee)) { function.WriteErrMsg("你只有[" + mu.Purse.ToString("f2") + "],需["+(money + fee).ToString("F2")+"]才可提现"); return; }
            //生成提现单据
            M_Cash cashMod = new M_Cash();
            cashMod.UserID = mu.UserID;
            cashMod.money = money;
            cashMod.WithDrawFee = fee;
            cashMod.YName = mu.UserName;
            cashMod.Account = Request.Form["Account_T"];
            cashMod.Bank = Request.Form["Bank_T"];
            cashMod.PeopleName = Request.Form["PName_T"];
            //cashMod.Remark = mu.UserName + "发起提现,金额:" + cashMod.money.ToString("f2");
            cashMod.Remark = Request.Form["Remark_T"];
            buser.MinusVMoney(mu.UserID, money, M_UserExpHis.SType.Purse, cashMod.Remark);
            if (cashMod.WithDrawFee > 0)
            {
                buser.MinusVMoney(mu.UserID, cashMod.WithDrawFee, M_UserExpHis.SType.Purse, "提现手续费率" + SiteConfig.SiteOption.MastMoney.ToString("F2") + "%");
            }
            cashBll.insert(cashMod);
            function.WriteSuccessMsg("提现申请成功,请等待管理员审核", "WithDrawLog"); return;
        }
        //----------------------------------赠送
        //用于将自己的虚拟币赠送给其他用户,默认关闭
        public int SType { get { return DataConverter.CLng(Request.QueryString["stype"]); } }
        public string TypeName { get { return buser.GetVirtualMoneyName(SType); } }
        public ActionResult GiveMoney()
        {
            if (SType <= 0) { function.WriteErrMsg("参数错误!"); return Content(""); }
            M_UserInfo usermod = buser.GetLogin(false);
            double score = GetUserScore(usermod);
            if (score <= 0) { function.WriteErrMsg("您的" + TypeName + "为0!"); return Content(""); }
            //UserScore_L.Text = score.ToString();
            ViewBag.TypeName = TypeName;
            ViewBag.score = score.ToString("f2");
            return View();
        }
        [HttpPost]
        public void GiveMoney_Add()
        {
            M_UserInfo usermod = buser.GetLogin();
            int score = DataConverter.CLng(Request.Form["Score_T"]);
            if (score < 1) { function.WriteErrMsg(TypeName + "不能小于1"); return; }
            if (GetUserScore(usermod) < score) { function.WriteErrMsg("您的" + TypeName + "不足!!"); return; }
            //检测
            M_UserInfo touser = null;
            string UName = Request.Form["UserName_T"];
            int UserID = DataConverter.CLng(Request.Form["UserID_T"]);
            if (!string.IsNullOrEmpty(UName) && UserID > 0)
            {
                M_UserInfo user1 = buser.GetUserByName(UName);
                M_UserInfo user2 = buser.SelReturnModel(UserID);
                if (user1.UserID > 0 && user1.UserID == user2.UserID) { touser = user1; }
            }
            else if (!string.IsNullOrEmpty(UName)){ touser = buser.GetUserByName(UName); }
            else if (UserID > 0) { touser = buser.SelReturnModel(UserID); }
            else { function.WriteErrMsg("会员名和ID至少填写一个"); return; }
            if (touser == null || touser.IsNull) { function.WriteErrMsg("赠送失败，请检查对方会员名或ID是否正确。"); return; }
            if (touser.UserID == usermod.UserID) { function.WriteErrMsg("不能给自己充值!"); return; }

            buser.ChangeVirtualMoney(usermod.UserID, new M_UserExpHis()
            {
                ScoreType = SType,
                score = -score,
                detail = "赠送给" + touser.UserName + score + TypeName
            });
            string remark = Request.Form["Remark_T"];
            if (string.IsNullOrEmpty(remark)) { remark = usermod.UserName + "赠送了" + score + TypeName + "给您!"; }
            buser.ChangeVirtualMoney(touser.UserID, new M_UserExpHis()
            {
                ScoreType = SType,
                score = score,
                detail = remark
            });
            function.WriteSuccessMsg("赠送成功", "GiveMoney?stype="+SType); return;
        }
        //获得用户的虚拟币
        private double GetUserScore(M_UserInfo mu)
        {
            M_UserExpHis.SType ExpType = (M_UserExpHis.SType)SType;
            switch (ExpType)
            {
                case M_UserExpHis.SType.Purse:
                    return mu.Purse;
                case M_UserExpHis.SType.SIcon:
                    return mu.SilverCoin;
                case M_UserExpHis.SType.Point:
                    return mu.UserExp;
                case M_UserExpHis.SType.UserPoint:
                    return mu.UserPoint;
                case M_UserExpHis.SType.DummyPoint:
                    return mu.DummyPurse;
                case M_UserExpHis.SType.Credit:
                    return mu.UserCreit;
                default:
                    return mu.UserExp;
            }
        }
        public ActionResult RedPacket()
        {
            //用户必须关注公众号之后才可访问
            WxAPI.AutoSync(Request.RawUrl);
            Appinfo uaMod = uaBll.SelModelByUid(mu.UserID, "wechat");
            ViewBag.state = uaMod == null ? "0" : "1";
            return View();
        }
        public ActionResult GetRedPacket()
        {
            ViewBag.state = "2";
            ViewBag.err = "";
            string flow = Request["flow"];
            string redcode = Request["redcode"];
            string rurl = Request["rurl"];//成功后的返回页
            //红包相关检测
            M_WX_RedDetail detMod = new M_WX_RedDetail();
            try
            {
                detMod = detBll.SelModelByCode(redcode);
                Appinfo uaMod = uaBll.SelModelByUid(mu.UserID, "wechat");
                if (detMod == null) { ViewBag.err = "红包不存在"; }
                else if (detMod.ZStatus != 1) { ViewBag.err = "该红包已经被使用"; }
                else if (detMod.Amount < 1 || detMod.Amount > 200) { ViewBag.err = "红包金额不正确"; }
                if (!string.IsNullOrEmpty(ViewBag.err)) { return View("RedPacket"); }
                M_WX_RedPacket redMod = redBll.SelReturnModel(detMod.MainID);
                if (!redMod.Flow.Equals(flow)) { ViewBag.err = "匹配码不正确"; }
                else if (redMod.SDate > DateTime.Now) { ViewBag.err = "活动尚未开始"; }
                else if (redMod.EDate < DateTime.Now) { ViewBag.err = "活动已经结束"; }
                if (!string.IsNullOrEmpty(ViewBag.err)) { return View("RedPacket"); }
                WxAPI api = WxAPI.Code_Get(redMod.AppID);
                WxAPI.AutoSync(Request.RawUrl, api.AppId);
                if (mu.IsNull) { ViewBag.err = "用户不存在"; }
                if (uaMod == null) { ViewBag.err = "用户信息不存在"; }
                if (!string.IsNullOrEmpty(ViewBag.err)) { return View("RedPacket"); }
                //-------------------
                string apiUrl = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
                //生成提交参数
                WxPayData wxdata = new WxPayData();
                wxdata.SetValue("nonce_str", WxAPI.nonce);
                wxdata.SetValue("mch_billno", api.AppId.Pay_AccountID + DateTime.Now.ToString("yyyyMMdd") + function.GetRandomString(10, 2));
                wxdata.SetValue("mch_id", api.AppId.Pay_AccountID);
                wxdata.SetValue("wxappid", api.AppId.APPID);
                wxdata.SetValue("send_name", SiteConfig.SiteInfo.SiteName);
                //接受红包的用户用户在wxappid下的openid
                wxdata.SetValue("re_openid", uaMod.OpenID);//oDTTbwLa7WuySP0WcJJzYJmErCQ4
                wxdata.SetValue("total_amount", (int)(detMod.Amount * 100));
                wxdata.SetValue("total_num", 1);
                wxdata.SetValue("wishing", redMod.Wishing);//红包祝福语
                wxdata.SetValue("client_ip", "58.215.75.11");//调用接口的机器IP地址
                wxdata.SetValue("act_name", redMod.Name);//活动名称
                wxdata.SetValue("remark", redMod.Remind);
                wxdata.SetValue("sign", wxdata.MakeSign());
                //随机码,签名
                string xml = wxdata.ToXml();
                string response = HttpService.Post(xml, apiUrl, true, 60, api.AppId);
                //------------------------发放完成(不论成功失败均算已领取,记录好返回)
                detMod.UserID = mu.UserID;
                detMod.UserName = mu.UserName;
                detMod.UseTime = DateTime.Now.ToString();
                //WxPayData result = new WxPayData(api.AppId.Pay_Key);
                //result.FromXml(response);
                //if (result.GetValue("return_code").ToString().Equals("SUCCESS"))//return_msg
                //{
                //    detMod.ZStatus = 99;
                //}
                //else
                //{
                //    detMod.ZStatus = -1;
                //    detMod.Remind = response;
                //}
                detMod.ZStatus = 99; detMod.Remind = response;
                detBll.UpdateByID(detMod);
                ViewBag.state = "3";
                ViewBag.amount = detMod.Amount.ToString("f2");
                return View("RedPacket");
            }
            catch (Exception ex)
            {
                ZLLog.L(ZLEnum.Log.pay, "微信红包异常,领取码:" + redcode + ",原因:" + ex.Message);
                ViewBag.state = "3";
                ViewBag.err = ex.Message;
                //如异常,该红包禁用,等待管理员审核
                detMod.ZStatus = -1;
                detMod.Remind = ex.Message;
                detBll.UpdateByID(detMod);
                return View("RedPacket");
            }
        }
    }
}

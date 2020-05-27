using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
namespace ZoomLaCMS.Areas.User.Controllers
{
    public class IndexController : Controller
    {
        B_User buser = new B_User();
        //
        // GET: /User/Index/
        public ActionResult Index()
        {
            B_PointGrounp pgBll = new B_PointGrounp();
            B_Search shBll = new B_Search();
            M_UserInfo mu = buser.GetLogin();
            M_Uinfo basemu = buser.GetUserBaseByuserid(mu.UserID);
            //--------------------------------------------------
            DataTable dt = shBll.SelByUserGroup(mu.GroupID);
            string userapptlp = "<div class='col-lg-2 col-md-3 col-sm-4 col-xs-4 padding10'><div class='user_menu'><a target='@target' href='@fileUrl'>@ico<br/>@name</a></div></div>";
            string onthertlp = "<li><a target='@target' href='@fileUrl'>@ico<span>@name</span></a></li>";
            string userhtml = "";
            string ontherhtml = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string targetlink = GetLinkTarget(dt.Rows[i]["OpenType"].ToString());
                if (DataConverter.CLng(dt.Rows[i]["EliteLevel"]) == 1)//抽出推荐应用
                {
                    userhtml += ReplaceData(userapptlp, dt.Rows[i]).Replace("@target", targetlink);
                    continue;
                }
                else
                    ontherhtml += ReplaceData(onthertlp, dt.Rows[i]).Replace("@target", targetlink);
            }
            //---------------------
            ViewBag.userhtml = MvcHtmlString.Create(userhtml);
            ViewBag.ontherhtml = MvcHtmlString.Create(ontherhtml);
            ViewBag.pgMod = pgBll.SelectPintGroup((int)mu.UserExp);
            ViewBag.basemu = basemu;
            return View(mu);
        }
        public void Default() { Response.Redirect("/User/Index"); }
        public ActionResult Login() {
            if (buser.CheckLogin()) { Response.Redirect("/User/"); }
            return View(buser.GetLogin());
        }
        //替换userapp字符串
        private string ReplaceData(string value, DataRow dr)
        {
            string[] replce = "ico,fileUrl,name".Split(',');
            foreach (string item in replce)
            {
                string temptext = dr[item].ToString();
                if (item.Equals("ico"))
                {//图标替换
                    temptext = StringHelper.GetItemIcon(temptext, "width:50px;height:50px;");

                }
                value = value.Replace("@" + item, temptext);
            }
            return value;
        }
        private string GetLinkTarget(string target)
        {
            switch (target)
            {
                case "1":
                    return "_blank";
                default:
                    return "_self";
            }
        }
    }
}

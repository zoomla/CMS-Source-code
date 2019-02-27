using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Message;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Models.Bar
{
    //贴子列表
    public class VM_PClass
    {
        B_Guest_Bar barBll = new B_Guest_Bar();
        B_Guest_BarAuth authBll = new B_Guest_BarAuth();
        B_GuestBookCate cateBll = new B_GuestBookCate();
        B_TempUser tuBll=new B_TempUser();
        //父cate下子cate列表
        public DataTable childCateDT = null;
        public M_GuestBookCate cateMod = null;
        //贴子列表
        public PageSetting setting = null;
        //贴子列表支持游客发贴
        public M_UserInfo mu = null;
        //是否为贴吧管理员
        public bool auth_barowner = false;
        //发贴是否需登录
        public bool auth_send = false;
        public int CateID = 0;
        //贴子数,回复数
        public int tiecount = 0, recout = 0;
        public VM_PClass() { }
        //贴子列表
        public VM_PClass(HttpRequestBase Request,int cpage)
        {
            CateID = DataConverter.CLng(Request.QueryString["ID"]);
            mu = tuBll.GetLogin(); //barBll.GetUser();
            //dt = barBll.SelByCateID(CateID.ToString(), 1, true);
            setting = barBll.SelPage(cpage, 15, CateID, 0, Request.Form["skey"],true);
            cateMod = cateBll.SelReturnModel(CateID);
            if (cateMod == null) { function.WriteErrMsg("指定栏目不存在"); }
            #region 权限校验
            if (cateMod.IsBarOwner(mu.UserID))//按用户或搜索时无cateid
            {
                auth_barowner = true;
                auth_send = true;
                //DPBind();
            }
            else//非吧主权限验证
            {
                switch (cateMod.PermiBit)
                {
                    case "1"://版面类别
                        //emptydiv.Style.Add("display", "none");
                        //send_div.Visible = false;
                        //RPT.Visible = false;
                        break;
                    default:
                        if (!authBll.AuthCheck(cateMod, mu, "needlog"))//访问权限
                        {
                            function.WriteErrMsg("你没有访问权限");
                        }
                        auth_send=authBll.AuthCheck(cateMod, mu, "send");//发贴权限
                        break;
                }

            }
            #endregion
            childCateDT = cateBll.GetCateList(CateID);
         
        }
        public void GetTieCount()
        {
            barBll.GetCount(cateMod.CateID, out tiecount, out recout);
        }
        public MvcHtmlString CreateCateLi() 
        {
            DataTable dt = cateBll.Cate_SelByType(M_GuestBookCate.TypeEnum.PostBar);
            BindItem(dt);
            return MvcHtmlString.Create(CateLiHtml);
        }
        private string CateLiHtml = "";
        private void BindItem(DataTable dt, int pid = 0, int layer = 0)
        {
            DataRow[] drs = dt.Select("ParentID=" + pid);
            string pre = layer > 0 ? "{0}<img src='/Images/TreeLineImages/t.gif' />" : "";
            string nbsp = "";
            for (int i = 0; i < layer; i++)
            {
                nbsp += "&nbsp;&nbsp;&nbsp;";
            }
            pre = string.Format(pre, nbsp);
            foreach (DataRow dr in drs)
            {
                CateLiHtml += string.Format("<li class='barli' data-barid='{1}'><a role='menuitem' tabindex='1' href='javascript:;'>{0}</a></li>", pre + dr["CateName"].ToString(), dr["CateID"].ToString());
                BindItem(dt, Convert.ToInt32(dr["CateID"]), (layer + 1));
            }
        }
        //-------------前端使用
        public MvcHtmlString GetTitle(DataRow dr)
        {
            string title = dr["Title"].ToString().Trim();
            title = title.Length > 45 ? title.Substring(0, 44) : title.ToString();
            string result = "";
            result += "<a style='" + dr["Style"] + "' href='/PItem?id=" + dr["ID"] + "'>" + title + "</a>";
            if (DataConvert.CLng(dr["C_Status"]) == 3 && DataConvert.CLng(dr["Status"]) < 1)//后台开启审核,并且未审核
            {
                result = "<span class='uncheck_title'>" + result + "[未审核]</span>";
            }
            return MvcHtmlString.Create(result);
        }
        public MvcHtmlString GetSubTitle(DataRow dr)
        {
            if (DataConvert.CLng(dr["C_Status"]) == 3 && DataConvert.CLng(dr["Status"]) < 1)
                return MvcHtmlString.Create("");
            return MvcHtmlString.Create(DataConvert.CStr(dr["SubTitle"]));
        }
        public MvcHtmlString GetTieStaues(DataRow dr)
        {
            string[] statues = DataConvert.CStr(dr["PostFlag"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string tieimgs = "";
            if (statues.Length > 0)
            {
                for (int i = 0; i < statues.Length; i++)
                {
                    switch (statues[i])
                    {
                        case "Recommend":
                            tieimgs += "<span class='fa fa-trophy'></span> ";
                            break;
                    }
                }
            }
            if (Convert.ToInt32(dr["OrderFlag"]) == 1)
            {
                tieimgs += "<span title='版面置顶' class='fa fa-eyedropper'></span>";
            }
            if (Convert.ToInt32(dr["OrderFlag"]) == 2)
            {
                tieimgs += "<span title='全局置顶' class='fa fa-arrow-up'></span>";
            }
            return MvcHtmlString.Create(tieimgs);
        }
        public MvcHtmlString GetRUser(DataRow dr)
        {
            string tlp = "<span class='uname' title='回复时间:{0}'><span class='fa fa-comment'></span><a href='{1}'>{2}</a>"
                         + "<span class='pull-right remind_g_x'>{3}</span></span>";
            bool isnull = dr["R_CUser"] == DBNull.Value;
            int rcuser = isnull ? DataConvert.CLng(dr["CUser"]) : DataConvert.CLng(dr["R_CUser"]);

            string rcuname = isnull ? function.GetStr(barBll.GetUName(dr["HoneyName"], dr["CUName"]), 6) : function.GetStr(dr["R_CUName"], 6);
            string url = rcuser == 0 ? "javascript:;" : "PostSearch?uid=" + rcuser;
            DateTime cdate = isnull ? Convert.ToDateTime(dr["CDate"]) : Convert.ToDateTime(dr["R_CDate"]);
            string rdate = cdate.ToString("yyyy-MM-dd HH:mm");
            string rdate2 = isnull ? DataConvert.CDate(dr["CDate"]).ToString("yyyy/MM/dd HH:mm") : DataConvert.CDate(dr["R_CDate"]).ToString("yyyy/MM/dd HH:mm");
            return MvcHtmlString.Create(string.Format(tlp, rdate, url, rcuname, rdate2));
        }

    }
}
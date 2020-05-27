using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Message;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Message;
using ZoomLa.SQLDAL;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;
using ZoomLa.Components;
using ZoomLa.BLL.User;

namespace ZoomLaCMS.Guest.Bar
{
    public partial class PostSearch : System.Web.UI.Page
    {
        B_Guest_Bar barBll = new B_Guest_Bar();
        B_TempUser tpuser_Bll = new B_TempUser();
        B_User buser = new B_User();
        public string Skey { get { return (Request.QueryString["skey"] ?? ""); } }
        public bool IsBarOwner = false;
        public int pageSize = 20;
        public int CPage
        {
            get
            {
                return PageCommon.GetCPage();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            if (!string.IsNullOrEmpty(Skey) || !string.IsNullOrEmpty(Request.QueryString["uid"]) || !string.IsNullOrEmpty(Request.QueryString["islike"]))
            {
                int pageCount = 0;
                DataTable dt = new DataTable();
                string skey = Skey.Trim();
                int uid = DataConvert.CLng(Request.QueryString["uid"]);
                bool sellike = DataConvert.CLng(Request.QueryString["islike"]) > 0 ? true : false;
                string skeyTlp = "\"<span style='color:#ff6a00;'>{0}</span>\"";
                string barimg = "/UploadFiles/timg.jpg";
                M_UserInfo mu = tpuser_Bll.GetLogin(); //barBll.GetUser();
                if (!string.IsNullOrEmpty(skey))
                {
                    BarName_L.Text = "相关" + string.Format(skeyTlp, skey.Replace("|", "、")) + "的贴子";
                    Title_L.Text = skey + "\"的贴子";
                    dt = barBll.SelByCateID(skey, 3);
                }
                else if (sellike)
                {
                    BarName_L.Text = string.Format(skeyTlp, "我的收藏");
                    Title_L.Text = "我的收藏";
                    dt = barBll.SelByCateID(mu.UserID.ToString(), 4);
                }
                else
                {
                    if (uid > 0)//匿名用户不允许搜索
                    {
                        M_UserInfo smu = buser.GetUserByUserID(uid);
                        dt = barBll.SelByCateID(uid.ToString(), 2);
                        barimg = smu.UserFace;
                        BarName_L.Text = string.Format(skeyTlp, smu.HoneyName) + "的贴子";
                        Title_L.Text = smu.HoneyName + "的贴子";
                    }
                    else { Title_L.Text = "用户不存在"; }
                }
                totalspan.InnerText = "共搜索到" + dt.Rows.Count + "个贴子";
                function.Script(this, "SetImg('" + barimg + "');");
                RPT.DataSource = PageCommon.GetPageDT(pageSize, CPage, dt, out pageCount);
                RPT.DataBind();
                int tiecount = 0;
                int recout = 0;

                barBll.GetCount(0, out tiecount, out recout);
                replycount.InnerText = recout.ToString();
                dnum_span.InnerText = dt.Rows.Count.ToString();
                dnum_span2.InnerText = tiecount.ToString();
                pagenum_span1.InnerText = pageCount.ToString();
                MsgPage_L.Text = PageCommon.CreatePageHtml(pageCount, CPage);
                if (dt.Rows.Count > 0)
                {
                    contentdiv.Visible = true;
                }
                else
                {
                    emptydiv.Visible = true;
                }
            }



        }
        public string DisCheckBox()
        {
            if (IsBarOwner)
                return "<input type='checkbox' name='idchk' value='" + Eval("ID") + "'/>";
            else return "";
        }
        public string GetTitle()
        {
            //"<a style="<%#Eval("Style") %>" href="<%#CreateUrl(2,Eval("ID")) %>">"
            string title = Eval("Title").ToString().Trim();
            title = title.Length > 45 ? title.Substring(0, 44) : title.ToString();
            string result = "";
            result = "[<a class='search_title' href='/PClass?id=" + Eval("CateID") + "'>" + Eval("CateName") + "</a>]";
            result += "<a style='" + Eval("Style") + "' href='/PItem?id=" + Eval("ID") + "'>" + title + "</a>";
            if (DataConvert.CLng(Eval("C_Status")) == 3 && DataConvert.CLng(Eval("Status")) < 1)//后台开启审核,并且未审核
            {
                result = "<span class='uncheck_title'>" + result + "[未审核]</span>";
            }
            return result;
        }
        public string GetTieStaues()
        {
            string[] statues = Eval("PostFlag").ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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
            if (Convert.ToInt32(Eval("OrderFlag")) == 1)
            {
                tieimgs += "<span title='版面置顶' class='fa fa-eyedropper'></span>";
            }
            if (Convert.ToInt32(Eval("OrderFlag")) == 2)
            {
                tieimgs += "<span title='全局置顶' class='fa fa-arrow-up'></span>";
            }
            return tieimgs;
        }
        public string GetSubTitle()
        {
            if (DataConvert.CLng(Eval("C_Status")) == 3 && DataConvert.CLng(Eval("Status")) < 1)
                return "";
            return Eval("SubTitle").ToString();
        }
        public string GetUName()
        {
            return barBll.GetUName(Eval("HoneyName"), Eval("CUName"));
        }
        public string GetRUser()
        {
            string tlp = "<span class='uname' title='回复时间:{0}'><span class='fa fa-comment'></span><a href='{1}'>{2}</a>"
                         + "<span class='pull-right remind_g_x'>{3}</span></span>";
            bool isnull = Eval("R_CUser") == DBNull.Value;
            int rcuser = isnull ? DataConvert.CLng(Eval("CUser").ToString()) : DataConvert.CLng(Eval("R_CUser").ToString());

            string rcuname = isnull ? function.GetStr(barBll.GetUName(Eval("HoneyName"), Eval("CUName")), 6) : function.GetStr(Eval("R_CUName"), 6);
            string url = rcuser == 0 ? "javascript:;" : "PostSearch?uid=" + rcuser;
            DateTime cdate = isnull ? Convert.ToDateTime(Eval("CDate")) : Convert.ToDateTime(Eval("R_CDate"));
            string rdate = cdate.ToString("yyyy-MM-dd HH:mm");
            string rdate2 = isnull ? DataConvert.CDate(Eval("CDate")).ToString("yyyy/MM/dd HH:mm") : DataConvert.CDate(Eval("R_CDate")).ToString("yyyy/MM/dd HH:mm");
            return string.Format(tlp, rdate, url, rcuname, rdate2);
        }
    }
}
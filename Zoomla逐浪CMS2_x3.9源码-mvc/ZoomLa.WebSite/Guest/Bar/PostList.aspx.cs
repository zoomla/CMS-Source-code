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
    public partial class PostList : System.Web.UI.Page
    {
        M_GuestBookCate cateMod = new M_GuestBookCate();
        M_Guest_Bar barMod = new M_Guest_Bar();
        B_Guest_Bar barBll = new B_Guest_Bar();
        B_GuestBookCate cateBll = new B_GuestBookCate();
        B_Guest_BarAuth authBll = new B_Guest_BarAuth();
        B_User buser = new B_User();
        B_TempUser tpuser_Bll = new B_TempUser();

        public int pageSize = 20;
        public bool IsBarOwner = false;
        //private string forwardTlp = "<i class='fa fa-share-alt'></i>来自社区[<a href='{4}'>{3}</a>]的分享：<strong>{0}</strong> <a href='{1}'>{2}</a>";
        public int Cid { get { return ViewState["cid"] == null ? 0 : Convert.ToInt32(ViewState["cid"]); } set { ViewState["cid"] = value; } }
        public int CPage
        {
            get
            {
                return PageCommon.GetCPage();
            }
        }
        public int CateID { get { return DataConvert.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            int pageCount = 0;
            DataTable dt = new DataTable();
            M_UserInfo mu = tpuser_Bll.GetLogin(); //barBll.GetUser();
            Cid = cateMod.CateID;
            dt = barBll.SelByCateID(CateID.ToString(), 1, true);
            cateMod = cateBll.SelReturnModel(CateID);
            if (cateMod == null) { function.WriteErrMsg("指定栏目不存在"); }
            #region 权限校验

            if (cateMod.IsBarOwner(mu.UserID))//按用户或搜索时无cateid
            {
                barowner_div.Visible = true;
                IsBarOwner = true;
                DPBind();
            }
            else//非吧主权限验证
            {
                switch (cateMod.PermiBit)
                {
                    case "1"://版面类别
                        emptydiv.Style.Add("display", "none");
                        send_div.Visible = false;
                        RPT.Visible = false;
                        break;
                    default:
                        if (!authBll.AuthCheck(cateMod, mu, "needlog"))//访问权限
                        {
                            function.WriteErrMsg("你没有访问权限");
                        }
                        if (!authBll.AuthCheck(cateMod, mu, "send"))//发贴权限
                        {
                            send_div.Visible = false;
                            noauth_div.Visible = true;
                        }
                        break;
                }

            }
            #endregion
            BarInfo_L.Text = cateMod.Desc;
            function.Script(this, "SetImg('" + cateMod.BarImage + "');");
            Title_L.Text = cateMod.CateName;
            BarName_L.Text = cateMod.CateName;
            DataTable chdt = cateBll.GetCateList(CateID);
            if (chdt.Rows.Count > 0)
            {
                ChildRPT.DataSource = chdt;
                ChildRPT.DataBind();
            }
            else
            {
                childBar.Visible = false;
            }
            RPT.DataSource = PageCommon.GetPageDT(pageSize, CPage, dt, out pageCount);
            RPT.DataBind();
            int tiecount = 0;
            int recout = 0;
            barBll.GetCount(CateID, out tiecount, out recout);
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
            Anony_Span.Visible = mu.UserID <= 0;
        }
        //版面下拉列胶
        public void DPBind()
        {
            DataTable dt = cateBll.Cate_SelByType(M_GuestBookCate.TypeEnum.PostBar);
            BindItem(dt);
            selected_Hid.Value = Request.QueryString["PID"];
        }
        public void BindItem(DataTable dt, int pid = 0, int layer = 0)
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
                PCate_ul.InnerHtml += string.Format("<li class='barli' data-barid='{1}'><a role='menuitem' tabindex='1' href='javascript:;'>{0}</a></li>", pre + dr["CateName"].ToString(), dr["CateID"].ToString());
                BindItem(dt, Convert.ToInt32(dr["CateID"]), (layer + 1));
            }
        }
        public string ConverDate(object o, string format)
        {
            if (o != null && o != DBNull.Value)
            {
                return DataConvert.CDate(o).ToString(format);
            }
            else
            {
                return DateTime.Now.ToString(format);
            }
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
        protected void PostMsg_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = tpuser_Bll.GetLogin();//barBll.GetUser();
            if (mu.Status != 0)
                function.WriteErrMsg("您的账户已被锁定，无法进行发帖、回复等操作！");
            M_Guest_Bar lastMod = barBll.SelLastModByUid(mu);
            M_GuestBookCate catemod = cateBll.SelReturnModel(CateID);
            BarOption baroption = GuestConfig.GuestOption.BarOption.Find(v => v.CateID == CateID);
            int usertime = baroption == null ? 120 : baroption.UserTime;
            int sendtime = baroption == null ? 5 : baroption.SendTime;
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], VCode.Text.Trim()))
            {
                function.WriteErrMsg("验证码不正确", "/PClass?id=" + CateID + "&cpage=" + CPage);
            }
            else if (catemod.IsBarOwner(mu.UserID))
            {

            }
            else if (mu.UserID > 0 && (DateTime.Now - mu.RegTime).TotalMinutes < usertime)//匿名用户不受此限
            {
                int minute = usertime - (int)(DateTime.Now - mu.RegTime).TotalMinutes;
                function.WriteErrMsg("新注册用户" + usertime + "分钟内不能发贴,你还需要" + minute + "分钟", "javascript:history.go(-1);");
            }
            else if (lastMod != null && (DateTime.Now - lastMod.CDate).TotalMinutes < sendtime)
            {
                int minute = sendtime - (int)(DateTime.Now - lastMod.CDate).TotalMinutes;
                function.WriteErrMsg("你发贴太快了," + minute + "分钟后才能再次发贴", "javascript:history.go(-1);");
            }
            string msg = MsgContent_T.Text;
            GetSubTitle(ref msg);
            catemod = cateBll.SelReturnModel(CateID);
            barMod = FillMsg(MsgTitle_T.Text, msg, catemod);
            int id = barBll.Insert(barMod);

            if (catemod.Status == 1 && mu.UserID > 0)//是否需审核
            {
                buser.ChangeVirtualMoney(mu.UserID, new M_UserExpHis()
                {
                    score = catemod.SendScore,
                    ScoreType = (int)M_UserExpHis.SType.Point,
                    detail = string.Format("{0} {1}在版面:{2}发表主题:{3},赠送{4}分", DateTime.Now, mu.UserName, catemod.CateName, MsgTitle_T.Text.Trim(), catemod.SendScore)
                });
                Response.Redirect("/PItem?id=" + id);
            }
            else { Response.Redirect(Request.RawUrl); }
        }
        public M_Guest_Bar FillMsg(string title, string msg, M_GuestBookCate cmode)
        {
            string base64 = StrHelper.CompressString(msg);
            if (base64.Length > 40000) function.WriteErrMsg("发贴失败,原因:内容过长,请减少内容文字");
            M_UserInfo mu = tpuser_Bll.GetLogin("匿名用户");//barBll.GetUser();
            M_Guest_Bar model = new M_Guest_Bar();
            model.MsgType = 1;
            model.Status = cmode.Status > 1 ? (int)ZLEnum.ConStatus.UnAudit : (int)ZLEnum.ConStatus.Audited;//判断贴吧是否开启审核，如果是就默认设置为未审核
            model.CUser = mu.UserID;
            model.CUName = mu.HoneyName;
            model.R_CUName = mu.HoneyName;
            model.Title = title.Trim();
            model.SubTitle = GetSubTitle(ref msg);
            model.MsgContent = base64;
            model.CateID = cmode.CateID;
            model.IP = EnviorHelper.GetUserIP();
            model.IDCode = mu.UserID <= 0 ? mu.WorkNum : mu.UserID.ToString();
            string ipadd = IPScaner.IPLocation(model.IP);
            ipadd = ipadd.IndexOf("本地") > 0 ? "未知地址" : ipadd;
            model.IP = model.IP + "|" + ipadd;
            model.Pid = 0;
            model.ReplyID = 0;
            model.ColledIDS = "";
            return model;
        }
        public string GetSubTitle(ref string msg)
        {
            string text = StringHelper.StripHtml(msg, 500).Replace(" ", "");
            string result = (text.Length > 50 ? text.Substring(0, 50) + "..." : text) + "<br/><ul class='thumbul'>";
            RegexHelper regHelper = new RegexHelper();
            int need = 3;
            int curCount = 0;
            if (msg.Contains("edui-faked-video"))//在线视频,如不以swf结尾,则直接显示链接
            {
                string qvtlp = "<li class='thumbli'><img src='/App_Themes/User/videologo.png' data-type='quotevideo' data-content='{0}'/></li>";
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
                string videotlp = "<li class='thumbli'><img src='/App_Themes/User/videologo.png' data-type='video' data-content='{0}'/></li>";
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
        public string GetTitle()
        {
            //"<a style="<%#Eval("Style") %>" href="<%#CreateUrl(2,Eval("ID")) %>">"
            string title = Eval("Title").ToString().Trim();
            title = title.Length > 45 ? title.Substring(0, 44) : title.ToString();
            string result = "";
            result += "<a style='" + Eval("Style") + "' href='/PItem?id=" + Eval("ID") + "'>" + title + "</a>";
            if (DataConvert.CLng(Eval("C_Status")) == 3 && DataConvert.CLng(Eval("Status")) < 1)//后台开启审核,并且未审核
            {
                result = "<span class='uncheck_title'>" + result + "[未审核]</span>";
            }
            return result;
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
        //删除,置顶,精华,沉底
        protected void Bar_Btn_Click(object sender, EventArgs e)
        {
            cateMod = cateBll.SelReturnModel(CateID);
            int uid = buser.GetLogin().UserID;
            string ids = Request.Form["idchk"];
            if (cateMod.IsBarOwner(uid) && !string.IsNullOrWhiteSpace(ids))
            {

                switch ((sender as LinkButton).CommandArgument)
                {
                    case "Del":
                        barBll.UpdateStatus(CateID, ids, (int)ZLEnum.ConStatus.Recycle);
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
                    case "Checked":
                        //审核加积分操作
                        DataTable dt = barBll.SelByIDS(ids);
                        dt.DefaultView.RowFilter = "Status=" + (int)ZLEnum.ConStatus.UnAudit;
                        dt = dt.DefaultView.ToTable();
                        foreach (DataRow item in dt.Rows)
                        {
                            if (DataConverter.CLng(item["Status"]) != (int)ZLEnum.ConStatus.Audited && DataConverter.CLng(item["CUser"]) > 0)
                            {
                                //if (cateMod.IsPlat == 1)
                                //{
                                //    string siteurl = "http://" + Request.Url.Authority + "/";
                                //    string url = B_Guest_Bar.CreateUrl(2, Convert.ToInt32(item["ID"]));
                                //    string cateurl = B_Guest_Bar.CreateUrl(1, cateMod.CateID);
                                //    //需要处理
                                //    msgBll.InsertMsg(string.Format(forwardTlp, item["MsgContent"], siteurl + url, siteurl + url, cateMod.CateName, siteurl + cateurl));
                                //}
                                buser.ChangeVirtualMoney(DataConvert.CLng(item["CUser"]), new M_UserExpHis()
                                {
                                    score = DataConvert.CLng(item["SendScore"]),
                                    detail = string.Format("{0} {1}在版面:{2}发表主题:{3},赠送{4}分", DateTime.Now, item["CUName"], item["Catename"], item["Title"], DataConverter.CLng(item["SendScore"])
                                    , DataConverter.CLng(item["ReplyScore"])),
                                    ScoreType = (int)M_UserExpHis.SType.Point

                                });
                            }
                        }
                        //Audit
                        barBll.CheckByIDS(ids);
                        break;
                    case "UnCheck":
                        barBll.UnCheckByIDS(ids);
                        break;
                }
            }
            Refresh();
        }
        public string DisCheckBox()
        {
            if (IsBarOwner)
                return "<input type='checkbox' name='idchk' value='" + Eval("ID") + "'/>";
            else return "";
        }
        //Uid为提供
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
        public void Refresh()
        {
            Response.Redirect("/PClass?id=" + CateID + "&cpage" + CPage);
        }
        protected void SureMove_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            int cid = DataConvert.CLng(selected_Hid.Value);
            if (!string.IsNullOrEmpty(ids) && cid > 0)
            {
                barBll.ShiftPost(ids, cid);
                Response.Redirect(Request.RawUrl);
            }
            else { function.WriteErrMsg("提交的参数不正确"); }
        }
    }
}
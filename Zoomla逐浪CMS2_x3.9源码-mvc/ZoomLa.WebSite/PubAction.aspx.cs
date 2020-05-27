using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;
using System.Linq;
using ZoomLa.BLL.CreateJS;


namespace ZoomLaCMS
{
    public partial class PubAction : System.Web.UI.Page
    {
        B_Pub pubBll = new B_Pub();
        B_Model modBll = new B_Model();
        B_User buser = new B_User();
        B_ModelField mfBll = new B_ModelField();
        public int Pid
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Pubid"]))
                    return DataConverter.CLng(Request.QueryString["Pubid"]);
                else
                    return DataConverter.CLng(Request.Form["Pubid"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                ProcAjax();
            }
            if (Pid <= 0) function.WriteErrMsg("参数错误!不存在此信息!");
            M_Pub mpub = pubBll.GetSelect(Pid);
            if (mpub != null && mpub.Pubid > 0)
            {
                #region 是否需要登录PubLogin
                if (mpub.PubLogin == 1)
                {
                    string logstr = mpub.PubLoginUrl;
                    if (!buser.CheckLogin())
                    {
                        if (string.IsNullOrEmpty(logstr))
                        {
                            Response.Redirect("/User/Login.aspx?ReturnUrl=" + Request.UrlReferrer.PathAndQuery);
                        }
                        else
                        {
                            Response.Redirect(logstr);
                        }
                        Response.End();
                    }
                }
                #endregion
                #region 是否已经结束
                if (mpub.PubEndTime < DateTime.Now)
                {
                    function.WriteErrMsg("Sorry,此互动已经结束!不接收任何提交的数据!");
                    Response.End();
                }
                #endregion
                string cookflag = Request.Cookies["cookflag"] == null ? "" : Request.Cookies["cookflag"].Value;
                string PubInputer = Request.Form["PubInputer"];
                int PubContentid = DataConverter.CLng(Request.Form["PubContentid"]);
                //初始化参数
                int pubitemid = 0;
                int Pubnum = 0;
                int Parentid = 0;
                int userid = 0; string username = "";
                //IP可发信息数量
                int pubipnum = mpub.PubIPOneOrMore;
                //是否需要审核
                bool isinto = false;
                //用户提交
                string pbtitle = Server.HtmlEncode(Request.Form["PubTitle"]);
                string pbcontent = Server.HtmlEncode(Request.Form["PubContent"]);
                //-----------------------------------------------------------------
                if (mpub.PubCode == 1)
                {
                    if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], Request.Form["PostValidateCode"]))
                    {
                        Response.Write("<script>alert('验证码错误!');window.history.go(-1);</script>");
                        Response.Flush();
                        Response.End();
                    }
                }
                if (buser.CheckLogin())
                {
                    M_UserInfo mu = buser.GetLogin();
                    userid = mu.UserID;
                    username = mu.UserName;
                }
                //查找是否存在主题
                DataTable temptable = mfBll.SelectTableName(mpub.PubTableName, "PubContentid=" + PubContentid + " and Pubupid=" + Pid + " and Parentid=0");
                //同IP的同一篇文章回复次数
                int msgCount = pubBll.SelMsgCount(mpub, PubContentid, mpub.Pubid, EnviorHelper.GetUserIP());
                //最后回复时间
                DataTable selecttime = mfBll.SelectTableName(mpub.PubTableName, "PubContentid=" + PubContentid + " and Pubupid=" + Pid + " and PubIP='" + EnviorHelper.GetUserIP() + "' order by id desc");
                //判断是否存在,获得数据的值
                if (temptable.Rows.Count > 0)
                {
                    pubitemid = DataConverter.CLng(temptable.Rows[0]["ID"]);
                    Pubnum = DataConverter.CLng(temptable.Rows[0]["Pubnum"]);
                }
                //删除超过保留期限的值
                pubBll.DeleteModel(mpub.PubTableName, "DateDiff(d,PubAddTime,getdate())>" + mpub.Pubkeep);
                switch (pubipnum)
                {
                    case 0:
                        Parentid = 0;
                        isinto = true;
                        break;
                    case 1://Only One
                        Parentid = temptable.Rows.Count == 0 ? 0 : Parentid = DataConverter.CLng(temptable.Rows[0]["ID"]);
                        isinto = msgCount < 1;
                        break;
                    default:
                        Parentid = temptable.Rows.Count == 0 ? 0 : Parentid = DataConverter.CLng(temptable.Rows[0]["ID"]);
                        isinto = msgCount < pubipnum;
                        break;
                }
                //开启cookies身份判断(主用于移动端例如微信浏览器)
                if (isinto && mpub.PubFlag == 1 && Request.Cookies["cookflag"] != null)
                {
                    if (string.IsNullOrEmpty(cookflag))
                    {
                        function.WriteErrMsg("身份信息不正确,无法参与互动!");
                    }
                    SqlParameter[] sp = new SqlParameter[] { new SqlParameter("cookflag", cookflag) };
                    string sql = " cookflag=@cookflag";
                    DataTable pubinfoDT = mfBll.SelectTableName(mpub.PubTableName, sql, sp);
                    if (pubinfoDT.Rows.Count >= mpub.PubTimeSlot)
                    {
                        function.WriteErrMsg("很抱歉,每人只能提交" + mpub.PubFlag + "次");
                        isinto = false;
                    }
                }
                //用户信息数量限制
                if (isinto && mpub.PubOneOrMore > 0 && !string.IsNullOrEmpty(username))
                {
                    SqlParameter[] sp2 = new SqlParameter[] { new SqlParameter("uname", username) };
                    string sql2 = "PubContentid=" + PubContentid + " and Pubupid=" + mpub.Pubid + " and PubUserName=@uname order by id desc";
                    DataTable pubinfoDT = mfBll.SelectTableName(mpub.PubTableName, sql2, sp2);
                    if (mpub.PubOneOrMore == 3)
                    {
                        if (pubinfoDT.Select("PubUserID=" + userid).Length > 1) function.WriteErrMsg("很抱歉,对于此次互动，您只能参与一次!");
                    }
                    isinto = pubinfoDT.Rows.Count < mpub.PubOneOrMore;
                }
                if (!isinto)//不符合添加条件
                {
                    if (!string.IsNullOrEmpty(mpub.Puberrmsg))
                        Response.Write("<script>alert('" + mpub.Puberrmsg + "');window.history.go(-1);</script>");
                    else
                        Response.Write("<script>window.history.go(-1);</script>");
                    Response.End();
                    return;
                }
                DateTime PubAddTimes = DateTime.MinValue;
                if (selecttime.Rows.Count > 0 && Parentid > 0)
                {
                    PubAddTimes = DataConverter.CDate(selecttime.Rows[0]["PubAddTime"]);
                }
                //TimeSpan timespan = DateTime.Now - PubAddTimes;//时间间隔,用于限定用户第二次提交限制
                //double TotalSecondsnum = timespan.TotalSeconds;
                //ModelField表中仅存了自定义的字段
                B_CodeModel codeBll = new B_CodeModel(mpub.PubTableName);
                DataRow dr = codeBll.NewModel();
                DataTable mfDT = mfBll.DB_SelByModel(mpub.PubModelID);
                mfDT.DefaultView.RowFilter = "sys_type=0";
                mfDT = mfDT.DefaultView.ToTable();
                //-----固定的系统字段
                dr["Pubnum"] = 1;
                dr["PubIP"] = EnviorHelper.GetUserIP();
                dr["PubUserID"] = userid;
                dr["PubUserName"] = username;
                dr["Pubupid"] = mpub.Pubid;
                dr["PubAddTime"] = DateTime.Now;
                dr["Parentid"] = DataConvert.CLng(Request.Form["Parentid"]);
                dr["PubTitle"] = pbtitle;
                dr["PubContent"] = pbcontent;
                dr["Pubstart"] = mpub.PubIsTrue == 1 ? 0 : 1;//取反
                dr["PubInputer"] = PubInputer;
                dr["PubContentid"] = PubContentid;
                //dr["Mood"] = Request.Form["Mood"];
                if (dr.Table.Columns.Contains("cookflag"))
                {
                    dr["cookflag"] = cookflag;
                }
                //------非系统字段
                for (int i = 0; i < mfDT.Rows.Count; i++)
                {
                    M_ModelField mfMod = new M_ModelField().GetModelFromReader(mfDT.Rows[i]);
                    string value = Server.HtmlEncode(Request.Form[mfMod.FieldName] ?? "");
                    Parentid = DataConvert.CLng(Request.Form["Parentid"]);
                    if (mfMod.IsNotNull && string.IsNullOrEmpty(value))
                    {
                        Response.Write("<script>alert('" + mfMod.FieldName + "不能为空!');window.history.go(-1);</script>");
                        Response.End();
                    }
                    dr[mfMod.FieldName] = value;
                    switch (mpub.PubType)
                    {
                        #region 根据互动类型,进行空值判断
                        case 0:
                            if (string.IsNullOrEmpty(pbcontent))
                            {
                                Response.Write("<script>alert('评论内容不能为空!');window.history.go(-1);</script>");
                                Response.End();
                            }
                            break;
                        case 1:
                            if (string.IsNullOrEmpty(pbtitle))
                            {
                                Response.Write("<script>alert('标题不能为空!');window.history.go(-1);</script>");
                                Response.End();
                            }
                            break;
                        case 2:
                            if (string.IsNullOrEmpty(pbtitle))
                            {
                                Response.Write("<script>alert('标题不能为空!');window.history.go(-1);</script>");
                                Response.End();
                            }
                            if (string.IsNullOrEmpty(pbcontent))
                            {
                                Response.Write("<script>alert('活动信息不能为空!');window.history.go(-1);</script>");
                                Response.End();
                            }
                            break;
                        case 3:
                            if (string.IsNullOrEmpty(pbtitle) || string.IsNullOrEmpty(pbcontent))
                            {
                                Response.Write("<script>alert('标题与内容不能为空!');window.history.go(-1);</script>");
                                Response.End();
                            }
                            break;
                        case 4:
                            if (string.IsNullOrEmpty(pbtitle))
                            {
                                Response.Write("<script>alert('标题不能为空!');window.history.go(-1);</script>");
                                Response.End();
                            }
                            break;
                        case 5:
                            break;
                        case 7://将评星控件的数据写入数据表中
                            dr["PubContent"] = Request.Params["scoreVal"];
                            break;
                        case 8://互动表单
                            break;
                        #endregion
                    }
                    //if (Parentid > 0)
                    //{
                    //    //更新主题信息
                    //    SqlParameter[] sqlparacc = new SqlParameter[1];
                    //    sqlparacc[0] = new SqlParameter("Pubnum", SqlDbType.Int);
                    //    sqlparacc[0].Value = Pubnum + 1;
                    //    pubBll.UpdateModel(sqlparacc, mpub.PubTableName, "id=" + pubitemid.ToString() + "");
                    //}
                }
                codeBll.Insert(dr);
                Upaddnums(mpub); //更新总参与人数
            }
        }
        // 更新总参与人数
        private void Upaddnums(M_Pub mpub)
        {
            mpub.PubAddnum = mpub.PubAddnum + 1;
            pubBll.GetUpdate(mpub);
            buser.ChangeVirtualMoney(buser.GetLogin().UserID, new M_UserExpHis()
            {
                score = SiteConfig.UserConfig.CommentRule,
                detail = "",
                ScoreType = (int)M_UserExpHis.SType.Point
            });
            if (string.IsNullOrEmpty(mpub.PubGourl))
            {
                if (mpub.PubGourl == "")
                {
                    function.Script(this, "ActionSec(1,'" + Server.HtmlEncode(Request.UrlReferrer.ToString()) + "')");
                }
                else
                {
                    B_CreateShopHtml bll = new B_CreateShopHtml();
                    bll.CreateShopHtml(mpub.PubGourl);
                    function.Script(this, "ActionSec(1,'" + bll.CreateShopHtml(mpub.PubGourl) + "')");
                }
            }
            else
            {
                B_CreateShopHtml bll = new B_CreateShopHtml();
                bll.CreateShopHtml(mpub.PubGourl);
                function.Script(this, "ActionSec(1,'" + mpub.PubGourl + "')");
            }
        }
        #region 仅用于评星
        //汇总，获取每项评价的分数，用于评分1
        public string[] GetTotal(string tbName, int contentID)
        {
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + tbName + " Where PubContentID=" + contentID);
            string result = "";
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["PubContent"].ToString();
            }
            string[] scoreArr = result.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //int a = scoreArr.Count(p => p == "SPBZMYD:5");
            return scoreArr;
        }
        // 处理AJAX请求
        private void ProcAjax()
        {
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            int pubID = DataConvert.CLng(Request.Form["pubID"]);//PubID
            string result = "";
            switch (action)
            {
                case "GetScore1"://获取评分1,返回json效果,s:源,v:value
                    int cid = DataConvert.CLng(Request.Form["cid"]);
                    M_Pub pubinfo = pubBll.GetSelect(pubID);
                    string[] valArr = GetTotal(pubinfo.PubTableName, cid);
                    DataTable resultDT = new DataTable();
                    resultDT.Columns.Add(new DataColumn("s", typeof(string)));
                    resultDT.Columns.Add(new DataColumn("v", typeof(string)));
                    string[] arr = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < arr.Length; i++)
                    {
                        DataRow dr = resultDT.NewRow();
                        dr["s"] = arr[i];
                        int s = valArr.Count(p => p == arr[i]); ;
                        dr["v"] = s;
                        resultDT.Rows.Add(dr);
                    }
                    result = JsonHelper.JsonSerialDataTable(resultDT);
                    break;
            }
            Response.Clear(); Response.Write(result); Response.Flush(); Response.End();
        }
        #endregion
    }
}
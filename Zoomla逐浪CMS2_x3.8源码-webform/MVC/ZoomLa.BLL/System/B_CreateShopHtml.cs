using System;
using System.Data;
using System.Text.RegularExpressions;
using ZoomLa.Model;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using ZoomLa.SQLDAL;
using System.Text;
using System.Data.SqlClient;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Globalization;
using System.Web;
using System.Collections.Generic;
using ZoomLa.BLL.Helper;

namespace ZoomLa.BLL
{
    /// <summary>
    /// 将多用户商城及互动模块标签转换成HTML
    /// </summary>
    public class B_CreateShopHtml
    {
        //使用:页面上再$("#VCode").ValidateCode();
        private string pubCodeLabel = "<input type='text' id='VCode' name='VCode' placeholder='验证码' maxlength='6' class='codestyle' style='display:inline;' />"
                                    + "<img id='VCode_img' title='点击刷新验证码' class='codeimg'/><input type='hidden' id='VCode_hid' name='VCode_hid' />";
        public HttpContext CurrentReq = HttpContext.Current;
        public string rawurl="";//Get从其中获取值
        public B_CreateShopHtml() { rawurl = CurrentReq.Request.RawUrl; }
        //// 标签处理
        //public string CreateShopHtml(string TemplateContent, int Storeid, int Userid)
        //{
        //    string Result = TemplateContent;
        //    if (Storeid > 0)
        //    {
        //        Result = GetGradeinfo(TemplateContent, Storeid, Userid);
        //    }
        //    Result = GetGradename(Result);
        //    Result = GetGradeimages(Result);
        //    Result = GetUserGradeName(Result);
        //    Result = GetUserGradeimg(Result);
        //    Result = GetStoreGradeName(Result);
        //    Result = GetStoreGradeimg(Result);
        //    Result = Prolistoption(Result);
        //    return Result;
        //}

        /// <summary>
        /// 方法标签处理，用于黄页内容页等(需要优化)
        /// </summary>
        /// <param name="TemplateContent">模板Html</param>
        /// <returns>解析后的内容</returns>
        public string CreateShopHtml(string TemplateContent)
        {
            string Result = TemplateContent;
            try
            {
                //对指定标签，如GetRequest,GetImg等进处理解析,
                Result = GetRequest(Result);
                //Result = PostRequest(Result);
                Result = Publable(Result);
                Result = Pubinputlable(Result);
                Result = GetUrldecode(Result);
                Result = GetUrlencode(Result);
                Result = GetGetExcel(Result);//只处理{$GetExcel 标签
                Result = GetWord(Result);
                //Result = GetImg(Result);
            }
            catch (Exception ex) { WriteLog("CreateShopHtml", "", ex.Message); }
            return Result;
        }
        /// <summary>
        /// 互动列表标签
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Publable(string str)
        {
            string pattern = @"\{Pub\.Load_[\s\S]*?/}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[i].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string requesttxt = Smallmatch.ToString().Replace(@"{Pub.Load_", "").Replace(@"/}", "");
                    #region 查找数据库里requesttxt标签的属性
                    B_Pub pubs = new B_Pub();
                    DataTable pubinfos = pubs.SelBy(requesttxt);

                    if (pubinfos.Rows.Count > 0)
                    {
                        string PubLogin = pubinfos.Rows[0]["PubLogin"].ToString();
                        string requestvalue = "";

                        if (pubinfos.Rows.Count > 0)
                        {
                            string PubTemplate = pubinfos.Rows[0]["PubTemplate"].ToString(); //模板地址
                            if (string.IsNullOrEmpty(PubTemplate))
                            {
                                int PubModelID = DataConverter.CLng(pubinfos.Rows[0]["PubModelID"].ToString());
                                if (PubModelID > 0)
                                {
                                    B_Model mll = new B_Model();
                                    M_ModelInfo modelinfo = mll.GetModelById(PubModelID);
                                    PubTemplate = modelinfo.ContentModule;
                                }
                            }
                            if (string.IsNullOrEmpty(PubTemplate))
                            {
                                requestvalue = "[产生错误的可能原因：该互动模块未指定模板！]";
                            }
                            else
                            {
                                string IndexDir = SiteConfig.SiteMapath() + SiteConfig.SiteOption.TemplateDir + PubTemplate;
                                if (!FileSystemObject.IsExist(IndexDir, FsoMethod.File))
                                {
                                    requestvalue = "[标签模板不存在]";
                                }
                                else
                                {
                                    string readfile = FileSystemObject.ReadFile(IndexDir);
                                    requestvalue = readfile;
                                }
                            }
                        }
                        else
                        {
                            requestvalue = "[" + pattern + "标签不存在]";
                        }
                        str = str.Replace(Smallmatch.ToString(), requestvalue);
                    }
                    #endregion
                }
            }
            return str;
        }
        /// <summary>
        /// 回复提交窗口标签
        /// </summary>
        public string Pubinputlable(string str)
        {
            string pattern = @"\{Pub\.[\s\S]*?/}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[i].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string requesttxt = Smallmatch.ToString().Replace(@"{Pub.", "").Replace(@"/}", "");
                    #region 查找数据库里requesttxt标签的属性
                    B_Pub pubs = new B_Pub();
                    DataTable pubinfos = pubs.SelBy("", requesttxt);
                    string requestvalue = "";
                    if (pubinfos.Rows.Count > 0)
                    {
                        #region 处理标签
                        string PubTemplate = pubinfos.Rows[0]["PubInputTM"].ToString(); //模板地址
                        if (string.IsNullOrEmpty(PubTemplate))
                        {
                            requestvalue = "";
                        }
                        else
                        {
                            string IndexDir = SiteConfig.SiteMapath() + SiteConfig.SiteOption.TemplateDir + PubTemplate;
                            if (!FileSystemObject.IsExist(IndexDir, FsoMethod.File))
                            {
                                requestvalue = "";
                            }
                            else
                            {
                                string readfile = FileSystemObject.ReadFile(IndexDir);
                                requestvalue = readfile;
                                int PubClass = DataConverter.CLng(pubinfos.Rows[0]["PubClass"].ToString());
                                string getstr = "";
                                switch (PubClass)
                                {
                                    //0-内容 1-商城 2-黄页 3-店铺 4-会员
                                    case 0:
                                        getstr = "ID";
                                        break;
                                    case 1:
                                        getstr = "ID";
                                        break;
                                    case 2:
                                        getstr = "Pageid";
                                        break;
                                    case 3:
                                        getstr = "ID";
                                        break;
                                    case 4:
                                        getstr = "Userid";
                                        break;
                                    case 5:
                                        getstr = "Nodeid";
                                        break;
                                    case 6:
                                        getstr = "";
                                        break;
                                    default:
                                        getstr = "ID";
                                        break;
                                }

                                int PubContentid = -1;

                                if (!string.IsNullOrEmpty(getstr) && CurrentReq.Request != null)
                                {
                                    PubContentid = DataConverter.CLng(CurrentReq.Request.QueryString[getstr]);
                                }
                                if (PubContentid == 0)
                                {
                                    PubContentid = DataConvert.CLng(GetIDVal(rawurl.ToLower()));

                                }
                                requestvalue = requestvalue.Replace("{PubContentid/}", PubContentid.ToString());
                                if (DataConverter.CLng(pubinfos.Rows[0]["PubCode"].ToString()) == 1)
                                {
                                    requestvalue = requestvalue.Replace("{PubCode/}", pubCodeLabel);
                                }
                                else
                                {
                                    requestvalue = requestvalue.Replace("{PubCode/}", "");
                                }

                                B_User buser = new B_User();
                                bool islogiin = buser.CheckLogin();
                                int userid = 0;
                                string username = "";
                                if (islogiin)
                                {
                                    M_UserInfo userinfo = buser.GetLogin();
                                    userid = userinfo.UserID;
                                    username = userinfo.UserName;
                                }
                                #region 处理统计
                                int Pubid = DataConverter.CLng(pubinfos.Rows[0]["Pubid"].ToString());
                                M_Pub mpub = pubs.GetSelect(Pubid);


                                // 删除超过保留期限的值
                                pubs.DeleteModel(mpub.PubTableName, "DateDiff(d,PubAddTime,getdate())>" + mpub.Pubkeep + " and Parentid>0");
                                if (mpub.PubType == 5) //通用统计
                                {
                                    if (mpub.PubLogin == 1 && islogiin)
                                    {
                                        mpub.PubAddnum = mpub.PubAddnum + 1;
                                    }
                                    if (mpub.PubLogin != 1)
                                    {
                                        mpub.PubAddnum = mpub.PubAddnum + 1;
                                    }
                                    pubs.GetUpdate(mpub);//更新参数人数

                                    //查找是否存在第一条信息
                                    B_ModelField mfll = new B_ModelField();
                                    DataTable temptable = mfll.SelectTableName(mpub.PubTableName, "PubContentid=" + PubContentid + " and Pubupid=" + Pubid + " and Parentid=0");

                                    //获取IP
                                    int pubitemid = 0;
                                    int Pubnum = 0;
                                    int Parentid = 0;
                                    //判断是否存在,获得数据的值
                                    if (temptable.Rows.Count > 0)
                                    {
                                        pubitemid = DataConverter.CLng(temptable.Rows[0]["ID"]);
                                        Pubnum = DataConverter.CLng(temptable.Rows[0]["Pubnum"]);
                                    }
                                    int pubipnum = mpub.PubIPOneOrMore;
                                    int Pubusernum = mpub.PubOneOrMore;

                                    DataTable temptables = mfll.SelectTableName(mpub.PubTableName, "PubContentid=" + PubContentid + " and Pubupid=" + Pubid + " and Parentid=0");
                                    if (temptables.Rows.Count < 1)
                                    {
                                        SqlParameter[] sqlpara = new SqlParameter[] {
                                            new SqlParameter("Pubnum",SqlDbType.Int),
                                            new SqlParameter("PubIP",SqlDbType.NVarChar,255),
                                            new SqlParameter("PubContentid",SqlDbType.Int),
                                            new SqlParameter("PubUserID",SqlDbType.Int),
                                            new SqlParameter("PubUserName",SqlDbType.NVarChar,255),
                                            new SqlParameter("Pubupid",SqlDbType.Int),
                                            new SqlParameter("PubAddTime",SqlDbType.DateTime),
                                            new SqlParameter("Parentid",SqlDbType.Int),
                                            new SqlParameter("Pubstart",SqlDbType.Int)
                                            };
                                        sqlpara[0].Value = 0;
                                        sqlpara[1].Value = IPScaner.GetUserIP();
                                        sqlpara[2].Value = DataConverter.CLng(PubContentid);
                                        sqlpara[3].Value = userid;
                                        sqlpara[4].Value = username;
                                        sqlpara[5].Value = Pubid;
                                        sqlpara[6].Value = DateTime.Now;
                                        sqlpara[7].Value = 0;
                                        sqlpara[8].Value = 0;

                                        if (mpub.PubIsTrue == 0)
                                        {
                                            sqlpara[8].Value = 1;
                                        }
                                        else
                                        {
                                            sqlpara[8].Value = 0;
                                        }

                                        pubs.InsertModel(sqlpara, mpub.PubTableName);
                                    }
                                    temptables = SqlHelper.ExecuteTable(CommandType.Text, "SELECT * FROM " + mpub.PubTableName + " WHERE PubContentid=" + PubContentid + " and Pubupid=" + Pubid + " and Parentid=0");
                                    DataTable temptablesinfo = mfll.SelectTableName(mpub.PubTableName, "PubContentid=" + PubContentid + " and Pubupid=" + Pubid + " and PubIP='" + IPScaner.GetUserIP() + "' and Parentid>0 order by id desc");

                                    bool isinto = false;
                                    switch (pubipnum)
                                    {
                                        case 0:
                                            if (temptables.Rows.Count == 0)
                                            {
                                                Parentid = 0;
                                            }
                                            else
                                            {
                                                Parentid = DataConverter.CLng(temptables.Rows[0]["ID"]);
                                            }
                                            isinto = true;
                                            break;
                                        case 1:
                                            if (temptables.Rows.Count == 0)
                                            {
                                                Parentid = 0;
                                            }
                                            else
                                            {
                                                Parentid = DataConverter.CLng(temptables.Rows[0]["ID"]);
                                            }
                                            if (temptablesinfo.Rows.Count < 1)
                                            {
                                                //可添加
                                                isinto = true;
                                            }
                                            else
                                            {
                                                isinto = false;
                                            }
                                            break;
                                        default:
                                            if (temptables.Rows.Count == 0)
                                            {
                                                Parentid = 0;
                                            }
                                            else
                                            {
                                                Parentid = DataConverter.CLng(temptables.Rows[0]["ID"]);
                                            }
                                            if (temptablesinfo.Rows.Count < pubipnum || pubipnum == 0)
                                            {
                                                //可添加
                                                isinto = true;
                                            }
                                            else
                                            {
                                                isinto = false;
                                            }
                                            break;
                                    }

                                    if (isinto == true)
                                    {
                                        temptablesinfo = mfll.SelectTableName(mpub.PubTableName, "PubContentid=" + PubContentid + " and Pubupid=" + Pubid + " and PubUserName='" + username + "' and Parentid>0 order by id desc");
                                        switch (Pubusernum)
                                        {
                                            case 0:
                                                if (temptables.Rows.Count == 0)
                                                {
                                                    Parentid = 0;
                                                }
                                                else
                                                {
                                                    Parentid = DataConverter.CLng(temptables.Rows[0]["ID"]);
                                                }
                                                isinto = true;
                                                break;
                                            case 1:
                                                if (temptables.Rows.Count == 0)
                                                {
                                                    Parentid = 0;
                                                }
                                                else
                                                {
                                                    Parentid = DataConverter.CLng(temptables.Rows[0]["ID"]);
                                                }
                                                if (temptablesinfo.Rows.Count < 1)
                                                {
                                                    //可添加
                                                    isinto = true;
                                                }
                                                else
                                                {
                                                    isinto = false;
                                                }
                                                break;
                                            default:
                                                if (temptables.Rows.Count == 0)
                                                {
                                                    Parentid = 0;
                                                }
                                                else
                                                {
                                                    Parentid = DataConverter.CLng(temptables.Rows[0]["ID"]);
                                                }

                                                if (temptablesinfo.Rows.Count < Pubusernum || Pubusernum == 0)
                                                {
                                                    //可添加
                                                    isinto = true;
                                                }
                                                else
                                                {
                                                    isinto = false;
                                                }
                                                break;
                                        }
                                    }


                                    if (isinto == true)
                                    {
                                        DateTime PubAddTimes = DateTime.MinValue;
                                        if (temptablesinfo.Rows.Count < 1)
                                        {
                                            PubAddTimes = DataConverter.CDate(temptables.Rows[0]["PubAddTime"]);
                                        }
                                        else
                                        {
                                            PubAddTimes = DataConverter.CDate(temptablesinfo.Rows[0]["PubAddTime"]);
                                        }
                                        TimeSpan timespan = DateTime.Now - PubAddTimes;
                                        double TotalSecondsnum = timespan.TotalSeconds;
                                        if ((double)mpub.PubTimeSlot < TotalSecondsnum)
                                        {
                                            SqlParameter[] sqlpara = new SqlParameter[] {
                                            new SqlParameter("Pubnum",SqlDbType.Int),
                                            new SqlParameter("PubIP",SqlDbType.NVarChar,255),
                                            new SqlParameter("PubContentid",SqlDbType.Int),
                                            new SqlParameter("PubUserID",SqlDbType.Int),
                                            new SqlParameter("PubUserName",SqlDbType.NVarChar,255),
                                            new SqlParameter("Pubupid",SqlDbType.Int),
                                            new SqlParameter("PubAddTime",SqlDbType.DateTime),
                                            new SqlParameter("Parentid",SqlDbType.Int),
                                            new SqlParameter("Pubstart",SqlDbType.Int)
                                            };
                                            sqlpara[0].Value = Pubnum + 1;
                                            sqlpara[1].Value = IPScaner.GetUserIP();
                                            sqlpara[2].Value = DataConverter.CLng(PubContentid);
                                            sqlpara[3].Value = userid;
                                            sqlpara[4].Value = username;
                                            sqlpara[5].Value = Pubid;
                                            sqlpara[6].Value = DateTime.Now;
                                            sqlpara[7].Value = 0;
                                            sqlpara[8].Value = 0;



                                            //更新与添加
                                            if (temptable.Rows.Count > 0)
                                            {
                                                sqlpara[8].Value = DataConverter.CLng(temptables.Rows[0]["Pubstart"].ToString());
                                                pubs.UpdateModel(sqlpara, mpub.PubTableName, "id=" + pubitemid + "");

                                                if (mpub.PubIsTrue == 0)
                                                {
                                                    sqlpara[8].Value = 1;
                                                }
                                                else
                                                {
                                                    sqlpara[8].Value = 0;
                                                }

                                                sqlpara[0].Value = 1;
                                                sqlpara[7].Value = Parentid;
                                                pubs.InsertModel(sqlpara, mpub.PubTableName);
                                            }
                                            else
                                            {
                                                if (mpub.PubIsTrue == 0)
                                                {
                                                    sqlpara[8].Value = 1;
                                                }
                                                else
                                                {
                                                    sqlpara[8].Value = 0;
                                                }
                                                pubs.InsertModel(sqlpara, mpub.PubTableName);
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        requestvalue = "";
                    }
                    #endregion

                    //将标签替换成提交框


                    str = str.Replace(Smallmatch.ToString(), requestvalue);
                    //end

                }
            }
            str = str.Replace("{PubCode/}", pubCodeLabel);

            return str;
        }
        /// <summary>
        /// 转换URL编码
        /// </summary>
        public string GetUrlencode(string str)
        {
            string pattern = @"\{\$GetUrlencode\([\s\S]*?\)\$\}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {

                MatchCollection Smallmatchs = Regex.Matches(matchs[i].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string requesttxt = Smallmatch.ToString().Replace(@"{$GetUrlencode(", "").Replace(@")$}", "");
                    string requestvalue = System.Web.HttpUtility.UrlEncode(requesttxt);
                    str = str.Replace(Smallmatch.Value, requestvalue);
                }
            }
            return str;
        }
        /// <summary>
        /// 导出为Excel或Word
        /// </summary>
        public string GetGetExcel(string str)
        {
            string pattern = @"\{\$GetExcel\([\s\S]*?\)\$\}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[i].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string requesttxt = Smallmatch.ToString().Replace(@"{$GetExcel(", "").Replace(@")$}", "");
                    string id = requesttxt.Split(',')[0];
                    string name = requesttxt.Split(',')[1];
                    string st = string.Empty;
                    st += "<script src='/JS/Label/ZLHelper.js'></script>";
                    st += "<input type='button' id='GetExcel' value='导出Excel' onclick=\"ZLHelper.ToExcelByID('" + id + "','" + name + "');\" />";
                    str = str.Replace(Smallmatch.ToString(), st);
                }
            }
            return str;
        }
        public string GetWord(string str)
        {
            string pattern = @"\{\$GetWord\([\s\S]*?\)\$\}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[i].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string requesttxt = Smallmatch.ToString().Replace(@"{$GetWord(", "").Replace(@")$}", "");
                    string id = requesttxt.Split(',')[0];
                    string name = requesttxt.Split(',')[1];
                    string st = string.Empty;
                    st += "<script src='/JS/Label/ZLHelper.js'></script>";
                    st += "<input type='button' id='GetWord' value='导出Word' onclick=\"ZLHelper.ToWordByID('" + id + "','" + name + "');\" />";
                    str = str.Replace(Smallmatch.ToString(), st);
                }
            }
            return str;
        }
        /// <summary>
        /// URL反编码
        /// </summary>
        /// <param name="str">模板Html</param>
        /// <returns></returns>
        public string GetUrldecode(string str)
        {
            string pattern = @"\{\$GetUrldecode\([\s\S]*?\)\$\}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {

                MatchCollection Smallmatchs = Regex.Matches(matchs[i].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string requesttxt = Smallmatch.ToString().Replace(@"{$GetUrldecode(", "").Replace(@")$}", "");
                    string requestvalue = HttpUtility.UrlDecode(requesttxt);// System.Web.CurrentReq.Server.UrlDecode(requesttxt);
                    str = str.Replace(Smallmatch.Value, requestvalue);
                }
            }
            return str;
        }

        #region 自定义页面参数
        /// <summary>
        /// 获取GET提交
        /// </summary>
        /// <param name="html">模板html</param>
        /// <returns></returns>
        public string GetRequest(string html)
        {
            string pattern = @"\{\$GetRequest\([a-z0-9]*\)\$\}";//{$GetRequest(变量名)$}
            string url = rawurl.ToLower();
            string query = url.Contains("?") ? url.Split('?')[1] : "";
            MatchCollection matchs = Regex.Matches(html, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[i].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string requesttxt = (Smallmatch.ToString().Replace(@"{$GetRequest(", "").Replace(@")$}", "") ?? "").ToLower();//变量名
                    string result = "";
                    try
                    {
                        //从query中取值
                        result = StrHelper.GetValFromUrl(url, requesttxt);
                        //如为空,则检测是否为路由页面 /Shop/1.aspx  /Item/1.aspx
                        if (string.IsNullOrEmpty(result) && (requesttxt.Equals("id") || requesttxt.Equals("itemid")))
                        {
                            result = GetIDVal(url);
                        }
                        // /class_1/default.aspx
                        if (string.IsNullOrEmpty(result) && requesttxt.Equals("nodeid"))
                        {
                            result = Regex.Split(url, Regex.Escape("class_"))[1].Split('/')[0];
                        }
                        //ZLLog.L(url + "|" + result + "|" + Smallmatchs[0].Value);
                    }
                    catch (Exception ex) { ZLLog.L(Model.ZLEnum.Log.exception, ex.Message); }
                    result = HttpUtility.HtmlEncode(result);
                    html = html.Replace(Smallmatch.ToString(), result);
                }
            }
            return html;
        }
        private string GetIDVal(string url)
        {
            string[] pages = "/item/,/shop/".Split(',');
            foreach (string page in pages)
            {
                if (url.StartsWith(page))
                {
                    // /Item/10.aspx   /Item/10_2.aspx   
                    url = Regex.Split(url, Regex.Escape(page))[1].Split('.')[0];//10.aspx||10_2.aspx-->10||10_2
                    if (url.Contains("_"))
                    {
                        return url.Split('_')[0];// realurl = "~/Content.aspx?ID=" + url.Split('_')[0] + "&CPage=" + url.Split('_')[1];
                    }
                    else
                    {
                        return url;
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// 获取POST提交
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string PostRequest(string str)
        {
            string pattern = @"\{\$PostRequest\([a-z0-9]*\)\$\}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[i].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string requesttxt = Smallmatch.ToString().Replace(@"{$PostRequest(", "").Replace(@")$}", "");
                    string requestvalue = "";
                    try
                    {
                        //requestvalue = System.Web.CurrentReq.Request.Form[requesttxt];
                        if (CurrentReq.Request.Form[requesttxt] != "")
                        {
                            //System.Web.HttpCookie cookie = new System.Web.HttpCookie["dd"];
                            //cookie.Value = "灌水小鱼";
                            //Response.AppendCookie(cookie);
                            requestvalue = CurrentReq.Request.Form[requesttxt];
                        }
                        else
                        {
                            requestvalue = CurrentReq.Request.Cookies[requesttxt].ToString();
                        }
                    }
                    catch { }
                    requestvalue = HttpUtility.HtmlEncode(requestvalue);
                    str = str.Replace(Smallmatch.ToString(), requestvalue);
                }
            }
            return str;
        }

        #endregion

        #region 获得商铺等级
        /// <summary>
        /// 获得商铺等级:获得等级名称{$GetGradename(id)$}
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetGradename(string str)
        {
            string pattern = @"\{\$GradeName\((\d+\.?\d*|\.\d+)\)\$\}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[i].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string Smallpattern = @"(\d+\.?\d*|\.\d+)";
                    MatchCollection Smallmatchsd = Regex.Matches(Smallmatch.ToString(), Smallpattern, RegexOptions.IgnoreCase);
                    int Storeid = DataConverter.CLng(Smallmatchsd[0]);
                    string OtherNames = GetGrade(Storeid).OtherName;
                    string GradeNames = GetGrade(Storeid).GradeName;
                    if (OtherNames != "")
                    {
                        str = str.Replace(Smallmatch.ToString(), OtherNames);
                    }
                    else
                    {
                        str = str.Replace(Smallmatch.ToString(), GradeNames);
                    }
                }
            }
            return str;
        }
        #endregion

        #region 获得商铺图片
        /// <summary>
        /// 获得商铺图片:获得等级名称{$GetGradeimages(id)$}
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetGradeimages(string str)
        {
            string pattern = @"\{\$Gradeimg\((\d+\.?\d*|\.\d+)\)\$\}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int ic = 0; ic < matchs.Count; ic++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[ic].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string Smallpattern = @"(\d+\.?\d*|\.\d+)";
                    MatchCollection Smallmatchsd = Regex.Matches(Smallmatch.ToString(), Smallpattern, RegexOptions.IgnoreCase);
                    int Storeid = DataConverter.CLng(Smallmatchsd[0]);
                    string gradeimg = GetGrade(Storeid).Gradeimg;
                    int imgnum = GetGrade(Storeid).Imgnum;
                    string storedimg = "";
                    for (int i = 0; i < imgnum; i++)
                    {
                        storedimg = storedimg + "<img src=\"/Images/levelIcon/" + gradeimg + "\" style=\"border-width:0px;\" >";
                    }
                    str = str.Replace(Smallmatch.ToString(), storedimg);
                }
            }

            return str;
        }
        #endregion

        #region 获得买家等级
        /// <summary>
        /// 获得买家等级:获得等级名称{$GetUserGradeName(id)$}
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetUserGradeName(string str)
        {
            string pattern = @"\{\$UserGradeName\((\d+\.?\d*|\.\d+)\)\$\}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[i].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string Smallpattern = @"(\d+\.?\d*|\.\d+)";
                    MatchCollection Smallmatchsd = Regex.Matches(Smallmatch.ToString(), Smallpattern, RegexOptions.IgnoreCase);
                    int Storeid = DataConverter.CLng(Smallmatchsd[0]);
                    string OtherNames = GetUserGrade(Storeid).OtherName;
                    string GradeNames = GetUserGrade(Storeid).GradeName;
                    if (OtherNames != "")
                    {
                        str = str.Replace(Smallmatch.ToString(), OtherNames);
                    }
                    else
                    {
                        str = str.Replace(Smallmatch.ToString(), GradeNames);
                    }
                }
            }
            return str;
        }
        #endregion

        #region 获得买家等级图片
        /// <summary>
        /// 获得买家等级图片:获得买家等级图片{$UserGradeimg(id)$}
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetUserGradeimg(string str)
        {
            string pattern = @"\{\$UserGradeimg\((\d+\.?\d*|\.\d+)\)\$\}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int ib = 0; ib < matchs.Count; ib++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[ib].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string Smallpattern = @"(\d+\.?\d*|\.\d+)";
                    MatchCollection Smallmatchsd = Regex.Matches(Smallmatch.ToString(), Smallpattern, RegexOptions.IgnoreCase);
                    int Storeid = DataConverter.CLng(Smallmatchsd[0]);
                    string storedimg = "";
                    string gradeimg = GetUserGrade(Storeid).Gradeimg;
                    int imgnum = GetUserGrade(Storeid).Imgnum;
                    for (int i = 0; i < imgnum; i++)
                    {
                        storedimg = storedimg + "<img src=\"/Images/levelIcon/" + gradeimg + "\" style=\"border-width:0px;\" >";
                    }
                    str = str.Replace(Smallmatch.ToString(), storedimg);
                }
            }
            return str;
        }
        #endregion

        #region 获得卖家等级名称
        /// <summary>
        /// 获得卖家等级名称:获得卖家等级名称{$StoreGradeName(str)$}
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetStoreGradeName(string str)
        {
            string pattern = @"\{\$StoreGradeName\((\d+\.?\d*|\.\d+)\)\$\}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int i = 0; i < matchs.Count; i++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[i].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string Smallpattern = @"(\d+\.?\d*|\.\d+)";
                    MatchCollection Smallmatchsd = Regex.Matches(Smallmatch.ToString(), Smallpattern, RegexOptions.IgnoreCase);
                    int Storeid = DataConverter.CLng(Smallmatchsd[0]);
                    string OtherNames = GetStoreGrade(Storeid).OtherName;
                    string GradeNames = GetStoreGrade(Storeid).GradeName;
                    if (OtherNames != "")
                    {
                        str = str.Replace(Smallmatch.ToString(), OtherNames);
                    }
                    else
                    {
                        str = str.Replace(Smallmatch.ToString(), GradeNames);
                    }
                }
            }
            return str;
        }
        #endregion

        #region 获得卖家等级图片
        /// <summary>
        /// 获得卖家等级图片:获得卖家等级名称{$StoreGradeimg(str)$}
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetStoreGradeimg(string str)
        {
            string pattern = @"\{\$StoreGradeimg\((\d+\.?\d*|\.\d+)\)\$\}";
            MatchCollection matchs = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);
            for (int ib = 0; ib < matchs.Count; ib++)
            {
                MatchCollection Smallmatchs = Regex.Matches(matchs[ib].ToString(), pattern, RegexOptions.IgnoreCase);
                foreach (Match Smallmatch in Smallmatchs)
                {
                    string Smallpattern = @"(\d+\.?\d*|\.\d+)";
                    MatchCollection Smallmatchsd = Regex.Matches(Smallmatch.ToString(), Smallpattern, RegexOptions.IgnoreCase);
                    int Storeid = DataConverter.CLng(Smallmatchsd[0]);
                    string storedimg = "";
                    string gradeimg = GetStoreGrade(Storeid).Gradeimg;
                    int imgnum = GetStoreGrade(Storeid).Imgnum;
                    for (int i = 0; i < imgnum; i++)
                    {
                        storedimg = storedimg + "<img src=\"/Images/levelIcon/" + gradeimg + "\" style=\"border-width:0px;\" >";
                    }
                    str = str.Replace(Smallmatch.ToString(), storedimg);
                }
            }
            return str;
        }
        #endregion

        #region 获得等级及图片
        /// <summary>
        /// 获得等级及图片
        /// </summary>
        /// <param name="TemplateContent"></param>
        /// <param name="Storeid"></param>
        /// <returns></returns>
        public string GetGradeinfo(string TemplateContent, int Storeid, int Userid)
        {
            if (Storeid > 0)
            {
                //商铺等级名称
                string OtherNames = GetGrade(Storeid).OtherName;

                if (OtherNames != "")
                {
                    TemplateContent = TemplateContent.Replace("{$GradeName$}", OtherNames);
                }
                else
                {
                    string GradeNames = GetGrade(Storeid).GradeName;
                    TemplateContent = TemplateContent.Replace("{$GradeName$}", GradeNames);
                }

                //
                //商铺等级图片
                string storedimg = "";
                string gradeimg = GetGrade(Storeid).Gradeimg;
                int imgnum = GetGrade(Storeid).Imgnum;
                for (int i = 0; i < imgnum; i++)
                {
                    storedimg = storedimg + "<img src=\"/Images/levelIcon/" + gradeimg + "\" style=\"border-width:0px;\" >";
                }
                TemplateContent = TemplateContent.Replace("{$Gradeimg$}", storedimg);
                //
            }

            if (Userid > 0)
            {
                //买家等级名称
                string OtherNames = GetUserGrade(Userid).OtherName;
                string GradeNames = GetUserGrade(Userid).GradeName;
                if (OtherNames != "")
                {
                    TemplateContent = TemplateContent.Replace("{$UserGradeName$}", OtherNames);
                }
                else
                {
                    TemplateContent = TemplateContent.Replace("{$UserGradeName$}", GradeNames);
                }


                //买家等级图片
                string storedimg = "";
                string gradeimg = GetUserGrade(Userid).Gradeimg;
                int imgnum = GetUserGrade(Userid).Imgnum;
                for (int i = 0; i < imgnum; i++)
                {
                    storedimg = storedimg + "<img src=\"/Images/levelIcon/" + gradeimg + "\" style=\"border-width:0px;\" >";
                }
                TemplateContent = TemplateContent.Replace("{$UserGradeimg$}", storedimg);
                //



                //卖家等级名称
                OtherNames = GetStoreGrade(Userid).OtherName;
                GradeNames = GetStoreGrade(Userid).GradeName;
                if (OtherNames != "")
                {
                    TemplateContent = TemplateContent.Replace("{$StoreGradeName$}", OtherNames);
                }
                else
                {
                    TemplateContent = TemplateContent.Replace("{$StoreGradeName$}", GradeNames);
                }


                //卖家等级图片
                storedimg = "";
                gradeimg = GetStoreGrade(Userid).Gradeimg;
                imgnum = GetStoreGrade(Userid).Imgnum;
                for (int i = 0; i < imgnum; i++)
                {
                    storedimg = storedimg + "<img src=\"/Images/levelIcon/" + gradeimg + "\" style=\"border-width:0px;\" >";
                }
                TemplateContent = TemplateContent.Replace("{$StoreGradeimg$}", storedimg);
                //

            }
            return TemplateContent;
        }
        #endregion

        #region 计算商铺等级模块
        /// <summary>
        /// 获得商铺等级等级信息
        /// </summary>
        /// <param name="Sid">商铺ID</param>
        /// <returns></returns>
        public M_ShopGrade GetGrade(int Sid)
        {
            B_Content cll = new B_Content();
            M_CommonData cinfo = cll.GetCommonData(Sid);
            string tablename = cinfo.TableName;
            string ItemID = cinfo.ItemID.ToString();
            B_ModelField mll = new B_ModelField();
            try
            {
                DataTable Iteminfo = mll.SelectTableName(tablename, "id=" + ItemID);
                int StoreCredit = DataConverter.CLng(Iteminfo.Rows[0]["StoreCredit"]);
                if (Iteminfo != null)
                    Iteminfo.Dispose();
                return GetGradestr(StoreCredit);
            }
            catch
            {
                return new M_ShopGrade();
            }
        }
        /// <summary>
        /// 获得用户消费等级等级信息
        /// </summary>
        /// <param name="Userid">用户ID</param>
        /// <returns></returns>
        public M_ShopGrade GetUserGrade(int Userid)
        {
            B_User uinfo = new B_User();
            int ConsumeExp = uinfo.GetUserByUserID(Userid).ConsumeExp;
            return GetUserGradestr(ConsumeExp);
        }
        /// <summary>
        /// 获得卖家等级信息
        /// </summary>
        /// <param name="Userid">用户</param>
        /// <returns></returns>
        public M_ShopGrade GetStoreGrade(int Userid)
        {
            B_User uinfo = new B_User();
            int boffExp = uinfo.GetUserByUserID(Userid).boffExp;
            return GetStoreGradestr(boffExp);
        }
        #endregion

        #region 判断商铺等级模块

        //等级类型 0-购物等级,1-卖家等级,2-商户等级

        /// <summary>
        /// 判断商铺等级
        /// </summary>
        /// <param name="GetGrade"></param>
        /// <returns></returns>
        private M_ShopGrade GetGradestr(int GetGrade)
        {
            B_ShopGrade gll = new B_ShopGrade();
            M_ShopGrade Gradeinfo = gll.GetShopGradebynum(GetGrade, 2);
            return Gradeinfo;
        }
        /// <summary>
        /// 判断用户消费等级
        /// </summary>
        /// <param name="GetUserGrade"></param>
        /// <returns></returns>
        private M_ShopGrade GetUserGradestr(int GetUserGrade)
        {
            B_ShopGrade gll = new B_ShopGrade();
            M_ShopGrade Gradeinfo = gll.GetShopGradebynum(GetUserGrade, 0);
            return Gradeinfo;
        }
        /// <summary>
        /// 判断卖家积分
        /// </summary>
        /// <param name="GetStoreGrade"></param>
        /// <returns></returns>
        private M_ShopGrade GetStoreGradestr(int GetStoreGrade)
        {
            B_ShopGrade gll = new B_ShopGrade();
            M_ShopGrade Gradeinfo = gll.GetShopGradebynum(GetStoreGrade, 1);
            return Gradeinfo;
        }

        #endregion

        #region 获取商品送货方式(proid 商品ID,type=1 列表 type=2 菜单)
        /// <summary>
        /// 获取商品送货方式
        /// </summary>
        /// <param name="proid">商品ID</param>
        /// <param name="type">1-列表,2-菜单</param>
        /// <returns></returns>
        public string GetProDeliver(int proid, int type)
        {
            return "";
        }

        #endregion

        public string Prolistoption(string tempstr)
        {
            return "(Prolistoption)已过期";
        }
        private void WriteLog(string func, string label, string message, string remind = "")
        {
            string tlp = "来源：B_CreateShopHtml,方法名：{0},标签：{1},报错：{2},备注：{3}";
            ZLLog.L(ZLEnum.Log.labelex, string.Format(tlp, func, label, message, remind));
        }
    }
}
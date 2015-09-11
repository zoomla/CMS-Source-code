using System;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using ZoomLa.Model;
using System.Collections;
using ZoomLa.SQLDAL;
using System.Text;
using System.Data.SqlClient;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Globalization;
using System.Web;
namespace ZoomLa.BLL
{
    /// <summary>
    /// 将模板内容转换成HTML代码
    /// </summary>
    public class B_CreateHtml
    {
        protected B_Node bnode = new B_Node();
        protected B_Content bcontent = new B_Content();
        int m_InfoID;
        public B_CreateHtml()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 将模板内容转换成Html代码
        /// </summary>
        /// <param name="TemplateContent"></param>
        /// <returns></returns>
        public string CreateHtml(string TemplateContent, int Cpage, int InfoID)
        {
            string Result = TemplateContent;
            m_InfoID = InfoID;
            //正则表达式查找数据源标签
            string pattern = @"{ZL\.Source([\s\S])*?\/}";
            bool flag = false;
            do
            {
                flag = false;
                MatchCollection matchs = Regex.Matches(Result, pattern, RegexOptions.IgnoreCase);
                foreach (Match match in matchs)
                {
                    Result = ContentSourceLabelProc(match.Value, Cpage, InfoID, Result);
                    flag = true;
                }
            }
            while (flag);
            //正则表达式查找自定义标签
            pattern = @"{ZL\.Label([\s\S])*?\/}";
            flag = false;
            do
            {
                flag = false;
                MatchCollection matchs = Regex.Matches(Result, pattern, RegexOptions.IgnoreCase);
                foreach (Match match in matchs)
                {
                    Result = Result.Replace(match.Value, ContentLabelProc(match.Value, Cpage, InfoID));
                    flag = true;
                }
            }
            while (flag);
            //替换系统标签
            pattern = @"{\$([\s\S])*?\/}";
            do
            {
                flag = false;
                MatchCollection matchs = Regex.Matches(Result, pattern, RegexOptions.IgnoreCase);
                foreach (Match match in matchs)
                {
                    Result = Result.Replace(match.Value, SysLabelProc(match.Value));
                    flag = true;
                }
            }
            while (flag);
            //替换扩展函数
            pattern = @"{ZL:([\s\S])*?\/}";
            do
            {
                flag = false;
                MatchCollection matchs = Regex.Matches(Result, pattern, RegexOptions.IgnoreCase);
                foreach (Match match in matchs)
                {
                    Result = Result.Replace(match.Value, FunLabelProc(match.Value));
                    flag = true;
                }
            }
            while (flag);

            return Result;
        }

        private string ContentSourceLabelProc(string ilabel, int Cpage, int InfoID, string Result)
        {
            M_Label label = new M_Label();
            B_Label bll = new B_Label();
            Hashtable hb = GetParam(ilabel);
            string temp = Result;
            if (!hb.Contains("id"))
            {
                temp.Replace(ilabel, "[err:标签不正确]");
                return temp;
            }
            label = bll.GetLabel(hb["id"].ToString());
            string strSql = "Select ";
            string sqlCount = "";
            string sqlField = "";
            string sqlTable = "";
            string sqlWhere = "";
            string sqlOrder = "";

            if (string.IsNullOrEmpty(label.LabelField))
            {
                temp.Replace(ilabel, "[err:标签没有定义查询的字段数据]");
                return temp;
            }
            else
            {
                sqlField = label.LabelField;
            }
            if (string.IsNullOrEmpty(label.LabelTable))
            {
                temp.Replace(ilabel, "[err:标签没有定义查询的数据表]");
                return temp;
            }
            else
            {
                sqlTable = label.LabelTable;
            }
            if (!string.IsNullOrEmpty(label.LabelCount))
                sqlCount = label.LabelCount;
            if (!string.IsNullOrEmpty(label.LabelWhere))
                sqlWhere = label.LabelWhere;
            if (!string.IsNullOrEmpty(label.LabelOrder))
                sqlOrder = label.LabelOrder;
            //替换参数
            if (!string.IsNullOrEmpty(label.Param))
            {
                string[] pa = label.Param.Split(new char[] { '|' });
                string paname = "";
                for (int i = 0; i < pa.Length; i++)
                {
                    paname = pa[i].Split(new char[] { ',' })[0];
                    if (DataConverter.CLng(pa[i].Split(new char[] { ',' })[2]) == 2)
                    {
                        sqlWhere = sqlWhere.Replace("@" + paname, InfoID.ToString());
                    }
                    else
                    {
                        sqlField = sqlField.Replace("@" + paname, hb["" + paname + ""].ToString());
                        sqlWhere = sqlWhere.Replace("@" + paname, hb["" + paname + ""].ToString());
                        sqlCount = sqlCount.Replace("@" + paname, hb["" + paname + ""].ToString());
                    }
                }
            }
            //组合查询语句
            if (DataConverter.CLng(sqlCount) > 0)
                strSql = strSql + "top " + sqlCount + " ";
            strSql = strSql + sqlField + " ";
            strSql = strSql + "From " + sqlTable + " ";
            if (!string.IsNullOrEmpty(sqlWhere))
                strSql = strSql + "Where " + sqlWhere + " ";
            if (!string.IsNullOrEmpty(sqlOrder))
                strSql = strSql + "Order by " + sqlOrder;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
            temp = temp.Replace(ilabel, "");
            return GetHtmlSource(temp, hb["id"].ToString(), dt);
        }

        private string FunLabelProc(string funlabel)
        {
            //处理扩展函数标签获得换算后的内容
            string lbl = funlabel;
            lbl = lbl.Replace("{ZL:", "").Replace("/}", "");
            string funcname = "";
            string funparam = "";
            funcname = lbl.Substring(0, lbl.IndexOf("("));
            funparam = lbl.Substring(lbl.IndexOf("("));
            funparam = funparam.Replace("(", "").Replace(")", "");
            switch (funcname)
            {
                //根据节点ID获取节点列表或节点首页链接
                case "GetNodeUrl":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "[err:缺少节点ID参数]";
                    }
                    else
                    {
                        if (!DataValidator.IsNumber(funparam))
                        {
                            return "[err:节点ID参数必须是数字]";
                        }
                    }
                    return GetNodePath(DataConverter.CLng(funparam));
                //根据内容ID获取内容页链接
                case "GetInfoUrl":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "[err:缺少内容ID参数]";
                    }
                    else
                    {
                        if (!DataValidator.IsNumber(funparam))
                        {
                            return "[err:内容ID参数必须是数字]";
                        }
                    }
                    return GetInfoPath(DataConverter.CLng(funparam));
                //根据节点ID获取节点链接打开方式
                case "GetNodeOpen":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "[err:缺少节点ID参数]";
                    }
                    else
                    {
                        if (!DataValidator.IsNumber(funparam))
                        {
                            return "[err:节点ID参数必须是数字]";
                        }
                    }
                    return GetNodeOpen(DataConverter.CLng(funparam));
                //根据节点ID获取该节点下内容页链接打开方式
                case "GetInfoOpen":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "[err:缺少节点ID参数]";
                    }
                    else
                    {
                        if (!DataValidator.IsNumber(funparam))
                        {
                            return "[err:节点ID参数必须是数字]";
                        }
                    }
                    return GetInfoOpen(DataConverter.CLng(funparam));

                //获取系统当前时间
                case "TimeNow":
                    return DateTime.Now.ToShortTimeString();
                //当前日期
                case "DateNow":
                    return DateTime.Now.ToShortDateString();
                //当前日期时间
                case "DateAndTime":
                    return DateTime.Now.ToString();

                //将日期时间转换成星期几
                case "ConverToWeek":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "[err:缺少日期时间参数]";
                    }
                    else
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(funparam);
                            return dt.DayOfWeek.ToString();
                        }
                        catch
                        {
                            return "[err:日期时间参数不是正确的日期时间]";
                        }
                    }
                //将日期时间格式化成指定模式
                case "FormatDate":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "[err:缺少日期时间参数]";
                    }
                    else
                    {
                        if (funparam.IndexOf(",") < 0)
                        {
                            return "[err:格式化日期时间缺少参数]";
                        }
                        else
                        {
                            string objdate = funparam.Split(new char[] { ',' })[0];
                            string objfor = funparam.Split(new char[] { ',' })[1];
                            try
                            {
                                DateTime dt = DateTime.Parse(objdate);
                                return dt.ToString(objfor, DateTimeFormatInfo.InvariantInfo);
                            }
                            catch
                            {
                                return "[err:参数错误]";
                            }
                        }
                    }
                //将超过一定长度的字符串截取以'...'结尾的字符串
                case "CutText":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "[err:缺少字符串格式化所需参数]";
                    }
                    else
                    {
                        if (funparam.LastIndexOf(",") < 0)
                        {
                            return "[err:格式化字符串缺少参数]";
                        }
                        else
                        {
                            try
                            {
                                int pos = funparam.LastIndexOf(",");
                                string ex = "";
                                if (pos < funparam.Length)
                                    ex = funparam.Substring(pos + 1);
                                else
                                    ex = "";
                                funparam = funparam.Substring(0, pos);
                                pos = funparam.LastIndexOf(",");
                                string input = funparam.Substring(0, pos);
                                int len = DataConverter.CLng(funparam.Substring(pos + 1));

                                if (DataSecurity.Len(input) <= len)
                                    return input;
                                else
                                {
                                    return DataSecurity.GetSubString(input, len) + ex;
                                }
                            }
                            catch
                            {
                                return "[err:字符串格式化参数错误]";
                            }
                        }
                    }
                case "SplitDown":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "[err:缺少下载地址参数]";
                    }
                    else
                    {
                        string[] downurl = funparam.Split(new char[] { '$' });
                        StringBuilder sb = new StringBuilder();
                        string str = "";
                        string str1 = "";
                        string str2 = "";
                        for (int i = 0; i < downurl.Length; i++)
                        {
                            str1 = downurl[i].Split(new char[] { '|' })[1];
                            str2 = downurl[i].Split(new char[] { '|' })[0];
                            if (str1.StartsWith("http://", true, CultureInfo.CurrentCulture) || str1.StartsWith("/", true, CultureInfo.CurrentCulture))
                                str = str1;
                            else
                                str = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + str1;
                            sb.Append("<li><a href=\"" + str + "\">" + str2 + "</a></li>");
                        }
                        return sb.ToString();
                    }
                case "SplitPicUrl":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "[err:缺少图片地址数组参数]";
                    }
                    else
                    {
                        string[] downurl = funparam.Split(new char[] { '$' });
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < downurl.Length; i++)
                        {
                            sb.Append("<li><a href=\"/Comments/CommentFor.aspx?ID=" + m_InfoID.ToString() + "&p=" + i.ToString() + "\">" + downurl[i].Split(new char[] { '|' })[0] + "</a></li>");
                        }
                        return sb.ToString();
                    }
                case "GetPicUrlCount":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "0";
                    }
                    else
                    {
                        string[] downurl = funparam.Split(new char[] { '$' });

                        return downurl.Length.ToString();
                    }
                case "PreInfoID":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "[err:缺少当前内容ID参数]";
                    }
                    else
                    {
                        int infoid = DataConverter.CLng(funparam);

                        return bcontent.GetPreID(infoid).ToString();
                    }
                case "NextInfoID":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return "[err:缺少当前内容ID参数]";
                    }
                    else
                    {
                        int infoid = DataConverter.CLng(funparam);

                        return bcontent.GetNextID(infoid).ToString();
                    }
                case "GetPicUrl":
                    if (string.IsNullOrEmpty(funparam))
                    {
                        return SiteConfig.SiteOption.UploadDir + "/nopic.gif";
                    }
                    else
                    {
                        if (funparam.StartsWith("http://", true, CultureInfo.CurrentCulture) || funparam.StartsWith("/", true, CultureInfo.CurrentCulture))
                            return funparam;
                        else
                        {
                            return VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.UploadDir) + funparam;
                        }

                    }
            }
            return "[err:不可识别的扩展函数标签]";
        }
        /// <summary>
        /// 根据节点ID获取该节点下内容页链接打开方式
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string GetInfoOpen(int NodeID)
        {
            M_Node nodeinfo = this.bnode.GetNode(NodeID);
            if (nodeinfo.ItemOpenType)
                return "_blank";
            else
                return "_self";
        }
        /// <summary>
        /// 根据节点ID获取节点链接打开方式
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string GetNodeOpen(int NodeID)
        {
            M_Node nodeinfo = this.bnode.GetNode(NodeID);
            if (nodeinfo.OpenNew)
                return "_blank";
            else
                return "_self";
        }
        /// <summary>
        /// 根据内容ID获取内容页链接
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string GetInfoPath(int p)
        {
            M_CommonData cinfo = this.bcontent.GetCommonData(p);
            M_Node nodeinfo = this.bnode.GetNode(cinfo.NodeID);
            if (cinfo.IsCreate == 1 && nodeinfo.ContentFileEx < 3)
                return SiteConfig.SiteInfo.SiteUrl + cinfo.HtmlLink;
            else
                return "/Content.aspx?ItemID=" + p;
        }
        /// <summary>
        /// 根据节点ID获取节点列表或节点首页链接
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        private string GetNodePath(int NodeID)
        {
            M_Node nodeinfo = this.bnode.GetNode(NodeID);
            if (!string.IsNullOrEmpty(nodeinfo.NodeUrl) && nodeinfo.ListPageHtmlEx < 3)
                return SiteConfig.SiteInfo.SiteUrl + nodeinfo.NodeUrl;
            else
                return "/ColumnList.aspx?NodeID=" + NodeID;
        }

        private string SysLabelProc(string syslabel)
        {
            //处理系统标签获得函数后的内容
            string lbl = syslabel;
            lbl = lbl.Replace("{$", "").Replace("/}", "");
            switch (lbl)
            {
                case "SiteName":
                    return SiteConfig.SiteInfo.SiteName;

                case "SiteURL":
                    return SiteConfig.SiteInfo.SiteUrl;

                case "SiteTitle":
                    return SiteConfig.SiteInfo.SiteTitle;

                case "MetaKeywords":
                    return "<meta name=\"Keywords\" content=\"" + SiteConfig.SiteInfo.MetaKeywords + "\">";

                case "MetaDescription":
                    return "<meta name=\"Description\" content=\"" + SiteConfig.SiteInfo.MetaDescription + "\">";

                case "LogoUrl":
                    return SiteConfig.SiteInfo.LogoUrl;

                case "Banner":
                    return SiteConfig.SiteInfo.BannerUrl;

                case "Webmaster":
                    return SiteConfig.SiteInfo.Webmaster;

                case "WebmasterEmail":
                    return SiteConfig.SiteInfo.WebmasterEmail;

                case "Copyright":
                    return SiteConfig.SiteInfo.Copyright;

                case "UploadDir":
                    return SiteConfig.SiteOption.UploadDir + "/";

                case "ManageDir":
                    return SiteConfig.SiteOption.ManageDir + "/";

                case "CssDir":
                    return SiteConfig.SiteOption.CssDir + "/";

                case "AdDir":
                    return SiteConfig.SiteOption.AdvertisementDir + "/";

                default:
                    return "[err:不可识别的系统标签]";

            }
        }
        /// <summary>
        /// 处理标签取得换算后的标签Html的代码
        /// </summary>
        /// <param name="ilabel"></param>
        /// <returns></returns>
        private string ContentLabelProc(string ilabel, int Cpage, int InfoID)
        {
            M_Label label = new M_Label();
            B_Label bll = new B_Label();
            Hashtable hb = GetParam(ilabel);
            if (!hb.Contains("id"))
            {
                return "[err:标签不正确]";
            }
            label = bll.GetLabel(hb["id"].ToString());
            if (label.LableType == 1)
            {
                return label.Content;
            }
            else
            {
                string labelcontent = label.Content;
                string strSql = "Select ";
                string sqlCount = "";
                string sqlField = "";
                string sqlTable = "";
                string sqlWhere = "";
                string sqlOrder = "";
                string sqlIdentity = "";
                sqlCount = label.LabelCount;


                if (string.IsNullOrEmpty(label.LabelField))
                {
                    return "[err:标签没有定义查询的字段数据]";
                }
                else
                {
                    sqlField = label.LabelField;
                }
                if (string.IsNullOrEmpty(label.LabelTable))
                {
                    return "[err:标签没有定义查询的数据表]";
                }
                else
                {
                    sqlTable = label.LabelTable;
                }
                if (!string.IsNullOrEmpty(label.LabelWhere))
                    sqlWhere = label.LabelWhere;
                if (!string.IsNullOrEmpty(label.LabelOrder))
                    sqlOrder = label.LabelOrder;

                if (!string.IsNullOrEmpty(label.Param))
                {
                    string[] pa = label.Param.Split(new char[] { '|' });
                    string paname = "";
                    for (int i = 0; i < pa.Length; i++)
                    {
                        paname = pa[i].Split(new char[] { ',' })[0];
                        if (DataConverter.CLng(pa[i].Split(new char[] { ',' })[2]) == 2)
                        {
                            sqlWhere = sqlWhere.Replace("@" + paname, InfoID.ToString());
                        }
                        else
                        {
                            labelcontent = labelcontent.Replace("@" + paname, hb["" + paname + ""].ToString());
                            sqlField = sqlField.Replace("@" + paname, hb["" + paname + ""].ToString());
                            sqlWhere = sqlWhere.Replace("@" + paname, hb["" + paname + ""].ToString());
                            sqlCount = sqlCount.Replace("@" + paname, hb["" + paname + ""].ToString());
                        }
                    }
                }
                if (label.LableType == 2)
                {
                    if (DataConverter.CLng(sqlCount) > 0)
                        strSql = strSql + "top " + sqlCount + " ";
                    strSql = strSql + sqlField + " ";
                    strSql = strSql + "From " + sqlTable + " ";
                    if (!string.IsNullOrEmpty(sqlWhere))
                        strSql = strSql + "Where " + sqlWhere + " ";
                    if (!string.IsNullOrEmpty(sqlOrder))
                        strSql = strSql + "Order by " + sqlOrder;
                    DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
                    return GetHtmlContent(labelcontent, dt);
                }
                else
                {
                    strSql = "PR_GetRecordFromPage";
                    SqlParameter[] cmdParam = new SqlParameter[] { 
                        new SqlParameter("@TableName",SqlDbType.VarChar),       //表名，可以是多个表 
                        new SqlParameter("@Identity",SqlDbType.VarChar),
                        new SqlParameter("@Fields",SqlDbType.VarChar),          //要取出的字段，可以是多个表的字段，可以为空，为空表示select *
                        new SqlParameter("@sqlWhere",SqlDbType.VarChar),        //条件，可以为空，不用填 where                        
                        new SqlParameter("@OrderField",SqlDbType.VarChar),      //排序字段，可以为空，为空默认按主键升序排列，不用填 order by
                        new SqlParameter("@pageSize",SqlDbType.Int),            //每页记录数
                        new SqlParameter("@pageIndex",SqlDbType.Int)            //当前页，1表示第1页
                    };
                    cmdParam[0].Value = sqlTable;
                    cmdParam[1].Value = sqlIdentity;
                    cmdParam[2].Value = sqlField;
                    cmdParam[3].Value = sqlWhere;
                    cmdParam[4].Value = sqlOrder;
                    cmdParam[5].Value = DataConverter.CLng(sqlCount);
                    cmdParam[6].Value = Cpage - 1;

                    DataSet ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, strSql, cmdParam);
                    DataTable dt = ds.Tables[0];
                    DataTable dt1 = ds.Tables[1];

                    int Count = DataConverter.CLng(dt.Rows[0][0]);
                    labelcontent = labelcontent.Replace("{ZL.Page/}", function.ShowlistPage(Count, DataConverter.CLng(sqlCount), Cpage, true, InfoID));
                    return GetHtmlContent(labelcontent, dt1);
                }
            }
        }
        /// <summary>
        /// 将数据集中的数据替换标签中的字段
        /// </summary>
        /// <param name="content"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetHtmlContent(string content, DataTable dt)
        {
            string result = content;
            StringBuilder sb = new StringBuilder();
            string pattern = @"{Repeate}([\s\S])*?{/Repeate}";
            string temp = "";
            //有循环列表，换算循环列表部分
            if (Regex.IsMatch(result, pattern, RegexOptions.IgnoreCase))
            {
                temp = Regex.Match(result, pattern, RegexOptions.IgnoreCase).Value;
                temp = temp.Replace("{Repeate}", "").Replace("{/Repeate}", "");
                foreach (DataRow dr in dt.Rows)
                {
                    string s = temp;
                    string pattern1 = @"{Field=([\s\S])*?/}";
                    MatchCollection matchs = Regex.Matches(s, pattern1, RegexOptions.IgnoreCase);
                    foreach (Match match in matchs)
                    {
                        s = s.Replace(match.Value, dr[GetFileName(match.Value)].ToString());
                    }
                    sb.Append(s);
                }
                result = result.Replace(temp, sb.ToString()).Replace("{Repeate}", "").Replace("{/Repeate}", "");
            }
            string pattern2 = @"{Field=([\s\S])*?/}";
            MatchCollection matchs1 = Regex.Matches(result, pattern2, RegexOptions.IgnoreCase);
            if (dt.Rows.Count > 0)
            {
                DataRow dr1 = dt.Rows[0];
                foreach (Match match1 in matchs1)
                {
                    result = result.Replace(match1.Value, dr1[GetFileName(match1.Value)].ToString());
                }
            }
            else
            {
                foreach (Match match1 in matchs1)
                {
                    result = result.Replace(match1.Value, "[err:无记录]");
                }
            }
            return result;
        }
        /// <summary>
        /// 将数据源中的字段替换标签中的字段
        /// </summary>
        /// <param name="content"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetHtmlSource(string content, string LabelName, DataTable dt)
        {
            string result = content;
            StringBuilder sb = new StringBuilder();
            string pattern = @"{Repeate}([\s\S])*?{/Repeate}";
            string temp = "";
            //有循环列表，换算循环列表部分
            if (Regex.IsMatch(result, pattern, RegexOptions.IgnoreCase))
            {
                temp = Regex.Match(result, pattern, RegexOptions.IgnoreCase).Value;
                temp = temp.Replace("{Repeate}", "").Replace("{/Repeate}", "");
                foreach (DataRow dr in dt.Rows)
                {
                    string s = temp;
                    string pattern1 = @"{SField([\s\S])*?/}";
                    MatchCollection matchs = Regex.Matches(s, pattern1, RegexOptions.IgnoreCase);
                    foreach (Match match in matchs)
                    {
                        s = s.Replace(match.Value, GetSourceField(match.Value, dr, LabelName));
                    }
                    sb.Append(s);
                }
                result = result.Replace(temp, sb.ToString()).Replace("{Repeate}", "").Replace("{/Repeate}", "");
            }
            string pattern2 = @"{SField([\s\S])*?/}";
            MatchCollection matchs1 = Regex.Matches(result, pattern2, RegexOptions.IgnoreCase);
            if (dt.Rows.Count > 0)
            {
                DataRow dr1 = dt.Rows[0];
                foreach (Match match1 in matchs1)
                {
                    result = result.Replace(match1.Value, GetSourceField(match1.Value, dr1, LabelName));
                }
            }
            else
            {
                foreach (Match match1 in matchs1)
                {
                    result = result.Replace(match1.Value, "[暂无记录]");
                }
            }
            return result;
        }
        private string GetSourceField(string ilabel, DataRow dr, string LabelName)
        {
            string re = ilabel;
            string lbl = ilabel;
            lbl = lbl.Replace("{SField ", "").Replace("/}", "").Replace(" ", ",");
            string[] paArr = lbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string lblname = paArr[0].Split(new char[] { '=' })[1].Replace("\"", "");
            string fname = paArr[1].Split(new char[] { '=' })[1].Replace("\"", "");
            if (lblname == LabelName)
                re = dr[fname].ToString();
            return re;
        }
        private Hashtable GetParam(string ilabel)
        {
            ilabel = ilabel.Replace("{ZL.Label ", "").Replace("{ZL.Source ", "").Replace("/}", "").Replace(" ", ",");

            Hashtable hb = new Hashtable();
            string[] paArr = ilabel.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string key = "";
            string value = "";
            for (int i = 0; i < paArr.Length; i++)
            {
                key = paArr[i].Split(new char[] { '=' })[0];
                value = paArr[i].Split(new char[] { '=' })[1].Replace("\"", "");
                hb.Add(key, value);
            }
            return hb;
        }
        private string GetFileName(string regFiled)
        {
            string result = regFiled.Replace("{Field=", "").Replace("/}", "").Replace("\"", "");
            return result;
        }
    }
}
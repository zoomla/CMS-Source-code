namespace ZoomLa.Common
{
    using System;
    using System.Text;
    using System.Web;

    /// <summary>
    /// function 的摘要说明
    /// </summary>
    public static class function
    {
        public static string ApplicationRootPath = ((HttpContext.Current.Request.ApplicationPath == "/") ? "" : HttpContext.Current.Request.ApplicationPath);

        public static string ShowPage(int totalnumber, int MaxPerPage, int CurrentPage, bool ShowTotal, string strUnit)
        {
            StringBuilder strTemp = new StringBuilder();
            string strRaw = string.Empty;
            string strUrl = string.Empty;
            strRaw = HttpContext.Current.Request.RawUrl;
            string[] strArray = strRaw.Split(new char[] { '/' });
            if (strArray.Length > 0)
            {
                strRaw = strArray[strArray.Length - 1];
            }
            if (strRaw.IndexOf('?') > 0)
            {
                strUrl = strRaw.Substring(0, strRaw.IndexOf('?'));
                string strParam = strRaw.Substring(strRaw.IndexOf('?') + 1);
                string[] strArray1 = strParam.Split(new char[] { '&' });
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < strArray1.Length; k++)
                {
                    //int pos = strArray1[k].IndexOf("=");
                    string pa = strArray1[k].Substring(0, strArray1[k].IndexOf("="));
                    if (string.Compare(pa, "p", true) != 0)
                    {
                        sb.Append(strArray1[k] + "&");
                    }
                }
                strUrl = strUrl + "?" + sb.ToString();
            }
            else
            {
                strUrl = strRaw + "?";
            }
            
            int TotalPage;

            if (totalnumber == 0 || MaxPerPage == 0)
            {
                return "";
            }
            if (totalnumber % MaxPerPage == 0)
                TotalPage = totalnumber / MaxPerPage;
            else
                TotalPage = totalnumber / MaxPerPage + 1;

            if (CurrentPage > TotalPage)
                CurrentPage = TotalPage;
            strTemp.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" class=\"border\" align=\"center\">");
            strTemp.Append("<tr>	<td valign=\"bottom\" align=\"center\" nowrap=\"nowrap\" style=\"width:40%;\">");

            if (ShowTotal)
                strTemp.Append("共 <b>" + totalnumber.ToString() + "</b> " + strUnit + "&nbsp;&nbsp;");
            strTemp.Append("页次：<strong><font color=red>" + CurrentPage.ToString() + "</font>/" + TotalPage.ToString() + "</strong>页&nbsp; ");
            if (CurrentPage == 1)
                strTemp.Append("首页 上一页&nbsp;");
            else
            {
                strTemp.Append("<a href='" + strUrl + "p=1'>首页</a>&nbsp;");
                strTemp.Append("<a href='" + strUrl + "p=" + (CurrentPage - 1) + "'>上一页</a>&nbsp;");
            }
            if (CurrentPage >= TotalPage)
                strTemp.Append("下一页 尾页");
            else
            {
                strTemp.Append("<a href='" + strUrl + "p=" + (CurrentPage + 1) + "'>下一页</a>&nbsp;");
                strTemp.Append("<a href='" + strUrl + "p=" + TotalPage + "'>尾页</a>");
            }
            strTemp.Append("</td>		</tr>	</table>");
            return strTemp.ToString();
        }
        public static string ShowlistPage(int totalnumber, int MaxPerPage, int CurrentPage, bool ShowTotal,int NodeID)
        {
            StringBuilder strTemp = new StringBuilder();
            string strUrl = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("/ColumnList.aspx?NodeID="+NodeID.ToString()+"&");                
            strUrl = sb.ToString();
            

            int TotalPage;

            if (totalnumber == 0 || MaxPerPage == 0)
            {
                return "";
            }
            if (totalnumber % MaxPerPage == 0)
                TotalPage = totalnumber / MaxPerPage;
            else
                TotalPage = totalnumber / MaxPerPage + 1;

            if (CurrentPage > TotalPage)
                CurrentPage = TotalPage;
            strTemp.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" align=\"center\">");
            strTemp.Append("<tr>	<td valign=\"bottom\" align=\"center\" nowrap=\"nowrap\" style=\"width:40%;\">");

            if (ShowTotal)
                strTemp.Append("共 <b>" + totalnumber.ToString() + "</b>项目&nbsp;&nbsp;");
            strTemp.Append("页次：<strong><font color=red>" + CurrentPage.ToString() + "</font>/" + TotalPage.ToString() + "</strong>页&nbsp; ");
            strTemp.Append("每页：<strong><font color=red>" + MaxPerPage.ToString() + "</font>项目&nbsp; ");
            if (CurrentPage == 1)
                strTemp.Append("首页 上一页&nbsp;");
            else
            {
                strTemp.Append("<a href='" + strUrl + "page=1'>首页</a>&nbsp;");
                strTemp.Append("<a href='" + strUrl + "page=" + (CurrentPage - 1) + "'>上一页</a>&nbsp;");
            }
            if (CurrentPage >= TotalPage)
                strTemp.Append("下一页 尾页");
            else
            {
                strTemp.Append("<a href='" + strUrl + "page=" + (CurrentPage + 1) + "'>下一页</a>&nbsp;");
                strTemp.Append("<a href='" + strUrl + "page=" + TotalPage + "'>尾页</a>");
            }
            strTemp.Append("</td>		</tr>	</table>");
            return strTemp.ToString();
        }
        public static string GetFileName()
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            builder.Append(DateTime.Now.ToString("yyyyMMddHHmmss"));
            builder.Append(random.Next(0x186a0, 0xf423f).ToString());
            return builder.ToString();
        }
        /// <summary>
        /// 重定向输出错误信息给用户
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="returnurl">界面返回链接</param>
        public static void WriteErrMsg(string errorMessage, string returnurl)
        {
            HttpContext.Current.Items["ErrorMessage"] = errorMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Manage/Prompt/ShowError.aspx");
        }
        public static void WriteErrMsg(string errorMessage)
        {
            WriteErrMsg(errorMessage, string.Empty);
        }
        public static void WriteSuccessMsg(string successMessage)
        {
            WriteSuccessMsg(successMessage, string.Empty);
        }

        /// <summary>
        /// 重定向输出成功信息
        /// </summary>
        /// <param name="successMessage">成功信息</param>
        /// <param name="returnurl">返回链接</param>
        public static void WriteSuccessMsg(string successMessage, string returnurl)
        {
            HttpContext.Current.Items["SuccessMessage"] = successMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Manage/Prompt/ShowSuccess.aspx");
        }
        /// <summary>
        /// 重定向输出信息
        /// </summary>
        /// <param name="message">输出的信息</param>
        /// <param name="returnurl">返回链接</param>
        /// <param name="messageTitle">信息标题</param>
        public static void WriteMessage(string message, string returnurl, string messageTitle)
        {
            HttpContext.Current.Items["Message"] = message;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Items["MessageTitle"] = messageTitle;
            HttpContext.Current.Server.Transfer("~/Manage/Prompt/ShowMessage.aspx");
        }
        public static string HtmlEncode(object s)
        {
            if ((s != null) && (s.ToString() != ""))
            {
                return HtmlEncode(s.ToString());
            }
            return "";
        }

        public static string HtmlEncode(string input)
        {
            return HttpUtility.HtmlEncode(input);
        }

        public static string Decode(string strHtml)
        {
            strHtml = strHtml.Replace("<br>", "\n");
            strHtml = strHtml.Replace("&gt;", ">");
            strHtml = strHtml.Replace("&lt;", "<");
            strHtml = strHtml.Replace("&nbsp;", " ");
            strHtml = strHtml.Replace("&quot;", "\"");
            return strHtml;
        }
    }
}
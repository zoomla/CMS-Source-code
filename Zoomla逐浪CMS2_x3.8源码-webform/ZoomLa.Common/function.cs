namespace ZoomLa.Common
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.IO;
    using System.Data;
    using System.Xml;
    using System.Text.RegularExpressions;
    using ZoomLa.Components;

    public static class function
    {
        private static string app = "";
        public static string ApplicationRootPath
        {
            get { return app; }
            set { app = value; }
        }
        public static void AccessRulo()
        {}
        /// <summary>
        /// 获得随机数 x uu Oblog
        /// </summary>
        public static string GetRandomString(int length, int type = 1)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            string charPool = "";
            switch (type)
            {
                case 2:
                    charPool = "1234567890";
                    break;
                case 3:
                    charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    break;
                case 4://验证码使用
                    //charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefhjklmnrtuvwxyz234578";//soi1069gpq
                    break;
                default:
                    charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
                    break;
            }
            System.Text.StringBuilder rs = new System.Text.StringBuilder();
            while (length-- > 0)
                rs.Append(charPool[(int)(rnd.NextDouble() * charPool.Length)]);
            return rs.ToString();
        }
        /// <summary>
        /// 产生随机数
        /// </summary>
        public static string GetFileName()
        {
            //不填种子数则默认会用当前时间,在for循环中产生的数值会一模一样,所以此处使用Guid产生的hashcode作为种子
            Random random = new Random(Guid.NewGuid().GetHashCode());
            StringBuilder builder = new StringBuilder();
            builder.Append(DateTime.Now.ToString("yyyyMMddHHmmss"));
            builder.Append(random.Next(0x186a0, 0xf423f).ToString());
            return builder.ToString();
        }
        /// <summary>
        /// 例:yyyyMMddHHmmss
        /// </summary>
        public static string GetFileName(string format)
        {
            //不填种子数则默认会用当前时间,在for循环中产生的数值会一模一样,所以此处使用Guid产生的hashcode作为种子
            Random random = new Random(Guid.NewGuid().GetHashCode());
            StringBuilder builder = new StringBuilder();
            builder.Append(DateTime.Now.ToString(format));
            builder.Append(random.Next(0x186a0, 0xf423f).ToString());
            return builder.ToString();
        }
        /// <summary>
        /// 生成强密码,必须包含数字，字符,字母为大写
        /// </summary>
        public static string GeneratePasswd(int num = 7)
        {
            string[] strArr = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z".Split(new char[] { ',' });
            string[] specArr = @"!,#,%,~".Split(',');
            string[] numArr = "2,3,4,5,6,7,8,9".Split(new char[] { ',' });
            string passwd = "";
            Random r = new Random();
            for (int i = 0; i < num; i++)//只包含一个特殊字符
            {
                if (i < num / 2)
                    passwd += strArr[r.Next(strArr.Length)];
                else if (i == num / 2)
                    passwd += specArr[r.Next(specArr.Length)];
                else passwd += numArr[r.Next(numArr.Length)];
            }
            return passwd;
        }
        //-----------------------------------------------------

        public static void WriteErrMsg(int str)
        {
            WriteErrMsg(str.ToString());
        }
        public static void WriteErrMsg(string errorMessage)
        {
            WriteErrMsg(errorMessage, string.Empty);
        }
        /// <summary>
        /// 重定向输出错误信息给用户
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="returnurl">界面返回链接</param>
        public static void WriteErrMsg(string errorMessage, string returnurl)
        {
            WriteMsg("err", "", errorMessage, returnurl, 0);
        }
        public static void WriteErrMsg(string title, string msg, string url)
        {
            WriteMsg("err", title, msg, url,0);
        }
        public static void WriteMsgTime(string msg, string url, int time = 3000)
        {
            if (time > 1)
            {
                msg += "," + time / 1000 + "秒后自动跳转.";
            }
            url = url.Replace("~", "");
            WriteMsg("ok", "", msg, url, time);
        }
        /// <summary>
        /// 重定向输出成功信息
        /// </summary>
        /// <param name="successMessage">成功信息</param>
        /// <param name="returnurl">返回链接</param>
        public static void WriteSuccessMsg(string msg, string url, int time = 3000)
        {
            WriteMsgTime(msg, url, time);
        }
        public static void WriteSuccessMsg(string successMessage)
        {
            WriteSuccessMsg(successMessage, HttpContext.Current.Request.RawUrl);
        }
        /// <summary>
        /// 重定向输出信息
        /// </summary>
        /// <param name="message">输出的信息</param>
        /// <param name="returnurl">返回链接</param>
        /// <param name="messageTitle">信息标题</param>
        public static void WriteMessage(string message, string returnurl, string messageTitle)
        {
            WriteMsg("", messageTitle, message, returnurl, 0);
        }
        /// <summary>
        /// 跳转于信息页
        /// </summary>
        /// <param name="autoSkipTime">以毫秒为单位,0为不跳转</param>
        private static void WriteMsg(string type, string messageTitle, string message, string returnurl, int autoSkipTime)
        {
            string aspxUrl = "";
            switch (type)
            {
                case "err":
                    aspxUrl = "~/Prompt/ShowError.aspx";
                    break;
                case "ok":
                    aspxUrl = "~/Prompt/ShowSuccess.aspx";
                    break;
                default:
                    aspxUrl = "~/Prompt/ShowMessage.aspx";
                    break;
            }
            HttpContext.Current.Items["Message"] = message;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Items["MessageTitle"] = messageTitle;
            HttpContext.Current.Items["autoSkipTime"] = autoSkipTime;
            HttpContext.Current.Server.Transfer(aspxUrl);
        }
        //-----------------------------------------------------
        public static string HtmlEncode(object s)
        {
            if ((s != null) && (s.ToString() != ""))
            {
                return HtmlEncode(s.ToString());
            }
            return "";
        }
        public static void Alert(string s)
        {
            HttpContext.Current.Response.ContentType = "UTF-8";
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + s + "');history.back();</script>");
        }
        public static void Script(Page page, string s)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), GetRandomString(4), s, true);
        }
        public static string HtmlEncode(string input)
        {
            return HttpUtility.HtmlEncode(input);
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void SetPageNoCache()
        {
            //System.Web.HttpContext.Current.Response.Buffer = true;
            //System.Web.HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            //System.Web.HttpContext.Current.Response.Expires = 0;
            //System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
            //System.Web.HttpContext.Current.Response.AddHeader("Pragma", "No-Cache");
        }
        public static string Decode(string s)
        {
            return HttpUtility.HtmlDecode(s);
        }
        #region 将XML字符串转成DataSet
        /// <summary>
        /// 将XML字符串转成DataSet
        /// </summary>
        /// <param name="xmldocuments"></param>
        /// <returns></returns>
        public static DataSet XmlToTable(string xmldocuments)
        {

            xmldocuments = "<?xml version=\"1.0\" encoding=\"gb2312\"?><xml>" + xmldocuments + "</xml>";
            xmldocuments = xmldocuments.Replace("<ListStart>", "<ListStart><![CDATA[");
            xmldocuments = xmldocuments.Replace("</ListStart>", "]]></ListStart>");
            xmldocuments = xmldocuments.Replace("<ListEnd>", "<ListEnd><![CDATA[");
            xmldocuments = xmldocuments.Replace("</ListEnd>", "]]></ListEnd>");
            xmldocuments = xmldocuments.Replace("<LinkStart>", "<LinkStart><![CDATA[");
            xmldocuments = xmldocuments.Replace("</LinkStart>", "]]></LinkStart>");
            xmldocuments = xmldocuments.Replace("<LinkEnd>", "<LinkEnd><![CDATA[");
            xmldocuments = xmldocuments.Replace("</LinkEnd>", "]]></LinkEnd>");
            //////////////////////////////////////////////
            xmldocuments = xmldocuments.Replace("<PageDivBegin>", "<PageDivBegin><![CDATA[");
            xmldocuments = xmldocuments.Replace("</PageDivBegin>", "]]></PageDivBegin>");
            xmldocuments = xmldocuments.Replace("<PageDivEnd>", "<PageDivEnd><![CDATA[");
            xmldocuments = xmldocuments.Replace("</PageDivEnd>", "]]></PageDivEnd>");
            xmldocuments = xmldocuments.Replace("<PageUrlBegin>", "<PageUrlBegin><![CDATA[");
            xmldocuments = xmldocuments.Replace("</PageUrlBegin>", "]]></PageUrlBegin>");
            xmldocuments = xmldocuments.Replace("<PageUrlEnd>", "<PageUrlEnd><![CDATA[");
            xmldocuments = xmldocuments.Replace("</PageUrlEnd>", "]]></PageUrlEnd>");

            xmldocuments = xmldocuments.Replace("<FieldStart>", "<FieldStart><![CDATA[");
            xmldocuments = xmldocuments.Replace("</FieldStart>", "]]></FieldStart>");
            xmldocuments = xmldocuments.Replace("<FieldEnd>", "<FieldEnd><![CDATA[");
            xmldocuments = xmldocuments.Replace("</FieldEnd>", "]]></FieldEnd>");


            Stream xmlreadStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(xmldocuments));
            DataSet ds = new DataSet();
            ds.ReadXml(xmlreadStream, XmlReadMode.Auto);
            xmldocuments = "";
            return ds;

        }
        #endregion
        /// <summary>
        /// 获取xml中节点的值
        /// </summary>
        /// <param name="nls">XML</param>
        /// <param name="NodeName">节点名</param>
        /// <returns></returns>
        public static string GetXmlNode(XmlNodeList nls, string NodeName)
        {
            string node = "";
            foreach (XmlNode xn1 in nls)//遍历   
            {
                XmlNode xe2 = (XmlNode)xn1;//转换类型  
                if (xe2.Name == NodeName)
                {
                    node = xe2.InnerText;
                }
                if (xe2.ChildNodes.Count > 1)
                {
                    node = GetXmlNode(xe2.ChildNodes, NodeName);
                }
                if (!string.IsNullOrEmpty(node))
                {
                    break;
                }
            }
            return node;
        }
        /// <summary>
        /// 判断是否是数字
        /// </summary>
        public static bool IsNumeric(string Value)
        {
            return (!string.IsNullOrEmpty(Value)&&Regex.IsMatch(Value, @"^\d+$"));
        }
        /// <summary>
        /// 中文转换声母
        /// </summary>
        /// <param name="chineseStr"></param>
        /// <returns></returns>
        public static string GetChineseFirstChar(string chineseStr)
        {
            StringBuilder sb = new StringBuilder();
            int length = chineseStr.Length;
            for (int i = 0; i < length; i++)
            {
                string chineseChar = chineseStr[i].ToString();
                sb.Append(GetpyChar(chineseChar));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 字符串转ASCII
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int StrToASCII(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            return 0;
        }
        /// <summary>
        /// ASCII转字符串
        /// </summary>
        /// <param name="asciiCode"></param>
        /// <returns></returns>
        public static string ASCIIToStr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            return "";
        }
        /// <summary>
        /// 获得拼音
        /// </summary>
        /// <param name="cn"></param>
        /// <returns></returns>
        public static string GetpyChar(string cn)
        {
            #region 处理过程
            byte[] arrCN = Encoding.Default.GetBytes(cn);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return cn;
            }
            else return cn;
            #endregion
        }
        /// <summary>
        /// 获得当前频道ID
        /// </summary>
        /// <returns></returns>
        public static bool isAjax(HttpRequest r)
        {
            if (r == null)
            {
                throw new ArgumentNullException("request");
            }
            return ((r["X-Requested-With"] == "XMLHttpRequest") || ((r.Headers != null) && (r.Headers["X-Requested-With"] == "XMLHttpRequest")));
        }
        /// <summary>
        /// 是否Ajax提交
        /// </summary>
        public static bool isAjax() 
        {
            HttpRequest r = HttpContext.Current.Request;
            return isAjax(r);
        }
        /// <summary>
        /// 物理路径转虚拟路径
        /// </summary>
        public static string PToV(string ppath)
        {
            ppath = ppath.Replace(@"\\", "\\");
            ppath = ppath.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
            ppath = ppath.Replace(@"\", "/");
            return ("/" + ppath).Replace("//","/");//避免有些带/有些不带
        }
        /// <summary>
        /// 虚拟路径转物理路径,用于异步等地方
        /// </summary>
        /// <param name="vpath">虚拟路径(全路径)</param>
        public static string VToP(string vpath)
        {
            if (vpath.Contains(":"))
                return vpath;
            else
                return (AppDomain.CurrentDomain.BaseDirectory + vpath.Replace("/", "\\")).Replace(@"\\", "\\");
        }
        /// <summary>
        /// 获取上传图片的imgurl
        /// </summary>
        /// <param name="o">路径</param>
        public static string GetImgUrl(object o)
        {
            if (o == DBNull.Value || string.IsNullOrEmpty(o.ToString())) return "/Images/nopic.gif";
            string url = o.ToString().Replace(" ", "").ToLower();
            string updir = SiteConfig.SiteOption.UploadDir.ToLower();
            if (url.IndexOf("://") > 0 || url.IndexOf("/design") == 0)//不做处理
            {
 
            }
            else if (url.IndexOf("uploadfiles/") != 0 && url.IndexOf(updir) != 0)
            {
                url = updir + url;
            }
            url = "/" + url.TrimStart('/').Replace("//", "/");
            return url;
        }
        //-----------数组，字符串
        /// <summary>
        /// 移除字符串中的重复元素
        /// </summary>
        public static string RemoveRepeat(string[] a, string[] b)
        {
            string result = "";
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j] == b[i] && a[j] != "")
                        a[j] = "";
                }
            }
            foreach (string s in a)
            {
                if (!string.IsNullOrEmpty(s))
                    result += s + ",";
            }
            return result.TrimEnd(',');
        }
        public static string RemoveRepeat(string a, string b)
        {
            return RemoveRepeat(a.Split(','),b.Split(','));
        }
        //-----------字符串区域
        public static string GetStr(object o, int len, string ellipse = "...")
        {
            if (o == DBNull.Value || string.IsNullOrEmpty(o.ToString()) || len < 1) return "";
            string str = o.ToString();
            return str.Length > len ? str.Substring(0, len) + ellipse : str;
        }
        //-----------日期处理
        //贴吧类型时间
        public static string GetBarDate(object o)
        {
            if (o == DBNull.Value || o == null) return "";
            DateTime date = Convert.ToDateTime(o);
            return ((date - DateTime.Now).Days == 0 && date.Day == DateTime.Now.Day) ? date.ToString("HH:mm") : date.ToString("MM-dd");
        }
        /// <summary>
        /// 将.net时间转换为时间戳(10位)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int DateToUnix(DateTime date)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(date - startTime).TotalSeconds;
        }
        /// <summary>
        /// 将时间戳转换为.net时间
        /// </summary>
        /// <param name="unix"></param>
        /// <returns></returns>
        public static DateTime UnixToDate(string unix)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(unix + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        //------------other
        /// <summary>
        /// 生成搜索索引文件
        /// </summary>
        /// <param name="path">文件目录</param>
        /// <param name="xmlPath">配置文件名</param>
        public static DataTable CreateSiteMap(string path, string xmlPath)
        {
            Regex reg = new Regex(@"(?<=<title>).*(?=</title>)", RegexOptions.IgnoreCase);
            string[] dirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Title", typeof(string)));
            dt.Columns.Add(new DataColumn("Url", typeof(string)));
            //dt.Columns.Add(new DataColumn("PPath", typeof(string)));
            dt.Columns.Add(new DataColumn("Desc", typeof(string)));
            for (int d = 0; d < dirs.Length; d++)
            {
                string[] files = Directory.GetFiles(dirs[d]);
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].EndsWith(".aspx"))
                    {
                        DataRow dr = dt.NewRow();
                        dr["Title"] = DecreateTitle(reg.Match(File.ReadAllText(files[i])).Value);
                        dr["Url"] = function.PToV(files[i]);
                        //dr["PPath"] = files[i];
                        dt.Rows.Add(dr);
                    }
                }
            }
            dt.TableName = "SiteMap";
            dt.WriteXml(xmlPath);
            return dt;
        }
        /// <summary>
        /// 获取页面Title
        /// </summary>
        private static string DecreateTitle(string title)
        {
            Regex reg2 = new Regex("(?<=<%=.*\").*(?=\".*%>)", RegexOptions.IgnoreCase);
            title = title.Replace("<%=Call.SiteName%>", SiteConfig.SiteInfo.SiteName).Replace("<%:Call.SiteName%>", SiteConfig.SiteInfo.SiteName);
            if (title.Contains("lang."))
            {
                title = lang.LF(reg2.Match(title).Value);
            }
            return title;
        }
    }
}
namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Text.RegularExpressions;
    using System.Net;
    using System.Text;
    using System.Collections.Specialized;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    using System.Collections.Generic;
    public class B_CollectionInfo
    {
        private string TbName, PK;
        private M_CollectionInfo initMod = new M_CollectionInfo();
        public B_CollectionInfo()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_CollectionInfo SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_CollectionInfo SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, PK + " DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_CollectionInfo model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.C_IID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "SELECT * FROM " + TbName + " WHERE C_IID IN ("+ids+")";
            return SqlHelper.ExecuteSql(sql);
        }
        public int insert(M_CollectionInfo model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_CollectionInfo model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool GetUpdate(M_CollectionInfo model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.C_IID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool InsertUpdate(M_CollectionInfo model)
        {
            if (model.C_IID > 0)
                GetUpdate(model);
            else
                GetInsert(model);
            return true;
        }
        public M_CollectionInfo GetSelect(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable Select_All()
        {
            return Sql.Sel(TbName);
        }
        /// <summary>
        /// 提取规则内的内容
        /// </summary>
        /// <returns></returns>
        public string checkList(string stext, string etext,string htmlstr)
        {
            string strhtml = htmlstr;
            string strRef = stext + "([\\s\\S])*?" + etext;
            MatchCollection matches = new Regex(strRef).Matches(strhtml);
            Match TitleMatch = Regex.Match(strhtml, strRef, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string str = "";
            foreach (Match m in matches)
            {
                str += m.Value.Replace(stext, "").Replace(etext, "") + "\n";
            }
            return str;
        }
        //执行采集
        public void ExeColl()
        {
            string strhtml = "";
            B_CollectionItem bc = new B_CollectionItem();
            DataTable dtUrl = new DataTable();
            dtUrl.Columns.Add(new DataColumn("url", System.Type.GetType("System.String")));
            //查询所有开始执行采集的项目
            DataTable dt = bc.SelBySwitch(1);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ListSettings"] != null && dr["CollUrl"] != null) //判断是否有规则，满足条件
                {
                    strhtml = GetHtml(dr["CollUrl"].ToString());//获得HTML

                    StringCollection tempurl = GetPageUrl(strhtml);
                    for (int i = 0; i < tempurl.Count; i++)
                    {
                        System.Web.HttpContext.Current.Response.Write(tempurl[i]);
                        System.Web.HttpContext.Current.Response.Write("<br />");
                    }
                    #region bak
                    //DataTable dd = new DataTable();
                    //dd.Columns.Add(new DataColumn("url", System.Type.GetType("System.String")));
                    //DataRow drs = dd.NewRow();
                    //drs["url"] = dr["CollUrl"];
                    //dd.Rows.Add(drs);

                    //string[] urlarr = GetPage(SetUrl(dr["PageSettings"].ToString(), strhtml)).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    //foreach (string a in urlarr)
                    //{
                    //    DataRow dra = dd.NewRow();
                    //    dra["url"] = a;
                    //    dd.Rows.Add(dra);
                    //}

                    ////提取地址列表
                    //foreach (DataRow drUrl in dtUrl.Rows)
                    //{
                    //    //提取地址列表信息
                    //    string html = GetHtml(drUrl["url"].ToString());

                    //}
                    #endregion
                }
                //结束采集
                //M_CollectionItem mc = bc.GetSelect(DataConverter.CLng(dr["CItem_ID"]));
                //mc.Switch = 0;
                //bc.GetUpdate(mc);
            }
        }


        /// <summary>
        /// 获取所有的链接地址
        /// </summary>
        /// <param name="Pagestr">页面代码</param>
        /// <returns>返回的列表</returns>
        public StringCollection GetPageUrl(string Pagestr)
        {
            StringCollection resultList = new StringCollection();
            try
            {
                Regex regexObj = new Regex("<a[^>]*?href=\"(?<url>[^\"]*)\"[^>]*>(?<title>.*?)</a>");
                Match matchResult = regexObj.Match(Pagestr);
                while (matchResult.Success)
                {
                    resultList.Add(matchResult.Value);
                    matchResult = matchResult.NextMatch();
                }
            }
            catch (ArgumentException)
            {
                // Syntax error in the regular expression
            }
            return resultList;
        }

        #region 获取分页代码
        private string GetPage(string PageSettings, string strhtml)
        {
            string strurl = "";
            DataSet ds = function.XmlToTable(PageSettings);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    switch (dr["PageType"].ToString())
                    {
                        case "1":
                            strurl = "";
                            break;
                        case "2":
                            strurl = orderUrl(checkList(dr["PageNextBegin"].ToString().Replace("&lt;", "<").Replace("&gt;", ">"), dr["PageNextEnd"].ToString().Replace("&lt;", "<").Replace("&gt;", ">"), strhtml));
                            break;
                        case "3":
                            for (int i = DataConverter.CLng(dr["PageBeginNum"].ToString()); i <= DataConverter.CLng(dr["PageEndNum"].ToString()); i++)
                            {
                                strurl += dr["PageUrl"].ToString().Replace("{$ID}", i.ToString()) + "\n";
                            }
                            break;
                        case "4":
                            strurl = dr["PageInfo"].ToString().Replace("&lt;", "<").Replace("&gt;", ">");
                            break;
                        case "5":
                            strurl = checkList(dr["PageDivBegin"].ToString().Replace("&lt;", "<").Replace("&gt;", ">"), dr["PageDivEnd"].ToString().Replace("&lt;", "<").Replace("&gt;", ">"), strhtml);
                            strurl = checkList(dr["PageUrlBegin"].ToString().Replace("&lt;", "<").Replace("&gt;", ">"), dr["PageUrlEnd"].ToString().Replace("&lt;", "<").Replace("&gt;", ">"), strurl);
                            strurl = orderUrl(strurl);
                            break;
                    }
                }
            }
            return strurl;
        }
        #endregion

        public string SetUrl(string Url, string str)
        {
            string strurl = "";
            //切割字符串地址
            string[] UrlArr = str.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            //循环地址
            foreach (string ustr in UrlArr)
            {
                if (ustr.IndexOf("http://") < 0)
                {
                    //切割地址
                    string[] urlinfo = ustr.Split(new char[] { '/' });
                    int i = 0;
                    //循环切割后的地址字符
                    foreach (string s in urlinfo)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            i++;
                        }
                    }
                    if (i > 1)
                    {
                        i--;
                    }
                    //切割当前页面地址
                    string[] infoarr = Url.Split(new char[] { '/' });
                    for (int j = 0; j < infoarr.Length - i; j++)
                    {
                        strurl += infoarr[j].ToString() + "/";
                    }
                    strurl += urlinfo[urlinfo.Length - 1] + "\n";
                }
                else
                {
                    strurl += ustr + "\n";
                }

            }
            return strurl;
        }

        private string orderUrl(string strurl)
        {
            string[] str = strurl.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] str2 = new string[str.Length];
            strurl = "";
            for (int i = 0; i < str.Length; i++)
            {
                bool b = true;
                for (int j = 0; j < str2.Length; j++)
                {

                    if (str[i] == str2[j])
                    {
                        b = false;
                    }
                }
                if (b)
                {
                    str2[i] = str[i];
                    strurl += str[i] + "\n";
                }
            }
            return strurl;
        }

        //private void GetUrl(string listset,string collurl,DataTable dt)
        //{
        //    string liststart = "";
        //    string listend = "";
        //    string strhtml = "";
        //    DataSet dslist = function.XmlToTable(listset);
        //    if (dslist.Tables.Count > 0)
        //    {
        //        //替换特殊字符
        //        foreach (DataRow listdr in dslist.Tables[0].Rows)
        //        {
        //            if (!string.IsNullOrEmpty(listdr["ListStart"].ToString()))
        //                liststart = listdr["ListStart"].ToString().Replace("&lt;", "<").Replace("&gt;", ">");
        //            if (!string.IsNullOrEmpty(listdr["ListEnd"].ToString()))
        //                listend = listdr["ListEnd"].ToString().Replace("&lt;", "<").Replace("&gt;", ">");
        //            if (!string.IsNullOrEmpty(listdr["LinkStart"].ToString()))
        //                linkstart = listdr["LinkStart"].ToString().Replace("&lt;", "<").Replace("&gt;", ">");
        //            if (!string.IsNullOrEmpty(listdr["LinkEnd"].ToString()))
        //                linkend = listdr["LinkEnd"].ToString().Replace("&lt;", "<").Replace("&gt;", ">");
        //        }
        //    }
        //    //通过地址获得列表页面的源代码
        //    strhtml = GetHtml(collurl);
        //    //获得列表
        //    if (!string.IsNullOrEmpty(liststart) && !string.IsNullOrEmpty(listend))
        //    {
        //        strhtml = checkList(liststart, listend, strhtml);
        //    }
        //    //获得地址
        //    if (!string.IsNullOrEmpty(linkstart) && !string.IsNullOrEmpty(linkend))
        //    {
        //        strhtml = checkList(linkstart, linkend, strhtml);
        //    }
        //    //将地址提取
        //    if (!string.IsNullOrEmpty(linkstart) && !string.IsNullOrEmpty(linkend))
        //    {
        //        string[] strarr = strhtml.Split(new string[] { "\n" }, StringSplitOptions.None);


        //        foreach (string s in strarr)
        //        {
        //            if (!string.IsNullOrEmpty(s))
        //            {
        //                DataRow drs = dt.NewRow();
        //                drs["url"] = s.Replace(linkstart, " ").Replace(linkend, " ");
        //                dts.Rows.Add(drs);
        //            }
        //        }
        //    }
        //}
        public string GetHtml(string Url)
        {
            WebClient wb = new WebClient();    //创建一个webclient实例 

            //获取或设置用于对向   internet   资源的请求进行身份验证的网络凭据。（可有可无） 
            //wb.credentials=credentialcache.defaultcredentials;  

            //从资源下载数据并返回字节数组。（加@是因为网址中间有"/"符号） 
            byte[] pagedata = wb.DownloadData(@Url);

            //转换字符、 
            string result = Encoding.Default.GetString(pagedata);
            // string result = Encoding.ASCII.GetString(pagedata);
            return result;
        }


        //获取设置规则
        //private void SetField(string strxml,string IName)
        //{
        //    //将XML设置成DataSet
        //    DataSet ds = function.XmlToTable(strxml);
        //    if (ds.Tables.Count > 0)
        //    {
        //        //获得表
        //        foreach (DataTable dt in ds.Tables)
        //        {
        //            //是否是当前字段设置的XML节点
        //            if (dt.TableName == IName)
        //            {
        //                //是否是使用规则
        //                if (dt.Columns[0].ColumnName == IName + "_Id")
        //                {
        //                    foreach (DataTable dtx in ds.Tables)
        //                    {
        //                        if (dtx.TableName == IName + "_CollConfig")
        //                        {
        //                            foreach (DataRow dr in dtx.Rows)
        //                            {
        //                                txtListStart.Text = dr["FieldStart"].ToString().Replace("&lt;", "<").Replace("&gt;", ">");
        //                                txtListEnd.Text = dr["FieldEnd"].ToString().Replace("&lt;", "<").Replace("&gt;", ">");
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}

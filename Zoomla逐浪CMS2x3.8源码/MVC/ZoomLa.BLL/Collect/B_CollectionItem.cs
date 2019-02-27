namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text;
    using MSXML2;
    using System.Data.SqlClient;
    using ZoomLa.Components;
    using SQLDAL.SQL;
    using System.Collections.Generic;
    using System.Web;
    public class B_CollectionItem
    {
        public B_CollectionItem()
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        private string PK, strTableName;
        private M_CollectionItem initMod = new M_CollectionItem();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_CollectionItem SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return new M_CollectionItem().GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_CollectionItem SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return new M_CollectionItem().GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, PK + " DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_CollectionItem model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.CItem_ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, "CItem_ID=" + ID);
        }
        public int insert(M_CollectionItem model)
        {
            return Sql.insertID(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_CollectionItem model)
        {
            return insert(model);
        }
        public bool GetUpdate(M_CollectionItem model)
        {
            return UpdateByID(model);
        }
        public bool InsertUpdate(M_CollectionItem model)
        {
            if (model.CItem_ID > 0)
                UpdateByID(model);
            else
                insert(model);
            return true;
        }
        public bool DelByIds(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return Sql.Del(strTableName, "CItem_ID in (" + ids + ")");
        }
        public M_CollectionItem GetSelect(int ID)
        {
            return SelReturnModel(ID);
        }
        public DataTable SelBySwitch(int status)
        {
            string sql = "Select * From " + strTableName + " Where Switch=" + status + " Order BY AddTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }

        public DataTable Select_All()
        {
            return Sel();
        }

        public string ShowStyleField(string Alias, string Name, string Type, string Content, string Description, int ModelID, DataRow dr)
        {
            CollectionField cf = new CollectionField();
            return cf.ShowStyleField(Alias, Name, Type, Content, Description, ModelID, dr);
        }

        private string Fromatstr(string str)
        {
            str = Regex.Replace(str, @"[\r\n]|[ \t]*", "");
            return BaseClass.Htmlcode(str);
        }

        private string checkList(string stext, string etext, string htmlstr)
        {
            string strhtml = "";
            string strRef = "";

            stext = System.Web.HttpContext.Current.Server.HtmlEncode(stext);//起始条件
            etext = System.Web.HttpContext.Current.Server.HtmlEncode(etext);//结束条件
            htmlstr = System.Web.HttpContext.Current.Server.HtmlEncode(htmlstr);//页面HTML
            strRef = stext + "[\\s\\S]*?" + etext;
            if (htmlstr != "")
            {
                MatchCollection matches = new Regex(BaseClass.Htmldecode(strRef)).Matches(BaseClass.Htmldecode(htmlstr));
                Match TitleMatch = Regex.Match(strhtml, strRef, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string str = "";

                foreach (Match m in matches)
                {
                    str += m.Value.Replace(BaseClass.Htmldecode(stext), "").Replace(BaseClass.Htmldecode(etext), "") + "\n\r";
                }
                return str;
            }
            else
            {
                return "";
            }
        }



        //执行采集
        public int ExeColl(bool readto)
        {
            int scnum = 0;
            B_CollectionItem bc = new B_CollectionItem();
            DataTable dtUrl = new DataTable();
            dtUrl.Columns.Add(new DataColumn("url", System.Type.GetType("System.String")));
            //查询所有开始执行采集的项目
            DataTable dt = bc.SelBySwitch(1);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["LinkList"] != null) //判断是否有规则，满足条件
                {
                    scnum = dr["LinkList"].ToString().Split(',').Length;
                }
                //结束采集
                if (readto)
                {
                    M_CollectionItem mc = bc.GetSelect(DataConverter.CLng(dr["CItem_ID"]));
                    //停止采集
                    mc.Switch = 2;
                    bc.GetUpdate(mc);
                }
            }
            return scnum;
        }


        /// <summary>
        /// 写入字段
        /// </summary>
        /// <param name="info"></param>
        /// <param name="strhtml"></param>
        /// <param name="dt"></param>
        /// <param name="tablefiled"></param>
        private string Insertinfo(string info, string strhtml, string tablefiledname)
        {
            string s = "";
            //获取列表规则
            if (!string.IsNullOrEmpty(info))
            {
                DataSet ds = function.XmlToTable(info);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataTable dt in ds.Tables)//规则里的数据
                    {
                        if (dt.TableName == tablefiledname)
                        {
                            //是否是使用默认值
                            if (dt.Columns[0].ColumnName == tablefiledname + "_Default")
                            {

                            }
                            //是否是指定值
                            if (dt.Columns[0].ColumnName == tablefiledname + "_Appoint")
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    s = dr[tablefiledname + "_Appoint"].ToString();
                                }
                            }
                            //是否是使用规则
                            if (dt.Columns[0].ColumnName == tablefiledname + "_Id")
                            {
                                s = SetField(info, tablefiledname, strhtml);
                            }
                            /*结束*/
                        }
                    }
                }
            }
            return s;
        }


        private string SetUrl(string Url, string str)
        {
            string strurl = "";
            //切割字符串地址
            string[] UrlArr = str.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (UrlArr.Length > 0)
            {
                //循环地址
                foreach (string ustr in UrlArr)
                {
                    strurl += GetStr(Url, ustr);
                }
            }
            else
            {
                strurl = GetStr(Url, str);
            }

            return strurl;
        }

        private static string GetStr(string Url, string ustr)
        {
            string strurl = "";
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
                strurl += urlinfo[urlinfo.Length - 1] + "\n\r";

            }
            else
            {
                strurl += ustr + "\n\r";
            }
            return strurl;
        }
        /// <summary>
        /// 判断URL列表里的URL是否重复。如果重复就选择第一个
        /// </summary>
        /// <param name="strurl">url列表</param>
        /// <returns></returns>
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
                    strurl += str[i] + "\n\r";
                }
            }
            return strurl;
        }


        public string GetRemoteHtmlCode(string Url, Encoding code)
        {
            //EC.GetRemoteObj ecd = new EC.GetRemoteObj();
            //return ecd.Url(Url,code);
            return "";
        }


        //获取设置规则
        private string SetField(string config, string IName, string htmlstr)
        {
            string sf = "";
            //将XML设置成DataSet
            if (!string.IsNullOrEmpty(config))
            {
                DataSet ds = function.XmlToTable(config);
                if (ds.Tables.Count > 0)
                {
                    //获得表
                    foreach (DataTable dt in ds.Tables)
                    {
                        //是否是当前字段设置的XML节点
                        if (dt.TableName == IName)
                        {
                            //是否是使用规则
                            if (dt.Columns[0].ColumnName == IName + "_Id")
                            {
                                foreach (DataTable dtx in ds.Tables)
                                {
                                    if (dtx.TableName == IName + "_CollConfig")
                                    {
                                        foreach (DataRow dr in dtx.Rows)
                                        {
                                            string filestr = dr["FieldStart"].ToString();
                                            string filend = dr["FieldEnd"].ToString();
                                            sf = checkList(filestr.Replace("&lt;", "<").Replace("&gt;", ">"), filend.Replace("&lt;", "<").Replace("&gt;", ">"), htmlstr);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return sf;
            //}
        }
    }
    /// <summary>
    /// 前台用户 添加内容用的显示模型字段输入
    /// </summary>
    public class B_ContentField
    {
        private B_ShowField showBll = new B_ShowField();
        //日期时间
        public string GetDateType(string Alias, string Name, bool IsNotNull, string content, string Description, DataRow dr)
        {
            string str2 = "";
            StringBuilder builder = new StringBuilder();
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            if (dr == null)//为空,添加状态,不为空,修改状态
            {
                builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\"><span>" + Alias + "</span>  </td>\r\n<td>");
                if (content.Equals("1"))
                    builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"18\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });\" value=\"" + DateTime.Now + "\"> " + str2 + " " + Description + "");
                else
                    builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"18\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });\" value=\"\"> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\"><span>" + Alias + "</span>  </td>\r\n<td>");
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"18\" onclick=\"WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });\" value=\"" + dr["" + Name + ""].ToString() + "\"> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //文件
        public string GetFileType(string Alias, string Name, bool IsNotNull, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {
            string str2 = "";
            string[] strArray = Content.Split(new char[] { ',' });
            bool ChkFileSize = DataConverter.CBool(strArray[0].Split(new char[] { '=' })[1]);
            string FileSizeField = strArray[1].Split(new char[] { '=' })[1];
            StringBuilder builder = new StringBuilder();
            string str = "";
            if (ChkFileSize && FileSizeField != "")
            {
                str = "<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">文件大小</td>\r\n<td>";
                if (dr == null)
                {
                    str = str + "<input  id=\"txt_" + FileSizeField + "\" name=\"txt_" + FileSizeField + "\" type=\"text\" class=\"inputtext\" />K</td>\r\n</tr>";
                }
                else
                {
                    str = str + "<input id=\"txt_" + FileSizeField + "\" name=\"txt_" + FileSizeField + "\" type=\"text\" class=\"inputtext\" value=\"" + dr["" + FileSizeField + ""].ToString() + "\" />K</td>\r\n</tr>";
                }
            }
            str = str + "<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">上传文件</td>\r\n<td>";
            str = str + "<iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            builder.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n");
            builder.Append("    <tr>\r\n");
            builder.Append("<td style=\"width: 400px\">\r\n");
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                {
                    builder.Append("<input  id=\"txt_" + Name + "\" type=\"hidden\" name=\"txt_" + Name + "\" value=\"" + dr["" + Name + ""].ToString() + "\" />");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" />");
                }
            }
            else
            {
                builder.Append("<input type=\"hidden\"  id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" />");
            }
            builder.Append("<select  id=\"sel_" + Name + "\" name=\"sel_" + Name + "\" style=\"width: 400px; height: 100px\" size=\"2\">");
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                {
                    string[] strValue = dr["" + Name + ""].ToString().Split(new char[] { '$' });
                    for (int i = 0; i < strValue.Length; i++)
                    {
                        builder.Append("<option value=\"" + strValue[i] + "\">" + strValue[i] + "</option>");
                    }
                }
            }
            builder.Append("</select> " + str2 + "</td>\r\n<td>");
            builder.Append("<input type=\"button\" class=\"button\" onclick=\"AddSoftUrl('sel_" + Name + "','txt_" + Name + "')\" value=\"添加外部地址\" /><br/>");
            builder.Append("<input type=\"button\" class=\"button\" value=\"修改当前地址\" onclick=\"return ModifyPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" /><br />");
            builder.Append("<input type=\"button\" class=\"button\" value=\"删除当前地址\" onclick=\"DelPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" />");
            builder.Append("</td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>");
            builder.Append(str);
            return builder.ToString();
        }
        //多行文本支持Html Coffee
        public string GetMultipleHtmlType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            string str = "";
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string[] strArray4 = strArray[2].Split(new char[] { '=' });
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            string str3 = strArray4[1];
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "1"))
            {
                if (str3 != "2")
                {
                    if (str3 != "3")
                    {
                        return str;
                    }
                    if (dr == null)
                    {
                        if (SiteConfig.SiteOption.EditVer == "2")
                        {
                            builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                            builder.AppendLine(@"<script type=""text/javascript"">");
                            builder.AppendLine(@"KE.show({");
                            builder.AppendLine("id : 'txt_" + Name + "'");
                            builder.AppendLine(@"});");
                            builder.AppendLine(@"</script>");
                        }
                        else if (SiteConfig.SiteOption.EditVer == "3")
                        {//百度编辑器
                            builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;\"  name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                            builder.AppendLine(@"<script type=""text/javascript"">");
                            builder.AppendLine(@"setTimeout(function(){UE.getEditor('txt_" + Name + "');},500);");
                            builder.AppendLine(@"</script>");

                        }
                        else if (SiteConfig.SiteOption.EditVer == "1")
                        {
                            builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                            builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                            builder.AppendLine("//<![CDATA[");
                            builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                            builder.AppendLine("{");
                            //builder.AppendLine("    skin: 'v2',");
                            builder.AppendLine("    ");
                            builder.AppendLine("});");
                            builder.AppendLine("//]]>");
                            builder.AppendLine("</script>");
                        }
                        else
                        {
                            builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_3.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                            //builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_3.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                        }
                        builder.Append("</td>\r\n</tr>\r\n");
                        return builder.ToString();
                    }
                    if (SiteConfig.SiteOption.EditVer == "2")
                    {
                        builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                        builder.AppendLine(@"<script type=""text/javascript"">");
                        builder.AppendLine(@"KE.show({");
                        builder.AppendLine("id : 'txt_" + Name + "'");
                        builder.AppendLine(@"});");
                        builder.AppendLine(@"</script>");
                    }
                    else if (SiteConfig.SiteOption.EditVer == "3")
                    {//百度编辑器
                        builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\"  style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;\"  name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                        builder.AppendLine(@"<script type=""text/javascript"">");
                        builder.AppendLine(@"setTimeout(function(){UE.getEditor('txt_" + Name + "');},500);");
                        builder.AppendLine(@"</script>");

                    }
                    else if (SiteConfig.SiteOption.EditVer == "1")
                    {
                        builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                        builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                        builder.AppendLine("//<![CDATA[");
                        builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                        builder.AppendLine("{");
                        //builder.AppendLine("    skin: 'v2',");
                        builder.AppendLine("    ");
                        builder.AppendLine("});");
                        builder.AppendLine("//]]>");
                        builder.AppendLine("</script>");
                    }
                    else
                    {
                        builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_3.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                    }
                    builder.Append("</td>\r\n</tr>\r\n");
                    return builder.ToString();
                }
            }
            else
            {
                if (dr == null)
                {
                    if (SiteConfig.SiteOption.EditVer == "2")
                    {
                        builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                        builder.AppendLine(@"<script type=""text/javascript"">");
                        builder.AppendLine(@"KE.show({");
                        builder.AppendLine("id : 'txt_" + Name + "'");
                        builder.AppendLine(@"});");
                        builder.AppendLine(@"</script>");
                    }
                    else if (SiteConfig.SiteOption.EditVer == "3")
                    {
                        builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\"  style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;\"  name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                        builder.AppendLine(@"<script type=""text/javascript"">");
                        builder.AppendLine(@"setTimeout(function(){UE.getEditor('txt_" + Name + "', {" + BLLCommon.ueditorMin + "});},500);");
                        builder.AppendLine(@"</script>");

                    }
                    else if (SiteConfig.SiteOption.EditVer == "1")
                    {
                        builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                        builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                        builder.AppendLine("//<![CDATA[");
                        builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                        builder.AppendLine("{");
                        //builder.AppendLine("    skin: 'v2',");
                        builder.AppendLine("    toolbar : [['Source', '-', 'Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', 'Smiley', 'ShowBlocks', 'Maximize', 'About']]");
                        builder.AppendLine("});");
                        builder.AppendLine("//]]>");
                        builder.AppendLine("</script>");
                    }
                    else
                    {
                        builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                        //builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                    }
                    builder.Append("</td>\r\n</tr>\r\n");
                    return builder.ToString();
                }
                if (SiteConfig.SiteOption.EditVer == "2")
                {
                    builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                    builder.AppendLine(@"<script type=""text/javascript"">");
                    builder.AppendLine(@"KE.show({");
                    builder.AppendLine("id : 'txt_" + Name + "'");
                    builder.AppendLine(@"});");
                    builder.AppendLine(@"</script>");
                }
                else if (SiteConfig.SiteOption.EditVer == "3")
                {
                    builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\"  style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;\"  name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                    builder.AppendLine(@"<script type=""text/javascript"">");
                    builder.AppendLine(@"setTimeout(function(){UE.getEditor('txt_" + Name + "', {" + BLLCommon.ueditorMin + "});},500);");
                    builder.AppendLine(@"</script>");

                }
                else if (SiteConfig.SiteOption.EditVer == "1")
                {
                    builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                    builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                    builder.AppendLine("//<![CDATA[");
                    builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                    builder.AppendLine("{");
                    // builder.AppendLine("    skin: 'v2',");
                    builder.AppendLine("    toolbar : [['Source', '-', 'Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', 'Smiley', 'ShowBlocks', 'Maximize', 'About']]");
                    builder.AppendLine("});");
                    builder.AppendLine("//]]>");
                    builder.AppendLine("</script>");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                    //builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                }
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            if (dr == null)
            {
                if (SiteConfig.SiteOption.EditVer == "2")
                {
                    builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                    builder.AppendLine(@"<script type=""text/javascript"">");
                    builder.AppendLine(@"KE.show({");
                    builder.AppendLine("id : 'txt_" + Name + "'");
                    builder.AppendLine(@"});");
                    builder.AppendLine(@"</script>");
                }
                else if (SiteConfig.SiteOption.EditVer == "3")
                {
                    builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\"  style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;\"  name=\"txt_" + Name + "\" rows=\"10\"></textarea>");

                    builder.AppendLine(@"<script type=""text/javascript"">");
                    builder.AppendLine(@"setTimeout(function(){UE.getEditor('txt_" + Name + "', {" + BLLCommon.ueditorMid + "});},500);");
                    builder.AppendLine(@"</script>");

                }
                else if (SiteConfig.SiteOption.EditVer == "1")
                {
                    builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                    builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                    builder.AppendLine("//<![CDATA[");
                    builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                    builder.AppendLine("{");
                    //builder.AppendLine("    skin: 'v2',");
                    builder.AppendLine("    toolbar : [['Source', '-', 'Bold', 'Italic', 'Underline', 'Strike'], ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'], ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'], ['Link', 'Unlink', 'Anchor'], ['Image', 'Flash', 'SpecialChar'], '/', ['Styles', 'Format', 'Font', 'FontSize'], ['TextColor', 'BGColor', 'Smiley', 'ShowBlocks', 'Maximize', 'About']]");
                    builder.AppendLine("});");
                    builder.AppendLine("//]]>");
                    builder.AppendLine("</script>");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                    //builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                }
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            if (SiteConfig.SiteOption.EditVer == "2")
            {
                builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                builder.AppendLine(@"<script type=""text/javascript"">");
                builder.AppendLine(@"KE.show({");
                builder.AppendLine("id : 'txt_" + Name + "'");
                builder.AppendLine(@"});");
                builder.AppendLine(@"</script>");
            }
            else if (SiteConfig.SiteOption.EditVer == "3")
            {
                builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;\"  name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                builder.AppendLine(@"<script type=""text/javascript"">");
                builder.AppendLine(@"setTimeout(function(){UE.getEditor('txt_" + Name + "', {" + BLLCommon.ueditorMid + "});},500);");
                builder.AppendLine(@"</script>");

            }
            else if (SiteConfig.SiteOption.EditVer == "1")
            {
                builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                builder.AppendLine("//<![CDATA[");
                builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                builder.AppendLine("{");
                // builder.AppendLine("    skin: 'v2',");
                builder.AppendLine("    toolbar : [['Source', '-', 'Bold', 'Italic', 'Underline', 'Strike'], ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'], ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'], ['Link', 'Unlink', 'Anchor'], ['Image', 'Flash', 'SpecialChar'], '/', ['Styles', 'Format', 'Font', 'FontSize'], ['TextColor', 'BGColor', 'Smiley', 'ShowBlocks', 'Maximize', 'About']]");
                builder.AppendLine("});");
                builder.AppendLine("//]]>");
                builder.AppendLine("</script>");
            }

            else
            {
                builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                //builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
            }
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //多行文本不支持Html
        public string GetMultipleTextType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                builder.Append("<textarea name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" style=\"height:" + strArray3[1] + "px;width:" + strArray2[1] + "px;\"></textarea> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<textarea name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" style=\"height:" + strArray3[1] + "px;width:" + strArray2[1] + "px;\">" + HttpUtility.UrlDecode(dr["" + Name + ""].ToString()) + "</textarea> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //数字
        public string GetMoneyType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray1 = strArray[1].Split(new char[] { '=' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[2].Split(new char[] { '=' });
            string[] strArray4 = strArray[3].Split(new char[] { '=' });
            if (strArray.Length == 7)
            {
                strArray4 = strArray[6].Split(new char[] { '=' });
            }
            string valuestxt = "";
            if (dr != null)
            {
                valuestxt = dr["" + Name + ""].ToString();
                switch (strArray1[1])
                {
                    case "1":
                        break;
                    case "2":
                        valuestxt = Double.Parse(dr["" + Name + ""].ToString()).ToString("F" + strArray3[1]);
                        break;
                    case "3":
                        valuestxt = Double.Parse(dr["" + Name + ""].ToString()).ToString("F" + strArray3[1]);
                        break;
                }
            }
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray3[1] + "\" /> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"txt\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + valuestxt + "\" /> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //单个文件
        public string GetSmallFileType(string Alias, string Name, bool IsNotNull, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            builder.Append("<table border='0' cellpadding='0' cellspacing='1' width='100%'>\r\n");
            builder.Append("<tr class='tdbg'>\r\n");
            builder.Append("<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"35\" />" + str2 + " " + Description + "</td>\r\n</tr>");
                builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            if (dr[Name] != null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"35\" value=\"" + dr["" + Name + ""].ToString() + "\" />" + str2 + " " + Description + "</td>\r\n</tr>");
            }
            builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //单个图片
        public string GetPicType(string Alias, string Name, bool IsNotNull, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            builder.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\">\r\n");
            builder.Append("<tr class=\"tdbg\">\r\n");
            builder.Append("<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"35\" />" + str2 + " " + Description + "</td>\r\n</tr>");
                builder.Append("<tr class=\"tdbg\">\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"35\" value=\"" + dr["" + Name + ""].ToString() + "\" />" + str2 + " " + Description + "</td>\r\n</tr>");
            builder.Append("<tr class=\"tdbg\">\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe> </td>\r\n</tr>\r\n</table>");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //单选项
        public string GetRadioType(string FieldID, string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr, int ModelID)
        {
            string str = "";
            string[] strArray2 = Content.Split(new char[] { ',' })[0].Split(new char[] { '=' });
            string[] strArray3 = SortStr(strArray2[1]).Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            switch (strArray2[0])
            {
                case "1":
                    {
                        str = "<select name=\"txt_" + Name + "\">";
                        for (int i = 0; i < strArray3.Length; i++)
                        {
                            if (strArray3[i].Contains("|"))
                            {
                                string[] strx = strArray3[i].Split(new char[] { '|' });
                                string showtxt = "0";
                                string showvalue = strx[1];
                                if (showvalue.IndexOf("$") > -1)
                                {
                                    string[] arr = showvalue.Split(new char[] { '$' });
                                    showtxt = arr[1];
                                    showvalue = arr[0];
                                }
                                B_Admin all = new B_Admin();
                                B_User ull = new B_User();
                                bool isadmin = all.CheckLogin();
                                ZoomLa.Model.M_UserInfo info = ull.GetLogin();
                                if (showtxt == "0" || info.UserID == DataConverter.CLng(showtxt))
                                {
                                    if (dr == null)
                                    {
                                        string str4 = str;
                                        str = str4 + "<option value=\"" + showvalue + "\">" + strx[0] + "</option>";
                                    }
                                    else if (showvalue == dr["" + Name + ""].ToString())
                                    {
                                        string str5 = str;
                                        str = str5 + "<option value=\"" + showvalue + "\" selected>" + strx[0] + "</option>";
                                    }
                                    else
                                    {
                                        string str6 = str;
                                        str = str6 + "<option value=\"" + showvalue + "\">" + strx[0] + "</option>";
                                    }
                                }
                            }
                            else
                            {
                                if (dr == null)
                                {
                                    string str4 = str;
                                    str = str4 + "<option value=\"" + strArray3[i] + "\">" + strArray3[i] + "</option>";
                                }
                                else if (strArray3[i] == dr["" + Name + ""].ToString())
                                {
                                    string str5 = str;
                                    str = str5 + "<option value=\"" + strArray3[i] + "\" selected>" + strArray3[i] + "</option>";
                                }
                                else
                                {
                                    string str6 = str;
                                    str = str6 + "<option value=\"" + strArray3[i] + "\">" + strArray3[i] + "</option>";
                                }
                            }
                        }
                        string str7 = str;
                        builder.Append(str7 + "</select> " + str2 + " " + Description + "");
                        //builder.Append("<a href=javascript:void(0) onclick=\"SelectValues(" + FieldID + "," + ModelID + ")\">管理选项</a>");
                        builder.Append("</td>\r\n</tr>\r\n");
                        return builder.ToString();
                    }
                case "2":
                    {
                        str = "";
                        for (int j = 0; j < strArray3.Length; j++)
                        {
                            if (strArray3[j].Contains("|"))
                            {
                                string[] strx = strArray3[j].Split(new char[] { '|' });
                                string showtxt = "0";
                                string showvalue = strx[1];
                                if (strArray3[j].IndexOf('$') > -1)
                                {
                                    string[] arr = showvalue.Split(new char[] { '$' });
                                    showtxt = arr[1];
                                    showvalue = arr[0];
                                }
                                if (dr == null)
                                {
                                    if (j == 0)
                                    {
                                        object obj2 = str;
                                        str = string.Concat(new object[] { obj2, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", showvalue, "\" checked />", strx[1], "" });
                                    }
                                    else
                                    {
                                        object obj3 = str;
                                        str = string.Concat(new object[] { obj3, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", showvalue, "\" />", strx[0], "" });
                                    }
                                }
                                else if (showvalue == dr["" + Name + ""].ToString())//showtxt == "0" || 
                                {
                                    object obj4 = str;
                                    str = string.Concat(new object[] { obj4, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", showvalue, "\" checked />", strx[0], "" });
                                }
                                else
                                {
                                    object obj5 = str;
                                    str = string.Concat(new object[] { obj5, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", showvalue, "\" />", strx[0], "" });
                                }
                            }
                            else
                            {
                                if (dr == null)
                                {
                                    if (j == 0)
                                    {
                                        object obj2 = str;
                                        str = string.Concat(new object[] { obj2, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strArray3[j], "\" checked />", strArray3[j], "" });
                                    }
                                    else
                                    {
                                        object obj3 = str;
                                        str = string.Concat(new object[] { obj3, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strArray3[j], "\" />", strArray3[j], "" });
                                    }
                                }
                                else if (strArray3[j] == dr["" + Name + ""].ToString())
                                {
                                    object obj4 = str;
                                    str = string.Concat(new object[] { obj4, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strArray3[j], "\" checked />", strArray3[j], "" });
                                }
                                else
                                {
                                    object obj5 = str;
                                    str = string.Concat(new object[] { obj5, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strArray3[j], "\" />", strArray3[j], "" });
                                }
                            }
                        }
                        string str8 = str;
                        builder.Append(str8 + "" + str2 + " " + Description + "");
                        builder.Append("</td>\r\n</tr>\r\n");
                        return builder.ToString();
                    }
            }
            return "";
        }
        ///////////////////////////////////////////////////////////////////
        //单行文本
        public string GetTextType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string[] strArray4 = strArray[2].Split(new char[] { '=' });
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input  id=\"txt_" + Name + "\" type=\"" + strArray3[1] + "\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + GetNowuser(strArray4[1]) + "\" /> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"" + strArray3[1] + "\"  id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //超链接
        private string GetSuperLinkType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray3[1] + "\" /> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //运行平台
        private string GetOperatingType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string[] strArray4 = strArray[2].Split(new char[] { '=' });
            string str2 = "";
            string str = "";
            string str3 = "";
            string[] strArr = strArray3[1].Split(new char[] { '|' });
            for (int i = 0; i < strArr.Length; i++)
            {
                if (string.IsNullOrEmpty(str3))
                {
                    str3 = "<a href=\"javascript:ToSystem('" + strArr[i] + "','txt_" + Name + "')\"" + strArr[i] + "</a>";
                }
                else
                {
                    str3 = "/" + "<a href=\"javascript:ToSystem('" + strArr[i] + "','txt_" + Name + "')\"" + strArr[i] + "</a>";
                }
            }
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                str = "<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray4[1] + "\" /> " + str2 + " " + Description + "<br/>";
                str = str + str3;
                builder.Append(str);
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            str = "<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /> " + str2 + " " + Description + "<br/>";
            str = str + str3;
            builder.Append(str);
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //多图片
        private string GetMultPicType(string Alias, string Name, bool IsNotNull, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {
            string str2 = "";
            string[] strArray = Content.Split(new char[] { ',' });
            bool ChkThumb = DataConverter.CBool(strArray[0].Split(new char[] { '=' })[1]);
            string ThumbField = strArray[1].Split(new char[] { '=' })[1];
            StringBuilder builder = new StringBuilder();
            string str = "";
            if (ChkThumb && ThumbField != "")
            {
                str = "<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">缩略图</td>\r\n<td>";
                if (dr == null)
                {
                    str = str + "<input name=\"txt_" + ThumbField + "\" id=\"txt_" + ThumbField + "\" type=\"text\" class=\"inputtext\" style=\"width: 400px;\" /></td>\r\n</tr>";
                }
                else
                {
                    str = str + "<input name=\"txt_" + ThumbField + "\" id=\"txt_" + ThumbField + "\" type=\"text\" class=\"inputtext\" style=\"width: 400px;\" value=\"" + dr["" + ThumbField + ""] + "\" /></td>\r\n</tr>";
                }
            }
            string str1 = "";
            str1 = str1 + "<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">上传图片</td>\r\n<td>";
            str1 = str1 + "<iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiPicUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"165\" scrolling=\"no\"></iframe></td>\r\n</tr>";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            builder.Append(str);
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            builder.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n");
            builder.Append("    <tr>\r\n");
            builder.Append("<td style=\"width: 400px\">\r\n");
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" value=\"" + dr["" + Name + ""].ToString() + "\" />");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" />");
                }
            }
            else
            {
                builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" />");
            }
            builder.Append("<select name=\"sel_" + Name + "\" id=\"sel_" + Name + "\" style=\"width: 400px; height: 100px\" size=\"2\">");
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                {
                    string[] strValue = dr["" + Name + ""].ToString().Split(new char[] { '$' });
                    for (int i = 0; i < strValue.Length; i++)
                    {
                        builder.Append("<option value=\"" + strValue[i] + "\">" + strValue[i] + "</option>");
                    }
                }
            }
            builder.Append("</select> " + str2 + "</td>\r\n<td>");
            builder.Append("<input type=\"button\" class=\"button\" onclick=\"AddPhotoUrl('sel_" + Name + "','txt_" + Name + "')\" value=\"添加外部地址\" /><br/>");
            builder.Append("<input type=\"button\" class=\"button\" value=\"修改当前地址sss\" onclick=\"return ModifyPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" /><br />");
            builder.Append("<input type=\"button\" class=\"button\" value=\"删除当前地址\" onclick=\"DelPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" />");
            builder.Append("</td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>");
            builder.Append(str1);
            return builder.ToString();
        }
        /// <summary>
        /// 多级选项
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Name"></param>
        /// <param name="IsNotNull"></param>
        /// <param name="Content"></param>
        /// <param name="Description"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetGradeOption(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            // throw new Exception(Alias+":"+Name+":"+Content);所属地区:addr:GradeCate=a,Direction=0
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(Content))
            {
                string[] strArray = Content.Split(new char[] { ',' });
                string[] strArray2 = null;
                string[] strArray3 = null;

                if (strArray != null && strArray.Length > 0)
                {
                    strArray2 = strArray[0].Split(new char[] { '=' });
                }
                if (strArray != null && strArray.Length > 1)
                {
                    strArray3 = strArray[1].Split(new char[] { '=' });
                }
                int CateID = DataConverter.CLng(strArray2[1]);
                builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\"><span>" + Alias + "</span>  </td>\r\n<td>\r\n");
                if (dr != null)
                {
                    if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                    {
                        builder.Append("<input type='hidden' name='txt_" + Name + "' id='txt_" + Name + "' value='" + dr["" + Name + ""].ToString() + "' />");
                        if (strArray2[1] == "a")
                        {
                            builder.Append("<iframe id='Drop_" + Name + "' src='" + function.ApplicationRootPath + "/Common/Ppc.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=" + dr["" + Name + ""].ToString() + "' marginheight='0' marginwidth='0' frameborder='0' width='100%' height='30' scrolling='no'></iframe>");
                        }
                        else
                        {
                            builder.Append("<iframe id=\"Drop_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiDropList.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=" + dr["" + Name + ""].ToString() + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe>");
                        }
                    }
                    else
                    {
                        builder.Append("<input type='hidden' name='txt_" + Name + "' id='txt_" + Name + "' />");
                        if (strArray2[1] == "a")
                        {
                            builder.Append("<iframe id='Drop_" + Name + "' src='" + function.ApplicationRootPath + "/Common/Ppc.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=' marginheight='0' marginwidth='0' frameborder='0' width='100%' height='30' scrolling='no'></iframe>");
                        }
                        else
                        {
                            builder.Append("<iframe id=\"Drop_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiDropList.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe>");
                        }
                    }
                }
                else
                {
                    builder.Append("<input type='hidden' name='txt_" + Name + "' id='txt_" + Name + "' value='' />");
                    if (strArray2[1] == "a")
                    {
                        builder.Append("<iframe id='Drop_" + Name + "' src='" + function.ApplicationRootPath + "/Common/Ppc.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=' marginheight='0' marginwidth='0' frameborder='0' width='100%' height='30' scrolling='no'></iframe>");
                    }
                    else
                    {
                        builder.Append("<iframe id=\"Drop_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiDropList.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe>");
                    }
                }
                builder.Append("</td>\r\n</tr>\r\n");
            }

            return builder.ToString();
        }
        //颜色代码
        public string GetColorType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            builder.Append("<script language=\"JavaScript\" type=\"text/javascript\">\r\n");
            builder.Append("function SelectColor(t, clientId) {\r\n");
            builder.Append("var url = \"../../Common/SelectColor.aspx?d=f&t=6\"; \r\n");
            builder.Append("var old_color = (document.getElementById(clientId).value.indexOf('#') == 0) ? '&' + document.getElementById(clientId).value.substr(1) : '&' + document.getElementById(clientId).value;\r\n");
            builder.Append("if (document.all) {\r\n");
            builder.Append("var color = showModalDialog(url + old_color, '',\"dialogWidth:18.5em; dialogHeight:16.0em; status:0\");\r\n");
            builder.Append("if (color != null) {\r\n");
            builder.Append("document.getElementById(clientId).value = color;\r\n");
            builder.Append("} else {\r\n");
            builder.Append("document.getElementById(clientId).focus();\r\n");
            builder.Append("}\r\n");
            builder.Append("} else {\r\n");
            builder.Append("var color = window.open(url + '&' + clientId, \"hbcmsPop\", \"top=200,left=200,scrollbars=yes,dialog=yes,modal=no,width=300,height=260,resizable=yes\");\r\n");
            builder.Append("}\r\n");
            builder.Append("}\r\n");
            builder.Append("</script>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" maxlength=\"7\"  size=\"7\"  value=\"" + strArray3[1] + "\" /> " + str2 + " " + Description + "");
                builder.Append("<img onclick=\"SelectColor(this,'txt_" + Name + "');\" src=\"/images/Rect.gif\" align=\"absmiddle\" style=\"border-width: 0px; cursor: pointer\" /></td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" maxlength=\"7\"  size=\"7\"  value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /> " + str2 + " " + Description + "");
            builder.Append("<img onclick=\"SelectColor(this,'txt_" + Name + "');\" src=\"/images/Rect.gif\" align=\"absmiddle\" style=\"border-width: 0px; cursor: pointer\" /></td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        /*********************************************************************************************************************************************************/
        //日期时间
        public string GetDateType(string Alias, string Name, bool IsNotNull, string Description, DataRow dr, string tempid)
        {
            string str2 = "";
            StringBuilder builder = new StringBuilder();
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            if (dr == null)
            {
                builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>");
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"18\" onclick=\"setday(this);\" readonly=\"true\" value=\"\" /> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>");
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"18\"  onclick=\"setday(this);\" readonly=\"true\" value=\"" + dr["" + Name + ""].ToString() + "\" /> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //文件
        public string GetFileType(string Alias, string Name, bool IsNotNull, string Content, string Description, int ModelID, int NodeID, DataRow dr, string tempid)
        {
            string str2 = "";
            string[] strArray = Content.Split(new char[] { ',' });
            bool ChkFileSize = DataConverter.CBool(strArray[0].Split(new char[] { '=' })[1]);
            string FileSizeField = strArray[1].Split(new char[] { '=' })[1];
            StringBuilder builder = new StringBuilder();
            string str = "";
            if (ChkFileSize && FileSizeField != "")
            {
                str = "<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">文件大小</td>\r\n<td>";
                if (dr == null)
                {
                    str = str + "<input name=\"txt_" + FileSizeField + "\" type=\"text\" class=\"inputtext\" />K</td>\r\n</tr>";
                }
                else
                {
                    str = str + "<input name=\"txt_" + FileSizeField + "\" type=\"text\" class=\"inputtext\" value=\"" + dr["" + FileSizeField + ""].ToString() + "\" />K</td>\r\n</tr>";
                }
            }
            str = str + "<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">上传文件</td>\r\n<td>";
            str = str + "<iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            builder.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n");
            builder.Append("    <tr>\r\n");
            builder.Append("<td style=\"width: 400px\">\r\n");
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" value=\"" + dr["" + Name + ""].ToString() + "\" />");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" />");
                }
            }
            else
            {
                builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" />");
            }
            builder.Append("<select id=\"sel_" + Name + "\" name=\"sel_" + Name + "\" style=\"width: 400px; height: 100px\" size=\"2\">");
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                {
                    string[] strValue = dr["" + Name + ""].ToString().Split(new char[] { '$' });
                    for (int i = 0; i < strValue.Length; i++)
                    {
                        builder.Append("<option value=\"" + strValue[i] + "\">" + strValue[i] + "</option>");
                    }
                }
            }
            builder.Append("</select> " + str2 + "</td>\r\n<td>");
            builder.Append("<input type=\"button\" class=\"button\" onclick=\"AddSoftUrl('sel_" + Name + "','txt_" + Name + "')\" value=\"添加外部地址\" /><br/>");
            builder.Append("<input type=\"button\" class=\"button\" value=\"修改当前地址\" onclick=\"return ModifyPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" /><br />");
            builder.Append("<input type=\"button\" class=\"button\" value=\"删除当前地址\" onclick=\"DelPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" />");
            builder.Append("</td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>");
            builder.Append(str);
            return builder.ToString();
        }
        //多行文本支持Html
        public string GetMultipleHtmlType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr, string tempid)
        {
            string str = "";
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string[] strArray4 = strArray[2].Split(new char[] { '=' });
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            string str3 = strArray4[1];
            if (str3 == null)
            {
                return str;
            }
            if (!(str3 == "1"))
            {
                if (str3 != "2")
                {
                    if (str3 != "3")
                    {
                        return str;
                    }
                    if (dr == null)
                    {
                        if (SiteConfig.SiteOption.EditVer == "2")
                        {
                            builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                            builder.AppendLine(@"<script type=""text/javascript"">");
                            builder.AppendLine(@"KE.show({");
                            builder.AppendLine("id : 'txt_" + Name + "'");
                            builder.AppendLine(@"});");
                            builder.AppendLine(@"</script>");
                        }
                        else if (SiteConfig.SiteOption.EditVer == "1")
                        {
                            builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                            builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                            builder.AppendLine("//<![CDATA[");
                            builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                            builder.AppendLine("{");
                            builder.AppendLine("    skin: 'v2',");
                            builder.AppendLine("    ");
                            builder.AppendLine("});");
                            builder.AppendLine("//]]>");
                            builder.AppendLine("</script>");
                        }
                        else
                        {
                            builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_3.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                            //builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_3.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                        }
                        builder.Append("</td>\r\n</tr>\r\n");
                        return builder.ToString();
                    }
                    if (SiteConfig.SiteOption.EditVer == "2")
                    {
                        builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                        builder.AppendLine(@"<script type=""text/javascript"">");
                        builder.AppendLine(@"KE.show({");
                        builder.AppendLine("id : 'txt_" + Name + "'");
                        builder.AppendLine(@"});");
                        builder.AppendLine(@"</script>");
                    }
                    else if (SiteConfig.SiteOption.EditVer == "1")
                    {
                        builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                        builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                        builder.AppendLine("//<![CDATA[");
                        builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                        builder.AppendLine("{");
                        builder.AppendLine("    skin: 'v2',");
                        builder.AppendLine("    ");
                        builder.AppendLine("});");
                        builder.AppendLine("//]]>");
                        builder.AppendLine("</script>");
                    }
                    else
                    {
                        builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_3.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                    }
                    builder.Append("</td>\r\n</tr>\r\n");
                    return builder.ToString();
                }
            }
            else
            {
                if (dr == null)
                {
                    if (SiteConfig.SiteOption.EditVer == "2")
                    {
                        builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                        builder.AppendLine(@"<script type=""text/javascript"">");
                        builder.AppendLine(@"KE.show({");
                        builder.AppendLine("id : 'txt_" + Name + "'");
                        builder.AppendLine(@"});");
                        builder.AppendLine(@"</script>");
                    }
                    else if (SiteConfig.SiteOption.EditVer == "1")
                    {
                        builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                        builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                        builder.AppendLine("//<![CDATA[");
                        builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                        builder.AppendLine("{");
                        builder.AppendLine("    skin: 'v2',");
                        builder.AppendLine("    toolbar : [['Source', '-', 'Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', 'Smiley', 'ShowBlocks', 'Maximize', 'About']]");
                        builder.AppendLine("});");
                        builder.AppendLine("//]]>");
                        builder.AppendLine("</script>");
                    }
                    else
                    {
                        builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                        //builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                    }
                    builder.Append("</td>\r\n</tr>\r\n");
                    return builder.ToString();
                }
                if (SiteConfig.SiteOption.EditVer == "2")
                {
                    builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                    builder.AppendLine(@"<script type=""text/javascript"">");
                    builder.AppendLine(@"KE.show({");
                    builder.AppendLine("id : 'txt_" + Name + "'");
                    builder.AppendLine(@"});");
                    builder.AppendLine(@"</script>");
                }
                else if (SiteConfig.SiteOption.EditVer == "1")
                {
                    builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                    builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                    builder.AppendLine("//<![CDATA[");
                    builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                    builder.AppendLine("{");
                    builder.AppendLine("    skin: 'v2',");
                    builder.AppendLine("    toolbar : [['Source', '-', 'Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', 'Smiley', 'ShowBlocks', 'Maximize', 'About']]");
                    builder.AppendLine("});");
                    builder.AppendLine("//]]>");
                    builder.AppendLine("</script>");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                    //builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                }
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            if (dr == null)
            {
                if (SiteConfig.SiteOption.EditVer == "2")
                {
                    builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                    builder.AppendLine(@"<script type=""text/javascript"">");
                    builder.AppendLine(@"KE.show({");
                    builder.AppendLine("id : 'txt_" + Name + "'");
                    builder.AppendLine(@"});");
                    builder.AppendLine(@"</script>");
                }
                else if (SiteConfig.SiteOption.EditVer == "1")
                {
                    builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\"></textarea>");
                    builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                    builder.AppendLine("//<![CDATA[");
                    builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                    builder.AppendLine("{");
                    builder.AppendLine("    skin: 'v2',");
                    builder.AppendLine("    toolbar : [['Source', '-', 'Bold', 'Italic', 'Underline', 'Strike'], ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'], ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'], ['Link', 'Unlink', 'Anchor'], ['Image', 'Flash', 'SpecialChar'], '/', ['Styles', 'Format', 'Font', 'FontSize'], ['TextColor', 'BGColor', 'Smiley', 'ShowBlocks', 'Maximize', 'About']]");
                    builder.AppendLine("});");
                    builder.AppendLine("//]]>");
                    builder.AppendLine("</script>");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                    //builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                }
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            if (SiteConfig.SiteOption.EditVer == "2")
            {
                builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" style=\"width:" + strArray2[1] + "px;height:" + strArray3[1] + "px;visibility:hidden;\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                builder.AppendLine(@"<script type=""text/javascript"">");
                builder.AppendLine(@"KE.show({");
                builder.AppendLine("id : 'txt_" + Name + "'");
                builder.AppendLine(@"});");
                builder.AppendLine(@"</script>");
            }
            else if (SiteConfig.SiteOption.EditVer == "1")
            {
                builder.AppendLine("<textarea cols=\"80\" id=\"txt_" + Name + "\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" name=\"txt_" + Name + "\" rows=\"10\">" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "</textarea>");
                builder.AppendLine("<script id=\"headscript\" type=\"text/javascript\">");
                builder.AppendLine("//<![CDATA[");
                builder.AppendLine("CKEDITOR.replace('txt_" + Name + "',");
                builder.AppendLine("{");
                builder.AppendLine("    skin: 'v2',");
                builder.AppendLine("    toolbar : [['Source', '-', 'Bold', 'Italic', 'Underline', 'Strike'], ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'], ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'], ['Link', 'Unlink', 'Anchor'], ['Image', 'Flash', 'SpecialChar'], '/', ['Styles', 'Format', 'Font', 'FontSize'], ['TextColor', 'BGColor', 'Smiley', 'ShowBlocks', 'Maximize', 'About']]");
                builder.AppendLine("});");
                builder.AppendLine("//]]>");
                builder.AppendLine("</script>");
            }
            else
            {
                builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\" /><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                //builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
            }
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //多行文本不支持Html
        public string GetMultipleTextType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr, string tempid)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                builder.Append("<textarea name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" style=\"height:" + strArray3[1] + "px;width:" + strArray2[1] + "px;\"></textarea> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<textarea name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" style=\"height:" + strArray3[1] + "px;width:" + strArray2[1] + "px;\">" + HttpUtility.UrlDecode(dr["" + Name + ""].ToString()) + "</textarea> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //数字
        public string GetNumberType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray1 = strArray[1].Split(new char[] { '=' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[2].Split(new char[] { '=' });
            string[] strArray4 = strArray[3].Split(new char[] { '=' });
            if (strArray.Length == 7)
            {
                strArray4 = strArray[6].Split(new char[] { '=' });
            }
            string valuestxt = "";
            if (dr != null)
            {
                valuestxt = dr["" + Name + ""].ToString();
                switch (strArray1[1])
                {
                    case "1":
                        break;
                    case "2":
                        valuestxt = Double.Parse(dr["" + Name + ""].ToString()).ToString("F" + strArray3[1]);
                        break;
                    case "3":
                        valuestxt = Double.Parse(dr["" + Name + ""].ToString()).ToString("F" + strArray3[1]);
                        break;
                }
            }
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray3[1] + "\" /> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"txt\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + valuestxt + "\" /> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }//数字
        public string GetMoneyType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr, string tempid)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray1 = strArray[1].Split(new char[] { '=' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[2].Split(new char[] { '=' });
            string[] strArray4 = strArray[3].Split(new char[] { '=' });
            if (strArray.Length == 7)
            {
                strArray4 = strArray[6].Split(new char[] { '=' });
            }
            string valuestxt = "";
            if (dr != null)
            {
                valuestxt = dr["" + Name + ""].ToString();
                switch (strArray1[1])
                {
                    case "1":
                        break;
                    case "2":
                        valuestxt = Double.Parse(dr["" + Name + ""].ToString()).ToString("F" + strArray3[1]);
                        break;
                    case "3":
                        valuestxt = Double.Parse(dr["" + Name + ""].ToString()).ToString("F" + strArray3[1]);
                        break;
                }
            }
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray3[1] + "\" /> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"txt\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + valuestxt + "\" /> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //单个文件
        public string GetSmallFileType(string Alias, string Name, bool IsNotNull, string Content, string Description, int ModelID, int NodeID, DataRow dr, string tempid)
        {
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            builder.Append("<table border='0' cellpadding='0' cellspacing='1' width='100%'>\r\n");
            builder.Append("<tr class='tdbg'>\r\n");
            builder.Append("<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"35\" />" + str2 + " " + Description + "</td>\r\n</tr>");
                builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"35\" value=\"" + dr["" + Name + ""].ToString() + "\" />" + str2 + " " + Description + "</td>\r\n</tr>");
            builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //单个图片
        public string GetPicType(string Alias, string Name, bool IsNotNull, string Content, string Description, int ModelID, int NodeID, DataRow dr, string tempid)
        {
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            builder.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"1\" width=\"100%\">\r\n");
            builder.Append("<tr class=\"tdbg\">\r\n");
            builder.Append("<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"35\" />" + str2 + " " + Description + "</td>\r\n</tr>");
                builder.Append("<tr class=\"tdbg\">\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"35\" value=\"" + dr["" + Name + ""].ToString() + "\" />" + str2 + " " + Description + "</td>\r\n</tr>");
            builder.Append("<tr class=\"tdbg\">\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //单选项
        public string GetRadioType(string FieldID, string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr, string tempid, int ModelID)
        {
            string str = "";
            string[] strArray2 = Content.Split(new char[] { ',' })[0].Split(new char[] { '=' });
            string[] strArray3 = SortStr(strArray2[1]).Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            switch (strArray2[0])
            {
                case "1":
                    {
                        str = "<select name=\"txt_" + Name + "\">";
                        for (int i = 0; i < strArray3.Length; i++)
                        {
                            if (strArray3[i].Contains("|"))
                            {
                                string[] strx = strArray3[i].Split(new char[] { '|' });
                                string showtxt = "0";
                                string showvalue = strx[1];
                                if (showvalue.IndexOf("$") > -1)
                                {
                                    string[] arr = showvalue.Split(new char[] { '$' });
                                    showtxt = arr[1];
                                    showvalue = arr[0];
                                }
                                B_Admin all = new B_Admin();
                                B_User ull = new B_User();
                                bool isadmin = all.CheckLogin();
                                ZoomLa.Model.M_UserInfo info = ull.GetLogin();
                                if (showtxt == "0" || info.UserID == DataConverter.CLng(showtxt))
                                {
                                    if (dr == null)
                                    {
                                        string str4 = str;
                                        str = str4 + "<option value=\"" + showvalue + "\">" + strx[0] + "</option>";
                                    }
                                    else if (showvalue == dr["" + Name + ""].ToString())
                                    {
                                        string str5 = str;
                                        str = str5 + "<option value=\"" + showvalue + "\" selected>" + strx[0] + "</option>";
                                    }
                                    else
                                    {
                                        string str6 = str;
                                        str = str6 + "<option value=\"" + showvalue + "\">" + strx[0] + "</option>";
                                    }
                                }
                            }
                            else
                            {
                                if (dr == null)
                                {
                                    string str4 = str;
                                    str = str4 + "<option value=\"" + strArray3[i] + "\">" + strArray3[i] + "</option>";
                                }
                                else if (strArray3[i] == dr["" + Name + ""].ToString())
                                {
                                    string str5 = str;
                                    str = str5 + "<option value=\"" + strArray3[i] + "\" selected>" + strArray3[i] + "</option>";
                                }
                                else
                                {
                                    string str6 = str;
                                    str = str6 + "<option value=\"" + strArray3[i] + "\">" + strArray3[i] + "</option>";
                                }
                            }
                        }
                        string str7 = str;
                        builder.Append(str7 + "</select> " + str2 + " " + Description + "");
                        builder.Append("<a href=javascript:void(0) onclick=\"SelectValues(" + FieldID + "," + ModelID.ToString() + ")\">管理选项</a>");
                        builder.Append("</td>\r\n</tr>\r\n");
                        return builder.ToString();
                    }
                case "2":
                    {
                        str = "";
                        for (int j = 0; j < strArray3.Length; j++)
                        {
                            if (strArray3[j].Contains("|"))
                            {
                                string[] strx = strArray3[j].Split(new char[] { '|' });
                                string showtxt = "0";
                                string showvalue = strx[1];
                                if (strArray3[j].IndexOf('$') > -1)
                                {
                                    string[] arr = showvalue.Split(new char[] { '$' });
                                    showtxt = arr[1];
                                    showvalue = arr[0];
                                }
                                if (dr == null)
                                {
                                    if (j == 0)
                                    {
                                        object obj2 = str;
                                        str = string.Concat(new object[] { obj2, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", showvalue, "\" checked />", strx[1], "" });
                                    }
                                    else
                                    {
                                        object obj3 = str;
                                        str = string.Concat(new object[] { obj3, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", showvalue, "\" />", strx[0], "" });
                                    }
                                }
                                else if (showtxt == "0" || showvalue == dr["" + Name + ""].ToString())
                                {
                                    object obj4 = str;
                                    str = string.Concat(new object[] { obj4, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", showvalue, "\" checked />", strx[0], "" });
                                }
                                else
                                {
                                    object obj5 = str;
                                    str = string.Concat(new object[] { obj5, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", showvalue, "\" />", strx[0], "" });
                                }
                            }
                            else
                            {
                                if (dr == null)
                                {
                                    if (j == 0)
                                    {
                                        object obj2 = str;
                                        str = string.Concat(new object[] { obj2, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strArray3[j], "\" checked />", strArray3[j], "" });
                                    }
                                    else
                                    {
                                        object obj3 = str;
                                        str = string.Concat(new object[] { obj3, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strArray3[j], "\" />", strArray3[j], "" });
                                    }
                                }
                                else if (strArray3[j] == dr["" + Name + ""].ToString())
                                {
                                    object obj4 = str;
                                    str = string.Concat(new object[] { obj4, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strArray3[j], "\" checked />", strArray3[j], "" });
                                }
                                else
                                {
                                    object obj5 = str;
                                    str = string.Concat(new object[] { obj5, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strArray3[j], "\" />", strArray3[j], "" });
                                }
                            }
                        }
                        string str8 = str;
                        builder.Append(str8 + "" + str2 + " " + Description + "");
                        builder.Append("</td>\r\n</tr>\r\n");
                        return builder.ToString();
                    }
            }
            return "";
        }
        ///////////////////////////////////////////////////////////////////
        //单行文本
        public string GetTextType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr, string tempid)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string[] strArray4 = strArray[2].Split(new char[] { '=' });
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"" + strArray3[1] + "\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + GetNowuser(strArray4[1]) + "\" /> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"" + strArray3[1] + "\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //超链接
        private string GetSuperLinkType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr, string tempid)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray3[1] + "\" /> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //运行平台
        private string GetOperatingType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr, string tempid)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string[] strArray4 = strArray[2].Split(new char[] { '=' });
            string str2 = "";
            string str = "";
            string str3 = "";
            string[] strArr = strArray3[1].Split(new char[] { '|' });
            for (int i = 0; i < strArr.Length; i++)
            {
                if (string.IsNullOrEmpty(str3))
                {
                    str3 = "<a href=\"javascript:ToSystem('" + strArr[i] + "','txt_" + Name + "')\"" + strArr[i] + "</a>";
                }
                else
                {
                    str3 = "/" + "<a href=\"javascript:ToSystem('" + strArr[i] + "','txt_" + Name + "')\"" + strArr[i] + "</a>";
                }
            }
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                str = "<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray4[1] + "\" /> " + str2 + " " + Description + "<br/>";
                str = str + str3;
                builder.Append(str);
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            str = "<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /> " + str2 + " " + Description + "<br/>";
            str = str + str3;
            builder.Append(str);
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //多图片
        private string GetMultPicType(string Alias, string Name, bool IsNotNull, string Content, string Description, int ModelID, int NodeID, DataRow dr, string tempid)
        {
            string str2 = "";
            string[] strArray = Content.Split(new char[] { ',' });
            bool ChkThumb = DataConverter.CBool(strArray[0].Split(new char[] { '=' })[1]);
            string ThumbField = strArray[1].Split(new char[] { '=' })[1];
            StringBuilder builder = new StringBuilder();
            string str = "";
            if (ChkThumb && ThumbField != "")
            {
                str = "<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">缩略图</td>\r\n<td>";
                if (dr == null)
                {
                    str = str + "<input name=\"txt_" + ThumbField + "\" id=\"txt_" + ThumbField + "\" type=\"text\" class=\"inputtext\" style=\"width: 400px;\" /></td>\r\n</tr>";
                }
                else
                {
                    str = str + "<input name=\"txt_" + ThumbField + "\" id=\"txt_" + ThumbField + "\" type=\"text\" class=\"inputtext\" style=\"width: 400px;\" value=\"" + dr["" + ThumbField + ""] + "\" /></td>\r\n</tr>";
                }
            }
            string str1 = "";
            str1 = str1 + "<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">上传图片</td>\r\n<td>";
            str1 = str1 + "<iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiPicUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"165\" scrolling=\"no\"></iframe></td>\r\n</tr>";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            builder.Append(str);
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            builder.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n");
            builder.Append("    <tr>\r\n");
            builder.Append("<td style=\"width: 400px\">\r\n");
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" value=\"" + dr["" + Name + ""].ToString() + "\" />");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" />");
                }
            }
            else
            {
                builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" />");
            }
            builder.Append("<select name=\"sel_" + Name + "\" id=\"sel_" + Name + "\" style=\"width: 400px; height: 100px\" size=\"2\">");
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                {
                    string[] strValue = dr["" + Name + ""].ToString().Split(new char[] { '$' });
                    for (int i = 0; i < strValue.Length; i++)
                    {
                        builder.Append("<option value=\"" + strValue[i] + "\">" + strValue[i] + "</option>");
                    }
                }
            }
            builder.Append("</select> " + str2 + "</td>\r\n<td>");
            builder.Append("<input type=\"button\" class=\"button\" onclick=\"AddPhotoUrl('sel_" + Name + "','txt_" + Name + "')\" value=\"添加外部地址\" /><br/>");
            builder.Append("<input type=\"button\" class=\"button\" value=\"修改当前地址\" onclick=\"return ModifyPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" /><br />");
            builder.Append("<input type=\"button\" class=\"button\" value=\"删除当前地址\" onclick=\"DelPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" />");
            builder.Append("</td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>");
            builder.Append(str1);
            return builder.ToString();
        }
        /// <summary>
        /// 多级选项
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Name"></param>
        /// <param name="IsNotNull"></param>
        /// <param name="Content"></param>
        /// <param name="Description"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetGradeOption(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr, string tempid)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            int CateID = DataConverter.CLng(strArray2[1]);
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" value=\"" + dr["" + Name + ""].ToString() + "\" />");
                    builder.Append("<iframe id=\"Drop_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiDropList.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=" + dr["" + Name + ""].ToString() + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe>");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" />");
                    builder.Append("<iframe id=\"Drop_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiDropList.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe>");
                }
            }
            else
            {
                builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" />");
                builder.Append("<iframe id=\"Drop_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiDropList.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe>");
            }
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //颜色代码
        public string GetColorType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr, string tempid)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            builder.Append("<script language=\"JavaScript\" type=\"text/javascript\">\r\n");
            builder.Append("function SelectColor(t, clientId) {\r\n");
            builder.Append("var url = \"../../Common/SelectColor.aspx?d=f&t=6\"; \r\n");
            builder.Append("var old_color = (document.getElementById(clientId).value.indexOf('#') == 0) ? '&' + document.getElementById(clientId).value.substr(1) : '&' + document.getElementById(clientId).value;\r\n");
            builder.Append("if (document.all) {\r\n");
            builder.Append("var color = showModalDialog(url + old_color, '',\"dialogWidth:18.5em; dialogHeight:16.0em; status:0\");\r\n");
            builder.Append("if (color != null) {\r\n");
            builder.Append("document.getElementById(clientId).value = color;\r\n");
            builder.Append("} else {\r\n");
            builder.Append("document.getElementById(clientId).focus();\r\n");
            builder.Append("}\r\n");
            builder.Append("} else {\r\n");
            builder.Append("var color = window.open(url + '&' + clientId, \"hbcmsPop\", \"top=200,left=200,scrollbars=yes,dialog=yes,modal=no,width=300,height=260,resizable=yes\");\r\n");
            builder.Append("}\r\n");
            builder.Append("}\r\n");
            builder.Append("</script>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" maxlength=\"7\"  size=\"7\"  value=\"" + strArray3[1] + "\" /> " + str2 + " " + Description + "");
                builder.Append("<img onclick=\"SelectColor(this,'txt_" + Name + "');\" src=\"/images/Rect.gif\" align=\"absmiddle\" style=\"border-width: 0px; cursor: pointer\" /></td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" maxlength=\"7\"  size=\"7\"  value=\"" + HttpUtility.HtmlEncode(dr["" + Name + ""].ToString()) + "\" /> " + str2 + " " + Description + "");
            builder.Append("<img onclick=\"SelectColor(this,'txt_" + Name + "');\" src=\"/images/Rect.gif\" align=\"absmiddle\" style=\"border-width: 0px; cursor: pointer\" /></td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        /******************************************************************************************************************************************************************/
        /// <summary>
        /// 用于用户中心MyContent.aspx(Disuse)
        /// </summary>
        public string ShowStyleField(string FieldID, string Alias, string Name, bool IsNotNull, string Type, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {
            switch (Type)
            {
                case "TextType":
                    //return this.GetTextType(Alias, Name, IsNotNull, Content, Description, dr);
                    return showBll.GetTextType(Alias, Name, IsNotNull, Content, Description, dr);
                case "ListBoxType":
                    return showBll.GetListBoxType(Alias, Name, IsNotNull, Content, Description, dr);
                case "DateType":
                    return this.GetDateType(Alias, Name, IsNotNull, Content, Description, dr);
                case "MultipleTextType":
                    return showBll.GetMultipleTextType(Alias, Name, IsNotNull, Content, Description, dr);
                case "MultipleHtmlType":
                    return showBll.GetMultipleHtmlType(Alias, Name, IsNotNull, Content, Description, NodeID, dr, ModelID);
                case "PicType":
                    return showBll.GetPicType(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
                case "OptionType":
                    return showBll.GetRadioType(Alias, Name, IsNotNull, Content, Description, dr);
                case "FileType":
                    return this.GetFileType(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
                case "SmallFileType":
                    return this.GetSmallFileType(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
                case "NumType":
                    return this.GetNumberType(Alias, Name, IsNotNull, Content, Description, dr);
                case "MoneyType2":
                    return this.GetMoneyType(Alias, Name, IsNotNull, Content, Description, dr);
                case "MultiPicType":
                    return this.GetMultPicType(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
                case "OperatingType":
                    return this.GetOperatingType(Alias, Name, IsNotNull, Content, Description, dr);
                case "SuperLinkType":
                    return this.GetSuperLinkType(Alias, Name, IsNotNull, Content, Description, dr);
                case "GradeOptionType":
                    return this.GetGradeOption(Alias, Name, IsNotNull, Content, Description, dr);
                case "ColorType":
                    return this.GetColorType(Alias, Name, IsNotNull, Content, Description, dr);
                case "DoubleDateType":
                    return this.GetDoubleDateType(Alias, Name, IsNotNull, Description, dr);
                case "Upload":
                    return this.GetSwf(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
                case "MapType":
                    return this.GetGoogleMap(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
                //case "Charts":
                //    return this.Charts(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
                case "SwfFileUpload":
                    return this.SwfFileUpload(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
                case "TableField":
                    return showBll.TableField(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
                case "Random":
                    return showBll.RandomCode(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
                case "Images":
                    return showBll.ImagesCode(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
                case "CameraType":
                    return showBll.CameraCode(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);
            }
            return "";
        }

        //双时间字段
        public string GetDoubleDateType(string Alias, string Name, bool IsNotNull, string Description, DataRow dr)
        {
            string str2 = "";
            StringBuilder builder = new StringBuilder();
            builder.Append("<link rel='stylesheet' type='text/css' href='/App_Themes/AdminDefaultTheme/doutime.css'>");
            string timeNow = DateTime.Now.ToShortDateString().Replace('/', '-');
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            if (dr == null || (dr != null && dr["" + Name + ""].ToString().Split('|').Length < 2))
            {
                builder.Append("<tr class='tdbg'>\r\n<td align=\"right\" class=\"tdbgleft\"><span>" + Alias + "</span>  </td>\r\n<td>");
                builder.Append("<div id='hotelDiv'>");
                builder.Append("<input type='text' value='yyyy-mm-dd' id='CheckInDate' name='txt" + Name + "' /> 至 ");
                builder.Append("<input type='text' value='yyyy-mm-dd' id='CheckOutDate' name='txt_" + Name + "'/> </div>");
                builder.Append("<input id='IntervallCheckInAndChekOut' value='1' type='hidden' />");
                builder.Append("<input id='CheckOut' type='hidden' />");
                builder.Append("<script type='text/javascript' src='/JS/systemall.js'></script>");
                builder.Append("<script type='text/javascript' src='/JS/homecn.js'></script>");
                builder.Append("<div id='serverdate' value='" + timeNow + "'></div>");
                builder.Append("<div id='m_contentend'></div>");
                builder.Append("</td>\r\n</tr>\r\n");

                str2 += builder.ToString();
            }
            else
            {
                string[] valu = dr["" + Name + ""].ToString().Split('|');
                if (valu != null && valu.Length > 1)
                {
                    builder.Append("<tr class='tdbg'>\r\n<td align=\"right\" class=\"tdbgleft\"><span>" + Alias + "</span>  </td>\r\n<td>");
                    builder.Append("<div id='hotelDiv'>");
                    builder.Append("<input type='text' value='yyyy-mm-dd ' id='CheckInDate' name='txt" + Name + "' /> 至 ");
                    builder.Append("<input type='text' value='yyyy-mm-dd' id='CheckOutDate' name='txt_" + Name + "'/> </div>");
                    builder.Append("<input id='IntervallCheckInAndChekOut' value='1' type='hidden' />");
                    builder.Append("<input id='CheckOut' type='hidden' value='" + valu[1].Trim().Replace('/', '-') + "' />");
                    builder.Append("<script type='text/javascript' src='/JS/systemall.js'></script>");
                    builder.Append("<script type='text/javascript' src='/JS/homecn.js'></script>");
                    builder.Append("<div id='serverdate' value='" + valu[0].Trim().Replace('/', '-') + "'></div>");
                    builder.Append("<div id='m_contentend'></div>");
                    builder.Append("</td>\r\n</tr>\r\n");
                    str2 += builder.ToString();
                }
            }
            return str2;
        }
        private string GetNowuser(string label)
        {
            B_User ull = new B_User();

            if (label == "{nowuser}")
            {
                return ull.GetLogin().UserName;
            }
            return label;
        }
        private string SortStr(string str)
        {
            string context = "";
            string[] strx = str.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in strx)
            {
                if (s.IndexOf("|") > 0)
                {
                    string[] ss = s.Split(new char[] { '|' });
                    if (ss.Length > 2)
                        context += SortStr(s.Replace("|", "||"));
                    else
                        context += s;
                }
                else
                {
                    context += s + "|" + s;
                }
                context += "||";
            }
            if (context.EndsWith("||"))
            {
                context = context.Remove(context.Length - 2);
            }
            return context;
        }
        //在线浏览
        public string GetSwf(string Alias, string Name, bool IsNotNull, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\"><span>" + Alias + "</span>  </td>\r\n<td>\r\n");
            builder.Append("<table border='0' cellpadding='0' cellspacing='1' width='100%'>\r\n");
            builder.Append("<tr class='tdbg'>\r\n");
            builder.Append("<td>\r\n");
            B_Node bn = new B_Node();
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"35\">" + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>");
                builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"/Common/OfficeToFlash.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" size=\"35\" value=\"" + dr["" + Name + ""].ToString() + "\">" + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>");
            builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"/Common/OfficeToFlash.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }




        public string GetGoogleMap(string Alias, string Name, bool IsNotNull, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {

            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\"><span>" + Alias + "</span>  </td>\r\n");
            B_Node bn = new B_Node();
            builder.Append("<td class=\"bqright\" style=\"line-height:100px; height:100px\">");

            //builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"/Common/OfficeToFlash.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
            try
            {
                if (dr == null)
                {
                    builder.Append("<input type=\"hidden\" id=\"hmap\" name=\"hmap\" value=\"0\" />");
                    builder.Append(" <input type=\"button\" value=\"添加地图\" onclick=\"Addmap();\" />");
                    builder.Append("</td>\r\n");
                    builder.Append("</td>\r\n");
                    builder.Append("<script>function Addmap() {window.open('../Content/AddMap.aspx?Mid=0', 'newWin', 'modal=yes,width=900,height=600,resizable=yes,scrollbars=yes');}</script>");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" id=\"hmap\" name=\"hmap\" value=\"" + dr["" + Name + ""].ToString() + "\" />");
                    builder.Append(" <input type=\"button\" value=\"预览地图\" onclick=\"Addmap();\" />");
                    builder.Append("</td>\r\n");
                    builder.Append("</td>\r\n");
                    builder.Append("<script>function Addmap() {window.open('../Content/AddMap.aspx?Mid=" + dr["" + Name + ""].ToString() + "', 'newWin', 'modal=yes,width=900,height=600,resizable=yes,scrollbars=yes');}</script>");
                }
            }
            catch
            {
                builder.Append("<input type=\"hidden\" id=\"hmap\" name=\"hmap\" value=\"0\" />");
                builder.Append("<script>function Addmap() {window.open('../Content/AddMap.aspx?Mid=0', 'newWin', 'modal=yes,width=900,height=600,resizable=yes,scrollbars=yes');}</script>");
            }
            return builder.ToString();
            //StringBuilder builder = new StringBuilder();
            //builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\"><span>" + Alias + "</span>  </td>\r\n");
            //B_Node bn = new B_Node();
            //builder.Append("<td class=\"bqright\" style=\"line-height:100px; height:100px\">");
            //builder.Append("<input type=\"hidden\" id=\"hmap\" name=\"hmap\" />");
            //builder.Append(" <input type=\"button\" value=\"标注地图\" onclick=\"Addmap();\" />");
            //builder.Append("</td>\r\n");
            ////builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"/Common/OfficeToFlash.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
            //builder.Append("</td>\r\n");
            //try
            //{
            //    if (dr == null || dr[Name].ToString() == "0")
            //    { builder.Append("<script>function Addmap() {window.open('../Content/AddMap.aspx?Mid=0', 'newWin', 'modal=yes,width=900,height=500,resizable=yes,scrollbars=yes');}</script>"); }
            //    else
            //    {
            //        builder.Append("<script>function Addmap() {window.open('../Content/AddMap.aspx?Mid=" + dr["" + Name + ""].ToString() + "', 'newWin', 'modal=yes,width=900,height=500,resizable=yes,scrollbars=yes');}</script>");
            //    }
            //}
            //catch
            //{

            //    builder.Append("<script>function Addmap() {window.open('../Content/AddMap.aspx=0', 'newWin', 'modal=yes,width=900,height=500,resizable=yes,scrollbars=yes');}</script>");
            //}
            //return builder.ToString();
        }
        /// <summary>
        /// 智能多文件上传
        /// </summary>
        public string SwfFileUpload(string Alias, string Name, bool IsNotNull, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class='tdbg'><td align='right' class='tdbgleft'><span>" + Alias + "</span></td><td>");
            builder.Append("<table border='0' cellpadding='0' cellspacing='1' width='100%'>");
            builder.Append("<tr class='tdbg'><td style='width:410px;'>");
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\" value=\"" + dr["" + Name + ""].ToString() + "\">");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\">");
                }
            }
            else
            {
                builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" id=\"txt_" + Name + "\">");
            }
            builder.Append("<select name='sel_" + Name + "' id='sel_" + Name + "' style='width: 400px; height: 100px;' ondblclick=\"ModifyUrl(document.form1.sel_" + Name + ",'sel_" + Name + "','txt_" + Name + "')\" size='2'>");
            if (dr != null)
            {
                if (!string.IsNullOrEmpty(dr["" + Name + ""].ToString()))
                {
                    string[] strValue = dr["" + Name + ""].ToString().Split(new char[] { '$' });
                    for (int i = 0; i < strValue.Length; i++)
                    {
                        builder.Append("<option value=\"" + strValue[i] + "\">" + strValue[i] + "</option>");
                    }
                }
            }
            builder.Append("</select></td><td>");
            // builder.Append("<input type=\"button\" class=\"button\" onclick=\"SelectFiles('sel_" + Name + "','txt_" + Name + "')\" value=\"从已上传文件中选择\"> <br/>");
            builder.Append("<input type=\"button\" class=\"button\" onclick=\"AddPhotoUrl('sel_" + Name + "','txt_" + Name + "')\" value=\"添加外部地址\"><br/>");
            builder.Append("<input type=\"button\" class=\"button\" value=\"修改当前地址\" onclick=\"return ModifyPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" /><br />");
            builder.Append("<input type=\"button\" class=\"button\" value=\"删除当前地址\" onclick=\"DelPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" />");
            builder.Append("</td><tr class='tdbg'><td style='height:150px' colspan='2'><iframe id='Upload_" + Name + "' src='/Plugins/swfFileUpload/Uploadify.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "&content=" + Content + "' marginheight='0' marginwidth='0' frameborder='0' width='100%' height='100%' scrolling='no'></iframe></td></tr></table>");
            builder.Append("</td></tr>");
            return builder.ToString();
        }
    }
}
using System;
using System.Data;
using ZoomLa.Common;
using System.Text;
namespace ZoomLa.BLL
{
    /// <summary>
    /// 前台用户 添加内容用的显示模型字段输入
    /// </summary>
    public class B_ContentField
    {
        //日期时间
        public string GetDateType(string Alias, string Name, bool IsNotNull, string Description, DataRow dr)
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
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"18\" onclick=\"setday(this);\" readonly=\"true\" value=\"\"> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>");
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"18\"  onclick=\"setday(this);\" readonly=\"true\" value=\"" + dr["" + Name + ""].ToString() + "\"> " + str2 + " " + Description + "");
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
                    str = str + "<input name=\"txt_" + FileSizeField + "\" type=\"text\" class=\"inputtext\" />&nbsp;K</td>\r\n</tr>";
                }
                else
                {
                    str = str + "<input name=\"txt_" + FileSizeField + "\" type=\"text\" class=\"inputtext\" value=\"" + dr["" + FileSizeField + ""].ToString() + "\" />&nbsp;K</td>\r\n</tr>";
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
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" value=\"" + dr["" + Name + ""].ToString() + "\">");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\">");
                }
            }
            else
            {
                builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\">");
            }

            builder.Append("<select name=\"sel_" + Name + "\" style=\"width: 400px; height: 100px\" size=\"2\">");
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
            builder.Append("<input type=\"button\" class=\"button\" onclick=\"AddSoftUrl('sel_" + Name + "','txt_" + Name + "')\" value=\"添加外部地址\"><br/>");
            builder.Append("<input type=\"button\" class=\"button\" value=\"修改当前地址\" onclick=\"return ModifyPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" /><br />");
            builder.Append("<input type=\"button\" class=\"button\" value=\"删除当前地址\" onclick=\"DelPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" />");
            builder.Append("</td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>");

            builder.Append(str);
            return builder.ToString();
        }
        //多选项
        public string GetListBoxType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            string[] strArray4;
            string str = "";
            string[] strArray2 = Content.Split(new char[] { ',' })[0].Split(new char[] { '=' });
            string[] strArray3 = strArray2[1].Split(new char[] { '|' });
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
                        str = "";
                        for (int i = 0; i < strArray3.Length; i++)
                        {
                            bool flag = false;
                            if (dr == null)
                            {
                                object obj2 = str;
                                str = string.Concat(new object[] { obj2, "<input id=\"txt_", Name, "_", i, "\" type=\"checkbox\" name=\"txt_", Name, "\" value=\"", strArray3[i], "\" />", strArray3[i], "" });
                            }
                            else
                            {
                                strArray4 = dr["" + Name + ""].ToString().Split(new char[] { ',' });
                                for (int j = 0; j < strArray4.Length; j++)
                                {
                                    if (strArray3[i].Trim() == strArray4[j])
                                    {
                                        object obj3 = str;
                                        str = string.Concat(new object[] { obj3, "<input id=\"txt_", Name, "_", i, "\" type=\"checkbox\" name=\"txt_", Name, "\" value=\"", strArray3[i], "\" checked />", strArray3[i], "" });
                                        flag = true;
                                    }
                                }
                                if (!flag)
                                {
                                    object obj4 = str;
                                    str = string.Concat(new object[] { obj4, "<input id=\"txt_", Name, "_", i, "\" type=\"checkbox\" name=\"txt_", Name, "\" value=\"", strArray3[i], "\" />", strArray3[i], "" });
                                }
                            }
                        }
                        string str4 = str;
                        builder.Append(str4 + "" + str2 + " " + Description + "");
                        builder.Append("</td>\r\n</tr>\r\n");
                        return builder.ToString();
                    }
                case "2":
                    {
                        str = "<select size=\"4\" name=\"txt_" + Name + "\"  style=\"width:300px;height:126px\" multiple>";
                        for (int k = 0; k < strArray3.Length; k++)
                        {
                            bool flag2 = false;
                            if (dr == null)
                            {
                                string str5 = str;
                                str = str5 + "<option value=\"" + strArray3[k] + "\">" + strArray3[k] + "</option>";
                            }
                            else
                            {
                                strArray4 = dr["" + Name + ""].ToString().Split(new char[] { ',' });
                                for (int m = 0; m < strArray4.Length; m++)
                                {
                                    if (strArray3[k].Trim() == strArray4[m])
                                    {
                                        string str6 = str;
                                        str = str6 + "<option value=\"" + strArray3[k] + " \" selected>" + strArray3[k] + "</option>";
                                        flag2 = true;
                                    }
                                }
                                if (!flag2)
                                {
                                    string str7 = str;
                                    str = str7 + "<option value=\"" + strArray3[k] + "\">" + strArray3[k] + "</option>";
                                }
                            }
                        }
                        string str8 = str;
                        builder.Append(str8 + "</select> " + str2 + " " + Description + "");
                        builder.Append("</td>\r\n</tr>\r\n");
                        return builder.ToString();
                    }
            }
            return "";
        }
        //多行文本支持Html
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
                        builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_3.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                        builder.Append("</td>\r\n</tr>\r\n");
                        return builder.ToString();
                    }
                    builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_3.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                    builder.Append("</td>\r\n</tr>\r\n");
                    return builder.ToString();
                }
            }
            else
            {
                if (dr == null)
                {
                    builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                    builder.Append("</td>\r\n</tr>\r\n");
                    return builder.ToString();
                }
                builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            if (dr == null)
            {
                builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " + str2 + " " + Description + "");
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
                builder.Append("<textarea name=\"txt_" + Name + "\" style=\"height:" + strArray3[1] + "px;width:" + strArray2[1] + "px;\"></textarea> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<textarea name=\"txt_" + Name + "\" style=\"height:" + strArray3[1] + "px;width:" + strArray2[1] + "px;\">" + function.Decode(dr["" + Name + ""].ToString()) + "</textarea> " + str2 + " " + Description + "");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //数字
        public string GetNumberType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[2].Split(new char[] { '=' });
            string str2 = "";
            if (IsNotNull)
            {
                str2 = "<font color=\"red\">*</font>";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray3[1] + "\"> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"txt\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"> " + str2 + " " + Description + "");
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
            builder.Append("<table border='0' cellpadding='0' cellspacing='1' width='100%'>\r\n");
            builder.Append("<tr class='tdbg'>\r\n");
            builder.Append("<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"35\">" + str2 + " " + Description + "</td>\r\n</tr>");
                builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"35\" value=\"" + dr["" + Name + ""].ToString() + "\">" + str2 + " " + Description + "</td>\r\n</tr>");
            builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "&NodeId=" + NodeID + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        //单选项
        public string GetRadioType(string Alias, string Name, bool IsNotNull, string Content, string Description, DataRow dr)
        {
            string str = "";
            string[] strArray2 = Content.Split(new char[] { ',' })[0].Split(new char[] { '=' });
            string[] strArray3 = strArray2[1].Split(new char[] { '|' });
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
                        string str7 = str;
                        builder.Append(str7 + "</select> " + str2 + " " + Description + "");
                        builder.Append("</td>\r\n</tr>\r\n");
                        return builder.ToString();
                    }
                case "2":
                    {
                        str = "";
                        for (int j = 0; j < strArray3.Length; j++)
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
                        string str8 = str;
                        builder.Append(str8 + "" + str2 + " " + Description + "");
                        builder.Append("</td>\r\n</tr>\r\n");
                        return builder.ToString();
                    }
            }
            return "";
        }
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
                builder.Append("<input type=\"" + strArray3[1] + "\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray4[1] + "\"> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"" + strArray3[1] + "\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"> " + str2 + " " + Description + "");
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
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray3[1] + "\"> " + str2 + " " + Description + "");
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"> " + str2 + " " + Description + "");
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
                str = "<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray4[1] + "\"> " + str2 + " " + Description + "<br/>";
                str = str + str3;
                builder.Append(str);
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            str = "<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"> " + str2 + " " + Description + "<br/>";
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
                    str = str + "<input name=\"txt_" + ThumbField + "\" type=\"text\" class=\"inputtext\" style=\"width: 400px;\" />&nbsp;</td>\r\n</tr>";
                }
                else
                {
                    str = str + "<input name=\"txt_" + ThumbField + "\" type=\"text\" class=\"inputtext\" style=\"width: 400px;\" value=\"" + dr["" + ThumbField + ""] + "\" />&nbsp;</td>\r\n</tr>";
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
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" value=\"" + dr["" + Name + ""].ToString() + "\">");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\">");
                }
            }
            else
            {
                builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\">");
            }
            builder.Append("<select name=\"sel_" + Name + "\" style=\"width: 400px; height: 100px\" size=\"2\">");
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
            
            builder.Append("<input type=\"button\" class=\"button\" onclick=\"AddPhotoUrl('sel_" + Name + "','txt_" + Name + "')\" value=\"添加外部地址\"><br/>");
            builder.Append("<input type=\"button\" class=\"button\" value=\"修改当前地址\" onclick=\"return ModifyPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" /><br />");
            builder.Append("<input type=\"button\" class=\"button\" value=\"删除当前地址\" onclick=\"DelPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" />");
            builder.Append("</td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>");

            builder.Append(str1);
            return builder.ToString();
        }

        public string ShowStyleField(string Alias, string Name, bool IsNotNull, string Type, string Content, string Description, int ModelID, int NodeID, DataRow dr)
        {
            switch (Type)
            {
                case "TextType":
                    return this.GetTextType(Alias, Name, IsNotNull, Content, Description, dr);

                case "ListBoxType":
                    return this.GetListBoxType(Alias, Name, IsNotNull, Content, Description, dr);

                case "DateType":
                    return this.GetDateType(Alias, Name, IsNotNull, Description, dr);

                case "MultipleTextType":
                    return this.GetMultipleTextType(Alias, Name, IsNotNull, Content, Description, dr);

                case "MultipleHtmlType":
                    return this.GetMultipleHtmlType(Alias, Name, IsNotNull, Content, Description, dr);

                case "PicType":
                    return this.GetPicType(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);

                case "OptionType":
                    return this.GetRadioType(Alias, Name, IsNotNull, Content, Description, dr);

                case "FileType":
                    return this.GetFileType(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);

                case "NumType":
                    return this.GetNumberType(Alias, Name, IsNotNull, Content, Description, dr);

                case "MultiPicType":
                    return this.GetMultPicType(Alias, Name, IsNotNull, Content, Description, ModelID, NodeID, dr);

                case "OperatingType":
                    return this.GetOperatingType(Alias, Name, IsNotNull, Content, Description, dr);

                case "SuperLinkType":
                    return this.GetSuperLinkType(Alias, Name, IsNotNull, Content, Description, dr);
            }
            return "";
        }
    }
}
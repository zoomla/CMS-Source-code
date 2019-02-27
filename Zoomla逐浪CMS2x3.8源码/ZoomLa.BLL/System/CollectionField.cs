using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Common;
using System.Data;

namespace ZoomLa.BLL
{
    class CollectionField
    {
        /// <summary>
        /// 给用户模型的某个字段生成编辑代码
        /// </summary>
        /// <param name="Alias">字段别名</param>
        /// <param name="Name">字段名称</param>
        /// <param name="IsNotNull">是否必填</param>
        /// <param name="Type">字段类型</param>
        /// <param name="Content">字段设定内容</param>
        /// <param name="Description">字段描述</param>
        /// <param name="ModelID">模型ID</param>
        /// <param name="UserData">用户模型信息表的一行记录</param>
        /// <returns>编辑页面html代码</returns>
        public string ShowStyleField(string Alias, string Name,  string Type, string Content, string Description, int ModelID,  DataRow dr)
        {
            switch (Type)
            {
                //单行文本
                case "TextType":
                    return this.GetTextType(Alias, Name, Content, Description, dr);
                //多选项
                case "ListBoxType":
                    return this.GetListBoxType(Alias, Name, Content, Description, dr);
                // 日期时间
                case "DateType":
                    return this.GetDateType(Alias, Name, Description, dr);
                //多行文本不支持Html
                case "MultipleTextType":
                    return this.GetMultipleTextType(Alias, Name, Content, Description, dr);
                //多行文本支持Html
                case "MultipleHtmlType":
                    return this.GetMultipleTextType(Alias, Name, Content, Description, dr);
                //单个图片
                case "PicType":
                    return this.GetPicType(Alias, Name, Content, Description, ModelID, dr);
                //单选项
                case "OptionType":
                    return this.GetRadioType(Alias, Name, Content, Description, dr);
                //文件
                case "FileType":
                    return this.GetFileType(Alias, Name, Content, Description, ModelID,dr);
                //数字
                case "NumType":
                    return this.GetNumberType(Alias, Name, Content, Description, dr);
                //多图片
                case "MultiPicType":
                    return this.GetMultPicType(Alias, Name, Content, Description, ModelID, dr);
                //运行平台
                case "OperatingType":
                    return this.GetOperatingType(Alias, Name, Content, Description, dr);
                //超链接
                case "SuperLinkType":
                    return this.GetSuperLinkType(Alias, Name, Content, Description, dr);
                // 多级选项 
                case "GradeOptionType":
                    return this.GetGradeOption(Alias, Name, Content, Description, dr);
                //颜色代码 
                case "ColorType":
                    return this.GetColorType(Alias, Name, Content, Description);
            }
            return "";
        }

        #region 日期时间
        /// <summary>
        /// 日期时间
        /// </summary>
        /// <param name="Alias">字段别名</param>
        /// <param name="Name">字段名称</param>
        /// <param name="IsNotNull">是否必填</param>
        /// <param name="Description">字段描述</param>
        /// <param name="dr"></param>
        /// <returns></returns>
        
        public string GetDateType(string Alias, string Name,  string Description, DataRow dr)
        {
            StringBuilder builder = new StringBuilder();

            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\"  style=\"width:200px;\" class=\"form-control\" onclick=\"setday(this);\" readonly=\"true\" value=\"\"> ");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\"  style=\"width:200px;\" class=\"form-control\"  onclick=\"setday(this);\" readonly=\"true\" value=\"" + dr["" + Name + ""].ToString() + "\"> ");
            return builder.ToString();
        }
        #endregion

        #region 文件
        /// <summary>
        /// 文件
        /// </summary>
        ///  <param name="Alias">字段别名</param>
        /// <param name="Name">字段名称</param>
        /// <param name="IsNotNull">是否必填</param>
        /// <param name="Description">字段描述</param>
        /// <param name="Content">字段设定内容</param>
        /// <param name="ModelID">模型ID</param>
        /// <param name="dr"></param>
        /// <returns></returns>
        
        public string GetFileType(string Alias, string Name,  string Content, string Description, int ModelID, DataRow dr)
        {
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
            str = str + "<iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>";

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
            builder.Append("</select></td>\r\n<td>");
            builder.Append("<input type=\"button\" class=\"button\" onclick=\"AddSoftUrl('sel_" + Name + "','txt_" + Name + "')\" value=\"添加外部地址\"><br/>");
            builder.Append("<input type=\"button\" class=\"button\" value=\"修改当前地址\" onclick=\"return ModifyPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" /><br />");
            builder.Append("<input type=\"button\" class=\"button\" value=\"删除当前地址\" onclick=\"DelPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" />");
            builder.Append("</td>\r\n</tr>\r\n</table>");

            builder.Append(str);
            return builder.ToString();
        }
        #endregion

        #region 多选项
        /// <summary>
        /// 多选项
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Name"></param>
        /// <param name="IsNotNull"></param>
        /// <param name="Content"></param>
        /// <param name="Description"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        //多选项
        public string GetListBoxType(string Alias, string Name,  string Content, string Description, DataRow dr)
        {
            string[] strArray4;
            string str = "";
            string[] strArray2 = Content.Split(new char[] { ',' })[0].Split(new char[] { '=' });
            string[] strArray3 = SortStr(strArray2[1]).Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder builder = new StringBuilder();
            switch (strArray2[0])
            {
                case "1":
                    {
                        str = "";
                        for (int i = 0; i < strArray3.Length; i++)
                        {
                            if (strArray3[i].Contains("|"))
                            {
                                string[] strx = strArray3[i].Split(new char[] { '|' });
                                bool flag = false;
                                if (dr == null)
                                {
                                    object obj2 = str;
                                    str = string.Concat(new object[] { obj2, "<input id=\"txt_", Name, "_", i, "\" type=\"checkbox\" name=\"txt_", Name, "\" value=\"", strx[1], "\" />", strx[0], "" });
                                }
                                else
                                {
                                    strArray4 = dr["" + Name + ""].ToString().Split(new char[] { ',' });
                                    for (int j = 0; j < strArray4.Length; j++)
                                    {
                                        if (strArray3[i].Trim() == strArray4[j])
                                        {
                                            object obj3 = str;
                                            str = string.Concat(new object[] { obj3, "<input id=\"txt_", Name, "_", i, "\" type=\"checkbox\" name=\"txt_", Name, "\" value=\"", strx[1], "\" checked />", strx[0], "" });
                                            flag = true;
                                        }
                                    }
                                    if (!flag)
                                    {
                                        object obj4 = str;
                                        str = string.Concat(new object[] { obj4, "<input id=\"txt_", Name, "_", i, "\" type=\"checkbox\" name=\"txt_", Name, "\" value=\"", strx[1], "\" />", strx[0], "" });
                                    }
                                }
                            }
                            else
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
                        }
                        string str4 = str;
                        builder.Append(str4 );
                        return builder.ToString();
                    }
                case "2":
                    {
                        str = "<select size=\"4\" name=\"txt_" + Name + "\"  style=\"width:300px;height:126px\" multiple>";
                        for (int k = 0; k < strArray3.Length; k++)
                        {
                            if (strArray3[k].Contains("|"))
                            {
                                string[] strx = strArray3[k].Split(new char[] { '|' });
                                bool flag2 = false;
                                if (dr == null)
                                {
                                    string str5 = str;
                                    str = str5 + "<option value=\"" + strx[0] + "\">" + strx[1] + "</option>";
                                }
                                else
                                {
                                    strArray4 = dr["" + Name + ""].ToString().Split(new char[] { ',' });
                                    for (int m = 0; m < strArray4.Length; m++)
                                    {
                                        if (strArray3[k].Trim() == strArray4[m])
                                        {
                                            string str6 = str;
                                            str = str6 + "<option value=\"" + strx[0] + " \" selected>" + strx[1] + "</option>";
                                            flag2 = true;
                                        }
                                    }
                                    if (!flag2)
                                    {
                                        string str7 = str;
                                        str = str7 + "<option value=\"" + strx[0] + "\">" + strx[1] + "</option>";
                                    }
                                }
                            }
                            else
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
                        }
                        string str8 = str;
                        builder.Append(str8 + "</select> " );
                        return builder.ToString();
                    }
            }
            return "";
        }
        #endregion

        #region 多行文本不支持Html
        //多行文本不支持Html
        public string GetMultipleTextType(string Alias, string Name,  string Content, string Description, DataRow dr)
        {
            StringBuilder builder = new StringBuilder();
            if (dr == null)
            {
                builder.Append("<input type='text' name=\"txt_" + Name + "\" style=\"width:200px;\" class=\"form-control\"/> ");
                return builder.ToString();
            }
            builder.Append("<input type='text' name=\"txt_" + Name + "\" style=\"width:200px;\" class=\"form-control\" value=\"" + function.Decode(dr["" + Name + ""].ToString()) + "\" />");
            return builder.ToString();
        }
        #endregion

        #region 多行文本支持Html
        /// <summary>
        /// 多行文本支持Html
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Name"></param>
        /// <param name="IsNotNull"></param>
        /// <param name="Content"></param>
        /// <param name="Description"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string GetMultipleHtmlType(string Alias, string Name,  string Content, string Description, DataRow dr)
        {
            string str = "";
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string[] strArray4 = strArray[2].Split(new char[] { '=' });

            StringBuilder builder = new StringBuilder();
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
                        builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_3.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> ");
                        return builder.ToString();
                    }
                    builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_3.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " );
                    return builder.ToString();
                }
            }
            else
            {
                if (dr == null)
                {
                    builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> " );
                    return builder.ToString();
                }
                builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_1.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> ");
                return builder.ToString();
            }
            if (dr == null)
            {
                builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> ");
                return builder.ToString();
            }
            builder.Append("<input type=\"hidden\" id=\"txt_" + Name + "\" name=\"txt_" + Name + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"><input type=\"hidden\" id=\"txt_" + Name + "___Config\" value=\"\"><iframe id=\"txt_" + Name + "___Frame\" src=\"" + function.ApplicationRootPath + "/editor/fckeditor_2.html?InstanceName=txt_" + Name + "&Toolbar=Default\" width=\"" + strArray2[1] + "px\" height=\"" + strArray3[1] + "px\" frameborder=\"no\" scrolling=\"no\"></iframe> ");
            return builder.ToString();
        }
        #endregion


        #region 数字
        /// <summary>
        /// 数字
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Name"></param>
        /// <param name="IsNotNull"></param>
        /// <param name="Content"></param>
        /// <param name="Description"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string GetNumberType(string Alias, string Name,  string Content, string Description, DataRow dr)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[2].Split(new char[] { '=' });
            StringBuilder builder = new StringBuilder();
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\"  style=\"width:200px;\" class=\"form-control\" value=\"" + strArray3[1] + "\"> ");
                return builder.ToString();
            }
            builder.Append("<input type=\"txt\" name=\"txt_" + Name + "\"  style=\"width:200px;\" class=\"form-control\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"> ");
            return builder.ToString();
        }
        #endregion

        #region 单个图片
        /// <summary>
        /// 单个图片
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Name"></param>
        /// <param name="IsNotNull"></param>
        /// <param name="Content"></param>
        /// <param name="Description"></param>
        /// <param name="ModelID"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string GetPicType(string Alias, string Name,  string Content, string Description, int ModelID, DataRow dr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table border='0' cellpadding='0' cellspacing='1' width='100%'>\r\n");
            builder.Append("<tr class='tdbg'>\r\n");
            builder.Append("<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"35\"></td>\r\n</tr>");
                builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"35\" value=\"" + dr["" + Name + ""].ToString() + "\"></td>\r\n</tr>");
            builder.Append("<tr class='tdbg'>\r\n<td><iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/FileUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe></td>\r\n</tr>\r\n</table>");
            return builder.ToString();
        }
        #endregion

        #region 单选项
        /// <summary>
        /// 单选项
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Name"></param>
        /// <param name="IsNotNull"></param>
        /// <param name="Content"></param>
        /// <param name="Description"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string GetRadioType(string Alias, string Name,  string Content, string Description, DataRow dr)
        {
            string str = "";

            string[] strArray2 = Content.Split(new char[] { ',' })[0].Split(new char[] { '=' });
            string[] strArray3 = SortStr(strArray2[1]).Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            
            StringBuilder builder = new StringBuilder();
            switch (strArray2[0])
            {
                case "1":
                    {
                        str = "<select name=\"txt_" + Name + "\" style=\"width:200px;\" class=\"form-control\">";
                        for (int i = 0; i < strArray3.Length; i++)
                        {
                            if (strArray3[i].Contains("|"))
                            {
                                string[] strx = strArray3[i].Split(new char[] { '|' });
                                if (dr == null)
                                {
                                    string str4 = str;
                                    str = str4 + "<option value=\"" + strx[1] + "\">" + strx[0] + "</option>";
                                }
                                else
                                    if (strx[1] == dr["" + Name + ""].ToString())
                                {
                                    string str5 = str;
                                    str = str5 + "<option value=\"" + strx[1] + "\" selected=\"selected\">" + strx[0] + "</option>";
                                }
                                else
                                {
                                    string str6 = str;
                                    str = str6 + "<option value=\"" + strx[1] + "\">" + strx[0] + "</option>";
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
                        builder.Append(str7 + "</select> ");
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
                                if (dr == null)
                                {
                                    if (j == 0)
                                    {
                                        object obj2 = str;
                                        str = string.Concat(new object[] { obj2, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strx[1], "\" checked />", strx[0], "" });
                                    }
                                    else
                                    {
                                        object obj3 = str;
                                        str = string.Concat(new object[] { obj3, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strx[1], "\" />", strx[0], "" });
                                    }
                                }
                                else if (strArray3[j] == dr["" + Name + ""].ToString())
                                {
                                    object obj4 = str;
                                    str = string.Concat(new object[] { obj4, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strx[1], "\" checked />", strx[0], "" });
                                }
                                else
                                {
                                    object obj5 = str;
                                    str = string.Concat(new object[] { obj5, "<input id=\"txt_", Name, "_", j, "\" type=\"radio\" name=\"txt_", Name, "\" value=\"", strx[1], "\" />", strx[0], "" });
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
                        builder.Append(str8 );
                        return builder.ToString();
                    }
            }
            return "";
        }
        #endregion

        #region 单行文本
        /// <summary>
        /// 单行文本
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Name"></param>
        /// <param name="IsNotNull"></param>
        /// <param name="Content"></param>
        /// <param name="Description"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string GetTextType(string Alias, string Name,  string Content, string Description, DataRow dr)
        {
            try
            {
                string type = "text", value = "";
                string[] strArray = Content.Split(new char[] { ',' });
                if(strArray.Length>2&&strArray[1].Contains("="))
                    type = strArray[1].Split('=')[1];
                  if(strArray.Length>3&&strArray[2].Contains("="))
                      value = strArray[2].Split('=')[1];
                StringBuilder builder = new StringBuilder();
                if (dr == null)
                {
                    builder.Append("<input type=\"" + type + "\" name=\"txt_" + Name + "\" class=\"form-control\" style=\"width:200px;\" value=\"" + value + "\"> ");
                    return builder.ToString();
                }
                builder.Append("<input type=\"" + type + "\" name=\"txt_" + Name + "\" class=\"form-control\" style=\"width:200px;\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"> ");
                return builder.ToString();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message + ":" + Alias + ":" + Name + ":" + Content + ":" + Description);
            }
        }
        #endregion

        #region 超链接
        /// <summary>
        /// 超链接
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Name"></param>
        /// <param name="IsNotNull"></param>
        /// <param name="Content"></param>
        /// <param name="Description"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetSuperLinkType(string Alias, string Name,  string Content, string Description, DataRow dr)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });

            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray3[1] + "\"> " );
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"> " );
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        #endregion

        #region 运行平台
        /// <summary>
        /// 运行平台
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Name"></param>
        /// <param name="IsNotNull"></param>
        /// <param name="Content"></param>
        /// <param name="Description"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetOperatingType(string Alias, string Name,  string Content, string Description, DataRow dr)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[0].Split(new char[] { '=' });
            string[] strArray3 = strArray[1].Split(new char[] { '=' });
            string[] strArray4 = strArray[2].Split(new char[] { '=' });
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

            StringBuilder builder = new StringBuilder();
            builder.Append("<tr class=\"tdbg\">\r\n<td align=\"right\" class=\"tdbgleft\">" + Alias + "</td>\r\n<td>\r\n");
            if (dr == null)
            {
                str = "<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + strArray4[1] + "\"><br/>";
                str = str + str3;
                builder.Append(str);
                builder.Append("</td>\r\n</tr>\r\n");
                return builder.ToString();
            }
            str = "<input type=\"text\" name=\"txt_" + Name + "\" size=\"" + strArray2[1] + "\" value=\"" + function.HtmlEncode(dr["" + Name + ""].ToString()) + "\"><br/>";
            str = str + str3;
            builder.Append(str);
            builder.Append("</td>\r\n</tr>\r\n");
            return builder.ToString();
        }
        #endregion

        #region 多图片
        /// <summary>
        /// 多图片
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Name"></param>
        /// <param name="IsNotNull"></param>
        /// <param name="Content"></param>
        /// <param name="Description"></param>
        /// <param name="ModelID"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetMultPicType(string Alias, string Name,  string Content, string Description, int ModelID, DataRow dr)
        {
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
            str1 = str1 + "<iframe id=\"Upload_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiPicUpload.aspx?ModelID=" + ModelID + "&FieldName=" + Name + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"165\" scrolling=\"no\"></iframe></td>\r\n</tr>";

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
            builder.Append("</select></td>\r\n<td>");

            builder.Append("<input type=\"button\" class=\"button\" onclick=\"AddPhotoUrl('sel_" + Name + "','txt_" + Name + "')\" value=\"添加外部地址\"><br/>");
            builder.Append("<input type=\"button\" class=\"button\" value=\"修改当前地址\" onclick=\"return ModifyPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" /><br />");
            builder.Append("<input type=\"button\" class=\"button\" value=\"删除当前地址\" onclick=\"DelPhotoUrl('sel_" + Name + "','txt_" + Name + "');\" />");
            builder.Append("</td>\r\n</tr>\r\n</table>\r\n</td>\r\n</tr>");

            builder.Append(str1);
            return builder.ToString();
        }
        #endregion
 
        #region  多级选项
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
        private string GetGradeOption(string Alias, string Name,  string Content, string Description, DataRow dr)
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
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\" value=\"" + dr["" + Name + ""].ToString() + "\">");
                    builder.Append("<iframe id=\"Drop_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiDropList.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=" + dr["" + Name + ""].ToString() + "\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe>");
                }
                else
                {
                    builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\">");
                    builder.Append("<iframe id=\"Drop_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiDropList.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe>");
                }
            }
            else
            {
                builder.Append("<input type=\"hidden\" name=\"txt_" + Name + "\">");
                builder.Append("<iframe id=\"Drop_" + Name + "\" src=\"" + function.ApplicationRootPath + "/Common/MultiDropList.aspx?CateID=" + CateID + "&FieldName=" + Name + "&FValue=\" marginheight=\"0\" marginwidth=\"0\" frameborder=\"0\" width=\"100%\" height=\"30\" scrolling=\"no\"></iframe>");
            }

            builder.Append("</td>\r\n</tr>\r\n");

            return builder.ToString();
        }
        #endregion

        #region 颜色代码
        /// <summary>
        /// 颜色代码 
        /// </summary>
        /// <param name="Alias">字段别名</param>
        /// <param name="Name">字段名称</param>
        /// <param name="IsNotNull">是否必填</param>
        /// <param name="Description">字段描述</param>
        /// <param name="Content">字段设定内容</param>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetColorType(string Alias, string Name,  string Content, string Description)
        {
            string[] strArray = Content.Split(new char[] { ',' });
            string[] strArray2 = strArray[1].Split(new char[] { '=' });
            StringBuilder builder = new StringBuilder();
            string str2 = "";
            builder.Append("<script language=\"JavaScript\" type=\"text/javascript\">\r\n");
            builder.Append("function SelectColor(t, clientId) {\r\n");
            builder.Append("var url = \"../Common/SelectColor.aspx?d=f&t=6\"; \r\n");
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
            builder.Append("<input type=\"text\" name=\"txt_" + Name + "\" maxlength=\"7\"   style=\"width:165px;\"  value=\"" + strArray2[1] + "\"> " + str2);
            builder.Append("<img onclick=\"SelectColor(this,'txt_" + Name + "');\" src=\"/App_Themes/AdminDefaultTheme/images/selectclolor.gif\" align=\"absmiddle\" style=\"border-width: 0px; cursor: pointer\" />" + Description + "\r\n");
            return builder.ToString();
        }
        #endregion

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

    }
    
}

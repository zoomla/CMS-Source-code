using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using ZoomLa.BLL;
using System.Xml;


namespace ZoomLaCMS.Manage.Content
{
    public partial class AddJsPic : CustomerPageAction
    {
        protected B_CreateJS Jll = new B_CreateJS();
        protected B_Node Nll = new B_Node();
        protected int pcount = 0;
        protected int scount = 0;
        protected B_Admin mll = new B_Admin();
        protected B_Content cll = new B_Content();
        /// <summary>
        /// 系统页面载入时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            mll.CheckIsLogin();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a  href=\"CreateHtmlContent.aspx\">生成管理</a></li><li class=\"active\">添加新JS</li>");
            int id = DataConverter.CLng(Request.QueryString["id"]);
            M_CreateJS jinfo = Jll.GetSelect(id);
            if (jinfo.id > 0)
            {
                if (jinfo.JsType != 1)
                {
                    function.WriteErrMsg("JS信息分类读取错误！");
                }
            }

            if (!IsPostBack)
            {
                ReadNodeList();
                ListItem itemdd = new ListItem();
                itemdd.Value = "";
                itemdd.Text = "所有栏目";
                this.ClassID.Items.Insert(0, itemdd);
                ReadSpecInfo();
                if (jinfo.id > 0)
                {
                    this.JsFileName.Text = jinfo.JsFileName;
                    this.Jsname.Text = jinfo.Jsname;
                    this.JsReadme.Text = jinfo.JsReadme;
                    this.JsFileName.Text = jinfo.JsFileName;
                    string jsxml = jinfo.JsXmlContent;
                    ContentType.SelectedIndex = jinfo.ContentType;
                    XmlDocument xmldoc = new XmlDocument();
                    jsxml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><doc>" + jsxml + "</doc>";
                    xmldoc.LoadXml(jsxml);
                    PageInit(xmldoc);
                    JSID_Hid.Value = jinfo.id.ToString();
                    this.TxtTitle.Text = "编辑JS文件（图片列表方式）";
                }
                else
                {
                    this.TxtTitle.Text = "添加新的JS文件（图片列表方式）";
                    Save_Btn.Text = "添加";
                }
            }
        }

        /// <summary>
        /// 追加页面载入事件
        /// </summary>
        /// <param name="xmldoc"></param>
        private void PageInit(XmlDocument xmldoc)
        {
            this.ClassID.SelectedValue = GetChild(xmldoc, "ClassID");
            this.IncludeChild.Checked = (GetChild(xmldoc, "IncludeChild") == "1") ? true : false;
            this.ArticleNum.Text = GetChild(xmldoc, "ArticleNum");
            this.ContentProperty.SelectedValue = GetChild(xmldoc, "ContentProperty");
            this.DateNum.Text = GetChild(xmldoc, "DateNum");
            this.OrderType.SelectedValue = GetChild(xmldoc, "OrderType");
            this.ShowType.SelectedValue = GetChild(xmldoc, "ShowType");
            this.ImgWidth.Text = GetChild(xmldoc, "ImgWidth");
            this.ImgHeight.Text = GetChild(xmldoc, "ImgHeight");
            this.TitleLen.Text = GetChild(xmldoc, "TitleLen");
            this.ContentLen.Text = GetChild(xmldoc, "ContentLen");
            this.ShowTips.Checked = (GetChild(xmldoc, "ShowTips") == "1") ? true : false;
            this.Cols.SelectedValue = GetChild(xmldoc, "Cols");
            this.UrlType.SelectedValue = GetChild(xmldoc, "UrlType");
        }

        /// <summary>
        /// 读取节点值
        /// </summary>
        /// <param name="xmldoc"></param>
        /// <param name="SingleName"></param>
        /// <returns></returns>
        protected string GetChild(XmlDocument xmldoc, string SingleName)
        {
            string IncludeChild = xmldoc.SelectSingleNode("//" + SingleName + "").InnerText;
            return IncludeChild;
        }

        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            M_CreateJS Mjs = new M_CreateJS();
            Mjs.JsFileName = this.JsFileName.Text;
            Mjs.Jsname = this.Jsname.Text;
            Mjs.JsReadme = this.JsReadme.Text;
            Mjs.JsFileName = this.JsFileName.Text;
            Mjs.ContentType = ContentType.SelectedIndex;
            Mjs.JsType = 1;

            StringBuilder xmlcontent = new StringBuilder();
            xmlcontent.AppendLine("<ClassID>" + this.ClassID.Text + "</ClassID>");
            xmlcontent.AppendLine("<IncludeChild>" + (this.IncludeChild.Checked ? "1" : "0") + "</IncludeChild>");
            xmlcontent.AppendLine("<ArticleNum>" + ArticleNum.Text + "</ArticleNum>");
            xmlcontent.AppendLine("<ContentProperty>" + ContentProperty.SelectedValue + "</ContentProperty>");
            xmlcontent.AppendLine("<DateNum>" + DateNum.Text + "</DateNum>");
            xmlcontent.AppendLine("<OrderType>" + OrderType.SelectedValue + "</OrderType>");
            xmlcontent.AppendLine("<ShowType>" + ShowType.SelectedValue + "</ShowType>");
            xmlcontent.AppendLine("<ImgWidth>" + ImgWidth.Text + "</ImgWidth>");
            xmlcontent.AppendLine("<ImgHeight>" + ImgHeight.Text + "</ImgHeight>");
            xmlcontent.AppendLine("<TitleLen>" + TitleLen.Text + "</TitleLen>");
            xmlcontent.AppendLine("<ContentLen>" + ContentLen.Text + "</ContentLen>");
            xmlcontent.AppendLine("<ShowTips>" + (ShowTips.Checked ? "1" : "0") + "</ShowTips>");
            xmlcontent.AppendLine("<Cols>" + Cols.SelectedValue + "</Cols>");
            xmlcontent.AppendLine("<UrlType>" + UrlType.SelectedValue + "</UrlType>");
            Mjs.JsXmlContent = xmlcontent.ToString();

            DataTable ContentList = Getlist();
            string picPath = "";  //图片路径
            int Colsnum = 0;//列数

            if (DataConverter.CLng(Cols.Text) > 0)
            {
                Colsnum = DataConverter.CLng(Cols.Text);
            } //每行显示文章数

            string txt_ShowType = ShowType.SelectedValue;
            StringBuilder jscontent = new StringBuilder();
            int ContentNum = 0;
            if (Colsnum > 0)
            {
                ContentNum = ContentList.Rows.Count / Colsnum;
            }
            float ContentMod = 0;

            if (Colsnum > 0)
            {
                ContentMod = ContentList.Rows.Count % Colsnum;
            }

            int ReadCst = ContentList.Rows.Count - (int)ContentMod;
            switch (ShowType.SelectedValue)
            {
                case "1":   //图片+标题：上下排列

                    #region 图片+标题：上下排列
                    jscontent.AppendLine("");
                    jscontent.AppendLine("<table>");

                    for (int c = 0; c < ContentNum; c++)
                    {
                        jscontent.AppendLine("<tr>");

                        for (int i = 0; i < Colsnum; i++)
                        {
                            string title = "";
                            string c_ClassName = "";
                            //string c_Author = "";
                            //string c_Hits = "";
                            string c_HotSign = "";
                            //string c_Tips = "";
                            string c_CommentLink = "";
                            string c_OpenType = "";

                            if (this.ShowTips.Checked)  //显示内容
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[i]["NodeID"]);

                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }

                                title = "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";  //作者
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";   //点击数
                                                                                                      //热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20) //文章大于20则为热门
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>[评论信息]</a>";
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                                //更新时间
                                DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);
                                string showtime = UpDateTime.ToString();
                                title += "更新时间:" + showtime + "&#13;";
                                picPath = GetPicPathByModelID(DataConverter.CLng(ContentList.Rows[c + i]["ModelID"]), DataConverter.CLng(ContentList.Rows[c + i]["ItemID"]));
                            }
                            else
                            {
                                c_ClassName = "";
                            }
                            jscontent.AppendLine("<td>");
                            jscontent.AppendLine("<img title='" + title + "' " + " src='" + GetUrl(UrlType.SelectedValue) + "/UploadFiles" + picPath + "' width='" + ImgWidth.Text + "' height='" + ImgHeight.Text + "' /> <br/>");
                            jscontent.AppendLine("<a title='" + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                            jscontent.AppendLine("</td>");
                        }
                        jscontent.AppendLine("</tr>");
                    }


                    if ((int)ContentMod > 0)
                    {
                        jscontent.AppendLine("<tr>");
                        for (int p = 0; p < (int)ContentMod; p++)
                        {
                            string title = "";
                            string c_HotSign = "";
                            string c_ClassName = "";
                            //string c_Author = "";
                            //string c_Hits = "";
                            //string c_Tips = "";
                            string c_CommentLink = "";
                            //string c_OpenType = "";

                            if (this.ShowTips.Checked)  //显示内容
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[ReadCst + p]["NodeID"]);

                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }

                                title = "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";  //作者
                                title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";   //点击数
                                                                                                                //热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20) //文章大于20则为热门
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                                //更新时间
                                DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);
                                string showtime = UpDateTime.ToString();
                                title += "更新时间:" + showtime + "&#13;";
                            }
                            else
                            {
                                c_ClassName = "";
                            }
                            jscontent.AppendLine("<td>");
                            picPath = GetPicPathByModelID(DataConverter.CLng(ContentList.Rows[ReadCst + p]["ModelID"]), DataConverter.CLng(ContentList.Rows[ReadCst + p]["ItemID"]));
                            jscontent.AppendLine("<img title='" + title + "' " + " src='" + GetUrl(UrlType.SelectedValue) + "/UploadFiles" + picPath + "' width='" + ImgWidth.Text + "' height='" + ImgHeight.Text + "' /><br/>");
                            jscontent.AppendLine("<a title='" + title + "' " + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                            jscontent.AppendLine("</td>");
                        }

                        int Ccount = Colsnum - (int)ContentMod;
                        jscontent.AppendLine("</tr>");
                    }
                    jscontent.AppendLine("</table>");

                    #endregion
                    break;
                case "2":   //图片+标题：左右排列

                    #region 图片+标题：左右排列
                    jscontent.AppendLine("");
                    jscontent.AppendLine("<table>");

                    for (int c = 0; c < ContentNum; c++)
                    {
                        jscontent.AppendLine("<tr>");

                        for (int i = 0; i < Colsnum; i++)
                        {
                            string title = "";
                            string c_ClassName = "";
                            //string c_Author = "";
                            //string c_Hits = "";
                            string c_HotSign = "";
                            //string c_Tips = "";
                            string c_CommentLink = "";
                            string c_OpenType = "";

                            if (this.ShowTips.Checked)  //显示内容
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[i]["NodeID"]);

                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }

                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";  //作者
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";   //点击数
                                                                                                      //热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20) //文章大于20则为热门
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>[评论信息]</a>";
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                                //更新时间
                                DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);
                                string showtime = UpDateTime.ToString();
                                title += "更新时间:" + showtime + "&#13;";
                                picPath = GetPicPathByModelID(DataConverter.CLng(ContentList.Rows[c + i]["ModelID"]), DataConverter.CLng(ContentList.Rows[c + i]["ItemID"]));
                            }
                            else
                            {
                                c_ClassName = "";
                            }
                            jscontent.AppendLine("<td><img title='" + title + "' " + " src='" + GetUrl(UrlType.SelectedValue) + "/UploadFiles" + picPath + "' width='" + ImgWidth.Text + "' height='" + ImgHeight.Text + "' /></td>");
                            jscontent.AppendLine("<td>");
                            jscontent.AppendLine("<a title='" + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                            jscontent.AppendLine("</td>");
                        }
                        jscontent.AppendLine("</tr>");
                    }


                    if ((int)ContentMod > 0)
                    {
                        jscontent.AppendLine("<tr>");
                        for (int p = 0; p < (int)ContentMod; p++)
                        {
                            string title = "";
                            string c_HotSign = "";
                            string c_ClassName = "";
                            //string c_Author = "";
                            //string c_Hits = "";
                            //string c_Tips = "";
                            string c_CommentLink = "";
                            //string c_OpenType = "";

                            if (this.ShowTips.Checked)  //显示内容
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[ReadCst + p]["NodeID"]);

                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }

                                title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";  //作者
                                title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";   //点击数
                                                                                                                //热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20) //文章大于20则为热门
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                                //更新时间
                                DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);
                                string showtime = UpDateTime.ToString();
                                title += "更新时间:" + showtime + "&#13;";
                            }
                            else
                            {
                                c_ClassName = "";
                            }

                            jscontent.AppendLine("<td>");
                            picPath = GetPicPathByModelID(DataConverter.CLng(ContentList.Rows[ReadCst + p]["ModelID"]), DataConverter.CLng(ContentList.Rows[ReadCst + p]["ItemID"]));
                            jscontent.AppendLine("<img title='" + title + "' " + " src='" + GetUrl(UrlType.SelectedValue) + "/UploadFiles" + picPath + "' width='" + ImgWidth.Text + "' height='" + ImgHeight.Text + "' /><br/>");
                            jscontent.AppendLine("<a title='" + title + "' " + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                            jscontent.AppendLine("</td>");
                        }

                        int Ccount = Colsnum - (int)ContentMod;
                        jscontent.AppendLine("</tr>");
                    }
                    jscontent.AppendLine("</table>");

                    #endregion
                    break;
            }

            if (ContentType.SelectedValue == "JS")
            {
                FileSystemObject.WriteFile(Server.MapPath("/JS/Ads/JS/" + this.JsFileName.Text), ConvertJs(jscontent.ToString()));
            }
            else
            {

                FileSystemObject.WriteFile(Server.MapPath("/JS/Ads/HTML/" + this.JsFileName.Text), "<html><head></head><body>" + jscontent.ToString() + "</body></html>");
            }

            if (!string.IsNullOrEmpty(JSID_Hid.Value))
            {
                Mjs.id = DataConverter.CLng(JSID_Hid.Value);
                Jll.GetUpdate(Mjs);
                function.WriteSuccessMsg("修改成功！", "ManageJsContent.aspx");
            }
            else
            {
                Jll.GetInsert(Mjs);
                function.WriteSuccessMsg("添加成功！", "ManageJsContent.aspx");
            }
        }

        protected string ConvertJs(string oSource)
        {
            string oResultvalue = Filter(oSource, "htmltojs");
            oResultvalue = "document.writeln(\"" + oResultvalue + "\");";
            return oResultvalue;
        }

        /// <summary>
        /// 字符过滤
        /// </summary>
        /// <param name="str">传入的字符</param>
        /// <param name="mode">过滤模式</param>
        /// <example>Filter("<b>b</b>>hfghfgh", "HTML")</example>
        /// <returns>过滤后的字符串</returns>
        public static string Filter(string str, string mode)
        {
            mode = mode.ToLower();
            switch (mode)
            {
                case "html":
                    str = str.Replace("\r\n", "\n");
                    str = str.Replace("'", "&#39;");
                    str = str.Replace("\"", "&#34;");
                    str = str.Replace("<", "&#60;");
                    str = str.Replace(">", "&#62;");
                    str = str.Replace("\n", "<br />");
                    break;
                case "nohtml":
                    str = Regex.Replace(str, "<[^>]*>", "");
                    break;
                case "sql1":
                    str = str.Replace("'", "");
                    str = str.Replace(";", "");
                    break;
                case "htmltojs":
                    str = str.Replace("\r\n", "\n");
                    str = str.Replace(@"\", @"\\");
                    str = str.Replace("'", "\\\'");
                    str = str.Replace("\"", "\\\"");
                    str = str.Replace("/", "\\/");
                    str = str.Replace("\n", " ");
                    //str = str.Replace(" ", "&nbsp;");
                    break;
                default:
                    str = str.Replace("'", "''");
                    str = str.Replace(";", "；");
                    break;
            }
            return str;
        }


        /// <summary>
        /// 通过ModelID ,itemID查找图片路径
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="itemID"></param>
        /// <returns></returns>
        private string GetPicPathByModelID(int modelID, int itemID)
        {
            B_Model bm = new B_Model();
            M_ModelInfo modelinfo = bm.GetModelById(modelID);
            B_ModelField bmf = new B_ModelField();
            M_ModelField mf = bmf.GetFieldNameByMID(modelID);
            string fieldName = "";
            if (mf != null && mf.ModelID > 0)
            {
                fieldName = mf.FieldName;
            }
            if (modelinfo != null && modelinfo.ModelID > 0)
            {
                //DataSet ds = cll.PRoContentChilds(modelinfo.TableName, itemID);
                //if (ds != null && ds.Tables[0].Rows.Count > 0)
                //{
                //    DataRow dr = ds.Tables[0].Rows[0];
                //    foreach (DataColumn item in ds.Tables[0].Columns)
                //    {
                //        if (item.ToString().Trim().Equals(fieldName))
                //        {
                //            return dr[item].ToString();
                //        }
                //    }
                //}
            }
            return "";
        }

        /// <summary>
        /// 连接路径问题：绝对路径和相对路径
        /// </summary>
        /// <param name="type"></param>
        /// <param name="generalID"></param>
        /// <returns></returns>
        private string GetUrl(string type)
        {
            if (type == null || type == "" || type == "Relative") //相对路径
            {
                return "";
            }
            else if (type == "Absolute")  //使用绝对路径
            {
                return SiteConfig.SiteInfo.SiteUrl;
            }
            return "";
        }


        /// <summary>
        /// 获得内容列表
        /// </summary>
        private DataTable Getlist()
        {
            string OrderTypevalue = this.OrderType.SelectedValue;
            //string descstr = "";
            //string ShowField = "";
            //switch (OrderTypevalue)
            //{
            //    case "GIDDesc"://内容ID降序
            //        descstr = "desc";
            //        ShowField = "GeneralID";
            //        break;
            //    case "GID"://升序
            //        ShowField = "GeneralID";
            //        break;
            //    case "UpdateDesc"://更新时间降序
            //        descstr = "desc";
            //        ShowField = "UpDateTime";
            //        break;
            //    case "Update"://更新时间升序
            //        ShowField = "UpDateTime";
            //        break;
            //    case "HitsDesc"://点击数降序
            //        ShowField = "Hits";
            //        descstr = "desc";
            //        break;
            //    case "Hits"://点击数升序
            //        ShowField = "Hits";
            //        break;
            //    default:
            //        break;
            //}

            //string Inputer = "";

            int DateNumvalue = 0;
            if (DataConverter.CLng(this.DateNum.Text) > 0)
            {
                DateNumvalue = DataConverter.CLng(this.DateNum.Text);
            }

            string ContentPropertyvalue = this.ContentProperty.SelectedValue;
            //bool IsHot = false;
            //bool IsElite = false;

            //if (ContentPropertyvalue.IndexOf("IsHot") > -1)
            //{
            //    IsHot = true;
            //}

            //if (ContentPropertyvalue.IndexOf("IsElite") > -1)
            //{
            //    IsElite = true;
            //}
            //DataTable sptable = cll.GetSpecInfoContent(DataConverter.CLng(ArticleNum.Text), DataConverter.CLng(ClassID.SelectedValue), -1, 99, ShowField, descstr, Inputer, DateNumvalue, this.IncludeChild.Checked, IsHot, IsElite);
            return null;
        }

        /// <summary>
        /// 读取专题
        /// </summary>
        protected void ReadSpecInfo()
        {
            //B_SpecCate sill = new B_SpecCate();
            //DataTable tablet = sill.GetCateLidt();
            //for (int i = 0; i < tablet.Rows.Count; i++)
            //{
            //    ListItem item = new ListItem();
            //    item.Value = tablet.Rows[i]["SpecCateID"].ToString();
            //    item.Text = tablet.Rows[i]["SpecCateName"].ToString();
            //    this.SpecialID.Items.Add(item);
            //    ReadSpecInfoParentList(DataConverter.CLng(tablet.Rows[i]["SpecCateID"].ToString()));
            //}
        }

        /// <summary>
        /// 读下一级专题
        /// </summary>
        /// <param name="PerentID"></param>
        protected void ReadSpecInfoParentList(int PerentID)
        {
            //scount = scount + 1;
            //B_Spec pll = new B_Spec();
            //DataTable tbl = pll.GetSpecList(PerentID);
            //for (int ii = 0; ii < tbl.Rows.Count; ii++)
            //{
            //    ListItem ditems = new ListItem();
            //    ditems.Text = new string('　', scount) + tbl.Rows[ii]["SpecName"].ToString();
            //    ditems.Value = tbl.Rows[ii]["SpecID"].ToString();
            //    this.SpecialID.Items.Add(ditems);
            //}
            //scount = scount - 1;
        }

        /// <summary>
        /// 读取节点列表
        /// </summary>
        protected void ReadNodeList()
        {
            DataTable tables = Nll.SelByPid(0);
            for (int i = 0; i < tables.Rows.Count; i++)
            {
                ListItem ditems = new ListItem();
                ditems.Text = tables.Rows[i]["NodeName"].ToString();
                ditems.Value = tables.Rows[i]["NodeID"].ToString();
                this.ClassID.Items.Add(ditems);
                ReadNodeParentList(DataConverter.CLng(tables.Rows[i]["NodeID"].ToString()));
            }
        }

        /// <summary>
        /// 递归加载分类算法
        /// </summary>
        /// <param name="PerentID"></param>
        protected void ReadNodeParentList(int PerentID)
        {
            pcount = pcount + 1;
            DataTable tables = Nll.SelByPid(PerentID);
            for (int i = 0; i < tables.Rows.Count; i++)
            {
                ListItem ditems = new ListItem();
                ditems.Text = new string('　', pcount) + "├ " + tables.Rows[i]["NodeName"].ToString();
                ditems.Value = tables.Rows[i]["NodeID"].ToString();
                this.ClassID.Items.Add(ditems);
                ReadNodeParentList(DataConverter.CLng(tables.Rows[i]["NodeID"].ToString()));
            }
            pcount = pcount - 1;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageJsContent.aspx");
        }
    }
}
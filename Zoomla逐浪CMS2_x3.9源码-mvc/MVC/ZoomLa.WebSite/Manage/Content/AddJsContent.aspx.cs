using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Data;
using System.Text;
using ZoomLa.BLL;
using System.Xml;
using System.Text.RegularExpressions;

namespace ZoomLaCMS.Manage.Content
{
    public partial class AddJsContent : CustomerPageAction
    {
        protected B_CreateJS Jll = new B_CreateJS();
        protected B_Node Nll = new B_Node();
        protected int pcount = 0;
        protected int scount = 0;
        protected B_Admin mll = new B_Admin();
        protected B_Content cll = new B_Content();

        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a  href=\"CreateHtmlContent.aspx\">生成发布</a></li><li class=\"active\">添加新JS</li>");
            int id = DataConverter.CLng(Request.QueryString["id"]);
            M_CreateJS jinfo = Jll.GetSelect(id);
            if (jinfo.id > 0)
            {
                if (jinfo.JsType != 0)
                {
                    function.WriteErrMsg("JS信息分类读取错误！");
                }
            }

            if (!IsPostBack)
            {
                ReadNodeList();
                ReadSpecInfo();
                ListItem itemdd = new ListItem();
                itemdd.Value = "";
                itemdd.Text = "所有栏目";
                this.ClassID.Items.Insert(0, itemdd);
                if (jinfo.id > 0)
                {
                    this.JsFileName.Text = jinfo.JsFileName;
                    this.JsName.Text = jinfo.Jsname;
                    this.JsReadme.Text = jinfo.JsReadme;
                    this.JsFileName.Text = jinfo.JsFileName;
                    string jsxml = jinfo.JsXmlContent;
                    this.ContentType.SelectedIndex = jinfo.ContentType;

                    XmlDocument xmldoc = new XmlDocument();
                    jsxml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><doc>" + jsxml + "</doc>";
                    xmldoc.LoadXml(jsxml);
                    PageInit(xmldoc);
                    JSID_Hid.Value = jinfo.id.ToString();
                    this.TxtTitle.Text = "编辑JS文件（普通列表方式）";
                }
                else
                {
                    this.TxtTitle.Text = "添加新的JS文件（普通列表方式）";
                    Save_Btn.Text = "添加";
                }

            }
        }
        // 追加页面载入事件
        private void PageInit(XmlDocument xmldoc)
        {
            this.ShowType.SelectedValue = GetChild(xmldoc, "ShowType");
            this.Author.Text = GetChild(xmldoc, "Author");
            this.ArticleNum.Text = GetChild(xmldoc, "ArticleNum");
            this.ClassID.SelectedValue = GetChild(xmldoc, "ClassID");
            this.IncludeChild.Checked = GetChild(xmldoc, "IncludeChild") == "1" ? true : false;
            this.DateNum.Text = GetChild(xmldoc, "DateNum");
            this.OrderType.SelectedValue = GetChild(xmldoc, "OrderType");
            this.ShowPropertyType.SelectedValue = GetChild(xmldoc, "ShowPropertyType");
            this.TitleLen.Text = GetChild(xmldoc, "TitleLen");
            this.ContentLen.Text = GetChild(xmldoc, "ContentLen");
            this.ShowClassName.Checked = GetChild(xmldoc, "ShowClassName") == "1" ? true : false;
            this.ShowIncludePic.Checked = GetChild(xmldoc, "ShowIncludePic") == "1" ? true : false;
            this.ShowAuthor.Checked = GetChild(xmldoc, "ShowAuthor") == "1" ? true : false;
            this.ShowDateType.SelectedValue = GetChild(xmldoc, "ShowDateType");
            this.ShowHits.Checked = GetChild(xmldoc, "ShowHits") == "1" ? true : false;
            this.ShowHotSign.Checked = GetChild(xmldoc, "ShowHotSign") == "1" ? true : false;
            this.ShowNewSign.Checked = GetChild(xmldoc, "ShowNewSign") == "1" ? true : false;
            this.ShowTips.Checked = GetChild(xmldoc, "ShowTips") == "1" ? true : false;
            this.ShowHits.Checked = GetChild(xmldoc, "ShowHits") == "1" ? true : false;
            this.OpenType.SelectedValue = GetChild(xmldoc, "OpenType");
            this.UrlType.SelectedValue = GetChild(xmldoc, "UrlType");
            this.Cols.Text = GetChild(xmldoc, "Cols");
            this.ShowCommentLink.Checked = GetChild(xmldoc, "ShowCommentLink") == "1" ? true : false;
            this.ContentProperty.SelectedValue = GetChild(xmldoc, "ContentProperty");
            this.CssNameA.Text = GetChild(xmldoc, "CssNameA");
            this.CssName1.Text = GetChild(xmldoc, "CssName1");
            this.CssName2.Text = GetChild(xmldoc, "CssName2");
        }

        // 读取节点值
        protected string GetChild(XmlDocument xmldoc, string SingleName)
        {
            string IncludeChild = xmldoc.SelectSingleNode("//" + SingleName + "").InnerText;
            return IncludeChild;
        }

        // 操作
        protected void Button1_Click(object sender, EventArgs e)
        {
            M_CreateJS Mjs = new M_CreateJS();
            Mjs.JsFileName = this.JsFileName.Text;
            Mjs.Jsname = this.JsName.Text;
            Mjs.JsReadme = this.JsReadme.Text;
            Mjs.JsFileName = this.JsFileName.Text;
            Mjs.ContentType = this.ContentType.SelectedIndex;
            Mjs.JsType = 0;

            StringBuilder xmlcontent = new StringBuilder();
            xmlcontent.AppendLine("<ShowType>" + ShowType.SelectedValue + "</ShowType>");
            xmlcontent.AppendLine("<Author>" + Author.Text + "</Author>");
            xmlcontent.AppendLine("<ArticleNum>" + ArticleNum.Text + "</ArticleNum>");
            xmlcontent.AppendLine("<ClassID>" + ClassID.SelectedValue + "</ClassID>");
            xmlcontent.AppendLine("<IncludeChild>" + (IncludeChild.Checked ? "1" : "0") + "</IncludeChild>");
            xmlcontent.AppendLine("<ContentProperty>" + ContentProperty.SelectedValue + "</ContentProperty>");
            xmlcontent.AppendLine("<DateNum>" + DateNum.Text + "</DateNum>");
            xmlcontent.AppendLine("<OrderType>" + OrderType.SelectedValue + "</OrderType>");
            xmlcontent.AppendLine("<ShowPropertyType>" + ShowPropertyType.SelectedValue + "</ShowPropertyType>");
            xmlcontent.AppendLine("<TitleLen>" + TitleLen.Text + "</TitleLen>");
            xmlcontent.AppendLine("<ContentLen>" + ContentLen.Text + "</ContentLen>");
            xmlcontent.AppendLine("<ShowClassName>" + (ShowClassName.Checked ? "1" : "0") + "</ShowClassName>");
            xmlcontent.AppendLine("<ShowIncludePic>" + (ShowIncludePic.Checked ? "1" : "0") + "</ShowIncludePic>");
            xmlcontent.AppendLine("<ShowAuthor>" + (ShowAuthor.Checked ? "1" : "0") + "</ShowAuthor>");
            xmlcontent.AppendLine("<ShowDateType>" + ShowDateType.SelectedValue + "</ShowDateType>");
            xmlcontent.AppendLine("<ShowHits>" + ((ShowHits.Checked == true) ? "1" : "0") + "</ShowHits>");
            xmlcontent.AppendLine("<ShowHotSign>" + ((ShowHotSign.Checked == true) ? "1" : "0") + "</ShowHotSign>");
            xmlcontent.AppendLine("<ShowNewSign>" + ((ShowNewSign.Checked == true) ? "1" : "0") + "</ShowNewSign>");
            xmlcontent.AppendLine("<ShowTips>" + ((ShowTips.Checked == true) ? "1" : "0") + "</ShowTips>");
            xmlcontent.AppendLine("<ShowCommentLink>" + ((ShowCommentLink.Checked == true) ? "1" : "0") + "</ShowCommentLink>");
            xmlcontent.AppendLine("<OpenType>" + OpenType.Text + "</OpenType>");
            xmlcontent.AppendLine("<UrlType>" + UrlType.Text + "</UrlType>");
            xmlcontent.AppendLine("<Cols>" + Cols.Text + "</Cols>");
            xmlcontent.AppendLine("<CssNameA>" + CssNameA.Text + "</CssNameA>");
            xmlcontent.AppendLine("<CssName1>" + CssName1.Text + "</CssName1>");
            xmlcontent.AppendLine("<CssName2>" + CssName2.Text + "</CssName2>");
            Mjs.JsXmlContent = xmlcontent.ToString();
            DataTable ContentList = GetList();//内容列表

            int Colsnum = 0;//列数

            if (DataConverter.CLng(Cols.Text) > 0)
            {
                Colsnum = DataConverter.CLng(Cols.Text);
            } //每行显示标题的列数

            string txt_ShowType = ShowType.SelectedValue;
            StringBuilder jscontent = new StringBuilder();
            int ContentNum = 0;
            if (ContentList != null && Colsnum > 0)
            {
                ContentNum = ContentList.Rows.Count / Colsnum;
            }
            float ContentMod = 0;
            if (ContentList != null)
            {
                ContentMod = ContentList.Rows.Count;
            }
            if (ContentList != null && Colsnum > 0)
            {
                ContentMod = ContentList.Rows.Count % Colsnum;
            }

            int ReadCst = 0;
            if (ContentList != null)
            {
                ReadCst = ContentList.Rows.Count - (int)ContentMod;
            }
            switch (txt_ShowType)
            {
                case "0"://DIV输出

                    #region DIV输出
                    jscontent.AppendLine("");
                    for (int c = 0; c < ContentNum; c++)
                    {
                        string stylenames = "";
                        if ((c + 1) % 2 == 0)
                        {
                            stylenames = " style=\"" + CssName2.Text + "\"";//偶数
                        }
                        else
                        {
                            stylenames = " style=\"" + CssName1.Text + "\"";//奇数
                        }
                        jscontent.AppendLine("<div " + stylenames + ">");
                        for (int i = 0; i < Colsnum; i++)
                        {
                            string title = "title='";
                            string c_ClassName = "";
                            //string c_IncludePic = "";
                            string c_Author = "";
                            string c_Hits = "";
                            string c_HotSign = "";
                            //tring c_NewSign = "";
                            //string c_Tips = "";
                            string c_CommentLink = "";
                            string c_OpenType = "";

                            if (ShowClassName.Checked)
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[i]["NodeID"]);

                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }
                            }
                            else
                            {
                                c_ClassName = "";
                            }

                            if (ShowIncludePic.Checked)
                            {
                                //ShowIncludePic “图文”标志
                            }
                            if (ShowAuthor.Checked)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHits.Checked)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSign.Checked)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSign.Checked)
                            {//最新文章标志

                            }

                            if (ShowTips.Checked)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLink.Checked)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }
                            if (this.OpenType.SelectedValue != "")
                            {
                                c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);

                            string showtime = WirteTimeFormat(ref UpDateTime);

                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }

                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);

                            if (i < Colsnum - 1)
                            {
                                jscontent.AppendLine(" ");
                            }
                        }
                        jscontent.AppendLine("</div></br>");
                    }


                    string stylesname = "";

                    if ((ContentNum + 1) % 2 == 0)
                    {
                        stylesname = " style=\"" + CssName2.Text + "\"";
                    }
                    else
                    {
                        stylesname = " style=\"" + CssName1.Text + "\"";
                    }
                    jscontent.AppendLine("<div " + stylesname + ">");
                    for (int p = 0; p < (int)ContentMod; p++)
                    {
                        string title = "title='";
                        string c_ClassName = "";
                        //string c_IncludePic = "";
                        string c_Author = "";
                        string c_Hits = "";
                        string c_HotSign = "";
                        //string c_NewSign = "";
                        //string c_Tips = "";
                        string c_CommentLink = "";
                        string c_OpenType = "";
                        if (ShowClassName.Checked)
                        {
                            //ShowClassName 所属栏目
                            int NodeID = DataConverter.CLng(ContentList.Rows[ReadCst + p]["NodeID"]);
                            if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                            {
                                c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                            }
                        }
                        else
                        {
                            c_ClassName = "";
                        }

                        if (ShowIncludePic.Checked)
                        {
                            //ShowIncludePic “图文”标志
                        }

                        if (ShowAuthor.Checked)
                        {
                            //ShowAuthor 作者
                            c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                            title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                        }

                        if (ShowHits.Checked)
                        {
                            //ShowHits 点击次数
                            c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                            title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                        }

                        if (ShowHotSign.Checked)
                        {//热门文章标志
                            if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20)  //为热门文章
                            {
                                c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                            }
                        }

                        if (ShowNewSign.Checked)
                        {//最新文章标志

                        }

                        if (ShowTips.Checked)
                        {
                            //显示提示信息
                            title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                        }

                        if (ShowCommentLink.Checked)
                        {
                            //显示评论链接
                            c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                        }
                        if (this.OpenType.SelectedValue != "")
                        {
                            c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                        }


                        DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);

                        string showtime = WirteTimeFormat(ref UpDateTime);
                        if (showtime != "")
                        {
                            title += "更新时间:" + showtime + "&#13;";
                        }
                        jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                        jscontent.AppendLine("");
                    }
                    jscontent.AppendLine("</div>");
                    break;
                #endregion
                case "1":
                    #region li列表
                    jscontent.AppendLine("");
                    for (int c = 0; c < ContentNum; c++)
                    {
                        string stylenames = "";
                        if ((c + 1) % 2 == 0)
                        {
                            stylenames = " style=\"" + CssName2.Text + "\"";//偶数
                        }
                        else
                        {
                            stylenames = " style=\"" + CssName1.Text + "\"";//奇数
                        }

                        jscontent.AppendLine("<ul" + stylenames + ">");

                        for (int i = 0; i < Colsnum; i++)
                        {
                            string title = "title='";
                            string c_ClassName = "";
                            string c_Author = "";
                            string c_Hits = "";
                            string c_HotSign = "";
                            //string c_Tips = "";
                            string c_CommentLink = "";
                            string c_OpenType = "";

                            if (ShowClassName.Checked)
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[i]["NodeID"]);
                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }
                            }
                            else
                            {
                                c_ClassName = "";
                            }

                            if (ShowIncludePic.Checked)
                            {
                                //ShowIncludePic “图文”标志
                            }

                            if (ShowAuthor.Checked)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHits.Checked)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSign.Checked)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSign.Checked)
                            {//最新文章标志

                            }

                            if (ShowTips.Checked)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLink.Checked)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (this.OpenType.SelectedValue != "")
                            {
                                c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);
                            string showtime = WirteTimeFormat(ref UpDateTime);

                            string tdstyle = "";
                            if (CssNameA.Text != "")
                            {
                                tdstyle = " style=\"" + CssNameA.Text + "\"";
                            }

                            jscontent.AppendLine("<li" + tdstyle + ">");
                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }
                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                            jscontent.AppendLine("</li>");
                        }
                        jscontent.AppendLine("</ul>");
                    }

                    string stylename = "";

                    if ((ContentNum + 1) % 2 == 0)
                    {
                        stylename = " style=\"" + CssName2.Text + "\"";
                    }
                    else
                    {
                        stylename = " style=\"" + CssName1.Text + "\"";
                    }


                    if ((int)ContentMod > 0)
                    {
                        jscontent.AppendLine("<ul" + stylename + ">");
                        for (int p = 0; p < (int)ContentMod; p++)
                        {
                            string title = "title='";
                            string c_HotSign = "";
                            string c_ClassName = "";
                            string c_Author = "";
                            string c_Hits = "";
                            //string c_Tips = "";
                            string c_CommentLink = "";
                            string c_OpenType = "";

                            if (ShowClassName.Checked)
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[ReadCst + p]["NodeID"]);
                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }
                            }

                            if (ShowIncludePic.Checked)
                            {
                                //ShowIncludePic “图文”标志

                            }
                            if (ShowAuthor.Checked)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHits.Checked)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSign.Checked)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSign.Checked)
                            {//最新文章标志

                            }

                            if (ShowTips.Checked)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLink.Checked)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (this.OpenType.SelectedValue != "")
                            {
                                c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);
                            string showtime = WirteTimeFormat(ref UpDateTime);
                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }

                            string tdstyle = "";
                            if (CssNameA.Text != "")
                            {
                                tdstyle = " style=\"" + CssNameA.Text + "\"";
                            }

                            jscontent.AppendLine("<li" + tdstyle + ">");
                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                            jscontent.AppendLine("</li>");
                        }

                        int Ccount = Colsnum - (int)ContentMod;

                    }
                    jscontent.AppendLine("</ul>");

                    #endregion
                    break;
                case "2"://普通列表

                    #region 普通列表
                    jscontent.AppendLine("");
                    for (int c = 0; c < ContentNum; c++)
                    {
                        for (int i = 0; i < Colsnum; i++)
                        {
                            string title = "title=\"";  //显示的信息;
                            string c_ClassName = "";  //节点名称
                            string c_Author = "";     //作者
                            string c_Hits = "";       //热点
                            string c_HotSign = "";    //热点标志
                                                      //string c_NewSign = "";    //最新标志 
                            string c_CommentLink = "";  //评论链接
                            string c_OpenType = "";    //打开类型

                            if (ShowClassName.Checked)
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[i]["NodeID"]);

                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }
                            }
                            else
                            {
                                c_ClassName = "";
                            }

                            if (ShowIncludePic.Checked)
                            {
                                //ShowIncludePic “图文”标志
                            }

                            if (ShowAuthor.Checked)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHits.Checked)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSign.Checked)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20) //文章大于20则为热门
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSign.Checked)
                            {//最新文章标志

                            }

                            if (ShowTips.Checked)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLink.Checked)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }
                            if (this.OpenType.SelectedValue != "")
                            {
                                c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);

                            string showtime = WirteTimeFormat(ref UpDateTime);
                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }
                            if (ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() == "")
                            {

                            }
                            jscontent.AppendLine("<a " + title + "\"" + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a> &nbsp;" + c_HotSign + c_CommentLink);

                            if (i < Colsnum - 1)
                            {
                                jscontent.AppendLine(" ");
                            }
                        }
                        jscontent.AppendLine("</br>");
                    }

                    for (int p = 0; p < (int)ContentMod; p++)
                    {
                        string title = "title=\"";
                        string c_ClassName = "";
                        //string c_IncludePic = "";
                        string c_Author = "";
                        string c_Hits = "";
                        string c_HotSign = "";
                        //string c_NewSign = "";
                        //string c_Tips = "";
                        string c_CommentLink = "";
                        string c_OpenType = "";

                        if (ShowClassName.Checked)
                        {
                            //ShowClassName 所属栏目
                            int NodeID = DataConverter.CLng(ContentList.Rows[ReadCst + p]["NodeID"]);
                            if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                            {
                                c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                            }
                        }
                        else
                        {
                            c_ClassName = "";
                        }

                        if (ShowIncludePic.Checked)
                        {
                            //ShowIncludePic “图文”标志
                        }

                        if (ShowAuthor.Checked)
                        {
                            //ShowAuthor 作者
                            c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                            title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                        }

                        if (ShowHits.Checked)
                        {
                            //ShowHits 点击次数
                            c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                            title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                        }

                        if (ShowHotSign.Checked)
                        {//热门文章标志
                            if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20) //文章大于20则为热门
                            {
                                c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                            }
                        }

                        if (ShowNewSign.Checked)
                        {//最新文章标志

                        }

                        if (ShowTips.Checked)
                        {
                            //显示提示信息
                            title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                        }

                        if (ShowCommentLink.Checked)
                        {
                            //显示评论链接
                            c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                        }
                        if (this.OpenType.SelectedValue != "")
                        {
                            c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                        }


                        DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);

                        string showtime = WirteTimeFormat(ref UpDateTime);
                        if (showtime != "")
                        {
                            title += "更新时间:" + showtime + "&#13;";
                        }
                        jscontent.AppendLine("<a  " + title + "\" " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                        jscontent.AppendLine(" ");

                    }
                    break;
                #endregion
                case "3"://表格式
                    #region 表格式
                    jscontent.AppendLine("");
                    jscontent.AppendLine("<table>");

                    for (int c = 0; c < ContentNum; c++)
                    {
                        string stylenames = "";
                        if ((c + 1) % 2 == 0)
                        {
                            stylenames = " style=\"" + CssName2.Text + "\"";//偶数
                        }
                        else
                        {
                            stylenames = " style=\"" + CssName1.Text + "\"";//奇数
                        }

                        jscontent.AppendLine("<tr" + stylenames + ">");

                        for (int i = 0; i < Colsnum; i++)
                        {
                            string title = "title='";
                            string c_ClassName = "";
                            string c_Author = "";
                            string c_Hits = "";
                            string c_HotSign = "";
                            //string c_Tips = "";
                            string c_CommentLink = "";
                            string c_OpenType = "";

                            if (ShowClassName.Checked)
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[i]["NodeID"]);
                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }
                            }
                            else
                            {
                                c_ClassName = "";
                            }

                            if (ShowIncludePic.Checked)
                            {
                                //ShowIncludePic “图文”标志
                            }

                            if (ShowAuthor.Checked)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHits.Checked)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSign.Checked)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSign.Checked)
                            {//最新文章标志

                            }

                            if (ShowTips.Checked)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLink.Checked)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (this.OpenType.SelectedValue != "")
                            {
                                c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);
                            string showtime = WirteTimeFormat(ref UpDateTime);

                            string tdstyle = "";
                            if (CssNameA.Text != "")
                            {
                                tdstyle = " style=\"" + CssNameA.Text + "\"";
                            }

                            jscontent.AppendLine("<td" + tdstyle + ">");
                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }
                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                            jscontent.AppendLine("</td>");
                        }
                        jscontent.AppendLine("</tr>");
                    }

                    string stylesname1 = "";

                    if ((ContentNum + 1) % 2 == 0)
                    {
                        stylesname1 = " style=\"" + CssName2.Text + "\"";
                    }
                    else
                    {
                        stylesname1 = " style=\"" + CssName1.Text + "\"";
                    }


                    if ((int)ContentMod > 0)
                    {
                        jscontent.AppendLine("<tr" + stylesname1 + ">");
                        for (int p = 0; p < (int)ContentMod; p++)
                        {
                            string title = "title='";
                            string c_HotSign = "";
                            string c_ClassName = "";
                            string c_Author = "";
                            string c_Hits = "";
                            //MOstring c_Tips = "";
                            string c_CommentLink = "";
                            string c_OpenType = "";

                            if (ShowClassName.Checked)
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[ReadCst + p]["NodeID"]);
                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }
                            }

                            if (ShowIncludePic.Checked)
                            {
                                //ShowIncludePic “图文”标志

                            }
                            if (ShowAuthor.Checked)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHits.Checked)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSign.Checked)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSign.Checked)
                            {//最新文章标志

                            }

                            if (ShowTips.Checked)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLink.Checked)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (this.OpenType.SelectedValue != "")
                            {
                                c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);
                            string showtime = WirteTimeFormat(ref UpDateTime);
                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }

                            string tdstyle = "";
                            if (CssNameA.Text != "")
                            {
                                tdstyle = " style=\"" + CssNameA.Text + "\"";
                            }

                            jscontent.AppendLine("<td" + tdstyle + ">");
                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                            jscontent.AppendLine("</td>");
                        }

                        int Ccount = Colsnum - (int)ContentMod;
                        for (int k = 0; k < Ccount; k++)
                        {
                            jscontent.AppendLine("<td>");
                            jscontent.AppendLine("</td>");
                        }
                        jscontent.AppendLine("</tr>");
                    }
                    jscontent.AppendLine("</table>");

                    #endregion
                    break;
                case "4"://各项独立式--框架显示
                    #region 各项独立式--框架显示
                    List<string> fileNames = new List<string>();  //框架中所有文件名
                    string FolderName = JsFileName.Text.Substring(0, (JsFileName.Text.LastIndexOf('.'))); //文件名称(除去后缀)
                    string folderPath = FileSystemObject.CreateFileFolder(Server.MapPath("/JS/Ads/" + ContentType.SelectedValue + "/" + FolderName)); //创建文件夹
                    DataTable dt = FileSystemObject.GetFileList(folderPath);  //获取该文件夹下所有文件 
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        //删除该文件夹下所有文件
                        FileSystemObject.Delete((Server.MapPath("/JS/Ads/" + ContentType.SelectedValue + "/" + FolderName + "/" + dr[0].ToString())), FsoMethod.File);
                    }

                    //内容开始写入文件
                    StringBuilder sbFile = new StringBuilder();  //文件内容
                    for (int c = 0; c < ContentNum; c++)
                    {
                        sbFile = new StringBuilder();
                        for (int i = 0; i < Colsnum; i++)
                        {
                            string title = "title='";
                            string c_ClassName = "";
                            //string c_IncludePic = "";
                            string c_Author = "";
                            string c_Hits = "";
                            string c_HotSign = "";
                            //string c_NewSign = "";
                            //string c_Tips = "";
                            string c_CommentLink = "";
                            string c_OpenType = "";

                            if (ShowClassName.Checked)
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[i]["NodeID"]);

                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }
                            }
                            else
                            {
                                c_ClassName = "";
                            }

                            if (ShowIncludePic.Checked)
                            {
                                //ShowIncludePic “图文”标志
                            }
                            if (ShowAuthor.Checked)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHits.Checked)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSign.Checked)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSign.Checked)
                            {//最新文章标志

                            }

                            if (ShowTips.Checked)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLink.Checked)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (this.OpenType.SelectedValue != "")
                            {
                                c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);

                            string showtime = WirteTimeFormat(ref UpDateTime);
                            if (showtime != null)
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }
                            sbFile.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);

                            if (i < Colsnum - 1)
                            {
                                sbFile.AppendLine(" ");
                            }
                        }
                        string fileName = DataSecurity.MakeFileRndName() + c + ".html"; //框架中文件名
                        fileNames.Add(fileName); //添加到所有文件中
                        FileSystemObject.WriteFile(folderPath + "\\" + fileName, "<html><head></head><body>" + sbFile.ToString() + "</body></html>"); //写入文件
                    }
                    sbFile = new StringBuilder();
                    for (int p = 0; p < (int)ContentMod; p++)
                    {
                        string title = "title='";
                        string c_ClassName = "";
                        //string c_IncludePic = "";
                        string c_Author = "";
                        string c_Hits = "";
                        string c_HotSign = "";
                        //string c_NewSign = "";
                        /// string c_Tips = "";
                        string c_CommentLink = "";
                        string c_OpenType = "";

                        if (ShowClassName.Checked)
                        {
                            //ShowClassName 所属栏目
                            int NodeID = DataConverter.CLng(ContentList.Rows[ReadCst + p]["NodeID"]);
                            if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                            {
                                c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                            }
                        }
                        else
                        {
                            c_ClassName = "";
                        }

                        if (ShowIncludePic.Checked)
                        {
                            //ShowIncludePic “图文”标志
                        }

                        if (ShowAuthor.Checked)
                        {
                            //ShowAuthor 作者
                            c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                            title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                        }

                        if (ShowHits.Checked)
                        {
                            //ShowHits 点击次数
                            c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                            title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                        }

                        if (ShowHotSign.Checked)
                        {//热门文章标志
                            if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20)  //为热门文章
                            {
                                c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                            }
                        }

                        if (ShowNewSign.Checked)
                        {//最新文章标志

                        }

                        if (ShowTips.Checked)
                        {
                            //显示提示信息
                            title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                        }

                        if (ShowCommentLink.Checked)
                        {
                            //显示评论链接
                            c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                        }

                        if (this.OpenType.SelectedValue != "")
                        {
                            c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                        }
                        DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);

                        string showtime = WirteTimeFormat(ref UpDateTime);
                        if (showtime != "")
                        {
                            title += "更新时间:" + showtime + "&#13;";
                        }

                        sbFile.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                    }
                    string fileName1 = DataSecurity.MakeFileRndName() + ".html"; //框架文件名
                    fileNames.Add(fileName1);
                    FileSystemObject.WriteFile(folderPath + "\\" + fileName1, "<html><head></head><body>" + sbFile.ToString() + "</body></html>");


                    //主文件，框架显示
                    if (fileNames != null && fileNames.Count > 0)
                    {
                        StringBuilder mianConten = new StringBuilder(); //主页面的内容
                        string rows = (100 / fileNames.Count).ToString(); //显示框架个数
                        mianConten.Append(@"<frameset rows='" + rows);
                        for (int i = 0; i < fileNames.Count; i++)
                        {
                            mianConten.Append("," + rows);
                            string fileUrl = folderPath + "\\" + fileNames[i];
                            jscontent.AppendLine(@"<frame src='" + fileUrl + "'scrolling='No' noresize='noresize' title='topFrame' />");
                        }
                        mianConten.AppendLine(@"' frameborder='no' border='0' framespacing='0'>");
                        jscontent.AppendLine("</frameset>");
                        if (ContentType.SelectedValue.Equals("JS")) //写入
                        {
                            FileSystemObject.WriteFile(Server.MapPath("/JS/Ads/JS/" + this.JsFileName.Text), "<html><head></head>" + mianConten.ToString() + jscontent.ToString() + "<noframes><body></body></noframes></html>");
                        }
                        if (ContentType.SelectedValue.Equals("HTML"))
                        {
                            FileSystemObject.WriteFile(Server.MapPath("/JS/Ads/HTML/" + this.JsFileName.Text), "<html><head></head>" + mianConten.ToString() + jscontent.ToString() + "<noframes><body></body></noframes></html>");
                        }
                    }
                    if (Request.Form["JSID"] != null)  //更新
                    {
                        Mjs.id = DataConverter.CLng(Request.Form["JSID"]);
                        Jll.GetUpdate(Mjs);
                        function.WriteSuccessMsg("修改成功！", "ManageJsContent.aspx");
                    }
                    else  //添加
                    {
                        Jll.GetInsert(Mjs);
                        function.WriteSuccessMsg("添加成功！", "ManageJsContent.aspx");
                    }
                    return;
                #endregion
                case "5"://智能多列式 
                    #region 智能多列式
                    jscontent.AppendLine("");
                    for (int c = 0; c < ContentNum; c++)
                    {
                        string stylenames = "";
                        if ((c + 1) % 2 == 0)
                        {
                            stylenames = " style=\"" + CssName2.Text + "\"";//偶数
                        }
                        else
                        {
                            stylenames = " style=\"" + CssName1.Text + "\"";//奇数
                        }
                        jscontent.AppendLine("<span " + stylenames + ">");
                        for (int i = 0; i < Colsnum; i++)
                        {
                            string title = "title='";
                            string c_ClassName = "";
                            // string c_IncludePic = "";
                            string c_Author = "";
                            string c_Hits = "";
                            string c_HotSign = "";
                            //string c_NewSign = "";
                            // string c_Tips = "";
                            string c_CommentLink = "";
                            string c_OpenType = "";

                            if (ShowClassName.Checked)
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[i]["NodeID"]);

                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }
                            }
                            else
                            {
                                c_ClassName = "";
                            }

                            if (ShowIncludePic.Checked)
                            {
                                //ShowIncludePic “图文”标志
                            }

                            if (ShowAuthor.Checked)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHits.Checked)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSign.Checked)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSign.Checked)
                            {//最新文章标志

                            }

                            if (ShowTips.Checked)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLink.Checked)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (this.OpenType.SelectedValue != "")
                            {
                                c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);

                            string showtime = WirteTimeFormat(ref UpDateTime);

                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }

                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);

                            if (i < Colsnum - 1)
                            {
                                jscontent.AppendLine(" ");
                            }
                        }
                        jscontent.AppendLine("</span></br>");
                    }


                    string stylename1 = "";
                    if ((ContentNum + 1) % 2 == 0)
                    {

                        stylename1 = " style=\"" + CssName2.Text + "\"";
                    }
                    else
                    {

                        stylename1 = " style=\"" + CssName1.Text + "\"";
                    }
                    jscontent.AppendLine("<span " + stylename1 + ">");
                    for (int p = 0; p < (int)ContentMod; p++)
                    {
                        string title = "title='";
                        string c_ClassName = "";
                        //string c_IncludePic = "";
                        string c_Author = "";
                        string c_Hits = "";
                        string c_HotSign = "";
                        //string c_NewSign = "";
                        //string c_Tips = "";
                        string c_CommentLink = "";
                        string c_OpenType = "";
                        if (ShowClassName.Checked)
                        {
                            //ShowClassName 所属栏目
                            int NodeID = DataConverter.CLng(ContentList.Rows[ReadCst + p]["NodeID"]);
                            if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                            {
                                c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                            }
                        }
                        else
                        {
                            c_ClassName = "";
                        }

                        if (ShowIncludePic.Checked)
                        {
                            //ShowIncludePic “图文”标志
                        }

                        if (ShowAuthor.Checked)
                        {
                            //ShowAuthor 作者
                            c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                            title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                        }

                        if (ShowHits.Checked)
                        {
                            //ShowHits 点击次数
                            c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                            title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                        }

                        if (ShowHotSign.Checked)
                        {//热门文章标志
                            if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20)  //为热门文章
                            {
                                c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                            }
                        }

                        if (ShowNewSign.Checked)
                        {//最新文章标志

                        }

                        if (ShowTips.Checked)
                        {
                            //显示提示信息
                            title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                        }

                        if (ShowCommentLink.Checked)
                        {
                            //显示评论链接
                            c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                        }
                        if (this.OpenType.SelectedValue != "")
                        {
                            c_OpenType = " target=\"" + this.OpenType.SelectedValue + "\"";
                        }


                        DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);

                        string showtime = WirteTimeFormat(ref UpDateTime);
                        if (showtime != "")
                        {
                            title += "更新时间:" + showtime + "&#13;";
                        }
                        jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlType.SelectedValue) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLen.Text)) + "</a>" + c_HotSign + c_CommentLink);
                        jscontent.AppendLine("");
                    }
                    jscontent.AppendLine("</span>");
                    break;
                #endregion
                default:
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

        //连接路径问题：绝对路径和相对路径
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

        // 显示日期格式
        private string WirteTimeFormat(ref DateTime UpDateTime)
        {
            string showtime = "";
            switch (ShowDateType.SelectedValue)//更新时间ShowDateType
            {
                case "0"://不显示
                    break;
                case "1"://年月日
                    showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";
                    break;
                case "2"://月日
                    showtime = UpDateTime.Month + "月" + UpDateTime.Day + "日";
                    break;
                case "3"://月-日
                    showtime = UpDateTime.Month + "-" + UpDateTime.Day;
                    break;
                default:
                    break;
            }
            return showtime;
        }

        // 获得内容列表
        private DataTable GetList()
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

            string Inputer = "";
            if (Author.Text != "")
            {
                Inputer = Author.Text; //作者
            }

            int DateNumvalue = 0;  //多少天内更新的文章
            if (DataConverter.CLng(this.DateNum.Text) > 0)
            {
                DateNumvalue = DataConverter.CLng(this.DateNum.Text);
            }

            //文章属性:热门还是推荐
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
            //ArticleNum.Text   文章数目;  SpecialID :所属专题;  99为终审通过
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

        // 读下一级专题
        //protected void ReadSpecInfoParentList(int PerentID)
        //{
        //    scount = scount + 1;
        //    B_Spec pll = new B_Spec();
        //    DataTable tbl = pll.GetSpecList(PerentID);
        //    for (int ii = 0; ii < tbl.Rows.Count; ii++)
        //    {
        //        ListItem ditems = new ListItem();
        //        ditems.Text = new string('　', scount) + tbl.Rows[ii]["SpecName"].ToString();
        //        ditems.Value = tbl.Rows[ii]["SpecID"].ToString();
        //        this.SpecialID.Items.Add(ditems);
        //    }
        //    scount = scount - 1;
        //}

        /// <summary>
        /// 读取节点信息
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
        // 递归读取节点信息
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
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

namespace ZoomLaCMS.Manage.Content
{
    public partial class ManageJsContent : CustomerPageAction
    {
        protected B_CreateJS Jll = new B_CreateJS();
        protected B_Admin mll = new B_Admin();
        protected B_Content cll = new B_Content();
        protected B_Node Nll = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='ContentManage.aspx'>" + Resources.L.内容管理 + "</a></li><li><a href='CreateHtmlContent.aspx'>" + Resources.L.生成发布 + "</a></li><li class='active'><a href='" + Request.RawUrl + "'>" + Resources.L.文章JS文件管理 + "</a></li>");
            }
        }
        //绑定数据源
        public void MyBind()
        {
            EGV.DataSource = Jll.Select_All();
            EGV.DataBind();
        }
        //分页
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        // 删除/刷新操作
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delInfo":
                    int id = Convert.ToInt32(e.CommandArgument);
                    Jll.DeleteByGroupID(id);
                    break;
                case "refresh":
                    id = Convert.ToInt32(e.CommandArgument);
                    M_CreateJS Jscontent = Jll.GetSelect(id);
                    if (Jscontent.JsType == 1)
                    {
                        CreateJs(id);
                    }
                    else
                    {
                        CreateContent(id);
                    }
                    function.WriteSuccessMsg(Resources.L.刷新完成 + "!", "ManageJsContent.aspx");
                    break;
            }
            MyBind();
        }
        //获取脚本文件
        protected string Getscript(string id)
        {
            M_CreateJS Jscontent = Jll.GetSelect(DataConverter.CLng(id));
            string jsc = "";
            if (Jscontent.ContentType == 0)
            {
                jsc = "<script src='/JS/Ads/JS/" + Jscontent.JsFileName + "'></script>";
            }
            else
            {
                jsc = "<iframe src='/JS/Ads/HTML/" + Jscontent.JsFileName + "'></iframe>";
            }
            return jsc;
        }
        //刷新js
        protected bool RefreshJS(int id)
        {
            M_CreateJS cjs = Jll.GetSelect(id);
            string jsxmlcontent = cjs.JsXmlContent;
            switch (cjs.JsType)
            {
                case 1:
                    break;
                case 2:
                    break;
            }

            return true;
        }

        //创建js
        protected void CreateJs(int id)
        {
            M_CreateJS jinfo = Jll.GetSelect(id);
            if (jinfo.id > 0) { if (jinfo.JsType != 1) { function.WriteErrMsg(Resources.L.JS信息分类读取错误 + "！"); } }


            string JsFileNametxt = jinfo.JsFileName;
            string Jsnametxt = jinfo.Jsname;
            string JsReadmetxt = jinfo.JsReadme;

            string jsxml = jinfo.JsXmlContent;
            int ContentTypetxt = jinfo.ContentType;
            XmlDocument xmldoc = new XmlDocument();
            jsxml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><doc>" + jsxml + "</doc>";
            xmldoc.LoadXml(jsxml);

            string ClassIDtxt = GetChild(xmldoc, "ClassID");
            bool IncludeChildtxt = (GetChild(xmldoc, "IncludeChild") == "1") ? true : false;
            string ArticleNumtxt = GetChild(xmldoc, "ArticleNum");
            string ContentPropertytxt = GetChild(xmldoc, "ContentProperty");
            string DateNumtxt = GetChild(xmldoc, "DateNum");
            string OrderTypetxt = GetChild(xmldoc, "OrderType");
            string ShowTypetxt = GetChild(xmldoc, "ShowType");
            string ImgWidthtxt = GetChild(xmldoc, "ImgWidth");
            string ImgHeighttxt = GetChild(xmldoc, "ImgHeight");
            string TitleLentxt = GetChild(xmldoc, "TitleLen");
            string ContentLentxt = GetChild(xmldoc, "ContentLen");
            bool ShowTipstxt = (GetChild(xmldoc, "ShowTips") == "1") ? true : false;
            string colstxt = GetChild(xmldoc, "Cols");
            string UrlTypetxt = GetChild(xmldoc, "UrlType");

            DataTable ContentList = Getlist(xmldoc);
            string picPath = "";  //图片路径
            int Colsnum = 0;//列数

            if (DataConverter.CLng(colstxt) > 0) { Colsnum = DataConverter.CLng(colstxt); } //每行显示文章数

            string txt_ShowType = ShowTypetxt;
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
            switch (ShowTypetxt)
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

                            if (ShowTipstxt)  //显示内容
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
                            jscontent.AppendLine("<img title='" + title + "' " + " src='" + GetUrl(UrlTypetxt) + "/UploadFiles" + picPath + "' width='" + ImgWidthtxt + "' height='" + ImgHeighttxt + "' /> <br/>");
                            jscontent.AppendLine("<a title='" + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
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

                            if (ShowTipstxt)  //显示内容
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
                            jscontent.AppendLine("<img title='" + title + "' " + " src='" + GetUrl(UrlTypetxt) + "/UploadFiles" + picPath + "' width='" + ImgWidthtxt + "' height='" + ImgHeighttxt + "' /><br/>");
                            jscontent.AppendLine("<a title='" + title + "' " + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
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

                            if (ShowTipstxt)  //显示内容
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
                            jscontent.AppendLine("<td><img title='" + title + "' " + " src='" + GetUrl(UrlTypetxt) + "/UploadFiles" + picPath + "' width='" + ImgWidthtxt + "' height='" + ImgHeighttxt + "' /></td>");
                            jscontent.AppendLine("<td>");
                            jscontent.AppendLine("<a title='" + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
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

                            if (ShowTipstxt)  //显示内容
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
                            jscontent.AppendLine("<img title='" + title + "' " + " src='" + GetUrl(UrlTypetxt) + "/UploadFiles" + picPath + "' width='" + ImgWidthtxt + "' height='" + ImgHeighttxt + "' /><br/>");
                            jscontent.AppendLine("<a title='" + title + "' " + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
                            jscontent.AppendLine("</td>");
                        }

                        int Ccount = Colsnum - (int)ContentMod;
                        jscontent.AppendLine("</tr>");
                    }
                    jscontent.AppendLine("</table>");

                    #endregion
                    break;
            }

            if (ContentTypetxt == 0)
            {
                FileSystemObject.WriteFile(Server.MapPath("/JS/Ads/JS/" + JsFileNametxt), ConvertJs(jscontent.ToString()));
            }
            else
            {
                FileSystemObject.WriteFile(Server.MapPath("/JS/Ads/HTML/" + JsFileNametxt), "<html><head></head><body>" + jscontent.ToString() + "</body></html>");
            }
        }
        //创建内容
        protected void CreateContent(int id)
        {
            M_CreateJS jinfo = Jll.GetSelect(id);
            string JsFileNametxt = jinfo.JsFileName;
            string JsNametxt = jinfo.Jsname;
            string JsReadmetxt = jinfo.JsReadme;

            string jsxml = jinfo.JsXmlContent;
            int ContentTypetxt = jinfo.ContentType;

            XmlDocument xmldoc = new XmlDocument();
            jsxml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><doc>" + jsxml + "</doc>";
            xmldoc.LoadXml(jsxml);

            string ShowTypetxt = GetChild(xmldoc, "ShowType");
            string Authortxt = GetChild(xmldoc, "Author");
            string ArticleNumtxt = GetChild(xmldoc, "ArticleNum");
            string ClassIDtxt = GetChild(xmldoc, "ClassID");
            bool IncludeChildtxt = GetChild(xmldoc, "IncludeChild") == "1" ? true : false;
            string DateNumtxt = GetChild(xmldoc, "DateNum");
            string OrderTypetxt = GetChild(xmldoc, "OrderType");
            string ShowPropertyTypetxt = GetChild(xmldoc, "ShowPropertyType");
            string TitleLentxt = GetChild(xmldoc, "TitleLen");
            string ContentLentxt = GetChild(xmldoc, "ContentLen");
            bool ShowClassNametxt = GetChild(xmldoc, "ShowClassName") == "1" ? true : false;
            bool ShowIncludePictxt = GetChild(xmldoc, "ShowIncludePic") == "1" ? true : false;
            bool ShowAuthortxt = GetChild(xmldoc, "ShowAuthor") == "1" ? true : false;
            string ShowDateTypetxt = GetChild(xmldoc, "ShowDateType");
            bool ShowHitstxt = GetChild(xmldoc, "ShowHits") == "1" ? true : false;
            bool ShowHotSigntxt = GetChild(xmldoc, "ShowHotSign") == "1" ? true : false;
            bool ShowNewSigntxt = GetChild(xmldoc, "ShowNewSign") == "1" ? true : false;
            bool ShowTipstxt = GetChild(xmldoc, "ShowTips") == "1" ? true : false;
            string OpenTypetxt = GetChild(xmldoc, "OpenType");
            string UrlTypetxt = GetChild(xmldoc, "UrlType");
            string Colstxt = GetChild(xmldoc, "Cols");
            bool ShowCommentLinktxt = GetChild(xmldoc, "ShowCommentLink") == "1" ? true : false;
            string ContentPropertytxt = GetChild(xmldoc, "ContentProperty");
            string CssNameAtxt = GetChild(xmldoc, "CssNameA");
            string CssName1txt = GetChild(xmldoc, "CssName1");
            string CssName2txt = GetChild(xmldoc, "CssName2");


            DataTable ContentList = Getlist(xmldoc);//内容列表

            int Colsnum = 0;//列数

            if (DataConverter.CLng(Colstxt) > 0) { Colsnum = DataConverter.CLng(Colstxt); } //每行显示标题的列数

            string txt_ShowType = ShowTypetxt;
            StringBuilder jscontent = new StringBuilder();
            int ContentNum = 0;
            if (Colsnum > 0)
            {
                ContentNum = ContentList.Rows.Count / Colsnum;
            }
            float ContentMod = ContentList.Rows.Count;

            if (Colsnum > 0)
            {
                ContentMod = ContentList.Rows.Count % Colsnum;
            }

            int ReadCst = ContentList.Rows.Count - (int)ContentMod;
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
                            stylenames = " style=\"" + CssName2txt + "\"";//偶数
                        }
                        else
                        {
                            stylenames = " style=\"" + CssName1txt + "\"";//奇数
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
                            //string c_NewSign = "";
                            //string c_Tips = "";
                            string c_CommentLink = "";
                            string c_OpenType = "";

                            if (ShowClassNametxt)
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

                            if (ShowIncludePictxt)
                            {
                                //ShowIncludePic “图文”标志
                            }
                            if (ShowAuthortxt)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHitstxt)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSigntxt)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSigntxt)
                            {//最新文章标志

                            }

                            if (ShowTipstxt)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLinktxt)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }
                            if (OpenTypetxt != "")
                            {
                                c_OpenType = " target=\"" + OpenTypetxt + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);

                            string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";

                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }

                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);

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
                        stylesname = " style=\"" + CssName2txt + "\"";
                    }
                    else
                    {
                        stylesname = " style=\"" + CssName1txt + "\"";
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
                        if (ShowClassNametxt)
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

                        if (ShowIncludePictxt)
                        {
                            //ShowIncludePic “图文”标志
                        }

                        if (ShowAuthortxt)
                        {
                            //ShowAuthor 作者
                            c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                            title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                        }

                        if (ShowHitstxt)
                        {
                            //ShowHits 点击次数
                            c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                            title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                        }

                        if (ShowHotSigntxt)
                        {//热门文章标志
                            if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20)  //为热门文章
                            {
                                c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                            }
                        }

                        if (ShowNewSigntxt)
                        {//最新文章标志

                        }

                        if (ShowTipstxt)
                        {
                            //显示提示信息
                            title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                        }

                        if (ShowCommentLinktxt)
                        {
                            //显示评论链接
                            c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                        }
                        if (OpenTypetxt != "")
                        {
                            c_OpenType = " target=\"" + OpenTypetxt + "\"";
                        }


                        DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);

                        string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";
                        if (showtime != "")
                        {
                            title += "更新时间:" + showtime + "&#13;";
                        }
                        jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
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
                            stylenames = " style=\"" + CssName2txt + "\"";//偶数
                        }
                        else
                        {
                            stylenames = " style=\"" + CssName1txt + "\"";//奇数
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

                            if (ShowClassNametxt)
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

                            if (ShowIncludePictxt)
                            {
                                //ShowIncludePic “图文”标志
                            }

                            if (ShowAuthortxt)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHitstxt)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSigntxt)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSigntxt)
                            {//最新文章标志

                            }

                            if (ShowTipstxt)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLinktxt)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (OpenTypetxt != "")
                            {
                                c_OpenType = " target=\"" + OpenTypetxt + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);
                            string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";

                            string tdstyle = "";
                            if (CssNameAtxt != "")
                            {
                                tdstyle = " style=\"" + CssNameAtxt + "\"";
                            }

                            jscontent.AppendLine("<li" + tdstyle + ">");
                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }
                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
                            jscontent.AppendLine("</li>");
                        }
                        jscontent.AppendLine("</ul>");
                    }

                    string stylename = "";

                    if ((ContentNum + 1) % 2 == 0)
                    {
                        stylename = " style=\"" + CssName2txt + "\"";
                    }
                    else
                    {
                        stylename = " style=\"" + CssName1txt + "\"";
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

                            if (ShowClassNametxt)
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[ReadCst + p]["NodeID"]);
                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }
                            }

                            if (ShowIncludePictxt)
                            {
                                //ShowIncludePic “图文”标志

                            }
                            if (ShowAuthortxt)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHitstxt)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSigntxt)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSigntxt)
                            {//最新文章标志

                            }

                            if (ShowTipstxt)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLinktxt)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (OpenTypetxt != "")
                            {
                                c_OpenType = " target=\"" + OpenTypetxt + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);
                            string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";
                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }

                            string tdstyle = "";
                            if (CssNameAtxt != "")
                            {
                                tdstyle = " style=\"" + CssNameAtxt + "\"";
                            }

                            jscontent.AppendLine("<li" + tdstyle + ">");
                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
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

                            if (ShowClassNametxt)
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

                            if (ShowIncludePictxt)
                            {
                                //ShowIncludePic “图文”标志
                            }

                            if (ShowAuthortxt)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHitstxt)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSigntxt)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20) //文章大于20则为热门
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSigntxt)
                            {//最新文章标志

                            }

                            if (ShowTipstxt)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLinktxt)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }
                            if (OpenTypetxt != "")
                            {
                                c_OpenType = " target=\"" + OpenTypetxt + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);

                            string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";
                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }
                            if (ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() == "")
                            {

                            }
                            jscontent.AppendLine("<a " + title + "\"" + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a> &nbsp;" + c_HotSign + c_CommentLink);

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

                        if (ShowClassNametxt)
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

                        if (ShowIncludePictxt)
                        {
                            //ShowIncludePic “图文”标志
                        }

                        if (ShowAuthortxt)
                        {
                            //ShowAuthor 作者
                            c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                            title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                        }

                        if (ShowHitstxt)
                        {
                            //ShowHits 点击次数
                            c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                            title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                        }

                        if (ShowHotSigntxt)
                        {//热门文章标志
                            if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20) //文章大于20则为热门
                            {
                                c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                            }
                        }

                        if (ShowNewSigntxt)
                        {//最新文章标志

                        }

                        if (ShowTipstxt)
                        {
                            //显示提示信息
                            title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                        }

                        if (ShowCommentLinktxt)
                        {
                            //显示评论链接
                            c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                        }
                        if (OpenTypetxt != "")
                        {
                            c_OpenType = " target=\"" + OpenTypetxt + "\"";
                        }


                        DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);

                        string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";
                        if (showtime != "")
                        {
                            title += "更新时间:" + showtime + "&#13;";
                        }
                        jscontent.AppendLine("<a  " + title + "\" " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
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
                            stylenames = " style=\"" + CssName2txt + "\"";//偶数
                        }
                        else
                        {
                            stylenames = " style=\"" + CssName1txt + "\"";//奇数
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

                            if (ShowClassNametxt)
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

                            if (ShowIncludePictxt)
                            {
                                //ShowIncludePic “图文”标志
                            }

                            if (ShowAuthortxt)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHitstxt)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSigntxt)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSigntxt)
                            {//最新文章标志

                            }

                            if (ShowTipstxt)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLinktxt)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (OpenTypetxt != "")
                            {
                                c_OpenType = " target=\"" + OpenTypetxt + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);
                            string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";

                            string tdstyle = "";
                            if (CssNameAtxt != "")
                            {
                                tdstyle = " style=\"" + CssNameAtxt + "\"";
                            }

                            jscontent.AppendLine("<td" + tdstyle + ">");
                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }
                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
                            jscontent.AppendLine("</td>");
                        }
                        jscontent.AppendLine("</tr>");
                    }

                    string stylesname1 = "";

                    if ((ContentNum + 1) % 2 == 0)
                    {
                        stylesname1 = " style=\"" + CssName2txt + "\"";
                    }
                    else
                    {
                        stylesname1 = " style=\"" + CssName1txt + "\"";
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
                            //string c_Tips = "";
                            string c_CommentLink = "";
                            string c_OpenType = "";

                            if (ShowClassNametxt)
                            {
                                //ShowClassName 所属栏目
                                int NodeID = DataConverter.CLng(ContentList.Rows[ReadCst + p]["NodeID"]);
                                if (Nll.GetNodeXML(NodeID).NodeName != "" && Nll.GetNodeXML(NodeID).NodeName != null)
                                {
                                    c_ClassName = "[" + Nll.GetNodeXML(NodeID).NodeName + "]";
                                }
                            }

                            if (ShowIncludePictxt)
                            {
                                //ShowIncludePic “图文”标志

                            }
                            if (ShowAuthortxt)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHitstxt)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSigntxt)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSigntxt)
                            {//最新文章标志

                            }

                            if (ShowTipstxt)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLinktxt)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (OpenTypetxt != "")
                            {
                                c_OpenType = " target=\"" + OpenTypetxt + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);
                            string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";
                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }

                            string tdstyle = "";
                            if (CssNameAtxt != "")
                            {
                                tdstyle = " style=\"" + CssNameAtxt + "\"";
                            }

                            jscontent.AppendLine("<td" + tdstyle + ">");
                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
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
                    string FolderName = JsFileNametxt.Substring(0, (JsFileNametxt.LastIndexOf('.'))); //文件名称(除去后缀)
                    string folderPath = FileSystemObject.CreateFileFolder(Server.MapPath("/JS/Ads/" + ContentTypetxt + "/" + FolderName)); //创建文件夹
                    DataTable dt = FileSystemObject.GetFileList(folderPath);  //获取该文件夹下所有文件 
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        //删除该文件夹下所有文件
                        FileSystemObject.Delete((Server.MapPath("/JS/Ads/" + ContentTypetxt + "/" + FolderName + "/" + dr[0].ToString())), FsoMethod.File);
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

                            if (ShowClassNametxt)
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

                            if (ShowIncludePictxt)
                            {
                                //ShowIncludePic “图文”标志
                            }
                            if (ShowAuthortxt)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHitstxt)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSigntxt)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSigntxt)
                            {//最新文章标志

                            }

                            if (ShowTipstxt)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLinktxt)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (OpenTypetxt != "")
                            {
                                c_OpenType = " target=\"" + OpenTypetxt + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);

                            string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日"; //WirteTimeFormat(ref UpDateTime);
                            if (showtime != null)
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }
                            sbFile.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);

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
                        //string c_Tips = "";
                        string c_CommentLink = "";
                        string c_OpenType = "";

                        if (ShowClassNametxt)
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

                        if (ShowIncludePictxt)
                        {
                            //ShowIncludePic “图文”标志
                        }

                        if (ShowAuthortxt)
                        {
                            //ShowAuthor 作者
                            c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                            title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                        }

                        if (ShowHitstxt)
                        {
                            //ShowHits 点击次数
                            c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                            title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                        }

                        if (ShowHotSigntxt)
                        {//热门文章标志
                            if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20)  //为热门文章
                            {
                                c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                            }
                        }

                        if (ShowNewSigntxt)
                        {//最新文章标志

                        }

                        if (ShowTipstxt)
                        {
                            //显示提示信息
                            title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                        }

                        if (ShowCommentLinktxt)
                        {
                            //显示评论链接
                            c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                        }

                        if (OpenTypetxt != "")
                        {
                            c_OpenType = " target=\"" + OpenTypetxt + "\"";
                        }
                        DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);

                        string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";
                        if (showtime != "")
                        {
                            title += "更新时间:" + showtime + "&#13;";
                        }

                        sbFile.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
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
                        if (ContentTypetxt == 0) //写入
                        {
                            FileSystemObject.WriteFile(Server.MapPath("/JS/Ads/JS/" + JsFileNametxt), "<html><head></head>" + mianConten.ToString() + jscontent.ToString() + "<noframes><body></body></noframes></html>");
                        }
                        if (ContentTypetxt == 1)
                        {
                            FileSystemObject.WriteFile(Server.MapPath("/JS/Ads/HTML/" + JsFileNametxt), "<html><head></head>" + mianConten.ToString() + jscontent.ToString() + "<noframes><body></body></noframes></html>");
                        }
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
                            stylenames = " style=\"" + CssName2txt + "\"";//偶数
                        }
                        else
                        {
                            stylenames = " style=\"" + CssName1txt + "\"";//奇数
                        }
                        jscontent.AppendLine("<span " + stylenames + ">");
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

                            if (ShowClassNametxt)
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

                            if (ShowIncludePictxt)
                            {
                                //ShowIncludePic “图文”标志
                            }

                            if (ShowAuthortxt)
                            {
                                //ShowAuthor 作者
                                c_Author = "[" + ContentList.Rows[i]["Inputer"].ToString() + "]";
                                title += "作者:" + ContentList.Rows[i]["Inputer"].ToString() + "&#13;";
                            }

                            if (ShowHitstxt)
                            {
                                //ShowHits 点击次数
                                c_Hits = "(" + ContentList.Rows[i]["Hits"].ToString() + ")";
                                title += "点击数:" + ContentList.Rows[i]["Hits"].ToString() + "&#13;";
                            }

                            if (ShowHotSigntxt)
                            {//热门文章标志
                                if (DataConverter.CLng(ContentList.Rows[i]["Hits"]) > 20)  //为热门文章
                                {
                                    c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                                }
                            }

                            if (ShowNewSigntxt)
                            {//最新文章标志

                            }

                            if (ShowTipstxt)
                            {
                                //显示提示信息
                                title += "文章标题:" + ContentList.Rows[i]["Title"].ToString() + "&#13;";
                            }

                            if (ShowCommentLinktxt)
                            {
                                //显示评论链接
                                c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[i]["GeneralID"].ToString() + "'>[评论信息]</a>";

                            }

                            if (OpenTypetxt != "")
                            {
                                c_OpenType = " target=\"" + OpenTypetxt + "\"";
                            }

                            DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[c + i]["UpDateTime"]);

                            string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";

                            if (showtime != "")
                            {
                                title += "更新时间:" + showtime + "&#13;";
                            }

                            jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[c * Colsnum + i]["GeneralID"].ToString() + "'>" + c_ClassName + BaseClass.Left(ContentList.Rows[c * Colsnum + i]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);

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

                        stylename1 = " style=\"" + CssName2txt + "\"";
                    }
                    else
                    {

                        stylename1 = " style=\"" + CssName1txt + "\"";
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
                        if (ShowClassNametxt)
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

                        if (ShowIncludePictxt)
                        {
                            //ShowIncludePic “图文”标志
                        }

                        if (ShowAuthortxt)
                        {
                            //ShowAuthor 作者
                            c_Author = "[" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "]";
                            title += "作者:" + ContentList.Rows[ReadCst + p]["Inputer"].ToString() + "&#13;";
                        }

                        if (ShowHitstxt)
                        {
                            //ShowHits 点击次数
                            c_Hits = "(" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + ")";
                            title += "点击数:" + ContentList.Rows[ReadCst + p]["Hits"].ToString() + "&#13;";
                        }

                        if (ShowHotSigntxt)
                        {//热门文章标志
                            if (DataConverter.CLng(ContentList.Rows[ReadCst + p]["Hits"]) > 20)  //为热门文章
                            {
                                c_HotSign = "<img src='/Images/Article/hot.gif' alt='固顶文章'>";
                            }
                        }

                        if (ShowNewSigntxt)
                        {//最新文章标志

                        }

                        if (ShowTipstxt)
                        {
                            //显示提示信息
                            title += "文章标题:" + ContentList.Rows[ReadCst + p]["Title"].ToString() + "&#13;";
                        }

                        if (ShowCommentLinktxt)
                        {
                            //显示评论链接
                            c_CommentLink = " <a href ='" + GetUrl("0") + "/Comments/CommentFor.aspx?ID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>[评论信息]</a>";

                        }
                        if (OpenTypetxt != "")
                        {
                            c_OpenType = " target=\"" + OpenTypetxt + "\"";
                        }


                        DateTime UpDateTime = DataConverter.CDate(ContentList.Rows[ReadCst + p]["UpDateTime"]);

                        string showtime = UpDateTime.Year + "年" + UpDateTime.Month + "月" + UpDateTime.Day + "日";
                        if (showtime != "")
                        {
                            title += "更新时间:" + showtime + "&#13;";
                        }
                        jscontent.AppendLine("<a " + title + "' " + c_OpenType + " href='" + GetUrl(UrlTypetxt) + "/Content.aspx?ItemID=" + ContentList.Rows[ReadCst + p]["GeneralID"].ToString() + "'>" + BaseClass.Left(c_ClassName + ContentList.Rows[ReadCst + p]["title"].ToString(), DataConverter.CLng(TitleLentxt)) + "</a>" + c_HotSign + c_CommentLink);
                        jscontent.AppendLine("");
                    }
                    jscontent.AppendLine("</span>");
                    break;
                #endregion
                default:
                    break;
            }

            if (ContentTypetxt == 0)
            {
                FileSystemObject.WriteFile(Server.MapPath("/JS/Ads/JS/" + JsFileNametxt), ConvertJs(jscontent.ToString()));
            }
            else
            {
                FileSystemObject.WriteFile(Server.MapPath("/JS/Ads/HTML/" + JsFileNametxt), "<html><head></head><body>" + jscontent.ToString() + "</body></html>");
            }
        }
        // 通过ModelID ,itemID查找图片路径
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

        //转换成js文件
        protected string ConvertJs(string oSource)
        {
            string oResultvalue = Filter(oSource, "htmltojs");
            oResultvalue = "document.writeln(\"" + oResultvalue + "\");";
            return oResultvalue;
        }
        // 字符过滤
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

        // 读取节点值
        protected string GetChild(XmlDocument xmldoc, string SingleName)
        {
            string IncludeChild = xmldoc.SelectSingleNode("//" + SingleName + "").InnerText;
            return IncludeChild;
        }
        // 连接路径问题：绝对路径和相对路径
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

        // 获得内容列表
        private DataTable Getlist(XmlDocument xmldoc)
        {
            string OrderTypevalue = GetChild(xmldoc, "OrderType");
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
            if (DataConverter.CLng(GetChild(xmldoc, "DateNum")) > 0)
            {
                DateNumvalue = DataConverter.CLng(GetChild(xmldoc, "DateNum"));
            }

            string ContentPropertyvalue = GetChild(xmldoc, "ContentProperty");
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
            bool IncludeChildtxt = (GetChild(xmldoc, "IncludeChild") == "1") ? true : false;
            //DataTable sptable = cll.GetSpecInfoContent(DataConverter.CLng(GetChild(xmldoc, "ArticleNum")), DataConverter.CLng(GetChild(xmldoc, "ClassID")), -1, 99, ShowField, descstr, Inputer, DateNumvalue, IncludeChildtxt, IsHot, IsElite);
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using BDUBLL;
using BDUModel;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Data;

namespace ZoomLa.Sns
{
    public class ZoneFun
    {
        #region 替换函数
        public static string MessageReplace(string message, int id, Guid cid, Guid sid)
        {
            #region 业务对象
            blogTableBLL btbll = new blogTableBLL();
            BlogStyleTableBLL bstbll = new BlogStyleTableBLL();
            LogManageBLL lmbll = new LogManageBLL();
            PicCateg_BLL pcbll = new PicCateg_BLL();
            PicTure_BLL ptbll = new PicTure_BLL();
            blogTable bt = btbll.GetUserBlog(id);
            BlogStyleTable bst = bstbll.GetBlogStyleByID(bt.StyleID);
            B_User ubll = new B_User();
            BDUModel.PagePagination newpage = new BDUModel.PagePagination();
            #endregion

            #region 空间名称
            if (message.IndexOf("$Zone_Name$") > -1)
            {
                message = message.Replace("$Zone_Name$", bt.BlogName);
            }
            #endregion

            #region 空间日志
            string strLog = "$Zone_UserLog";
            if (message.IndexOf(strLog) > -1)
            {

                string messa = StrSub(message, strLog);

                string substrt = messa.Substring(strLog.Length + 1, messa.Length - strLog.Length - 3);
                string[] subgroup = substrt.Split(new char[] { ',' });
                int logcount = int.Parse(subgroup[0].ToString());
                int logzcount = int.Parse(subgroup[1].ToString());
                int logtype = int.Parse(subgroup[2].ToString());
                int logorder = int.Parse(subgroup[3].ToString());
                int showtype = int.Parse(subgroup[4].ToString());
                List<UserLog> list = new List<UserLog>();
                Dictionary<string, string> order = new Dictionary<string, string>();
                if (logorder == 1)
                {
                    order.Add("CreatDate", "0");
                }
                else
                {
                    order.Add("readCount", "0");
                }
                newpage.PageOrder = order;
                if (logtype == 1)
                {

                    list = lmbll.GetSelfUserLogByUserID(id, 1, new Guid("ff503f4b-7972-4cd2-8fc8-08bdcfff7115"), new DateTime(), newpage);
                }
                else
                {
                    list = lmbll.GetAllLog(newpage);
                }

                string mess = string.Empty;
                //mess = "<table  width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">";
                if (list.Count < 1)
                {
                    //mess += "  <tr><td>无日志记录</td></tr>";
                    mess += "  <li>无日志记录</li>";
                }
                else
                {
                    int i = 0;
                    foreach (UserLog ul in list)
                    {
                        i++;
                        if (i > logcount)
                            break;
                        if (ul.LogTitle.Length > logzcount)
                        {
                            ul.LogTitle = ul.LogTitle.Substring(0, logzcount) + "...";
                        }
                        mess = "<li>";
                        if (logtype == 1)
                        {
                            //mess += "  <tr height=\"46\"><td><img src='../User/UserZone/Skin/index2_06.jpg' /><a href=\"ShowList.aspx?id=" + ul.UserID.ToString() + "&cid=" + ul.ID.ToString() + "&action=LogShow\"><font color=\"#003366\" size=\"4\">" + ul.LogTitle + "</font></a>   <br/>发表时间" + ul.CreatDate + "</td></tr>";
                            mess += "  <h1><a href=\"ShowList.aspx?id=" + ul.UserID.ToString() + "&cid=" + ul.ID.ToString() + "&sid=" + ul.ID.ToString() + "&action=LogShow\">" + ul.LogTitle + "</a></h1>";
                            if (showtype == 1)
                            {
                                mess += "<div>发表时间" + ul.CreatDate + "</div>";
                                if (ul.LogContext.Length > 30)
                                {

                                    //mess += "<tr ><td>&nbsp;&nbsp;  " + ul.LogContext.Substring(0, 30) + "...</td></tr>";
                                    mess += "<div>" + ul.LogContext.Substring(0, 30).Replace("<p>", "") + "...</div>";
                                }
                                else
                                {
                                    //mess += "<tr ><td>&nbsp;&nbsp;  " + ul.LogContext + "</td></tr>";
                                    mess += "<div>" + ul.LogContext + "</div>";
                                }
                                //mess += "  <tr height=\"46\" align=\"right\"><td><a href=\"#\">阅读全文</a>&nbsp;&nbsp;<a href=\"#\">阅读全文</a>&nbsp;&nbsp;<a href=\"#\">阅读全文</a>&nbsp;&nbsp;<a href=\"#\">阅读全文</a>&nbsp;&nbsp;</td></tr>";
                                mess += "  <div><a href=\"ShowList.aspx?id=" + ul.UserID.ToString() + "&cid=" + ul.ID.ToString() + "&sid=" + ul.ID.ToString() + "&action=LogShow\">阅读全文</a></div>";
                            }
                        }
                        else
                        {
                            //mess += "  <tr height=\"46\"><td><a href=\"ShowList.aspx?id=" + ul.UserID.ToString() + "&cid=" + ul.ID.ToString() + "&action=LogShow\"><font color=\"#003366\" size=\"4\">" + ul.LogTitle + "</font></a>";
                            mess += "  <div><a href=\"ShowList.aspx?id=" + ul.UserID.ToString() + "&cid=" + ul.ID.ToString() + "&sid=" + ul.ID.ToString() + "&action=LogShow\">" + ul.LogTitle + "</div>";
                        }
                        mess += "</li>";
                    }
                }
                //mess += "</table>";



                message = message.Replace(messa, mess);
            }
            #endregion

            #region 空间栏目
            if (message.IndexOf("$Zone_UserBanner$") > -1)
            {
                string UserBanner = "";
                UserBanner += "<li><a href=\"ShowList.aspx?id=" + id.ToString() + "\">首页</a></li><li><a href=\"ShowList.aspx?id=" + id.ToString() + "&action=LogStyle\">日志</a></li> <li><a href=\"ShowList.aspx?id=" + id.ToString() + "&action=PhotoStyle\">相册</a></li> <li><a href=\"ShowList.aspx?id=" + id.ToString() + "&action=GroupStyle\">族群</a></li>";

                message = message.Replace("$Zone_UserBanner$", UserBanner);
            }
            #endregion

            #region 空间相册
            string strPhoto = "$Zone_UserPhoto";
            if (message.IndexOf(strPhoto) > -1)
            {
                string SubPhoto = StrSub(message, strPhoto);
                string[] GroupPhoto = SubPhoto.Substring(strPhoto.Length + 1, SubPhoto.Length - strPhoto.Length - 3).Split(new char[] { ',' });
                int count = int.Parse(GroupPhoto[0].ToString());
                int counth = int.Parse(GroupPhoto[1].ToString());
                int photow = int.Parse(GroupPhoto[2].ToString());
                int photoh = int.Parse(GroupPhoto[3].ToString());
                int phototype = int.Parse(GroupPhoto[4].ToString());
                int photoorder = int.Parse(GroupPhoto[5].ToString());
                List<PicCateg> list = new List<PicCateg>();
                Dictionary<string, string> order = new Dictionary<string, string>();
                if (photoorder == 1)
                {
                    order.Add("CategTime", "0");
                }
                else
                {
                    order.Add("CategTime", "0");
                }
                newpage.PageOrder = order;
                if (phototype == 1)
                {

                    list = pcbll.GetPicCategList(id, id, newpage);
                }
                else
                {
                    list = pcbll.GetAllPic(newpage);
                }


                string messphoto = string.Empty;
                //messphoto = "<table  width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">";
                if (list.Count < 1)
                {
                    //messphoto += "  <tr><td>无相册记录</td></tr>";
                    messphoto += "  <li>无相册记录</li>";
                }
                else
                {
                    int picount = 0;
                    PicCateg[] pc = list.ToArray();
                    count = count < list.Count ? count : list.Count;
                    for (int pj = 0; pj < count; )
                    {


                        //messphoto += "<tr>";

                        for (int pi = 0; pi < counth; pi++)
                        {
                            if (picount < count)
                                picount++;
                            else
                                break;
                            //messphoto += "<td><a href=\"ShowList.aspx?id=" + pc[pj].PicCategUserID.ToString() + "&cid=" + pc[pj].ID.ToString() + "&action=PhotoShow\"><img src='../UpImages/" + pc[pj].TitlePIcUrl.Substring(2) + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/></a><br><a href=\"#\">" + pc[pj].PicCategTitle + "</a></td>";
                            messphoto += "<li><a href=\"ShowList.aspx?id=" + pc[pj].PicCategUserID.ToString() + "&cid=" + pc[pj].ID.ToString() + "&action=PhotoShow\"><img src='" + pc[pj].TitlePIcUrl.Replace("~/", "/") + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/></a><a href=\"ShowList.aspx?id=" + pc[pj].PicCategUserID.ToString() + "&cid=" + pc[pj].ID.ToString() + "&action=PhotoShow\">" + pc[pj].PicCategTitle + "</a></li>";
                            pj++;
                        }
                        //messphoto += "</tr>";
                    }
                }
                //messphoto += "</table>";
                message = message.Replace(SubPhoto, messphoto);
            }
            #endregion

            #region 空间族群
            string strGroup = "$Zone_UserGroup";
            if (message.IndexOf(strGroup) > -1)
            {
                string SubPhoto = StrSub(message, strGroup);
                string[] GroupPhoto = SubPhoto.Substring(strGroup.Length + 1, SubPhoto.Length - strGroup.Length - 3).Split(new char[] { ',' });
                int count = int.Parse(GroupPhoto[0].ToString());
                int counth = int.Parse(GroupPhoto[1].ToString());
                int photow = int.Parse(GroupPhoto[2].ToString());
                int photoh = int.Parse(GroupPhoto[3].ToString());
                int phototype = int.Parse(GroupPhoto[4].ToString());
                int photoorder = int.Parse(GroupPhoto[5].ToString());
                List<GatherStrain> list = new List<GatherStrain>();
                Dictionary<string, string> order = new Dictionary<string, string>();
                if (photoorder == 1)
                {
                    order.Add("creatTime", "0");
                }
                else
                {
                    order.Add("creatTime", "0");
                }
                newpage.PageOrder = order;
                string messphoto = string.Empty;
                //messphoto = "<table  width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">";
                if (list.Count < 1)
                {
                    //messphoto += "  <tr><td>无族群记录</td></tr>";
                    messphoto += "<li>无族群记录</li>";
                }
                else
                {
                    int picount = 0;
                    GatherStrain[] pc = list.ToArray();
                    count = count < list.Count ? count : list.Count;
                    for (int pj = 0; pj < count; )
                    {

                        //messphoto += "<tr>";

                        for (int pi = 0; pi < counth; pi++)
                        {
                            if (picount < count)
                                picount++;
                            else
                                break;
                            //messphoto += "<td><a href=\"ShowList.aspx?action=GroupShow&id=" + pc[pj].UserID + "&cid=" + pc[pj].ID + "\"><img src='../" + pc[pj].GSICO + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/></a><br><a href=\"#\">" + pc[pj].GSName + "</a></td>";
                            messphoto += "<li><a href=\"ShowList.aspx?action=GroupShow&id=" + pc[pj].UserID + "&cid=" + pc[pj].ID + "\"><img src='" + pc[pj].GSICO.Replace("~/","/") + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/></a><a href=\"#\">" + pc[pj].GSName + "</a></li>";
                            pj++;
                        }
                        //messphoto += "</tr>";

                    }
                }
                //messphoto += "</table>";
                message = message.Replace(SubPhoto, messphoto);
            }
            #endregion

            #region 日志标题
            if (message.IndexOf("$Zone_LogTitle$") > -1)
            {
                UserLog ul = lmbll.GetUserLogByID(cid);
                message = message.Replace("$Zone_LogTitle$", ul.LogTitle);
            }
            #endregion

            #region 日志内容
            if (message.IndexOf("$Zone_LogContent$") > -1)
            {
                UserLog ul = lmbll.GetUserLogByID(cid);
                message = message.Replace("$Zone_LogContent$", ul.LogContext + "<iframe src=\"Zone/CommentFor.aspx?action=LogShow&cid=" + cid + "\" frameborder=\"0\" width=\"500px\"></iframe>");
            }
            #endregion

            #region 日志添加时间
            if (message.IndexOf("$Zone_LogTime$") > -1)
            {
                UserLog ul = lmbll.GetUserLogByID(cid);
                message = message.Replace("$Zone_LogTime$", ul.CreatDate.ToString());
            }
            #endregion

            #region 相册名称
            if (message.IndexOf("$Zone_PhotoName$") > -1)
            {
                PicCateg pc = pcbll.GetPicCateg(cid);
                message = message.Replace("$Zone_PhotoName$", pc.PicCategTitle);
            }
            #endregion

            #region 相册时间
            if (message.IndexOf("$Zone_PhotoTime$") > -1)
            {
                PicCateg pc = pcbll.GetPicCateg(cid);
                message = message.Replace("$Zone_PhotoTime$", pc.CategTime.ToString());
            }
            #endregion

            #region 相册图片
            string strPhotoUrl = "$Zone_PhotoUrl";
            if (message.IndexOf(strPhotoUrl) > -1)
            {
                string SubPhoto = StrSub(message, strPhotoUrl);
                string[] GroupPhoto = SubPhoto.Substring(strPhotoUrl.Length + 1, SubPhoto.Length - strPhotoUrl.Length - 3).Split(new char[] { ',' });
                int count = int.Parse(GroupPhoto[0].ToString());
                int counth = int.Parse(GroupPhoto[1].ToString());
                PicCateg pc = pcbll.GetPicCateg(cid);
                string messphoto = "<img src=" + pc.TitlePIcUrl.Replace("~/","/")+ " width=" + count.ToString() + " height=" + counth.ToString() + "/>";
                message = message.Replace(SubPhoto, messphoto);

            }
            #endregion

            #region 相片名称
            if (message.IndexOf("$Zone_PicName$") > -1)
            {
                PicTure pt = ptbll.GetPic(sid);
                message = message.Replace("$Zone_PicName$", pt.PicName);
            }
            #endregion

            #region 相片添加时间
            if (message.IndexOf("$Zone_PicTime$") > -1)
            {
                PicTure pt = ptbll.GetPic(sid);
                message = message.Replace("$Zone_PicTime$", pt.PicUpTime.ToString());
            }
            #endregion

            #region 相片图片
            string strPicUrl = "$Zone_PicUrl";
            if (message.IndexOf(strPicUrl) > -1)
            {
                string SubPhoto = StrSub(message, strPicUrl);
                string[] GroupPhoto = SubPhoto.Substring(strPicUrl.Length + 1, SubPhoto.Length - strPicUrl.Length - 3).Split(new char[] { ',' });
                int count = int.Parse(GroupPhoto[0].ToString());
                int counth = int.Parse(GroupPhoto[1].ToString());
                PicTure pt = ptbll.GetPic(sid);
                string messphoto = "<img src=" + pt.PicUrl.Replace("~/", "/") + " width=" + count.ToString() + " height=" + counth.ToString() + "/>";
                message = message.Replace(SubPhoto, messphoto);

            }
            #endregion

            #region 空间相册展示
            string strPhotoPic = "$Zone_PicList";
            if (message.IndexOf(strPhotoPic) > -1)
            {
                string SubPhoto = StrSub(message, strPhotoPic);
                string[] GroupPhoto = SubPhoto.Substring(strPhotoPic.Length + 1, SubPhoto.Length - strPhotoPic.Length - 3).Split(new char[] { ',' });
                int count = int.Parse(GroupPhoto[0].ToString());
                int counth = int.Parse(GroupPhoto[1].ToString());
                int photow = int.Parse(GroupPhoto[2].ToString());
                int photoh = int.Parse(GroupPhoto[3].ToString());



                List<PicTure> list = ptbll.GetPicTureList(cid, null);

                string messphoto = string.Empty;
                //messphoto = "<table  width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">";
                if (list.Count < 1)
                {
                    //messphoto += "  <tr><td>无相片记录</td></tr>";
                    messphoto += "  <li>无相片记录</li>";
                }
                else
                {
                    int picount = 0;
                    PicTure[] pc = list.ToArray();
                    count = count < list.Count ? count : list.Count;
                    for (int pj = 0; pj < count; )
                    {


                        //messphoto += "<tr>";

                        for (int pi = 0; pi < counth; pi++)
                        {
                            if (picount < count)
                                picount++;
                            else
                                break;
                            //messphoto += "<td><a href=\"ShowList.aspx?id=" + id.ToString() + "&cid=" + cid.ToString() + "&sid="+pc[pj].ID.ToString()+"&action=PicShow\"><img src='../" + pc[pj].PicUrl.Substring(2) + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/></a></td>";
                            messphoto += "<li><a href=\"ShowList.aspx?id=" + id.ToString() + "&cid=" + cid.ToString() + "&sid=" + pc[pj].ID.ToString() + "&action=PicShow\"><img src='" + pc[pj].PicUrl.Replace("~/","/") + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/></a></li>";
                            pj++;
                        }
                        //messphoto += "</tr>";

                    }
                }
                //messphoto += "</table>";
                message = message.Replace(SubPhoto, messphoto);
            }
            #endregion

            #region 会员登录框
            if (message.IndexOf("$Zone_Login$") > -1)
            {
                string UserBanner = "<iframe src=\"Login.aspx\" frameborder=\"0\" width=\"195px\"></iframe>";
                message = message.Replace("$Zone_Login$", UserBanner);
            }
            #endregion

            #region 用户空间列表
            string strZone = "$Zone_ZoneList";
            if (message.IndexOf(strZone) > -1)
            {
                string SubPhoto = StrSub(message, strZone);
                string[] GroupPhoto = SubPhoto.Substring(strZone.Length + 1, SubPhoto.Length - strZone.Length - 3).Split(new char[] { ',' });
                int count = int.Parse(GroupPhoto[0].ToString());
                int counth = int.Parse(GroupPhoto[1].ToString());
                int photow = int.Parse(GroupPhoto[2].ToString());
                int photoh = int.Parse(GroupPhoto[3].ToString());
                int zoneorder = int.Parse(GroupPhoto[4].ToString());

                List<blogTable> list = new List<blogTable>();
                Dictionary<string, string> order = new Dictionary<string, string>();
                if (zoneorder == 1)
                {
                    order.Add("CategTime", "0");
                }
                else
                {
                    order.Add("CategTime", "0");
                }
                newpage.PageOrder = order;

                list = btbll.GetBlogTableByState(1);

                string messphoto = string.Empty;
                //messphoto = "<table  width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">";
                if (list.Count < 1)
                {
                    //messphoto += "  <tr><td>无空间记录</td></tr>";
                    messphoto += "  <li>无空间记录</li>";
                }
                else
                {
                    int picount = 0;
                    blogTable[] pc = list.ToArray();
                    count = count < list.Count ? count : list.Count;
                    for (int pj = 0; pj < count; )
                    {


                        //messphoto += "<tr>";

                        for (int pi = 0; pi < counth; pi++)
                        {

                            if (picount < count)
                                picount++;
                            else
                                break;
                            M_Uinfo uinfo = ubll.GetUserBaseByuserid(pc[pj].UserID);
                            //messphoto += "<td><a href=\"#\"><img src='" + uinfo.UserFace + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/></a><br><a href=\"#\">" + pc[pj].BlogName + "</a></td>";
                            messphoto += "<li><a href=\"ShowList.aspx?id=" + pc[pj].UserID.ToString() + "\"><img src='" + uinfo.UserFace + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/></a><br><a href=\"ShowList.aspx?id=" + pc[pj].UserID.ToString() + "\">" + pc[pj].BlogName + "</a></li>";
                            pj++;
                        }
                        //messphoto += "</tr>";

                    }
                }
                //messphoto += "</table>";
                message = message.Replace(SubPhoto, messphoto);
            }
            #endregion

            #region 空间模板列表
            string strZoneStyle = "$Zone_StyleList";
            if (message.IndexOf(strZoneStyle) > -1)
            {
                string SubPhoto = StrSub(message, strZoneStyle);
                string[] GroupPhoto = SubPhoto.Substring(strZoneStyle.Length + 1, SubPhoto.Length - strZoneStyle.Length - 3).Split(new char[] { ',' });
                int count = int.Parse(GroupPhoto[0].ToString());
                int counth = int.Parse(GroupPhoto[1].ToString());
                int photow = int.Parse(GroupPhoto[2].ToString());
                int photoh = int.Parse(GroupPhoto[3].ToString());
                int zoneorder = int.Parse(GroupPhoto[4].ToString());

                List<BlogStyleTable> list = new List<BlogStyleTable>();
                Dictionary<string, string> order = new Dictionary<string, string>();
                if (zoneorder == 1)
                {
                    order.Add("CategTime", "0");
                }
                else
                {
                    order.Add("CategTime", "0");
                }
                newpage.PageOrder = order;

                DataTable dt = BlogStyleTableLogic.GetBlogStyleByState(1);

                string messphoto = string.Empty;
                //messphoto = "<table  width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">";
                if (dt.Rows.Count < 1)
                {
                    //messphoto += "  <tr><td>无模板记录</td></tr>";
                    messphoto += "  <li>无模板记录</li>";
                }
                else
                {
                    int picount = 0;
                    BlogStyleTable[] pc = list.ToArray();
                    count = count < list.Count ? count : list.Count;
                    for (int pj = 0; pj < count; )
                    {


                        //messphoto += "<tr>";

                        for (int pi = 0; pi < counth; pi++)
                        {

                            if (picount < count)
                                picount++;
                            else
                                break;

                            //messphoto += "<td><a href=\"#\"><img src='../" + pc[pj].StylePic + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/></a><br><a href=\"#\">" + pc[pj].StyleName + "</a></td>";
                            messphoto += "<li><img src='" + pc[pj].StylePic + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/><a href=\"#\">" + pc[pj].StyleName + "</li>";
                            pj++;
                        }
                        //messphoto += "</tr>";

                    }
                }
                //messphoto += "</table>";
                message = message.Replace(SubPhoto, messphoto);
            }
            #endregion

            #region 网站名称
            if (message.IndexOf("$Zone_SiteName$") > -1)
            {
                message = message.Replace("$Zone_SiteName$", SiteConfig.SiteInfo.SiteName);
            }
            #endregion

            #region 网站Url
            if (message.IndexOf("$Zone_SiteURL$") > -1)
            {
                message = message.Replace("$Zone_SiteURL$", SiteConfig.SiteInfo.SiteUrl);
            }
            #endregion

            #region 网站标题
            if (message.IndexOf("$Zone_SiteTitle$") > -1)
            {
                message = message.Replace("$Zone_SiteTitle$", SiteConfig.SiteInfo.SiteTitle);
            }
            #endregion

            #region 网站关键字
            if (message.IndexOf("$Zone_MetaKeywords$") > -1)
            {
                message = message.Replace("$Zone_MetaKeywords$", SiteConfig.SiteInfo.MetaKeywords);
            }
            #endregion

            #region 网站摘要
            if (message.IndexOf("$Zone_MetaDescription$") > -1)
            {
                message = message.Replace("$Zone_MetaDescription$", SiteConfig.SiteInfo.MetaDescription);
            }
            #endregion

            #region 网站LOGO
            if (message.IndexOf("$Zone_LogoUrl$") > -1)
            {
                message = message.Replace("$Zone_LogoUrl$", "<img src='../" + SiteConfig.SiteInfo.LogoUrl + "'/>");
            }
            #endregion

            #region 网站站长
            if (message.IndexOf("$Zone_Webmaster$") > -1)
            {
                message = message.Replace("$Zone_Webmaster$", SiteConfig.SiteInfo.Webmaster);
            }
            #endregion

            #region 网站Email
            if (message.IndexOf("$Zone_WebmasterEmail$") > -1)
            {
                message = message.Replace("$Zone_WebmasterEmail$", SiteConfig.SiteInfo.WebmasterEmail);
            }
            #endregion

            #region 版权信息
            if (message.IndexOf("$Zone_Copyright$") > -1)
            {
                message = message.Replace("$Zone_Copyright$", SiteConfig.SiteInfo.Copyright);
            }
            #endregion

            #region 族群名称
            if (message.IndexOf("$Zone_GroupName$") > -1)
            {
            }
            #endregion

            #region 族群图标
            string strGroupUrl = "$Zone_GroupUrl";
            if (message.IndexOf(strGroupUrl) > -1)
            {
                string SubPhoto = StrSub(message, strGroupUrl);
                string[] GroupPhoto = SubPhoto.Substring(strGroupUrl.Length + 1, SubPhoto.Length - strGroupUrl.Length - 3).Split(new char[] { ',' });
                int count = int.Parse(GroupPhoto[0].ToString());
                int counth = int.Parse(GroupPhoto[1].ToString());
            }
            #endregion
            #region 族群话题
            string strTopic = "$Zone_GroupToPic";
            if (message.IndexOf(strTopic) > -1)
            {

                string messa = StrSub(message, strTopic);

                string substrt = messa.Substring(strTopic.Length + 1, messa.Length - strTopic.Length - 3);
                string[] subgroup = substrt.Split(new char[] { ',' });
                int logcount = int.Parse(subgroup[0].ToString());
                int logzcount = int.Parse(subgroup[1].ToString());
                int logtype = int.Parse(subgroup[2].ToString());
                int logorder = int.Parse(subgroup[3].ToString());
                List<GSHuatee> list = new List<GSHuatee>();
                Dictionary<string, string> order = new Dictionary<string, string>();
                if (logorder == 1)
                {
                    order.Add("revertCount", "0");
                }
                else
                {
                    order.Add("readCount", "0");
                }
                newpage.PageOrder = order;
                string mess = string.Empty;
                //mess = "<table  width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">";
                if (list.Count < 1)
                {
                    //mess += "  <tr><td>无话题记录</td></tr>";
                    mess += "  <li>无话题记录</li>";
                }
                else
                {
                    int i = 0;
                    foreach (GSHuatee ul in list)
                    {
                        i++;
                        if (i > logcount)
                            break;
                        if (ul.HuaTeeTitle.Length > logzcount)
                        {
                            ul.HuaTeeTitle = ul.HuaTeeTitle.Substring(0, logzcount) + "...";
                        }

                        //mess += "  <tr height=\"46\"><td><a href=\"ShowList.aspx?id=" + id.ToString() + "&cid=" + cid.ToString() + "&action=GroupShow\">" + ul.HuaTeeTitle + "</a>";
                        mess += "  <li><a href=\"ShowList.aspx?id=" + id.ToString() + "&cid=" + cid.ToString() + "&action=GroupTopicShow\">" + ul.HuaTeeTitle + "</li>";
                    }
                }
                //mess += "</table>";

                message = message.Replace(messa, mess);
            }
            #endregion

            #region 话题标题
            string TopicNam = "$Zone_TopicName";
            if (message.IndexOf(TopicNam) > -1)
            {
                string messa = StrSub(message, TopicNam);
                string substrt = messa.Substring(TopicNam.Length + 1, messa.Length - TopicNam.Length - 3);
                string[] subgroup = substrt.Split(new char[] { ',' });
            }
            #endregion

            #region 话题发表时间
            string TopicTime = "$Zone_TopicTime";
            if (message.IndexOf(TopicTime) > -1)
            {
                string messa = StrSub(message, TopicTime);
                string substrt = messa.Substring(TopicTime.Length + 1, messa.Length - TopicTime.Length - 3);
                string[] subgroup = substrt.Split(new char[] { ',' });
            }
            #endregion

            #region 话题内容
            string TopicContent = "$Zone_TopicContent";
            if (message.IndexOf(TopicContent) > -1)
            {
                string messa = StrSub(message, TopicContent);
                string substrt = messa.Substring(TopicContent.Length + 1, messa.Length - TopicContent.Length - 3);
                string[] subgroup = substrt.Split(new char[] { ',' });
            }
            #endregion

            #region 话题回复内容
            string strTopicRe = "$Zone_TopicRe";
            if (message.IndexOf(strTopic) > -1)
            {
                string messa = StrSub(message, strTopicRe);
                string substrt = messa.Substring(strTopicRe.Length + 1, messa.Length - strTopicRe.Length - 3);
                string[] subgroup = substrt.Split(new char[] { ',' });
                List<GSReverCricicism> list = new List<GSReverCricicism>();


                string mess = string.Empty;
                //mess = "<table  width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">";
                if (list.Count < 1)
                {
                    //mess += "  <tr><td>无话题记录</td></tr>";
                    mess += "  <li>无回复记录</li>";
                }
                else
                {
                    foreach (GSReverCricicism ul in list)
                    {

                        //mess += "  <tr height=\"46\"><td><a href=\"ShowList.aspx?id=" + id.ToString() + "&cid=" + cid.ToString() + "&action=GroupShow\">" + ul.HuaTeeTitle + "</a>";
                        mess += "<li>回复人:" + ul.UserName + ",回复时间:" + ul.CreatTime.ToString() + "";
                        mess += "  <li>" + ul.Content + "</li>";
                    }
                }
                //mess += "</table>";

                message = message.Replace(strTopicRe, mess);
            }
            #endregion

            #region 族群成员
            string strGroupUser = "$Zone_GroupUser";
            if (message.IndexOf(strGroupUser) > -1)
            {
                string SubPhoto = StrSub(message, strGroupUser);
                string[] GroupPhoto = SubPhoto.Substring(strGroupUser.Length + 1, SubPhoto.Length - strGroupUser.Length - 3).Split(new char[] { ',' });
                int count = int.Parse(GroupPhoto[0].ToString());
                int counth = int.Parse(GroupPhoto[1].ToString());
                int photow = int.Parse(GroupPhoto[2].ToString());
                int photoh = int.Parse(GroupPhoto[3].ToString());

                List<User_R_GS> list = new List<User_R_GS>();
                Dictionary<string, string> order = new Dictionary<string, string>();
                //if (zoneorder == 1)
                //{
                //    order.Add("CategTime", "0");
                //}
                //else
                //{
                //    order.Add("CategTime", "0");
                //}
                newpage.PageOrder = order;

                string messphoto = string.Empty;
                //messphoto = "<table  width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">";
                if (list.Count < 1)
                {
                    //messphoto += "  <tr><td>无模板记录</td></tr>";
                    messphoto += "  <li>无成员记录</li>";
                }
                else
                {
                    int picount = 0;
                    User_R_GS[] pc = list.ToArray();
                    count = count < list.Count ? count : list.Count;
                    for (int pj = 0; pj < count; )
                    {


                        //messphoto += "<tr>";

                        for (int pi = 0; pi < counth; pi++)
                        {

                            if (picount < count)
                                picount++;
                            else
                                break;

                            //messphoto += "<td><a href=\"#\"><img src='../" + pc[pj].StylePic + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/></a><br><a href=\"#\">" + pc[pj].StyleName + "</a></td>";
                            messphoto += "<li><img src='" + pc[pj].Userpic + "' width=" + photow.ToString() + " height=" + photoh.ToString() + "/></br>" + pc[pj].UserName + "</li>";
                            pj++;
                        }
                        //messphoto += "</tr>";

                    }
                }
                //messphoto += "</table>";
                message = message.Replace(SubPhoto, messphoto);
            }
            #endregion

            #region 好友列表
            string strFriendUser = "$Zone_UserFriend";
            if (message.IndexOf(strFriendUser) > -1)
            {
                string SubPhoto = StrSub(message, strFriendUser);
                string[] GroupPhoto = SubPhoto.Substring(strFriendUser.Length + 1, SubPhoto.Length - strFriendUser.Length - 3).Split(new char[] { ',' });
                int count = int.Parse(GroupPhoto[0].ToString());
                int counth = int.Parse(GroupPhoto[1].ToString());
                int photow = int.Parse(GroupPhoto[2].ToString());
                int photoh = int.Parse(GroupPhoto[3].ToString());

                Dictionary<string, string> order = new Dictionary<string, string>();
                //if (zoneorder == 1)
                //{
                //    order.Add("CategTime", "0");
                //}
                //else
                //{
                //    order.Add("CategTime", "0");
                //}
                newpage.PageOrder = order;

             
            }
            #endregion

            #region 小屋显示
            if (message.IndexOf("$Zone_HomeSet$") > -1)
            {
                string UserBanner = "<iframe src=\"User/UserZone/Home/Homeshow.aspx?shid=" + id.ToString() + "\" frameborder=\"0\" width=\"100%\" height=\"600px\"></iframe>";
                message = message.Replace("$Zone_HomeSet$", UserBanner);
            }
            #endregion

            #region 在线人数
            if (message.IndexOf("$Zone_Online$") > -1)
            {
                //string UserBanner = ubll.GetCountOnLine().ToString();
                message = message.Replace("$Zone_Online$", "0");
            }
            #endregion

            #region 人气排名
            strZoneStyle = "$Zone_UserHot";
            if (message.IndexOf(strZoneStyle) > -1)
            {
                string SubPhoto = StrSub(message, strZoneStyle);
                string[] GroupPhoto = SubPhoto.Substring(strZoneStyle.Length + 1, SubPhoto.Length - strZoneStyle.Length - 3).Split(new char[] { ',' });
                int count = int.Parse(GroupPhoto[0].ToString());
                int counti = int.Parse(GroupPhoto[1].ToString());
                string messphoto = string.Empty;
               
                message = message.Replace(SubPhoto, messphoto);
            }
            #endregion

            #region 替换ID
            if (message.IndexOf("$Zone_ID_Re$") > -1)
            {
                message = message.Replace("$Zone_ID_Re$", cid.ToString());
            }
            #endregion

            #region 快速搜索
            if (message.IndexOf("$Zone_UserSearch$") > -1)
            {
                string UserBanner = "<iframe src=\"Zone/ZoneFriendSearch.aspx\" frameborder=\"0\" width=\"195px\"></iframe>";
                message = message.Replace("$Zone_UserSearch$", UserBanner);
            }
            #endregion

            #region 检查是否还有未替换的
            if (message.IndexOf("$Zone_") > -1)
            {
                message = MessageReplace(message, id, cid, sid);
            }
            #endregion

            return message;
        }
        #endregion

        #region 辅助方法
        public static string StrSub(string message, string ReName)
        {
            int subj = message.IndexOf(ReName);
            string substr = message.Substring(message.IndexOf(ReName) + ReName.Length);
            int subi = substr.IndexOf("$");
            string substrt = substr.Substring(0, subi - 1);
            string messa = message.Substring(subj, subi + ReName.Length + 1);
            return messa;
        }
        #endregion

        #region 系统标签
        public static string GetSysLabel()
        {

            return "";
        }
        #endregion

        #region 用户标签
        public static string GetUserLabel()
        {
            return "";
        }
        #endregion
    }
}

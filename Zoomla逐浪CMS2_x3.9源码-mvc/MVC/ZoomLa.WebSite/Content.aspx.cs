using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

namespace ZoomLaCMS
{
    public partial class Content : FrontPage
    {
        B_CreateHtml bll = new B_CreateHtml();
        B_Content bcontent = new B_Content();
        B_Model bmode = new B_Model();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ItemID < 1) { ErrToClient("[产生错误的可能原因：内容信息不存在或未开放!调用方法：/Item/内容GID.aspx]"); }
            M_CommonData ItemInfo = bcontent.GetCommonData(ItemID);
            if (ItemInfo == null||ItemInfo.IsNull) { ErrToClient("[产生错误的可能原因：["+ ItemID + "]内容信息不存在或未开放!]"); }
            M_ModelInfo modelinfo = bmode.GetModelById(ItemInfo.ModelID);
            M_Node nodeMod = nodeBll.SelReturnModel(ItemInfo.NodeID);
            if (modelinfo == null || modelinfo.IsNull) { ErrToClient("[产生错误的可能原因：(" + ItemInfo.ModelID + ")模型不存在!]"); }
            if (ItemInfo.Status == -2)
            {
                ErrToClient("[当前信息已删除，您无法浏览!]");
            }
            else if (ItemInfo.Status == 0)
            {
                ErrToClient("[当前信息待审核状态，您无法浏览!]");
            }
            else if (ItemInfo.Status != 99)
            {
                ErrToClient("[当前信息未通过审核，您无法浏览!]");
            }
            if (nodeMod.PurviewType)
            {
                if (!buser.CheckLogin())
                {
                    function.WriteErrMsg("该信息所属栏目需登录验证，请先<a href='/User/login.aspx' target='_top'>登录</a>再进行此操作！", "/User/login.aspx");
                }
            }
            //-------------------------End;
            if (nodeMod.ConsumePoint > 0)
            {
                M_UserInfo userinfo = buser.GetLogin();
                int groupID = 0; //会员级别id
                int groupNum = 0; //浏览文章的次数

                if (nodeMod.Viewinglimit != "" || nodeMod.Viewinglimit != null)
                {
                    #region 查找当前登录会员浏览该文章规定的次数
                    string Viewinglimits = nodeMod.Viewinglimit;
                    string[] ViewinglimitArray = Viewinglimits.Split(new char[] { '|' });
                    if (ViewinglimitArray.Length > 1)
                    {
                        for (int i = 0; i < ViewinglimitArray.Length; i++)
                        {
                            if (userinfo.GroupID == int.Parse(ViewinglimitArray[i].Substring(0, ViewinglimitArray[i].IndexOf("="))))
                            {
                                groupID = int.Parse(ViewinglimitArray[i].Substring(0, ViewinglimitArray[i].IndexOf("=")));
                                groupNum = int.Parse(ViewinglimitArray[i].Substring(ViewinglimitArray[i].IndexOf("=") + 1, ViewinglimitArray[i].Length - ViewinglimitArray[i].Substring(0, ViewinglimitArray[i].IndexOf("=") + 1).Length));
                                break;
                            }
                        }
                    }
                    #endregion
                }
                if (buser.CheckLogin() && (userinfo.UserPoint - nodeMod.ConsumePoint) > 0)
                {
                    #region 计费
                    //switch (nodeMod.ConsumeType)
                    //{
                    //    case 0://0-不重复收费
                    //        ReadArticleStandardCharges(userinfo, nodeMod, buser, bcomhistory, ItemInfo.GeneralID, nodeMod.ConsumeType, groupID, groupNum);
                    //        break;
                    //    case 1://1-距离上次收费时间多少小时后重新收费
                    //        ReadArticleStandardCharges(userinfo, nodeMod, buser, bcomhistory, ItemInfo.GeneralID, nodeMod.ConsumeType, groupID, groupNum);
                    //        break;
                    //    case 2://2-重复阅读内容多少次重新收费
                    //        ReadArticleStandardCharges(userinfo, nodeMod, buser, bcomhistory, ItemInfo.GeneralID, nodeMod.ConsumeType, groupID, groupNum);
                    //        break;
                    //    case 3://3-上述两者都满足时重新收费
                    //        ReadArticleStandardCharges(userinfo, nodeMod, buser, bcomhistory, ItemInfo.GeneralID, nodeMod.ConsumeType, groupID, groupNum);
                    //        break;
                    //    case 4://4- 1、2两者任一个满足时就重新收费
                    //        ReadArticleStandardCharges(userinfo, nodeMod, buser, bcomhistory, ItemInfo.GeneralID, nodeMod.ConsumeType, groupID, groupNum);
                    //        break;
                    //    case 5: //5-每阅读一次就重复收费一次
                    //        ReadArticleStandardCharges(userinfo, nodeMod, buser, bcomhistory, ItemInfo.GeneralID, nodeMod.ConsumeType, groupID, groupNum);
                    //        break;
                    //    default:
                    //        ReadArticleStandardCharges(userinfo, nodeMod, buser, bcomhistory, ItemInfo.GeneralID, nodeMod.ConsumeType, groupID, groupNum);//不重复收费
                    //        break;
                    //}
                    #endregion
                }
                else
                {
                    function.WriteErrMsg("您的点券不足,请充值!");
                }
            }
            //自定义模板>节点模板>模型模板
            string TemplateDir=ItemInfo.Template;
            if(string.IsNullOrEmpty(TemplateDir))
            {
                TemplateDir=nodeBll.GetModelTemplate(ItemInfo.NodeID, ItemInfo.ModelID);
            }
            if(string.IsNullOrEmpty(TemplateDir))
            {
                TemplateDir= modelinfo.ContentModule;
            }
            if (string.IsNullOrEmpty(TemplateDir))
            {
                function.WriteErrMsg("该内容所属模型未指定模板");
            }
            else
            {
                TemplateDir = function.VToP(SiteConfig.SiteOption.TemplateDir + "/" + TemplateDir);
                string Templatestrstr = FileSystemObject.ReadFile(TemplateDir);
                string ContentHtml = bll.CreateHtml(Templatestrstr, Cpage, ItemID, "0");//Templatestrstr:模板页面字符串,页码,该文章ID
                /* --------------------判断是否分页 并做处理------------------------------------------------*/
                if (!string.IsNullOrEmpty(ContentHtml))
                {
                    string infoContent = ""; //进行处理的内容字段
                    string pagelabel = "";
                    string infotmp = "";
                    #region 分页符分页
                    string pattern = @"{\#PageCode}([\s\S])*?{\/\#PageCode}";  //查找要分页的内容
                    if (Regex.IsMatch(ContentHtml, pattern, RegexOptions.IgnoreCase))
                    {
                        infoContent = Regex.Match(ContentHtml, pattern, RegexOptions.IgnoreCase).Value;
                        infotmp = infoContent;
                        infoContent = infoContent.Replace("{#PageCode}", "").Replace("{/#PageCode}", "");
                        //查找分页标签
                        bool isPage = false;
                        string pattern1 = @"{ZL\.Page([\s\S])*?\/}";

                        if (Regex.IsMatch(ContentHtml, pattern1, RegexOptions.IgnoreCase))
                        {
                            pagelabel = Regex.Match(ContentHtml, pattern1, RegexOptions.IgnoreCase).Value;
                            isPage = true;
                        }
                        if (isPage)
                        {
                            if (string.IsNullOrEmpty(infoContent)) //没有设定要分页的字段内容
                            {
                                ContentHtml = ContentHtml.Replace(pagelabel, "");
                            }
                            else   //进行内容分页处理
                            {
                                //文件名
                                string file1 = "Content.aspx?ID=" + ItemID.ToString();
                                //取分页标签处理结果 返回字符串数组 根据数组元素个数生成几页 
                                string ilbl = pagelabel.Replace("{ZL.Page ", "").Replace("/}", "").Replace(" ", ",");
                                string lblContent = "";
                                IList<string> ContentArr = new List<string>();

                                if (string.IsNullOrEmpty(ilbl))
                                {
                                    lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                    ContentArr = bll.GetContentPage(infoContent);
                                }
                                else
                                {
                                    string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (paArr.Length == 0)
                                    {
                                        lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                        ContentArr = bll.GetContentPage(infoContent);
                                    }
                                    else
                                    {
                                        string lblname = paArr[0].Split(new char[] { '=' })[1].Replace("\"", "");
                                        B_Label blbl = new B_Label();
                                        lblContent = blbl.GetLabelXML(lblname).Content;
                                        if (string.IsNullOrEmpty(lblContent))
                                        {
                                            lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                        }
                                        ContentArr = bll.GetContentPage(infoContent);
                                    }
                                }
                                if (ContentArr.Count > 0) //存在分页数据
                                {
                                    string curCPage = B_Route.GetParam("cpage", this);
                                    bool isAll = !(string.IsNullOrEmpty(curCPage)) && curCPage.Equals("0");
                                    if (isAll)//必须明确传值,才显示全部
                                    {
                                        ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                                        ContentHtml = ContentHtml.Replace("[PageCode/]", "");
                                    }
                                    else
                                    {
                                        int _cpage = PageHelper.GetCPage(Cpage, 1, ContentArr.Count) - 1;
                                        ContentHtml = ContentHtml.Replace(infotmp, ContentArr[_cpage]);
                                        ContentHtml = ContentHtml.Replace("{#Content}", "").Replace("{/#Content}", "");
                                    }
                                    ContentHtml = ContentHtml.Replace(pagelabel, bll.GetPage(lblContent, ItemID, Cpage, ContentArr.Count, ContentArr.Count));//输出分页
                                }
                                else
                                {
                                    ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                                    ContentHtml = ContentHtml.Replace(pagelabel, "");
                                }
                            }
                        }
                        else  //没有分页标签
                        {
                            //如果设定了分页内容字段 将该字段内容的分页标志清除
                            if (!string.IsNullOrEmpty(infoContent))
                                ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                        }
                    }
                    #endregion
                    #region  查找要分页的内容
                    pattern = @"{\#Content}([\s\S])*?{\/\#Content}";
                    if (Regex.IsMatch(ContentHtml, pattern, RegexOptions.IgnoreCase))
                    {
                        infoContent = Regex.Match(ContentHtml, pattern, RegexOptions.IgnoreCase).Value;
                        infotmp = infoContent;
                        infoContent = infoContent.Replace("{#Content}", "").Replace("{/#Content}", "");
                        //查找分页标签
                        bool isPage = false;
                        string pattern1 = @"{ZL\.Page([\s\S])*?\/}";
                        if (Regex.IsMatch(ContentHtml, pattern1, RegexOptions.IgnoreCase))
                        {
                            pagelabel = Regex.Match(ContentHtml, pattern1, RegexOptions.IgnoreCase).Value;
                            isPage = true;
                        }
                        if (isPage)//包含分页
                        {
                            if (string.IsNullOrEmpty(infoContent)) //没有设定要分页的字段内容
                            {
                                ContentHtml = ContentHtml.Replace(pagelabel, "");
                            }
                            else   //进行内容分页处理
                            {
                                //文件名
                                string file1 = "Content.aspx?ID=" + ItemID.ToString();
                                //取分页标签处理结果 返回字符串数组 根据数组元素个数生成几页 
                                string ilbl = pagelabel.Replace("{ZL.Page ", "").Replace("/}", "").Replace(" ", ",");
                                string lblContent = "";
                                int NumPerPage = 500;
                                IList<string> ContentArr = new List<string>();

                                if (string.IsNullOrEmpty(ilbl))
                                {
                                    lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                    ContentArr = bll.GetContentPage(infoContent, NumPerPage);
                                }
                                else
                                {
                                    string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (paArr.Length == 0)
                                    {
                                        lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                        ContentArr = bll.GetContentPage(infoContent, NumPerPage);
                                    }
                                    else
                                    {
                                        string lblname = paArr[0].Split(new char[] { '=' })[1].Replace("\"", "");
                                        if (paArr.Length > 1)
                                        {
                                            NumPerPage = DataConverter.CLng(paArr[1].Split(new char[] { '=' })[1].Replace("\"", ""));
                                        }
                                        B_Label blbl = new B_Label();
                                        lblContent = blbl.GetLabelXML(lblname).Content;
                                        if (string.IsNullOrEmpty(lblContent))
                                        {
                                            lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                        }
                                        ContentArr = bll.GetContentPage(infoContent, NumPerPage);
                                    }
                                }
                                if (ContentArr.Count > 0) //存在分页数据
                                {
                                    int _cpage = PageHelper.GetCPage(Cpage, 0, ContentArr.Count - 1);
                                    ContentHtml = ContentHtml.Replace(infotmp, ContentArr[_cpage]);
                                    ContentHtml = ContentHtml.Replace(pagelabel, bll.GetPage(lblContent, ItemID, Cpage, ContentArr.Count, NumPerPage));
                                }
                                else
                                {
                                    ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                                    ContentHtml = ContentHtml.Replace(pagelabel, "");
                                }
                            }
                        }
                        else//没有分页标签
                        {
                            //如果设定了分页内容字段 将该字段内容的分页标志清除
                            if (!string.IsNullOrEmpty(infoContent))
                                ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                        }
                    }
                    #endregion
                }
                //替换默认分页标签
                string patterns = @"{ZL\.Page([\s\S])*?\/}";
                string pagelabels = Regex.Match(ContentHtml, patterns, RegexOptions.IgnoreCase).Value;
                if (!string.IsNullOrEmpty(pagelabels)) { ContentHtml = ContentHtml.Replace(pagelabels, ""); }
                if (nodeMod.SafeGuard == 1 && File.Exists(Server.MapPath("/JS/Guard.js"))) { ContentHtml = ContentHtml + SafeSC.ReadFileStr("/JS/Guard.js"); }
                if (SiteConfig.SiteOption.IsSensitivity == 1) { ContentHtml = B_Sensitivity.Process(ContentHtml); }
                Response.Write(ContentHtml);
            }
        }  
    }
}
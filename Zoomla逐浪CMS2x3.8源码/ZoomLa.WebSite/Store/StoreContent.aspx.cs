using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public partial class Store_StoreContent : System.Web.UI.Page
{
    protected B_CreateShopHtml shll = new B_CreateShopHtml();
    protected B_CreateHtml bll = new B_CreateHtml();
    B_Product bcontent = new B_Product();
    B_Model bmode = new B_Model();
    B_Node bnode = new B_Node();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(base.Request.QueryString["ItemID"]))
        {
            int ItemID = DataConverter.CLng(base.Request.QueryString["ItemID"]);
            int CPage = string.IsNullOrEmpty(base.Request.QueryString["page"]) ? 1 : DataConverter.CLng(base.Request.QueryString["page"]);
            if (CPage <= 0)
                CPage = 1;
            M_Product ItemInfo = bcontent.GetproductByid(ItemID);
            if (ItemInfo == null)
            {
                Response.Write("[产生错误的可能原因：您访问的商品信息不存在！]");
                Response.End();
            }
            M_Node nodeinfo = bnode.GetNodeXML(ItemInfo.Nodeid);
            if (nodeinfo.PurviewType)
            {
                if (!buser.CheckLogin())
                {
                    function.WriteErrMsg("该信息所属栏目需登录验证，请先登录再进行此操作！", "/User/login.aspx");
                }
                else
                {
                    //此处以后可以加上用户组权限检测
                }
            }
            string TemplateDir = "";
            //ItemID　商品ＩＤ
            B_Product pll = new B_Product();
            if (pll.SelectProByCmdID(ItemID).Rows.Count < 1)
            {
                function.WriteErrMsg("该商品不存在!");
            }
            int UserID = DataConverter.CLng(pll.SelectProByCmdID(ItemID).Rows[0]["UserID"]);

            B_User ull = new B_User();
            string username = ull.GetUserByUserID(UserID).UserName;
            B_ModelField model = new B_ModelField();
            DataTable mosinfo = model.SelectTableName("ZL_CommonModel", "TableName like 'ZL_Store_%' and Inputer='" + username + "'");
            int GeneralID = DataConverter.CLng(mosinfo.Rows[0]["GeneralID"]);
            B_Content cll = new B_Content();
            DataTable infos = cll.GetContent(GeneralID);
            int StoreStyleID = DataConverter.CLng(infos.Rows[0]["StoreStyleID"]);
            B_StoreStyleTable stll = new B_StoreStyleTable();
            M_StoreStyleTable stinfo = stll.GetStyleByID(DataConverter.CLng(StoreStyleID));
            string ContentStyle = stinfo.ContentStyle;

            M_ModelInfo modelinfo = bmode.GetModelById(ItemInfo.ModelID);
            string TempNode = bnode.GetModelTemplate(ItemInfo.Nodeid, ItemInfo.ModelID);

            if (!string.IsNullOrEmpty(TempNode))
            {
                TemplateDir = TempNode;
            }

            if (!string.IsNullOrEmpty(ContentStyle))
            {
                TemplateDir = ContentStyle;
            }

            if (string.IsNullOrEmpty(TemplateDir))
            {
                Response.Write("[产生错误的可能原因：该商品所属模型未指定模板！]");
            }
            else
            {
                TemplateDir = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/" + TemplateDir;
                TemplateDir = TemplateDir.Replace("/", @"\");

                string ContentHtml = FileSystemObject.ReadFile(TemplateDir);
                //ContentHtml = this.shll.CreateShopHtml(ContentHtml, GeneralID, UserID);
                ContentHtml = this.bll.CreateHtml(ContentHtml, 0, ItemID, "0");
                if (!string.IsNullOrEmpty(ContentHtml))
                {
                    /* --------------------判断是否分页 并做处理------------------------------------------------*/
                    string infoContent = ""; //进行处理的商品字段
                    string pagelabel = "";
                    string infotmp = "";
                    string pattern = @"{\#Content}([\s\S])*?{\/\#Content}";  //查找要分页的商品
                    if (Regex.IsMatch(ContentHtml, pattern, RegexOptions.IgnoreCase))
                    {
                        infoContent = Regex.Match(ContentHtml, pattern, RegexOptions.IgnoreCase).Value;
                        infotmp = infoContent;
                        infoContent = infoContent.Replace("{#Content}", "").Replace("{/#Content}", "");
                    }
                    //查找分页标签
                    //bool flag = false;
                    bool isPage = false;
                    string pattern1 = @"{ZL\.Page([\s\S])*?\/}";
                    if (Regex.IsMatch(ContentHtml, pattern1, RegexOptions.IgnoreCase))
                    {
                        pagelabel = Regex.Match(ContentHtml, pattern1, RegexOptions.IgnoreCase).Value;
                        isPage = true;
                    }
                    if (isPage)
                    {
                        if (string.IsNullOrEmpty(infoContent)) //没有设定要分页的字段商品
                        {
                            ContentHtml = ContentHtml.Replace(pagelabel, "");
                        }
                        else   //进行商品分页处理
                        {
                            //文件名
                            string file1 = "StoreContent.aspx?ItemID=" + ItemID.ToString();
                            //取分页标签处理结果 返回字符串数组 根据数组元素个数生成几页 
                            string ilbl = pagelabel.Replace("{ZL.Page ", "").Replace("/}", "").Replace(" ", ",");
                            string lblContent = "";
                            int NumPerPage = 500;
                            IList<string> ContentArr = new List<string>();
                            if (string.IsNullOrEmpty(ilbl))
                            {
                                lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                ContentArr = this.bll.GetContentPage(infoContent, NumPerPage);
                            }
                            else
                            {
                                string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (paArr.Length == 0)
                                {
                                    lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                    ContentArr = this.bll.GetContentPage(infoContent, NumPerPage);
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
                                    ContentArr = this.bll.GetContentPage(infoContent, NumPerPage);
                                }
                            }
                            //Response.Write(NumPerPage.ToString());
                            //Response.End();
                            if (ContentArr.Count > 0) //存在分页数据
                            {
                                ContentHtml = ContentHtml.Replace(infotmp, ContentArr[CPage - 1]);
                                ContentHtml = ContentHtml.Replace(pagelabel, this.bll.GetPage(lblContent, ItemID, CPage, ContentArr.Count, NumPerPage));
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
                        //如果设定了分页商品字段 将该字段商品的分页标志清除
                        if (!string.IsNullOrEmpty(infoContent))
                            ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                    }
                }
                /*--------------------- 分页商品处理结束-------------------------------------------------------------------------*/
                Response.Write(ContentHtml);
            }
        }
        else
        {
            Response.Write("[产生错误的可能原因：您访问的商品信息不存在！]");
        }
    }
}

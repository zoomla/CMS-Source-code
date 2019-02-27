using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

namespace ZoomLaCMS.Controllers
{
    public class FrontStoreController : Controller
    {
        B_Product proBll = new B_Product();
        B_User buser = new B_User();
        B_CreateHtml createBll = new B_CreateHtml();
        B_Content conBll = new B_Content();
        B_StoreStyleTable sstbll = new B_StoreStyleTable();
        B_CreateShopHtml shBll = new B_CreateShopHtml();
        B_Node nodeBll = new B_Node();
        B_ModelField mfbll = new B_ModelField();
        B_Model modBll = new B_Model();
        public int ItemID { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        public int Cpage { get { return DataConverter.CLng(B_Route.GetParam("page", Request)); } }
        protected int listnum = -1;
        public void ProductShow() { Response.Redirect("/Shop/" + ItemID + ".aspx"); return; }
        public void Shopindex()
        {
            string IndexDir = SiteConfig.SiteOption.ShopTemplate;

            IndexDir = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/" + IndexDir;
            IndexDir = IndexDir.Replace("/", @"\");
            if (!FileSystemObject.IsExist(IndexDir, FsoMethod.File))
                Response.Write("[产生错误的可能原因：内容信息不存在或未开放！]");
            else
            {
                string IndexHtml = FileSystemObject.ReadFile(IndexDir);
                IndexHtml = createBll.CreateHtml(IndexHtml, 0, 0, "0");
                Response.Write(IndexHtml);
            }
        }
        public void Shoplist()
        {
            if (!string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
            {
                int ItemID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
                M_Node nodeinfo = nodeBll.GetNodeXML(ItemID);
                if (nodeinfo.IsNull) { function.WriteErrMsg("产生错误的可能原因：您访问的内容信息不存在");return; }
                string TemplateDir = "";
                if (string.IsNullOrEmpty(nodeinfo.IndexTemplate))
                    TemplateDir = nodeinfo.ListTemplateFile;
                else
                    TemplateDir = nodeinfo.IndexTemplate;

                if (string.IsNullOrEmpty(TemplateDir)) { function.WriteErrMsg("产生错误的可能原因：节点不存在或未绑定模型模板"); return; }
                else
                {
                    TemplateDir = SiteConfig.SiteOption.TemplateDir + "/" + TemplateDir;
                    TemplateDir = base.Request.PhysicalApplicationPath + TemplateDir;
                    TemplateDir = TemplateDir.Replace("/", @"\");
                    string ContentHtml = FileSystemObject.ReadFile(TemplateDir);
                    ContentHtml = this.createBll.CreateHtml(ContentHtml, Cpage, ItemID, "1");
                    Response.Write(ContentHtml);
                }
            }
            else
            {
                Response.Write("[产生错误的可能原因：没有指定栏目ID]");
            }
        }
        public void ShopSearch()
        {
            Response.Redirect("/Search/SearchList?keyword=&Node=" + NodeID);return;
        }
        public void StoreContent()
        {
            if (ItemID < 1) { ErrToClient("[产生错误的可能原因：您访问的商品信息不存在！"); }
            M_Product ItemInfo = proBll.GetproductByid(ItemID);
            if (ItemInfo == null)
            {
                ErrToClient("[产生错误的可能原因：您访问的商品信息不存在！]");return;
            }
            M_Node nodeinfo = nodeBll.GetNodeXML(ItemInfo.Nodeid);
            if (nodeinfo.PurviewType)
            {
                if (!buser.CheckLogin())
                {
                    function.WriteErrMsg("该信息所属栏目需登录验证，请先登录再进行此操作！", "/User/login");return;
                }
                else
                {
                    //此处以后可以加上用户组权限检测
                }
            }
            string TemplateDir = "";
            //ItemID　商品ＩＤ
            if (proBll.SelectProByCmdID(ItemID).Rows.Count < 1)
            {
                function.WriteErrMsg("该商品不存在!");return;
            }
            int UserID = DataConverter.CLng(proBll.SelectProByCmdID(ItemID).Rows[0]["UserID"]);
            string username = buser.GetUserByUserID(UserID).UserName;
            DataTable mosinfo = mfbll.SelectTableName("ZL_CommonModel", "TableName like 'ZL_Store_%' and Inputer='" + username + "'");
            int GeneralID = DataConverter.CLng(mosinfo.Rows[0]["GeneralID"]);

            DataTable infos = conBll.GetContent(GeneralID);
            int StoreStyleID = DataConverter.CLng(infos.Rows[0]["StoreStyleID"]);
            M_StoreStyleTable stinfo = sstbll.GetStyleByID(DataConverter.CLng(StoreStyleID));
            string ContentStyle = stinfo.ContentStyle;
            M_ModelInfo modelinfo = modBll.GetModelById(ItemInfo.ModelID);
            string TempNode = nodeBll.GetModelTemplate(ItemInfo.Nodeid, ItemInfo.ModelID);

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
                ContentHtml = this.createBll.CreateHtml(ContentHtml, 0, ItemID, "0");
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
                                ContentArr = this.createBll.GetContentPage(infoContent, NumPerPage);
                            }
                            else
                            {
                                string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (paArr.Length == 0)
                                {
                                    lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                    ContentArr = this.createBll.GetContentPage(infoContent, NumPerPage);
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
                                    ContentArr = this.createBll.GetContentPage(infoContent, NumPerPage);
                                }
                            }
                            //Response.Write(NumPerPage.ToString());
                            //Response.End();
                            if (ContentArr.Count > 0) //存在分页数据
                            {
                                ContentHtml = ContentHtml.Replace(infotmp, ContentArr[Cpage - 1]);
                                ContentHtml = ContentHtml.Replace(pagelabel, this.createBll.GetPage(lblContent, ItemID, Cpage, ContentArr.Count, NumPerPage));
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
        public void StoreIndex()
        {
            if (ItemID < 1) { function.WriteErrMsg("店铺ID错误,StoreIndex?id=店铺ID");return; }
            M_CommonData cdata = conBll.GetCommonData(ItemID);
            if (cdata == null) {function.WriteErrMsg("店铺信息不存在");return; }
            if (!cdata.IsStore) { function.WriteErrMsg("错误,指定的ID并非店铺"); return; }
            DataTable dt = conBll.GetContent(ItemID);
            if (dt != null && dt.Rows.Count < 1)
            {
                function.WriteErrMsg("该店铺不存在");return;
            }
            else if (cdata.Status == 0)
            {
                function.WriteErrMsg("该店铺被关闭了");return;
            }
            else if (cdata.Status != 99)
            {
                function.WriteErrMsg("该店铺还在审核中");return;
            }
            else
            {
                string username = cdata.Inputer;
                int userid = buser.GetUserByName(username).UserID;
                int StoreStyleID = DataConverter.CLng(dt.Rows[0]["StoreStyleID"]);//店铺风格
                M_StoreStyleTable stinfo = sstbll.GetStyleByID(StoreStyleID);
                string ContentHtml = SafeSC.ReadFileStr(SiteConfig.SiteOption.TemplateDir + "/" + stinfo.StyleUrl);
                ContentHtml = this.createBll.CreateHtml(ContentHtml, 0, ItemID, 0);
                Response.Write(ContentHtml);
            }
        }
        public void Storelist()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int sid = DataConverter.CLng(base.Request.QueryString["id"]);
                if (sid == 0)
                {
                    ErrToClient("店铺ID错误,Storelist.aspx?id=店铺ID");return;
                }
                else
                {
                    M_CommonData cdate = conBll.GetCommonData(sid);
                    DataTable dt = null;
                    try
                    {
                        dt = conBll.GetContent(sid);
                    }
                    catch
                    {
                        ErrToClient("该店铺不存在");
                    }
                    if (dt.Rows.Count < 1)
                    {
                        ErrToClient("该店铺不存在");
                    }
                    else
                    {
                        try
                        {
                            if (cdate.Status != 99)
                            {
                                ErrToClient("该店铺还在审核中");
                            }
                            else if (cdate.Status != 99)
                            {
                                ErrToClient("该店铺没有通过");
                            }
                            else
                            {
                                if (cdate.Status == 0)
                                {
                                    ErrToClient("该店铺被关闭了");
                                }
                                else
                                {
                                    string StoreStyleID = conBll.GetContent(sid).Rows[0]["StoreStyleID"].ToString();
                                    string username = conBll.GetCommonData(sid).Inputer;
                                    int userid = buser.GetUserByName(username).UserID;
                                    M_StoreStyleTable stinfo = sstbll.GetStyleByID(DataConverter.CLng(StoreStyleID));
                                    string ListStyle = stinfo.ListStyle;
                                    string TemplateDir = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/" + ListStyle;
                                    TemplateDir = TemplateDir.Replace("/", @"\");
                                    string ContentHtml = FileSystemObject.ReadFile(TemplateDir);
                                    ContentHtml = this.createBll.CreateHtml(ContentHtml, Cpage, sid, "1");
                                    Response.Write(ContentHtml);
                                }
                            }
                        }
                        catch
                        {
                            ErrToClient("店铺信息读取失败!ID错误!");
                        }
                    }
                }
            }
            else
            {
                ErrToClient("[产生错误的可能原因：没有指定店铺ID]");return;
            }
        }
        public void StoreProductShow()
        {
            Response.Redirect("/Shop/" + ItemID + ".aspx");return;
        }
        public void StoreShop()
        {

        }
        private void ErrToClient(string msg)
        {
            Response.Clear(); Response.Write(msg); Response.Flush(); Response.End(); return;
        }
    }
}

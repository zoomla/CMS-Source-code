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
using ZoomLa.BLL.Page;
using ZoomLa.Model.Page;

public partial class Pagelink : System.Web.UI.Page
{
    protected B_CreateHtml bll = new B_CreateHtml();
    protected B_ModelField mfll = new B_ModelField();
    protected B_PageReg b_PageReg = new B_PageReg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(base.Request.QueryString["ItemID"]))
        {
            B_Content bcontent = new B_Content();
            B_Model bmode = new B_Model();
            B_Node bnode = new B_Node();
            B_Templata tll = new B_Templata();

            int ItemID = DataConverter.CLng(base.Request.QueryString["ItemID"]);
            M_PageReg ItemInfo = b_PageReg.SelReturnModel(ItemID);
            if (ItemInfo.IsNull)
            {
                Response.Write("[产生错误的可能原因：内容信息不存在或未开放！]");
            }

            M_ModelInfo modelinfo = bmode.GetModelById(ItemInfo.ModelID);

            //M_Templata Nodeinfo = tll.Getbyid(ItemInfo.NodeID);
            //string modelist = Nodeinfo.Modelinfo;
            string TempNode = "";
            ////////////////////////////////////////////
            //if (!string.IsNullOrEmpty(modelist))
            //{
            //    if (modelist.IndexOf("|") > 0)
            //    {
            //        if (modelist.IndexOf(",") > 0)
            //        {
            //            string[] modearr = modelist.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //            {
            //                for (int i = 0; i < modearr.Length; i++)
            //                {
            //                    string[] ddaar = modearr[i].Split(new char[] { ',' });
            //                    int modeid = DataConverter.CLng(ddaar[0]);
            //                    if (ItemInfo.ModelID == modeid)
            //                    {
            //                        TempNode = ddaar[1].ToString();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (modelist.IndexOf(",") > 0)
            //        {
            //            string[] ddaar = modelist.Split(new char[] { ',' });
            //            int modeid = DataConverter.CLng(ddaar[0]);
            //            if (ItemInfo.ModelID == modeid)
            //            {
            //                TempNode = ddaar[1].ToString();
            //            }
            //        }
            //    }
            //}
            /////////////////////////////////////////////////
            string TempContent = ItemInfo.Template;
            string TemplateDir = modelinfo.ContentModule;

            //风格
            string pageuser = ItemInfo.UserName;
            DataTable cmdinfo = mfll.SelectTableName("ZL_PageReg", "TableName like 'ZL_Reg_%' and UserName='" + pageuser + "'");

            string templateurl = "/"+modelinfo.ContentModule;

            string styleurl = "";
            int lastindexOf = 0;
            if (templateurl.IndexOf('/') > -1)
            {
                lastindexOf = templateurl.LastIndexOf('/');
            }
            templateurl = styleurl + templateurl.Substring(lastindexOf, templateurl.Length - lastindexOf);

            TemplateDir = templateurl;

            if (!string.IsNullOrEmpty(TempContent))
            {
                int lastindexOfs = 0;
                if (TempContent.IndexOf('/') > -1)
                {
                    lastindexOfs = TempContent.LastIndexOf('/');
                }
                TempContent = styleurl + TempContent.Substring(lastindexOfs, TempContent.Length - lastindexOfs);

                TemplateDir = TempContent;
            }
            if (string.IsNullOrEmpty(TempContent))
            {
                if (!string.IsNullOrEmpty(TempNode))
                    TemplateDir = TempNode;
            }

            if (string.IsNullOrEmpty(TemplateDir))
            {
                Response.Write("[产生错误的可能原因：该内容所属模型未指定模板！]");
            }
            else
            {
                TemplateDir = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + TemplateDir;
                TemplateDir = TemplateDir.Replace("/", @"\");

                //SetCatch("Pagelink" + StringHelper.MD5(TemplateDir), TemplateDir);
                //string ContentHtml = GetCatch("Pagelink" + StringHelper.MD5(TemplateDir)).ToString();
                string ContentHtml = FileSystemObject.ReadFile(TemplateDir);
                ContentHtml = this.bll.CreateHtml(ContentHtml, 0, ItemID, 0);
                Response.Write(ContentHtml);
            }
        }
        else
        {
            Response.Write("[产生错误的可能原因：内容信息不存在或未开放！访问规则：Pagelink.aspx?ItemID=信息ID]");
        }
    }

    /// <summary>
    /// 获得缓存
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetCatch(string key)
    {
        return Cache.Get(key).ToString();
    }

    /// <summary>
    /// 设置文件缓存
    /// </summary>
    /// <param name="key"></param>
    /// <param name="dirvalue"></param>
    private void SetCatch(string key, string dirvalue)
    {
        if (Cache.Get(key) == null || Cache.Get(key).ToString() == "")
        {
            Cache.Insert(key, FileSystemObject.ReadFile(dirvalue), new System.Web.Caching.CacheDependency(dirvalue));
        }
    }
}

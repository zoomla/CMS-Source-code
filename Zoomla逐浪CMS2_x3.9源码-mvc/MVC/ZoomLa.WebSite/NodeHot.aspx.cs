﻿using System;
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
using ZoomLa.Sns;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;

namespace ZoomLaCMS
{
    public partial class NodeHot : FrontPage
    {
        protected B_CreateHtml bll = new B_CreateHtml();
        protected B_Sensitivity sll = new B_Sensitivity();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ItemID < 1) { ErrToClient("[产生错误的可能原因：栏目ID不能为空!]"); }
            M_Node nodeMod = nodeBll.SelReturnModel(ItemID);
            if (nodeMod == null || nodeMod.IsNull) { ErrToClient("[产生错误的可能原因：栏目ID您访问的节点信息不存在!]"); }
            if (string.IsNullOrEmpty(nodeMod.HotinfoTemplate)) { ErrToClient("[产生错误的可能原因：该节点未指定热门信息模板!]"); }
            string vpath = (SiteConfig.SiteOption.TemplateDir + "/" + nodeMod.HotinfoTemplate);
            string TemplateDir = function.VToP(vpath);
            if (!File.Exists(TemplateDir)) { ErrToClient("[产生错误的可能原因：(" + vpath + ")文件不存在]"); }
            string Templatestr = FileSystemObject.ReadFile(TemplateDir);
            string ContentHtml = this.bll.CreateHtml(Templatestr, Cpage, ItemID, "1");
            if (SiteConfig.SiteOption.IsSensitivity == 1) { ContentHtml = B_Sensitivity.Process(ContentHtml); }
            Response.Write(ContentHtml);
        }
    }
}
using System;
using System.Data;
using System.Configuration;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Web;
using ZoomLa.Model;
namespace ZoomLa.BLL
{
    /// <summary>
    /// 生成静态文件的业务逻辑
    /// </summary>
    public class B_Create
    {

        protected B_Model bmodel = new B_Model();
        protected B_Node bnode = new B_Node();
        protected B_Content bContent = new B_Content();
        protected B_CreateHtml bll = new B_CreateHtml();
        protected string SitePhyPath;

        public B_Create()
        {
            this.SitePhyPath = HttpContext.Current.Request.PhysicalApplicationPath;
        }

        public void CreateInfo(int InfoID, int NodeID, int ModelID)
        {
            M_Node nodeinfo = this.bnode.GetNode(NodeID);
            string NodeDir = nodeinfo.NodeDir;
            string TemplateDir = "";
            M_CommonData mdata = this.bContent.GetCommonData(InfoID);
            if (!mdata.IsNull)
            {
                if (!string.IsNullOrEmpty(mdata.Template))
                    TemplateDir = mdata.Template;
                else
                {
                    if (this.bnode.IsExistTemplate(NodeID, ModelID))
                    {
                        TemplateDir = this.bnode.GetModelTemplate(NodeID, ModelID);
                    }
                    else
                    {
                        TemplateDir = this.bmodel.GetModelById(ModelID).ContentModule;
                    }
                }
                if (!string.IsNullOrEmpty(TemplateDir) && nodeinfo.ContentFileEx<3)
                {
                    string infoTitle = mdata.Title;
                    TemplateDir = this.SitePhyPath + SiteConfig.SiteOption.TemplateDir + TemplateDir;
                    TemplateDir = TemplateDir.Replace("/", @"\");
                    string ContentHtml = this.bll.CreateHtml(FileSystemObject.ReadFile(TemplateDir), 0, InfoID);

                    int InfoFileRule = nodeinfo.ContentPageHtmlRule;
                    string InfoFile = "";
                    //内容页文件命名规则
                    switch (InfoFileRule)
                    {
                        case 0:
                            InfoFile = "/" + NodeDir + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + InfoID;
                            break;
                        case 1:
                            InfoFile = "/" + NodeDir + "/" + DateTime.Now.Year + "-" + DateTime.Now.Month + "/" + InfoID;
                            break;
                        case 2:
                            InfoFile = "/" + NodeDir + "/" + InfoID;
                            break;
                        case 3:
                            InfoFile = "/" + NodeDir + "/" + infoTitle;
                            break;
                    }
                    //文件扩展名
                    int InfoFileEx = nodeinfo.ContentFileEx;
                    switch (InfoFileEx)
                    {
                        case 0:
                            InfoFile = InfoFile + ".html";
                            break;
                        case 1:
                            InfoFile = InfoFile + ".htm";
                            break;
                        case 2:
                            InfoFile = InfoFile + ".shtml";
                            break;
                        case 3:
                            InfoFile = InfoFile + ".aspx";
                            break;
                    }
                    string HtmlLink = InfoFile;
                    InfoFile = this.SitePhyPath + InfoFile;
                    InfoFile = InfoFile.Replace("/", @"\");
                    if (FileSystemObject.IsExist(InfoFile, FsoMethod.File))
                    {
                        FileSystemObject.WriteFile(InfoFile, ContentHtml);
                    }
                    else
                    {
                        FileSystemObject.WriteFile(InfoFile, ContentHtml);
                    }
                    this.bContent.UpdateCreate(InfoID, HtmlLink);
                }
            }
        }
        public void CreateIndex()
        {
            string IndexDir = SiteConfig.SiteOption.IndexTemplate;
            IndexDir = this.SitePhyPath + SiteConfig.SiteOption.TemplateDir + IndexDir;
            IndexDir = IndexDir.Replace("/", @"\");
            string IndexHtml = this.bll.CreateHtml(FileSystemObject.ReadFile(IndexDir), 0, 0);
            string objindex = this.SitePhyPath + @"\" + "index.html";
            if (FileSystemObject.IsExist(objindex, FsoMethod.File))
            {
                FileSystemObject.WriteFile(objindex, IndexHtml);
            }
            else
            {
                FileSystemObject.WriteFile(objindex, IndexHtml);
            }
        }
        public void CreateNodePage(int NodeID)
        {
            M_Node nodeinfo = this.bnode.GetNode(NodeID);
            string NodeDir = nodeinfo.NodeDir;
            string TemplateDir = SiteConfig.SiteOption.TemplateDir + nodeinfo.IndexTemplate;
            if (!string.IsNullOrEmpty(TemplateDir) && nodeinfo.ListPageHtmlEx<3)
            {
                TemplateDir = this.SitePhyPath + TemplateDir;
                TemplateDir = TemplateDir.Replace("/", @"\");
                string ContentHtml = this.bll.CreateHtml(FileSystemObject.ReadFile(TemplateDir), 0, NodeID);


                string InfoFile = "/" + NodeDir + "/index";

                //文件扩展名
                int InfoFileEx = nodeinfo.ContentFileEx;
                switch (InfoFileEx)
                {
                    case 0:
                        InfoFile = InfoFile + ".html";
                        break;
                    case 1:
                        InfoFile = InfoFile + ".htm";
                        break;
                    case 2:
                        InfoFile = InfoFile + ".shtml";
                        break;
                    case 3:
                        InfoFile = InfoFile + ".aspx";
                        break;
                }
                nodeinfo.NodeUrl = InfoFile;
                InfoFile = this.SitePhyPath + InfoFile;
                InfoFile = InfoFile.Replace("/", @"\");
                if (FileSystemObject.IsExist(InfoFile, FsoMethod.File))
                {
                    FileSystemObject.WriteFile(InfoFile, ContentHtml);
                }
                else
                {
                    FileSystemObject.WriteFile(InfoFile, ContentHtml);
                }
                this.bnode.UpdateNode(nodeinfo);
            }
        }

        public void CreateSpec(int SpecID)
        {

        }

    }
}
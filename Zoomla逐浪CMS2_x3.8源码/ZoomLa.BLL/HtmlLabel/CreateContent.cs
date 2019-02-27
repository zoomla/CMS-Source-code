using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;

namespace ZoomLa.BLL
{
    public class CreateContent
    {
        private GetRemoteObj remo = new GetRemoteObj();
        private IList<string> FileList = new List<string>();//创建地址字典
        private B_Node nll = new B_Node();
        private B_Content cll = new B_Content();
        private string Pardir = "";
        private string m_SiteMapath;
        private string m_Nodeist;
        private bool m_CreateParentID;
        private string templateDir;
        /// <summary>
        /// 根目录物理路径
        /// </summary>
        public string SiteMapath
        {
            get { return this.m_SiteMapath; }
            set { this.m_SiteMapath = value; }
        }

        public string Nodeist
        {
            get { return this.m_Nodeist; }
            set { this.m_Nodeist = value; }
        }
        /// <summary>
        /// 是否生成子节点
        /// </summary>
        public bool CreateParentID
        {
            get { return this.m_CreateParentID; }
            set { this.m_CreateParentID = value; }
        }

        public bool CreateHtml(int nodeid)
        {
            M_Node Pnid = new M_Node();
            if (nodeid > 0)
            {
                int pid = nll.GetContrarily(nodeid, 5);
                Pnid = nll.GetNodeXML(pid);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(Pnid.SiteConfige);
                XmlNodeList xnl = xml.SelectNodes("SiteConfig");
                templateDir = function.GetXmlNode(xnl, "TemplateDir");
                templateDir = templateDir.Substring(1, templateDir.LastIndexOf("Template") - 1);
                templateDir = templateDir.Replace(@"\", "/");
                templateDir = templateDir.Replace("//", "/");
            }

            if (nll.SelByIDS(this.Nodeist).Rows.Count > 0)
            {
                DataTable ntable = nll.SelByIDS(this.Nodeist);
                for (int i = 0; i < ntable.Rows.Count; i++)
                {
                    string NodeID = ntable.Rows[i]["NodeID"].ToString();
                    DataTable GetContent = cll.GetNodeAri(DataConverter.CLng(NodeID));//ZL_CommonModel nodeid

                    for (int b = 0; b < GetContent.Rows.Count; b++)
                    {
                        int GeneralID = DataConverter.CLng(GetContent.Rows[b]["GeneralID"].ToString());

                        SetCreateContent(DataConverter.CLng(NodeID), GeneralID);
                    }
                }
            }

            return false;
        }

        private void SetCreateContent(int NodeID, int GeneralID)
        {
            string ContentPath = MakeHtmlFile(nll.GetNodeXML(NodeID)) + GetPath(NodeID + "", GeneralID);
            ContentPath = ContentPath.Replace(@"\//", @"\");
            ContentPath = ContentPath.Replace("/", @"\");

            string readpath = templateDir + "Content.aspx?itemid=" + GeneralID.ToString();
            SaveHtmlLink(DataConverter.CLng(GeneralID), "/Site" + nll.GetDir(DataConverter.CLng(NodeID), "") + GetPath(NodeID + "", GeneralID));

            string NewContent = MakeContentPage(NodeID + "", readpath);
            //end
            FileSystemObject.WriteFile(ContentPath, NewContent);
        }

        /// <summary>
        /// 按日期发布内容
        /// </summary>
        /// <param name="nodeid"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <returns></returns>
        public bool CreateHtml(int nodeid, DateTime stime, DateTime etime)
        {

            M_Node Pnid = new M_Node();
            if (nodeid > 0)
            {
                int pid = nll.GetContrarily(nodeid, 5);
                Pnid = nll.GetNodeXML(pid);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(Pnid.SiteConfige);
                XmlNodeList xnl = xml.SelectNodes("SiteConfig");
                templateDir = function.GetXmlNode(xnl, "TemplateDir");
                templateDir = templateDir.Substring(1, templateDir.LastIndexOf("Template") - 1);
                templateDir = templateDir.Replace(@"\", "/");
                templateDir = templateDir.Replace("//", "/");
            }
            int count = 0;
            if (nll.SelByIDS(this.Nodeist).Rows.Count > 0)
            {
                DataTable ntable = nll.SelByIDS(this.Nodeist);  //zl_node nodeid
                for (int i = 0; i < ntable.Rows.Count; i++)
                {
                    string NodeID = ntable.Rows[i]["NodeID"].ToString();
                    DataTable GetContent = cll.GetNodeAri(DataConverter.CLng(NodeID));//ZL_CommonModel nodeid

                    for (int b = 0; b < GetContent.Rows.Count; b++)
                    {
                        int GeneralID = DataConverter.CLng(GetContent.Rows[b]["GeneralID"].ToString());
                        M_CommonData mcd = cll.GetCommonData(GeneralID);
                        if (mcd.CreateTime > stime && mcd.CreateTime < etime)
                        {
                            string ContentPath = MakeHtmlFile(nll.GetNodeXML(DataConverter.CLng(NodeID))) + GetPath(NodeID, GeneralID);
                            ContentPath = ContentPath.Replace(@"\//", @"\");
                            ContentPath = ContentPath.Replace("/", @"\");

                            string readpath = templateDir + "Content.aspx?itemid=" + GeneralID.ToString();

                            SaveHtmlLink(DataConverter.CLng(GeneralID), "/Site" + nll.GetDir(DataConverter.CLng(NodeID), "") + GetPath(NodeID, GeneralID));
                            string NewContent = MakeContentPage(NodeID, readpath);
                            FileSystemObject.WriteFile(ContentPath, NewContent);
                            count++;
                        }
                    }
                }
            }
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SaveHtmlLink(int GeneralID, string dir)
        {
            B_Content bc = new B_Content();
            M_CommonData mc = bc.GetCommonData(GeneralID);
            mc.HtmlLink = dir;
            bc.UpdateByID(mc);
        }

        /// <summary>
        /// 生成内容页静态文件
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="readpath"></param>
        /// <returns></returns>
        private string MakeContentPage(string NodeID, string readpath)
        {
            string netadd = SiteConfig.SiteInfo.SiteUrl + "/" + readpath;
            string NewContent = remo.GetRemoteHtmlCode(netadd);//远程代码
            NewContent = GetPagefunction(NewContent);

            //内容分页
            //.......
            try
            {
                Regex regexObj = new Regex(@"Content\.aspx\?itemid=?\b\d+\b&p=?\b\d+\b");
                Match matchResults = regexObj.Match(NewContent);


                ////////////////////////////////测试写文件////////////////////////////////////////
                //string xfile = System.Web.HttpContext.Current.Server.MapPath("/bbbb.txt");
                //if (FileSystemObject.IsExist(xfile, FsoMethod.File))
                //{
                //    FileSystemObject.Create(xfile, FsoMethod.File);
                //}
                //FileSystemObject.WriteAppend(xfile, NewContent);
                ////////////////////////////////////////////////////////////////////////////////////


                IList<string> Pagelist = new List<string>();

                while (matchResults.Success)
                {
                    string urlstr = matchResults.Value;
                    if (Pagelist.IndexOf(urlstr) > -1)
                    {
                        Pagelist.Add(urlstr);

                        if (urlstr.IndexOf("=") > -1)
                        {
                            string[] strlink = urlstr.Split(new string[] { "=" }, StringSplitOptions.None);
                            if (strlink.Length > 0)
                            {
                                int pageno = DataConverter.CLng(strlink[2].ToString());
                                int pageitemid = DataConverter.CLng(strlink[1].ToString().Replace("&p", ""));
                                string pageurlstr = pageno + "_" + pageitemid + GetNodeContentEx(nll.GetNodeXML(DataConverter.CLng(NodeID)));
                                string makepath = GetPath(NodeID, pageitemid);//生成路径
                                NewContent = NewContent.Replace(matchResults.Value, pageurlstr);
                            }
                        }
                        string siteurl = this.SiteMapath + "/" + matchResults.Value;
                        string pageurlcontent = remo.GetRemoteHtmlCode(siteurl);//内容分页代码

                        pageurlcontent = GetPagefunction(pageurlcontent);//替换链接地址
                        //替换内容分页地址
                        MakeContentPage(NodeID, matchResults.Value);// matchResults.Value
                    }
                    matchResults = matchResults.NextMatch();


                }
            }
            catch
            {
                // Syntax error in the regular expression
            }

            return NewContent;
        }

        /// <summary>
        /// 生成文件所在目录
        /// </summary>
        /// <param name="nodeid"></param>
        /// <param name="Nodeinfo"></param>
        /// <param name="HtmlPosition"></param>
        private string MakeHtmlFile(M_Node Nodeinfo)
        {
            string allFolder = "";
            //获得节点目录路径
            switch (Nodeinfo.HtmlPosition)
            {
                case 0:
                    allFolder = "/site";//0-根目录下
                    break;
                case 1:
                    Pardir = "/site" + nll.GetDir(Nodeinfo.ParentID, "");//继承父节点目录
                    allFolder = Pardir;
                    break;
            }
            //end
            allFolder = this.SiteMapath + "/" + allFolder + "/" + Nodeinfo.NodeDir;

            if (!FileSystemObject.IsExist(allFolder, FsoMethod.Folder))
            {
                FileSystemObject.CreateFileFolder(allFolder);
            }
            return allFolder;//返回路径
        }

        /// <summary>
        /// 获得父路径
        /// </summary>
        /// <param name="Nodeinfo"></param>
        private string GetParentDir(M_Node Nodeinfo)
        {
            if (Nodeinfo.Depth > 0)
            {
                if (Nodeinfo.ParentID > 0)
                {
                    int ParentID = Nodeinfo.ParentID;
                    M_Node nodesinfo = nll.GetNodeXML(ParentID);
                    string Nodedir = nodesinfo.NodeDir;
                    Pardir = GetParentDir(nodesinfo) + "/" + Pardir + Nodedir;
                }
            }
            return Pardir;
        }

        /// <summary>
        /// 获得内容生成路径
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="GeneralID"></param>
        /// <returns></returns>
        private string GetPath(string NodeID, int GeneralID)
        {
            M_Node Nodeinfo = nll.GetNodeXML(DataConverter.CLng(NodeID));
            string ContentFileEx = GetNodeContentEx(Nodeinfo);//内容页文件扩展名
            string ContentRoot = "";
            M_CommonData Cdata = cll.GetCommonData(GeneralID);

            int ContentPageHtmlRule = Nodeinfo.ContentPageHtmlRule;//内容页文件名规则
            switch (ContentPageHtmlRule)
            {
                case 0:
                    string filepath = MakeHtmlFile(Nodeinfo) + "/" + Cdata.CreateTime.Year + "/" + Cdata.CreateTime.Month + "/" + Cdata.CreateTime.Day;
                    if (!FileSystemObject.IsExist(filepath, FsoMethod.Folder))
                    {
                        FileSystemObject.CreateFileFolder(filepath);
                    }
                    ContentRoot = "/" + Cdata.CreateTime.Year + "/" + Cdata.CreateTime.Month + "/" + Cdata.CreateTime.Date.ToShortDateString() + "/" + Cdata.GeneralID.ToString();
                    break;
                case 1:
                    filepath = MakeHtmlFile(Nodeinfo) + "/" + Cdata.CreateTime.Year + "-" + Cdata.CreateTime.Month;
                    if (!FileSystemObject.IsExist(filepath, FsoMethod.Folder))
                    {
                        FileSystemObject.CreateFileFolder(filepath);
                    }
                    ContentRoot = filepath + Cdata.GeneralID.ToString();
                    break;
                case 2:
                    ContentRoot = "/" + Cdata.GeneralID.ToString();
                    break;
                case 3:
                    filepath = MakeHtmlFile(Nodeinfo) + "/" + Cdata.CreateTime.Year + Cdata.CreateTime.Month + Cdata.CreateTime.Day;
                    if (!FileSystemObject.IsExist(filepath, FsoMethod.Folder))
                    {
                        FileSystemObject.CreateFileFolder(filepath);
                    }
                    ContentRoot = "/" + Cdata.CreateTime.Year + Cdata.CreateTime.Month + Cdata.CreateTime.Day + "/" + Cdata.Title;
                    break;
            }
            return ContentRoot + "." + GetNodeContentEx(Nodeinfo);
        }

        /// <summary>
        /// 提取内容替换链接
        /// </summary>
        /// <param name="DefaultContent"></param>
        private string GetPagefunction(string DefaultContent)
        {
            string Defaultxt = DefaultContent;
            MatchCollection allMatchResults = null;
            try
            {
                Regex regexObj = new Regex(@"http:\/\/\S*(=\b\d+\b)|(ColumnList\.aspx\?NodeID=?\b\d+\b)|(Content\.aspx\?ItemID=?\b\d+\b)");
                allMatchResults = regexObj.Matches(DefaultContent);
                if (allMatchResults.Count > 0)
                {
                    for (int i = 0; i < allMatchResults.Count; i++)
                    {
                        string results = allMatchResults[i].ToString();
                        if (results.ToLower().IndexOf(".aspx") > -1)
                        {
                            if (GetlocalUrl(results))//是否为本站链接
                            {
                                Defaultxt = GetNodeUrl(Defaultxt, results);//替换节点路径
                                Defaultxt = GetContentUrl(Defaultxt, results);//替换内容路径
                                FileList.Add(results);//将地址加入地址字典
                            }
                        }
                    }
                }
                else
                {
                    // Match attempt failed
                }
            }
            catch
            {
                // Syntax error in the regular expression
            }
            return Defaultxt;
        }

        /// <summary>
        /// 替换节点路径
        /// </summary>
        /// <param name="Defaultxt"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private string GetNodeUrl(string Defaultxt, string results)
        {
            if (results.IndexOf("NodeID=") > -1)
            {
                string[] listarr = results.Split(new string[] { "NodeID=" }, StringSplitOptions.None);
                if (listarr.Length > 0)
                {
                    int nodeid = DataConverter.CLng(listarr[1].ToString());
                    string nodeurl = nll.GetNodeXML(nodeid).NodeListUrl;
                    if (nodeurl != "")
                    {
                        Defaultxt = Defaultxt.Replace(results, nodeurl);
                    }
                    else
                    {
                        CreateList cl = new CreateList();
                        cl.SiteMapath = this.SiteMapath;
                        cl.Nodeist = nodeid.ToString();
                        cl.CreateParentID = this.CreateParentID;
                        cl.CreateListFile(nodeid);
                        nodeurl = nll.GetNodeXML(nodeid).NodeListUrl;
                        Defaultxt = Defaultxt.Replace(results, nodeurl);
                    }
                }
            }
            return Defaultxt;
        }

        /// <summary>
        /// 获得内容页地址
        /// </summary>
        /// <param name="Defaultxt"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private string GetContentUrl(string Defaultxt, string results)
        {
            if (results.IndexOf("ItemID=") > -1)
            {
                string[] listarr = results.Split(new string[] { "ItemID=" }, StringSplitOptions.None);
                if (listarr.Length > 0)
                {
                    int ItemID = DataConverter.CLng(listarr[1].ToString());
                    M_CommonData cd = cll.GetCommonData(ItemID);
                    string Htmllink = cd.HtmlLink;
                    if (Htmllink != "")
                    {
                        Defaultxt = Defaultxt.Replace(results, Htmllink);
                    }
                    else
                    {
                        SetCreateContent(cd.NodeID, ItemID);
                        Defaultxt = Defaultxt.Replace(results, cll.GetCommonData(ItemID).HtmlLink);
                    }
                }
            }
            return Defaultxt;
        }

        /// <summary>
        /// 是否为本站路径
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        private bool GetlocalUrl(string results)
        {
            string siteurl = SiteConfig.SiteInfo.SiteUrl;
            if (results.ToLower().IndexOf("http://") > -1)
            {
                if (results.ToLower().IndexOf(siteurl.ToLower()) > -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 列表首页扩展名
        /// </summary>
        /// <param name="Nodeinfo"></param>
        /// <returns></returns>
        private static string GetNodeIndexEx(M_Node Nodeinfo)
        {
            int ListPageHtmlEx = Nodeinfo.ListPageHtmlEx;//列表首页扩展名   0-html 1-htm 2-shtml 3-aspx
            string FileEx = "htm";
            switch (ListPageHtmlEx)
            {
                case 0:
                    FileEx = "html";
                    break;
                case 1:
                    FileEx = "htm";
                    break;
                case 2:
                    FileEx = "shtml";
                    break;
                case 3:
                    FileEx = "aspx";
                    break;
            }
            return FileEx;
        }


        /// <summary>
        /// 内容页文件扩展名
        /// </summary>
        /// <param name="Nodeinfo"></param>
        /// <returns></returns>
        private static string GetNodeContentEx(M_Node Nodeinfo)
        {
            int ListPageHtmlEx = Nodeinfo.ContentFileEx;//内容页文件扩展名   0-html 1-htm 2-shtml 3-aspx
            string FileEx = "htm";
            switch (ListPageHtmlEx)
            {
                case 0:
                    FileEx = "html";
                    break;
                case 1:
                    FileEx = "htm";
                    break;
                case 2:
                    FileEx = "shtml";
                    break;
                case 3:
                    FileEx = "aspx";
                    break;
            }
            return FileEx;
        }
    }
}
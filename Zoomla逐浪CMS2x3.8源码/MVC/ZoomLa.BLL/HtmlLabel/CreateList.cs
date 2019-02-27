using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;

namespace ZoomLa.BLL
{
    public class CreateList
    {
        public CreateList()
        {
            /// 构造函数
        }
        private GetRemoteObj remo = new GetRemoteObj();
        private B_Node nll = new B_Node();
        private IList<string> FileList = new List<string>();//创建地址字典
        private B_Content cll = new B_Content();
        private string m_SiteMapath;
        private string m_Nodeist;
        private bool m_CreateParentID;
        private string Pardir = "";
        private string dir = "";
        /// <summary>
        /// 根目录物理路径
        /// </summary>
        public string SiteMapath
        {
            get { return this.m_SiteMapath; }
            set { this.m_SiteMapath = value; }
        }
        /// <summary>
        /// 节点列表
        /// </summary>
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

        /// <summary>
        /// 生成栏目页静态文件
        /// </summary>
        /// <param name="Noidlist">节点列表文件 逗号隔开</param>
        /// <param name="CreateParentID">是否生成子节点</param>
        /// <returns></returns>
        public bool CreateListFile(int nodeid)
        {
            XmlDocument xml = new XmlDocument();
            XmlNodeList xnl;
            if (nodeid > 0)
            {
                int pid = nll.SelFirstNodeID(nodeid);
                M_Node Pnid = nll.SelReturnModel(pid);
                if (!string.IsNullOrEmpty(Pnid.SiteConfige))
                {
                    xml.LoadXml(Pnid.SiteConfige);
                    xnl = xml.SelectNodes("SiteConfig");
                    dir = function.GetXmlNode(xnl, "TemplateDir");
                    dir = dir.Substring(1, dir.LastIndexOf("Template") - 1);
                    dir = dir.Replace(@"\", "/");
                    dir = dir.Replace("//", "/");
                    dir = "/" + dir;
                    DataTable ntable = nll.SelByIDS(this.Nodeist);//生成静态的节点
                    if (ntable != null && ntable.Rows.Count > 0)
                    {
                        SetCreateList(ntable);
                    }
                }
            }
            return false;
        }

        private void SetCreateList(DataTable ntable)
        {
            //先将文件都记录为静态文件，再生成文件
            for (int ii = 0; ii < ntable.Rows.Count; ii++)
            {
                int NodeID = DataConverter.CLng(ntable.Rows[ii]["NodeID"].ToString());
                M_Node nodeinfos = nll.SelReturnModel(NodeID);
                nodeinfos.NodeListUrl = nodeinfos.NodeDir + "/index." + GetNodeIndexEx(nodeinfos);
                nll.UpdateNode(nodeinfos);
            }
            // xnl = xml.SelectNodes("SiteInfo");
            for (int i = 0; i < ntable.Rows.Count; i++)
            {
                int NodeID = DataConverter.CLng(ntable.Rows[i]["NodeID"].ToString());
                int NodeType = DataConverter.CLng(ntable.Rows[i]["NodeType"].ToString());
                string NodeLinkUrl = "";
                switch (NodeType)
                {
                    case 1://栏目节点
                        NodeLinkUrl = SiteConfig.SiteInfo.SiteUrl + dir + "NodePage.aspx?NodeID=" + NodeID;
                        break;
                    case 2://单页节点
                        NodeLinkUrl = SiteConfig.SiteInfo.SiteUrl + dir + "ColumnList.aspx=" + NodeID;
                        break;
                }
                //System.Web.HttpContext.Current.Response.Write(NodeLinkUrl);
                //System.Web.HttpContext.Current.Response.End();
                GetReCreate(this.CreateParentID, NodeID, NodeLinkUrl);//生成文件并递归
            }
        }

        /// <summary>
        /// 递归调用参数生成栏目
        /// </summary>
        public bool CreateListFile(string Nodeist, bool CreateParentID)
        {
            DataTable ntable = nll.SelByIDS(Nodeist);
            for (int i = 0; i < ntable.Rows.Count; i++)
            {
                int NodeID = DataConverter.CLng(ntable.Rows[i]["NodeID"].ToString());
                int NodeType = DataConverter.CLng(ntable.Rows[i]["NodeType"].ToString());

                string NodeLinkUrl = "";
                switch (NodeType)
                {
                    case 1://栏目节点
                        NodeLinkUrl = SiteConfig.SiteInfo.SiteUrl + "/NodePage.aspx?NodeID=" + NodeID + "p=1";
                        break;
                    case 2://单页节点
                        NodeLinkUrl = SiteConfig.SiteInfo.SiteUrl + "/ColumnList.aspx=" + NodeID + "p=1";
                        break;
                }
                GetReCreate(CreateParentID, NodeID, NodeLinkUrl);//生成文件并递归
            }
            return false;
        }

        /// <summary>
        /// 生成文件并递归
        /// </summary>
        /// <param name="CreateParentID">是否生成子节点静态</param>
        /// <param name="NodeID">节点ID</param>
        /// <param name="NodeLinkUrl">节点物理链接地址</param>
        private void GetReCreate(bool CreateParentID, int NodeID, string NodeLinkUrl)
        {
            if (FileList.IndexOf(NodeLinkUrl) == -1 && NodeLinkUrl.IndexOf("http:") == 0)
            {
                FileList.Add(NodeLinkUrl);
                CreateFile(NodeLinkUrl, NodeID);//生成节点静态文件

                if (CreateParentID)
                {
                    DataTable NodePlist = nll.SelByPid(NodeID);
                    for (int p = 0; p < NodePlist.Rows.Count; p++)
                    {
                        CreateListFile(NodePlist.Rows[p]["NodeID"].ToString(), CreateParentID);//递归生成，节点递归
                    }
                }
            }
        }

        /// <summary>
        /// 分析节点静态文件
        /// </summary>
        /// <param name="url">动态连接地址</param>
        /// <param name="nodeid">节点ID</param>
        /// <returns></returns>
        public bool CreateFile(string url, int nodeid)
        {
            string ListContent = remo.GetRemoteHtmlCode(url);//获得节点内容
            M_Node Nodeinfo = nll.SelReturnModel(nodeid);
            string NodeIndexEx = GetNodeIndexEx(Nodeinfo);//列表首页扩展名
            int HtmlPosition = Nodeinfo.HtmlPosition;//节点目录生成位置     0-根目录下 1-继承父节点目录
            MakeHtmlFile(Nodeinfo);//生成文件所在目录
            return false;
        }


        /// <summary>
        /// 生成文件所在目录
        /// </summary>
        /// <param name="Nodeinfo"></param>
        private void MakeHtmlFile(M_Node Nodeinfo)
        {
            string allFolder = "";
            //获得节点目录路径
            string url = "/site";
            switch (Nodeinfo.HtmlPosition)
            {
                case 0:
                    allFolder = url;//0-根目录下
                    break;
                case 1:
                    Pardir = url + nll.GetDir(Nodeinfo.ParentID, "");//继承父节点目录
                    allFolder = Pardir;
                    break;
            }
            //end
            allFolder = this.SiteMapath + "/" + allFolder + "/" + Nodeinfo.NodeDir;
            if (!FileSystemObject.IsExist(allFolder, FsoMethod.Folder))
            {
                FileSystemObject.CreateFileFolder(allFolder);
            }

            MakePage(Nodeinfo, allFolder);//生成静态分页
        }

        /// <summary>
        /// 生成分页
        /// </summary>
        /// <param name="Nodeinfo"></param>
        /// <param name="allFolder"></param>
        private void MakePage(M_Node Nodeinfo, string allFolder)
        {
            IList<string> Nodelists = new List<string>();
            IList<string> Nodeliststs = new List<string>();

            //System.Web.HttpContext.Current.Response.Write(Nodeinfo.NodeType);
            //System.Web.HttpContext.Current.Response.End();
            switch (Nodeinfo.NodeType)
            {
                case 1://栏目节点

                    //节点分页(包含分页)
                    string NodelistTemp = SiteConfig.SiteInfo.SiteUrl + dir + "NodePage.aspx?NodeID=" + Nodeinfo.NodeID + "&p=1";//生成列表页
                    string nodelistcontent = remo.GetRemoteHtmlCode(NodelistTemp);

                    nodelistcontent = GetPageCode(Nodeinfo, Nodelists, nodelistcontent);//分析分页，将分页地址换成静态地址
                    nodelistcontent = GetPagefunction(nodelistcontent);//替换连接

                    string nodelisturl = (this.SiteMapath + (Pardir.Contains("/site/") ? Pardir : Pardir + "/Site/") + "/" + Nodeinfo.NodeListUrl).Replace(@"\\", @"\").Replace(@"/\", @"\").Replace(@"\/", @"\").Replace(@"\", "/");
                    string[] urlarr = nodelisturl.Split('/');
                    if (urlarr.Length > 1 && urlarr[urlarr.Length - 1].LastIndexOf('.') > 0)
                    {
                        FileSystemObject.WriteFile(nodelisturl, nodelistcontent);
                    }
                    for (int c = 0; c < Nodelists.Count; c++)
                    {
                        CreateFile(Nodelists[c], Nodeinfo.NodeID);
                    }

                    MakeColumnlist(Nodeinfo, allFolder);
                    //end
                    break;
                case 2://单页节点
                    NodelistTemp = SiteConfig.SiteInfo.SiteUrl + dir + "ColumnList.aspx?NodeID=" + Nodeinfo.NodeID + "&p=1";//生成列表页
                    nodelistcontent = remo.GetRemoteHtmlCode(NodelistTemp);

                    nodelistcontent = GetPageCode(Nodeinfo, Nodeliststs, nodelistcontent);//分析分页，将分页地址换成静态地址
                    nodelistcontent = GetPagefunction(nodelistcontent);//替换连接

                    nodelisturl = this.SiteMapath + "/" + (Pardir.Contains("/Site/") ? Pardir : Pardir + "/Site/") + "/" + Nodeinfo.NodeListUrl;


                    FileSystemObject.WriteFile(nodelisturl, nodelistcontent);

                    for (int c = 0; c < Nodeliststs.Count; c++)
                    {
                        CreateFile(Nodeliststs[c], Nodeinfo.NodeID);
                    }

                    MakeColumnlist(Nodeinfo, allFolder);
                    break;
            }
        }

        /// <summary>
        /// 分析分页，将分页地址换成静态地址
        /// </summary>
        /// <param name="Nodeinfo"></param>
        /// <param name="Nodelists"></param>
        /// <param name="nodelistcontent"></param>
        /// <returns></returns>
        private string GetPageCode(M_Node Nodeinfo, IList<string> Nodelists, string nodelistcontent)
        {
            try
            {
                Regex regexObj = new Regex(@"ColumnList\.aspx\?NodeID=?\b\d+\b&p=\b\d+\b");
                Match matchResults = regexObj.Match(nodelistcontent);

                while (matchResults.Success)
                {
                    string matchsxt = matchResults.Value;

                    if (Nodelists.IndexOf(matchsxt) == -1)
                    {
                        Nodelists.Add(matchsxt);
                        if (matchsxt.IndexOf("&") > -1)
                        {
                            string[] matcharr = matchsxt.Split(new string[] { "&" }, StringSplitOptions.None);
                            if (matcharr.Length > 0)
                            {
                                if (matcharr[0].IndexOf("=") > -1)//取值
                                {
                                    string[] matchchar = matcharr[0].Split(new string[] { "=" }, StringSplitOptions.None);
                                    if (matchchar.Length > 1)
                                    {
                                        string urlnodeid = matchchar[1].ToString();
                                        string txturl = matcharr[1].ToString();
                                        if (txturl != "")
                                        {
                                            txturl = txturl.Replace("p=", "");
                                            int Pnum = DataConverter.CLng(txturl);
                                            M_Node mn = nll.SelReturnModel(nll.SelFirstNodeID(Nodeinfo.NodeID));
                                            string fileindex = nll.GetDir(Nodeinfo.NodeID, "").Substring(nll.GetDir(mn.NodeID, "").Length + 1);


                                            if (Pnum > 1)//从第二开始获取
                                            {

                                                string Filename = fileindex + "/pagelist_" + urlnodeid + "_" + txturl + "." + GetNodeIndexEx(Nodeinfo);//静态节点分页地址
                                                string filedir = this.SiteMapath + "Site" + nll.GetDir(Nodeinfo.NodeID, "") + "/pagelist_" + urlnodeid + "_" + txturl + "." + GetNodeIndexEx(Nodeinfo);

                                                if (!FileSystemObject.IsExist(filedir, FsoMethod.File))
                                                {
                                                    MakeColumnlist(Nodeinfo, filedir, (SiteConfig.SiteInfo.SiteUrl + dir + matchsxt), Nodelists);
                                                }
                                                //nll.GetDir(Nodeinfo.NodeID);
                                                //System.Web.HttpContext.Current.Response.Write(this.SiteMapath + "Site" + nll.GetDir(Nodeinfo.NodeID, "") + "/pagelist_" + urlnodeid + "_" + txturl + "_" + GetNodeIndexEx(Nodeinfo) + "<br />");

                                                nodelistcontent = nodelistcontent.Replace(matchsxt, Filename);//替换分页地址
                                            }
                                            else
                                            {
                                                nodelistcontent = nodelistcontent.Replace(matchsxt, fileindex + "/index." + GetNodeIndexEx(Nodeinfo));//替换分页地址
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    matchResults = matchResults.NextMatch();
                }
            }
            catch 
            {
                // Syntax error in the regular expression
            }
            return nodelistcontent;
        }





        /// <summary>
        /// 提取内容替换链接
        /// </summary>
        /// <param name="DefaultContent"></param>
        private string GetPagefunction(string DefaultContent)
        {
            string Defaultxt = DefaultContent;
            IList<string> Filelists = new List<string>();
            MatchCollection allMatchResults = null;
            try
            {
                Regex regexObj = new Regex(@"http:\/\/\S*(=\b\d+\b)|(ColumnList\.aspx\?NodeID=http:\/\/\S*(=\b\d+\b)|(ColumnList\.aspx\?NodeID=?\b\d+\b)|(Content\.aspx\?ItemID=?\b\d+\b))|(Content\.aspx\?ItemID=?\b\d+\b)");
                allMatchResults = regexObj.Matches(DefaultContent);
                if (allMatchResults.Count > 0)
                {
                    for (int i = 0; i < allMatchResults.Count; i++)
                    {
                        string results = allMatchResults[i].ToString();
                        if (results.ToLower().IndexOf(".aspx") > -1 && Filelists.IndexOf(results) == -1)
                        {
                            if (GetlocalUrl(results))//是否为本站链接
                            {
                                Defaultxt = GetNodeUrl(Defaultxt, results);//替换节点路径
                                Defaultxt = GetContentUrl(Defaultxt, results);//替换内容路径
                                Filelists.Add(results);//将地址加入地址字典
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
            if (results.ToLower().IndexOf("nodeid=") > -1)
            {
                string[] listarr = results.ToLower().Split(new string[] { "nodeid=" }, StringSplitOptions.None);
                if (listarr.Length > 0)
                {
                    int nodeid = DataConverter.CLng(listarr[1].ToString());
                    string nodeurl = nll.SelReturnModel(nodeid).NodeListUrl;
                    if (!string.IsNullOrEmpty(nodeurl))
                    {
                        if(FileSystemObject.IsExist(System.Web.HttpContext.Current.Server.MapPath(nodeurl),FsoMethod.File))
                        {
                            Defaultxt = Defaultxt.Replace(results, nodeurl);
                        }
                        else
                        {
                            DataTable ntable = nll.SelByIDS(nodeid+"");
                            if (ntable != null && ntable.Rows.Count > 0)
                            {
                                SetCreateList(ntable);
                            }
                            Defaultxt = Defaultxt.Replace(results,nll.SelReturnModel(nodeid).NodeListUrl);
                        }
                    }
                    else
                    {
                        DataTable ntable = nll.SelByIDS(nodeid + "");
                        if (ntable != null && ntable.Rows.Count > 0)
                        {
                            SetCreateList(ntable);
                        }
                        Defaultxt = Defaultxt.Replace(results,nll.SelReturnModel(nodeid).NodeListUrl);
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
            if (results.ToLower().IndexOf("itemid=") > -1)
            {
                string[] listarr = results.ToLower().Split(new string[] { "itemid=" }, StringSplitOptions.None);
                if (listarr.Length > 0)
                {
                    int ItemID = DataConverter.CLng(listarr[1].ToString());
                    string Htmllink = cll.GetCommonData(ItemID).HtmlLink;
                    if (Htmllink != "")
                    {
                        Defaultxt = Defaultxt.Replace(results, Htmllink);
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
        /// 生成节点首页
        /// </summary>
        /// <param name="Nodeinfo"></param>
        /// <param name="allFolder"></param>
        private void MakeColumnlist(M_Node Nodeinfo, string allFolder)
        {
            IList<string> Nodelists = new List<string>();
            //节点首页(可以包含分页)
            string NodeindexTemp = SiteConfig.SiteInfo.SiteUrl + dir + "ColumnList.aspx?NodeID=" + Nodeinfo.NodeID + "&p=1"; //生成节点首页

            string nodeindexcontent = remo.GetRemoteHtmlCode(NodeindexTemp);

            //将分页转成静态 替换分页地址=========================================
            nodeindexcontent = GetPageCode(Nodeinfo, Nodelists, nodeindexcontent);//分析分页，将分页地址换成静态地址
            nodeindexcontent = GetPagefunction(nodeindexcontent);            //替换连接地址

            allFolder = allFolder.Replace(@"\//", @"\");
            // allFolder = allFolder.Replace("/", @"\");
            //System.Web.HttpContext.Current.Response.Write(NodeindexTemp+"</br>");
            //System.Web.HttpContext.Current.Response.Write(allFolder + @"\" + "index." + GetNodeIndexEx(Nodeinfo));
            //System.Web.HttpContext.Current.Response.End();
            //最后保存节点首页文件

            FileSystemObject.WriteFile(allFolder + @"\" + "index." + GetNodeIndexEx(Nodeinfo), nodeindexcontent);

            //保存节点分页文件，根据分页获取内容
        }


        /// <summary>
        /// 生成分页列表
        /// </summary>
        /// <param name="Nodeinfo"></param>
        /// <param name="allFolder"></param>
        /// <param name="url"></param>
        /// <param name="Nodelists"></param>
        private void MakeColumnlist(M_Node Nodeinfo, string allFolder, string url, IList<string> Nodelists)
        {
            //节点首页(可以包含分页)
            string NodeindexTemp = url; //生成分页列表
            string nodeindexcontent = remo.GetRemoteHtmlCode(NodeindexTemp);
            //将分页转成静态 替换分页地址=========================================
            nodeindexcontent = GetPageCode(Nodeinfo, Nodelists, nodeindexcontent);//分析分页，将分页地址换成静态地址
            nodeindexcontent = GetPagefunction(nodeindexcontent);            //替换连接地址

            allFolder = allFolder.Replace(@"\//", @"\");
            // allFolder = allFolder.Replace("/", @"\");
            //System.Web.HttpContext.Current.Response.Write(NodeindexTemp+"</br>");
            //System.Web.HttpContext.Current.Response.Write(allFolder + @"\" + "index." + GetNodeIndexEx(Nodeinfo));
            //System.Web.HttpContext.Current.Response.End();
            //最后保存节点首页文件

            FileSystemObject.WriteFile(allFolder, nodeindexcontent);
            //保存节点分页文件，根据分页获取内容
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
                    M_Node nodesinfo = nll.SelReturnModel(ParentID);
                    string Nodedir = nodesinfo.NodeDir;
                    Pardir = GetParentDir(nodesinfo) + "/" + Pardir + Nodedir;
                }
            }
            return Pardir;

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
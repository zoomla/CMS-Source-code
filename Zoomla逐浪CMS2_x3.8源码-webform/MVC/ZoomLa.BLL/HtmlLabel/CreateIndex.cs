using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using ZoomLa.Model;
using System.Data;
namespace ZoomLa.BLL
{
    /// <summary>
    /// 创建首页
    /// </summary>
    public class CreateIndex
    {
        private GetRemoteObj remo = new GetRemoteObj();

        private IList<string> FileList = new List<string>();//创建地址字典
        private B_Node nll = new B_Node();
        private B_Content cll = new B_Content();
        private string m_Nodeist;
        private string m_SiteMapath;

        private string dir;
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
        public CreateIndex()
        {
            ///构造函数
        }

        /// <summary>
        /// 创建首页
        /// </summary>
        /// <param name="Path">创建路径</param>
        /// <returns></returns>
        public void CreateDefault()
        {
            //   int pid = nll.GetContrarily(DataConverter.CLng(this.Nodeist), 5);
            M_Node Pnid = nll.SelReturnModel(DataConverter.CLng(this.Nodeist));
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Pnid.SiteConfige);
            XmlNodeList xnl = xml.SelectNodes("SiteConfig");
            dir = function.GetXmlNode(xnl, "TemplateDir");
            dir = dir.Substring(1, dir.LastIndexOf("Template") - 1);
            string dir1 = dir;
            dir = dir.Replace(@"\", "/");
            dir = dir.Replace("//", "/");
            dir = "/" + dir;

            string Siteulr = SiteConfig.SiteInfo.SiteUrl + dir + "Default.aspx";

            string DefaultContent = remo.GetRemoteHtmlCode(Siteulr);//获得首页内容
            #region 路径机制替换
            DefaultContent = GetDefaultfunction(DefaultContent);//提取内容替换链接
            #endregion
            remo.SaveContent(DefaultContent, SiteMapath + dir1 + "index.htm");//保存文件
        }

        /// <summary>
        /// 创建频道首页
        /// </summary>
        /// <param name="SiteID">频道ID</param>
        /// <param name="Path">频道存放根目录的物理绝对路径，不带斜杠，使用Mapth获取再传进入</param>
        /// <returns></returns>
        public bool CreateDefault(int SiteID)
        {
            string nodedir = nll.SelReturnModel(SiteID).NodeDir;
            string Siteulr = SiteConfig.SiteInfo.SiteUrl + "/" + nodedir + "/Default.aspx";
            string DefaultContent = remo.GetRemoteHtmlCode(Siteulr);//获得首页内容
            #region 路径机制替换
            DefaultContent = GetDefaultfunction(DefaultContent);//提取内容替换链接
            #endregion
            remo.SaveContent(DefaultContent, SiteMapath + "/" + nodedir + "/index.htm");//保存文件
            return true;
        }

        /// <summary>
        /// 提取内容替换链接
        /// </summary>
        /// <param name="DefaultContent"></param>
        private string GetDefaultfunction(string DefaultContent)
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
                        if (results.ToLower().IndexOf(".aspx") > -1 && FileList.IndexOf(results) == -1)
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
                    string nodeurl = nll.SelReturnModel(nodeid).NodeListUrl;
                    if (nodeurl != "")
                    {
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
    }
}
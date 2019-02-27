using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using System.Configuration;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Web;
using System.Globalization;

namespace ZoomLa.BLL
{
    public class B_PageStart
    {
        private B_CollectionItem bll = new B_CollectionItem();
        private string siteurl = "";
        public B_PageStart()
        {

        }

        public void PageRead(string pageurl)
        {
            siteurl = pageurl;
            Thread run = new Thread(new ThreadStart(readpage));
            run.IsBackground = true;
            run.Start();
        }

        /// <summary>
        /// 预读页面
        /// </summary>
        public void readpage()
        {
            string[] pagelist = { "/Login.aspx", "/Profile/Worktable.aspx", "/Content/NodeTree.aspx", "/Content/ContentManage.aspx", "/Shop/ShopNodeTree.aspx", "/Shop/ProductManage.aspx", "/page/PageregGuide.aspx", "/page/UserModelManage.aspx", "/Exam/QuestionGuide.aspx", "/Exam/QuestionManage.aspx", "/User/UserGuide.aspx", "/User/UserManage.aspx", "/Plus/ADGuide.aspx", "/Plus/ADZoneManage.aspx", "/Config/ConfigGuide.aspx", "/Config/SiteInfo.aspx", "/SiteServer/ServerGuid.aspx", "/SiteServer/APIConfig.aspx", "/AddOn/ProjectGuide.aspx", "/AddOn/ProjectIndex.aspx" };
            GetRemoteHtmlCode(siteurl + "/Common/ValidateCode.aspx");
            for (int i = 0; i < pagelist.Length; i++)
            {
                try
                {
                    GetRemoteHtmlCode(siteurl + "/" + SiteConfig.SiteOption.ManageDir + "/" + pagelist[i]);
                }
                catch 
                {

                }
            }
           
            
        }

        /// <summary>
        /// 获取远程文件源代码
        /// </summary>
        /// <param name="url">远程url</param>
        /// <returns></returns>
        public void GetRemoteHtmlCode(string Url)
        {
            MSXML2.XMLHTTP _xmlhttp = new MSXML2.XMLHTTPClass();
            _xmlhttp.open("GET", Url, false, null, null);
            _xmlhttp.send(null);
            _xmlhttp.abort();
        }
    }
}

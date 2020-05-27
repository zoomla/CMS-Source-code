namespace ZoomLa.Components
{
    using System;
    using System.Web;
    [Serializable]
    public class SiteInfo
    {
        private string _siteurl = "";
        public string SiteName
        {
            get;
            set;
        }
        public string SiteTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 不带/结尾的网址,http://www.z01.com
        /// </summary>
        public string SiteUrl
        {
            get { return (_siteurl.TrimEnd('/')); }
            set { _siteurl = value; }
        }
        public string LogoUrl
        {
            get;
            set;
        }
        public string LogoAdmin { get; set; }
        /// <summary>
        /// 平台名称与LogoAdmin配合使用
        /// </summary>
        public string LogoPlatName { get; set; }
        public string BannerUrl
        {
            get;
            set;
        }
        public string CompanyName { get; set; }
        /// <summary>
        /// 站长名称
        /// </summary>
        public string Webmaster
        {
            get;
            set;
        }
        /// <summary>
        /// 站长手机
        /// </summary>
        public string MasterPhone { get; set; }
        /// <summary>
        /// 站长邮箱
        /// </summary>
        public string WebmasterEmail
        {
            get;
            set;
        }
        public string MetaKeywords
        {
            get;
            set;
        }
        public string MetaDescription
        {
            get;
            set;
        }
        public string Copyright
        {
            get;
            set;
        }
        /// <summary>
        /// 全站脚本
        /// </summary>
        public string AllSiteJS { get; set; }
        public string VirtualPath
        {
            get;
            set;
        }
        
    }
}
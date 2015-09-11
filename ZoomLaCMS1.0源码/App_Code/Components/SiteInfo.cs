namespace ZoomLa.Components
{
    using System;
    using System.Web;
    /// <summary>
    /// 网站信息配置
    /// </summary>
    [Serializable]
    public class SiteInfo
    {
        private string m_BannerUrl;
        private string m_Copyright;
        private string m_LogoUrl;
        private string m_MetaDescription;
        private string m_MetaKeywords;
        private string m_ProductEdition;
        private string m_SiteName;
        private string m_SiteTitle;
        private string m_SiteUrl;
        private string m_VirtualPath;
        private string m_Webmaster;
        private string m_WebmasterEmail;

        public string BannerUrl
        {
            get
            {
                return this.m_BannerUrl;
            }
            set
            {
                this.m_BannerUrl = value;
            }
        }

        public string Copyright
        {
            get
            {
                return this.m_Copyright;
            }
            set
            {
                this.m_Copyright = value;
            }
        }

        public string LogoUrl
        {
            get
            {
                return this.m_LogoUrl;
            }
            set
            {
                this.m_LogoUrl = value;
            }
        }

        public string MetaDescription
        {
            get
            {
                return this.m_MetaDescription;
            }
            set
            {
                this.m_MetaDescription = value;
            }
        }

        public string MetaKeywords
        {
            get
            {
                return this.m_MetaKeywords;
            }
            set
            {
                this.m_MetaKeywords = value;
            }
        }

        public string ProductEdition
        {
            get
            {
                return this.m_ProductEdition;
            }
            set
            {
                this.m_ProductEdition = value;
            }
        }

        public string SiteName
        {
            get
            {
                return this.m_SiteName;
            }
            set
            {
                this.m_SiteName = value;
            }
        }

        public string SiteTitle
        {
            get
            {
                return this.m_SiteTitle;
            }
            set
            {
                this.m_SiteTitle = value;
            }
        }

        public string SiteUrl
        {
            get
            {
                return this.m_SiteUrl;
            }
            set
            {
                this.m_SiteUrl = value;
            }
        }

        public string VirtualPath
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    this.m_VirtualPath = HttpContext.Current.Request.ApplicationPath;
                }
                return VirtualPathUtility.AppendTrailingSlash(this.m_VirtualPath);
            }
            set
            {
                if (string.IsNullOrEmpty(value) && (HttpContext.Current != null))
                {
                    this.m_VirtualPath = HttpContext.Current.Request.ApplicationPath;
                }
                else
                {
                    this.m_VirtualPath = value;
                }
            }
        }

        public string Webmaster
        {
            get
            {
                return this.m_Webmaster;
            }
            set
            {
                this.m_Webmaster = value;
            }
        }

        public string WebmasterEmail
        {
            get
            {
                return this.m_WebmasterEmail;
            }
            set
            {
                this.m_WebmasterEmail = value;
            }
        }
    }
}
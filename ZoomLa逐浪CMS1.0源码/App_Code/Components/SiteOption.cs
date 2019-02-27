namespace ZoomLa.Components
{
    using System;
    /// <summary>
    /// 网站参数配置
    /// </summary>
    [Serializable]
    public class SiteOption
    {
        //广告目录
        private string m_AdvertisementDir; 
        //是否启用后台管理认证码
        private bool m_EnableSiteManageCode;
        //是否使用软键盘输入密码
        private bool m_EnableSoftKey;
        //是否允许上传文件
        private bool m_EnableUploadFiles;
        //链接地址方式
        private bool m_IsAbsoluatePath;
        //后台管理目录
        private string m_ManageDir;
        //后台管理认证码
        private string m_SiteManageCode;
        //网站模板根目录
        private string m_TemplateDir;
        //网站首页模板
        private string m_IndexTemplate;
        private string m_CssDir;
        //网站上传目录
        private string m_UploadDir;
        //上传文件的保存扩展名规则
        private string m_UploadFileExts;
        //允许上传的最大文件大小
        private int m_UploadFileMaxSize;
        //上传文件的保存目录规则
        private string m_UploadFilePathRule;        

        public string AdvertisementDir
        {
            get
            {
                return this.m_AdvertisementDir;
            }
            set
            {
                this.m_AdvertisementDir = value;
            }
        }

        public bool EnableSiteManageCode
        {
            get
            {
                return this.m_EnableSiteManageCode;
            }
            set
            {
                this.m_EnableSiteManageCode = value;
            }
        }

        public bool EnableSoftKey
        {
            get
            {
                return this.m_EnableSoftKey;
            }
            set
            {
                this.m_EnableSoftKey = value;
            }
        }

        public bool EnableUploadFiles
        {
            get
            {
                return this.m_EnableUploadFiles;
            }
            set
            {
                this.m_EnableUploadFiles = value;
            }
        }

        public bool IsAbsoluatePath
        {
            get
            {
                return this.m_IsAbsoluatePath;
            }
            set
            {
                this.m_IsAbsoluatePath = value;
            }
        }

        public string ManageDir
        {
            get
            {
                return this.m_ManageDir;
            }
            set
            {
                this.m_ManageDir = value;
            }
        }

        public string SiteManageCode
        {
            get
            {
                return this.m_SiteManageCode;
            }
            set
            {
                this.m_SiteManageCode = value;
            }
        }

        public string TemplateDir
        {
            get
            {
                return this.m_TemplateDir;
            }
            set
            {
                this.m_TemplateDir = value;
            }
        }

        public string IndexTemplate
        {
            get { return this.m_IndexTemplate; }
            set { this.m_IndexTemplate = value; }
        }

        public string CssDir
        {
            get { return this.m_CssDir; }
            set { this.m_CssDir = value; }
        }

        public string UploadDir
        {
            get
            {
                return this.m_UploadDir;
            }
            set
            {
                this.m_UploadDir = value;
            }
        }

        public string UploadFileExts
        {
            get
            {
                return this.m_UploadFileExts;
            }
            set
            {
                this.m_UploadFileExts = value;
            }
        }

        public int UploadFileMaxSize
        {
            get
            {
                return this.m_UploadFileMaxSize;
            }
            set
            {
                this.m_UploadFileMaxSize = value;
            }
        }

        public string UploadFilePathRule
        {
            get
            {
                return this.m_UploadFilePathRule;
            }
            set
            {
                this.m_UploadFilePathRule = value;
            }
        }
    }
}
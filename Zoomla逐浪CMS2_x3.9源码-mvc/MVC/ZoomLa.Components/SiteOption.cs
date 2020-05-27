namespace ZoomLa.Components
{
    using System;
    /// <summary>
    /// 网站参数配置
    /// </summary>
    [Serializable]
    public class SiteOption
    {
        private string m_Language = "";
        private string m_UploadDir = "";
        private string m_IndexTemplate = "";
        public string Language
        {
            get { return string.IsNullOrEmpty(m_Language) ? "ZH-CN" : m_Language; }
            set { m_Language = value; }
        }
        /// <summary>
        /// 设备自适配开关
        /// </summary>
        public bool UAgent
        {
            get;
            set;
        }
        public string IsOpenHelp
        {
            get;
            set;
        }
        /// <summary>
        /// 删除本地帮助文件
        /// </summary>
        public bool DeleteLocal { get; set; }
        /// <summary>
        /// 是否允许域名路由,默认关闭 0:关闭,1:开启
        /// </summary>
        public string DomainRoute { get { return _domainRoute; } set { _domainRoute = value; } }
        /// <summary>
        /// 标题查重,多少字符以上才检测,0为不检测
        /// </summary>
        public int DupTitleNum { get; set; }
        /// <summary>
        /// 视频服务器地址
        /// </summary>
        public string Videourl
        {
            get;
            set;
        }
        /// <summary>
        /// 生成PDF目录
        /// </summary>
        public string PdfDirectory
        {
            get;
            set;
        }
        /// <summary>
        /// 广告目录
        /// </summary>
        public string AdvertisementDir
        {
            get;
            set;
        }
        /// <summary>
        /// 链接地址方式
        /// </summary>
        public bool IsAbsoluatePath
        {
            get;
            set;
        }
        /// <summary>
        /// 首页扩展名
        /// </summary>
        public string IndexEx
        {
            get;
            set;
        }
        /// <summary>
        /// 生成页面目录
        /// </summary>
        public string GeneratedDirectory
        {
            get;
            set;
        }
        /// <summary>
        /// 是否开启域名归集
        /// </summary>
        public bool IsZone
        {
            get;
            set;
        }
        /// <summary>
        /// 云盘,可读,可写,可删除权限,read,del,up
        /// </summary>
        public string Cloud_Auth
        {
            get;
            set;
        }
        /// <summary>
        /// 云台提示开启
        /// </summary>
        public string CloudLeadTips
        {
            get;
            set;
        }
        /// <summary>
        /// 简洁登录模式,不显示背景图,1:开启
        /// </summary>
        public int SiteManageMode
        {
            get;
            set;
        }
        /// <summary>
        /// 模板服务器
        /// </summary>
        public string ProjectServer
        {
            get;
            set;
        }
        /// <summary>
        /// 网站首页模板
        /// </summary>
        public string IndexTemplate
        {
            get { return "/" + this.m_IndexTemplate.Trim('/'); }
            set { this.m_IndexTemplate = value; }
        }
        /// <summary>
        /// 店铺首页模板
        /// </summary>
        public string ShopTemplate
        {
            get { return "/" + this._ShopTemplate.Trim('/'); }
            set { this._ShopTemplate = value; }
        }
        /// <summary>
        /// 网站模板根目录
        /// </summary>
        public string TemplateDir
        {
            get;
            set;
        }
        public string TemplateName { get { return TemplateDir.ToLower().Replace("/template/", "").Trim('/'); } }
        /// <summary>
        /// 风格路径
        /// </summary>
        public string CssDir
        {
            get;
            set;
        }
        /// <summary>
        ///默认风格
        /// </summary>
        public string StylePath
        {
            get;
            set;
        }
        /// <summary>
        /// 是否启用后台管理认证码
        /// </summary>
        public bool EnableSiteManageCode
        {
            get;
            set;
        }
        /// <summary>
        /// 是否开启Https访问 1:开启
        /// </summary>
        public string SafeDomain { get; set; }
        /// <summary>
        /// 是否使用软键盘输入密码
        /// </summary>
        public bool EnableSoftKey
        {
            get;
            set;
        }
        public bool OpenSendMessage
        {
            get;
            set;
        }
        /// <summary>
        /// 黄页是否需要审核
        /// </summary>
        public bool RegPageStart
        {
            get;
            set;
        }
        /// <summary>
        /// 游客邮件发送权限
        /// </summary>
        public string MailPermission { get; set; }
        /// <summary>
        /// 子站被采集密钥
        /// </summary>
        public string SiteCollKey
        {
            get;
            set;
        }
        /// <summary>
        /// FlexAPI密匙
        /// </summary>
        public string FlexKey
        {
            get;
            set;
        }
        /// <summary>
        /// 用于存APP身份证书(20位)
        /// </summary>
        public string WxAppID { get; set; }
        /// <summary>
        /// 是否开启留言
        /// </summary>
        public int OpenMessage
        {
            get;
            set;
        }
        /// <summary>
        /// 是否开启审核
        /// </summary>
        public int OpenAudit
        {
            get;
            set;
        }
        /// <summary>
        /// 是否开启过滤敏感词
        /// </summary>
        public int IsSensitivity
        {
            get;
            set;
        }
        /// <summary>
        /// 敏感词汇
        /// </summary>
        public string Sensitivity
        {
            get;
            set;
        }
        /// <summary>
        /// 是否允许上传文件
        /// </summary>
        public bool EnableUploadFiles
        {
            get;
            set;
        }
        /// <summary>
        /// 在线编辑器上传多媒体的最大文件大小
        /// </summary>
        public int UploadMdaMaxSize
        {
            get;
            set;
        }
        /// <summary>
        /// 在线编辑器上传Flash的最大文件大小
        /// </summary>        
        public int UploadFlhMaxSize
        {
            get;
            set;
        }
        /// <summary>
        /// 网站上传目录
        /// </summary>
        public string UploadDir
        {
            get
            {
                return this.m_UploadDir.EndsWith("/") ? m_UploadDir : m_UploadDir + "/";
            }
            set
            {
                this.m_UploadDir = value;
            }
        }
        /// <summary>
        /// 内容标题是否重复
        /// </summary>
        public int FileRj { get; set; }
        /// <summary>
        /// 命名规则
        /// </summary>
        public int FileN { get; set; }
        /// <summary>
        /// 在线编辑器上传附件的保存扩展名规则
        /// </summary>
        public string UploadFileExts
        {
            get;
            set;
        }
        /// <summary>
        /// 在线编辑器上传图片的保存扩展名规则
        /// </summary>
        public string UploadPicExts
        {
            get;
            set;
        }
        /// <summary>
        /// 在线编辑器上传图片的最大文件大小
        /// </summary>
        public int UploadPicMaxSize
        {
            get;
            set;
        }
        /// <summary>
        /// 在线编辑器上传多媒体的保存扩展名规则
        /// </summary>
        public string UploadMdaExts
        {
            get;
            set;
        }
        private string _siteid = "";
        /// <summary>
        /// 改为允许支付方式
        /// </summary>
        public string SiteID
        {
            get { return _siteid ?? ""; }
            set { _siteid = value; }
        }
        /// <summary>
        /// 使用外币结算
        /// </summary>
        public bool OpenMoneySel { get; set; }
        /// <summary>
        /// 快递跟踪APIKey
        /// </summary>
        public string KDKey { get; set; }
        /// <summary>
        /// 是否开启快递跟踪API
        /// </summary>
        public int KDAPI { get; set; }
        /// <summary>
        /// 退货时间时限(天)
        /// </summary>
        public int THDate { get; set; }
        //是否开启顾客订单短信
        public string OrderMsg_Chk { get; set; }
        //顾客订单短信模板
        public string OrderMsg_Tlp { get; set; }
        //管理员订单短信与模板,读MasterPhone
        public string OrderMasterMsg_Chk { get; set; }
        public string OrderMasterMsg_Tlp { get; set; }
        //管理员邮件与模板,读WebmasterEmail
        public string OrderMasterEmail_Chk { get; set; }
        public string OrderMasterEmail_Tlp { get; set; }
        #region 短信相关
        /// <summary>
        /// 是否下载远程图片
        /// </summary>
        public bool IsSaveRemoteImage { get; set; }        
        public string G_mtype
        {
            get;
            set;
        }
        public string G_content
        {
            get;
            set;
        }
        public string G_blackList
        {
            get;
            set;
        }
        public string G_uid
        {
            get;
            set;
        }
        public string G_eid
        {
            get;
            set;
        }
        public string G_pwd
        {
            get;
            set;
        }
        public string G_gate_id
        {
            get;
            set;
        }
        /// <summary>
        /// 默认使用哪个短信接口1:北京网通,2:深圳电信,3:亿美软件,4:云通迅
        /// </summary>
        public string DefaultSMS { get; set; }
        /// <summary>
        /// 亿美短信Key
        /// </summary>
        public string sms_key { get; set; }
        /// <summary>
        /// 亿美短信Passwd
        /// </summary>
        public string sms_pwd { get; set; }
        #endregion
        /// <summary>
        /// 是否安装FlashPaper
        /// </summary>
        public bool IsFlashPaper
        {
            get;
            set;
        }
        /// <summary>
        /// 是否黄页需要审核
        /// </summary>
        public bool EnableHuShenHe
        {
            get;
            set;
        }
        /// <summary>
        /// 后台管理目录
        /// </summary>
        public string ManageDir
        {
            get;
            set;
        }
        /// <summary>
        /// 后台管理认证码
        /// </summary>
        public string SiteManageCode
        {
            get;
            set;
        }
        
        private string _ShopTemplate;
        /// <summary>
        /// 在线编辑器上传附件的最大文件大小
        /// </summary>
        public int UploadFileMaxSize
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string MssUser
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string MssPsw
        {
            get;
            set;
        }
        /// <summary>
        /// 服务器类型
        /// </summary>
        public string ServerType
        {
            get;
            set;
        }
        private string _domainRoute = "0";
        /// <summary>
        /// 开启内容流程
        /// </summary>
        public int ContentConfig
        {
            get;
            set;
        }
        /// <summary>
        /// 站点间内容转移同时转移相关文件
        /// </summary>
        public int MoveFile
        {
            get;
            set;
        }
        /// <summary>
        /// 内容归档时间设置
        /// </summary>
        public string GetDownTime
        {
            get;
            set;
        }
        /// <summary>
        /// JS
        /// </summary>
        public string JS
        {
            get;
            set;
        }
        /// <summary>
        /// 场景配置
        /// </summary>
        public string Desk
        {
            get;
            set;
        }
        /// <summary>
        /// 短信发送次数
        /// </summary>
        public int SendNum
        {
            get;
            set;
        }
        /// <summary>
        /// 短消息提示
        /// </summary>
        public bool SMSTips { get; set; }
        public string NeedCheckRefer
        {
            get;
            set;
        }
        /// <summary>
        /// 管理员动态口令密钥
        /// </summary>
        public string AdminKey { get; set; }
        /// <summary>
        /// 1:本地,2:FTP服务器3,云端
        /// </summary>
        public string OpenFTP { get; set; }
        /// <summary>
        /// 舆情签名标记
        /// </summary>
        public string SenSign { get; set; }
        /// <summary>
        /// 微信欢迎语
        /// </summary>
        public string WxWel { get; set; }
        /// <summary>
        /// 是否开启管理员申请
        /// </summary>
        public int RegManager { get; set; }
        public bool DomainMerge { get; set; }
        /// <summary>
        /// 云通讯应用id
        /// </summary>
        public string CCPAppID { get; set; }
        /// <summary>
        /// 云通讯账号SID
        /// </summary>
        public string CCPAccount_SID { get; set; }
        /// <summary>
        /// 云通讯Token
        /// </summary>
        public string CCPToken { get; set; }
        /// <summary>
        /// 云通讯短信模板ID
        /// </summary>
        public int CCPMsgTempID { get; set; }
        /// <summary>
        /// 同一个手机号码发送短信的最大次数,为0则不限制
        /// </summary>
        public int MaxMobileMsg { get; set; }
        /// <summary>
        /// 同一个ip发送短信的最大次数
        /// </summary>
        public int MaxIpMsg { get; set; }
        private string _loggedUrl = "";
        /// <summary>
        /// 用户登录后跳转的页面,为空或默认为/User/
        /// </summary>
        public string LoggedUrl { get { return string.IsNullOrEmpty(_loggedUrl) ? "/User/" : _loggedUrl; } set { _loggedUrl = value; } }
        /// <summary>
        /// 动力的二级域名,用于动力模块
        /// </summary>
        public string DesignDomain { get; set; }
        public int DN_MBSiteCount { get; set; }
        public SiteOption()
        {
            THDate = 10;//默认值
            DomainMerge = false;
        }
        #region disuse
        /// <summary>
        /// Disuse(原微信公众号密钥)
        /// </summary>
        private string WxSecret { get; set; }
        /// <summary>
        /// 站长拥有资金(disuse,为兼容暂为public)
        /// </summary>
        public double MastMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 保留日志数量(disuse)
        /// </summary>
        private string Savanumlog
        {
            get;
            set;
        }
        /// <summary>
        /// 保留天数日志(disuse)
        /// </summary>
        private string Savadaylog
        {
            get;
            set;
        }
        /// <summary>
        /// 默认编辑器版本(disuse)
        /// </summary>
        public string EditVer
        {
            get;
            set;
        }
        /// <summary>
        /// 是否开启多用户网店(disuse)
        /// </summary>
        public bool IsMall
        {
            get;
            set;
        }
        /// <summary>
        /// 启用短信通知(disuse)
        /// </summary>
        private bool IsSMSNotice
        {
            get;
            set;
        }
        /// <summary>
        /// 是否开启日志(disuse)
        /// </summary>
        private string OpenLog
        {
            get;
            set;
        }
        /// <summary>
        /// 启用邮件通知(disuse)
        /// </summary>
        private bool IsEmailNotice
        {
            get;
            set;
        }
        #endregion
    }
}
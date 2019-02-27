namespace ZoomLa.Components
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;

    [Serializable, XmlRoot("Site")]

    public class StationGroupInfo
    {
        private bool _enableSA;
        private string _saName;
        private string _saPassWord;
        private string _codeSourceUrl;
        private string _zipSavePath;
        private string _setupPath;
        private string _backupUrl;
        private string _rootPath;
        private string _zipName;
        private string sitePath;
        private string _tDomName;
        private string _dnsOutputPath;
        private string _newNetClientID;
        private string _newNetApiPasswd;
        private string _DefaultCheck;
        private string _DefaultDisplay;
        private string _DnsOption;
        private string _ModelID;
        private string _NodeID;
        private string _DefaultIP;

        private string _DNSTemplate;

        public StationGroupInfo()
        {
            
        }
        public bool EnableSA
        {
            get
            {
                return this._enableSA;
            }
            set
            {
                this._enableSA = value;
            }
        }
        public string SAName
        {
            get
            {
                return this._saName;
            }
            set
            {
                this._saName = value;
            }
        }
        public string SAPassWord
        {
            get
            {
                return this._saPassWord;
            }
            set
            {
                this._saPassWord = value;
            }
        }
        public string CodeSourceUrl
        {
            get
            {
                return this._codeSourceUrl;
            }
            set
            {
                this._codeSourceUrl = value;
            }
        }
        public string ZipSavePath
        {
            get
            {
                return this._zipSavePath;
            }
            set
            {
                this._zipSavePath = value;
            }
        }
        public string SetupPath
        {
            get
            {
                return this._setupPath;
            }
            set
            {
                this._setupPath = value;
            }
        }
        public string BackupUrl
        {
            get
            {
                return this._backupUrl;
            }
            set
            {
                this._backupUrl = value;
            }
        }
        public string RootPath
        {
            get
            {
                return this._rootPath;
            }
            set
            {
                this._rootPath = value;
            }
        }
        public string ZipName
        {
            get
            {
                return this._zipName;
            }
            set
            {
                this._zipName = value;
            }
        }
        public string SitePath
        {
            get
            {
                return this.sitePath.EndsWith(@"\") ? this.sitePath : this.sitePath + @"\"; 
            }
            set
            {
                this.sitePath = value.EndsWith(@"\") ? value : value + @"\";
            }
        }
        public string TDomName
        {
            get
            {
                return this._tDomName.IndexOf('.') != 0 ? "." + this._tDomName : this._tDomName;
            }
            set
            {
                this._tDomName = value;
            }
        }
        public string DnsOutputPath
        {
            get
            {
                return this._dnsOutputPath.EndsWith(@"\") ? this._dnsOutputPath : this._dnsOutputPath + @"\";
            }
            set
            {
                this._dnsOutputPath = value.EndsWith(@"\") ? value : value + @"\";
            }
        }
        public string newNetClientID
        {
            get
            {
                return this._newNetClientID;
            }
            set
            {
                this._newNetClientID = value;
            }
        }
        public string newNetApiPasswd
        {
            get
            {
                return this._newNetApiPasswd;
            }
            set
            {
                this._newNetApiPasswd = value;
            }
        }
        public string DefaultCheck
        {
            get
            {
                return this._DefaultCheck;
            }
            set
            {
                this._DefaultCheck = value;
            }
        }
        public string DefaultDisplay
        {
            get
            {
                return this._DefaultDisplay;
            }
            set
            {
                this._DefaultDisplay = value;
            }
        }
        public string DnsOption
        {
            get
            {
                return this._DnsOption;
            }
            set
            {
                this._DnsOption = value;
            }
        }
        public string ModelID
        {
            get
            {
                return this._ModelID;
            }
            set
            {
                this._ModelID = value;
            }
        }
        public string NodeID
        {
            get
            {
                return this._NodeID;
            }
            set
            {
                this._NodeID = value;
            }
        }
        public string DefaultIP
        {
            get
            {
                return this._DefaultIP;
            }
            set
            {
                this._DefaultIP = value;
            }
        }
        public string DNSTemplate
        {
            get
            {
                return this._DNSTemplate;
            }
            set
            {
                this._DNSTemplate = value;
            }
        }
        public bool AutoCreateDB { get; set; }
        public string DBManagerName { get; set; }
        public string DBManagerPasswd { get; set; }
        public bool RemoteUser { get; set; }
        public bool RemoteEnable { get; set; }
        public string DBName { get; set; }
        public string DBUName { get; set; }
        public string RemoteUrl { get; set; }
        public string Token { get; set; }
    }
}

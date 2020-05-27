namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;
    using Newtonsoft.Json;
    using System.Data.Common;
    [Serializable]
    public class M_UserInfo : M_Base
    {
        private bool _isnull;
        /// <summary>
        /// 是否开通了云盘
        /// </summary>
        public int IsCloud
        {
            get;
            set;
        }
        /// <summary>
        /// 上级用户ID
        /// </summary>
        public string ParentUserID { get; set; }
        /// <summary>
        /// 用户工号
        /// </summary>
        public M_UserInfo()
        {
            UserID = 0;
            this.LastLoginTimes = DateTime.Now;
            this.UpdateTime = DateTime.Now;
            this.DeadLine = DateTime.Now;
            this.JoinTime = DateTime.Now;
            this.LastActiveTime = DateTime.Now;
            this.Purse = 0;
            this.DummyPurse = 0;
            this.SilverCoin = 0;
            this.UserPoint = 0;
            this.UserExp = 0;
            this.UserCreit = 0;
        }
        public M_UserInfo(bool value)
        {
            _isnull = value;
        }
        /// <summary>
        /// 分站返利金额
        /// </summary>
        public double SiteRebateBalance
        {
            get;
            set;
        }
        /// <summary>
        /// 用于存用户所选聚合模板
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 用户组ID,勿扩展为能拥有多个
        /// </summary>
        public int GroupID { set; get; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { set; get; }
        /// <summary>
        /// 安全提示问题
        /// </summary>
        public string Question { set; get; }
        /// <summary>
        /// 安全提示回答
        /// </summary>
        public string Answer { set; get; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime { set; get; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginTimes { set; get; }
        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime LastLoginTimes { set; get; }
        /// <summary>
        /// 最近登录IP
        /// </summary>
        public string LastLoginIP { set; get; }
        private DateTime _lastpwdchangetime;
        /// <summary>
        /// 最近修改密码时间
        /// </summary>
        public DateTime LastPwdChangeTime
        {
            get
            {
                if (_lastpwdchangetime.Year < 1901) _lastpwdchangetime = DateTime.Now;
                return this._lastpwdchangetime;
            }
            set
            {
                this._lastpwdchangetime = value;
            }
        }
        /// <summary>
        /// 最近被锁定时间
        /// </summary>
        public DateTime LastLockTime { set; get; }
        /// <summary>
        /// 状态 0:正常状态,1:锁定,2:待认证,3:双认证,4:邮件认证,5:待认证
        /// </summary>
        public int Status
        {
            get;
            set;
        }
        /// <summary>
        /// 注册邮件确认码
        /// </summary>
        public string CheckNum { set; get; }
        /// <summary>
        /// 消费积分
        /// </summary>
        public int ConsumeExp
        {
            get;
            set;
        }
        /// <summary>
        /// 卖家积分
        /// </summary>
        public int boffExp
        {
            get;
            set;
        }
        public bool IsNull
        {
            get { return (UserID < 1 || _isnull || string.IsNullOrEmpty(UserName)); }
        }
        /// <summary>
        /// 有效期截止时间
        /// </summary>
        public DateTime DeadLine
        {
            get;
            set;
        }
        /// <summary>
        /// 加入会员组时间
        /// </summary>
        public DateTime JoinTime
        {
            get;
            set;
        }
        public string TrueName { get; set; }
        /// <summary>
        /// 用于/design/当前站点ID
        /// </summary>
        public int SiteID { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { set; get; }
        /// <summary>
        /// 公司简介
        /// </summary>
        public string CompanyDescribe { set; get; }
        /// <summary>
        /// 印证 0-没有；1-有
        /// </summary>
        public int IsConfirm { set; get; }
        /// <summary>
        /// VIP级数
        /// </summary>
        public int VIP { set; get; }
        public string UserFace { get { return salt; } set { salt = value; } }
        /// <summary>
        /// 用作存储用户头像
        /// </summary>
        private string salt
        {
            get;
            set;
        }
        /// <summary>
        /// 企业用户认证状态|聚合认证,2为通过了聚合认证
        /// </summary>
        public int State
        {
            get;
            set;
        }
        /// <summary>
        /// 证书路径
        /// </summary>
        public string CerificateLogo { set; get; }
        /// <summary>
        /// 认证类型
        /// </summary>
        public string ApproveType { set; get; }

        /// <summary>
        /// 用户注册IP
        /// </summary>
        public string RegisterIP { set; get; }
        /// <summary>
        /// 证书认证过期时间
        /// </summary>
        public DateTime CerificateDeadLine { set; get; }
        /// <summary>
        /// 返利余额
        /// </summary>
        public double RebatesBalance { set; get; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { set; get; }
        /// <summary>
        /// 登入聊天房间id(disuse)
        /// </summary>
        public int RoomID { set; get; }
        /// <summary>
        /// 用户二级密码
        /// </summary>
        public string PayPassWord
        {
            get;
            set;
        }
        /// <summary>
        /// 用户二级密码页面
        /// </summary>
        public string seturl
        {
            get;
            set;
        }
        /// <summary>
        /// 问答系统积分
        /// </summary>
        public int GuestScore { set; get; }
        private string _structureid;
        /// <summary>
        /// 组织结构ID
        /// </summary>
        public string StructureID
        {
            get
            {
                _structureid = string.IsNullOrEmpty(_structureid) ? "" : "," + (_structureid.Trim(',')) + ",";
                return this._structureid;
            }
            set
            {
                this._structureid = value;
            }
        }
        private string _userrole;
        /// <summary>
        /// 用户角色
        /// </summary>
        public string UserRole
        {
            get
            {
                _userrole = string.IsNullOrEmpty(_userrole) ? "" : "," + (_userrole.Trim(',')) + ",";
                return _userrole;
            }
            set
            {
                _userrole = value;
            }
        }
        /// <summary>
        /// 用户工号,用于OA
        /// </summary>
        public string WorkNum { get; set; }
        /// <summary>
        /// 邮箱容量限制,暂只用于OA,-1:不限制,0:读默认配置,其余则为自定义容量
        /// </summary>
        public int MailSize { get; set; }
        /// <summary>
        /// 最后活动时间
        /// </summary>
        public DateTime LastActiveTime { get; set; }
        /// <summary>
        /// 用户动态密钥
        /// </summary>
        public string ZnPassword { get; set; }
        private string _honyname;
        /// <summary>
        /// 昵称
        /// </summary>
        public string HoneyName { get { return string.IsNullOrEmpty(_honyname) ? UserName : _honyname; } set { _honyname = value; } }
        //------------虚拟币
        /// <summary>
        /// 资金余额
        /// </summary>
        public double Purse
        {
            get;
            private set;
        }
        public double SilverCoin
        {
            get;
            private set;
        }
        /// <summary>
        /// 用户积分(Point)
        /// </summary>
        public double UserExp
        {
            get;
            private set;
        }
        /// <summary>
        /// 用户信誉值
        /// </summary>
        public double UserCreit
        {
            get;
            private set;
        }
        /// <summary>
        /// 点券
        /// </summary>
        public double UserPoint
        {
            get;
            private set;
        }
        /// <summary>
        /// 虚拟币
        /// </summary>
        public double DummyPurse
        {
            get;
            private set;
        }
        //------------ 不入数据库
        /// <summary>
        /// 用户名
        /// </summary>
        public M_Uinfo UserBase { set; get; }
        /// <summary>
        /// 用户首拼音字母(暂未写入)
        /// </summary>
        public string FirstChar { get; set; }
        /// <summary>
        /// 用于WebAPI
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 是否游客True:是
        /// </summary>
        public bool IsTemp { get; set; }
        /// <summary>
        /// 游客来源
        /// </summary>
        public string TempSource { get; set; }
        public override string TbName { get { return "ZL_User"; } }
        public override string PK { get { return "UserID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                    {"UserID","Int","4"},
                                    {"UserName","NVarChar","255"}, 
                                    {"UserPwd","NVarChar","200"}, 
                                    {"GroupID","Int","4"},
                                    {"Email","NVarChar","50"}, 
                                    {"Question","NVarChar","255"}, 
                                    {"Answer","NVarChar","255"}, 
                                    {"RegTime","DateTime","8"},
                                    {"LoginTimes","Int","4"},
                                    {"LastLoginTime","DateTime","8"},
                                    {"LastLoginIP","NVarChar","50"}, 
                                    {"LastPwdChangeTime","NVarChar","1000"}, 
                                    {"LastLockTime","DateTime","8"},
                                    {"Status","Int","4"},
                                    {"CheckNum","NVarChar","50"}, 
                                    {"UserExp","Money","8"},
                                    {"boffExp","Int","4"},
                                    {"ConsumeExp","Int","4"},
                                    {"Purse","Float","4"},
                                    {"DeadLine","DateTime","4"},
                                    {"UserPoint","Float","4"},
                                    {"JoinTime","DateTime","8"},
                                    {"dummyPurse","Float","4"},
                                    {"Permissions","NVarChar","4000"}, 
                                    {"SiteID","Int","4"},
                                    {"Remark","NVarChar","4000"}, 
                                    {"CompanyName","NVarChar","255"}, 
                                    {"CompanyDescribe","NVarChar","500"}, 
                                    {"IsConfirm","Int","4"},
                                    {"VIP","Int","4"}, 
                                    {"salt","NVarChar","500"}, 
                                    {"RegisterIP","NVarChar","50"}, 
                                    {"CerificateDeadLine","DateTime","8"}, 
                                    {"PageId","Int","4"},
                                    {"SiteRebateBalance","Money","64"},
                                    {"UserCreit","Int","4"},
                                    {"RoomID","Int","4"},
                                    {"ParentUserID","NVarChar","4000"},  
                                    {"IsCloud","Int","4"},
                                    {"SilverCoin","Money","64"} ,
                                    {"PayPassWord","NVarChar","255"},
                                    {"seturl","NText","4000"},
                                    {"GuestScore","Int","4"},
                                    {"StructureID","NVarChar","255"},
                                    {"UserRole","NVarChar","255"},
                                    {"WorkNum","NVarChar","255"},
                                    {"MailSize","Int","4"},
                                    {"LastActiveTime","DateTime","8"},
                                    {"ZnPassword","NVarChar","100"},
                                    {"HoneyName","NVarChar","100"},
                                    {"State","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_UserInfo model = this;
            EmptyDeal();
            SqlParameter[] sp = GetSP();
            model.HoneyName = SafeStr(model.HoneyName);
            model.TrueName = SafeStr(model.TrueName);
            sp[0].Value = model.UserID;
            sp[1].Value = model.UserName;
            sp[2].Value = model.UserPwd;
            sp[3].Value = model.GroupID;
            sp[4].Value = model.Email;
            sp[5].Value = model.Question;
            sp[6].Value = model.Answer;
            sp[7].Value = model.RegTime;
            sp[8].Value = model.LoginTimes;
            sp[9].Value = model.LastLoginTimes;
            sp[10].Value = model.LastLoginIP;
            sp[11].Value = model.LastPwdChangeTime;
            sp[12].Value = model.LastLockTime;
            sp[13].Value = model.Status;
            sp[14].Value = model.CheckNum;
            sp[15].Value = model.UserExp;
            sp[16].Value = model.boffExp;
            sp[17].Value = model.ConsumeExp;
            sp[18].Value = model.Purse;
            sp[19].Value = model.DeadLine;
            sp[20].Value = model.UserPoint;
            sp[21].Value = model.JoinTime;
            sp[22].Value = model.DummyPurse;
            sp[23].Value = model.TrueName;
            sp[24].Value = model.SiteID;
            sp[25].Value = model.Remark;
            sp[26].Value = model.CompanyName;
            sp[27].Value = model.CompanyDescribe;
            sp[28].Value = model.IsConfirm;
            sp[29].Value = model.VIP;
            sp[30].Value = model.salt;
            sp[31].Value = model.RegisterIP;
            sp[32].Value = model.CerificateDeadLine;
            sp[33].Value = model.PageID;
            sp[34].Value = model.SiteRebateBalance;
            sp[35].Value = model.UserCreit;
            sp[36].Value = model.RoomID;
            sp[37].Value = model.ParentUserID;
            sp[38].Value = model.IsCloud;
            sp[39].Value = model.SilverCoin;
            sp[40].Value = model.PayPassWord;
            sp[41].Value = model.seturl;
            sp[42].Value = model.GuestScore;
            sp[43].Value = model.StructureID;
            sp[44].Value = model.UserRole;
            sp[45].Value = model.WorkNum;
            sp[46].Value = model.MailSize;
            sp[47].Value = model.LastActiveTime;
            sp[48].Value = model.ZnPassword;
            sp[49].Value = model.HoneyName;
            sp[50].Value = model.State;
            return sp;
        }
        public M_UserInfo GetModelFromReader(DbDataReader rdr)
        {
            M_UserInfo model = new M_UserInfo();
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.UserName = rdr["UserName"].ToString();
            model.UserPwd = rdr["UserPwd"].ToString();
            model.GroupID = ConvertToInt(rdr["GroupID"]);
            model.Email = rdr["Email"].ToString();
            model.Question = ConverToStr(rdr["Question"]);
            model.Answer = ConverToStr(rdr["Answer"]);
            model.RegTime = ConvertToDate(rdr["RegTime"]);
            model.LoginTimes = ConvertToInt(rdr["LoginTimes"]);
            model.LastLoginTimes = ConvertToDate(rdr["LastLoginTime"]);
            model.LastLoginIP = ConverToStr(rdr["LastLoginIP"]);
            model.LastPwdChangeTime = ConvertToDate(rdr["LastPwdChangeTime"]);
            model.LastLockTime = ConvertToDate(rdr["LastLockTime"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.State = ConvertToInt(rdr["State"]);
            model.CheckNum = ConverToStr(rdr["CheckNum"]);
            model.UserExp = ConverToDouble(rdr["UserExp"]);
            model.boffExp = ConvertToInt(rdr["boffExp"]);
            model.ConsumeExp = ConvertToInt(rdr["ConsumeExp"]);
            model.Purse = ConverToDouble(rdr["Purse"]);
            model.DeadLine = ConvertToDate(rdr["DeadLine"]);
            model.UserPoint = ConverToDouble(rdr["UserPoint"]);
            model.JoinTime = ConvertToDate(rdr["JoinTime"]);
            model.DummyPurse = ConverToDouble(rdr["DummyPurse"]);
            model.TrueName = ConverToStr(rdr["Permissions"]);
            model.SiteID = ConvertToInt(rdr["SiteID"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.CompanyName = ConverToStr(rdr["CompanyName"]);
            model.CompanyDescribe = ConverToStr(rdr["CompanyDescribe"]);
            model.IsConfirm = ConvertToInt(rdr["IsConfirm"]);
            model.VIP = ConvertToInt(rdr["VIP"]);
            model.salt = ConverToStr(rdr["salt"]);
            model.State = ConvertToInt(rdr["State"]);
            model.RegisterIP = ConverToStr(rdr["RegisterIP"]);
            model.CerificateDeadLine = ConvertToDate(rdr["CerificateDeadLine"]);
            model.PageID = ConvertToInt(rdr["PageId"]);
            model.SiteRebateBalance = ConverToDouble(rdr["SiteRebateBalance"]);
            model.UserCreit = ConvertToInt(rdr["UserCreit"]);
            model.RoomID = ConvertToInt(rdr["RoomID"]);
            model.ParentUserID = ConverToStr(rdr["ParentUserID"]);
            model.IsCloud = ConvertToInt(rdr["IsCloud"].ToString());
            model.SilverCoin = ConverToDouble(rdr["SilverCoin"]);
            model.PayPassWord = ConverToStr(rdr["PayPassWord"]);
            model.seturl = ConverToStr(rdr["seturl"]);
            model.GuestScore = ConvertToInt(rdr["GuestScore"]);
            model.StructureID = ConverToStr(rdr["StructureID"]);
            model.UserRole = ConverToStr(rdr["UserRole"]);
            model.WorkNum = ConverToStr(rdr["WorkNum"]);
            model.MailSize = ConvertToInt(rdr["MailSize"]);
            model.LastActiveTime = ConvertToDate(rdr["LastActiveTime"]);
            model.ZnPassword = ConverToStr(rdr["ZnPassword"]);
            model.HoneyName = ConverToStr(rdr["HoneyName"]);
            rdr.Close();
            return model;
        }
        public M_UserInfo GetModelFromReader(DataRow rdr)
        {
            M_UserInfo model = new M_UserInfo();
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.UserName = rdr["UserName"].ToString();
            model.UserPwd = rdr["UserPwd"].ToString();
            model.GroupID = ConvertToInt(rdr["GroupID"]);
            model.Email = rdr["Email"].ToString();
            model.Question = ConverToStr(rdr["Question"]);
            model.Answer = ConverToStr(rdr["Answer"]);
            model.RegTime = ConvertToDate(rdr["RegTime"]);
            model.LoginTimes = ConvertToInt(rdr["LoginTimes"]);
            model.LastLoginTimes = ConvertToDate(rdr["LastLoginTime"]);
            model.LastLoginIP = ConverToStr(rdr["LastLoginIP"]);
            model.LastPwdChangeTime = ConvertToDate(rdr["LastPwdChangeTime"]);
            model.LastLockTime = ConvertToDate(rdr["LastLockTime"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.State = ConvertToInt(rdr["State"]);
            model.CheckNum = ConverToStr(rdr["CheckNum"]);
            model.Purse = ConverToDouble(rdr["Purse"]);
            model.UserExp = ConverToDouble(rdr["UserExp"]);
            model.SilverCoin = ConverToDouble(rdr["SilverCoin"]);
            model.UserPoint = ConverToDouble(rdr["UserPoint"]);
            model.DummyPurse = ConverToDouble(rdr["DummyPurse"]);
            model.GuestScore = ConvertToInt(rdr["GuestScore"]);
            model.boffExp = ConvertToInt(rdr["boffExp"]);
            model.ConsumeExp = ConvertToInt(rdr["ConsumeExp"]);
            model.DeadLine = ConvertToDate(rdr["DeadLine"]);
            model.JoinTime = ConvertToDate(rdr["JoinTime"]);
            model.TrueName = ConverToStr(rdr["Permissions"]);
            model.SiteID = ConvertToInt(rdr["SiteID"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.CompanyName = ConverToStr(rdr["CompanyName"]);
            model.CompanyDescribe = ConverToStr(rdr["CompanyDescribe"]);
            model.IsConfirm = ConvertToInt(rdr["IsConfirm"]);
            model.VIP = ConvertToInt(rdr["VIP"]);
            model.salt = ConverToStr(rdr["salt"]);
            model.RegisterIP = ConverToStr(rdr["RegisterIP"]);
            model.CerificateDeadLine = ConvertToDate(rdr["CerificateDeadLine"]);
            model.PageID = ConvertToInt(rdr["PageId"]);
            model.SiteRebateBalance = ConverToDouble(rdr["SiteRebateBalance"]);
            model.UserCreit = ConvertToInt(rdr["UserCreit"]);
            model.RoomID = ConvertToInt(rdr["RoomID"]);
            model.ParentUserID = ConverToStr(rdr["ParentUserID"]);
            model.IsCloud = ConvertToInt(rdr["IsCloud"].ToString());
            model.PayPassWord = ConverToStr(rdr["PayPassWord"]);
            model.seturl = ConverToStr(rdr["seturl"]);
            model.StructureID = ConverToStr(rdr["StructureID"]);
            model.UserRole = ConverToStr(rdr["UserRole"]);
            model.WorkNum = ConverToStr(rdr["WorkNum"]);
            model.MailSize = ConvertToInt(rdr["MailSize"]);
            model.LastActiveTime = ConvertToDate(rdr["LastActiveTime"]);
            model.ZnPassword = ConverToStr(rdr["ZnPassword"]);
            model.HoneyName = ConverToStr(rdr["HoneyName"]);
            return model;
        }
        public void EmptyDeal()
        {
            if (LastActiveTime <= DateTime.MinValue) LastActiveTime = DateTime.Now;
            if (LastLoginTimes <= DateTime.MinValue) LastLoginTimes = DateTime.Now;
            if (DeadLine <= DateTime.MinValue) { DeadLine = DateTime.MaxValue; }
            if (RegTime <= DateTime.MinValue) { RegTime = DateTime.Now; }
            if (LastPwdChangeTime <= DateTime.MinValue) { LastPwdChangeTime = DateTime.Now; }
            if (LastLockTime <= DateTime.MinValue) { LastLockTime = DateTime.Now; }
            if (CerificateDeadLine <= DateTime.MinValue) { CerificateDeadLine = DateTime.Now; }
            if (UpdateTime <= DateTime.MinValue) { UpdateTime = DateTime.Now; }
            if (JoinTime <= DateTime.MinValue) { JoinTime = DateTime.Now; }
            if (string.IsNullOrEmpty(ZnPassword)) ZnPassword = "";
            if (string.IsNullOrEmpty(WorkNum)) WorkNum = "";
            if (!string.IsNullOrEmpty(ParentUserID)) { ParentUserID = ParentUserID.Replace(" ", ""); }
        }
    }
    /// <summary>
    /// 网络上传输用,用户相关API
    /// </summary>
    public class M_AJAXUser
    {
        public int UserID = 0;
        public int GroupID = 0;
        public string UserName = "";
        public string UserFace = "";
        public string TrueName = "";
        public string HoneyName = "";
        public double? purse = null;
        public double? sicon = null;
        public double? point = null;
        public string openid = null;
        public M_AJAXUser() { }
        public M_AJAXUser(M_UserInfo mu)
        {
            Copy(mu);
        }
        public void Copy(M_UserInfo mu)
        {
            if (mu == null || mu.IsNull) { return; }
            UserID = mu.UserID;
            UserName = mu.UserName;
            UserFace = mu.UserFace;
            TrueName = mu.TrueName;
            HoneyName = mu.HoneyName;
            purse = mu.Purse;
            sicon = mu.SilverCoin;
            point = mu.UserExp;
            GroupID = mu.GroupID;
            openid = mu.OpenID;
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None,
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }

}
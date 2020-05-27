namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;
    /// <summary>
    /// 用户基本信息
    /// </summary>
    public class M_Uinfo : M_Base
    {

        ///<summary>
        ///头像地址
        ///</summary>
        private string userFace = String.Empty;

        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public M_Uinfo()
        {
            this.IsNull = false;
        }
        public M_Uinfo(bool value)
        {
            this.IsNull = value;
        }
        ///<summary>
        ///
        ///</summary>
        public M_Uinfo
        (
            int userID,
            string trueName,
            string birthDay,
            string address,
            string officePhone,
            string homePhone,
            string fax,
            string zipCode,
            string pHS,
            string mobile,
            bool userSex,
            string qQ,
            string mSN,
            string iCQ,
            string uC,
            string yahoo,
            string iDCard,
            string homePage,
            string userFace,
            int faceWidth,
            int faceHeight,
            string sign,
            string country,
            string province,
            string city,
            string county,
            int privacy,
            string honeyName,
            string cardType,
            string workProvince,
            string workCounty,
            string shengxiao,
            string xingzuo,
            int VIPUser,
            int bugle
        )
        {
            this.UserId = userID;
            this.TrueName = trueName;
            this.BirthDay = birthDay;
            this.Address = address;
            this.OfficePhone = officePhone;
            this.HomePhone = homePhone;
            this.Fax = fax;
            this.ZipCode = zipCode;
            this.PHS = pHS;
            this.Mobile = mobile;
            this.UserSex = userSex;
            this.QQ = qQ;
            this.MSN = mSN;
            this.ICQ = iCQ;
            this.UC = uC;
            this.Yahoo = yahoo;
            this.IDCard = iDCard;
            this.HomePage = homePage;
            this.UserFace = userFace;
            this.FaceWidth = faceWidth;
            this.FaceHeight = faceHeight;
            this.Sign = sign;
            this.Country = country;
            this.Province = province;
            this.City = city;
            this.County = county;
            this.Privating = privacy;
            this.HoneyName = honeyName;
            this.CardType = cardType;
            this.WorkProvince = workProvince;
            this.WorkCounty = workCounty;
            this.Shengxiao = shengxiao;
            this.Xingzuo = xingzuo;
            this.VIPUser = VIPUser;
            this.Bugle = bugle;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public int UserId { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string TrueName { get; set; }

        ///<summary>
        ///生日
        ///</summary>
        public string BirthDay { get; set; }

        /// <summary>
        /// 多人视频小喇叭
        /// </summary>
        public int Bugle { get; set; }
        ///<summary>
        ///联系地址
        ///</summary>
        public string Address { get; set; }

        ///<summary>
        ///办公电话
        ///</summary>
        public string OfficePhone { get; set; }

        ///<summary>
        ///家庭电话
        ///</summary>
        public string HomePhone { get; set; }

        ///<summary>
        ///传真号码
        ///</summary>
        public string Fax { get; set; }

        ///<summary>
        ///邮编
        ///</summary>
        public string ZipCode { get; set; }

        ///<summary>
        ///小灵通
        ///</summary>
        public string PHS { get; set; }

        ///<summary>
        ///手机号
        ///</summary>
        public string Mobile { get; set; }

        ///<summary>
        ///性别
        ///</summary>
        public bool UserSex { get; set; }

        ///<summary>
        ///QQ号码
        ///</summary>
        public string QQ { get; set; }

        ///<summary>
        ///MSN号码
        ///</summary>
        public string MSN { get; set; }

        ///<summary>
        ///ICQ号码
        ///</summary>
        public string ICQ { get; set; }

        ///<summary>
        ///UC号码
        ///</summary>
        public string UC { get; set; }

        ///<summary>
        ///雅虎通账号
        ///</summary>
        public string Yahoo { get; set; }

        ///<summary>
        ///身份证号码
        ///</summary>
        public string IDCard { get; set; }

        ///<summary>
        ///个人主页
        ///</summary>
        public string HomePage { get; set; }

        ///<summary>
        ///头像地址
        ///</summary>
        public string UserFace
        {
            get
            {
                if (string.IsNullOrEmpty(userFace))
                    return "/Images/userface/noface.png";
                else
                    return userFace;
            }
            set { userFace = value; }
        }

        ///<summary>
        ///头像宽度
        ///</summary>
        public int FaceWidth { get; set; }

        ///<summary>
        ///头像高度
        ///</summary>
        public int FaceHeight { get; set; }

        ///<summary>
        ///签名档
        ///</summary>
        public string Sign { get; set; }

        ///<summary>
        ///国家
        ///</summary>
        public string Country { get; set; }

        ///<summary>
        /// 户籍省
        ///</summary>
        public string Province { get; set; }

        public string City { get; set; }
        ///<summary>
        ///户籍县
        ///</summary>
        public string County { get; set; }

        ///<summary>
        ///隐私设定
        ///</summary>
        public int Privating { get; set; }

        public bool IsNull { get; private set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string HoneyName { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 工作所在省
        /// </summary>
        public string WorkProvince { get; set; }
        /// <summary>
        /// 工作所在县市
        /// </summary>
        public string WorkCounty { get; set; }
        /// <summary>
        /// 征友状态：0征友中;1已经找到;-1暂停交友
        /// </summary>
        public int SFStatus { get; set; }
        /// <summary>
        /// 生肖
        /// </summary>
        public string Shengxiao { get; set; }
        /// <summary>
        /// 星座
        /// </summary>
        public string Xingzuo { get; set; }

        /// <summary>
        /// VIP会员组
        /// </summary>
        public int VIPUser { get; set; }

        ///<summary>
        ///公司名称
        ///</summary>
        public string Position { get; set; }
        ///<summary>
        ///职务
        ///</summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        public override string TbName { get { return "ZL_UserBase"; } }
        public string _pk = "UserID";
        public override string PK { get { return _pk; } }
        #endregion
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"UserId","Int","4"},
                                  {"TrueName","NVarChar","50"},
                                  {"BirthDay","NVarChar","20"},
                                  {"Address","NVarChar","255"},
                                  {"HomePhone","NVarChar","50"},
                                  {"Fax","NVarChar","50"},
                                  {"ZipCode","NVarChar","10"},
                                  {"Mobile","NVarChar","50"},
                                  {"UserSex","Bit","1"},
                                  {"QQ","NVarChar","50"},
                                  {"MSN","NVarChar","255"},
                                  {"ICQ","NVarChar","50"},
                                  {"UC","NVarChar","50"},
                                  {"Yahoo","NVarChar","50"},
                                  {"IDCard","NVarChar","50"},
                                  {"HomePage","NVarChar","255"},
                                  {"UserFace","NVarChar","255"},
                                  {"FaceWidth","Int","4"},
                                  {"FaceHeight","Int","4"},
                                  {"Sign","NVarChar","1000"},
                                  {"Country","NVarChar","50"},
                                  {"Province","NVarChar","50"},
                                  {"City","NVarChar","50"},
                                  {"County","NVarChar","50"},
                                  {"Privacy","Int","4"},
                                  {"OfficePhone","NVarChar","50"},
                                  {"PHS","NVarChar","50"},
                                  {"HoneyName","NVarChar","50"},
                                  {"CardType","NVarChar","255"},
                                  {"Shengxiao","NVarChar","50"},
                                  {"Xingzuo","NVarChar","50"},
                                  {"WorkProvince","NVarChar","50"},
                                  {"WorkCity","NVarChar","50"},
                                  {"SFStatus","Int","4"},
                                  {"VIPUser","Int","4"},
                                  {"Bugle","Int","4"},
                                  {"Position","NVarChar","50"},
                                  {"Corp","NVarChar","50"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Uinfo model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.UserId;
            sp[1].Value = model.TrueName;
            sp[2].Value = model.BirthDay;
            sp[3].Value = model.Address;
            sp[4].Value = model.HomePhone;
            sp[5].Value = model.Fax;
            sp[6].Value = model.ZipCode;
            sp[7].Value = model.Mobile;
            sp[8].Value = model.UserSex;
            sp[9].Value = model.QQ;
            sp[10].Value = model.MSN;
            sp[11].Value = model.ICQ;
            sp[12].Value = model.UC;
            sp[13].Value = model.Yahoo;
            sp[14].Value = model.IDCard;
            sp[15].Value = model.HomePage;
            sp[16].Value = model.userFace;
            sp[17].Value = model.FaceWidth;
            sp[18].Value = model.FaceHeight;
            sp[19].Value = model.Sign;
            sp[20].Value = model.Country;
            sp[21].Value = model.Province;
            sp[22].Value = model.City;
            sp[23].Value = model.County;
            sp[24].Value = model.Privating;
            sp[25].Value = model.OfficePhone;
            sp[26].Value = model.PHS;
            sp[27].Value = model.HoneyName;
            sp[28].Value = model.CardType;
            sp[29].Value = model.Shengxiao;
            sp[30].Value = model.Xingzuo;
            sp[31].Value = model.WorkProvince;
            sp[32].Value = model.WorkCounty;
            sp[33].Value = model.SFStatus;
            sp[34].Value = model.VIPUser;
            sp[35].Value = model.Bugle;
            sp[36].Value = model.Position;
            sp[37].Value = model.CompanyName;
            return sp;
        }
        public M_Uinfo GetModelFromReader(SqlDataReader rdr)
        {
            M_Uinfo model = new M_Uinfo();
            model.UserId = Convert.ToInt32(rdr["UserID"]);
            model.TrueName = ConverToStr(rdr["TrueName"]);
            model.BirthDay = ConverToStr(rdr["BirthDay"]);
            model.Address = ConverToStr(rdr["Address"]);
            model.HomePhone = ConverToStr(rdr["HomePhone"]);
            model.Fax = ConverToStr(rdr["Fax"]);
            model.ZipCode = ConverToStr(rdr["ZipCode"]);
            model.Mobile = rdr["Mobile"].ToString();
            model.UserSex = ConverToBool(rdr["UserSex"]);
            model.QQ = ConverToStr(rdr["QQ"]);
            model.MSN = ConverToStr(rdr["MSN"]);
            model.ICQ = ConverToStr(rdr["ICQ"]);
            model.UC = ConverToStr(rdr["UC"]);
            model.Yahoo = ConverToStr(rdr["Yahoo"]);
            model.IDCard = ConverToStr(rdr["IDCard"]);
            model.HomePage = ConverToStr(rdr["HomePage"]);
            model.UserFace = ConverToStr(rdr["UserFace"]);
            model.FaceWidth = ConvertToInt(rdr["FaceWidth"]);
            model.FaceHeight = ConvertToInt(rdr["FaceHeight"]);
            model.Sign = ConverToStr(rdr["Sign"]);
            model.Country = ConverToStr(rdr["Country"]);
            model.Privating = ConvertToInt(rdr["Privacy"]);
            model.Province = ConverToStr(rdr["Province"]);
            model.City = ConverToStr(rdr["City"]);
            model.County = ConverToStr(rdr["County"]);
            model.CardType = ConverToStr(rdr["cardType"]);
            model.OfficePhone = ConverToStr(rdr["OfficePhone"]);
            model.PHS = ConverToStr(rdr["PHS"]);
            model.HoneyName = ConverToStr(rdr["honeyName"]);
            model.Shengxiao = ConverToStr(rdr["Shengxiao"]);
            model.Xingzuo = ConverToStr(rdr["Xingzuo"]);
            model.WorkProvince = rdr["WorkProvince"].ToString();
            model.WorkCounty = rdr["workCity"].ToString();
            model.SFStatus = ConvertToInt(rdr["SFStatus"]);
            model.VIPUser = ConvertToInt(rdr["VIPUser"]);
            model.Bugle = ConvertToInt(rdr["Bugle"]);
            model.Position = ConverToStr(rdr["Position"]);
            model.CompanyName = ConverToStr(rdr["Corp"]);
            rdr.Close();
            return model;
        }
    }
}
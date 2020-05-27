using System;
namespace ZoomLa.Sns.Model
{
    public class UserMoreinfo
    {
        #region 定义字段
        /// <summary>
        /// 用户ID
        /// </summary>
        private int m_userID;
        /// <summary>
        /// 
        /// </summary>
        private string m_userIdcard = string.Empty;
        /// <summary>
        /// 性别
        /// </summary>
        private bool m_userSex;
        /// <summary>
        /// 身高
        /// </summary>
        private string m_userStature = string.Empty;
        /// <summary>
        /// 体重
        /// </summary>
        private string m_userAvoir = string.Empty;
        /// <summary>
        /// 出生日期
        /// </summary>
        private string m_userBir=string.Empty;
        /// <summary>
        /// 婚姻状况
        /// </summary>
        private string m_userMarry = string.Empty;
        /// <summary>
        /// 学历
        /// </summary>
        private string m_userBachelor = string.Empty;
        /// <summary>
        /// 月收入
        /// </summary>
        private string m_userMonthIn = string.Empty;
        /// <summary>
        /// 住房条件
        /// </summary>
        private string m_userHome = string.Empty;
        /// <summary>
        /// 有没有孩子
        /// </summary>
        private string m_userChild = string.Empty;
        /// <summary>
        /// 性别
        /// </summary>
        private bool m_userConsortSex;
        /// <summary>
        /// 年龄
        /// </summary>
        private string m_userConsortAge = string.Empty;
        /// <summary>
        /// 工作地区
        /// </summary>
        private string m_userConsortWorkArea = string.Empty;
        /// <summary>
        /// 婚姻状况
        /// </summary>
        private string m_userConsortMarry = string.Empty;
        /// <summary>
        /// 身高
        /// </summary>
        private string m_userConsortStature = string.Empty;
        /// <summary>
        /// 体重
        /// </summary>
        private string m_userConsortAvoir = string.Empty;
        /// <summary>
        /// 月收入
        /// </summary>
        private string m_userConsortMonthIN = string.Empty;
        /// <summary>
        /// 学历
        /// </summary>
        private string m_userConsortBachelor = string.Empty;
        /// <summary>
        /// 住房条件
        /// </summary>
        private string m_userConsortHome = string.Empty;
        /// <summary>
        /// 其它要求
        /// </summary>
        private string m_userConsortOther = string.Empty;
        /// <summary>
        /// 体型
        /// </summary>
        private string m_userSomato = string.Empty;
        /// <summary>
        /// 血型
        /// </summary>
        private string m_userBlood = string.Empty;
        /// <summary>
        /// 省
        /// </summary>
        private string m_userProvince = string.Empty;
        /// <summary>
        /// 市
        /// </summary>
        private string m_userCity = string.Empty;
        /// <summary>
        /// 民族
        /// </summary>
        private string m_userNation = string.Empty;
        /// <summary>
        /// 毕业学校
        /// </summary>
        private string m_userSchool = string.Empty;
        /// <summary>
        /// 兄弟姐妹
        /// </summary>
        private string m_userBrother = string.Empty;
        /// <summary>
        /// 语言能力
        /// </summary>
        private string m_userLanguage = string.Empty;
        /// <summary>
        /// 生活照片
        /// </summary>
        private string m_userLifePic4 = string.Empty;
        /// <summary>
        /// 生活照片
        /// </summary>
        private string m_userLifePic3 = string.Empty;
        /// <summary>
        /// 生活照片
        /// </summary>
        private string m_userLifePic1 = string.Empty;
        /// <summary>
        /// 生活状态
        /// </summary>
        private string m_userLife = string.Empty;
        /// <summary>
        /// 是否吸烟
        /// </summary>
        private string m_userSmoke = string.Empty;
        /// <summary>
        /// 是否喝酒
        /// </summary>
        private string m_userDrink = string.Empty;
        /// <summary>
        /// 职业类别
        /// </summary>
        private string m_userWork = string.Empty;
        /// <summary>
        /// 是否购车
        /// </summary>
        private string m_userCar = string.Empty;
        /// <summary>
        /// 公司类别
        /// </summary>
        private string m_userCom = string.Empty;
        /// <summary>
        /// 是否想要孩子
        /// </summary>
        private string m_userNeedchild = string.Empty;
        /// <summary>
        /// 喜欢的活动
        /// </summary>
        private string m_userAcctive = string.Empty;
        /// <summary>
        /// 喜欢的体育运动
        /// </summary>
        private string m_userAth = string.Empty;
        /// <summary>
        /// 喜欢的音乐
        /// </summary>
        private string m_userMusic = string.Empty;
        /// <summary>
        /// 喜欢的影视节目/书
        /// </summary>
        private string m_userMovie = string.Empty;
        /// <summary>
        /// 喜欢的食物
        /// </summary>
        private string m_userFood = string.Empty;
        /// <summary>
        /// 喜欢的地方
        /// </summary>
        private string m_userArea = string.Empty;
        /// <summary>
        /// 兴趣爱好，宠物，生活技能
        /// </summary>
        private string m_userlovepetlife = string.Empty;
        /// <summary>
        /// 我的内心独白
        /// </summary>
        private string m_userHeart = string.Empty;
        /// <summary>
        /// 我的爱情宣言
        /// </summary>
        private string m_userLove = string.Empty;
        /// <summary>
        /// 生活照片
        /// </summary>
        private string m_userLifePic2 = string.Empty;
        /// <summary>
        /// 星座
        /// </summary>
        private string m_userConstellation = string.Empty;

        ///<summary>
        ///图片
        ///</summary>
        private string userPic = String.Empty;

        ///<summary>
        ///用户名
        ///</summary>
        private string userName = String.Empty;

        /// <summary>
        /// 信仰
        /// </summary>
        private string m_Xinyang = String.Empty;
        /// <summary>
        /// 外貌自评
        /// </summary>
        private string m_Facey=String.Empty;
        /// <summary>
        /// 脸型
        /// </summary>
        private string m_FaceType = String.Empty;
        /// <summary>
        /// 发型
        /// </summary>
        private string m_HairType = String.Empty;
        /// <summary>
        /// 工作前景、状态等
        /// </summary>
        private string m_JobAnd = String.Empty;
       
        #endregion

        #region 构造函数
        public UserMoreinfo()
        {
        }

        public UserMoreinfo
        (
            int UserID,
            string UserIdcard,
            bool UserSex,
            string UserStature,
            string UserAvoir,
            string UserBir,
            string UserMarry,
            string UserBachelor,
            string UserMonthIn,
            string UserHome,
            string UserChild,
            bool UserConsortSex,
            string UserConsortAge,
            string UserConsortWorkArea,
            string UserConsortMarry,
            string UserConsortStature,
            string UserConsortAvoir,
            string UserConsortMonthIN,
            string UserConsortBachelor,
            string UserConsortHome,
            string UserConsortOther,
            string UserSomato,
            string UserBlood,
            string UserProvince,
            string UserCity,
            string UserNation,
            string UserSchool,
            string UserBrother,
            string UserLanguage,
            string UserLifePic4,
            string UserLifePic3,
            string UserLifePic1,
            string UserLife,
            string UserSmoke,
            string UserDrink,
            string UserWork,
            string UserCar,
            string UserCom,
            string UserNeedchild,
            string UserAcctive,
            string UserAth,
            string UserMusic,
            string UserMovie,
            string UserFood,
            string UserArea,
            string UserHeart,
            string UserLove,
            string UserLifePic2,
            string UserConstellation
        )
        {
            this.UserID = m_userID;
            this.UserIdcard = m_userIdcard;
            this.UserSex = m_userSex;
            this.UserStature = m_userStature;
            this.UserAvoir = m_userAvoir;
            this.UserBir = m_userBir;
            this.UserMarry = m_userMarry;
            this.UserBachelor = m_userBachelor;
            this.UserMonthIn = m_userMonthIn;
            this.UserHome = m_userHome;
            this.UserChild = m_userChild;
            this.UserConsortSex = m_userConsortSex;
            this.UserConsortAge = m_userConsortAge;
            this.UserConsortWorkArea = m_userConsortWorkArea;
            this.UserConsortMarry = m_userConsortMarry;
            this.UserConsortStature = m_userConsortStature;
            this.UserConsortAvoir = m_userConsortAvoir;
            this.UserConsortMonthIN = m_userConsortMonthIN;
            this.UserConsortBachelor = m_userConsortBachelor;
            this.UserConsortHome = m_userConsortHome;
            this.UserConsortOther = m_userConsortOther;
            this.UserSomato = m_userSomato;
            this.UserBlood = m_userBlood;
            this.UserProvince = m_userProvince;
            this.UserCity = m_userCity;
            this.UserNation = m_userNation;
            this.UserSchool = m_userSchool;
            this.UserBrother = m_userBrother;
            this.UserLanguage = m_userLanguage;
            this.UserLifePic4 = m_userLifePic4;
            this.UserLifePic3 = m_userLifePic3;
            this.UserLifePic1 = m_userLifePic1;
            this.UserLife = m_userLife;
            this.UserSmoke = m_userSmoke;
            this.UserDrink = m_userDrink;
            this.UserWork = m_userWork;
            this.UserCar = m_userCar;
            this.UserCom = m_userCom;
            this.UserNeedchild = m_userNeedchild;
            this.UserAcctive = m_userAcctive;
            this.UserAth = m_userAth;
            this.UserMusic = m_userMusic;
            this.UserMovie = m_userMovie;
            this.UserFood = m_userFood;
            this.UserArea = m_userArea;
            this.UserHeart = m_userHeart;
            this.UserLove = m_userLove;
            this.UserLifePic2 = m_userLifePic2;
            this.UserConstellation = m_userConstellation;
            
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] Sns_UserMoreinfoList()
        {
            string[] Tablelist = { "UserID", "UserIdcard", "UserSex", "UserStature", "UserAvoir", "UserBir", "UserMarry", "UserBachelor", "UserMonthIn", "UserHome", "UserChild", "UserConsortSex", "UserConsortAge", "UserConsortWorkArea", "UserConsortMarry", "UserConsortStature", "UserConsortAvoir", "UserConsortMonthIN", "UserConsortBachelor", "UserConsortHome", "UserConsortOther", "UserSomato", "UserBlood", "UserProvince", "UserCity", "UserNation", "UserSchool", "UserBrother", "UserLanguage", "UserLifePic4", "UserLifePic3", "UserLifePic1", "UserLife", "UserSmoke", "UserDrink", "UserWork", "UserCar", "UserCom", "UserNeedchild", "UserAcctive", "UserAth", "UserMusic", "UserMovie", "UserFood", "UserArea", "UserHeart", "UserLove", "UserLifePic2", "UserConstellation" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get { return m_userID; }
            set { m_userID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserIdcard
        {
            get { return m_userIdcard; }
            set { m_userIdcard = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public bool UserSex
        {
            get { return m_userSex; }
            set { m_userSex = value; }
        }
        /// <summary>
        /// 身高
        /// </summary>
        public string UserStature
        {
            get { return m_userStature; }
            set { m_userStature = value; }
        }
        /// <summary>
        /// 体重
        /// </summary>
        public string UserAvoir
        {
            get { return m_userAvoir; }
            set { m_userAvoir = value; }
        }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string UserBir
        {
            get
            {
                if (m_userBir.ToString ()=="")
                    return string.Empty;
                else
                    return m_userBir;
            }
            set { m_userBir = value; }
        }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string UserMarry
        {
            get { return m_userMarry; }
            set { m_userMarry = value; }
        }
        /// <summary>
        /// 学历
        /// </summary>
        public string UserBachelor
        {
            get { return m_userBachelor; }
            set { m_userBachelor = value; }
        }
        /// <summary>
        /// 月收入
        /// </summary>
        public string UserMonthIn
        {
            get { return m_userMonthIn; }
            set { m_userMonthIn = value; }
        }
        /// <summary>
        /// 住房条件
        /// </summary>
        public string UserHome
        {
            get { return m_userHome; }
            set { m_userHome = value; }
        }
        /// <summary>
        /// 有没有孩子
        /// </summary>
        public string UserChild
        {
            get { return m_userChild; }
            set { m_userChild = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public bool UserConsortSex
        {
            get { return m_userConsortSex; }
            set { m_userConsortSex = value; }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public string UserConsortAge
        {
            get { return m_userConsortAge; }
            set { m_userConsortAge = value; }
        }
        /// <summary>
        /// 工作地区
        /// </summary>
        public string UserConsortWorkArea
        {
            get { return m_userConsortWorkArea; }
            set { m_userConsortWorkArea = value; }
        }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string UserConsortMarry
        {
            get { return m_userConsortMarry; }
            set { m_userConsortMarry = value; }
        }
        /// <summary>
        /// 身高
        /// </summary>
        public string UserConsortStature
        {
            get { return m_userConsortStature; }
            set { m_userConsortStature = value; }
        }
        /// <summary>
        /// 体重
        /// </summary>
        public string UserConsortAvoir
        {
            get { return m_userConsortAvoir; }
            set { m_userConsortAvoir = value; }
        }
        /// <summary>
        /// 月收入
        /// </summary>
        public string UserConsortMonthIN
        {
            get { return m_userConsortMonthIN; }
            set { m_userConsortMonthIN = value; }
        }
        /// <summary>
        /// 学历
        /// </summary>
        public string UserConsortBachelor
        {
            get { return m_userConsortBachelor; }
            set { m_userConsortBachelor = value; }
        }
        /// <summary>
        /// 住房条件
        /// </summary>
        public string UserConsortHome
        {
            get { return m_userConsortHome; }
            set { m_userConsortHome = value; }
        }
        /// <summary>
        /// 其它要求
        /// </summary>
        public string UserConsortOther
        {
            get { return m_userConsortOther; }
            set { m_userConsortOther = value; }
        }
        /// <summary>
        /// 体型
        /// </summary>
        public string UserSomato
        {
            get { return m_userSomato; }
            set { m_userSomato = value; }
        }
        /// <summary>
        /// 血型
        /// </summary>
        public string UserBlood
        {
            get { return m_userBlood; }
            set { m_userBlood = value; }
        }
        /// <summary>
        /// 省
        /// </summary>
        public string UserProvince
        {
            get { return m_userProvince; }
            set { m_userProvince = value; }
        }
        /// <summary>
        /// 市
        /// </summary>
        public string UserCity
        {
            get { return m_userCity; }
            set { m_userCity = value; }
        }
        /// <summary>
        /// 民族
        /// </summary>
        public string UserNation
        {
            get { return m_userNation; }
            set { m_userNation = value; }
        }
        /// <summary>
        /// 毕业学校
        /// </summary>
        public string UserSchool
        {
            get { return m_userSchool; }
            set { m_userSchool = value; }
        }
        /// <summary>
        /// 兄弟姐妹
        /// </summary>
        public string UserBrother
        {
            get { return m_userBrother; }
            set { m_userBrother = value; }
        }
        /// <summary>
        /// 语言能力
        /// </summary>
        public string UserLanguage
        {
            get { return m_userLanguage; }
            set { m_userLanguage = value; }
        }
        /// <summary>
        /// 生活照片
        /// </summary>
        public string UserLifePic4
        {
            get {
                if (!string.IsNullOrEmpty(m_userLifePic4))
                    return m_userLifePic4;
                else
                    return "/Images/nopic.gif";
            }
            set { m_userLifePic4 = value; }
        }
        /// <summary>
        /// 生活照片
        /// </summary>
        public string UserLifePic3
        {
            get
            {
                if (!string.IsNullOrEmpty(m_userLifePic3))
                    return m_userLifePic3;
                else
                    return "/Images/nopic.gif";
            }
            set { m_userLifePic3 = value; }
        }
        /// <summary>
        /// 生活照片
        /// </summary>
        public string UserLifePic2
        {
            get
            {
                if (!string.IsNullOrEmpty(m_userLifePic2))
                    return m_userLifePic2;
                else
                    return "/Images/nopic.gif";
            }
            set { m_userLifePic2 = value; }
        }
        /// <summary>
        /// 生活照片
        /// </summary>
        public string UserLifePic1
        {
            get
            {
                if (!string.IsNullOrEmpty(m_userLifePic1))
                    return m_userLifePic1;
                else
                    return "/Images/nopic.gif";
            }
            set { m_userLifePic1 = value; }
        }
        /// <summary>
        /// 生活状态
        /// </summary>
        public string UserLife
        {
            get { return m_userLife; }
            set { m_userLife = value; }
        }
        /// <summary>
        /// 是否吸烟
        /// </summary>
        public string UserSmoke
        {
            get { return m_userSmoke; }
            set { m_userSmoke = value; }
        }
        /// <summary>
        /// 是否喝酒
        /// </summary>
        public string UserDrink
        {
            get { return m_userDrink; }
            set { m_userDrink = value; }
        }
        /// <summary>
        /// 职业类别
        /// </summary>
        public string UserWork
        {
            get { return m_userWork; }
            set { m_userWork = value; }
        }
        /// <summary>
        /// 是否购车
        /// </summary>
        public string UserCar
        {
            get { return m_userCar; }
            set { m_userCar = value; }
        }
        /// <summary>
        /// 公司类别
        /// </summary>
        public string UserCom
        {
            get { return m_userCom; }
            set { m_userCom = value; }
        }
        /// <summary>
        /// 是否想要孩子
        /// </summary>
        public string UserNeedchild
        {
            get { return m_userNeedchild; }
            set { m_userNeedchild = value; }
        }
        /// <summary>
        /// 喜欢的活动
        /// </summary>
        public string UserAcctive
        {
            get { return m_userAcctive; }
            set { m_userAcctive = value; }
        }
        /// <summary>
        /// 喜欢的体育运动
        /// </summary>
        public string UserAth
        {
            get { return m_userAth; }
            set { m_userAth = value; }
        }
        /// <summary>
        /// 喜欢的音乐
        /// </summary>
        public string UserMusic
        {
            get { return m_userMusic; }
            set { m_userMusic = value; }
        }
        /// <summary>
        /// 喜欢的影视节目
        /// </summary>
        public string UserMovie
        {
            get { return m_userMovie; }
            set { m_userMovie = value; }
        }
        /// <summary>
        /// 喜欢的食物
        /// </summary>
        public string UserFood
        {
            get { return m_userFood; }
            set { m_userFood = value; }
        }
        /// <summary>
        /// 喜欢的地方
        /// </summary>
        public string UserArea
        {
            get { return m_userArea; }
            set { m_userArea = value; }
        }
        /// <summary>
        /// 我的内心独白
        /// </summary>
        public string UserHeart
        {
            get { return m_userHeart; }
            set { m_userHeart = value; }
        }
        /// <summary>
        /// 我的爱情宣言
        /// </summary>
        public string UserLove
        {
            get { return m_userLove; }
            set { m_userLove = value; }
        }
        
        /// <summary>
        /// 星座
        /// </summary>
        public string UserConstellation
        {
            get { return m_userConstellation; }
            set { m_userConstellation = value; }
        }

        ///<summary>
        ///图片
        ///</summary>
        public string UserPic
        {
            get
            {
                if (userPic != "")
                    return userPic;
                else
                    return @"~/Images/head.jpg";
            }
            set { userPic = value; }
        }

        ///<summary>
        ///用户名
        ///</summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        /// <summary>
        /// 信仰
        /// </summary>
        public string Xinyang
        {
            get { return m_Xinyang; }
            set { m_Xinyang = value; }
        }
        /// <summary>
        /// 外貌自评
        /// </summary>
        public string Facey
        {
            get { return m_Facey; }
            set { m_Facey = value; }
        }
        /// <summary>
        /// 脸型
        /// </summary>
        public string FaceType
        {
            get { return m_FaceType; }
            set { m_FaceType = value; }
        }
        /// <summary>
        /// 发型
        /// </summary>
        public string HairType
        {
            get { return m_HairType; }
            set { m_HairType = value; }
        }
        public string Lovepetlife
        {
            get { return m_userlovepetlife; }
            set { m_userlovepetlife = value; }
        }
        public string JobAnd
        {
            get { return m_JobAnd; }
            set { m_JobAnd = value; }
        }

        #endregion
    }
}


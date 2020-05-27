using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.Model;
using ZoomLa.Sns.Model;
using FHModel;
using ZoomLa.SQLDAL;

namespace ZoomLa.Sns.Logic
{
    public class UserTableLogic
    {
        #region 初始化用户详细表信息
        /// <summary>
        /// 初始化用户详细表信息
        /// </summary>
        /// <param name="id"></param>
        public static void AddMoreinfo(int id)
        {
            try
            {
                string SqlStr = @"SET IDENTITY_INSERT [ZL_Sns_UserMoreinfo] ON;
                insert into ZL_Sns_UserMoreinfo (UserID,UserBir,UserSex) SELECT UserID,BirthDay,UserSex FROM dbo.ZL_UserBase where UserID=@Userid;
SET IDENTITY_INSERT [ZL_Sns_UserMoreinfo] OFF";
                SqlParameter[] sp = { new SqlParameter("Userid", id) };
                SqlHelper.ExecuteNonQuery(CommandType.Text, SqlStr, sp);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 根据ID查询用户详细表
        /// <summary>
        /// 根据ID查询用户详细表
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static UserMoreinfo GetMoreinfoByUserid(int UserID)
        {
            //try
            //{
                string sql = @"select ZL_Sns_UserMoreinfo.*,ZL_User.UserName,ZL_UserBase.UserFace FROM ZL_User left join
      ZL_UserBase ON ZL_User.UserID = ZL_UserBase.UserID right join ZL_Sns_UserMoreinfo on ZL_User.UserID=ZL_Sns_UserMoreinfo.UserID where ZL_Sns_UserMoreinfo.UserID=@UserID";
                SqlParameter[] parameter = { new SqlParameter("UserID", UserID) };
                UserMoreinfo um = new UserMoreinfo();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    if (dr.Read())
                    {
                        ReadMoreinfo(dr, um);
                    }
                }
                return um;
            //}
            //catch
            //{
            //    throw;
            //}
        }
        #endregion

        #region 读取用户详细表
        /// <summary>
        /// 读取用户详细表
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="um"></param>
        public static void ReadMoreinfo(SqlDataReader dr, UserMoreinfo um)
        {
            um.UserID = Convert.ToInt32(dr["UserID"].ToString());
            if (dr["UserSex"] is DBNull)
            {
                um.UserSex = true;
            }
            else
            {
                 if (dr["UserSex"].ToString().ToLower() == "true")
                {
                    um.UserSex = true;
                }
                else
                {
                    um.UserSex = false ;
                }
            }
            um.UserSex = bool.Parse(dr["UserSex"].ToString());
            um.UserBachelor = dr["UserBachelor"].ToString();
            um.UserStature = dr["UserStature"].ToString();
            um.UserAvoir = dr["UserAvoir"].ToString();
            um.UserBir = dr["UserBir"].ToString();
            um.UserMarry = dr["UserMarry"].ToString();
            um.UserMonthIn = dr["UserMonthIn"].ToString();
            um.UserHome = dr["UserHome"].ToString();
            um.UserChild = dr["UserChild"].ToString();            
            if (dr["UserConsortSex"] is DBNull)
            {
                um.UserConsortSex = true;
            }
            else
            {
                if (dr["UserConsortSex"].ToString().ToLower() == "true")
                {
                    um.UserConsortSex = true;
                }
                else
                {
                    um.UserConsortSex = false;
                }
            }
            um.UserConsortAge = dr["UserConsortAge"].ToString();
            um.UserConsortWorkArea = dr["UserConsortWorkArea"].ToString();
            um.UserConsortMarry = dr["UserConsortMarry"].ToString();
            um.UserConsortStature = dr["UserConsortStature"].ToString();
            um.UserConsortAvoir = dr["UserConsortAvoir"].ToString();
            um.UserConsortMonthIN = dr["UserConsortMonthIN"].ToString();
            um.UserConsortBachelor = dr["UserConsortBachelor"].ToString();
            um.UserConsortHome = dr["UserConsortHome"].ToString();
            um.UserConsortOther = dr["UserConsortOther"].ToString();
            um.UserSomato = dr["UserSomato"].ToString();
            um.UserBlood = dr["UserBlood"].ToString();
            um.UserProvince = dr["UserProvince"].ToString();
            um.UserCity = dr["UserCity"].ToString();
            um.UserNation = dr["UserNation"].ToString();

            um.UserSchool = dr["UserSchool"].ToString();
            um.UserBrother = dr["UserBrother"].ToString();
            um.UserLanguage = dr["UserLanguage"].ToString();
            um.UserLifePic4 = dr["UserLifePic4"].ToString();
            um.UserLifePic3 = dr["UserLifePic3"].ToString();
            um.UserLifePic2 = dr["UserLifePic2"].ToString();
            um.UserLifePic1 = dr["UserLifePic1"].ToString();

            um.UserLife = dr["UserLife"].ToString();
            um.UserSmoke = dr["UserSmoke"].ToString();
            um.UserDrink = dr["UserDrink"].ToString();
            um.UserWork = dr["UserWork"].ToString();
            um.UserCar = dr["UserCar"].ToString();
            um.UserCom = dr["UserCom"].ToString();
            um.UserNeedchild = dr["UserNeedchild"].ToString();
            um.UserAcctive = dr["UserAcctive"].ToString();
            um.UserAth = dr["UserAth"].ToString();

            um.UserMusic = dr["UserMusic"].ToString();
            um.UserMovie = dr["UserMovie"].ToString();
            um.UserFood = dr["UserFood"].ToString();
            um.UserArea = dr["UserArea"].ToString();
            um.UserHeart = dr["UserHeart"].ToString();
            um.UserLove = dr["UserLove"].ToString();
            um.UserConstellation = dr["UserConstellation"].ToString();
            um.UserName = dr["UserName"].ToString();
            um.Xinyang = dr["Xinyang"].ToString();
            um.Facey = dr["Facey"].ToString();
            um.FaceType = dr["FaceType"].ToString();
            um.HairType = dr["HairType"].ToString();
            um.Lovepetlife = dr["Lovepetlife"].ToString();
            um.JobAnd = dr["JobAnd"].ToString();
            // um.UserPic = dr["UserFace"].ToString();
        }
        #endregion

        #region 修改用户详细表
        /// <summary>
        /// 修改用户详细表
        /// </summary>
        /// <param name="um"></param>
        public static bool UpdateMoreinfo(UserMoreinfo um)
        {
            try
            {
                string sql = @"UPDATE [dbo].[ZL_Sns_UserMoreinfo] SET
	[UserIdcard] = @UserIdcard,
	[UserSex] = @UserSex,
	[UserStature] = @UserStature,
	[UserAvoir] = @UserAvoir,
	[UserBir] = @UserBir,
	[UserMarry] = @UserMarry,
	[UserBachelor] = @UserBachelor,
	[UserMonthIn] = @UserMonthIn,
	[UserHome] = @UserHome,
	[UserChild] = @UserChild,
	[UserConsortSex] = @UserConsortSex,
	[UserConsortAge] = @UserConsortAge,
	[UserConsortWorkArea] = @UserConsortWorkArea,
	[UserConsortMarry] = @UserConsortMarry,
	[UserConsortStature] = @UserConsortStature,
	[UserConsortAvoir] = @UserConsortAvoir,
	[UserConsortMonthIN] = @UserConsortMonthIN,
	[UserConsortBachelor] = @UserConsortBachelor,
	[UserConsortHome] = @UserConsortHome,
	[UserConsortOther] = @UserConsortOther,
	[UserSomato] = @UserSomato,
	[UserBlood] = @UserBlood,
	[UserProvince] = @UserProvince,
	[UserCity] = @UserCity,
	[UserNation] = @UserNation,
	[UserSchool] = @UserSchool,
	[UserBrother] = @UserBrother,
	[UserLanguage] = @UserLanguage,
	[UserLifePic4] = @UserLifePic4,
	[UserLifePic3] = @UserLifePic3,
	[UserLifePic1] = @UserLifePic1,
	[UserLife] = @UserLife,
	[UserSmoke] = @UserSmoke,
	[UserDrink] = @UserDrink,
	[UserWork] = @UserWork,
	[UserCar] = @UserCar,
	[UserCom] = @UserCom,
	[UserNeedchild] = @UserNeedchild,
	[UserAcctive] = @UserAcctive,
	[UserAth] = @UserAth,
	[UserMusic] = @UserMusic,
	[UserMovie] = @UserMovie,
	[UserFood] = @UserFood,
	[UserArea] = @UserArea,
	[UserHeart] = @UserHeart,
	[UserLove] = @UserLove,
	[UserLifePic2] = @UserLifePic2,
    [UserConstellation]=@UserConstellation,
    [Xinyang]=@Xinyang,
    [Facey]=@Facey,
    [FaceType]=@FaceType,
    [HairType]=@HairType,
    [Lovepetlife]=@Lovepetlife,
    [JobAnd]=@JobAnd
    
WHERE
	[UserID] = @UserID";

                SqlParameter[] parameter = new SqlParameter[55];
                parameter[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
                parameter[0].Value = um.UserID;
                parameter[1] = new SqlParameter("@UserIdcard", SqlDbType.NVarChar, 18);
                parameter[1].Value = um.UserIdcard;
                parameter[2] = new SqlParameter("@UserSex", SqlDbType.Bit);
                parameter[2].Value = um.UserSex;
                parameter[3] = new SqlParameter("@UserStature", SqlDbType.NVarChar, 3);
                parameter[3].Value = um.UserStature;
                parameter[4] = new SqlParameter("@UserAvoir", SqlDbType.NVarChar, 3);
                parameter[4].Value = um.UserAvoir;
                parameter[5] = new SqlParameter("@UserBir", SqlDbType.NVarChar, 50);
                parameter[5].Value = um.UserBir;
                parameter[6] = new SqlParameter("@UserMarry", SqlDbType.NVarChar, 50);
                parameter[6].Value = um.UserMarry;
                parameter[7] = new SqlParameter("@UserBachelor", SqlDbType.NVarChar, 50);
                parameter[7].Value = um.UserBachelor;
                parameter[8] = new SqlParameter("@UserMonthIn", SqlDbType.NVarChar, 50);
                parameter[8].Value = um.UserMonthIn;
                parameter[9] = new SqlParameter("@UserHome", SqlDbType.NVarChar, 50);
                parameter[9].Value = um.UserHome;
                parameter[10] = new SqlParameter("@UserChild", SqlDbType.NVarChar, 50);
                parameter[10].Value = um.UserChild;
                parameter[11] = new SqlParameter("@UserConsortSex", SqlDbType.NVarChar, 4);
                parameter[11].Value = um.UserConsortSex;
                parameter[12] = new SqlParameter("@UserConsortAge", SqlDbType.NVarChar, 50);
                parameter[12].Value = um.UserConsortAge;
                parameter[13] = new SqlParameter("@UserConsortWorkArea", SqlDbType.NVarChar, 50);
                parameter[13].Value = um.UserConsortWorkArea;
                parameter[14] = new SqlParameter("@UserConsortMarry", SqlDbType.NVarChar, 50);
                parameter[14].Value = um.UserConsortMarry;
                parameter[15] = new SqlParameter("@UserConsortStature", SqlDbType.NVarChar, 50);
                parameter[15].Value = um.UserConsortStature;
                parameter[16] = new SqlParameter("@UserConsortAvoir", SqlDbType.NVarChar, 50);
                parameter[16].Value = um.UserConsortAvoir;
                parameter[17] = new SqlParameter("@UserConsortMonthIN", SqlDbType.NVarChar, 50);
                parameter[17].Value = um.UserConsortMonthIN;
                parameter[18] = new SqlParameter("@UserConsortBachelor", SqlDbType.NVarChar, 50);
                parameter[18].Value = um.UserConsortBachelor;
                parameter[19] = new SqlParameter("@UserConsortHome", SqlDbType.NVarChar, 50);
                parameter[19].Value = um.UserConsortHome;
                parameter[20] = new SqlParameter("@UserConsortOther", SqlDbType.NVarChar, 200);
                parameter[20].Value = um.UserConsortOther;
                parameter[21] = new SqlParameter("@UserSomato", SqlDbType.NVarChar, 50);
                parameter[21].Value = um.UserSomato;
                parameter[22] = new SqlParameter("@UserBlood", SqlDbType.NVarChar, 50);
                parameter[22].Value = um.UserBlood;
                parameter[23] = new SqlParameter("@UserProvince", SqlDbType.NVarChar, 50);
                parameter[23].Value = um.UserProvince;
                parameter[24] = new SqlParameter("@UserCity", SqlDbType.NVarChar, 50);
                parameter[24].Value = um.UserCity;
                parameter[25] = new SqlParameter("@UserNation", SqlDbType.NVarChar, 50);
                parameter[25].Value = um.UserNation;
                parameter[26] = new SqlParameter("@UserSchool", SqlDbType.NVarChar, 50);
                parameter[26].Value = um.UserSchool;
                parameter[27] = new SqlParameter("@UserBrother", SqlDbType.NVarChar, 50);
                parameter[27].Value = um.UserBrother;
                parameter[28] = new SqlParameter("@UserLanguage", SqlDbType.NVarChar, 50);
                parameter[28].Value = um.UserLanguage;
                parameter[29] = new SqlParameter("@UserLifePic4", SqlDbType.NVarChar, 100);
                parameter[29].Value = um.UserLifePic4;
                parameter[30] = new SqlParameter("@UserLifePic3", SqlDbType.NVarChar, 100);
                parameter[30].Value = um.UserLifePic3;
                parameter[31] = new SqlParameter("@UserLifePic1", SqlDbType.NVarChar, 100);
                parameter[31].Value = um.UserLifePic1;
                parameter[32] = new SqlParameter("@UserLife", SqlDbType.NVarChar, 50);
                parameter[32].Value = um.UserLife;
                parameter[33] = new SqlParameter("@UserSmoke", SqlDbType.NVarChar, 50);
                parameter[33].Value = um.UserSmoke;
                parameter[34] = new SqlParameter("@UserDrink", SqlDbType.NVarChar, 50);
                parameter[34].Value = um.UserDrink;
                parameter[35] = new SqlParameter("@UserWork", SqlDbType.NVarChar, 50);
                parameter[35].Value = um.UserWork;
                parameter[36] = new SqlParameter("@UserCar", SqlDbType.NVarChar, 50);
                parameter[36].Value = um.UserCar;
                parameter[37] = new SqlParameter("@UserCom", SqlDbType.NVarChar, 50);
                parameter[37].Value = um.UserCom;
                parameter[38] = new SqlParameter("@UserNeedchild", SqlDbType.NVarChar, 50);
                parameter[38].Value = um.UserNeedchild;
                parameter[39] = new SqlParameter("@UserAcctive", SqlDbType.NVarChar, 200);
                parameter[39].Value = um.UserAcctive;
                parameter[40] = new SqlParameter("@UserAth", SqlDbType.NVarChar, 200);
                parameter[40].Value = um.UserAth;
                parameter[41] = new SqlParameter("@UserMusic", SqlDbType.NVarChar, 200);
                parameter[41].Value = um.UserMusic;
                parameter[42] = new SqlParameter("@UserMovie", SqlDbType.NVarChar, 200);
                parameter[42].Value = um.UserMovie;
                parameter[43] = new SqlParameter("@UserFood", SqlDbType.NVarChar, 200);
                parameter[43].Value = um.UserFood;
                parameter[44] = new SqlParameter("@UserArea", SqlDbType.NVarChar, 200);
                parameter[44].Value = um.UserArea;
                parameter[45] = new SqlParameter("@UserHeart", SqlDbType.NText);
                parameter[45].Value = um.UserHeart;
                parameter[46] = new SqlParameter("@UserLove", SqlDbType.NText);
                parameter[46].Value = um.UserLove;
                parameter[47] = new SqlParameter("@UserLifePic2", SqlDbType.NVarChar, 100);
                parameter[47].Value = um.UserLifePic2;
                parameter[48] = new SqlParameter("@UserConstellation", SqlDbType.NVarChar, 10);
                parameter[48].Value = um.UserConstellation;
                parameter[49] = new SqlParameter("@Xinyang", SqlDbType.NVarChar, 50);
                parameter[49].Value = um.Xinyang;
                parameter[50] = new SqlParameter("@Facey", SqlDbType.NVarChar, 500);
                parameter[50].Value = um.Facey;
                parameter[51] = new SqlParameter("@FaceType", SqlDbType.NVarChar, 50);
                parameter[51].Value = um.FaceType;
                parameter[52] = new SqlParameter("@HairType", SqlDbType.NVarChar, 50);
                parameter[52].Value = um.HairType;
                parameter[53] = new SqlParameter("@Lovepetlife", SqlDbType.NVarChar, 500);
                parameter[53].Value = um.Lovepetlife;
                parameter[54] = new SqlParameter("@JobAnd", SqlDbType.NVarChar, 255);
                parameter[54].Value = um.JobAnd;

                return SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter) > 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询职业收入
        /// <summary>
        /// 查询职业收入
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="age1"></param>
        /// <param name="age2"></param>
        /// <param name="sex"></param>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <param name="work"></param>
        /// <param name="bachelor"></param>
        /// <param name="monthIn"></param>
        /// <returns></returns>
        public static List<UserMoreinfo> GetWorkMoney(int userid, string age1, string age2, string sex, string pro, string city, string work, string bachelor, string monthIn)
        {
            try
            {
                string sql = null;
                if (userid == 0)
                {
                    sql = @"select ZL_Sns_UserMoreinfo.*,ZL_User.UserName,ZL_UserBase.UserFace FROM ZL_User left join
      ZL_UserBase ON ZL_User.UserID = ZL_UserBase.UserID right join ZL_Sns_UserMoreinfo on ZL_User.UserID=ZL_Sns_UserMoreinfo.UserID
    where ZL_UserBase.UserSex=@sex";
                }
                else
                {
                    sql = @"SELECT ZL_Sns_UserMoreinfo.*, ZL_User.UserName,ZL_UserBase.UserFace FROM ZL_User  left join
      ZL_UserBase ON ZL_User.UserID = ZL_UserBase.UserID right join ZL_Sns_UserMoreinfo on ZL_User.UserID=ZL_Sns_UserMoreinfo.UserID
where ZL_UserBase.UserSex=@sex and ZL_User.UserID not IN (SELECT ZL_UserFriendTable.FriendID FROM ZL_UserFriendTable where UserID=@UserID  )";
                }
                sql += @" and ZL_User.UserID<>@UserID";
                if (age1 != "")
                {
                    sql += @" and datediff(year,ZL_UserBase.BirthDay,getdate())>=@age1 ";
                }
                if (age2 != "")
                {
                    sql += @" and datediff(year,ZL_UserBase.BirthDay,getdate())<=@age2 ";
                }
                if (pro != "")
                {
                    sql += @" and ZL_UserBase.Province=@pro";

                    if (city != "")
                    {
                        sql += @" and ZL_UserBase.County=@city";
                    }
                }
                List<SystemConfig> sclist = new List<SystemConfig>();
                if (work != "")
                {

                    string[] workArry = work.Split(',');
                    sql += @" and ZL_Sns_UserMoreinfo.UserWork in (" + GetStr(workArry, sclist, "@work");
                }

                if (bachelor != "")
                {
                    string[] bachelorArry = bachelor.Split(',');
                    sql += @" and ZL_Sns_UserMoreinfo.UserBachelor in (" + GetStr(bachelorArry, sclist, "@bachelor");
                }

                if (monthIn != "")
                {
                    string[] monthInArry = monthIn.Split(',');
                    sql += @" and ZL_Sns_UserMoreinfo.UserMonthIn in (" + GetStr(monthInArry, sclist, "@monthIn");
                }

                SqlParameter[] sp = new SqlParameter[sclist.Count + 6];
                int i = 0;
                i = Setsp(sp, sclist, i);

                sp[i] = new SqlParameter("UserID", userid);
                sp[i + 1] = new SqlParameter("age1", age1);
                sp[i + 2] = new SqlParameter("age2", age2);
                sp[i + 3] = new SqlParameter("pro", pro);
                if (sex == "女生")
                    sp[i + 4] = new SqlParameter("sex", false);
                else
                    sp[i + 4] = new SqlParameter("sex", true);
                sp[i + 5] = new SqlParameter("city", city);

                List<UserMoreinfo> list = new List<UserMoreinfo>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, sp))
                {
                    while (dr.Read())
                    {
                        UserMoreinfo um = new UserMoreinfo();
                        ReadMoreinfo(dr, um);
                        list.Add(um);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        private static int Setsp(SqlParameter[] sp, List<SystemConfig> sp2, int i)
        {
            foreach (SystemConfig sqlp in sp2)
            {
                sp[i] = new SqlParameter(sqlp.IDKey, sqlp.IDvalue);
                i++;
            }
            return i;
        }

        private static string GetStr(string[] arry, List<SystemConfig> listsc, string str)
        {
            string sql = "";
            for (int i = 0; i < arry.Length; i++)
            {
                sql = sql + str + i.ToString() + ",";
                SystemConfig sc = new SystemConfig();
                sc.IDKey = str + i.ToString();
                sc.IDvalue = arry[i];
                listsc.Add(sc);

            }
            if (sql.EndsWith(","))
                sql = sql.Substring(0, sql.Length - 1) + ")";
            else
                sql = sql + ")";
            return sql;
        }

        #region 查找用户2
        /// <summary>
        /// 查找用户2
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="age1"></param>
        /// <param name="age2"></param>
        /// <param name="sex"></param>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <param name="marry"></param>
        /// <param name="monthin"></param>
        /// <param name="home"></param>
        /// <returns></returns>
        public static List<UserMoreinfo> GetSearch2(int UserID, string age1, string age2, string sex, string pro, string city, string marry)
        {
            return null;
        }
        #endregion


        #region 查找用户3

        public static List<UserMoreinfo> GetSearch3(int UserID, string username)
        {
            try
            {
                string sql = null;
                //System.Web.HttpContext.Current.Response.Write(username);
                //System.Web.HttpContext.Current.Response.End();
                sql = @"SELECT ZL_Sns_UserMoreinfo.*,ZL_User.UserName,ZL_User.userid as ido,ZL_UserBase.UserFace FROM ZL_User inner join  ZL_UserBase ON ZL_User.UserID = ZL_UserBase.UserID right join ZL_Sns_UserMoreinfo on ZL_User.UserID=ZL_Sns_UserMoreinfo.UserID where ZL_User.UserID not IN (SELECT ZL_UserFriendTable.FriendID FROM ZL_UserFriendTable where UserID=@UserID  )";
                sql += @" and ZL_User.UserID<>@UserID";
                if (username != "")
                {
                    sql += @" and ZL_User.UserName like '%" + username + "%'";                   
                }
                
                SqlParameter[] parameter =
                { 
                    new SqlParameter("UserID",UserID)
                };
                List<UserMoreinfo> list = new List<UserMoreinfo>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        UserMoreinfo um = new UserMoreinfo();
                        ReadMoreinfo(dr, um);
                        list.Add(um);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }

        #endregion




        #region 查询星座血型
        /// <summary>
        /// 查询星座血型
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="age1"></param>
        /// <param name="age2"></param>
        /// <param name="sex"></param>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <param name="blood"></param>
        /// <param name="constellation"></param>
        /// <returns></returns>
        public static List<UserMoreinfo> GetConstellation(int userid, string age1, string age2, string sex, string pro, string city, string blood, string constellation)
        {
            try
            {
                string sql = null;
                if (userid == 0)
                {
                    sql = @"select ZL_Sns_UserMoreinfo.*,ZL_User.UserName FROM ZL_User right join
      ZL_Sns_UserMoreinfo ON ZL_User.UserID = ZL_Sns_UserMoreinfo.UserID where ZL_UserBase.UserSex=@sex";
                }
                else
                {
                    sql = @"SELECT ZL_Sns_UserMoreinfo.*, ZL_User.UserName FROM ZL_User  right join
      ZL_Sns_UserMoreinfo ON ZL_User.UserID = ZL_Sns_UserMoreinfo.UserID left join ZL_UserBase on ZL_UserBase.UserID=ZL_Sns_UserMoreinfo.UserID where ZL_UserBase.UserSex=@sex and ZL_User.UserID not IN(SELECT ZL_UserFriendTable.FriendID FROM ZL_UserFriendTable where UserID=@UserID  )";
                }
                sql += @" and ZL_User.UserID<>@UserID";
                if (age1 != "")
                {
                    sql += @" and datediff(year,ZL_UserBase.BirthDay,getdate())>=@age1 ";
                }
                if (age2 != "")
                {
                    sql += @" and datediff(year,ZL_UserBase.BirthDay,getdate())<=@age2 ";
                }
                if (pro != "")
                {
                    sql += @" and ZL_UserBase.Province=@pro";

                    if (city != "")
                    {
                        sql += @" and ZL_UserBase.County=@city";
                    }
                }
                List<SystemConfig> sclist = new List<SystemConfig>();


                if (blood != "")
                {
                    string[] bloodArry = blood.Split(',');
                    sql += @" and ZL_Sns_UserMoreinfo.UserBlood in (" + GetStr(bloodArry, sclist, "@blood");

                }

                //if (constellation != "")
                //{
                //    string[] constellationArry = constellation.Split(',');
                //    sql += @" and ZL_UserBase.UserBlood in (" + GetStr(constellationArry, sclist, "@constellation");
                //}
                SqlParameter[] sp = new SqlParameter[sclist.Count + 6];
                int i = 0;
                i = Setsp(sp, sclist, i);

                sp[i] = new SqlParameter("UserID", userid);
                sp[i + 1] = new SqlParameter("age1", age1);
                sp[i + 2] = new SqlParameter("age2", age2);
                sp[i + 3] = new SqlParameter("pro", pro);
                if (sex == "女生")
                    sp[i + 4] = new SqlParameter("sex", false);
                else
                    sp[i + 4] = new SqlParameter("sex", true);
                sp[i + 5] = new SqlParameter("city", city);

                List<UserMoreinfo> list = new List<UserMoreinfo>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, sp))
                {
                    while (dr.Read())
                    {
                        UserMoreinfo um = new UserMoreinfo();
                        ReadMoreinfo(dr, um);
                        list.Add(um);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region 学校搜索
        /// <summary>
        /// 学校搜索
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="sex"></param>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public static List<UserMoreinfo> GetSchool(int userid, string sex, string pro, string city, string school)
        {
            try
            {
                string sql = null;
                if (userid == 0)
                {
                    sql = @"select ZL_Sns_UserMoreinfo.*,ZL_User.UserName FROM ZL_User right join
      ZL_Sns_UserMoreinfo ON ZL_User.UserID = ZL_Sns_UserMoreinfo.UserID where ZL_UserBase.UserSex=@sex";
                }
                else
                {
                    sql = @"SELECT ZL_Sns_UserMoreinfo.*, ZL_User.UserName FROM ZL_User  right join
      ZL_Sns_UserMoreinfo ON ZL_User.UserID = ZL_Sns_UserMoreinfo.UserID left join ZL_UserBase on ZL_UserBase.UserID=ZL_Sns_UserMoreinfo.UserID where ZL_UserBase.UserSex=@sex and ZL_User.UserID not IN(SELECT ZL_UserFriendTable.FriendID FROM ZL_UserFriendTable where UserID=@UserID  )";
                }
                sql += @" and ZL_User.UserID<>@UserID";
                if (pro != "")
                {
                    sql += @" and ZL_UserBase.Province=@pro";

                    if (city != "")
                    {
                        sql += @" and ZL_UserBase.County=@city";
                    }
                }
                if (school != "")
                {
                    sql += @" and ZL_Sns_UserMoreinfo.UserSchool LIKE '%'+@school+'%'";
                }
                bool bsex;
                if (sex == "女生")
                    bsex = false;
                else
                    bsex = true;
                SqlParameter[] parameter =
                { 
                    new SqlParameter("UserID",userid),
                    new SqlParameter("pro",pro),
                    new SqlParameter("sex", bsex ),
                    new SqlParameter("city",city),
                    new SqlParameter("school",school )
                };
                List<UserMoreinfo> list = new List<UserMoreinfo>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        UserMoreinfo um = new UserMoreinfo();
                        ReadMoreinfo(dr, um);
                        list.Add(um);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// WAP根据学校搜索好友
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="sex"></param>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        public static List<UserMoreinfo> GetSchools(int userid, string sex, string city, string school)
        {
            try
            {
                string sql = null;
                if (userid == 0)
                {
                    //除自己ID以外的用户信息
                    sql = @"select ZL_Sns_UserMoreinfo.*,ZL_User.UserName FROM ZL_User right join
      ZL_Sns_UserMoreinfo ON ZL_User.UserID = ZL_Sns_UserMoreinfo.UserID where ZL_UserBase.UserSex=@sex";
                }
                else
                {
                    //不等于自己好友的信息
                    sql = @"SELECT ZL_Sns_UserMoreinfo.*, ZL_User.UserName FROM ZL_User  right join
      ZL_Sns_UserMoreinfo ON ZL_User.UserID = ZL_Sns_UserMoreinfo.UserID left join ZL_UserBase on ZL_UserBase.UserID=ZL_Sns_UserMoreinfo.UserID where ZL_UserBase.UserSex=@sex and ZL_User.UserID not IN(SELECT ZL_UserFriendTable.FriendID FROM ZL_UserFriendTable where UserID=@UserID  )";
                }
                sql += @" and ZL_User.UserID<>@UserID";
               

                    if (city != "")
                    {
                        sql += @" and ZL_UserBase.County=@city";
                    }
                
                if (school != "")
                {
                    sql += @" and ZL_Sns_UserMoreinfo.UserSchool LIKE '%'+@school+'%'";
                }
                bool bsex;
                if (sex == "女生")
                    bsex = false;
                else
                    bsex = true;
                SqlParameter[] parameter =
                { 
                    new SqlParameter("UserID",userid),
                    
                    new SqlParameter("sex", bsex ),
                    new SqlParameter("city",city),
                    new SqlParameter("school",school )
                };
                List<UserMoreinfo> list = new List<UserMoreinfo>();
                
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        UserMoreinfo um = new UserMoreinfo();
                        ReadMoreinfo(dr, um);
                        list.Add(um);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        #region 高级搜索
        /// <summary>
        /// 在高级搜索中满足条件的用户部份信息
        /// </summary>
        /// <param name="wherex"></param>
        /// <returns></returns>
        public static DataTable GetSeachByCondition(string wherex)
        {
            DataTable dt = new DataTable();
            string sql = @"select zl_userbase.userid  as userid,honeyname,userlove,userface,workprovince,workcity,BirthDay from zl_userbase,zl_sns_usermoreinfo where zl_userbase.userid=zl_sns_usermoreinfo.userid " + wherex;
            dt = SqlHelper.ExecuteTable(CommandType.Text,sql,null);
            return dt;
        }
        //
        #endregion
        #region 搜索出好友显示
        /// <summary>
        /// 搜索出好友显示
        /// </summary>
        /// <param name="wherex"></param>
        /// <returns></returns>
        public static DataTable GetUsersInfo(string wherex)
        {
            DataTable dt = new DataTable();
            string sql = @"select userid,UserFace,honeyName,BirthDay,workProvince,workCity from zl_userbase where 1=1" + wherex ;
            dt = SqlHelper.ExecuteTable(CommandType.Text,sql,null);
            return dt;
        }
        #endregion

    }
}

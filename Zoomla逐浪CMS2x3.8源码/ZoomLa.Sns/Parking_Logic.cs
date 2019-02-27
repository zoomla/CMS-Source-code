using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace ZoomLa.Sns
{
    public class Parking_Logic
    {
        #region 添加车辆信息
        /// <summary>
        /// 添加车辆信息
        /// </summary>
        /// <param name="cl"></param>
        public static void AddCarList(P_CarList cl)
        {
            try
            {
                string sqlstr = @"INSERT INTO [dbo].[ZL_Sns_Carlist] (
	[P_car_name],[P_car_num],[P_car_surplus],[P_car_money],[P_car_old],[P_car_img],[P_car_img_logo],[P_car_content],[P_car_check]
) VALUES (@P_car_name,@P_car_num,@P_car_surplus,@P_car_money,@P_car_old,@P_car_img,@P_car_img_logo,@P_car_content,@P_car_check)";
                SqlParameter[] sp ={
                    new SqlParameter("P_car_name",cl.P_car_name),
                    new SqlParameter("P_car_num",cl.P_car_num),
                    new SqlParameter("P_car_surplus",cl.P_car_surplus),
                    new SqlParameter("P_car_money",cl.P_car_money),
                    new SqlParameter("P_car_old",cl.P_car_old),
                    new SqlParameter("P_car_img",cl.P_car_img),
                    new SqlParameter("P_car_img_logo",cl.P_car_img_logo),
                    new SqlParameter("P_car_content",cl.P_car_content),
                    new SqlParameter("P_car_check",cl.P_car_check)
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlstr, sp);
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region 更新车辆列表信息
        /// <summary>
        /// 更新车辆列表信息
        /// </summary>
        /// <param name="cl"></param>
        public static void UpdateCarList(P_CarList cl)
        {
            try
            {
                string sqlstr = @"UPDATE [dbo].[ZL_Sns_Carlist] SET [P_car_name] = @P_car_name,
[P_car_num] = @P_car_num,[P_car_surplus] = @P_car_surplus,[P_car_money] = @P_car_money,[P_car_old] = @P_car_old,
[P_car_img] = @P_car_img,[P_car_img_logo] = @P_car_img_logo,[P_car_content] = @P_car_content,[P_car_check] = @P_car_check WHERE [Pid] = @Pid";
                SqlParameter[] sp ={
                    new SqlParameter("Pid",cl.Pid),
                    new SqlParameter("P_car_name",cl.P_car_name),
                    new SqlParameter("P_car_num",cl.P_car_num),
                    new SqlParameter("P_car_surplus",cl.P_car_surplus),
                    new SqlParameter("P_car_money",cl.P_car_money),
                    new SqlParameter("P_car_old",cl.P_car_old),
                    new SqlParameter("P_car_img",cl.P_car_img),
                    new SqlParameter("P_car_img_logo",cl.P_car_img_logo),
                    new SqlParameter("P_car_content",cl.P_car_content),
                    new SqlParameter("P_car_check",cl.P_car_check)
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlstr, sp);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 设置车辆是否出售
        /// <summary>
        /// 设置车辆是否出售
        /// </summary>
        /// <param name="id"></param>
        /// <param name="check"></param>
        public static void UpdateCarListCheck(int id,int check)
        {
            try
            {
                string sqlstr = @"UPDATE [dbo].[ZL_Sns_Carlist] SET [P_car_check] = @P_car_check WHERE [Pid] = @Pid";
                SqlParameter[] sp ={
                    new SqlParameter("Pid",id),
                    new SqlParameter("P_car_check",check)
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlstr, sp);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询系统内所有车辆
        /// <summary>
        /// 查询系统内所有车辆
        /// </summary>
        /// <returns></returns>
        public static List<P_CarList> GetCarList()
        {
            try
            {
                string sqlstr = "select * from ZL_Sns_Carlist order BY P_car_money  ";
                List<P_CarList> list = new List<P_CarList>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlstr))
                {
                    while (dr.Read())
                    {
                        P_CarList pl = new P_CarList();
                        ReadP_CarList(dr, pl);
                        list.Add(pl);
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

        #region 查询单个车辆信息
        /// <summary>
        /// 查询单个车辆信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static P_CarList GetCar(int id)
        {
            try
            {
                string sqlstr = "select * from ZL_Sns_Carlist where Pid=@Pid  ";
                SqlParameter[] sp ={new SqlParameter("Pid",id) };
                P_CarList pl = new P_CarList();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlstr,sp))
                {
                    if(dr.Read())
                    {
                        ReadP_CarList(dr, pl);
                    }
                }
                return pl;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取车辆列表
        private static void ReadP_CarList(SqlDataReader dr, P_CarList pl)
        {
            pl.P_car_check = int.Parse(dr["P_car_check"].ToString());
            pl.P_car_content = dr["P_car_content"].ToString();
            pl.P_car_img = dr["P_car_img"].ToString();
            pl.P_car_img_logo = dr["P_car_img_logo"].ToString();
            pl.P_car_money = int.Parse(dr["P_car_money"].ToString());
            pl.P_car_name = dr["P_car_name"].ToString();
            pl.P_car_num = int.Parse(dr["P_car_num"].ToString());
            pl.P_car_old = int.Parse(dr["P_car_old"].ToString());
            pl.P_car_surplus = int.Parse(dr["P_car_surplus"].ToString());
            pl.Pid = int.Parse(dr["Pid"].ToString());
        }
        #endregion

        #region 购买车辆
        /// <summary>
        /// 购买车辆
        /// </summary>
        /// <param name="mcar"></param>
        public static void AddMyCar(ZL_Sns_MyCar mcar)
        {
            try
            {
                string sqlstr = @"INSERT INTO [dbo].[ZL_Sns_MyCar] (
	[P_uid],[Pid],[P_buy_time],[P_action]) VALUES (@P_uid,@Pid,@P_buy_time,@P_action)";
                SqlParameter[] sp ={
                    new SqlParameter("@P_uid",mcar.P_uid),
                    new SqlParameter("Pid",mcar.Pid),
                    new SqlParameter("P_buy_time",DateTime.Now),
                    new SqlParameter("P_action",mcar.P_action)
                };

                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlstr, sp);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 更新用户车辆信息
        /// <summary>
        /// 更新用户车辆信息
        /// </summary>
        /// <param name="mc"></param>
        public static void UpdataMyCar(ZL_Sns_MyCar mc)
        {
            string sqlstr = @"UPDATE [dbo].[ZL_Sns_MyCar] SET 
[P_last_time] = @P_last_time,[P_last_uid] = @P_last_uid,
	[P_last_user] = @P_last_user,[P_action] = @P_action WHERE [Pmid] = @Pmid";

            SqlParameter[] sp ={ 
                new SqlParameter("P_last_time",mc.P_last_time),
                new SqlParameter("P_last_uid",mc.P_last_uid),
                new SqlParameter("P_last_user",mc.P_last_user),
                new SqlParameter("P_action",mc.P_action),
                new SqlParameter("Pmid",mc.Pmid),
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, sqlstr, sp);
        }
        #endregion

        #region 根据用户ID查询用户拥有车辆
        /// <summary>
        /// 根据用户ID查询用户拥有车辆
        /// </summary>
        /// <param name="P_uid"></param>
        /// <returns></returns>
        public static List<ZL_Sns_MyCar> GetMyCarList(int P_uid)
        {
            try
            {
                string sqlstr = @"select ZL_Sns_MyCar.*,ZL_Sns_Carlist.P_car_img,ZL_Sns_Carlist.P_car_img_logo,
ZL_Sns_Carlist.P_car_name,ZL_User.UserName from ZL_Sns_MyCar JOIN dbo.ZL_Sns_Carlist ON 
dbo.ZL_Sns_MyCar.Pid=dbo.ZL_Sns_Carlist.Pid LEFT JOIN ZL_User ON dbo.ZL_Sns_MyCar.P_last_uid=ZL_User.UserID where ZL_Sns_MyCar.P_uid=@P_uid";

                SqlParameter[] sp ={ new SqlParameter("P_uid", P_uid) };
                List<ZL_Sns_MyCar> list = new List<ZL_Sns_MyCar>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlstr, sp))
                {
                    while (dr.Read())
                    {
                        ZL_Sns_MyCar mcar = new ZL_Sns_MyCar();
                        mcar.UserName = dr["UserName"].ToString();
                        mcar.CarImage = dr["P_car_img"].ToString();
                        mcar.CarImageLog = dr["P_car_img_logo"].ToString();
                        mcar.CarName = dr["P_car_name"].ToString();
                        ReadMyCar(dr, mcar);
                        list.Add(mcar);
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

        #region 根据编号ID查询用户拥有车辆信息
        /// <summary>
        /// 根据编号ID查询用户拥有车辆信息
        /// </summary>
        /// <param name="pmid"></param>
        /// <returns></returns>
        public static ZL_Sns_MyCar GetMyCar(int pmid)
        {
            try
            {
                string sqlstr = "select * from ZL_Sns_MyCar where Pmid=@Pmid";
                SqlParameter[] sp ={ new SqlParameter("Pmid", pmid) };
                ZL_Sns_MyCar mcar = new ZL_Sns_MyCar();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadMyCar(dr, mcar);
                    }
                }
                return mcar;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 根据用户ID，车辆ID查询用户单个车辆的信息
        /// <summary>
        /// 根据用户ID，车辆ID查询用户单个车辆的信息
       /// </summary>
       /// <param name="P_uid">用户ID</param>
       /// <param name="Pid">车辆ID</param>
       /// <returns></returns>
        public static ZL_Sns_MyCar GetMyCar(int P_uid,int Pid)
        {
            try
            {
                string sqlstr = "select * from ZL_Sns_MyCar where P_uid=@P_uid and Pid=@Pid";
                SqlParameter[] sp ={ new SqlParameter("P_uid", P_uid),new SqlParameter("Pid",Pid) };
                ZL_Sns_MyCar mcar = new ZL_Sns_MyCar();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadMyCar(dr, mcar);
                    }
                }
                return mcar;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 检查用户拥有的车辆
        /// <summary>
        /// 检查用户拥有的车辆
        /// </summary>
        /// <param name="id">车辆ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns>返回True该用户未拥有这种车，返回false该用户已经拥有这种车</returns>
        public static bool CheckCar(int id,int userid)
        {
            try
            {
                bool ccar = false ;
                string sqlstr = "select count(*) as carcount from ZL_Sns_MyCar where P_uid=@P_uid and Pid=@Pid";
                SqlParameter[] sp ={ new SqlParameter("P_uid", id), new SqlParameter("Pid",userid) };
                List<ZL_Sns_MyCar> list = new List<ZL_Sns_MyCar>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlstr, sp))
                {
                    while (dr.Read())
                    {
                        if (int.Parse(dr["carcount"].ToString()) > 0)
                            ccar = false;
                        else
                            ccar = true;
                    }
                }
                return ccar;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取用户车辆
        private static void ReadMyCar(SqlDataReader dr, ZL_Sns_MyCar mcar)
        {
            mcar.P_uid = int.Parse(dr["P_uid"].ToString());
            mcar.Pid = int.Parse(dr["Pid"].ToString());
            mcar.Pmid = int.Parse(dr["Pmid"].ToString());
            mcar.P_action = int.Parse(dr["P_action"].ToString());
            mcar.P_buy_time = DateTime.Parse(dr["P_buy_time"].ToString());
            mcar.P_last_time =dr["P_last_time"] is DBNull ? new DateTime (): DateTime.Parse(dr["P_last_time"].ToString());
            mcar.P_last_uid = dr["P_last_uid"] is DBNull ? 0 : int.Parse(dr["P_last_uid"].ToString());
            mcar.P_last_user = dr["P_last_user"].ToString();
        }
        #endregion

        #region 添加用户车辆日志
        /// <summary>
        /// 添加用户车辆日志
        /// </summary>
        /// <param name="zscarlog"></param>
        public static void AddCarLog(ZL_Sns_CarLog zscarlog)
        {
            try
            {
                string str = @"INSERT INTO [dbo].[ZL_Sns_CarLog] ([P_uid],[P_type],[P_content],[P_introtime]) VALUES (@P_uid,@P_type,@P_content,@P_introtime)";
                SqlParameter[] sp ={ 
                    new SqlParameter("P_uid",zscarlog.P_uid),
                    new SqlParameter("P_type",zscarlog.P_type),
                    new SqlParameter("P_content",zscarlog.P_content),
                    new SqlParameter("P_introtime",DateTime.Now)
                };

                SqlHelper.ExecuteNonQuery(CommandType.Text, str, sp);
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region 根据用户ID查询车辆日志
        /// <summary>
        /// 根据用户ID查询车辆日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ZL_Sns_CarLog> GetUserIDCarLog(int id)
        {
            try
            {
                string sqlstr = @"select * from ZL_Sns_CarLog where P_uid=@uid order by P_introtime DESC";
                SqlParameter[] sp ={ new SqlParameter("uid",id) };
                List<ZL_Sns_CarLog> list = new List<ZL_Sns_CarLog>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlstr, sp))
                {
                    while (dr.Read())
                    {
                        ZL_Sns_CarLog cl=new ZL_Sns_CarLog ();
                        ReadCarLog(dr, cl);
                        list.Add(cl);
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

        #region 读取车辆日志
        /// <summary>
        /// 读取车辆日志
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="carlog"></param>
        private static void ReadCarLog(SqlDataReader dr, ZL_Sns_CarLog carlog)
        {
            carlog.P_uid = int.Parse(dr["P_uid"].ToString());
            carlog.Plid = int.Parse(dr["Plid"].ToString());
            carlog.P_type = int.Parse(dr["P_type"].ToString());
            carlog.P_introtime = DateTime.Parse(dr["P_introtime"].ToString());
            carlog.P_content = dr["P_content"].ToString();
        }
        #endregion

        #region 初始化用户车位信息
        /// <summary>
        /// 初始化用户车位信息
        /// </summary>
        /// <param name="id"></param>
        public static void AddMyPose(int id)
        {
            try
            {
                string sqlstr = @"INSERT INTO [dbo].[ZL_Sns_MyPose] ([P_uid]) VALUES (@P_uid)";
                SqlParameter[] sp ={ 
                    new SqlParameter("P_uid", id)
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlstr, sp);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 检查用户车位
        /// <summary>
        /// 检查用户车位
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>返回true 该用户已经拥有车位，返回false 该用户没有车位</returns>
        public static bool CheckUserPose(int userid)
        {
            try
            {
                string sqlstr = "select *  from ZL_Sns_MyPose where P_uid=@P_uid";
                SqlParameter[] sp ={ new SqlParameter("P_uid", userid) };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlstr, sp))
                {
                    if (dr.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 更新用户车位信息
        /// <summary>
        /// 更新用户车位信息
        /// </summary>
        /// <param name="mp"></param>
        public static void UpdateMyPose(ZL_Sns_MyPose mp)
        {
            try
            {
                string sqlstr = @"UPDATE [dbo].[ZL_Sns_MyPose] SET
	                [P_pose_uid_1] = @P_pose_uid_1,
	                [P_pose_user_1] = @P_pose_user_1,
	                [P_pose_Pmid_1] = @P_pose_Pmid_1,
	                [P_pose_Pid_1] = @P_pose_Pid_1,
	                [P_pose_uid_2] = @P_pose_uid_2,
	                [P_pose_user_2] = @P_pose_user_2,
	                [P_pose_Pmid_2] = @P_pose_Pmid_2,
	                [P_pose_Pid_2] = @P_pose_Pid_2,
	                [P_pose_uid_3] = @P_pose_uid_3,
	                [P_pose_user_3] = @P_pose_user_3,
	                [P_pose_Pmid_3] = @P_pose_Pmid_3,
	                [P_pose_Pid_3] = @P_pose_Pid_3,
	                [P_pose_uid_4] = @P_pose_uid_4,
	                [P_pose_user_4] = @P_pose_user_4,
	                [P_pose_Pmid_4] = @P_pose_Pmid_4,
	                [P_pose_Pid_4] = @P_pose_Pid_4
                    WHERE
	                    [P_uid] = @P_uid";
                    SqlParameter[] sp ={
                    new SqlParameter("P_uid",mp.P_uid),
                    new SqlParameter("P_pose_uid_1",mp.P_pose_uid_1),
                    new SqlParameter("P_pose_user_1",mp.P_pose_user_1),
                    new SqlParameter("P_pose_Pmid_1",mp.P_pose_Pmid_1),
                    new SqlParameter("P_pose_Pid_1",mp.P_pose_Pid_1),

                    new SqlParameter("P_pose_uid_2",mp.P_pose_uid_2),
                    new SqlParameter("P_pose_user_2",mp.P_pose_user_2),
                    new SqlParameter("P_pose_Pmid_2",mp.P_pose_Pmid_2),
                    new SqlParameter("P_pose_Pid_2",mp.P_pose_Pid_2),

                    new SqlParameter("P_pose_uid_3",mp.P_pose_uid_3),
                    new SqlParameter("P_pose_user_3",mp.P_pose_user_3),
                    new SqlParameter("P_pose_Pmid_3",mp.P_pose_Pmid_3),
                    new SqlParameter("P_pose_Pid_3",mp.P_pose_Pid_3),

                    new SqlParameter("P_pose_uid_4",mp.P_pose_uid_4),
                    new SqlParameter("P_pose_user_4",mp.P_pose_user_4),
                    new SqlParameter("P_pose_Pmid_4",mp.P_pose_Pmid_4),
                    new SqlParameter("P_pose_Pid_4",mp.P_pose_Pid_4)
                    };
                    SqlHelper.ExecuteNonQuery(CommandType.Text, sqlstr, sp);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 根据用户ID查询用户车位
        /// <summary>
        /// 根据用户ID查询用户车位
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static ZL_Sns_MyPose GetMyPose(int userid)
        {
            try
            {
                string sqlstr = "select * from ZL_Sns_MyPose where P_uid=@P_uid";
                SqlParameter[] sp ={ new SqlParameter("P_uid", userid) };
                ZL_Sns_MyPose mp = new ZL_Sns_MyPose();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadMyPose(dr, mp);
                    }
                }
                return mp;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取用户的车位
        /// <summary>
        /// 读取用户的车位
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="mp"></param>
        private static void ReadMyPose(SqlDataReader dr, ZL_Sns_MyPose mp)
        {
            mp.P_uid = int.Parse(dr["P_uid"].ToString());
            mp.P_pose_Pid_1 = int.Parse(dr["P_pose_Pid_1"].ToString());
            mp.P_pose_Pmid_1 = int.Parse(dr["P_pose_Pmid_1"].ToString());
            mp.P_pose_uid_1 = int.Parse(dr["P_pose_uid_1"].ToString());
            mp.P_pose_user_1 = dr["P_pose_user_1"].ToString();
            mp.P_pose_Pid_2 = int.Parse(dr["P_pose_Pid_2"].ToString());
            mp.P_pose_Pmid_2 = int.Parse(dr["P_pose_Pmid_2"].ToString());
            mp.P_pose_uid_2 = int.Parse(dr["P_pose_uid_2"].ToString());
            mp.P_pose_user_2 = dr["P_pose_user_2"].ToString();
            mp.P_pose_Pid_3 = int.Parse(dr["P_pose_Pid_3"].ToString());
            mp.P_pose_Pmid_3 = int.Parse(dr["P_pose_Pmid_3"].ToString());
            mp.P_pose_uid_3 = int.Parse(dr["P_pose_uid_3"].ToString());
            mp.P_pose_user_3 = dr["P_pose_user_3"].ToString();
            mp.P_pose_Pid_4 = int.Parse(dr["P_pose_Pid_4"].ToString());
            mp.P_pose_Pmid_4 = int.Parse(dr["P_pose_Pmid_4"].ToString());
            mp.P_pose_uid_4 = int.Parse(dr["P_pose_uid_4"].ToString());
            mp.P_pose_user_4 = dr["P_pose_user_4"].ToString();
        }
        #endregion

        #region 添加停放信息
        /// <summary>
        /// 添加停放信息
        /// </summary>
        /// <param name="zsr"></param>
        public static int AddReport(ZL_Sns_Report zsr)
        {
            string sqlstr = @"INSERT INTO [dbo].[ZL_Sns_Report] ([Pmid],[Pid],[Puid],[R_to_uid],[R_to_time],[R_type]
) VALUES (@Pmid,@Pid,@Puid,@R_to_uid,@R_to_time,@R_type)";
            SqlParameter[] sp ={
                new SqlParameter("Pmid",zsr.Pmid),
                new SqlParameter("Pid",zsr.Pid),
                new SqlParameter("Puid",zsr.Puid),
                new SqlParameter("R_to_uid",zsr.R_to_uid),
                new SqlParameter("R_to_time",DateTime.Now ),
                new SqlParameter("R_type",1 )
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, sqlstr, sp);
            return SqlHelper.GetMaxId("ZL_Sns_Report", "PRid");
        }
        #endregion

        #region 更新车辆停放信息
        /// <summary>
        /// 更新车辆停放信息
        /// </summary>
        /// <param name="zsr"></param>
        public static void UpdateReport(int id)
        {
            string sqlstr = @"UPDATE [dbo].[ZL_Sns_Report] SET [R_type] = @R_type WHERE [PRid] = @PRid";
            SqlParameter[] sp ={ 
                new SqlParameter("R_type", 2),
                new SqlParameter("PRid", id) 
            };

            SqlHelper.ExecuteNonQuery(CommandType.Text, sqlstr, sp);

        }
        #endregion

        #region 根据ID查询车辆停放信息
        /// <summary>
        /// 根据ID查询车辆停放信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ZL_Sns_Report GetCarReport(int id)
        {
            try
            {
                string sqlstr = "select * from ZL_Sns_Report where PRid=@PRid";
                SqlParameter[] sp ={ new SqlParameter("PRid", id) };
                ZL_Sns_Report sr = new ZL_Sns_Report();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlstr, sp))
                {
                    if (dr.Read())
                    {
                        ReadReport(dr, sr);
                    }
                }
                return sr;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region 读取车辆停放信息
        /// <summary>
        /// 读取车辆停放信息
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sr"></param>
        private static void ReadReport(SqlDataReader dr, ZL_Sns_Report sr)
        {
            sr.Pid = int.Parse(dr["Pid"].ToString());
            sr.Pmid = int.Parse(dr["Pmid"].ToString());
            sr.PRid = int.Parse(dr["PRid"].ToString());
            sr.Puid = int.Parse(dr["Puid"].ToString());
            sr.R_to_time = dr["R_to_time"] is DBNull ? new DateTime() : DateTime.Parse(dr["R_to_time"].ToString());
            sr.R_to_uid = int.Parse(dr["R_to_uid"].ToString());
            sr.R_type = int.Parse(dr["R_type"].ToString());
        }
        #endregion

        #region 修改抢车位规则
        /// <summary>
        /// 修改抢车位规则
        /// </summary>
        /// <param name="cc"></param>
        public static void UpdateCarConfig(string cvalue,int id)
        {
            try
            {
                string sqlstr = @"UPDATE [dbo].[ZL_Sns_CarConfig] SET [Cvalue] = @Cvalue WHERE [ID] = @ID";
                SqlParameter[] sp ={
                    new SqlParameter("ID",id),
                    new SqlParameter("Cvalue",cvalue)
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlstr, sp);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 查询抢车位规则
        /// <summary>
        /// 查询抢车位规则
        /// </summary>
        /// <returns></returns>
        public static List<ZL_Sns_CarConfig> GetCarConfig()
        {
            try
            {
                string sqlstr = @"select * from ZL_Sns_CarConfig order by ID ";
                List<ZL_Sns_CarConfig> list = new List<ZL_Sns_CarConfig>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlstr))
                {
                    while (dr.Read())
                    {
                        ZL_Sns_CarConfig cc = new ZL_Sns_CarConfig();
                        ReadCarConfig(dr, cc);
                        list.Add(cc);
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

        #region 读取抢车位游戏规则
        /// <summary>
        /// 读取抢车位游戏规则
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="cc"></param>
        private static void ReadCarConfig(SqlDataReader dr, ZL_Sns_CarConfig cc)
        {
            cc.ID = int.Parse(dr["ID"].ToString());
            cc.CKey = dr["CKey"].ToString();
            cc.Ctext = dr["Ctext"].ToString();
            cc.Cvalue = dr["Cvalue"].ToString();
        }
        #endregion

        #region

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using BDUModel;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class SystemManageLogic
    {
        #region 创建布兜世界的模块
        /// <summary>
        /// 创建布兜世界的模块
        /// </summary>
        /// <param name="bdModule"></param>
        /// <returns></returns>
        public static Guid  CreatSystemBDModule(SystemBDModule bdModule)
        {
            try
            {
                bdModule.ID = Guid.NewGuid();
                string sqlCreatSystemBDModule = @"INSERT INTO [SystemBDModule]  ([ID],[ModuleName]
            ,[ModuleURL] ,[ModulePic],[ModuleDepict],[State])
             VALUES (@ID,@ModuleName,@ModuleURL,@ModulePic,@ModuleDepict,@State)";
                SqlParameter[] parameter ={ new SqlParameter("ID", bdModule.ID),
                    new SqlParameter("ModuleName", bdModule.ModuleName),
                    new SqlParameter("ModuleURL", bdModule.ModuleURL),
                    new SqlParameter("ModulePic", bdModule.ModulePic),
                    new SqlParameter("ModuleDepict", bdModule.ModuleDepict),
                    new SqlParameter("State", bdModule.State)
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlCreatSystemBDModule, parameter);
                return bdModule.ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 获取所有模块
        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public static List<SystemBDModule> GetSystemBDModule()
        {
            try
            {
                List<SystemBDModule> list=new List<SystemBDModule> ();
                string sqlGetSystemBDModule = @"select * from SystemBDModule";
                SqlParameter[] parameter ={ new SqlParameter("ID", "") };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetSystemBDModule, parameter))
                {
                    while (dr.Read())
                    {
                        SystemBDModule obj=new SystemBDModule ();
                        ReadSystemBDModule(dr, obj);
                        list.Add(obj);
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

        #region 用户添加组建
        /// <summary>
        /// 用户添加组建
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="mondelID"></param>
        public static Guid  AddMondel(Guid userID, Guid mondelID)
        {
            try
            {
                Guid ID = Guid.NewGuid();
                string sqlAddMondel = @"INSERT INTO [User_R_Module]  ([ID],[UserID]
            ,[VisibleDegree] ,[ModuleID],[Taix])
             VALUES (@ID,@UserID,@VisibleDegree,@ModuleID,@Taix)";
                SqlParameter[] parameter ={ new SqlParameter("ID", ID),
                    new SqlParameter("UserID", userID),
                    new SqlParameter("VisibleDegree", int.Parse("0")),
                    new SqlParameter("ModuleID", mondelID),
                    new SqlParameter("Taix",GetUserMondel(userID))
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlAddMondel, parameter);
                return ID;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 获取模块当前排序号
        /// <summary>
        /// 获取模块当前排序号
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="mondelID"></param>
        /// <returns></returns>
        private static int GetUserMondel(Guid userID)
        {
            try
            {
              
                string sqlGetUserMondel = @"SELECT (CASE WHEN FTaix IS NULL THEN 1 ELSE FTaix+1 END) AS FTaix FROM (
                                        SELECT MAX(Taix) AS FTaix FROM User_R_Module  where UserID=@UserID) AS aa ";
                SqlParameter[] parameter ={ 
                    new SqlParameter("UserID", userID)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetUserMondel, parameter))
                {
                    if (dr.Read())
                    {
                        return int.Parse(dr["FTaix"].ToString());
                    }

                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 通过用户编号和模块编号判断用户是否已经添加了模块
        /// <summary>
        /// 通过用户编号和模块编号判断用户是否已经添加了模块
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="mondelID"></param>
        /// <returns></returns>
        public static bool ExitUserHasMondel(Guid userID, Guid mondelID)
        {
            try
            {
                string sqlExitUserHasMondel = @"select * from User_R_Module where UserID=@UserID and ModuleID=@ModuleID";
                SqlParameter[] parameter ={ 
                    new SqlParameter("UserID", userID),
                    new SqlParameter("ModuleID", mondelID)
                };
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlExitUserHasMondel, parameter))
                {
                    if (dr.Read())
                        return true;
                    else return false;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 通过模块编号获取模块
        /// <summary>
        /// 通过模块编号获取模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SystemBDModule GetSystemBDModuleByID(Guid id)
        {
            try
            {
                SystemBDModule obj=new SystemBDModule ();
                string sqlGetSystemBDModuleByID = @"select * from SystemBDModule where ID=@ID";
                SqlParameter[] parameter ={ 
                    new SqlParameter("ID", id)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetSystemBDModuleByID, parameter))
                {
                    if (dr.Read())
                    {
                        ReadSystemBDModule(dr, obj);
                    }
                }
                return obj;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region  通过用户获取现有模块
        /// <summary>
        /// 通过用户获取现有模块
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<User_R_Module> GetUserModuleByUserID(Guid userID)
        {
            try
            {
                 List<User_R_Module> list=new List<User_R_Module> ();
                string sqlGetUserModuleByUserID = @"SELECT User_R_Module.*, SystemBDModule.ModuleName, 
                                                  SystemBDModule.ModuleURL, SystemBDModule.ModulePic, 
                                                  SystemBDModule.ModuleDepict, SystemBDModule.State, 
                                                  SystemBDModule.CanDelete, SystemBDModule.CanConceal, 
                                                  SystemBDModule.ModuleViewPic
                                            FROM SystemBDModule INNER JOIN
                                                  User_R_Module ON SystemBDModule.ID = User_R_Module.ModuleID where User_R_Module.UserID=@UserID and SystemBDModule.State=1 order by User_R_Module.Taix";
                SqlParameter[] parameter ={ 
                    new SqlParameter("UserID", userID)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetUserModuleByUserID, parameter))
                {
                    while (dr.Read())
                    {
                        User_R_Module obj=new User_R_Module ();
                        ReadUserBDModule(dr, obj);
                        list.Add(obj);
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

        #region 通过用户模块编号获取模块
        /// <summary>
        /// 通过用户模块编号获取模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User_R_Module GetUserRModuleByID(Guid id)
        {
            try
            {
                User_R_Module obj = new User_R_Module();
                string sqlGetUserRModuleByID = @"SELECT User_R_Module.*, SystemBDModule.ModuleName, 
                                                  SystemBDModule.ModuleURL, SystemBDModule.ModulePic, 
                                                  SystemBDModule.ModuleDepict, SystemBDModule.State, 
                                                  SystemBDModule.CanDelete, SystemBDModule.CanConceal, 
                                                  SystemBDModule.ModuleViewPic
                                            FROM SystemBDModule INNER JOIN
                                                  User_R_Module ON SystemBDModule.ID = User_R_Module.ModuleID where User_R_Module.ID=@ID  ";
                SqlParameter[] parameter ={ 
                    new SqlParameter("ID", id)};
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetUserRModuleByID, parameter))
                {
                    while (dr.Read())
                    {
                        ReadUserBDModule(dr, obj);
                      
                    }
                }
                return obj;


            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 删除用户选择的模块
        /// <summary>
        /// 删除用户选择的模块
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteUserMondelByID(Guid id)
        {
            try
            {
                string sqlDeleteUserMondelByID = @"delete User_R_Module where ID=@ID";
                  SqlParameter[] parameter ={ 
                    new SqlParameter("ID", id)};
                  SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDeleteUserMondelByID, parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 读取模块
        /// <summary>
        /// 读取模块
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sbdm"></param>
        public static void ReadSystemBDModule(SqlDataReader dr, SystemBDModule sbdm)
        {
            sbdm.ID = (Guid)dr["ID"];
            sbdm.ModuleDepict = dr["ModuleDepict"].ToString();
            sbdm.ModuleName = dr["ModuleName"].ToString();
            sbdm.ModulePic = dr["ModulePic"].ToString();
            sbdm.ModuleURL = dr["ModuleURL"].ToString();
            sbdm.State = int.Parse(dr["State"].ToString());
            sbdm.CanConceal = bool.Parse(dr["CanConceal"].ToString());
            sbdm.CanDelete = bool.Parse(dr["CanDelete"].ToString());
            sbdm.ModuleViewPic = dr["ModuleViewPic"].ToString();
        }
        #endregion

        #region 读取我的模块
        /// <summary>
        /// 读取模块
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sbdm"></param>
        public static void ReadUserBDModule(SqlDataReader dr, User_R_Module sbdm)
        {
            sbdm.ID = (Guid)dr["ID"];
            sbdm.ModuleDepict = dr["ModuleDepict"].ToString();
            sbdm.ModuleName = dr["ModuleName"].ToString();
            sbdm.ModulePic = dr["ModulePic"].ToString();
            sbdm.ModuleURL = dr["ModuleURL"].ToString();
            sbdm.State = int.Parse(dr["State"].ToString());
            sbdm.CanConceal = bool.Parse(dr["CanConceal"].ToString());
            sbdm.CanDelete = bool.Parse(dr["CanDelete"].ToString());
            sbdm.ModuleViewPic = dr["ModuleViewPic"].ToString();
            sbdm.Taix = int.Parse(dr["Taix"].ToString());
            sbdm.VisibleDegree = int.Parse(dr["VisibleDegree"].ToString());
        }
        #endregion



    }
}

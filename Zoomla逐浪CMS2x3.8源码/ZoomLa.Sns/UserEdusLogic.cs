using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BDUModel;
using ZoomLa.SQLDAL;

namespace BDULogic
{
    public class UserEdusLogic
    {
        #region 添加信息
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="ue"></param>
        /// <returns></returns>
        public static Guid InsertUseredu(UserEducationWork ue)
        {
            try
            {
                ue.ID = Guid.NewGuid();
                string sql = @"Insert into UserEducationWork(ID,CompSchoolName,BranchYard,StartTime,EndTime,UserID,IsPublic,Flage) values(@ID,@CompSchoolName,@BranchYard,@StartTime,@EndTime,@UserID,@IsPublic,@Flage)";
                SqlParameter[] parameter ={
                    new SqlParameter("@ID",ue.ID),
                    new SqlParameter("@CompSchoolName",ue.CompSchoolName),
                    new SqlParameter("@BranchYard",ue.BranchYard),
                    new SqlParameter("@StartTime",ue.StartTime),
                    new SqlParameter("@EndTime",ue.EndTime),
                    new SqlParameter("@UserID",ue.UserID),
                    new SqlParameter("@IsPublic",ue.IsPublic),
                    new SqlParameter("@Flage",ue.Flage)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);
                return ue.ID;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 修改信息
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static void Updateedu(UserEducationWork ue)
        {
            try
            {
                string sql = @"Update UserEducationWork set CompSchoolName=@CompSchoolName,BranchYard=@BranchYard,StartTime=@StartTime,EndTime=@EndTime,UserID=@UserID,IsPublic=@IsPublic,Flage=@Flage where ID=@ID";
                SqlParameter[] parameter ={
                    new SqlParameter("@ID",ue.ID),
                    new SqlParameter("@CompSchoolName",ue.CompSchoolName),
                    new SqlParameter("@BranchYard",ue.BranchYard),
                    new SqlParameter("@StartTime",ue.StartTime),
                    new SqlParameter("@EndTime",ue.EndTime),
                    new SqlParameter("@UserID",ue.UserID),
                    new SqlParameter("@IsPublic",false),
                    new SqlParameter("@Flage",ue.Flage)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static void Deledu(Guid ID)
        {
            try
            {
                string sql = @"Delete from UserEducationWork  where ID=@ID";
                SqlParameter[] parameter ={
                    new SqlParameter("@ID",ID)
            };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameter);

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region 根据用户ID查询信息
        /// <summary>
        /// 根据用户ID查询信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Flage"></param>
        /// <returns></returns>
        public static List<UserEducationWork> GetUsereduByUserID(Guid UserID, int Flage)
        {
            try
            {
                string sql = @"select * from UserEducationWork where UserID=@UserID and Flage=@Flage";
                SqlParameter[] parameter ={
                    new SqlParameter("@UserID",UserID),
                    new SqlParameter("@Flage",Flage)
            };
                List<UserEducationWork> list = new List<UserEducationWork>();
                using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql, parameter))
                {
                    while (dr.Read())
                    {
                        UserEducationWork uf = new UserEducationWork();
                        Readedu(dr, uf);
                        list.Add(uf);
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

        #region 读取表数据
        /// <summary>
        /// 读取表数据
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="ut"></param>
        public static void Readedu(SqlDataReader dr, UserEducationWork ue)
        {
            ue.ID = (Guid)dr["ID"];
            ue.CompSchoolName = dr["CompSchoolName"].ToString();
            ue.BranchYard = dr["BranchYard"].ToString();
            ue.StartTime = dr["StartTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["StartTime"].ToString());
            ue.EndTime = dr["EndTime"] is DBNull ? new DateTime() : DateTime.Parse(dr["EndTime"].ToString());
            ue.UserID = (Guid)dr["UserID"];
            ue.Flage = (int)dr["Flage"];
            ue.IsPublic = (bool)dr["IsPublic"];
        }
        #endregion
    }
}

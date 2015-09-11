namespace ZoomLa.SQLDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using System.Data.SqlClient;
    using ZoomLa.Common;

    /// <summary>
    /// SD_SpecInfo 的摘要说明
    /// </summary>
    public class SD_SpecInfo : ID_SpecInfo
    {
        public SD_SpecInfo()
        {            
        }

        #region ID_SpecInfo 成员
        /// <summary>
        /// 将专题信息添加到数据库中
        /// </summary>
        /// <param name="info">专题信息实例对象</param>
        void ID_SpecInfo.Add(M_SpecInfo info)
        {
            string strSql = "PR_SpecInfo_Add";
            SqlParameter[] parameter = new SqlParameter[] {                
                new SqlParameter("@SpecialID", SqlDbType.Int),
                new SqlParameter("@InfoID", SqlDbType.Int)
            };            
            parameter[0].Value = info.SpecialID;
            parameter[1].Value = info.InfoID;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, strSql, parameter);
        }
        /// <summary>
        /// 将专题信息从数据库中删除
        /// </summary>
        /// <param name="SpecInfoID"></param>
        void ID_SpecInfo.Del(int SpecInfoID)
        {
            string strSql = "PR_SpecInfo_Del";
            SqlParameter[] parameter = new SqlParameter[] {                
                new SqlParameter("@SpecInfoID", SqlDbType.Int)
            };
            parameter[0].Value = SpecInfoID;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, strSql, parameter);
        }
        /// <summary>
        /// 获取某内容所属的专题ID字符串，可用','拆分成数组
        /// </summary>
        /// <param name="ItemID">内容ID</param>
        /// <returns></returns>
        string ID_SpecInfo.GetSpecial(int ItemID)
        {
            string strSql = "Select SpecialID from ZL_SpecInfo Where InfoID=@InfoID";
            SqlParameter[] parameter = new SqlParameter[] {                
                new SqlParameter("@InfoID", SqlDbType.Int)
            };
            parameter[0].Value = ItemID;
            string SpecialID = "";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, parameter))
            {
                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(SpecialID))
                        SpecialID = reader["SpecialID"].ToString();
                    else
                        SpecialID = SpecialID + "," + reader["SpecialID"].ToString();
                }
            }
            return SpecialID;
        }
        /// <summary>
        /// 读取属于指定专题ID的内容列表
        /// </summary>
        /// <param name="SpecID">专题ID</param>
        /// <returns></returns>
        DataTable ID_SpecInfo.GetSpecContent(int SpecID)
        {
            string strSql = "select a.SpecInfoID,b.* from ZL_SpecInfo a left outer join ZL_CommonModel b on a.InfoID=b.GeneralID where a.SpecialID=@SpecID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SpecID",SqlDbType.Int)
            };
            sp[0].Value = SpecID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        /// <summary>
        /// 将指定专题信息ID的专题信息改变成另一个专题所属，即移动到另一专题
        /// </summary>
        /// <param name="SpecInfoID">专题信息ID</param>
        /// <param name="SpecID">另一专题ID</param>
        void ID_SpecInfo.ChgSpecID(int SpecInfoID, int SpecID)
        {
            string strSql = "Update ZL_SpecInfo Set SpecialID=@SpecID where SpecInfoID=@SpecInfoID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SpecID",SqlDbType.Int),
                new SqlParameter("@SpecInfoID",SqlDbType.Int)
            };
            sp[0].Value = SpecID;
            sp[1].Value = SpecInfoID;
            SqlHelper.ExecuteSql(strSql, sp);
        }
        /// <summary>
        /// 获取指定专题信息ID的专题信息实例对象
        /// </summary>
        /// <param name="SpecInfoID">专题信息ID</param>
        /// <returns></returns>
        M_SpecInfo ID_SpecInfo.GetSpecInfo(int SpecInfoID)
        {
            string strSql = "select * from ZL_SpecInfo where SpecInfoID=@SpecInfoID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@SpecInfoID",SqlDbType.Int)
            };
            sp[0].Value = SpecInfoID;
            using(SqlDataReader rdr=SqlHelper.ExecuteReader(CommandType.Text,strSql,sp))
            {
                if (rdr.Read())
                {
                    return GetInfoFromReader(rdr);
                }
                else
                {
                    return new M_SpecInfo(true);
                }
            }
        }
        #endregion

        private static SqlParameter[] GetParameters(M_SpecInfo info)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@SpecInfoID", SqlDbType.Int),
                new SqlParameter("@SpecialID", SqlDbType.Int),
                new SqlParameter("@InfoID", SqlDbType.Int)
            };
            parameter[0].Value = info.SpecInfoID;
            parameter[1].Value = info.SpecialID;
            parameter[2].Value = info.InfoID;
            return parameter;
        }
        private M_SpecInfo GetInfoFromReader(SqlDataReader rdr)
        {
            M_SpecInfo info = new M_SpecInfo();
            info.SpecInfoID = DataConverter.CLng(rdr["SpecInfoID"].ToString());
            info.SpecialID = DataConverter.CLng(rdr["SpecialID"].ToString());
            info.InfoID = DataConverter.CLng(rdr["InfoID"].ToString());
            return info;
        }


        #region ID_SpecInfo 成员

        /// <summary>
        /// 将指定内容不属于指定专题ID组的专题信息删除
        /// </summary>
        /// <param name="SpecID">专题ID组</param>
        /// <param name="ItemID">内容ID</param>
        void ID_SpecInfo.DelItemSpec(string SpecID, int ItemID)
        {
            string strSql = "Delete from ZL_SpecInfo where SpecialID not in(" + SpecID + ") and InfoID="+ItemID;
            SqlHelper.ExecuteNonQuery(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 检测指定专题ID和内容ID的专题信息是否存在
        /// </summary>
        /// <param name="SpecID">专题ID</param>
        /// <param name="ItemID">内容ID</param>
        /// <returns></returns>
        bool ID_SpecInfo.IsExist(int SpecID, int ItemID)
        {
            string strSql = "Select Count(SpecInfoID) from ZL_SpecInfo Where InfoID=@InfoID and SpecialID=@SpecialID";
            SqlParameter[] parameter = new SqlParameter[] {                
                new SqlParameter("@InfoID", SqlDbType.Int),
                new SqlParameter("@SpecialID", SqlDbType.Int),
            };
            parameter[0].Value = ItemID;
            parameter[1].Value = SpecID;
            return SqlHelper.Exists(CommandType.Text, strSql, parameter);
        }

        #endregion
    }
}
namespace ZoomLa.SQLDAL
{
    using System;
    using System.Data;
    using System.Configuration;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using System.Data.SqlClient;

    /// <summary>
    /// 会员模型sql server数据访问层
    /// </summary>
    public class SD_UserModel : ID_UserModel
    {
        public SD_UserModel()
        {
        }

        #region ID_UserModel 成员
        /// <summary>
        /// 添加会员模型
        /// </summary>
        /// <param name="UserInfo"></param>
        void ID_UserModel.Add(M_UserModel info)
        {
            string strSql = "PR_UserModel_Add";
            SqlParameter[] cmdParams = GetParameters(info);
            SqlHelper.ExecuteProc(strSql, cmdParams);
        }
        /// <summary>
        /// 删除指定会员模型
        /// </summary>
        /// <param name="ModelID"></param>
        void ID_UserModel.Delete(int ModelID)
        {
            string strSql = "PR_UserModel_Del";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@ModelID",SqlDbType.Int)
            };
            cmdParams[0].Value = ModelID;
            SqlHelper.ExecuteProc(strSql, cmdParams);
        }
        /// <summary>
        /// 读取指定会员模型
        /// </summary>
        /// <param name="ModelID"></param>
        /// <returns></returns>
        M_UserModel ID_UserModel.GetInfoByID(int ModelID)
        {
            string strSql = "PR_UserModel_Get";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@ModelID",SqlDbType.Int)
            };
            cmdParams[0].Value = ModelID;
            using(SqlDataReader rdr=SqlHelper.ExecuteReader(CommandType.StoredProcedure,strSql,cmdParams))
            {
                if (rdr.Read())
                {
                    return GetInfoFromReader(rdr);
                }
                else
                {
                    return new M_UserModel(true);
                }
            }
        }
        /// <summary>
        /// 更新会员模型
        /// </summary>
        /// <param name="info"></param>
        void ID_UserModel.Update(M_UserModel info)
        {
            string strSql = "PR_UserModel_Update";
            SqlParameter[] cmdParams = GetParameters(info);
            SqlHelper.ExecuteProc(strSql, cmdParams);
        }
        /// <summary>
        /// 会员模型列表
        /// </summary>
        /// <returns></returns>
        DataTable ID_UserModel.UserModelList()
        {
            string strSql = "PR_UserModel_List";
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, strSql, null);
        }
        /// <summary>
        /// 读取指定会员组启用的会员模型列表
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        DataTable ID_UserModel.UserModelListByGroup(int GroupID)
        {
            string strSql = "PR_UserModel_ListByGroup";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@GroupID",SqlDbType.Int)
            };
            sp[0].Value = GroupID;
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure, strSql, null);
        }

        #endregion
        /// <summary>
        /// 将模型信息的各属性值传递到参数中
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParameters(M_UserModel Info)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@ModelID", SqlDbType.Int, 4),
                new SqlParameter("@ModelName", SqlDbType.NVarChar, 20),
                new SqlParameter("@Description", SqlDbType.NVarChar, 255),
                new SqlParameter("@TableName", SqlDbType.NVarChar, 50)
            };
            parameter[0].Value = Info.ModelID;
            parameter[1].Value = Info.ModelName;
            parameter[2].Value = Info.Description;
            parameter[3].Value = Info.TableName;
            return parameter;
        }
        /// <summary>
        /// 从DataReader中读取模型记录
        /// </summary>
        /// <param name="rdr">DataReader</param>
        /// <returns>M_ModelInfo 模型信息</returns>
        private static M_UserModel GetInfoFromReader(SqlDataReader rdr)
        {
            M_UserModel info = new M_UserModel();
            info.ModelID = DataConverter.CLng(rdr["ModelID"].ToString());
            info.ModelName = rdr["ModelName"].ToString();
            info.Description = rdr["Description"].ToString();
            info.TableName = rdr["TableName"].ToString();
            return info;
        }
    }
}
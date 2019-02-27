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
    /// SD_Group 的摘要说明
    /// </summary>
    public class SD_Group : ID_Group
    {
        public SD_Group()
        {            
        }

        #region ID_Group 成员

        bool ID_Group.Add(M_Group info)
        {
            string strSql = "PR_Group_Add";
            SqlParameter[] cmdParams = GetParameters(info);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        bool ID_Group.Update(M_Group info)
        {
            string strSql = "PR_Group_Update";
            SqlParameter[] cmdParams = GetParameters(info);
            return SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        bool ID_Group.Del(int GroupID)
        {
            string strSql = "PR_Group_Del";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            cmdParams[0].Value = GroupID;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }

        M_Group ID_Group.GetGroupByID(int GroupID)
        {
            string strSql = "select * from ZL_Group where GroupID=@GroupID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@GroupID",SqlDbType.Int)
            };
            sp[0].Value = GroupID;
            using(SqlDataReader sdr=SqlHelper.ExecuteReader(CommandType.Text,strSql,sp))
            {
                if (sdr.Read())
                {
                    return GetInfoFromReader(sdr);
                }
                else
                    return new M_Group(true);
            }
        }

        DataTable ID_Group.GetGeoupList()
        {
            string strSql = "PR_Group_Get";            
            return SqlHelper.ExecuteTable(CommandType.StoredProcedure,strSql, null);
        }

        string ID_Group.GetGroupModel(int GroupID)
        {
            string strSql = "select UserModel from ZL_GroupModel where GroupID=@GroupID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@GroupID",SqlDbType.Int)
            };
            sp[0].Value = GroupID;
            string re = "";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, strSql, sp))
            {
                while (sdr.Read())
                {
                    if (string.IsNullOrEmpty(re))
                    {
                        re = sdr["UserModel"].ToString();
                    }
                    else
                    {
                        re = re + "," + sdr["UserModel"].ToString();
                    }
                }
            }
            return re;
        }

        bool ID_Group.DelGroupModel(int GroupID, string GroupModel)
        {
            string strSql = "Delete from ZL_GroupModel where GroupID=@GroupID and UserModel not in (" + GroupModel + ")";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@GroupID",SqlDbType.Int)
            };
            sp[0].Value = GroupID;
            return SqlHelper.ExecuteSql(strSql, sp);
        }

        bool ID_Group.AddGroupModel(int GroupID, int ModelID)
        {
            string strSql="Insert into ZL_GroupModel values(@GroupID,@UserModel)";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@GroupID",SqlDbType.Int),
                new SqlParameter("@UserModel",SqlDbType.Int)
            };
            sp[0].Value = GroupID;
            sp[1].Value = ModelID;
            return SqlHelper.ExecuteSql(strSql, sp);
        }

        bool ID_Group.IsExistModel(int GroupID, int ModelID)
        {
            string strSql = "select count(*) from ZL_GroupModel where GroupID=@GroupID and UserModel=@ModelID";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@GroupID",SqlDbType.Int),
                new SqlParameter("@ModelID",SqlDbType.Int)
            };
            sp[0].Value = GroupID;
            sp[1].Value = ModelID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp)) > 0;
        }

        bool ID_Group.SetGroupFun(int GroupID, string funname)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        bool ID_Group.DelGroupFun(int GroupID, string funname)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
        /// <summary>
        /// 将模型信息的各属性值传递到参数中
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParameters(M_Group Info)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@GroupID", SqlDbType.Int, 4),
                new SqlParameter("@GroupName", SqlDbType.NVarChar, 50),
                new SqlParameter("@Description", SqlDbType.NVarChar, 255),
                new SqlParameter("@RegSelect", SqlDbType.Bit, 1)
            };
            parameter[0].Value = Info.GroupID;
            parameter[1].Value = Info.GroupName;
            parameter[2].Value = Info.Description;
            parameter[3].Value = Info.RegSelect;
            return parameter;
        }
        /// <summary>
        /// 从DataReader中读取模型记录
        /// </summary>
        /// <param name="rdr">DataReader</param>
        /// <returns>M_ModelInfo 模型信息</returns>
        private static M_Group GetInfoFromReader(SqlDataReader rdr)
        {
            M_Group info = new M_Group();
            info.GroupID = DataConverter.CLng(rdr["GroupID"].ToString());
            info.GroupName = rdr["GroupName"].ToString();
            info.Description = rdr["Description"].ToString();
            info.RegSelect = DataConverter.CBool(rdr["RegSelect"].ToString());
            return info;
        }
    }
}
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
    /// SD_Spec 的摘要说明
    /// </summary>
    public class SD_Spec:ID_Spec
    {
        public SD_Spec()
        {            
        }

        #region ID_Spec 成员

        M_Spec ID_Spec.GetSpec(int SpecID)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@SpecID", SqlDbType.Int, 4) };
            cmdParams[0].Value = SpecID;
            string strSql = "select * from ZL_Special where SpecID=@SpecID";
            using (SqlDataReader drd = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (drd.Read())
                {
                    return GetInfoFromReader(drd);
                }
                else
                {
                    return new M_Spec(true);
                }
            }
        }

        void ID_Spec.AddSpec(M_Spec Spec)
        {
            string strSql = "PR_Spec_AddUpdate";
            SqlParameter[] cmdParams = GetParameters(Spec);
            SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        void ID_Spec.UpdateSpec(M_Spec Spec)
        {
            string strSql = "PR_Spec_AddUpdate";
            SqlParameter[] cmdParams = GetParameters(Spec);
            SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        void ID_Spec.DelSpec(int SpecID)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@SpecID", SqlDbType.Int, 4) };
            cmdParams[0].Value = SpecID;
            SqlHelper.ExecuteProc("PR_Spec_Del", cmdParams);
        }

        int ID_Spec.GetFirstID()
        {
            string strSql = "Select Top 1 SpecID from ZL_Special Order by SpecID ASC";
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSql, null));
        }

        DataTable ID_Spec.GetSpecList(int SpecCateID)
        {
            string strSql = "select * from ZL_Special Where SpecCate=@SpecCate";
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@SpecCate", SqlDbType.Int, 4) };
            cmdParams[0].Value = SpecCateID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, cmdParams);
        }
        DataTable ID_Spec.GetSpecAll()
        {
            string strSql = "select * from ZL_Special Order by SpecID ASC";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        #endregion
        /// <summary>
        /// 将专题信息的各属性值传递到参数中
        /// </summary>
        /// <param name="administratorInfo"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParameters(M_Spec Spec)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@SpecID", SqlDbType.Int),
                new SqlParameter("@SpecName", SqlDbType.NVarChar, 50),
                new SqlParameter("@SpecDir", SqlDbType.NVarChar, 50),
                new SqlParameter("@SpecDesc", SqlDbType.NVarChar, 255),
                new SqlParameter("@OpenType", SqlDbType.Bit, 1),
                new SqlParameter("@SpecCate",SqlDbType.Int),
                new SqlParameter("@ListFileExt", SqlDbType.Int, 4),
                new SqlParameter("@ListFileRule",SqlDbType.Int,4),
                new SqlParameter("@ListTemplate", SqlDbType.NVarChar, 255)
            };
            parameter[0].Value = Spec.SpecID;
            parameter[1].Value = Spec.SpecName;
            parameter[2].Value = Spec.SpecDir;
            parameter[3].Value = Spec.SpecDesc;
            parameter[4].Value = Spec.OpenNew;
            parameter[5].Value = Spec.SpecCate;
            parameter[6].Value = Spec.ListFileExt;
            parameter[7].Value = Spec.ListFileRule;
            parameter[8].Value = Spec.ListTemplate;
            return parameter;
        }
        /// <summary>
        /// 从DataReader中读取专题信息
        /// </summary>
        /// <param name="rdr">DataReader</param>
        /// <returns></returns>
        private static M_Spec GetInfoFromReader(SqlDataReader rdr)
        {
            M_Spec info = new M_Spec();
            info.SpecID = DataConverter.CLng(rdr["SpecID"]);
            info.SpecName = rdr["SpecName"].ToString();
            info.SpecDir = rdr["SpecDir"].ToString();
            info.SpecDesc = rdr["SpecDesc"].ToString();
            info.SpecCate = DataConverter.CLng(rdr["SpecCate"]);
            info.OpenNew = DataConverter.CBool(rdr["OpenType"].ToString());
            info.ListFileExt = DataConverter.CLng(rdr["ListFileExt"]);
            info.ListFileRule = DataConverter.CLng(rdr["ListFileRule"]);
            info.ListTemplate = rdr["ListTemplate"].ToString();
            return info;
        }
    }
}
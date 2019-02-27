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
    /// SD_SpecCate 的摘要说明
    /// </summary>
    public class SD_SpecCate :ID_SpecCate
    {
        public SD_SpecCate()
        {
            
        }

        #region ID_SpecCate 成员

        void ID_SpecCate.AddCate(M_SpecCate SpecCate)
        {
            string strSql = "PR_SpecCate_AddUpdate";
            SqlParameter[] cmdParams = GetParameters(SpecCate);
            SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        void ID_SpecCate.Update(M_SpecCate SpecCate)
        {
            string strSql = "PR_SpecCate_AddUpdate";
            SqlParameter[] cmdParams = GetParameters(SpecCate);
            SqlHelper.ExecuteProc(strSql, cmdParams);
        }

        DataTable ID_SpecCate.GetCateList()
        {
            string strSql = "select * from ZL_SpecCate Order by SpecCateID";            
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }

        void ID_SpecCate.DelCate(int SpecCateID)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@SpecCateID", SqlDbType.Int, 4) };
            cmdParams[0].Value = SpecCateID;
            SqlHelper.ExecuteProc("PR_SpecCate_Del", cmdParams);
        }

        M_SpecCate ID_SpecCate.GetSpecCate(int SpecCateID)
        {
            SqlParameter[] cmdParams = new SqlParameter[] { new SqlParameter("@SpecCateID", SqlDbType.Int, 4) };
            cmdParams[0].Value = SpecCateID;
            string strSql = "select * from ZL_SpecCate where SpecCateID=@SpecCateID";
            using (SqlDataReader drd = SqlHelper.ExecuteReader(CommandType.Text, strSql, cmdParams))
            {
                if (drd.Read())
                {
                    return GetInfoFromReader(drd);
                }
                else
                {
                    return new M_SpecCate(true);
                }
            }
        }

        #endregion
        /// <summary>
        /// 将专题类别信息的各属性值传递到参数中
        /// </summary>
        /// <param name="administratorInfo"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParameters(M_SpecCate SpecCate)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@SpecCateID", SqlDbType.Int),
                new SqlParameter("@SpecCateName", SqlDbType.NVarChar, 50),
                new SqlParameter("@SpecCateDir", SqlDbType.NVarChar, 50),
                new SqlParameter("@SpecCateDesc", SqlDbType.NVarChar, 255),
                new SqlParameter("@OpenType", SqlDbType.Bit, 1),
                new SqlParameter("@FileExt", SqlDbType.Int, 4),
                new SqlParameter("@ListTemplate", SqlDbType.NVarChar, 255)
            };
            parameter[0].Value = SpecCate.SpecCateID;
            parameter[1].Value = SpecCate.SpecCateName;
            parameter[2].Value = SpecCate.SpecCateDir;
            parameter[3].Value = SpecCate.SpecCateDesc;
            parameter[4].Value = SpecCate.OpenNew;
            parameter[5].Value = SpecCate.FileExt;
            parameter[6].Value = SpecCate.ListTemplate;
            return parameter;
        }
        /// <summary>
        /// 从DataReader中读取专题类别信息
        /// </summary>
        /// <param name="rdr">DataReader</param>
        /// <returns></returns>
        private static M_SpecCate GetInfoFromReader(SqlDataReader rdr)
        {
            M_SpecCate info = new M_SpecCate();
            info.SpecCateID = DataConverter.CLng(rdr["SpecCateID"]);
            info.SpecCateName = rdr["SpecCateName"].ToString();
            info.SpecCateDir = rdr["SpecCateDir"].ToString();
            info.SpecCateDesc = rdr["SpecCateDesc"].ToString();
            info.OpenNew = DataConverter.CBool(rdr["OpenType"].ToString());
            info.FileExt = DataConverter.CLng(rdr["FileExt"]);
            info.ListTemplate = rdr["ListTemplate"].ToString();
            return info;
        }
    }
}
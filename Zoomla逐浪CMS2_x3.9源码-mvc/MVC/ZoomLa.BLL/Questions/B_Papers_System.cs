using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.Common;


namespace ZoomLa.BLL
{
    public class B_Papers_System
    {
        public string strTableName, PK;
        private M_Papers_System initMod = new M_Papers_System();
        public B_Papers_System()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Papers_System SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public bool GetUpdate(M_Papers_System model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Papers_System model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 根据条件查询系统试卷信息
        /// </summary>
        /// <param name="mps"></param>
        /// <returns></returns>
        /// 
        private List<M_Papers_System> GetSelByDataset(DataSet ds)
        {
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<M_Papers_System> mpss = new List<M_Papers_System>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    M_Papers_System mps = new M_Papers_System();
                    mps.Id = DataConverter.CLng(dr["Id"]);
                    mps.p_name = dr["p_name"].ToString();
                    mps.p_type = DataConverter.CLng(dr["p_type"]);
                    mpss.Add(mps);
                }
                return mpss;
            }
            return new List<M_Papers_System>();
        }
        public M_Papers_System GetSelectByM_Ps(M_Papers_System mps)
        {
            string sql = "SELECT [id],[p_name],[p_type] FROM [ZL_Papers_System] WHERE 1=1";
            if (mps != null)  //判断条件是否为空
            {
                if (mps.Id > 0)
                {
                    sql += " AND [id]=" + mps.Id;
                }
                if (!string.IsNullOrEmpty(mps.p_name))
                {
                    sql += " AND [p_name]='" + mps.p_name + "'";
                }
                if (mps.p_type > 0)
                {
                    sql += " AND [p_type]=" + mps.p_type;
                }
            }
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
            List<M_Papers_System> mpss = GetSelByDataset(ds);
            if (mpss != null && mpss.Count > 0)
            {
                return mpss[0];
            }
            else
            {
                return new M_Papers_System();
            }
        }
        public List<M_Papers_System> GetSelect_All()
        {
            string sql = "SELECT [id],[p_name],[p_type] FROM [ZL_Papers_System]";
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
            List<M_Papers_System> mpss = GetSelByDataset(ds);
            if (mpss != null && mpss.Count > 0)
            {
                return mpss;
            }
            else
            {
                return new List<M_Papers_System>();
            }
        }

        public int GetInsert(M_Papers_System mps)
        {
            string sql = "INSERT INTO [ZL_Papers_System]([p_name],[p_type])VALUES(@p_name,@p_type);SELECT @@IDENTITY";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@p_name",mps.p_name),
                new SqlParameter("@p_type",mps.p_type)
            };
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql, para));
        }
        public M_Papers_System GetSelect(int id)
        {
            string sqlStr = "SELECT [id],[p_name],[p_type] FROM [dbo].[ZL_Papers_System] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = id;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Papers_System();
                }
            }
        }
    }
}

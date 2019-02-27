using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_GuestAnswer
    {
        private string strTableName, PK;
        private M_GuestAnswer initMod = new M_GuestAnswer();
        public B_GuestAnswer()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        /// <summary>
        /// 通过问题的ID找答案
        /// </summary>
        /// <param name="QuestionID">问题的ID</param>
        /// <returns></returns>
        public DataTable GetAnswersByQid(int Qid)
        {
            string strSql = "select * from ZL_GuestAnswer where QueId=@QueId  order by ID Desc";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("QueId", Qid) };
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        //使用当中
        public DataTable Sel(string strWhere, string strOrderby, SqlParameter[] sp)
        {
            return Sql.Sel(strTableName, strWhere, strOrderby, sp);
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <returns></returns>
        public int getnum()
        {
            string sql = "SELECT COUNT(*) AS guestCount FROM ZL_GuestAnswer";
            DataTable dt = SqlHelper.ExecuteTable(sql);
            return DataConvert.CLng(dt.Rows[0]["guestCount"]);
        }
        /// <summary>
        /// 根据条件查询记录数
        /// </summary>
        /// <returns></returns>
        public int IsExistInt(int status)
        {
            string sql = "SELECT COUNT(*) AS guestCount FROM ZL_GuestAnswer WHERE Status=" + status;
            DataTable dt = SqlHelper.ExecuteTable(sql);
            return DataConvert.CLng(dt.Rows[0]["guestCount"]);
        }
        public M_GuestAnswer SelReturnModel(int ID)
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
        public M_GuestAnswer SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
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
        public bool UpdateByID(M_GuestAnswer model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }
        public bool UpdateStatus(int audit, int id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("audit", audit), new SqlParameter("id", id) };
            string sqlStr = "Update " + strTableName + " Set Status=@audit Where ID=@id";
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool DelByQueID(int qid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("qid", qid) };
            string sqlStr = "Delete From " + strTableName + " Where QueID=@qid";
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }
        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }
        public int insert(M_GuestAnswer model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }


        //----------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 根据用户id获得答案数量
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int GetAnserCount(int uid)
        {
            string sql = "SELECT COUNT(*) FROM ZL_GuestAnswer WHERE UserID=" + uid;
            return DataConvert.CLng(SqlHelper.ExecuteTable(sql).Rows[0][0]);
        }
        /// <summary>
        /// 根据问题id获取答案数量
        /// </summary>
        /// <returns></returns>
        public int GetAnserCountByQid(int qid)
        {
            string sql = "SELECT COUNT(*) FROM ZL_GuestAnswer WHERE QueID=" + qid;
            return DataConvert.CLng(SqlHelper.ExecuteTable(sql).Rows[0][0]);
        }
        public int GetCountByStatus(int status)
        {
            string sql = "SELECT COUNT(*) FROM ZL_GuestAnswer WHERE Status=" + status;
            return DataConvert.CLng(SqlHelper.ExecuteTable(sql).Rows[0][0]);
        }
        /// <summary>
        /// 获取用户回答过的问题IDS
        /// </summary>
        /// <returns></returns>
        public string GetUserAnswerIDS(int userid,int status = -100)
        {
            string sql = "SELECT QueId FROM ZL_GuestAnswer WHERE UserID=" + userid;
            if (status == -100) { sql += " AND status <> 0"; }
            else { sql += " AND status = " + status; }
            sql += " GROUP BY QueId";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            string ids = "";
            foreach (DataRow dr in dt.Rows)
            {
                ids += dr[0] + ",";
            }
            return ids.Trim(',');
        }
    }
}


using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
namespace ZoomLa.BLL
{
    public class B_Answer
    {
        private string strTableName,PK;
        private M_Answer initMod=new M_Answer();
        public B_Answer()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;

        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Answer SelReturnModel(int ID)
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
        public static DataTable GetUserAnswers(int sid, int uid, string time)
        {
            string sql = "select Qid, Answercontent from zl_answer where Surveyid = @sid and Userid = @uid and datediff(m, @time, Submitdate)= 0";
            DataTable dtable = new DataTable();
            SqlParameter[] sp = new SqlParameter[] { 
                new SqlParameter("@sid", SqlDbType.Int),
                new SqlParameter("@uid", SqlDbType.Int),
                new SqlParameter("@time", SqlDbType.VarChar, 30)
            };
            sp[0].Value = sid;
            sp[1].Value = uid;
            sp[2].Value = time;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable Sel(string strWhere, string strOrderby)
        {
            return Sql.Sel(strTableName, strWhere, strOrderby);
        }
        public bool UpdateByID(M_Answer model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.AnswerID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }
        public int insert(M_Answer model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public DataTable SelAsk(string strWhere)
        {
            return Sql.Sel(strTableName, strWhere, "");
        }
        /// <summary>
        /// 查询所有问题
        /// </summary>
        /// <returns></returns>
        public DataTable SelAsk()
        {
            return Sql.Sel(strTableName);
        }
        public bool AddAnswer(M_Answer model)
        {
            return insert(model)>0;
        }
        public bool UpdateAnswer(M_Answer model)
        {
            return UpdateByID(model);
        }
        public M_Answer GetAnswerByAid(int AnswerID)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int);
            cmdParams[0].Value = AnswerID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "select * from ZL_Answer where AID=@ID", cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                    return null;
            }
        }
        public bool DelAnswerByAid(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        /// <summary>
        /// 通过问题的ID找答案
        /// </summary>
        /// <param name="QuestionID">问题的ID</param>
        /// <returns></returns>
        public DataTable GetAnswersByQid(int Qid)
        {
            string strSql = "select * from ZL_Answer where QID=" + Qid + "  order by AID Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);
        }
        public bool DelByUid(int uid,int sid)
        {
            string str = "Delete From " + strTableName + " Where Userid=" + uid + " And Surveyid=" + sid;
            return SqlHelper.ExecuteSql(str);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;


namespace ZoomLa.BLL
{
 public class B_Answer_Recode
 {
        public B_Answer_Recode()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_Answer_Recode initmod = new M_Answer_Recode();
        public bool AddAnswer(M_Answer_Recode model)
        {
            return insert(model)>0;
        }
        public bool UpdateAnswer(M_Answer_Recode model)
        {
            return UpdateByID(model);
        }
        public M_Answer_Recode GetAnswerByAid(int AnswerID)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int);
            cmdParams[0].Value = AnswerID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "select * from ZL_Answer_Recode where AID=@ID", cmdParams))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                    return new M_Answer_Recode();
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
        public  DataTable GetAnswersByQid(int Qid)
        {
            string strSql = "select * from ZL_Answer_Recode where QID=@QID  order by AID Desc";
            SqlParameter[] sp = new SqlParameter[] { 
                new SqlParameter("@Qid", SqlDbType.Int)
            };
            sp[0].Value = Qid;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
      
     /// <summary>
     /// 由问卷ID， 获取最新的一条答卷记录
     /// </summary>
        public M_Answer_Recode GetRecordBySurID(int sid)
        {
            string strSql = "select top 1 * from ZL_Answer_Recode where Sid=@SID order by Rid desc";
            SqlParameter sp = new SqlParameter("@SID", SqlDbType.Int);
            sp.Value = sid;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSql, sp))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
      /// <summary>
     /// 由问卷ID获取所有记录
     /// </summary>
        public static DataTable  GetRecordsBySid(int sid)
        {
            List<M_Answer_Recode> records = new List<Model.M_Answer_Recode>();
            string strQuery = "select * from zl_answer_recode where Sid = "+sid+" And Status<>-1  order by Rid desc";//已提交

            return SqlHelper.ExecuteTable(CommandType.Text, strQuery);
        }
     ///// <summary>
     ///// 由问卷ID获取所有记录
     ///// </summary>
     //   public static  List<M_Answer_Recode> GetRecordsBySid(int sid)
     //   {
     //       List<M_Answer_Recode> records = new List<Model.M_Answer_Recode>();
     //       string strQuery = "select * from zl_answer_recode where Sid = "+sid+" And Status<>-1  order by Rid desc";//已提交

            //using(SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strQuery))
            //{
            //    while (!reader.IsClosed && reader.Read())
            //    {
            //        records.Add(M_Answer_Recode.GetModelFromReader(reader));
            //    }
            //}
            //return records;
     //   }
     
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_Answer_Recode SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (!reader.IsClosed && reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable SelByUID(int userid,int sid,string time) 
        {
            string sql = "Select * From " + strTableName + " Where Userid=@userid And Sid=@sid And datediff(m,Submitdate,@time)=0";
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("userid",userid),new SqlParameter("sid",sid),new SqlParameter("time",time) };
            return SqlHelper.ExecuteTable(CommandType.Text,sql,sp);
        }
        /// <summary>
        /// 排序
        /// </summary>
        public DataTable Sel(string strWhere, string strOrderby)
        {
            return Sql.Sel(strTableName, strWhere, strOrderby);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public DataTable SelPage(string strWhere, string strOrderby, int pageNum, int pageSize)
        {
            return Sql.SelPage(strTableName, PK, strWhere, strOrderby, pageNum, pageSize);
        }

        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_Answer_Recode model)
        {
            return Sql.UpdateByID(strTableName, PK, model.Rid, model.GetFieldAndPara(), model.GetParameters(model));
        }

        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }

        public bool Del(int ID)
        {
            return Sql.Delall(strTableName, PK, ID.ToString());
        }
        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }
        public int insert(M_Answer_Recode model)
        {
            return Sql.insertID(strTableName, model.GetParameters(model), model.GetParas(), model.GetFields());
        }
    }
}
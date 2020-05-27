using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_Exam_Type
    {
        public B_Exam_Type()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_Exam_Type initmod = new M_Exam_Type();
        /// <summary>
        /// <summary>
        /// 通过ID获取题型
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public M_Exam_Type GetSelectById(int typeId)
        {
            string sqlStr = "SELECT * FROM [ZL_Exam_Type] WHERE [t_id]=" + typeId;
            List<M_Exam_Type> mqts = GetTypeBySql(sqlStr);
            if (mqts != null && mqts.Count > 0)
            {
                return mqts[0];
            }
            else
            {
                return new M_Exam_Type();
            }
        }
        private List<M_Exam_Type> GetTypeBySql(string sql)
        {
            List<M_Exam_Type> mqts;
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                mqts = new List<M_Exam_Type>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    M_Exam_Type mqt = new M_Exam_Type();
                    mqt.t_id = DataConverter.CLng(dr["t_id"].ToString());
                    mqt.t_name = dr["t_name"].ToString();
                    mqt.t_type = DataConverter.CLng(dr["t_type"].ToString());
                    mqt.t_remark = dr["t_remark"].ToString();
                    mqt.t_createtime = DataConverter.CDate(dr["t_createtime"].ToString());
                    mqt.t_creatuser = DataConverter.CLng(dr["t_creatuser"].ToString());
                    mqts.Add(mqt);
                }
                return mqts;
            }
            else
            {
                return new List<M_Exam_Type>();
            }
        }
         /// <summary>
        /// 根据名称查询题型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public M_Exam_Type GetSelectByName(String name)
        {
            string sqlStr = "SELECT * FROM [ZL_Exam_Type] WHERE [t_name]='" + name + "'";
            List<M_Exam_Type> mqts = GetTypeBySql(sqlStr);
            if (mqts != null && mqts.Count > 0)
            {
                return mqts[0];
            }
            else
            {
                return new M_Exam_Type();
            }
        }


        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public List<M_Exam_Type> SelectAll()
        {
            string sql = "SELECT * FROM [ZL_Exam_Type]";
            return GetTypeBySql(sql);
        }
        public DataTable SelAll()
        {
            string sql = "SELECT * FROM [ZL_Exam_Type]";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mqt"></param>
        /// <returns></returns>
        public int GetAdd(M_Exam_Type mqt)
        {
            string sqlStr = "INSERT INTO [dbo].[ZL_Exam_Type] ([t_name],[t_type],[t_remark],[t_createtime],[t_creatuser]) VALUES (@t_name,@t_type,@t_remark,@t_createtime,@t_creatuser);select @@IDENTITY";
            SqlParameter[] cmdParams = initmod.GetParameters(mqt);
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sqlStr, cmdParams));
        }

        /// <summary>
        /// 修改题型
        /// </summary>
        /// <param name="mqt"></param>
        /// <returns></returns>
        public bool GetUpdate(M_Exam_Type mqt)
        {
            string sqlStr = "UPDATE [dbo].[ZL_Exam_Type] SET [t_name] = @t_name,[t_type] = @t_type,[t_remark] = @t_remark,[t_createtime] = @t_createtime,[t_creatuser] = @t_creatuser WHERE [t_id] = @t_id";
            SqlParameter[] cmdParams = initmod.GetParameters(mqt);
            return SqlHelper.ExecuteSql(sqlStr, cmdParams);
        }

        /// <summary>
        /// 执行删除
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public bool GetDelete(int tid)
        {
            return Sql.Del(strTableName, tid);
        }

        /// <summary>
        /// 通过试卷ID查询
        /// </summary>
        /// <param name="pid">试卷ID</param>
        /// <returns></returns>
        public DataTable Select_Pid(int pid)
        {
            string sqlStr = "SELECT * FROM [ZL_Exam_Type] WHERE t_id IN (SELECT QuestionType from ZL_Paper_Questions where PaperID=@PaperID)";
            SqlParameter[] para = new SqlParameter[]{
             new SqlParameter("@PaperID", pid)   
            };
            return SqlHelper.ExecuteTable(CommandType.Text, sqlStr, para);
        }
    }
}

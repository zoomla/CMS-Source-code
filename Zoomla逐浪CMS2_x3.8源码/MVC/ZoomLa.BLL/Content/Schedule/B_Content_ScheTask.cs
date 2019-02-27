using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_Content_ScheTask
    {
        M_Content_ScheTask schModel = new M_Content_ScheTask();
        private string TbName;
        private string PK;
        public B_Content_ScheTask()
        {
            TbName = schModel.TbName;
            PK = schModel.PK;
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", PK + " DESC");
        }
        public DataTable Sel(int taskType, string skey = "")
        {
            string where = " 1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (taskType != -100) { where += " AND TaskType=" + taskType; }
            if (!string.IsNullOrEmpty(skey))
            {
                where += " AND TaskName LIKE @TaskName";
                sp.Add(new SqlParameter("TaskName", "%" + skey + "%"));
            }
            return DBCenter.Sel(TbName, where, PK + " DESC", sp);
        }
        public PageSetting SelPage(int cpage, int psize, int taskType = -100, string taskName = "")
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (taskType != -100) { where += " AND TaskType=" + taskType; }
            if (!string.IsNullOrEmpty(taskName)) { where += " AND TaskName LIKE @TaskName"; sp.Add(new SqlParameter("TaskName", "%" + taskName + "%")); }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, PK + " DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Content_ScheTask SelReturnModel(int id) { return GetModel(id); }
        public M_Content_ScheTask GetModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
            {
                if (reader.Read())
                {
                    return schModel.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable SelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string where = PK + " IN(" + ids + ")";
            return DBCenter.Sel(TbName, where);
        }
        public bool Update(M_Content_ScheTask model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Delete(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int Add(M_Content_ScheTask model)
        {
            return DBCenter.Insert(model);
        }
        //-------------------------------TaskCenter
        /// <summary>
        /// 获取需要执行的TaskType(用于TaskCenter)
        /// </summary>
        public List<M_Content_ScheTask> SelTaskList()
        {
            List<M_Content_ScheTask> list = new List<M_Content_ScheTask>();
            DataTable dt = DBCenter.Sel(TbName, "Status=0");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new M_Content_ScheTask().GetModelFromDR(dr));
            }
            return list;
        }
        //-------------------------------Tools
        /// <summary>
        /// 根据类型找出未完成的任务
        /// </summary>
        public DataTable SelByTaskType(M_Content_ScheTask.TaskTypeEnum type)
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Where TaskType=" + (int)type);
        }
        public M_Content_ScheTask SelByGid(int gid, M_Content_ScheTask.TaskTypeEnum type)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE TaskContent='" + gid + "' AND TaskType=" + (int)type;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (dr.Read())
                    return new M_Content_ScheTask().GetModelFromReader(dr);
                else
                    return null;
            }
        }
        public DataTable SelByStatus(string status)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("status", status) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Where Status=@status", sp);
        }
        /// <summary>
        /// 获取需要执行的任务
        /// </summary>
        public DataTable SelTask()
        {
            string sql = "Select * From " + TbName + " Where Status=0 OR IsLoop in(1,2)";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public string GetExecuteType(int executeType)
        {
            switch (executeType)
            {
                case (int)M_Content_ScheTask.ExecuteTypeEnum.JustOnce:
                    return "仅一次";
                case (int)M_Content_ScheTask.ExecuteTypeEnum.EveryDay:
                    return "每日";
                case (int)M_Content_ScheTask.ExecuteTypeEnum.Interval:
                    return "循环";
                case (int)M_Content_ScheTask.ExecuteTypeEnum.Passive:
                    return "被动";
                case (int)M_Content_ScheTask.ExecuteTypeEnum.EveryMonth:
                    return "每月";
                default:
                    return "未知(" + executeType + ")";
            }
        }
        //-------------------------------
        public DataTable DelByID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
            return SqlHelper.ExecuteTable(CommandType.Text, "delete from " + TbName + " Where ID=@id", sp);
        }
        public void UpdateStatus(string ids, int status)
        {
            if (string.IsNullOrWhiteSpace(ids)) { return; }
            SafeSC.CheckIDSEx(ids);
            DBCenter.UpdateSQL(TbName, "Status=" + status, PK + " IN(" + ids + ")");
        }
        //public void UpdateStatus(int id)
        //{
        //    SqlHelper.ExecuteScalar(CommandType.Text, "Update " + TbName + " Set [Status] = 99 , LastTime = '"+DateTime.Now.ToString()+"' Where id = " + id);
        //}
    }
}

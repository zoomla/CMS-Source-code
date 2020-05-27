using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Sentiment;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Sentiment
{
    public class B_Sen_Data : ZL_Bll_InterFace<M_Sen_Data>
    {
        public string TbName, PK;
        public M_Sen_Data initMod = new M_Sen_Data();
        public B_Sen_Data()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", " CDate DESC");
        }
        public DataTable SelById(int ID)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE ID=" + ID + " ORDER BY CDate DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable SelByKey(string key, string source)
        {
            return SelByKey(key, source, DateTime.Now.AddDays(-31));
        }
        public DataTable SelByKey(string skey, string source, DateTime stime)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("skey", skey), new SqlParameter("stime", stime), new SqlParameter("Source", source) };
            string sql = "SELECT * FROM " + TbName + " WHERE TaskInfo=@skey AND CollDate>=@stime ";
            if (!string.IsNullOrEmpty(source))
            {
                sql += " AND Source=@Source";
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public PageSetting SelPage(int cpage, int psize, string taskInfo = "", string source = "", DateTime? stime = null)
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(taskInfo)) { where += " AND TaskInfo=@taskInfo"; sp.Add(new SqlParameter("taskInfo", taskInfo)); }
            if (!string.IsNullOrEmpty(source)) { where += " AND Source=@source"; sp.Add(new SqlParameter("source", source)); }
            if (stime != null) { where += " AND CollDate>=@stime"; sp.Add(new SqlParameter("stime", stime)); }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public int Insert(M_Sen_Data model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Sen_Data model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_Sen_Data SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
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
        //筛选出最近一次采集到的该关键词数据
        public M_Sen_Data SelLastModel(string skey)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("skey", skey) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE TaskInfo=@skey", sp))
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
    }
}

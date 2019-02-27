using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Collections.Generic;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_MisInfo
    {
        private string PK, strTableName;
        private M_MisInfo initMod = new M_MisInfo();
        public B_MisInfo()
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public DataTable SelByUid(int uid)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE MID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public M_MisInfo SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return new M_MisInfo().GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public List<M_MisInfo> GetAllLastestMemos()
        {
            string sql = String.Format("select * from {0} where [Type] = 4 order by {1} desc", strTableName, PK);
            return GetMemos(sql, CommandType.Text);
        }
        public List<M_MisInfo> GetUserMemos(string uname)
        {
            string sql = String.Format("select * from {0} where [Type] = 4 and Inputer = @name order by {1} desc", strTableName, PK);
            SqlParameter pram = new SqlParameter("@name", uname);
            return GetMemos(sql, CommandType.Text, pram);
        }
        private List<M_MisInfo> GetMemos(string sql, CommandType stype, params SqlParameter[] prams)
        {
            List<M_MisInfo> lstMemos = new List<M_MisInfo>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(stype, sql, prams))
            {
                while (reader.Read())
                {
                    lstMemos.Add(new M_MisInfo().GetModelFromReader(reader));
                }
            }
            return lstMemos;
        }

        public List<M_MisInfo> GetSharedMemos(string uname)
        {
            return null;
        }
        public M_MisInfo SelFirstModel(int uid)
        {
            return SelReturnModel(" WHERE Mid=" + uid);
        }
        private M_MisInfo SelReturnModel(string strWhere)
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
        public DataTable Sel(string Inputer, int type = -100, string Date = "")
        {
            string where = "Inputer=@Inputer";
            List<SqlParameter> sp = new List<SqlParameter>();
            sp.Add(new SqlParameter("Inputer", Inputer));
            if (type != -100) { where += " And Type=" + type; }
            if (!string.IsNullOrEmpty(Date))
            {
                var newDate = DateTime.MinValue;
                bool isdate = DateTime.TryParse(Date, out newDate);
                if (isdate)
                {
                    where += " And Datediff(d,CreateTime,@Date) = 0";
                    sp.Add(new SqlParameter("Date", newDate));
                }
            }
            return Sql.Sel(strTableName, where, "createtime desc", sp.ToArray());
        }
        public PageSetting SelPage(int cpage, int psize, string inputer = "", int type = -100, string date = "")
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(inputer)) { where += " AND Inputer=@inputer"; sp.Add(new SqlParameter("inputer", inputer)); }
            if (type != -100) { where += " AND Type=" + type; }
            if (!string.IsNullOrEmpty(date))
            {
                var newDate = DateTime.MinValue;
                bool isdate = DateTime.TryParse(date, out newDate);
                if (isdate)
                {
                    where += " And Datediff(d,CreateTime,@Date) = 0";
                    sp.Add(new SqlParameter("Date", newDate));
                }
            }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_MisInfo model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool UpdateStatus(int status, int id)
        {
            return Sql.Update(strTableName, "Status=" + status, "ID=" + id, null);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_MisInfo model)
        {
            return Sql.insertID(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

    }
}

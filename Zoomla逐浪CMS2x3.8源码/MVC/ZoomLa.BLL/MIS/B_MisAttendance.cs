using System;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_MisAttendance
    {
        public B_MisAttendance()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        private string PK, strTableName;
        private M_MisAttendance initMod = new M_MisAttendance();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
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
        public List<M_MisAttendance> GetAllLastestMemos()
        {
            string sql = String.Format("select * from {0} where [Type] = 4 order by {1} desc", strTableName, PK);
            return GetMemos(sql, CommandType.Text);
        }
        public List<M_MisAttendance> GetUserMemos(string uname)
        {
            string sql = String.Format("select * from {0} where [Type] = 4 and Inputer = @name order by {1} desc", strTableName, PK);
            SqlParameter pram = new SqlParameter("@name", uname);
            return GetMemos(sql, CommandType.Text, pram);
        }
        private List<M_MisAttendance> GetMemos(string sql, CommandType stype, params SqlParameter[] prams)
        {
            List<M_MisAttendance> lstMemos = new List<M_MisAttendance>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(stype, sql, prams))
            {
                while (reader.Read())
                {
                    lstMemos.Add(initMod.GetModelFromReader(reader));
                }
            }
            return lstMemos;
        }
        private M_MisAttendance SelReturnModel(string strWhere)
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_MisAttendance model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), initMod.GetFieldAndPara(), initMod.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_MisAttendance model)
        {
            return Sql.insert(strTableName, initMod.GetParameters(), initMod.GetParas(), initMod.GetFields());
        }
    }
}

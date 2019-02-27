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
    public class B_Ask
    {
        public B_Ask()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        private string PK, strTableName;
        private M_Ask initMod = new M_Ask();
        public M_Ask SelReturnModel(int ID)
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
        public int getnum()
        {
            return Sql.getnum(strTableName);
        }
        public int IsExistInt(string strWhere)
        {
            return Sql.IsExistInt(strTableName, strWhere);
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable Sel(string strWhere, string strOrderby, SqlParameter[] sp)
        {
            return Sql.Sel(strTableName, strWhere, strOrderby, sp);
        }
        public bool UpdateByID(M_Ask model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), initMod.GetParameters());
        }
        //public bool Update(string strSet, string strWhere,SqlParameter[] sp)
        //{
        //    return Sql.Update(strTableName, strSet, strWhere, sp);
        //}
        public void UpdateByField(string field, string value, int mid)
        {
            UpdateByField(field, value, mid.ToString());
        }
        public void UpdateByField(string field, string value, string ids)
        {
            SafeSC.CheckDataEx(field);
            SafeSC.CheckIDSEx(ids);
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("value", value) };
            DBCenter.UpdateSQL(strTableName, field + " = @value", PK + " IN (" + ids + ")", sp);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Ask model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable SelUser()
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "select a.* from (select a.UserID as cuid,count(a.UserID) as ccount from ZL_User a,ZL_GuestAnswer b where b.Status=1 and a.UserID=b.UserID group by(a.UserID)) c,ZL_User a where c.cuid=a.UserID order by c.ccount desc", null);
            //return DBCenter.JoinQuery("A.*,","","ZL_User", "ZL_GuestAnswer",);
        }
        public DataTable SelfieldOrd(string strField, int num)
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "select top " + num + " " + strField + " from " + strTableName + " Group By " + strField + " Order By " + strField, null);
        }
        public DataTable SelAll()
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE Status=1 ORDER BY AddTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable SelWaitQuest()
        {
            string sql = "SELECT * FROM " + strTableName + " A WHERE (SELECT COUNT(*) FROM ZL_GuestAnswer WHERE QueId=A.ID)=0 AND Status=1 ORDER BY AddTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }

        //----------------------------------------------------------------------------------------------------------
        public DataTable Search(int queType, int status, int uid, string skey)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = " 1=1 ";
            skey = skey.Trim();
            if (queType > 0) { where += " AND QueType=" + queType; }
            if (status != -100) { where += " AND Status=" + status; }
            if (uid != 0) { where += " AND UserID=" + uid; }
            if (!string.IsNullOrEmpty(skey)) { where += " AND Qcontent LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return DBCenter.Sel(strTableName, where, PK + " DESC", sp);
        }
        public PageSetting SelPage(int cpage, int psize, int queType = -100, int status = -100, int uid = -100, string key = "")
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (queType != -100) { where += " AND QueType=" + queType; }
            if (status != -100) { where += " AND Status=" + status; }
            if (uid != -100) { where += " AND UserID=" + uid; }
            if (!string.IsNullOrEmpty(key)) { where += " AND Qcontent LIKE @skey"; sp.Add(new SqlParameter("skey", "%" +key + "%")); }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据用户id获得问题数量
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int GetAskCount(int uid)
        {
            return DBCenter.Count(strTableName, "UserID=" + uid);
        }
        /// <summary>
        /// 获得前5名问答积分最多的用户
        /// </summary>
        public DataTable GetTopUser()
        {
            return DBCenter.SelTop(5, "UserID", "*", "ZL_User", "", "GuestScore DESC");
        }
        /// <summary>
        ///根据问题类型统计数量
        /// </summary>
        public int GetCountByQueType(string ids)
        {
            if (string.IsNullOrEmpty(ids)) { return 0; }
            SafeSC.CheckIDSEx(ids);
            string sql = "SELECT COUNT(*) FROM " + strTableName + " WHERE QueType IN (" + ids + ")";
            return DataConvert.CLng(SqlHelper.ExecuteTable(sql).Rows[0][0]);
        }
        /// <summary>
        /// 根据问题状态统计数量
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public int GetCountByStatus(int status)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE Status=" + status;
            DataTable dt = SqlHelper.ExecuteTable(sql);
            if (dt.Rows.Count <= 0) { return 0; }
            return DataConvert.CLng(dt.Rows[0][0]);
        }
    }

}


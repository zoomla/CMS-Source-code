namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using SQLDAL.SQL;
    public class B_Cash
    {
        private string strTableName, PK;
        private M_Cash initMod = new M_Cash();
        public B_Cash() 
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize, int uid, string stime, string etime)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = " 1=1 ";
            if (uid > 0) { where += " AND UserID=" + uid; }
            if (!string.IsNullOrEmpty(stime)) { where += " AND sTime>=@stime"; sp.Add(new SqlParameter("stime", stime)); }
            if (!string.IsNullOrEmpty(etime)) { where += " AND etime<=@etime"; sp.Add(new SqlParameter("etime", etime)); }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Cash SelReturnModel(int ID)
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
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Cash model)
        {
            return DBCenter.Insert(model);
        }
        /// <summary>
        /// 根据状态筛选,具体见模型层
        /// </summary>
        public DataTable SelByState(Model.ZLEnum.WDState state)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE ystate=" + (int)state;
            return SqlHelper.ExecuteTable(sql);
        }
        public DataTable SelByState(int state, string username = "")
        {
            //string PK = "Y_ID";
            string mtable = "(SELECT A.*,B.GroupID,B.HoneyName FROM ZL_Cash A LEFT JOIN ZL_User B ON A.UserID=B.UserID)";
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("@uname", "%" + username + "%") };
            string where = " 1=1 ";
            string order = PK + " DESC";
            where += state == -2 ? "" : " AND A.yState=" + state;
            if (!string.IsNullOrEmpty(username)) { where += " AND A.YName LIKE @uname "; }
            return DBCenter.JoinQuery("A.*,B.GroupName", mtable, "ZL_Group", "A.GroupID=B.GroupID", where, order, sp.ToArray());
        }
        public void UpdateState(string ids, ZLEnum.WDState state)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE " + strTableName + " SET Ystate=" + (int)state + " WHERE Y_ID IN(" + ids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        public bool UpdateByID(M_Cash model)
        {
           return DBCenter.UpdateByID(model,model.Y_ID);
        }
    }
}

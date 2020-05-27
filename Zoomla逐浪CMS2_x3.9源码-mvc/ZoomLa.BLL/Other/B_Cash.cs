namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    public class B_Cash
    {
        private string strTableName, PK;
        private M_Cash initMod = new M_Cash();
        public B_Cash() 
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
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
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable SelByState(int state, string username = "")
        {
            //string PK = "Y_ID";
            string mtable = "(SELECT A.*,B.GroupID FROM ZL_Cash A LEFT JOIN ZL_User B ON A.UserID=B.UserID)";
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("@uname", "%" + username + "%") };
            string where = " 1=1 ";
            string order = PK + " DESC";
            where += state == -2 ? "" : " AND A.yState=" + state;
            if (!string.IsNullOrEmpty(username)) { where += " AND A.YName LIKE @uname "; }
            return DBCenter.JoinQuery("A.*,B.GroupName", mtable, "ZL_Group", "A.GroupID=B.GroupID", where, order, sp.ToArray());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Cash model)
        {
            return Sql.insertID(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 根据状态筛选,具体见模型层
        /// </summary>
        public DataTable SelByState(Model.ZLEnum.WDState state)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE ystate=" + (int)state;
            return SqlHelper.ExecuteTable(sql);
        }
        public void UpdateState(string ids, ZLEnum.WDState state)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE " + strTableName + " SET Ystate=" + (int)state + " WHERE Y_ID IN(" + ids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        public bool UpdateByID(M_Cash model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Y_ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        //------------------------------------------------
        public double SelectCashMoney(int uid)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@uid", SqlDbType.Int);
            cmdParams[0].Value = uid;
            string strsql = "select sum(money) from ZL_Cash where UserID=@uid";
            object objA = SqlHelper.ExecuteScalar(CommandType.Text, strsql, cmdParams);
            return (object.Equals(objA, null) ? 0 : DataConverter.CDouble(objA.ToString()));
        }
        public double SelectCashMoney(int uid, int type)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@uid", SqlDbType.Int);
            cmdParams[0].Value = uid;
            string strsql;
            if (type == 0)
                strsql = "select sum(money) from ZL_Cash where UserID=@uid and classes=0";
            else
                strsql = "select sum(money) from ZL_Cash where UserID=@uid and classes=1";
            object objA = SqlHelper.ExecuteScalar(CommandType.Text, strsql, cmdParams);
            return (object.Equals(objA, null) ? 0 : DataConverter.CDouble(objA.ToString()));
        }
        public bool UpCash(M_Cash c)
        {
            string strSql = "Update ZL_Cash set yState=@yState,eTime=@eTime Where Y_ID=@Y_ID";
            SqlParameter[] cmdParams = new SqlParameter[] {
                new SqlParameter("@yState", SqlDbType.Int),
                new SqlParameter("@eTime", SqlDbType.DateTime),  
                new SqlParameter("@Y_ID", SqlDbType.Int)
            };
            cmdParams[0].Value = c.yState;
            cmdParams[1].Value = c.eTime;
            cmdParams[2].Value = c.Y_ID;
            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        public DataTable SelectAll()
        {
            string strSql = "select * from ZL_Cash order by Y_ID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public DataTable SelectAll(int type)
        {
            string strSql;
            if (type == 0)
                strSql = "select * from ZL_Cash where  classes=0 order by Y_ID desc";
            else
                strSql = "select * from ZL_Cash where  classes=1 order by Y_ID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public DataTable SelectyState(int start)
        {
            string strSql = "select * from ZL_Cash where  yState=@start order by Y_ID desc";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@start", SqlDbType.Int, 4);
            cmdParams[0].Value = start;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, cmdParams);
        }
        public DataTable SelectUserAll(int UserID)
       {
           string strSql = "select * from ZL_Cash where UserID=@UserID order by Y_ID desc";
           SqlParameter[] cmdParams = new SqlParameter[1];
           cmdParams[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
           cmdParams[0].Value = UserID;
           return SqlHelper.ExecuteTable(CommandType.Text, strSql, cmdParams);
       }
        public DataTable SelectUserAll(int UserID, int type)
       {
           string strSql;
           if (type == 0)
               strSql = "select * from ZL_Cash where UserID=@UserID and classes=0 order by Y_ID desc";
           else
               strSql = "select * from ZL_Cash where UserID=@UserID and classes=1 order by Y_ID desc";
           SqlParameter[] cmdParams = new SqlParameter[1];
           cmdParams[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
           cmdParams[0].Value = UserID;
           return SqlHelper.ExecuteTable(CommandType.Text, strSql, cmdParams);
       }
        public bool DelCashid(int id)
        {

            string strSql = "DELETE FROM ZL_Cash WHERE Y_ID=@id";

            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = id;

            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        public bool DelCashAll(string c_id)
        {
            string sqlStr = "DELETE FROM ZL_Cash WHERE Y_ID in(" + c_id + ")";
            return SqlHelper.ExecuteSql(sqlStr, null);
        }
    }
}

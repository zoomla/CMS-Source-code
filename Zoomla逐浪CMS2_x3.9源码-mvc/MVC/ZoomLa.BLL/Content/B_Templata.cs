using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    
    public class B_Templata
    {
        private string strTableName, PK;
        private M_Templata initMod = new M_Templata();
        public B_Templata()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Templata SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Templata();
                }
            }
        }
        private M_Templata SelReturnModel(string strWhere)
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
            string sql = "SELECT *,TemplateID AS ID,TemplateName AS Name FROM " + strTableName;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        //根据styleid划分,为0的则是共有的
        public DataTable SelByStyleAndPid(int styleid)
        {
            string sql = "SELECT *,TemplateID AS ID,TemplateName AS Name FROM " + strTableName + " WHERE (UserGroup=" + styleid + " OR UserGroup=0) AND IsTrue=1";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_Templata model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.TemplateID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }

        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM "+strTableName+ " WHERE TemplateID IN ("+ids+") AND Userid!=0";
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 设置隐藏
        /// </summary>
        /// <returns></returns>
        public bool ChangeStatus(string ids,int istrue)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE "+strTableName+" SET IsTrue="+ istrue+ " WHERE  TemplateID IN (" + ids + ") AND AND Userid!=0";
            return SqlHelper.ExecuteSql(sql);
        }
        public int insert(M_Templata model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool Add(M_Templata model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model)) > 0;
        }
        public bool Delete(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        public bool Update(M_Templata model)
        {
            return Sql.UpdateByID(strTableName, model.PK, model.TemplateID, BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public DataTable Readall()
        {
            return Sel();
        }
        public DataTable SelByPid(int pid)
        {
            string sql = "SELECT * FROM "+strTableName+ " WHERE ParentID="+pid+" ORDER BY OrderID";
            return SqlHelper.ExecuteTable(sql);
        }
        public int GetNextOrderID(int pid)
        {
            string sql = "SELECT TOP 1 OrderID FROM "+strTableName+" WHERE ParentID="+pid+" ORDER BY OrderID DESC";
            DataTable dt = SqlHelper.ExecuteTable(sql);
            if (dt.Rows.Count > 0) { int orderid = DataConvert.CLng(dt.Rows[0]["OrderID"]);return orderid + 1; }
            return 1;
        }
        public M_Templata Getbyid(int ID)
        {
            return SelReturnModel(ID);
        }
        public DataTable Getinputinfo(string datefiles, string datavalue)
        {
            string strSql = "select * from ZL_PageTemplate where " + datefiles + "=" + datavalue + " order by orderid desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public DataTable ReadParentID(int ParentID)
        {
            string strSql = "SELECT *  FROM [ZL_PageTemplate] where ParentID=" + ParentID + "  order by orderid desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);
        }
        public DataTable ReadSysParentID(int ParentID)
        {
            string strSql = "SELECT *  FROM [ZL_PageTemplate] where ParentID=" + ParentID + " and  Userid=0  order by orderid desc";
            return SqlHelper.ExecuteTable(CommandType.Text,strSql);
        }

        public DataTable GetByUserinfo(string datefiles, string datavalue)
        {
            SafeSC.CheckDataEx(datefiles);
            string sql = "select * from ZL_PageTemplate where  " + datefiles + "=@TemplateID and ParentID=0 order by orderid desc";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("TemplateID", datavalue) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }

        public DataTable ReadUserall(int ParentID, int Userid, int UserGroup)
        {
            string strSql = "select * from ZL_PageTemplate where (UserGroup=@UserGroup and ParentID=@ParentID and userid=0)  or (userid=@Userid and ParentID=@ParentID and UserGroup=@UserGroup) order by orderid desc";
            SqlParameter[] parameter ={
            new SqlParameter("@ParentID",SqlDbType.Int),
            new SqlParameter("@Userid",SqlDbType.Int),
            new SqlParameter("@UserGroup", SqlDbType.Int)
            };
            parameter[0].Value = ParentID;
            parameter[1].Value = Userid;
            parameter[2].Value = UserGroup;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, parameter);
        }
        public bool UpdateOrder(int temid, int oid)
        {
            string strSql = "Update " + strTableName + " Set OrderID=" + oid + " where TemplateID=" + temid;
            return SqlHelper.ExecuteSql(strSql);
        }
        public int GetOrderID(int id)
        {
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where ParentID=" + id);
            if (dt.Rows.Count > 0)
            {
                string strSql3 = "select max(OrderID) from " + strTableName + " Where ParentID=" + id;
                return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql3, null));
            }
            else
                return 0;
        }
    }
}

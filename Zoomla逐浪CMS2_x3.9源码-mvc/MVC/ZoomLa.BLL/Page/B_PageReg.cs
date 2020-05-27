using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model.Page;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.Common;

namespace ZoomLa.BLL.Page
{
    public class B_PageReg
    {

        public string strTableName,PK;
        private M_PageReg initMod = new M_PageReg();
        public B_PageReg()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public M_PageReg GetSelectByUserID(int UserID)
        {
            string strSQL = "SELECT TOP 1 * FROM [ZL_PageReg] WHERE [UserID]=" + UserID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, strSQL))
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
        public M_PageReg GetSelectByUName(string uname)
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("UserName",uname) };
            string sql = "SELECT * FROM [ZL_PageReg] WHERE [UserName]=@UserName";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, sp))
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
        public int Add(M_PageReg m_PageReg)
        {
            string strSQL = "INSERT INTO [" + strTableName + "](" + BLLCommon.GetFields(m_PageReg) + ")VALUES(" + BLLCommon.GetParas(m_PageReg) +")";
            return DataConverter.CLng(SqlHelper.ExecuteScalar(CommandType.Text, strSQL, m_PageReg.GetParameters()));
        }

        public object Update(M_PageReg m_PageReg)
        {
            string strSQL = "UPDATE [" + strTableName + "] SET " + BLLCommon.GetFieldAndPara(m_PageReg) + " WHERE [ID]=" + m_PageReg.ID;
            return SqlHelper.ExecuteScalar(CommandType.Text, strSQL, m_PageReg.GetParameters());
        }
        /// <summary>
        /// 批量修改黄页样式
        /// </summary>
        public void UpldateByIds(string ids, int styleID)
        {
            string sql = "Update " + strTableName + " Set NodeStyle=" + styleID + " Where ID in({0})";
            SafeSC.CheckIDSEx(ids);
            sql = string.Format(sql, ids);
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        public M_PageReg SelReturnModel(int ID)
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
        public M_PageReg SelModelByUid(int uid)
        {
            string where = " WHERE UserID="+uid;
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName,where))
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

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable SelByWhere(int type)
        {
            string strwhere = "";
            switch (type)
            {
                case 0:
                    strwhere = "1=1";
                    break;
                case 1:
                    strwhere = "[Status]=0";
                    break;
                case 2:
                    strwhere = "[Status]=1";
                    break;
                default:
                    strwhere = "1=1";
                    break;
            }
            string sql = "Select * From " + strTableName + " Where " + strwhere + " Order By UpdateTime Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 排序
        /// </summary>
        public DataTable Sel(string strWhere, string strOrderby,SqlParameter[] sp=null)
        {
            return Sql.Sel(strTableName, strWhere, strOrderby,sp);
        }
        public DataTable SelByStatus(string status, string uname)
        {
            string where = " 1=1 ";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uname", "%" + uname + "%") };
            if (!string.IsNullOrEmpty(status))
            {
                where += " AND [Status]=" + DataConvert.CLng(status);
            }
            if (!string.IsNullOrEmpty(uname))
            {
                where += " AND UserName LIKE @uname";
            }
            return SqlHelper.JoinQuery("A.*,B.PageNodeName", strTableName, "ZL_PageStyle", "A.NodeStyle=B.PageNodeID", where, "UpdateTime DESC", sp);
        }

        /// <summary>
        /// 根据ID更新
        /// </summary>
        public bool UpdateByID(M_PageReg m_PageReg)
        {
            return Sql.UpdateByID(strTableName, PK, m_PageReg.ID, BLLCommon.GetFieldAndPara(m_PageReg), m_PageReg.GetParameters());
        }
        public bool UpdateByField(string fieldName,string value,string ids)
        {
            SafeSC.CheckDataEx(fieldName);
            SafeSC.CheckIDSEx(ids);
            string sql = "Update "+strTableName+" Set "+fieldName+" =@value Where [id] in("+ids+")";
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("value",value) };
            SqlHelper.ExecuteNonQuery(CommandType.Text,sql,sp);
            return true;
        }
        public bool Update(string strSet, string strWhere)
        {
            return Sql.Update(strTableName, strSet, strWhere, null);
        }
        public bool DelByIDS(string ids) 
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Delete "+strTableName+" Where "+PK+" in ("+ids+")";
            SqlHelper.ExecuteNonQuery(CommandType.Text,sql);
            return true;
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }

        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }

        public bool UpTemplata(int id, string tempstr)
        {
            string strSql = "Update ZL_PageReg set Template=@Temp where ID=@ID";
            SqlParameter[] cmdParam = new SqlParameter[] {
                new SqlParameter("@ID",SqlDbType.Int,4),
                new SqlParameter("@Temp",SqlDbType.NVarChar,200)
            };
            cmdParam[0].Value = id;
            cmdParam[1].Value = tempstr;
            return SqlHelper.ExecuteSql(strSql, cmdParam);
        }
    }
}
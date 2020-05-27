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
    public class B_Mail_BookRead
    {
        public B_Mail_BookRead()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        private string PK, strTableName;
        private M_Mail_BookRead initmod = new M_Mail_BookRead();

        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Mail_BookRead GetSelectById(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public M_Mail_BookRead SelByEMail(string email)
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("@email",email) };
            string sql = "SELECT * FROM "+strTableName+" WHERE EMail=@email";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text,sql,sp))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public M_Mail_BookRead SelByCode(string code)
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("@code",code) };
            string sql = "SELECT * FROM "+strTableName+ " WHERE AuthCode=@code";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, sp))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_Mail_BookRead SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
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
        public DataTable SelectAll(int audit=-10)
        {
            string wherestr = "";
            if (audit > -10) { wherestr += " AND isAudit=" + audit; }
            string sql = "SELECT A.*,B.UserName FROM "+strTableName+" A LEFT JOIN ZL_User B ON A.UserID=B.UserID WHERE 1=1 "+wherestr+" ORDER BY CDate DESC";
            return SqlHelper.ExecuteTable(sql);
        }
        public bool GetUpdata(M_Mail_BookRead model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), initmod.GetParameters());
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM "+strTableName+" WHERE ID IN ("+ids+")";
            return SqlHelper.ExecuteSql(sql);
        }
        public int GetInsert(M_Mail_BookRead model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
         /// <summary>
        /// 更新终止订阅值:0为不中止,1为终止
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IsNotRead"></param>
        /// <returns></returns>
        public bool GetUpdate(int id, int IsNotRead)
        {
            string sqlStr = "UPDATE ZL_BookRead SET isNotRead=@IsNotRead WHERE id=@id";
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@IsNotRead",IsNotRead),
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, param);
        }

        /// <summary>
        /// 更新审核状态,0为未审核，1为审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isAudit"></param>
        /// <returns></returns>
        public bool GetUpdateByid(int id, int isAudit)
        {
            string sqlStr = "UPDATE ZL_BookRead SET isAudit=@isAudit WHERE id=@id";
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@isAudit",isAudit),
                new SqlParameter("@id",id)
            };
            return SqlHelper.ExecuteSql(sqlStr, param);
        }
    }
}

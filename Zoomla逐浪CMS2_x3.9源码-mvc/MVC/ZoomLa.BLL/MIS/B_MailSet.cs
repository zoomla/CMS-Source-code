using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
namespace ZoomLa.BLL
{
    public class B_MailSet
    {
        public B_MailSet()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        private string PK, strTableName;
        private M_MailSet initMod = new M_MailSet();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public DataTable SelByUid(int uid, int IsDefault = -100)
        {
            string where = "UserID=" + uid;
            if (IsDefault != -100) { where += " And IsDefault=" + IsDefault; }
            return Sql.Sel(strTableName, where, PK + " DESC");
        }
        public DataTable SelIsDefault(int isDefault)
        {
            return Sql.Sel(strTableName,"isDefault="+isDefault,PK+" DESC");
        }
        public M_MailSet SelReturnModel(int ID)
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
        private M_MailSet SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_MailSet model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), initMod.GetFieldAndPara(), initMod.GetParameters());
        }
        public bool UpdateStatus(int status, int id)
        {
            return Sql.Update(strTableName, "Status=" + status, "ID=" + id, null);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_MailSet model)
        {
            return Sql.insert(strTableName, initMod.GetParameters(), initMod.GetParas(), initMod.GetFields());
        }
    }
}

using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;
using System.Collections.Generic;
namespace ZoomLa.BLL
{
    public class B_Mis_Model
    {
        public M_Mis_Model initMod = new M_Mis_Model();
        public string PK = "", strTableName = "";

        public B_Mis_Model()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public M_Mis_Model SelReturnModel(int ID)
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
        public DataTable Sel(int doctype = -100, string skey = "")
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("skey", "%" + skey + "%") };
            string where = "1=1 ";
            if (doctype != -100) { where += " AND DocType=" + doctype; }
            if (!string.IsNullOrEmpty(skey)) { where += " AND ModelName LIKE @skey"; }
            return DBCenter.Sel(strTableName, where, "", sp);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelWordHead() 
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE WordPath IS NOT NULL AND WordPath !=''";
            return SqlHelper.ExecuteTable(CommandType.Text,sql);
        }
        public int insert(M_Mis_Model model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Mis_Model model)
        {
           return DBCenter.UpdateByID(model,model.ID);
        }
        public bool DelByModelID(int id)
        {
            SqlParameter[] sp = new SqlParameter[]{ new SqlParameter("id",id)};
            return Sql.Del(strTableName, "ID=@id", sp);
        }
        //----------------Tools
        public string GetDocType(int docType)
        {
            switch (docType)
            {
                case 0:
                    return "公文";
                case 1:
                    return "事务";
                case 2:
                    return "快速流程";
                default:
                    return docType.ToString();
            }
        }
    }
}

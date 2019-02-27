
namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using ZoomLa.Model;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Data.SqlClient;
    using ZoomLa.SQLDAL;
    using SQLDAL.SQL;
    public class B_Source
    {
        private string strTableName ,PK;
        private M_Source initMod = new M_Source();
        public B_Source()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Source SelReturnModel(int ID)
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_Source model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.SourceID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Source model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool Add(M_Source model)
        {
            return insert(model)>0;
        }
        public bool DeleteByID(string ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        public DataTable GetSourceAll()
        {
            return Sel();
        }
        public bool Update(M_Source model)
        {
            return UpdateByID(model);
        }
        public M_Source GetSourceByid(int ID)
        {
            return SelReturnModel(ID);
        }
        public DataTable SearchSource(string sourcekey)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("sourcekey", "%" + sourcekey + "%") };
            string str = "select * from [ZL_Source] where Name like @sourcekey";
            return SqlHelper.ExecuteTable(CommandType.Text, str, sp);
        }
    }
}

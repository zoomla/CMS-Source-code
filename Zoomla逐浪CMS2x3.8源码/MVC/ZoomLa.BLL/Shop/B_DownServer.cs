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
    public class B_DownServer
    {
        private string TbName, PK;
        private M_DownServer initMod = new M_DownServer();
        public B_DownServer() 
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_DownServer SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
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
        private M_DownServer SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, strWhere))
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
            return Sql.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_DownServer model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ServerID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_DownServer model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool Add(M_DownServer model)
        {
            Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            return true;
        }
        public bool DeleteByID(string ID)
        {
            return Sql.Del(TbName, ID);
        }
        public DataTable GetDownServerAll()
        {
            return Sql.Sel(TbName, "", "");
        }
        public bool Update(M_DownServer model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ServerID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public M_DownServer GetDownServerByid(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
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
        public int Max()
        {
            string strsql = "SELECT TOP 1 ServerID FROM [ZL_DownServer] ORDER BY ServerID DESC";
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strsql, null));
        }
    }
}

namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;

    public class B_RoomNotify
    {
        private string TbName, PK;
        private M_RoomNotify initMod = new M_RoomNotify();
        public B_RoomNotify()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_RoomNotify SelReturnModel(int ID)
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
        private M_RoomNotify SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_RoomNotify model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_RoomNotify model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool GetInsert(M_RoomNotify model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model))>0;
        }
        public bool GetUpdate(M_RoomNotify model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool InsertUpdate(M_RoomNotify model)
        {
            if (model.ID > 0)
                GetUpdate(model);
            else
                GetInsert(model);
            return true;
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(TbName, PK + "=" + ID);
        }
        public M_RoomNotify GetSelect(int ID)
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
        public DataTable SelByRoomID(int id)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE RoomID=" + id + " ORDER BY AddTime DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable Select_All()
        {
            return Sql.Sel(TbName, "", "");
        }
        public DataTable SelByValue(int RoomID) 
        {
            string strSql = "SELECT TOP 1 * FROM " + TbName + "WHERE RoomID = " + RoomID + " ORDER BY AddTime desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql);
        }
    }
}

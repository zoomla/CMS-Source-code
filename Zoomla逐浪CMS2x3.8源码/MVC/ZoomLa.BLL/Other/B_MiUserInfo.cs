namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_MiUserInfo
    {
        private string strTableName ,PK;
        private M_MiUserInfo initMod = new M_MiUserInfo();
        public B_MiUserInfo()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_MiUserInfo SelReturnModel(int ID)
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
        public bool UpdateByID(M_MiUserInfo model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.M_id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_MiUserInfo model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int GetInsert(M_MiUserInfo model)
        {
            return insert(model);
        }
        public bool GetUpdate(M_MiUserInfo model)
        {
            return UpdateByID(model);
        }
        public bool InsertUpdate(M_MiUserInfo model)
        {
            if (model.M_id > 0)
                UpdateByID(model);
            else
                insert(model);
            return true;
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }

        public M_MiUserInfo GetSelect(int ID)
        {
            return SelReturnModel(ID);
        }
        public M_MiUserInfo GetSelectUserID(int UserID)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            cmdParams[0].Value = UserID;
            string str = "select * from ZL_MiUserInfo where UserID=@UserID";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, str, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_MiUserInfo();
                }
            }
        }
        public DataTable Select_All()
        {
            return Sel();
        }

    }
}

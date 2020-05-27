namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_PageStyle
    {
        public B_PageStyle()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        private string strTableName ,PK;
        private M_PageStyle initMod = new M_PageStyle();
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
        public M_PageStyle SelReturnModel(int ID)
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
        public bool UpdateByID(M_PageStyle model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.PageNodeid.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_PageStyle model)
        {
            return Sql.insert(strTableName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool AddPagestyle(M_PageStyle info)
        {
            return Sql.insert(strTableName, info.GetParameters(info), BLLCommon.GetParas(info), BLLCommon.GetFields(info)) > 0;
        }
        public bool DelPagestyle(int ID)
        {
            return Sql.Del(strTableName, PK + "=" + ID);
        }
        public bool UpdatePagestyle(M_PageStyle info)
        {
            return Sql.UpdateByIDs(strTableName, PK, info.PageNodeid.ToString(), BLLCommon.GetFieldAndPara(info), info.GetParameters(info));
        }
        public M_PageStyle Getpagestrylebyid(int pagestyleid)
        {
            string sql = "Select * From " + strTableName + " Where PageNodeid="+pagestyleid;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text,sql))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_PageStyle();
                }
            }
        }
        public DataTable GetPagestylelist()
        {
            string str = "Select * From " + strTableName + " order by OrderID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, str);
        }
        public DataTable GetPagestyleTruelist()
        {
            string sql = "SELECT * FROM [ZL_PageStyle] where istrue=1 order by OrderID desc";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        public M_PageStyle GetDefaultStyle()
        {
            string sql = "select top 1 * from ZL_PageStyle where IsDefault=1";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_PageStyle();
                }
            }
        }
        public bool SetEnableByIds(string ids,int flag)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Update " + strTableName + " SET IsTrue=" + flag + " WHERE PageNodeid IN ("+ids+")";
            return SqlHelper.ExecuteSql(sql);
        }
    }
}
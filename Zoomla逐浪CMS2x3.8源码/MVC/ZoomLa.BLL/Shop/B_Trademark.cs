namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_Trademark
    {
        private string TbName, PK;
        private M_Trademark initMod = new M_Trademark();
        public B_Trademark()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
        public M_Trademark SelReturnModel(int ID)
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
        private M_Trademark SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_Trademark model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public int insert(M_Trademark model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public DataTable GetTrademarkAll()
        {
            string strSql = "select * from ZL_Trademark order by(id) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        public bool Add(M_Trademark model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model)) > 0;
        }
        public bool DeleteByID(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool DeleteByList(string str)
        {
            string[] strArr = str.Split(',');
            string sqlStr = "";
            SqlParameter[] sp = new SqlParameter[strArr.Length];
            for (int i = 0; i < strArr.Length; i++)
            {
                sp[i] = new SqlParameter("str" + i, strArr[i]);
                sqlStr += "@" + sp[i].ParameterName + ",";
            }
            sqlStr = sqlStr.TrimEnd(',');
            return SqlHelper.ExecuteSql("delete from ZL_Trademark where (id in (" + sqlStr + "))", sp);
        }
        public bool Update(M_Trademark model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public M_Trademark GetTrademarkByid(int ID)
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

        public bool Upotherdata(string str, string id)
        {
            int sid = DataConverter.CLng(id);
            string sqlStr = "";
            string strclass = str;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("sid", sid) };
            switch (strclass)
            {
                case "1":
                    sqlStr = "update ZL_Trademark set Isuse=0 where id=@sid";
                    break;
                case "2":
                    sqlStr = "update ZL_Trademark set Istop=1 where id=@sid";
                    break;
                case "3":
                    sqlStr = "update ZL_Trademark set Isbest=1 where id=@sid";
                    break;
                case "4":
                    sqlStr = "update ZL_Trademark set Isuse=1 where id=@sid";
                    break;
                case "5":
                    sqlStr = "update ZL_Trademark set Istop=0 where id=@sid";
                    break;
                case "6":
                    sqlStr = "update ZL_Trademark set Isbest=0 where id=@sid";
                    break;
                default:
                    sqlStr = "update ZL_Trademark set Producername=Producername where id=@sid";
                    break;
            }
            return SqlHelper.ExecuteSql(sqlStr, sp);
        }
        public DataTable GetTrademarkproducter(int Producer)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Producer", Producer) };
            string strSql = "select * from ZL_Trademark where Producer=@Producer and Isuse=1 order by(id) desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
    }
}
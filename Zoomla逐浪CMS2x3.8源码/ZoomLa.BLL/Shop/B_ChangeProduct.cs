
namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    public class B_ChangeProduct
    {
        private string strTableName, PK;
        private M_ChangeProduct initMod = new M_ChangeProduct();
        public B_ChangeProduct() 
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_ChangeProduct SelReturnModel(int ID)
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
        private M_ChangeProduct SelReturnModel(string strWhere)
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
        public bool UpdateByID(M_ChangeProduct model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.P_ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool DelProduct(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_ChangeProduct model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool Add(M_ChangeProduct card)
        {
            string strSql = "insert into ZL_ChangeProduct (prodID,ExChangeID,PState,num) values (@prodID,@ExChangeID,@PState,@num)";
            SqlParameter[] sp = GetParameters(card, 1);
            return SqlHelper.ExecuteSql(strSql, sp);
        }
        private static SqlParameter[] GetParameters(M_ChangeProduct Cashinfo, int type)
        {
            SqlParameter[] parameter = new SqlParameter[] { };
            switch (type)
            {
                case 1:
                    parameter = new SqlParameter[4];
                    parameter[0] = new SqlParameter("@prodID", SqlDbType.Int);
                    parameter[1] = new SqlParameter("@ExChangeID", SqlDbType.NVarChar, 200);
                    parameter[2] = new SqlParameter("@PState", SqlDbType.Money, 8);
                    parameter[3] = new SqlParameter("@num", SqlDbType.NVarChar, 200);
                    parameter[0].Value = Cashinfo.prodID;
                    parameter[1].Value = Cashinfo.ExChangeID;
                    parameter[2].Value = Cashinfo.PState;
                    parameter[3].Value = Cashinfo.num;
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
            return parameter;
        }
        public bool delid(int id)
        {
            string strSql = "DELETE FROM ZL_ChangeProduct WHERE ExChangeID=@id";

            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = id;

            return SqlHelper.ExecuteSql(strSql, cmdParams);
        }
        public bool Delall(string c_id)
        {
            string sqlStr = "DELETE FROM ZL_ChangeProduct WHERE ExChangeID in(" + c_id + ")";
            return SqlHelper.ExecuteSql(sqlStr, null);
        }
        public bool UpProductNum(M_ChangeProduct c)
        {
            string strSql = "Update ZL_ChangeProduct set num=@num Where P_ID=@P_ID";
            SqlParameter[] parameter = new SqlParameter[] { };
            parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@P_ID", SqlDbType.Int);
            parameter[1] = new SqlParameter("@num", SqlDbType.Int);
            parameter[0].Value = c.P_ID;
            parameter[1].Value = c.num;
            return SqlHelper.ExecuteSql(strSql, parameter);
        }

       public DataTable SelectAll(int ExChangeID, int type)
       {
           string strSql = "";
           switch (type)
           {
               case 1:
                   strSql = "select * from ZL_ChangeProduct where ExChangeID=@ExChangeID order by P_ID desc";
                   break;
               case 2:
                   strSql = "select * from ZL_ChangeProduct where ExChangeID=@ExChangeID and PState=0 order by P_ID desc";
                   break;
               case 3:
                   strSql = "select * from ZL_ChangeProduct where ExChangeID=@ExChangeID and PState=1 order by P_ID desc";
                   break;
               case 4:
                   strSql = "select ZL_ChangeProduct.*,ZL_UserShop.Proname as prodName,ZL_UserShop.LinPrice as price,ZL_UserShop.ShiPrice from ZL_ChangeProduct inner join ZL_UserShop on ZL_UserShop.ID=ZL_ChangeProduct.prodID where ExChangeID=@ExChangeID and PState=0 order by P_id desc";
                   break;
               case 5:
                   strSql = "select ZL_ChangeProduct.*,ZL_UserShop.Proname as prodName,ZL_UserShop.LinPrice as price,ZL_UserShop.ShiPrice from ZL_ChangeProduct inner join ZL_UserShop on ZL_UserShop.ID=ZL_ChangeProduct.prodID where ExChangeID=@ExChangeID and PState=1 order by P_id desc";
                   break;
               default:
                   strSql = "select * from ZL_ChangeProduct where ExChangeID=@ExChangeID order by P_ID desc";
                   break;
           }
           SqlParameter[] cmdParams = new SqlParameter[1];
           cmdParams[0] = new SqlParameter("@ExChangeID", SqlDbType.Int, 4);
           cmdParams[0].Value = ExChangeID;
           return SqlHelper.ExecuteTable(CommandType.Text, strSql, cmdParams);
       }
    }
}

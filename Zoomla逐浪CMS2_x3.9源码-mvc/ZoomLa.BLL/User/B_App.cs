using System;
using System.Collections.Generic;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_App
    {
        private string PK, TbName;
        private M_App initMod = new M_App();
        public B_App() 
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_App model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int Add(M_App model)
        {
            return Insert(model);
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool Update(M_App model)
        {
            return UpdateMapByMid(model);
        }
        public bool UpdateMapByMid(M_App model)
        {
            return Sql.UpdateByID(TbName, "ID", model.ID, BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public M_App Select(int ID)
        {
            string sqlStr = "select * from " + TbName + " where ID=@ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = ID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
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
        public DataTable SelByAppName(string name)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("AppName", name) };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + TbName + " Where AppName=@AppName", sp);
        }
        public DataTable SelBySite(string uid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uid", uid) };
            string sql = "SELECT A.*,B.ImageUrl FROM " + TbName + " A LEFT JOIN ZL_QrCode B ON A.ID=B.AppID WHERE UserID=@uid";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(TbName, PK, ID);
        }
    }
}

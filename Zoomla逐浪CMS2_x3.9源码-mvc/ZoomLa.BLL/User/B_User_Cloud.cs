using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;


namespace ZoomLa.BLL
{
    public class B_User_Cloud : ZL_Bll_InterFace<M_User_Cloud>
    {
        public string TbName, PK;
        public M_User_Cloud initMod = new M_User_Cloud();

        public B_User_Cloud()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool DelByFile(string guid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@guid",guid)};
            string sql = "DELETE FROM "+TbName+" WHERE Guid=@guid";
            return SqlHelper.ExecuteSql(sql, sp);
        }

        public int Insert(M_User_Cloud model)
        {
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }

        public M_User_Cloud SelReturnModel(string guid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("guid", guid) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " Where Guid=@guid", sp))
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
        public M_User_Cloud SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, "ID", ID))
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
        public bool UpdateByID(M_User_Cloud model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.Guid.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public DataTable SelByPath(string path,int userid)
        {
            string sql = "SELECT * FROM "+TbName+ " WHERE VPath=@url AND UserID=" + userid;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@url", path) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
    }
}

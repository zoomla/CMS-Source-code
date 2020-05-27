using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Model.Plat;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Plat
{
    public class B_Plat_Task:ZL_Bll_InterFace<M_Plat_Task>
    {
        public string TbName, PK;
        public M_Plat_Task initMod = new M_Plat_Task();
        public DataTable dt = null;
        public B_Plat_Task()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Plat_Task model)
        {
            return Sql.insert(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Plat_Task model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_Plat_Task SelReturnModel(int ID)
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
        public DataTable Sel() 
        {
            return Sql.Sel(TbName);
        }
        /// <summary>
        /// 自已有权限浏览或参与的任务,自建,负责,
        /// </summary>
        public DataTable SelHasAuth(int uid)
        {
            string sql = "Select * From " + TbName + " Where CreateUser=" + uid + " OR LeaderIDS Like @uid OR PartTakeIDS Like @uid";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uid", "%," + uid + ",%") };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public bool ChangeColor(int id,string color)
        {
            string sql = "Update "+TbName+" Set Color=@color Where ID=@id";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@color",color), new SqlParameter("@id",id)};
            return SqlHelper.ExecuteSql(sql,sp);
        }
        public void DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Delete from ZL_Plat_Task where ID in(" + ids + ")";
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
    }
}

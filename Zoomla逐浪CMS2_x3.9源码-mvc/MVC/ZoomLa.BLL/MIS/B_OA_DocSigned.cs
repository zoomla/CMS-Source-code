using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.MIS;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.MIS
{
    public class B_OA_DocSigned:ZL_Bll_InterFace<M_OA_DocSigned>
    {
        private string TbName, PK;
        private M_OA_DocSigned initMod = new M_OA_DocSigned();
        public B_OA_DocSigned() 
        {
            TbName = initMod.TbName; PK = initMod.PK;
        }
        public int Insert(M_OA_DocSigned model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        ///  同一公文不允许使用同一签章
        /// </summary>
        public int InsertNoDup(M_OA_DocSigned model)
        {
            string sql = "SELECT ID FROM " + TbName + " WHERE AppID=" + model.AppID + " AND SignID=" + model.SignID;
            if (SqlHelper.ExecuteTable(CommandType.Text, sql).Rows.Count == 0)
                return Insert(model);
            else return 0;
        }
        public bool UpdateByID(M_OA_DocSigned model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public M_OA_DocSigned SelReturnModel(int ID)
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
        public DataTable SelByAppID(int appid)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE AppID=" + appid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
    }
}

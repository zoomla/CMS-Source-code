using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Project;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Project
{
    public class B_Pro_Msg
    {
        private string PK, TbName;
        private M_Pro_Msg initMod = new M_Pro_Msg();
        public B_Pro_Msg() 
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Pro_Msg model)
        {
            return Sql.insertID(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Pro_Msg model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_Pro_Msg SelReturnModel(int ID)
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
    }
}

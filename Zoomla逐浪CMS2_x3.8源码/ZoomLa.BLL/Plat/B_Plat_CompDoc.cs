using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL
{
    public class B_Plat_CompDoc:ZL_Bll_InterFace<M_Plat_CompDoc>
    {
        public string TbName, PK;
        public M_Plat_CompDoc initMod = new M_Plat_CompDoc();
        public B_Plat_CompDoc()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Plat_CompDoc model)
        {
           return DBCenter.Insert(model);
        }

        public bool UpdateByID(M_Plat_CompDoc model)
        {
           return DBCenter.UpdateByID(model,model.ID);
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public M_Plat_CompDoc SelReturnModel(int ID)
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

        public System.Data.DataTable Sel()
        {
            return Sql.Sel(TbName, "", "");
        }
    }
}

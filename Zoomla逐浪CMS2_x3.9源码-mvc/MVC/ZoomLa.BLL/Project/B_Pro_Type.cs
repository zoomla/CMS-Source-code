using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Project;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Project
{
    public class B_Pro_Type : ZL_Bll_InterFace<M_Pro_Type>
    {
        private string PK, TbName;
        private M_Pro_Type initMod = new M_Pro_Type();
        public B_Pro_Type()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Pro_Type model)
        {
            return DBCenter.Insert(model);
        }

        public bool UpdateByID(M_Pro_Type model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }

        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }

        public M_Pro_Type SelReturnModel(int ID)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
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
            return DBCenter.Sel(TbName);
        }

        public M_Pro_Type SelByTypeName(string name)
        {
            string where = "Type = @name";
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("name", name) };
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where, "", sp))
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
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
    }
}

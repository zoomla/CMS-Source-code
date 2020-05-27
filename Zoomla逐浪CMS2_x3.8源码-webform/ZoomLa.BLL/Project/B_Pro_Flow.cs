using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZoomLa.Model.Project;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Project
{
    public class B_Pro_Flow : ZL_Bll_InterFace<M_Pro_Flow>
    {
        private string PK, TbName;
        private M_Pro_Flow initMod = new M_Pro_Flow();
        public B_Pro_Flow()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }

        public int Insert(M_Pro_Flow model)
        {
            return DBCenter.Insert(model);
        }

        public bool UpdateByID(M_Pro_Flow model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }

        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }

        public M_Pro_Flow SelReturnModel(int ID)
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}

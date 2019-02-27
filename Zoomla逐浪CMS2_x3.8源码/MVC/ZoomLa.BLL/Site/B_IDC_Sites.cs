using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Site
{
    public class B_IDC_Sites:ZL_Bll_InterFace<M_IDC_Sites>
    {
        public string TbName, PK;
        private M_IDC_Sites initMod = new M_IDC_Sites();

        public B_IDC_Sites()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }

        public int Insert(M_IDC_Sites model)
        {
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_IDC_Sites model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }

        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public M_IDC_Sites SelReturnModel(int ID)
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
            return Sql.Sel(TbName, "", "");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}

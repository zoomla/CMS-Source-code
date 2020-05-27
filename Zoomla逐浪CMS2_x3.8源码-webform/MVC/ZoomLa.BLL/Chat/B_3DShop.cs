using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ZoomLa.Model.Chat;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_3DShop : ZL_Bll_InterFace<M_3DShop>
    {
        private M_3DShop initMod=new M_3DShop();
        public string TbName = "ZL_3DShop";
        public string PK = "ID";
        public DataTable Sel() 
        {
            return DBCenter.Sel(TbName);
        }
        public M_3DShop SelReturnModel(int ID)
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
        public int Insert(M_3DShop model)
        {
            //return Sql.insert(TbName, model.GetParameters(model), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_3DShop model)
        {
            //return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters(model));
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            string where = "1=1 ";
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}

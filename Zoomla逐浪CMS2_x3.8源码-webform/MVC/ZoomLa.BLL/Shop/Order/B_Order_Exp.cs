using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Shop
{
    public class B_Order_Exp : ZL_Bll_InterFace<M_Order_Exp>
    {
        private string PK, TbName = "";
        private M_Order_Exp initMod = new M_Order_Exp();
        public B_Order_Exp()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Order_Exp model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Order_Exp model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public void DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.DelByIDS(TbName, PK, ids);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", PK + " ASC");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Order_Exp SelReturnModel(int ID)
        {
            if (ID < 1) { return null; }
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
    }
}

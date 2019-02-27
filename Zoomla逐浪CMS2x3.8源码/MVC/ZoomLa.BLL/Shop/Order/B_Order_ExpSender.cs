using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Shop.Order;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Shop.Order
{
    public class B_Order_ExpSender : ZL_Bll_InterFace<M_Order_ExpSender>
    {
        private string PK, TbName = "";
        private M_Order_ExpSender initMod = new M_Order_ExpSender();
        public B_Order_ExpSender()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Order_ExpSender model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Order_ExpSender model)
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
        public DataTable Sel(string skey)
        {
            string where = "1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(skey)) { where += " AND Name LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return DBCenter.Sel(TbName, where, "ID DESC", sp);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public M_Order_ExpSender SelReturnModel(int ID)
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
        public bool SetDefault(int id)
        {
            DBCenter.UpdateSQL(TbName, "IsDefault=0", "");
            return DBCenter.UpdateSQL(TbName, "IsDefault=1", "ID=" + id);
        }
    }
}

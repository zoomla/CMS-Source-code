using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Shop
{
    public class B_Shop_FareTlp : ZL_Bll_InterFace<M_Shop_FareTlp>
    {
        public string TbName, PK;
        public M_Shop_FareTlp initMod = new M_Shop_FareTlp();
        public B_Shop_FareTlp()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_Shop_FareTlp model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Shop_FareTlp model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName,ID);
        }
        public M_Shop_FareTlp SelReturnModel(int ID)
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
        public DataTable U_SelByUid(int uid)
        {
            string sql = "SELECT * FROM " + initMod.TbName + " WHERE UserID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public PageSetting SelPage(int cpage, int psize, int uid)
        {
            string where = "1=1 ";
            if (uid > 0) { where += " AND UserID=" + uid; }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
        public void DelByIDS(string ids, int uid = 0)
        {
            if (string.IsNullOrEmpty(ids)) return;
            SafeSC.CheckIDSEx(ids);
            string where = PK + " IN (" + ids + ") ";
            if (uid > 0) { where += " AND UserID=" + uid; }
            DBCenter.DelByWhere(TbName, where);
        }
    }
}

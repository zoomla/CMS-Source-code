using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL.Helper;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Shop
{
    public class B_Order_Repair : ZL_Bll_InterFace<M_Order_Repair>
    {
        public string TbName, PK;
        public M_Order_Repair initMod = new M_Order_Repair();
        public B_Order_Repair()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }

        public int Insert(M_Order_Repair model)
        {
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }

        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "CDATE DESC");
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "", "CDate DESC");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelAll()
        {
            string sql = "SELECT A.*,B.Proname,B.Thumbnails FROM " + TbName + " A LEFT JOIN ZL_Commodities B ON A.ProID=B.ID WHERE ProID>0 ORDER BY A.CDATE DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public PageSetting U_SelAll(int cpage, int psize, int uid)
        {
            string where = "A.UserID=" + uid;
            PageSetting setting = PageSetting.Double(cpage, psize, TbName, "ZL_Commodities", "A." + PK, "A.ProID=B.ID", where, "A.CDate DESC");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateStatuByIDS(string ids, int status)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "UPDATE " + TbName + " SET [Status]=" + status + " WHERE ID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + TbName + " WHERE ID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public M_Order_Repair SelReturnModel(int ID)
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

        public bool UpdateByID(M_Order_Repair model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.PK, BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
    }
}

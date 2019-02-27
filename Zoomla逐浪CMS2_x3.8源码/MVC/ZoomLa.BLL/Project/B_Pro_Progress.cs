using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Project
{
    public class B_Pro_Progress
    {
        private string PK, TbName;
        private M_Pro_Progress initMod = new M_Pro_Progress();
        public B_Pro_Progress()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Pro_Progress model)
        {
            if (model.OrderID == 0) { model.OrderID = GetMaxOrder(model.ProID); }
            return Sql.insertID(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Pro_Progress model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_Pro_Progress SelReturnModel(int ID)
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
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelByProID(int proid)
        {
            return SqlHelper.ExecuteTable("SELECT * FROM " + TbName + " WHERE ProID=" + proid + " ORDER BY OrderID");
        }
        //--------Tools
        public int GetMaxOrder(int proid)
        {
            DataTable dt = SqlHelper.ExecuteTable("SELECT COUNT(*) FROM " + TbName + " WHERE ProID=" + proid);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        public int GetComplete(int proid)
        {
            if (proid < 1) { return 0; }
            string field = "SELECT COUNT(*) FROM " + TbName + " WHERE ProID = " + proid + " AND ZStatus = 1";
            string sql = "SELECT COUNT(*) CNum,(" + field + ") Dided FROM " + TbName + " WHERE ProID=" + proid;
            DataTable dt = SqlHelper.ExecuteTable(sql);
            return Convert.ToInt32((Convert.ToDouble(dt.Rows[0]["Dided"]) / Convert.ToDouble(dt.Rows[0]["CNum"])) * 100);
        }
    }
}

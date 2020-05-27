using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;

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
        //----------------------------Tools
        public DataTable ExpCompDT()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("id", typeof(int));
            string[] compArr = "顺风快递,中通快递,申通快递,圆通快递,韵达快递,百世汇通,韵达快递,天天快递,宅急送,全峰快递,EMS,UPS,其它".Split(',');
            for (int i = 0; i < compArr.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = (i + 1);
                dr["name"] = compArr[i];
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}

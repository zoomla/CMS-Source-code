using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Design
{
    public class B_Design_Question
    {
        private M_Design_Question initMod = new M_Design_Question();
        private string TbName, PK;
        public B_Design_Question()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Design_Question model)
        {
            if (model.OrderID == 0)
            {
                model.OrderID = DataConvert.CLng(DBCenter.ExecuteScala(TbName, "MAX(OrderID)", "AskID=" + model.AskID)) + 1;
            }
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public M_Design_Question SelReturnModel(int ID)
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
        public bool UpdateByID(M_Design_Question model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public DataTable Sel(int askID, string order = "")
        {
            string where = " 1=1 ";
            if (askID != 0) { where += " AND AskID=" + askID; }
            switch (order)
            {
                case "qlist":
                    order = "OrderID ASC";
                    break;
                default:
                    order = "ID DESC";
                    break;
            }
            return DBCenter.Sel(TbName, where, order);
        }
        public bool Move(int from, int target)
        {
            if (from == 0 || target == 0) { return false; }
            int fromOrder = Convert.ToInt32(DBCenter.ExecuteScala(TbName, "OrderID", "ID=" + from));
            int targetOrder = Convert.ToInt32(DBCenter.ExecuteScala(TbName, "OrderID", "ID=" + target));
            DBCenter.UpdateSQL(TbName, "OrderID=" + targetOrder, "ID=" + from);
            DBCenter.UpdateSQL(TbName, "OrderID=" + fromOrder, "ID=" + target);
            return true;
        }
        public string GetQType(string type)
        {
            switch (type)
            {
                case "radio":
                    return "单选";
                case "checkbox":
                    return "多选";
                case "blank":
                    return "填空";
                case "score":
                    return "评分";
                case "sort":
                    return "排序";
                default:
                    return "未知[" + type + "]";
            }
        }
        //--------------------------
        public M_Design_Question U_SelModel(int uid, int id)
        {
            string where = "CUser=" + uid + " AND ID=" + id;
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, where))
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
        public void U_Del(int uid, int id)
        {
            DBCenter.DelByWhere(TbName, "ID=" + id + " AND CUser=" + uid);
        }
    }
}

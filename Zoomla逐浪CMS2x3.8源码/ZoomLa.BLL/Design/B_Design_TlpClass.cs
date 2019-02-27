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
    public class B_Design_TlpClass : B_Base<M_Design_TlpClass>
    {
        private string PK, TbName = "";
        private M_Design_TlpClass initMod = new M_Design_TlpClass();
        public B_Design_TlpClass()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Design_TlpClass model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Design_TlpClass model)
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
        //public DataTable SelWith()
        //{
        //    string where = "";
        //    return DBCenter.SelWithField(TbName, "", where, PK + " DESC");
        //}
        public M_Design_TlpClass SelReturnModel(int ID)
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
        /// <summary>
        /// 返回由0开始的深度(0=除根目录外的第一级目录)
        /// </summary>
        public int GetDepth(int pid)
        {
            if (pid == 0) { return 0; }
            string tree = "";
            SelFirstNodeID(pid, ref tree);
            return tree.Trim(',').Split(',').Length;
        }
        public int SelFirstNodeID(int id, ref string nodeTree)
        {
            if (id < 1) { return 0; }
            string sql = "with f as(SELECT * FROM " + TbName + " WHERE ID=" + id + " UNION ALL SELECT A.* FROM " + TbName + " A, f WHERE a.ID=f.PID) SELECT * FROM " + TbName + " WHERE ID IN(SELECT ID FROM f)";
            DataTable dt = DBCenter.ExecuteTable(sql);
            if (dt.Rows.Count < 1) { return 0; }
            foreach (DataRow dr in dt.Rows)
            {
                nodeTree += dr["ID"] + ",";
            }
            nodeTree = nodeTree.Trim(',');
            return DataConvert.CLng(dt.Rows[0]["ID"]);
        }
        public DataTable SelByTlpType(int type)
        {
            string ids = "(SELECT ClassID FROM ZL_Design_tlp WHERE ClassID <> 0 AND ZType = " + type + " GROUP BY ClassID)";
            return DBCenter.Sel(TbName, "ID IN " + ids, PK + " ASC");
        }
    }
}

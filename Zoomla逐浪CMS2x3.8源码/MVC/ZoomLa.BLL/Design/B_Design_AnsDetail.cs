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
    public class B_Design_AnsDetail
    {
        private M_Design_AnsDetail initMod = new M_Design_AnsDetail();
        private string TbName, PK;
        public B_Design_AnsDetail()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Design_AnsDetail model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public DataTable Sel(string qid, int uid = -100, int askid = -100, int ansid = -100)
        {
            string where = " 1=1 ";
            if (uid != -100) { where += " AND UserID=" + uid; }
            if (askid != -100) { where += " AND AskID=" + askid; }
            if (ansid != -100) { where += " AND AnsID=" + ansid; }
            if (!string.IsNullOrEmpty(qid)) { SafeSC.CheckDataEx(qid); where += " AND Qid IN (" + qid + ")"; }
            return DBCenter.JoinQuery("A.*,B.QType,B.QTitle,B.QOption,B.QFlag,B.Required", TbName, "ZL_Design_Question", "A.Qid=B.ID", where, "A.ID DESC");
        }
        public M_Design_AnsDetail SelReturnModel(int ID)
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
        public bool UpdateByID(M_Design_AnsDetail model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
    }
}

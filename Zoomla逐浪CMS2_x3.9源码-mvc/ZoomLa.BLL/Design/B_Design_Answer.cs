using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Design
{
    public class B_Design_Answer
    {
        private M_Design_Answer initMod = new M_Design_Answer();
        public string TbName, PK;
        public B_Design_Answer()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Design_Answer model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        //是否仅用于list
        public DataTable Sel(int uid, int askid, string skey = "")
        {
            string where = " 1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (uid != -100) { where += " AND A.UserID=" + uid; }
            if (askid != -100 && askid != 0) { where += " AND A.AskID=" + askid; }
            if (!string.IsNullOrEmpty(skey))
            {

                where += " AND (B.UserName LIKE @username";
                if (DataConvert.CLng(skey) > 0) { where += " OR A.ID = " + skey; }
                where += ")";
                sp.Add(new SqlParameter("username", "%" + skey + "%"));
            }
            string stable = "(SELECT A.*,B.UserName FROM ZL_Design_Ask A LEFT JOIN ZL_User B ON A.CUser=B.UserID)";
            return DBCenter.JoinQuery("A.*,B.Title,B.Remind,B.UserName", TbName, stable, "A.AskID=B.ID", where, "A.ID DESC", sp.ToArray());
        }
        public M_Design_Answer SelReturnModel(int ID)
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
        public bool UpdateByID(M_Design_Answer model)
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

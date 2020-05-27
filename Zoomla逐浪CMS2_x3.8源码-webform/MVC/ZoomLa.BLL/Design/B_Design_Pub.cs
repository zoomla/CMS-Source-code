using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL.Helper;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Design
{
    public class B_Design_Pub : ZL_Bll_InterFace<M_Design_Pub>
    {
        private M_Design_Pub initMod = new M_Design_Pub();
        private string TbName, PK;
        public B_Design_Pub()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Design_Pub model)
        {
            if (string.IsNullOrEmpty(model.IP)) { model.IP = IPScaner.GetUserIP(); }
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public DataTable Sel(string h5id, int uid,string fname,string skey)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "1=1 ";
            if (!string.IsNullOrEmpty(h5id)) { where += " AND A.H5ID=@h5id"; sp.Add(new SqlParameter("h5id", h5id)); }
            if (uid != -100) { where += " AND UserID ='" + uid + "'"; }
            if (!string.IsNullOrEmpty(fname)) { where += " AND A.FormName=@fname"; sp.Add(new SqlParameter("fname",fname)); }
            if (!string.IsNullOrEmpty(skey)) { where += " AND (A.FormName LIKE @skey OR B.Title LIKE @skey)"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return DBCenter.JoinQuery("A.*,B.Title", TbName, "ZL_Design_Scence", "A.H5ID=B.Guid", where, "ID DESC", sp.ToArray());
        }
        public M_Design_Pub SelReturnModel(int ID)
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
        public bool UpdateByID(M_Design_Pub model)
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

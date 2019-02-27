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
    public class B_Design_Album:ZL_Bll_InterFace<M_Design_Album>
    {
        private M_Design_Album initMod = new M_Design_Album();
        private string TbName, PK;
        public static string TlpDir = "/design/album/tlps/";
        public B_Design_Album()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Design_Album model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public DataTable Sel(int uid, string skey)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = " 1=1 ";
            if (uid != -100) { where += " AND UserID=" + uid; }//过滤游客
            if (!string.IsNullOrEmpty(skey)) { where += " AND AlbumName LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return DBCenter.Sel(TbName, where, "ID DESC", sp);
        }
        public M_Design_Album SelReturnModel(int ID)
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
        public bool UpdateByID(M_Design_Album model)
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

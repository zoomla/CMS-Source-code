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
    public class B_Design_Ask : ZL_Bll_InterFace<M_Design_Ask>
    {
        private M_Design_Ask initMod = new M_Design_Ask();
        private string TbName, PK;
        public B_Design_Ask()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public int Insert(M_Design_Ask model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        public DataTable Sel(string title)
        {
            string where = " 1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(title)) { where += " AND Title LIKE @title"; sp.Add(new SqlParameter("title", "%" + title + "%")); }
            return DBCenter.Sel(TbName, where, "ID DESC", sp);
        }
        public DataTable Sel(int uid)
        {
            string where = " 1=1 ";
            if (uid != -100) { where += " AND CUser=" + uid; }//过滤游客
            return DBCenter.Sel(TbName, where, "ID DESC");
        }
        public M_Design_Ask SelReturnModel(int ID)
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
        public bool UpdateByID(M_Design_Ask model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
        //------------------------------------
        public M_Design_Ask U_SelModel(int uid, int id)
        {
            string where = " CUser=" + uid + " AND ID=" + id;
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
            DBCenter.DelByWhere(TbName, "CUser=" + uid + " AND ID=" + id);
        }
    }
}

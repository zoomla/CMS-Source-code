using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Other;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Other
{
    public class B_WX_RedDetail
    {
        private string TbName, PK;
        private M_WX_RedDetail initMod = new M_WX_RedDetail();
        public B_WX_RedDetail()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        /// <summary>
        /// 数据筛选
        /// </summary>
        /// <param name="mainID">所属红包ID</param>
        public DataTable Sel(int mainID, string skey)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "MainID=" + mainID;
            if (!string.IsNullOrEmpty(skey)) { where += " AND RedCode LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            return DBCenter.Sel(TbName, where, "", sp);
        }
        public int Insert(M_WX_RedDetail model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_WX_RedDetail model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
        public M_WX_RedDetail SelReturnModel(int ID)
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
        public M_WX_RedDetail SelModelByCode(string code)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("code", code) };
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, "RedCode=@code", "", sp))
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
    }
}

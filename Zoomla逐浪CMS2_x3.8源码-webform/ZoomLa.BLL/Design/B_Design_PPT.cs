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
    public class B_Design_PPT
    {
        private string PK, TbName = "ZL_Design_PPT";
        private M_Design_Page initMod = new M_Design_Page();
        public B_Design_PPT()
        {
            PK = initMod.PK;
        }
        public int Insert(M_Design_Page model)
        {
            model.TbName = TbName;
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Design_Page model)
        {
            model.TbName = TbName;
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public DataTable Sel(int uid)
        {
            string where = "1=1 ";
            if (uid > 0)
            {
                where += " AND CUser=" + uid;
            }
            return DBCenter.Sel(TbName, where, PK + " DESC");
        }
        /// <summary>
        /// 根据场景状态查询,默认查询未删除的场景
        /// </summary>
        public DataTable SelModelByStatus(string skey = "", int status = -100)
        {
            string where = "A.ZType = 0 AND TlpID = 0 AND ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (status == -100) { where += "A.status <> -2 "; }
            else { where += "A.status = " + status; }
            if (!string.IsNullOrEmpty(skey))
            {
                where += " AND (A.Title LIKE @skey OR B.UserName LIKE @skey) ";
                sp.Add(new SqlParameter("skey", "%" + skey + "%"));
            }
            string fields = "A.*,B.UserName,B.UserName UName,B.Permissions TrueName,B.HoneyName";
            return SqlHelper.JoinQuery(fields, TbName, "ZL_User", "A.UserID = B.UserID", where, PK + " DESC", sp.ToArray());
        }
        public M_Design_Page SelReturnModel(int ID)
        {
            if (ID < 1) { return null; }
            return SelModelByWhere(TbName, PK + "=" + ID, null);
        }
        public M_Design_Page SelModelByGuid(string guid)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("guid", guid) }; ;
            string where = "Guid=@guid";
            return SelModelByWhere(TbName, where, sp);
        }
        public M_Design_Page SelModelByDef(int uid)
        {
            string where = "UserID=" + uid;
            return SelModelByWhere(TbName, where, null);
        }
        public M_Design_Page SelModelByTlp(int tlpid)
        {
            string where = "TlpID=" + tlpid;
            return SelModelByWhere(TbName, where, null);
        }
        private M_Design_Page SelModelByWhere(string tbname, string where, List<SqlParameter> sp)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(tbname, where, PK + " DESC", sp))
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

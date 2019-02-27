using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.BLL.Message;
using System.Data.Common;
using ConStatus = ZoomLa.Model.ZLEnum.ConStatus;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_Baike
    {
        private string PK, TbName;
        private M_Baike initMod = new M_Baike();
        public B_Baike()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public M_Baike SelReturnModel(int ID)
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
        public M_Baike SelModelByFlow(string flow)
        {
            if (string.IsNullOrEmpty(flow)) return null;
            string sql = "WHERE Flow=@Flow";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("flow", flow.Trim()) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, sql, sp))
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
        public M_Baike SelByTittle(string tittle)
        {
            string sql = "WHERE Tittle=@tittle";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("tittle", tittle) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, sql, sp))
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
        public bool UpdateByID(M_Baike model)
        {
            model.TbName = initMod.TbName;
            return DBCenter.UpdateByID(model, model.ID);
        }
        public int insert(M_Baike model)
        {
            return DBCenter.Insert(model);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="status">词条状态：0未审核，1通过</param>
        /// <param name="title">词条名称</param>
        /// <param name="btype">词条标签</param>
        /// <param name="bkclass">词条所属类别</param>
        /// <returns></returns>
        public PageSetting SelPage(int cpage, int psize, int status = -100, string title = "", string btype = "", int uid = -100, string bkclass = "")
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (status != -100) { where += " AND Status=" + status; }
            if (!string.IsNullOrEmpty(title)) { where += " AND Tittle LIKE @title"; sp.Add(new SqlParameter("title", "%" + title + "%")); }
            if (!string.IsNullOrEmpty(btype)) { where += " AND btype LIKE @btype"; sp.Add(new SqlParameter("btype", "%" + btype + "%")); }
            if (uid != -100) { where += " AND UserID=" + uid; }
            if (!string.IsNullOrEmpty(bkclass)) { where += " ANd bkclass=@bkclass"; sp.Add(new SqlParameter("bkclass", bkclass)); }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 根据词条名,标签,类别来搜索数据,用于替代其他所有搜索
        /// </summary>
        /// <param name="title">词条名称</param>
        /// <param name="btype">词条标签</param>
        /// <param name="bkclass">词条所属类别</param>
        public PageSetting SelByInfo(int cpage, int psize, string title, string btype, int uid, string bkclass = "")
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = " Status=1 ";
            if (!string.IsNullOrEmpty(title)) { where += " AND Tittle LIKE @title"; sp.Add(new SqlParameter("title", "%" + title + "%")); }
            if (!string.IsNullOrEmpty(btype)) { where += " AND btype LIKE @btype"; sp.Add(new SqlParameter("btype", "%" + btype + "%")); }
            if (uid > 0) { where += " AND UserID=" + uid; }
            if (!string.IsNullOrEmpty(bkclass)) { where += " AND bkclass=@bkclass"; sp.Add(new SqlParameter("bkclass", bkclass)); }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, PK + " DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable U_SelMy(int uid, string title, int status)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = " UserID=" + uid;
            if (!string.IsNullOrEmpty(title))
            {
                sp.Add(new SqlParameter("title", "%" + title + "%"));
                where += " AND Tittle LIKE @title";
            }
            if (status != -100)//0:未审,1:已审,-2:拒绝,-100抽出全部
            {
                where += " AND Status=" + status;
            }
            return DBCenter.Sel(TbName, where, "ID DESC", sp);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
        //----------------------------------
        /// <summary>
        /// 用于会员组权限检测,为空代表不限制
        /// </summary>
        public bool AuthCheck(string authStr, int groupID)
        {
            if (string.IsNullOrEmpty(authStr)) { return true; }
            return ("," + authStr + ",").Contains("," + groupID + ",");
        }
        //----------------------------------------------------------------
        public DataTable SelectAll(int status = -100, string key = "")
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = " 1=1 ";
            if (status != -100)
            {
                where += " AND Status=" + status;
            }
            if (!string.IsNullOrEmpty(key))
            {
                where += " AND A.Tittle LIKE @key";
                sp.Add(new SqlParameter("key", "%" + key + "%"));
            }
            return SqlHelper.JoinQuery("A.*,B.GradeName", TbName, "ZL_Grade", "A.BType=B.GradeName", where, "AddTime DESC", sp.ToArray());
        }
        /// <summary>
        /// Default Page Used
        /// </summary>
        public DataTable SelectSee(int count, string Where, string strOrderby)
        {
            return DBCenter.SelTop(count, PK, "*", TbName, Where, strOrderby);
        }
        public DataTable SelBy(string keyWord, int flag = 0)
        {
            string sql = "";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("keyWord", keyWord) };
            switch (flag)
            {
                case 0:
                    sql = "Select * From " + TbName + " Where Status>0 and Classification=@keyWord Order By ID DESC";
                    break;
                case 1:
                    sql = "Select * From " + TbName + " Where Tittle=@keyWord And (status=1 or status=3) Order By ID DESC";
                    break;
            }
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public bool UpdateStatus(int status, int id)
        {
            return DBCenter.UpdateSQL(TbName, "Status=" + status, "ID=" + id);
        }
        public bool UpdateElite(int elite, int id)
        {
            return DBCenter.UpdateSQL(TbName, "Elite=" + elite, "ID=" + id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_Questions_Knowledge
    {
        public string strTableName, PK;
        M_Questions_Knowledge initMod = new M_Questions_Knowledge();
        public B_Questions_Knowledge()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        /// <summary>
        /// 筛选数据
        /// </summary>
        /// <param name="pid">-1:全部</param>
        /// <param name="status">-1:全部</param>
        public DataTable Select_All(int nid, int pid = -1, int status = -1, string knowname = "")
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", "%" + knowname + "%") };
            string wherestr = " 1=1 ";
            if (!string.IsNullOrEmpty(knowname))
            {
                wherestr += "AND A.k_name LIKE @name";
            }
            if (status != -1)
            {
                wherestr += " AND A.Status=" + status;
            }
            if (pid > -1) { wherestr += " AND A.Pid=" + pid; }
            string sql = "SELECT A.*,B.GradeName,C.C_ClassName,D.UserName,(SELECT COUNT(*) FROM " + strTableName + " WHERE Pid=A.k_id) AS ChildCount FROM " + strTableName + " A "
                        + "LEFT JOIN ZL_Grade B ON A.Grade=B.GradeID "
                        + "LEFT JOIN ZL_Exam_Class C ON A.k_Class_id=C.C_id "
                        + "LEFT JOIN ZL_User D ON A.CUser=D.UserID WHERE " + wherestr;
            if (nid > 0) { sql += " AND k_Class_id=" + nid; }
            sql += " ORDER BY CDate DESC";
            return SqlHelper.ExecuteTable(sql, sp);
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public M_Questions_Knowledge SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
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
        private M_Questions_Knowledge SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
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
        public PageSetting SelPage(int cpage, int psize, int cid = -100, string name = "")
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (cid != -100) { where += " AND k_class_id=" + cid; }
            if (!string.IsNullOrEmpty(name)) { where += " AND K_name=@name"; sp.Add(new SqlParameter("name", name)); }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        //按名称查询
        public DataTable SelByName(int nid, string name)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name) };
            string sql = "SELECT * FROM " + strTableName + " WHERE k_class_id=" + nid + " AND K_name=@name";
            return SqlHelper.ExecuteTable(sql, sp);
        }
        public DataTable SelByIDS(string ids)
        {
            if (!SafeSC.CheckIDS(ids)) { return new DataTable(); }
            string sql = "SELECT * FROM " + strTableName + " WHERE k_id IN (" + ids + ")";
            return SqlHelper.ExecuteTable(sql);
        }
        public string GetNamesByIDS(string ids)
        {
            if (!SafeSC.CheckIDS(ids)) { return ""; }
            string sql = "SELECT * FROM " + strTableName + " WHERE k_id IN (" + ids + ")";
            DataTable dt = SqlHelper.ExecuteTable(sql);
            string names = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                names += dt.Rows[i]["k_name"].ToString() + ",";
            }
            return names.Trim(',');
        }
        public bool GetUpdate(M_Questions_Knowledge model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.K_id.ToString(), BLLCommon.GetFieldAndPara(model), initMod.GetParameters());
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + strTableName + " WHERE k_id IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public int insert(M_Questions_Knowledge model)
        {
            return Sql.insertID(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        private SqlParameter[] GetParam(M_Questions_Knowledge mqk)
        {
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@k_name",mqk.K_name),
                new SqlParameter("@k_class_id",mqk.K_class_id),
                new SqlParameter("@k_OrderBy",mqk.K_OrderBy),
                new SqlParameter("@k_id",mqk.K_id)
            };
            return para;
        }
        public M_Questions_Knowledge GetSelectById(int k_id)
        {
            string sql = "SELECT [k_id],[k_name],[k_class_id],[k_OrderBy] FROM ZL_Questions_Knowledge WHERE K_id=" + k_id;
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
            List<M_Questions_Knowledge> mqks = GetKByds(ds);
            if (mqks != null && mqks.Count > 0)
            {
                return mqks[0];
            }
            else
            {
                return new M_Questions_Knowledge();
            }
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public List<M_Questions_Knowledge> GetSelectAll()
        {
            string sql = "SELECT [k_id],[k_name],[k_class_id],[k_OrderBy] FROM ZL_Questions_Knowledge";
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
            List<M_Questions_Knowledge> mqks = GetKByds(ds);
            if (mqks != null && mqks.Count > 0)
            {
                return mqks;
            }
            else
            {
                return new List<M_Questions_Knowledge>();
            }
        }
        /// <summary>
        /// 通过名称和分类ID查询知识点
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public M_Questions_Knowledge GetSelectByNameAndCid(string name, int cid)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", name) };
            string sql = "SELECT [k_id],[k_name],[k_class_id],[k_OrderBy] FROM ZL_Questions_Knowledge WHERE k_name=@name AND k_class_id=" + cid;
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql, sp);
            List<M_Questions_Knowledge> mqks = GetKByds(ds);
            if (mqks != null && mqks.Count > 0)
            {
                return mqks[0];
            }
            else
            {
                return new M_Questions_Knowledge();
            }
        }
        private List<M_Questions_Knowledge> GetKByds(DataSet ds)
        {
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<M_Questions_Knowledge> mqks = new List<M_Questions_Knowledge>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    M_Questions_Knowledge mqk = new M_Questions_Knowledge();
                    mqk.K_id = DataConverter.CLng(dr["k_id"]);
                    mqk.K_name = dr["k_name"].ToString();
                    mqk.K_class_id = DataConverter.CLng(dr["k_class_id"]);
                    mqk.K_OrderBy = DataConverter.CLng(dr["k_orderBy"]);
                    mqks.Add(mqk);
                }
                return mqks;
            }
            return new List<M_Questions_Knowledge>();
        }
        /// <summary>
        /// 根据分类ID查询所有知识点
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<M_Questions_Knowledge> GetSelectByCid(int cid)
        {
            string sql = "SELECT [k_id],[k_name],[k_class_id],[k_OrderBy] FROM ZL_Questions_Knowledge WHERE [k_class_id]=" + cid;
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, sql);
            List<M_Questions_Knowledge> mqks = GetKByds(ds);
            if (mqks != null && mqks.Count > 0)
            {
                return mqks;
            }
            else
            {
                return new List<M_Questions_Knowledge>();
            }
        }
        //-----------------------
        /// <summary>
        /// 用于添加与修改页面,增加新的知识点(过滤重复)
        /// </summary>
        /// <param name="knows">需要增加的知识点</param>
        /// <param name="nodeid">知识点ID</param>
        public string AddKnows(int nodeid, string knows, int uid = 0, bool isadd = true)
        {
            string ids = "";
            knows = knows.Trim(',').Replace(" ", "");
            if (string.IsNullOrEmpty(knows)) return ids;
            string[] knowArr = knows.Split(',');
            string paramStr = "";
            SqlParameter[] sp = new SqlParameter[knowArr.Length];
            for (int i = 0; i < knowArr.Length; i++)
            {
                sp[i] = new SqlParameter("k" + i, knowArr[i]);
                paramStr += "@k" + i + ",";
            }
            paramStr = paramStr.TrimEnd(',');
            string sql = "SELECT k_id,k_name FROM " + strTableName + " WHERE k_class_id=" + nodeid + " AND k_name IN(" + paramStr + ")";
            DataTable knowdt = SqlHelper.ExecuteTable(sql, sp);
            for (int i = 0; i < knowArr.Length; i++)
            {
                DataRow[] drs = knowdt.Select("k_name='" + knowArr[i] + "'");
                if (drs.Length < 1 && isadd)
                {
                    int id = insert(new M_Questions_Knowledge()
                    {
                        K_name = knowArr[i],
                        K_class_id = nodeid,
                        CUser = uid,
                        Status = 1
                    });
                    ids += id + ",";
                }
                else { ids += drs[0]["k_id"] + ","; }
            }
            return ids.TrimEnd(',');
        }
    }
}

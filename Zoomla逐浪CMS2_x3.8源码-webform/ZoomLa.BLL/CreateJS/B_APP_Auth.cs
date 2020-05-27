using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model.CreateJS;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.CreateJS
{
    public class B_APP_Auth : ZL_Bll_InterFace<M_APP_Auth>
    {
        private string PK, TbName = "";
        private M_APP_Auth initMod = new M_APP_Auth();
        public B_APP_Auth()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_APP_Auth model)
        {
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_APP_Auth model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_APP_Auth SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, PK, ID))
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
        public M_APP_Auth SelModelByKey(string key)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", key) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE AuthKey=@key", sp))
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
        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "CDate Desc");
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="cpage"></param>
        /// <param name="psize"></param>
        /// <param name="filter">1;未授权,2:已授权,其它:全部</param>
        /// <returns></returns>
        public PageSetting SelPage(int cpage, int psize, int filter = 0)
        {
            string where = " 1=1";
            switch (filter)
            {
                case 1:
                    where += " AND AuthKey=''";
                    break;
                case 2:
                    where += " AND AuthKey<>''";
                    break;
            }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, "CDate Desc");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelByID(int id)
        {
            return Sql.Sel(TbName, PK, id);
        }
        public DataTable SelByIDS(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return null;
            SafeSC.CheckIDSEx(ids);
            string sql = "SELECT * FROM " + TbName + " WHERE ID IN (" + ids + ")";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        /// <summary>
        /// 全部,已授权,未授权
        /// </summary>
        public DataTable SelByFilter(int filter)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE 1=1 ";
            switch (filter)
            {
                case 0://全部,未授权,已授权
                    break;
                case 1:
                    sql += " AND AuthKey='' ";
                    break;
                case 2:
                    sql += " AND AuthKey!='' ";
                    break;
            }
            sql += " ORDER BY CDate DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public void DelByIDS(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return;
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + TbName + " WHERE ID IN (" + ids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 审核(生成授权码)与取消审核(删除授权码)
        /// </summary>
        public string AuditApply(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return "";
            string result = "";
            SafeSC.CheckIDSEx(ids);
            foreach (string id in ids.Split(','))
            {
                M_APP_Auth model = SelReturnModel(Convert.ToInt32(id));
                if (string.IsNullOrEmpty(model.AuthKey))
                {
                    model.AuthKey = GetAuthKey();
                    UpdateByID(model);
                    result += id + ",";
                }
            }
            return result.TrimEnd(',');
        }
        public void UnAuditApply(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return;
            SafeSC.CheckIDSEx(ids);
            foreach (string id in ids.Split(','))
            {
                M_APP_Auth model = SelReturnModel(Convert.ToInt32(id));
                if (!string.IsNullOrEmpty(model.AuthKey))
                {
                    model.AuthKey = "";
                    UpdateByID(model);
                }
            }
        }
        //----------------------Tools
        public string GetAuthKey()
        {
            return function.GetRandomString(12) + DateTime.Now.ToString("yyyyMMddHHmm");
        }
    }
}

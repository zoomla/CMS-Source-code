using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Content
{

    public class B_Hits
    {
        private string PK, strTableName;
        private M_Hits initmod = new M_Hits();
        public B_Hits()
        {
            PK = initmod.PK;
            strTableName = initmod.TbName;
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_Hits Select(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private M_Hits SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return initmod.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool Update(M_Hits model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Delete(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int Add(M_Hits model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 返回列表按时间，标题
        /// </summary>
        public DataTable GetList(string title, DateTime cutterTime)
        {
            string where = " 1=1 ";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("title", "%" + title + "%") };
            if (!string.IsNullOrEmpty(title))
            {
                where += " AND B.Title like @title ";
            }
            if (cutterTime != DateTime.MaxValue)
            {
                where += " AND datediff(month, A.UpdateTime,'" + cutterTime + "')=0";
            }
            where += " AND A.status<>-2";
            //string field = "h.id,h.gid,h.UpdateTime,h.uid,,m.Title,(select userid  from zl_user where UserName=m.inputer) as Inputer, h.Ip,h.status";
            string field = "A.*,(select UserName from zl_user where userid=A.uid) as UserName,B.Title,B.inputer";
            return SqlHelper.JoinQuery(field, "ZL_Hits", "ZL_CommonModel", "A.Gid=B.GeneralID", where, "", sp);
        }
        public int CountIp(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "select count(distinct(Ip)) from zl_hits where id in(" + ids + ")";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, sql));
        }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Update(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "update [dbo].[ZL_Hits] set status=-2 where id in (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public string GetUserIDByHisID(string ids)
        {
            string Uids = "";
            SafeSC.CheckIDSEx(ids);
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select userid From ZL_User Where UserID in ( Select Uid From " + strTableName + " Where ID in (" + ids + "))");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Uids += dt.Rows[i]["userid"] + ",";
                }
                Uids = Uids.TrimEnd(',');
            }
            return Uids;
        }
        public string GetUserID(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string userids = "";
            string sql = "select userid from zl_user where userid in (" + ids + ")";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    userids += row["userid"] + ",";
                }
                userids = userids.Trim(',');
            }
            return userids;
        }
    }
}
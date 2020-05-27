using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.Common;

namespace ZoomLa.BLL
{
    public class B_Search
    {
        private string PK,strTableName;
        private M_Search initMod = new M_Search();
        public B_Search() 
        {
            PK = initMod.PK; strTableName = initMod.TbName;
        }
        public M_Search GetSearchById(int ID)
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
        public DataTable SelectAll(string path="")
        {
            DataTable dt = SelUsing();
            if (!string.IsNullOrEmpty(path))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["FileUrl"] = dt.Rows[i]["FileUrl"].ToString().Replace("/manage/", path);
                }
            }
            return dt;
        }
        public DataTable SelByUserGroup(int groupid)
        {
            string where = " State=1 AND Type=2 ";
            if (groupid > 0) { where += " AND (UserGroup='' OR  ','+UserGroup+',' LIKE '%,'+'" + groupid + "'+',%' ) "; }
            return DBCenter.Sel(strTableName, where, "OrderID DESC");
        }
        public DataTable SelByEliteLevel(int EliteLevel, int Type)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE [Type]=" + Type;
            if (EliteLevel < 2)
            {
                sql += " AND [EliteLevel]=" + EliteLevel;
            }
            sql += " ORDER BY OrderID ASC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable SelByName(string name)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@name", "%"+name+"%") };
            string sql = "SELECT * FROM "+strTableName+" WHERE name LIKE @name";
            return SqlHelper.ExecuteTable(CommandType.Text,sql,sp);
        }
        public DataTable SelUsing()
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName + " Where State=1");
        }
        public bool UpdateByID(M_Search model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.Id.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool GetDelete(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public int insert(M_Search model)
        {
            return Sql.insertID(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool GetAdd(M_Search model)
        {
            return insert(model) > 0;
        }
        public List<M_Search> selectByName(string name)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name", "%" + name + "%") };
            DataSet ds = SqlHelper.ExecuteDataSet(CommandType.Text, "select * from ZL_Search where name like @name", sp);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<M_Search> search = new List<M_Search>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    M_Search sea = new M_Search();
                    sea.Id = DataConverter.CLng(dr["id"]);
                    sea.Name = dr["Name"].ToString();
                    sea.FlieUrl = dr["fileUrl"].ToString();
                    sea.Ico = dr["ico"].ToString();
                    sea.Type = DataConverter.CLng(dr["type"]);
                    sea.State = DataConverter.CLng(dr["state"]);
                    sea.LinkState = DataConverter.CLng(dr["linkState"]); 
                    sea.OpenType = DataConverter.CLng(dr["OpenType"]);
                    sea.Time = DataConverter.CDate(dr["time"]);
                    sea.Mobile = Convert.ToInt32(dr["Mobile"]);
                    sea.Size = Convert.ToInt32(dr["Size"]);
                    search.Add(sea);
                }
                return search;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 筛选获取数据
        /// </summary>
        /// <param name="Type">0:后台,1:会员中心</param>
        /// <param name="customPath">后台路径</param>
        /// <param name="state">0:全部,1:启用,2:禁用</param>
        /// <returns></returns>
        public DataTable SelByType(int Type, string customPath, int state)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE [Type]=" + Type;
            if (state > 0)
            {
                sql += " AND [State]=" + state;
            }
            sql += " ORDER BY OrderID ASC";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            if (Type == 1)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["FileUrl"] = dt.Rows[i]["FileUrl"].ToString().Replace("/manage/", customPath);
                }
            }
            return dt;
        }

        public DataTable SelByName(string name, int type)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("name",name)};
            string sql = "SELECT * FROM "+strTableName+" WHERE Name=@name AND [Type]="+type;
            return SqlHelper.ExecuteTable(CommandType.Text,sql,sp);
        }

        //查询最大orderid
        public int SelMaxOrder() 
        {
            DataTable dt = Sel();
            int maxOrder = Convert.ToInt32(dt.Rows[dt.Rows.Count-1]["OrderID"]); 
            return maxOrder;
        }
        //查询最小orderid
        public DataTable Sel()
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE 1=1 ORDER BY OrderID ASC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public int SelMinOrder()
        {
            DataTable dt = Sel();
            int minOrder = Convert.ToInt32(dt.Rows[0]["OrderID"]);
            return minOrder;
        }
        public int GetNextID(int curID)
        {
            int NextID = 0;
            return NextID;
        }
        public bool UpdateStatusByIDS(string ids, int status)
        {
            SafeSC.CheckIDSEx(ids);
            return SqlHelper.ExecuteSql("Update " + strTableName + " Set State=" + status + " Where id in(" + ids + ")");
        }
        public void UpdateOrder(int mid, int oid)
        {
            string sql = "Update " + strTableName + " Set OrderID = " + oid + " Where ID=" + mid;
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        } 
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return SqlHelper.ExecuteSql("Delete " + strTableName + " Where id in(" + ids + ")");
        }
    }
}
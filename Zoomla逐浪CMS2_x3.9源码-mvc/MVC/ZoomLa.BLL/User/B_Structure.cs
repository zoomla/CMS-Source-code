using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.BLL.Helper;

namespace ZoomLa.BLL
{
    //按text标准
    public class B_Structure
    {
        private B_User buser = new B_User();
        private string PK, strTableName;
        private M_Structure initMod = new M_Structure();
        private StrHelper strHelp = new StrHelper();
        public B_Structure()
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        public int Insert(M_Structure model)
        {
            return Sql.insertID(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 为部门添加用户
        /// </summary>
        public string AddUsers(string uids, int structID, string op = "add")
        {
            SafeSC.CheckIDSEx(uids);
            if (structID < 1 || string.IsNullOrEmpty(uids)) return "";
            M_Structure strMod = SelReturnModel(structID);
            if (op.Equals("add"))
            {
                strMod.UserIDS = StrHelper.AddToIDS(strMod.UserIDS, uids.Split(','));
            }
            else
            {
                strMod.UserIDS = StrHelper.RemoveRepeat(strMod.UserIDS.Split(','), uids.Split(','));
            }
            string sql = "UPDATE " + strTableName + " SET UserIDS=@uids WHERE ID=" + structID;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uids", strMod.UserIDS) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, sp);
            return strMod.UserIDS;
        }
        public string AddManager(string uids, int structID, string op = "add")
        {
            SafeSC.CheckIDSEx(uids);
            if (structID < 1 || string.IsNullOrEmpty(uids)) return "";
            M_Structure strMod = SelReturnModel(structID);
            if (op.Equals("add"))
            {
                strMod.ManagerIDS = StrHelper.AddToIDS(strMod.ManagerIDS, uids.Split(','));
            }
            else
            {
                strMod.ManagerIDS = StrHelper.RemoveRepeat(strMod.ManagerIDS.Split(','), uids.Split(','));
            }
            string sql = "UPDATE " + strTableName + " SET ManagerIDS=@uids WHERE ID=" + structID;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("uids", strMod.ManagerIDS) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, sp);
            return strMod.ManagerIDS;
        }
        public int Add(M_Structure model)
        {
            return Insert(model);
        }
        public bool Update(string sqlset, string sqlwhere)
        {
            return Sql.Update(strTableName, sqlset, sqlwhere, null);
        }
        public bool Update(M_Structure model)
        {
            return UpdateMapByMid(model);
        }
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable SelStatus(int Group, int status)
        {
            string str = "select * from " + strTableName + " where [Group]=" + Group + " and status=" + status;
            return SqlHelper.ExecuteTable(CommandType.Text, str);
        }
        public DataTable Sel(string s, string s2)
        {
            string sql = "Select * From " + strTableName + " Where 2=1";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public DataTable SelByField(string field, string value)
        {
            SafeSC.CheckDataEx(field);
            string sql = "Select * From " + strTableName + " Where " + field + " = @value";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("value", value) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public DataTable SelByPid(int pid)
        {
            string sql = "Select *,childCount=(SELECT COUNT(*) FROM "+strTableName+" WHERE ParentID=A.ID) From " + strTableName + " A Where ParentID=" + pid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }

        public int GetCount(int sid)
        {
            string sql = "Select Count(*) From ZL_User Where  StructureID Like '%," + sid + ",%'";
            return DataConvert.CLng(SqlHelper.ExecuteScalar(CommandType.Text, sql));
        }
        /// <summary>
        /// 组织名是否存在
        /// </summary>
        public bool IsExist(string strName)
        {
            strName = strName.Trim();
            string sql = "Select * From " + strTableName + " Where Name=@Name";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Name", strName) };
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp).Rows.Count > 0;
        }
        public M_Structure SelReturnModel(int id)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, id))
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
        ///// <summary>
        ///// 依据结构ID，查出ZL_User表中的数据
        ///// </summary>
        // public DataTable SelBySID(int sid) 
        // {
        //     string sql = "Select * From ZL_User Where StructureID Like '%,"+sid+",%'";
        //     return SqlHelper.ExecuteTable(CommandType.Text,sql);
        // }
        public int getnum()
        {
            return Sql.getnum(strTableName);
        }
        public int getnum(int gid)
        {
            string where = " [Group]=" + gid;
            return Sql.IsExistInt(strTableName, where);
        }
        public M_Structure Select(int ID)
        {
            string sqlStr = "select * from " + strTableName + " where ID=@ID";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            cmdParams[0].Value = ID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return initMod.GetModelFromReader(reader);
                }
                else
                {
                    return new M_Structure();
                }
            }
        }
        public bool UpdateMapByMid(M_Structure model)
        {
            return Sql.UpdateByID(strTableName, "ID", model.ID, BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool Del(string strWhere)
        {
            return Sql.Del(strTableName, strWhere);
        }
        public bool DelByIds(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return Sql.Del(strTableName, "ID in (" + ids + ")");
        }
        public void RemoveByIDS(string ids, int strid)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Update ZL_User Set StructureID=REPLACE(StructureID,'," + strid + ",','') Where UserID in(" + ids + ")";
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        /// <summary>
        /// 查询节点的最终父节点
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="p">判断上级站点为5，判断最终父节点为0</param>
        /// <returns></returns>
        public int GetContrarily(int ID)
        {
            M_Structure mn = Select(ID);
            int x = 0;

            if (mn.ParentID == 0)
            {
                x = mn.ID;
            }
            else
            {
                x = GetContrarily(mn.ParentID);
            }

            return x;
        }
        //获取所有组织结构相关的用户
        public string GetAllUids()
        {
            string sql = "SELECT UserIDS FROM "+strTableName;
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            string ids = "";
            foreach (DataRow item in dt.Rows)
            {
                ids += item["UserIDS"] + ",";
            }
            return ids.Trim(',');

        }
        //根据uid查询相关组织机构
        public DataTable SelByUid(int uid)
        {
            string sql = "SELECT * FROM " + strTableName + " WHERE ','+UserIDS+',' LIKE '%,"+uid+",%'";
            return SqlHelper.ExecuteTable(CommandType.Text,sql);
        }
        public string SelStrNameByIDS(string ids, string tlp = "[{0}],")
        {
            ids = ids.Trim(',');
            if (string.IsNullOrEmpty(ids)) return "";
            SafeSC.CheckIDSEx(ids);
            string result = "";
            string sql = "SELECT Name FROM " + strTableName + " WHERE ID IN(" + ids + ")";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            foreach (DataRow dr in dt.Rows)
            {
                result += string.Format(tlp, dr["Name"]);
            }
            result = result.TrimEnd(',');
            return result;
        }
        /// <summary>
        /// 根据UserID返回所属部门名,默认返回第一个
        /// </summary>
        public string SelNameByUid(int uid, int num = 1)
        {
            string sql = "SELECT TOP " + num + " Name FROM " + strTableName + " WHERE UserIDS Like '%," + uid + ",%'";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            string result = "";
            foreach (DataRow dr in dt.Rows)
            {
                result += dr["Name"] + ",";
            }
            return (result.TrimEnd(','));
        }
    }
}

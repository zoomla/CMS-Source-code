using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLa.BLL
{
    public class B_UserPurview
    {
        public M_UserPurview model = new M_UserPurview();
        public string strTableName = "";
        public string PK = "";
        public B_UserPurview()
        {
            strTableName = model.TbName;
            PK = model.PK;
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_UserPurview SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据条件查询一条记录
        /// </summary>
        private M_UserPurview SelReturnModel(string strWhere)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, strWhere))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }

        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="Sensitivity"></param>
        /// <returns></returns>
        public int insert(M_UserPurview model)
        {
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 根据权限插入数据
        /// </summary>
        public void InsertByCodes(string Codes, int RoleID,string NodeID="")
        {
            M_UserPurview model = new M_UserPurview();
            model.RoleID = RoleID;
            DelByCodes(Codes, RoleID);
            string[] CodesArr = Codes.Split(',');
            for (int i = 0; i < CodesArr.Length; i++)
            {
                if (CodesArr[i] == "OATop" || CodesArr[i] == "OADel" || CodesArr[i] == "OAEdit")
                {
                    string[] nidArr = NodeID.Split('|');
                    DelByCodeIn(CodesArr[i], RoleID);
                    if (CodesArr[i] == "OATop")
                        model.NodeID = nidArr[0];
                    else if (CodesArr[i] == "OADel")
                        model.NodeID = nidArr[1];
                    else if (CodesArr[i] == "OAEdit")
                        model.NodeID = nidArr[2];
                    model.PurviewCode = CodesArr[i];
                    insert(model);
                }
                if (!IsExist(CodesArr[i], RoleID))
                {
                    model.PurviewCode = CodesArr[i];
                    insert(model);
                }
            }
        }
        public void DelByCodeIn(string codes,int RoleID)
        {
            string[] codesArr = codes.Split(',');
            string sql = "";
            SqlParameter[] sp = new SqlParameter[codesArr.Length + 1];

            for (int i = 0; i < codesArr.Length; i++)
            {
                sp[i] = new SqlParameter("codes" + i, codesArr[i]);
                sql += "@" + sp[i].ParameterName + ",";
            }
            sp[codesArr.Length] = new SqlParameter("RoleID", RoleID);
            sql = sql.TrimEnd(',');
            SqlHelper.ExecuteSql("delete from " + strTableName + " where RoleID=@RoleID and PurviewCode in(" + sql + ")", sp);
        }
        /// <summary>
        /// 根据权限删除数据
        /// </summary>
        public void DelByCodes(string codes,int RoleID)
        {
            string[] codesArr = codes.Split(',');
            string sql = "";
            SqlParameter[] sp = new SqlParameter[codesArr.Length+1];

            for (int i = 0; i < codesArr.Length; i++)
            {
                sp[i] = new SqlParameter("codes" + i, codesArr[i]);
                sql += "@" + sp[i].ParameterName + ",";
            }
            sp[codesArr.Length] = new SqlParameter("RoleID", RoleID);
            sql = sql.TrimEnd(',');
            SqlHelper.ExecuteSql("delete from " + strTableName + " where RoleID=@RoleID and PurviewCode not in(" + sql + ")", sp);
        }
        public bool DelByRoleID(int RoleID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("RoleID", RoleID) };
            string sql = "delete from " + strTableName + " where RoleID=@RoleID";
            return SqlHelper.ExecuteSql(sql, sp);
        }
        /// <summary>
        /// 查找指定角色ID的数据
        /// </summary>
        public DataTable SelTableByRoleID(int RoleID)
        {
            SqlParameter[] sp =new SqlParameter[]{ new SqlParameter("RoleID",RoleID)};
            string sql = "select * from " + strTableName + " where RoleID=@RoleID";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public string GetCodeByRoleID(int RoleID)
        {
            DataTable dt = SelTableByRoleID(RoleID);
            string Codes="";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Codes += dt.Rows[i]["PurviewCode"] + ",";
                }
                Codes = Codes.Trim(',');
                return Codes;
            }
            else
                return "";
        }
        /// <summary>
        /// 判断是否已存在权限，存在返回true否则返回false
        /// </summary>
        public bool IsExist(string Code, int RoleID)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Code", Code), new SqlParameter("RoleID", RoleID) };
            string sql = "select * from " + strTableName + " where RoleID=@RoleID and PurviewCode=@Code";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 查看是否有权限 有则返回True否则返回False code为权限代码
        /// </summary>
        public static bool SelReturnByIDs(string ids,string code)
        {
            SafeSC.CheckIDSEx(ids);
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "select * from ZL_UserPurview where roleid in(" + ids + ")");
            DataView dv = dt.DefaultView;
            dv.RowFilter = "PurviewCode='" + code + "'";
            return dv.Count > 0;
        }
        public string GetIDS(string ids) 
        {
            string result = "";
            string[] idArr = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < idArr.Length; i++)
            {
                result += Convert.ToInt32(idArr[i]) + ",";//不参数化，此处强行转换，避免注入
            }
            result = result.TrimEnd(',');
            return result;
        }
        /// <summary>
        /// 获取节点id列表
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <param name="code">权限代码</param>
        /// <returns></returns>
        public string GetNodeIDs(int roleid,string code)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("code", code) };
            M_UserPurview model = new M_UserPurview();
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, "Where RoleID=" + roleid + " And PurviewCode=@code", sp))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader).NodeID;
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="authName">权限名</param>
        /// <param name="roleIDS">RoleIDS</param>
        /// <param name="nodeID">需要验证的节点ID</param>
        public bool Auth(string purViewCode, string roleIDS, int nodeID = -1)
        {
            if (string.IsNullOrEmpty(roleIDS.Replace(",", "").Replace(" ", "")))
                return false;
            string ids = GetIDS(roleIDS);
            if (string.IsNullOrEmpty(ids))
                return false;
            string sql = "Select * From ZL_UserPurview Where PurviewCode=@PurViewCode And RoleID in(" + ids + ") ";
            if (nodeID > -1)
            {
                sql += " And NodeID Like '%," + nodeID + ",%'";
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("PurViewCode", purViewCode) };
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
            return dt.Rows.Count > 0;
        }
    }
}

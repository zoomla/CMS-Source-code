using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    /*
     * 先获取管理员的RoleID,再根据RoleID读到相应的权限记录,再根据记录的值与规范决定权限
     */ 
    public class B_CrmAuth
    {
        public B_CrmAuth()
        {
            PK = initMod.PK;
            strTableName = initMod.TbName;
        }
        private string PK, strTableName;
        private M_CrmAuth initMod = new M_CrmAuth();
        public int insert(M_CrmAuth model)
        {
            return Sql.insert(strTableName, model.GetParameters(), model.GetParas(), model.GetFields());
        }
        /// <summary>
        /// 根据角色ID，获取目标权限模型
        /// </summary>
        public M_CrmAuth GetSelect(int roleID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, "RoleID", roleID))
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
        /// <summary>
        /// 根据角色字符串，加载所有的CRM角色,存为List
        /// </summary>
        public List<M_CrmAuth> GetSelects(params string[] roleID)//无数据，即为空，则只拥有最小只读权限
        {
            List<M_CrmAuth> crmModelList = new List<M_CrmAuth>();
            for (int i = 0; i < roleID.Length; i++)
            {
                using (SqlDataReader reader = Sql.SelReturnReader(strTableName, "where RoleID=" + roleID[i]))
                {
                    if (reader.Read())
                    {
                        crmModelList.Add(initMod.GetModelFromReader(reader));
                    }
                }
            }
            return crmModelList;
        }
        /// <summary>
        /// 加载所有角色,返回DataTable
        /// </summary>
        public DataTable GetAuthTable(params string[] roleID)
        {
            string sql = "select * from " + strTableName + " where ";
            foreach (string s in roleID)
            {
                if (string.IsNullOrEmpty(s)) continue;
                sql += " RoleID = " + s + " or";
            }
            if (!sql.Contains("RoleID")) return new DataTable();
            sql = sql.TrimEnd(new char[] { 'o', 'r' }); 
            return SqlHelper.ExecuteTable(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 是否具有权,为0不拥有1以上再考虑为级如修改，增加,info用于判断其是否为超级管理员,不直接在其中获取,降低藕合
        /// </summary>
        public bool IsHasAuth(DataTable authDT,string fieldName,M_AdminInfo info) 
        { 
            bool flag=false;
            if (info.IsSuperAdmin(info.RoleList))
            {
                flag = true;
            }
            else if (authDT.Rows.Count > 0)
            {
                authDT.DefaultView.RowFilter = fieldName+" > 0";
                if (authDT.DefaultView.ToTable().Rows.Count > 0) flag = true;
            }
            return flag;
        }
      
        /// <summary>
        /// 检测是否有该角色存在
        /// </summary>
        public bool IsExist(int roleID) 
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("roleID",roleID) };
            string sql = "select * from "+strTableName+" where RoleID = @roleID";
            return SqlHelper.Exists(CommandType.Text,sql,sp);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        public bool UpdateModel(M_CrmAuth model)
        {
            return Sql.UpdateByID(strTableName, "RoleID", model.RoleID, model.GetFieldAndPara(), model.GetParameters());
        }
        /// <summary>
        /// 获取所有销售员,返回字符串便于用于管理查询
        /// </summary>
        public string GetSalesRoles() 
        {
            string result="";
            string sql = "Select RoleID from " + strTableName + " Where IsSalesMan = 1";
            DataTable dt=SqlHelper.ExecuteTable(CommandType.Text,sql,null);//角色信息
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                result += (dr["RoleID"].ToString())+",";
                result.TrimEnd(',');
            }
            return result;
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, PK + " DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}

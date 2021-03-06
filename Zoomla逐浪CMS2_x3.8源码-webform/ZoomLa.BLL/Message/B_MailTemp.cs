﻿namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using System.Net.Mail;
    using System.Collections.Generic;
    using ZoomLa.Components;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    public class B_MailTemp
    {
        public B_MailTemp()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        private string PK, strTableName;
        private M_MailTemp initMod = new M_MailTemp();
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public M_MailTemp SelReturnModel(int ID)
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
        private M_MailTemp SelReturnModel(string strWhere)
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
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public DataTable SelectAll(string name = "")
        {

            string wherestr = "";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@tempname", "%" + name + "%") };
            if (!string.IsNullOrEmpty(name)) { wherestr += " AND TempName LIKE @tempname"; }
            string sql = "SELECT * FROM " + strTableName + " WHERE 1=1" + wherestr + " ORDER BY CreateTime DESC";
            return SqlHelper.ExecuteTable(sql, sp);
        }
        public PageSetting SelPage(int cpage, int psize, string name = "")
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(name)) { where += " AND TempName LIKE @tempname"; sp.Add(new SqlParameter("tempname", "%" + name + "%")); }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where, "CreateTime DESC", sp);
            DBCenter.SelPage(setting);
            return setting;
        }
        public bool UpdateByID(M_MailTemp model)
        {
         return DBCenter.UpdateByID(model,model.ID);
        }
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "SELECT * FROM " + strTableName + " WHERE ID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        public int insert(M_MailTemp model)
        {
            return Sql.insert(strTableName, initMod.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public int insert_mod(M_MailTemp model)
        {
            string sqlStr = "PR_MailTemp_Add";
            SqlParameter[] cmdParams = initMod.GetParameters();
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, sqlStr, cmdParams));
        }

    }

}


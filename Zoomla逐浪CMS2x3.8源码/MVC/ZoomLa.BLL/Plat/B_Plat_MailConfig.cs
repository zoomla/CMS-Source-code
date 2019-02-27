using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Model.Plat;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Plat
{
    public class B_Plat_MailConfig : ZL_Bll_InterFace<M_Plat_MailConfig>
    {
        public string TbName, PK;
        M_Plat_MailConfig initMod = new M_Plat_MailConfig();
        public B_Plat_MailConfig()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Plat_MailConfig model)
        {
           return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Plat_MailConfig model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool DelByIDS(string ids,int uid)
        {
            SafeSC.CheckIDSEx(ids);
            if (string.IsNullOrEmpty(ids)) return false;
            string sql = "Delete From " + TbName + " Where ID IN(" + ids + ") And UserID=" + uid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, sql) > 0;
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_Plat_MailConfig SelReturnModel(int ID)
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
        public M_Plat_MailConfig SelModelByUid(int id, int uid)
        {
            string sql = " Where ID=" + id + " And UserID=" + uid;
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, sql))
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
            return Sql.Sel(TbName, "", "");
        }
        public M_Plat_MailConfig SelByMail(int uid,string acount)
        {
            string sql = " Where Acount=@acount And UserID=" + uid;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("acount", acount) };
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
        public DataTable SelByUid(int uid)
        {
            string sql = "Select * From " + TbName + " Where UserID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public string GetPopByType(int type)
        {
            string result="";
            switch (type)
            {
                case 1:
                    result="pop3.163.com";
                    break;
                case 2:
                    result="pop.exmail.qq.com";
                    break;
                case 3:
                    result="pop.qq.com";
                    break;
                case 4:
                    result="pop.sina.cn";
                    break;
                case 5:
                    result = "pop.139.com";
                    break;
                case 6:
                    result = "pop.tom.com";
                    break;
            }
            return result;
        }
        public string GetSmtpByType(int type)
        {
            string result = "";
            switch (type)
            {
                case 1:
                    result = "smtp.163.com";
                    break;
                case 2:
                    result = "smtp.exmail.qq.com";
                    break;
                case 3:
                    result = "smtp.qq.com";
                    break;
                case 4:
                    result = "smtp.sina.cn";
                    break;
                case 5:
                    result = "smtp.139.com";
                    break;
                case 6:
                    result = "smtp.tom.com";
                    break;
            }
            return result;
        }
    }
}

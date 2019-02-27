using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Model.Plat;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
using ZoomLa.Model;

namespace ZoomLa.BLL.Plat
{
    public class B_Plat_Mail:ZL_Bll_InterFace<M_Plat_Mail>
    {
        public string TbName, PK;
        M_Plat_Mail initMod = new M_Plat_Mail();
        public B_Plat_Mail()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_Plat_Mail model)
        {
           return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Plat_Mail model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public bool DelByUid(string ids, int uid)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Update " + TbName + " Set Status="+(int)ZLEnum.ConStatus.Recycle+" Where ID IN(" + ids + ") And UserID=" + uid;
            return SqlHelper.ExecuteSql(sql);
        }
        public bool DelByIds(string ids)
        {
            if (SafeSC.CheckIDS(ids))
            {
                string sql = "Delete From " + TbName + " Where ID IN(" + ids + ")";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sql) > 0;
            }
            return false;
        }
        public M_Plat_Mail SelReturnModel(int ID)
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
        public DataTable Sel()
        {
            return Sql.Sel(TbName, "", "");
        }
        /// <summary>
        /// 筛选邮件数据,不含内容
        /// </summary>
        /// <param name="mailtype">0:收件,1:发件</param>
        /// <param name="mailtype">0:正常,-1回收站</param>
        public DataTable Sel(int psize, int uid, int mailtype, int status, string email, string skey)
        {
            string where = "";
            string fields = "A.ID,A.UserID,A.Title,A.MailType,A.Sender,A.Receiver,A.Attach,A.MailDate,A.CDate,B.HoneyName";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (uid > 0) { where += " A.UserID=" + uid; }
            if (mailtype != -100) { where += " AND A.MailType=" + mailtype; }
            if (status != -100) { where += " AND A.status=" + status; }
            if (!string.IsNullOrEmpty(email)) { where += " AND Receiver=@email"; sp.Add(new SqlParameter("email",email)); }
            if (!string.IsNullOrEmpty(skey)) { where += " A.Title LIKE @skey"; sp.Add(new SqlParameter("skey", "%" + skey + "%")); }
            //return DBCenter.JoinQuery(fields, TbName, "ZL_User", "A.UserID=B.UserID", where, "A.ID DESC", sp.ToArray());
            PageSetting setting = new PageSetting()
            {
                cpage = 1,
                psize = psize,
                pk = "A.ID",
                fields = fields,
                t1 = TbName,
                t2 = "ZL_User",
                on = "A.UserID=B.UserID",
                where = where,
                order = "A.ID DESC",
                spList = sp
            };
            return DBCenter.SelPage(setting);
        }
        public DataTable SelByDel(int uid)
        {
            string sql = "Select * From " + TbName + " Where UserID=" + uid + " AND Status<0";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public bool ReBox(string ids)
        {
            if (SafeSC.CheckIDS(ids))
            {
                string sql = "Update " + TbName + " Set Status=1 Where ID IN(" + ids + ")";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sql) > 0;
            }
            return false;
        }
        public DataTable SelMailIDByUid(int uid)
        {
            string sql = "Select ID,MailID From " + TbName + " Where UserID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text,sql);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Design
{
    public class B_Design_Scence : ZL_Bll_InterFace<M_Design_Page>
    {
        private string PK, TbName = "";
        private M_Design_Page initMod = new M_Design_Page();
        public B_Design_Scence()
        {
            PK = initMod.PK;
            TbName = "ZL_Design_Scence";
        }
        public int Insert(M_Design_Page model)
        {
            if (model.OrderID < 1) { model.OrderID = MaxOrderID(model.UserID); }
            model.TbName = "ZL_Design_Scence";
            return DBCenter.Insert(model);
        }
        public bool UpdateByID(M_Design_Page model)
        {
            model.TbName = "ZL_Design_Scence";
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }
        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", PK + " DESC");
        }
        /// <summary>
        /// 根据场景状态查询,默认查询未删除的场景
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataTable SelModelByStatus(string skey = "", int status = -100)
        {
            string where = "A.ZType = 0 AND TlpID = 0 AND ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (status == -100) { where += "A.status <> -2 "; }
            else { where += "A.status = " + status; }
            if (!string.IsNullOrEmpty(skey))
            {
                where += " AND (A.Title LIKE @skey OR B.UserName LIKE @skey) ";
                sp.Add(new SqlParameter("skey", "%" + skey + "%"));
            }
            string fields = "A.*,B.UserName,B.UserName UName,B.Permissions TrueName,B.HoneyName";
            return SqlHelper.JoinQuery(fields, TbName, "ZL_User", "A.UserID = B.UserID", where, PK + " DESC", sp.ToArray());
        }
        public M_Design_Page SelReturnModel(int ID)
        {
            if (ID < 1) { return null; }
            return SelModelByWhere(TbName, PK + "=" + ID, null);
        }
        public M_Design_Page SelModelByGuid(string guid)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("guid", guid) }; ;
            string where = "Guid=@guid";
            return SelModelByWhere(TbName, where, sp);
        }
        public M_Design_Page SelModelByDef(int uid)
        {
            string where = "UserID=" + uid;
            return SelModelByWhere(TbName, where, null);
        }
        public M_Design_Page SelModelByTlp(int tlpid)
        {
            string where = "TlpID=" + tlpid;
            return SelModelByWhere(TbName, where, null);
        }
        private M_Design_Page SelModelByWhere(string tbname, string where, List<SqlParameter> sp)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(tbname, where, "", sp))
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
        //----------Logical
        public DataTable A_Sel(int uid, int id = 0)
        {
            return DBCenter.SelWithField(TbName, "id,guid,title", "UserID=" + uid, "OrderID ASC,ID ASC");
        }
        /// <summary>
        /// 直接新建一个页面
        /// </summary>
        public DataTable A_Add(M_UserInfo mu)
        {
            M_Design_Page model = new M_Design_Page();
            model.UserID = mu.UserID;
            model.UserName = mu.UserName;
            model.SiteID = mu.SiteID;
            model.Path = "/" + function.GetRandomString(6);
            model.Title = "";
            model.ZType = 0;
            model.ID = Insert(model);
            return A_Sel(mu.UserID, model.ID);
        }
        //----------
        public void U_Del(int uid, int id)
        {
            DBCenter.DelByWhere(TbName, "ID=" + id + " AND UserID=" + uid);
        }
        public void U_Del(int uid, string ids)
        {
            SafeSC.CheckIDSEx(ids);
            DBCenter.DelByWhere(TbName, "ID IN (" + ids + ") AND UserID=" + uid);
        }
        public DataTable U_Sel(int uid)
        {
            return DBCenter.Sel(TbName, "UserID=" + uid, PK + " DESC");
        }
        //----------Tools
        private int MaxOrderID(int uid)
        {
            string where = "UserID=" + uid;
            return (DataConvert.CLng(DBCenter.ExecuteScala(TbName, "MAX(OrderID)", where)) + 1);
        }
    }
}

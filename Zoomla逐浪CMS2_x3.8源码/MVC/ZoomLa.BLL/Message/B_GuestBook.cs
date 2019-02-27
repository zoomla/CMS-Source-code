using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_GuestBook
    {
        public B_GuestBook()
        {
            strTableName = initMod.TbName;
            PK = initMod.PK;
        }
        public string PK, strTableName;
        M_GuestBook initguest = new M_GuestBook();
        M_GuestBookCate initMod = new M_GuestBookCate();
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
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }
        public static bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM ZL_GuestBook WHERE GID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 添加或更新
        /// </summary>
        public bool AddTips(M_GuestBook model)
        {
            if (model.GID < 1)
            {
                Sql.insertID(model.TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
            }
            else
            {
                Sql.UpdateByID(model.TbName, model.PK, model.GID, BLLCommon.GetFieldAndPara(model), model.GetParameters());
            }
            return true;
        }
        /// <summary>
        /// 删除留言
        /// </summary>
        /// <param name="TID"></param>
        /// <returns></returns>
        public static bool DelTips(int TID)
        {
            string sql = "delete from ZL_GuestBook where GID=" + TID + " or ParentID=" + TID;
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool UpdateAudit(string TID, int status)
        {
            SafeSC.CheckIDSEx(TID);
            string sql = "Update ZL_GuestBook set Status=" + status + " where  GID in (" + TID + ")";
            return SqlHelper.ExistsSql(sql);
        }
        /// <summary>
        /// 读取留言实例
        /// </summary>
        /// <param name="GID"></param>
        /// <returns></returns>
        public M_GuestBook GetQuest(int GID)
        {
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@GID", SqlDbType.Int);
            cmdParams[0].Value = GID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, "select * from ZL_GuestBook where GID=@GID", cmdParams))
            {
                if (reader.Read())
                {
                    return initguest.GetModelFromReader(reader);

                }
                else
                    return new M_GuestBook(true);
            }
        }
        /// <summary>
        /// 读取所有帖子列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTipsAll()
        {
            string strSql = "select * from ZL_GuestBook where ParentID=0 AND Status>0 order by GDate Desc";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);

        }
        public static DataTable GetAllTips(int CateID)
        {
            return GetAllTips(CateID, -2);
        }
        /// <summary>
        /// 100获取全部,0未审,1:已审核,-1回收站
        /// </summary>
        public static DataTable GetAllTips(int CateID, int status, string skey = "", int pid = -100)
        {
            string wherestr = "1=1"; wherestr += CateID > 0 && status != -1 ? " AND A.CateID=" + CateID : "";
            if (status != 100)
                wherestr += " And A.[Status]=" + status;
            else
                wherestr += " AND A.[Status]<>-1";
            if (!string.IsNullOrEmpty(skey))
            {
                wherestr += " AND A.Title Like @skey OR A.Gid=@gid";
            }
            if (pid != -100) { wherestr += " AND A.Parentid=" + pid; }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("gid", DataConvert.CLng(skey)), new SqlParameter("skey", "%" + skey + "%") };
            return SqlHelper.JoinQuery("A.*,B.UserName", "ZL_GuestBook", "ZL_User", "A.UserID=B.UserID", wherestr, "GDate Desc", sp);
        }
        /// <summary>
        /// 读取某帖子及其下级帖子列表
        /// </summary>
        public static DataTable GetTipsList(int GID, int PageSize, int Cpage)
        {
            PageSetting setting = new PageSetting();
            setting.pk = "A.Gid";
            setting.fields = "A.*,B.salt AS UserFace,B.UserName";
            setting.t1 = "ZL_GuestBook";
            setting.t2 = "ZL_User";
            setting.on = "A.UserID=B.UserID";
            setting.psize = PageSize;
            setting.cpage = Cpage;
            setting.where = "A.GID=" + GID + " OR A.ParentID=" + GID;
            setting.order = "A.ParentID ASC,A.Gid ASC";
            return DBCenter.SelPage(setting);
        }
        public static PageSetting GetTipsList_SPage(int cpage, int psize, int gid)
        {
            PageSetting setting = PageSetting.Double(cpage, psize, "ZL_GuestBook", "ZL_User", "A.Gid", "A.UserID=B.UserID", "A.GID=" + gid + " OR A.ParentID=" + gid, "A.ParentID ASC,A.Gid ASC", null, "A.*,B.salt AS UserFace,B.UserName");
            DBCenter.SelPage(setting);
            return setting;
        }
        /// <summary>
        /// 某留言及下级帖子总数
        /// </summary>
        /// <param name="GID"></param>
        /// <returns></returns>
        public static int GetTipsTotal(int GID)
        {
            string strSql = "select count(GID) from ZL_GuestBook where GID=@GID and Status =1 or ParentID=@GID and Status =1";
            SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@GID", SqlDbType.Int)
            };
            sp[0].Value = GID;
            return SqlHelper.ObjectToInt32(SqlHelper.ExecuteScalar(CommandType.Text, strSql, sp));
        }
    }
}

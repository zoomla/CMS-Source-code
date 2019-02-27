using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL.Helper;
using ZoomLa.Model;
using ZoomLa.Model.Chat;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Chat
{
    public class B_UAction : ZL_Bll_InterFace<M_UAction>
    {
        private string PK, TbName = "";
        private M_UAction initMod = new M_UAction();
        public B_UAction()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public int Insert(M_UAction model)
        {
           return DBCenter.Insert(model);
        }
        //批量插入(需优化代码)
        public void Insert(List<M_UAction> list)
        {
            string ip = IPScaner.GetUserIP();
            int uid = 0; string uname = ""; DateTime cdate = DateTime.Now;
            M_UserInfo mu = new B_User().GetLogin();
            if (mu != null && mu.UserID > 0)
            {
                uid = mu.UserID;
                uname = mu.UserName;
            }
            for (int i = 0; i < list.Count; i++)//后期更换为批量插入
            {
                list[i].uid = uid;
                list[i].ip = ip;
                list[i].uname = uname;
                list[i].cdate = DateTime.Now;
                Insert(list[i]);
            }
        }
        public bool UpdateByID(M_UAction model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_UAction SelReturnModel(int ID)
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
        public M_UAction SelModelByAlias(string alias)
        {
            if (string.IsNullOrEmpty(alias.Trim())) return null;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("alias", alias) };
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE Alias=@alias AND MyState=1", sp))
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
            return Sql.Sel(TbName, "", "CDate Desc");
        }
        public DataTable SelByFlag(string idflag)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("idflag", idflag) };
            string sql = "SELECT * FROM " + TbName + " WHERE idflag=@idflag";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable SelByUid(int uid)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE uid=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "DELETE FROM " + TbName + " WHERE ID IN (" + ids + ")";
            return SqlHelper.ExecuteSql(sql);
        }

        public DataTable SelBySearch(string search, int type)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("search", "%" + search + "%") };
            string wherestr = "1=1";
            switch (type)
            {
                case 1://按用户名搜索
                    wherestr += " AND uname LIKE @search";
                    break;
                case 2://按用户标识搜索
                    wherestr += " AND idflag LIKE @search";
                    break;
            }
            string sql = "SELECT * FROM " + TbName + " WHERE " + wherestr;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public PageSetting SelPage(int cpage, int psize, int uid = 0, string idflag = "")
        {
            string where = "1=1 ";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (uid > 0) { where += " AND uid =" + uid; }
            if (!string.IsNullOrEmpty(idflag)) { where += " AND idflag LIKE @idflag"; sp.Add(new SqlParameter("idflag", idflag)); }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where);
            setting.order = " CDate Desc";
            setting.sp = sp.ToArray();
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}

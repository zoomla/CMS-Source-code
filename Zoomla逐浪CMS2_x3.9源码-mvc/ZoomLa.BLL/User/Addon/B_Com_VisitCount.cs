using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;


namespace ZoomLa.BLL.User
{
    public class B_Com_VisitCount
    {
        private string PK, TbName;
        private M_Com_VisitCount initMod = new M_Com_VisitCount();
        public B_Com_VisitCount()
        {
            PK = initMod.PK;
            TbName = initMod.TbName;
        }
        public static int Insert(M_Com_VisitCount model)
        {
            return Sql.insertID(model.TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_Com_VisitCount model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public void UpdateNum(int ID)
        {
            string sql = "UPDATE ZL_Com_VisitCount SET DVisitNum=DVisitNum+1 WHERE ID=" + ID;
            SqlHelper.ExecuteSql(sql);
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_Com_VisitCount SelReturnModel(int ID)
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
        public bool SelByIP(string IP)
        {
            return Sql.IsExist(TbName, "IP=" + IP + " AND CDate LIKE " + DateTime.Now.Date);
        }
        public DataTable SelDataByIP(string IP)
        {
            return SqlHelper.ExecuteTable("SELECT * FROM " + TbName + " WHERE IP = " + IP);
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        public DataTable CountByIP()
        {
            string sql = "SELECT IP FROM " + TbName + " GROUP BY IP";
            return SqlHelper.ExecuteTable(sql);
        }
        public int GetAllNum(string field)
        {
            string sql = "SELECT COUNT(" + field + ")AS Count FROM " + TbName;
            object obj = SqlHelper.ExecuteScalar(CommandType.Text, sql);
            return DataConvert.CLng(obj);

        }
        public DataTable SelByField(string field)
        {
            string sql = "SELECT COUNT(" + field + ")AS NUM," + field + " FROM " + TbName + " WHERE " + field + " IS NOT NULL GROUP BY " + field;
            return SqlHelper.ExecuteTable(sql);
        }
        /// <summary>
        /// 统计月总访问量
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public DataTable CountByMonth(int year, int month)
        {
            string sql = "SELECT COUNT(*) AS Num FROM " + TbName + " WHERE YEAR(CDate)=" + year + " AND MONTH(CDate)=" + month;
            return SqlHelper.ExecuteTable(sql);
        }
        public DataTable CountByYear(int year)
        {
            string sql = "SELECT COUNT(*) AS Num FROM " + TbName + " WHERE YEAR(CDate)=" + year;
            return SqlHelper.ExecuteTable(sql);
        }
        public DataTable SelByTime(int type, DateTime time)
        {
            string sql = "";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("Year", time.Year), new SqlParameter("Month", time.Month) };
            switch (type)
            {
                case 1://按年统计
                    sql = "SELECT COUNT(YEAR)AS NUM,A.YEAR FROM (SELECT *,YEAR(CDate)AS YEAR FROM ZL_Com_VisitCount )AS A GROUP BY YEAR";
                    break;
                case 2://按月统计
                    sql = "SELECT COUNT(MONTH)AS NUM,A.MONTH FROM (SELECT *,MONTH(CDate)AS MONTH FROM ZL_Com_VisitCount WHERE YEAR(CDate)=@Year)AS A GROUP BY MONTH";
                    break;
                case 3://按日统计
                    sql = "SELECT COUNT(DAY)AS NUM,A.DAY,A.Year,A.Month FROM (SELECT *,YEAR(CDate) AS Year,MONTH(CDate) AS Month,DAY(CDate)AS DAY FROM ZL_Com_VisitCount WHERE (YEAR(CDate)=@Year AND MONTH(CDate)=@Month))AS A GROUP BY Year,Month,DAY";
                    break;
            }
            return SqlHelper.ExecuteTable(sql, sp);
        }
        /// <summary>
        /// 按系统查询
        /// </summary>
        /// <returns></returns>
        public DataTable SelBySys()
        {
            string sql = "SELECT COUNT(OSVersion) AS Num,OSVersion FROM " + TbName + " GROUP BY OSVersion";
            return SqlHelper.ExecuteTable(sql);
        }
        /// <summary>
        /// 按浏览器管理
        /// </summary>
        /// <returns></returns>
        public DataTable SelByBrowser()
        {
            string sql = "SELECT COUNT(BrowerVersion) AS Num,BrowerVersion FROM " + TbName + " GROUP BY BrowerVersion";
            return SqlHelper.ExecuteTable(sql);
        }
        public DataTable SelectAll(int year = 0, int month = 0, string username = "", string source = "")
        {
            string wherestr = "1=1";
            if (year > 0)
            { wherestr += " AND YEAR(CDate)=" + year; }
            if (month > 0)
            { wherestr += " AND MONTH(CDate)=" + month; }
            if (!string.IsNullOrEmpty(username))
            { wherestr += " AND B.UserName LIKE @username"; }
            if (!string.IsNullOrEmpty(source))
            { wherestr += " AND A.Source LIKE @source"; }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("username", "%" + username + "%"), new SqlParameter("source", "%" + source + "%") };
            string sql = "SELECT A.*,B.UserName FROM " + TbName + " A LEFT JOIN ZL_User B ON A.UserID=B.UserID WHERE " + wherestr + " ORDER BY A.CDate DESC";
            return SqlHelper.ExecuteTable(sql, sp);
        }
        public DataTable SelForSum(string ztype, string infotitle = "")
        {
            string where = "ZType IN (";
            List<SqlParameter> sp = new List<SqlParameter>();
            string[] types = ztype.Split(',');
            for (int i = 0; i < types.Length; i++)
            {
                where += "@type" + i + ",";
                sp.Add(new SqlParameter("type" + i, types[i]));
            }
            where = where.Trim(',') + ")";
            if (!string.IsNullOrEmpty(infotitle))
            {
                where += " AND InfoTitle LIKE @infotitle";
                sp.Add(new SqlParameter("infotitle", "%" + infotitle + "%"));
            }
            string sql = "SELECT *,(SELECT COUNT(*) FROM ZL_Com_VisitCount WHERE INFOID=A.InfoID)VisitCount FROM ZL_Com_VisitCount A WHERE ID IN (SELECT MAX(ID) ID FROM ZL_Com_VisitCount WHERE " + where + " GROUP BY InfoID)";
            return SqlHelper.ExecuteTable(sql, sp.ToArray());
        }
        public DataTable SelByType(string ztype, string infotitle, int uid)
        {
            string where = "ZType IN (";
            List<SqlParameter> sp = new List<SqlParameter>();
            string[] types = ztype.Split(',');
            for (int i = 0; i < types.Length; i++)
            {
                where += "@type" + i + ",";
                sp.Add(new SqlParameter("type" + i, types[i]));
            }
            where = where.Trim(',') + ")";
            if (!string.IsNullOrEmpty(infotitle))
            {
                where += " AND InfoTitle LIKE @infotitle";
                sp.Add(new SqlParameter("infotitle", "%" + infotitle + "%"));
            }
            if (uid > 0) { where += " AND UserID = " + uid; }
            return DBCenter.Sel(TbName, where, "CDate DESC,InfoID ASC", sp);
        }
        public DataTable SelByType(string ztype, string infotitle = "")
        {
            return SelByType(ztype, infotitle, 0);
        }
        public DataTable SelByInfoID(string InfoId)
        {
            string where = " InfoID = @InfoID";
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("InfoID", InfoId) };
            return DBCenter.Sel(TbName, where, "CDate DESC", sp);
        }
    }
}

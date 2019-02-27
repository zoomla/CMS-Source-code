using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.BLL.Helper;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.User
{
    public class B_Plat_Sign : ZL_Bll_InterFace<M_Plat_Sign>
    {
        private M_Plat_Sign initMod = new M_Plat_Sign();
        private string TbName, PK;
        B_User buser = new B_User();
        public B_Plat_Sign()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }

        public int Insert(M_Plat_Sign model)
        {
            return DBCenter.Insert(model);
        }

        public DataTable Sel()
        {
            return DBCenter.Sel(TbName);
        }

        public M_Plat_Sign SelReturnModel(int ID)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, PK, ID))
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

        public bool UpdateByID(M_Plat_Sign model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        /// <summary>
        /// 执行签到/签退操作
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>签到记录ID，若返回值为-1说明今日已签退，此次没有进行任何操作</returns>
        private int Sign(M_UserInfo mu, int ZType)
        {
            M_Plat_Sign model = new M_Plat_Sign();
            DateTime sdate = DataConvert.CDate("08:30");//上班时间
            DateTime edate = DataConvert.CDate("18:00");//下班时间
            model.State = 0;
            if (ZType == 0 && DateTime.Now > sdate) { model.State = 1; }//迟到
            if (ZType == 1 && DateTime.Now < edate) { model.State = 2; }//早退
            model.UserID = mu.UserID;
            model.IP = IPScaner.GetUserIP();
            model.IPLocation = IPScaner.IPLocation(model.IP);
            model.ZType = ZType;
            return Insert(model);
        }
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="mu"></param>
        /// <returns></returns>
        public int SignIn(M_UserInfo mu)
        {
            return Sign(mu, 0);
        }
        /// <summary>
        /// 签退
        /// </summary>
        /// <param name="mu"></param>
        /// <returns></returns>
        public int SignOut(M_UserInfo mu)
        {
            return Sign(mu, 1);
        }
        /// <summary>
        /// 获取该用户的签到情况
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public string GetSignType(M_UserInfo mu)
        {
            //M_Plat_Sign signMod = GetLastModel(mu);
            //if (signMod == null||signMod.ZType==1) { return "signin"; }//没有签到记录或上一次为签退操作
            //return "signout";

            DataTable dt = GetToDaySign(mu);//获取今日的签到记录
            switch (dt.Rows.Count)
            {
                case 0:
                    return "signin";
                case 1:
                    return "signout";
                default:
                    return "end";
            }
        }
        /// <summary>
        /// 获取今天的签到记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetToDaySign(M_UserInfo mu)
        {
            string where = "UserID = " + mu.UserID;
            where += " AND Convert(varchar(10),[CDate],120) = Convert(varchar(10),getDate(),120)";
            return DBCenter.Sel(TbName, where, "CDate DESC");
        }
        /// <summary>
        /// 获取用户最后一条签到记录
        /// </summary>
        /// <param name="mu"></param>
        /// <returns></returns>
        public M_Plat_Sign GetLastModel(M_UserInfo mu)
        {
            using (DbDataReader reader = DBCenter.SelReturnReader(TbName, "UserID = " + mu.UserID, "CDate DESC"))
            {
                if (reader.Read())//结果按时间倒序排列，取的是最后一条数据
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
        /// 根据月份获取用户考勤记录
        /// </summary>
        /// <param name="time"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public DataTable SelUserByMonth(DateTime date, int uid)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("date", date) };
            string where = "UserID = " + uid + " AND DATEDIFF(MONTH,[CDate],@date) = 0";
            return DBCenter.Sel(TbName, where, "", sp);
        }
        /// <summary>
        /// 根据月份获取公司的考勤统计
        /// </summary>
        /// <param name="date"></param>
        /// <param name="compid"></param>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public DataTable SelCompByMonth(DateTime date, int compid, int groupid = 0)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("date", date), new SqlParameter("compid", compid) };
            string sql = @"SELECT 
                        A.TrueName,c.CompName,A.UserID,
                        (SELECT COUNT(*) FROM ZL_Plat_Sign D WHERE DATEDIFF(MONTH,[CDate], @date)=0 AND UserID = A.UserID AND ZType=0) AS attendance,
                        (SELECT COUNT(*) FROM ZL_Plat_Sign D WHERE DATEDIFF(MONTH,[CDate], @date)=0 AND UserID = A.UserID AND State=1) AS late,
                        (SELECT COUNT(*) FROM ZL_Plat_Sign D WHERE DATEDIFF(MONTH,[CDate], @date)=0 AND UserID = A.UserID AND State=2) AS leave,
                        (SELECT COUNT(*) FROM ZL_Plat_Sign D WHERE DATEDIFF(MONTH,[CDate], @date)=0 AND UserID = A.UserID) AS signcount
                        FROM ZL_User_Plat A
                        LEFT join ZL_Plat_Sign B on A.UserID = B.UserID
                        INNER join ZL_Plat_Comp C on A.CompID = C.ID
                        WHERE  CompID = @compid
                        GROUP BY A.UserID,A.TrueName,C.CompName";
            return SqlHelper.ExecuteTable(sql, sp.ToArray());
        }
        /// <summary>
        /// 获取公司用户签到详情
        /// </summary>
        /// <param name="date"></param>
        /// <param name="compid"></param>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public DataTable SelInfoByMonth(DateTime date, int compid, int groupid = 0)
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("date", date), new SqlParameter("compid", compid) };
            string sql =  @"SELECT A.CDate,A.UserID,B.TrueName,B.CompID FROM ZL_Plat_Sign A
                            INNER JOIN ZL_User_Plat B ON A.UserID = B.UserID
                            INNER JOIN ZL_Plat_Comp C ON B.CompID = C.ID
                            WHERE CompID = @compid AND DATEDIFF(MONTH,[CDate], @date)= 0
                            ORDER BY A.UserID ASC, A.CDate ASC";
            return SqlHelper.ExecuteTable(sql, sp.ToArray());
        }
    }
}

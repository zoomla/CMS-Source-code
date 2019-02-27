using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.User
{
    /*
     * 只记录需要提成的用户消费记录
     */
    public class B_User_Consume//使用M_User_UnitWeek作为模型类,后期修正过来
    {
        private string TbName, PK;
        private M_User_Consume initMod = new M_User_Consume();
        public B_User_Consume()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        //插入消费记录
        public void Insert(int uid, double amount, string cdate)
        {
            M_UserInfo mu = new B_User().GetSelect(uid);
            int PUserID = string.IsNullOrEmpty(mu.ParentUserID) ? 0 : Convert.ToInt32(mu.ParentUserID);
            PUserID = mu.UserID == PUserID ? 0 : PUserID;
            string sql = "Insert Into " + TbName + " (UserID,PUserID,MType,AMount,CDate)";
            sql += " values(" + uid + "," + PUserID + ",1," + amount + ",'" + cdate + "')";
            SqlHelper.ExecuteSql(sql);
        }
        //插入分成记录
        public int Insert(M_User_Consume model)
        {
            return DBCenter.Insert(model);
        }
        //最近的一条组升级Log
        public M_User_Consume SelLastUpModel(int uid, int gid)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(TbName, " WHERE Detail='" + gid + "' And UserID=" + uid + " ORDER BY CDate DESC"))
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
        //获取流水记录
        public DataTable SelAll(int puserid, DateTime time)
        {
            string stime, etime; B_User_UnitWeek.GetWeekSE(time, out stime, out etime);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("stime", stime), new SqlParameter("etime", etime) };
            string sql = "Select * From " + TbName + " Where CDate BetWeen @stime AND @etime And PUserID=" + puserid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public PageSetting SelPage(int cpage, int psize, int puserid = -100, DateTime? time = null)
        {
            string where = " 1=1";
            List<SqlParameter> sp = new List<SqlParameter>();
            if (puserid != -100) { where += " AND PUserID=" + puserid; }
            if (time != null)
            {
                string stime, etime; B_User_UnitWeek.GetWeekSE((DateTime)time, out stime, out etime);
                where += " AND CDate BETWEEN @stime AND @etime";
                sp.Add(new SqlParameter("stime", stime));
                sp.Add(new SqlParameter("etime", etime));
            }
            PageSetting setting = PageSetting.Single(cpage, psize, TbName, PK, where, "", sp);
            DBCenter.SelPage(setting);
            return setting;
        }

        /// <summary>
        /// 按时间获取消费信息,为空则取全部的消费记录,同用户的消费记录聚合为一个
        /// </summary>
        /// <param name="time">输入则自动获取该周的起始与结束时间</param>
        /// <returns></returns>
        public DataTable SelByTime(string time = "")
        {
            string stime, etime;
            B_User_UnitWeek.GetWeekSE(DataConvert.CDate(time), out stime, out etime);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("stime", stime), new SqlParameter("etime", etime) };
            string fields = "A.UserID,A.UserName,A.ParentUserID,A.GroupID,A.HoneyName,B.AMount";
            string sql = "Select " + fields + " FROM ZL_User AS A LEFT JOIN"
                     + " (SELECT UserID,Sum(AMount)AS AMount FROM " + TbName + " WHERE 1=1 ";
            if (!string.IsNullOrEmpty(time))
            {
                sql += " AND CDate BetWeen @stime AND @etime";
            }
            sql += " AND UserID>0 GROUP BY UserID)B ON A.UserID=B.UserID";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        public DataTable Sel()
        {
            return Sql.Sel(TbName);
        }
        //获取某段时间下的记录
        public DataTable SelByTime(DateTime time)
        {
            string stime, etime;
            B_User_UnitWeek.GetWeekSE(time, out stime, out etime);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("stime", stime), new SqlParameter("etime", etime) };
            string sql = "Select * From " + TbName + " Where CDate BetWeen @stime AND @etime";
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        /// <summary>
        /// 获取下用户的所有子级业绩
        /// </summary>
        /// <param name="flow">流水号</param>
        /// <param name="ids">用户IDS</param>
        /// <returns></returns>
        public DataTable SelByPid(string flow, string ids)
        {
            SafeSC.CheckIDSEx(ids);
            ids = ids.TrimEnd(',');
            SqlParameter[] sp = new SqlParameter[]{
            new SqlParameter("flow",flow)

            };
            string sql = "Select *,(Select UserName From ZL_User Where UserID=A.UserID) UserName,(Select UserName From ZL_User Where UserID=A.PUserID) PUserName From " + TbName + " AS A Where Flow=@flow AND UserID IN(" + ids + ")";
            //if (string.IsNullOrEmpty(date))
            //{
            //    string stime, etime; B_User_UnitWeek.GetWeekSE(DataConvert.CDate(date), out stime, out etime);
            //    sp = new SqlParameter[] { new SqlParameter("stime", stime), new SqlParameter("etime", etime) };
            //    sql += "  AND CDate BetWeen @stime AND @etime";
            //}
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        //更新分成金额
        public void UpdateUnit(M_User_UnitWeek pmodel, double percent, DateTime time)
        {
            string stime, etime; B_User_UnitWeek.GetWeekSE(time, out stime, out etime);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("stime", stime), new SqlParameter("etime", etime), new SqlParameter("remind", DateTime.Now) };
            if (pmodel.PUserID == 0 && !string.IsNullOrEmpty(pmodel.ChildIDS.Replace(",", "")))
            {
                pmodel.ChildIDS = pmodel.ChildIDS.Trim(',');
                string sql = "Update " + TbName + " Set UnitPercent=" + percent + ",UnitAmount=AMount*" + percent + ",Remind=@remind Where UserID IN(" + pmodel.ChildIDS + ") And CDate BetWeen @stime AND @etime";
                SqlHelper.ExecuteSql(sql, sp);
            }
        }
    }
    public class B_User_UnitWeek : ZL_Bll_InterFace<M_User_UnitWeek>
    {
        public string TbName, PK;
        public M_User_UnitWeek initMod = new M_User_UnitWeek();
        public B_User_UnitWeek()
        {
            TbName = initMod.UnitWeekTbName;
            PK = initMod.PK;
        }
        public int Insert(M_User_UnitWeek model)
        {
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_User_UnitWeek model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.UserID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public void UpdateStateByIDS(string ids, int state = 1)
        {
            SafeSC.CheckIDSEx(ids);
            string sql = "Update " + TbName + " Set State=" + state + " WHERE ID IN(" + ids + ")";
            SqlHelper.ExecuteSql(sql);
        }
        public void UpdateStateByTime(DateTime time)
        {
            string stime, etime; GetWeekSE(time, out stime, out etime);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("stime", stime), new SqlParameter("etime", etime) };
            string sql = "Update " + TbName + " Set State=1 WHERE CDate BetWeen @stime AND @etime";
            SqlHelper.ExecuteSql(sql, sp);
        }
        public bool Del(int ID)
        {
            return Sql.Del(PK, ID);
        }
        public M_User_UnitWeek SelReturnModel(int ID)
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
            return Sql.Sel(TbName);
        }
        public DataTable SelByUid(int uid)
        {
            string sql = "SELECT * FROM " + TbName + " WHERE UserID=" + uid;
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        //最高一级用户消费,因为无推荐用户，所以会计到一个为0的用户上,这里不将其显示出来
        public DataTable SelByTime()
        {
            string fields = "*,(Select UserName From ZL_User Where UserID=A.UserID) UserName,(Select UserName From ZL_User Where UserID=A.PUserID) PUserName";
            string sql = "Select " + fields + " From " + TbName + " AS A Where UserID>0";
            sql += " ORDER BY UserID ASC";
            return SqlHelper.ExecuteTable(CommandType.Text, sql);
        }
        public void DelByTime(DateTime time)
        {
            string stime, etime; GetWeekSE(time, out stime, out etime);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("stime", stime), new SqlParameter("etime", etime) };
            //DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select Top 1 * From " + TbName + " WHERE UserID=0 AND CDate BetWeen @Stime AND @ETime", sp);
            string sql = "Delete FROM " + TbName + " WHERE CDate BetWeen @stime AND @etime";//State=0 AND
            SqlHelper.ExecuteSql(sql, sp);
            string sql2 = "Update ZL_User_Consume Set UnitPercent='',UnitAmount=0.00,Remind='' WHERE CDate BetWeen @Stime AND @ETime";//STATE=0 AND 
            SqlHelper.ExecuteSql(sql2, sp);
        }
        public DataTable GetNeedUnitDT(DateTime time)
        {
            string stime, etime; GetWeekSE(time, out stime, out etime);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("stime", stime), new SqlParameter("etime", etime) };
            string sql = "SELECT * FROM " + TbName + " WHERE  CDate BetWeen @Stime AND @ETime AND UserID>0";//State=0 AND
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }
        /// <summary>
        /// 获取给定时间的起始周时间与结束周时间
        /// </summary>
        /// <param name="time">需要计算的周时间</param>
        /// <param name="stime">周起始时间,周一起计</param>
        /// <param name="etime">周结束时间,周日23:59</param>
        public static void GetWeekSE(DateTime time, out string stime, out string etime)
        {
            //周日是0,周六是6
            int today = (int)time.DayOfWeek;
            if (today == 0) today = 7;//以周一至周日算
            stime = time.AddDays(-today + 1).ToString("yyyy-MM-dd");
            etime = time.AddDays(7 - today).ToString("yyyy-MM-dd 23:59");
        }
    }
}

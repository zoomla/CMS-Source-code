using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.SQLDAL;
using ZoomLa.Model.Plat;
using System.Data.SqlClient;
using System.Data.Common;
using ZoomLa.BLL.Helper;
using Newtonsoft.Json;

namespace ZoomLa.BLL.Plat
{
    public class B_Blog_Sdl : ZL_Bll_InterFace<M_Blog_Sdl>
    {
        public string TbName, PK;
        public M_Blog_Sdl initMod = new M_Blog_Sdl();
        public B_Blog_Sdl()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }

        public int Insert(M_Blog_Sdl model)
        {
            return DBCenter.Insert(model);
        }

        public bool UpdateByID(M_Blog_Sdl model)
        {
            return DBCenter.UpdateByID(model, model.ID);
        }
        public bool Del(int ID)
        {
            return DBCenter.Del(TbName, PK, ID);
        }

        public M_Blog_Sdl SelReturnModel(int ID)
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

        public DataTable Sel()
        {
            return DBCenter.Sel(TbName, "", "");
        }
        public bool DelByIDS(string ids)
        {
            SafeSC.CheckIDSEx(ids);
            return DBCenter.DelByIDS(TbName, PK, ids);
        }
        /// <summary>
        /// 按月份取数据
        /// </summary>
        public DataTable SelByMonth(DateTime st, int uid, int type = 0) //获取指定日期
        {
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("st", st) };
            string where = "datediff(month,[StartDate],@st)=0 AND Name!=''";//type < 0 ? "" : " AND TaskType=@type";
            if (uid > 0) { where += " AND UserID=" + uid; }
            return DBCenter.Sel(TbName, where, "", sp);
        }
        /// <summary>
        /// 按星期取数据
        /// </summary>
        /// <param name="st"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public DataTable SelByWeek(DateTime st, int uid, int type, int day = 7)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@type", type) };
            string where = type < 0 ? "" : " AND TaskType=@type";
            string sql = "SELECT * FROM " + TbName + " WHERE DATEDIFF(day,[StartDate],'" + st + "')<" + day + " AND UserID=" + uid + where;
            return SqlHelper.ExecuteTable(CommandType.Text, sql, sp);
        }

        /// <summary>
        /// 获取我的最近日程
        /// </summary>
        /// <returns></returns>
        public DataTable SelTopSubject(int uid, int type, int num = 20)
        {
            string where = type < 0 ? "" : " AND TaskType=" + type;
            where = "UserID=" + uid + where;
            int count = 0;
            return DBCenter.SelPage(num, 1, out count, "A." + PK, "A.*,B.Title", TbName, "ZL_MisInfo", "A.TaskType=B.ID", where, "StartDate DESC");
        }
        /// <summary>
        /// 获取我的最近5个,未完成的任务
        /// </summary>
        public DataTable SelMyTop(int uid, int num = 5)
        {
            string suid = "%," + uid + ",%";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("suid", suid) };
            int count = 0;
            return DBCenter.SelPage(1, num, out count, PK, "*", TbName, "LeaderIDS Like @suid OR ParterIDS Like @suid");
        }
        public bool DelByType(int uid, int type)
        {
            return DBCenter.DelByWhere(TbName, "TaskType=" + type + " AND UserID=" + uid);
        }
        //--------------------Special
        public string SelMonthToJson(DateTime date, int uid)
        {
            string stime = "", etime = "";
            DateHelper.GetMonthSE(date, ref stime, ref etime);
            string where = "UserID=" + uid;
            where += " AND (StartDate>='" + stime + "' AND StartDate<='" + etime + "')";
            DataTable dt = DBCenter.SelWithField(TbName, "id,name,startdate", where, "");
            return JsonConvert.SerializeObject(dt);
        }
    }
}

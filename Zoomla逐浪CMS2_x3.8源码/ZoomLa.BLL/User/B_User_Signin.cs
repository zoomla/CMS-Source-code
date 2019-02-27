using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.User
{
    public class B_User_Signin
    {
        public string TbName, PK;
        public M_User_Signin initMod = new M_User_Signin();
        public DataTable dt = null;
        public B_User_Signin()
        {
            TbName = initMod.TbName;
            PK = initMod.PK;
        }
        public int Insert(M_User_Signin model)
        {
            return Sql.insert(TbName, model.GetParameters(), BLLCommon.GetParas(model),BLLCommon.GetFields(model));
        }
        public bool UpdateByID(M_User_Signin model)
        {
            return Sql.UpdateByIDs(TbName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public bool Del(int ID)
        {
            return Sql.Del(TbName, ID);
        }
        public M_User_Signin SelReturnModel(int ID)
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
            return Sql.Sel(TbName, "", "CreateTime Desc");
        }
        /// <summary>
        /// 今天是否签到
        /// </summary>
        /// <returns>True:已签</returns>
        public bool IsSignToday(int uid)
        {
            //select * from "+TbName+" where DateDiff(dd,createtime,getdate())=0
            string sql = "Select ID From " + TbName + " Where DateDiff(dd,CreateTime,getdate())=0 And UserID=" + uid;
            dt = SqlHelper.ExecuteTable(CommandType.Text,sql);
            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// 获取本月已经签到的日期
        /// </summary>
        public string GetHasSignDays(int uid)
        {
            string sql = "Select [ID],CreateTime From ZL_User_Signin Where datediff(month,CreateTime,getdate())=0 And UserID=" + uid;
            dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            string days = "";
            foreach (DataRow dr in dt.Rows)
            {
                days += Convert.ToDateTime(dr["CreateTime"]).Day + ",";
            }
            return days.TrimEnd(',') ;
        }
        /// <summary>
        /// 根据签到次数送积分
        /// </summary>
        public int GetSignCount(int uid, DateTime stime)
        {
            string sql = "Select CreateTime From ZL_User_Signin Where CreateTime<'" + stime.AddDays(1).Date + "' And UserID=" + uid + " Order By CreateTime DESC";
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, sql);
            int count = 1;
            if (dt.Rows.Count < 1 || (stime.Date - Convert.ToDateTime(dt.Rows[0]["CreateTime"]).Date).Days != 0)//如果今天的签到非连续签到
            {
                return count;
            }
            for (int i = 0; i < (dt.Rows.Count - 1); i++)
            {
                DateTime st = Convert.ToDateTime(dt.Rows[i]["CreateTime"]).Date;
                DateTime et = Convert.ToDateTime(dt.Rows[i + 1]["CreateTime"]).Date;
                if ((st - et).Days == 1) count++;
                else if ((st - et).Days == 0) continue;//如果天数重复则忽略
                else break;
            }
            return count;
        }
    }
}

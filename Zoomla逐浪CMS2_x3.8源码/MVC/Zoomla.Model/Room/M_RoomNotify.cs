using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_RoomNotify:M_Base
    {
        #region 构造函数
        public M_RoomNotify()
        {
        }

        public M_RoomNotify
        (
            int ID,
            string NotifyTitle,
            string NotifyContext,
            DateTime AddTime,
            int RoomID,
            int UserID
        )
        {
            this.ID = ID;
            this.NotifyTitle = NotifyTitle;
            this.NotifyContext = NotifyContext;
            this.AddTime = AddTime;
            this.RoomID = RoomID;
            this.UserID = UserID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] RoomNotifyList()
        {
            string[] Tablelist = { "ID", "NotifyTitle", "NotifyContext", "AddTime", "RoomID", "UserID" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 通知标题
        /// </summary>
        public string NotifyTitle { get; set; }
        /// <summary>
        /// 通知内容
        /// </summary>
        public string NotifyContext { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 班级ID
        /// </summary>
        public int RoomID { get; set; }
        /// <summary>
        /// 添加用户ID
        /// </summary>
        public int UserID { get; set; }
        #endregion
        public override string TbName { get { return "ZL_RoomNotify"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"NotifyTitle","NChar","200"},
                                  {"NotifyContext","NChar","1000"}, 
                                  {"AddTime","DateTime","8"},
                                  {"RoomID","Int","4"}, 
                                  {"UserID","Int","4"}
                              };
            return Tablelist;
        }
        public string GetFieldAndPara()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public SqlParameter[] GetParameters(M_RoomNotify model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.NotifyTitle;
            sp[2].Value = model.NotifyContext;
            sp[3].Value = model.AddTime;
            sp[4].Value = model.RoomID;
            sp[5].Value = model.UserID;
            return sp;
        }
        public M_RoomNotify GetModelFromReader(SqlDataReader rdr)
        {
            M_RoomNotify model = new M_RoomNotify();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.NotifyTitle = rdr["SendID"].ToString();
            model.NotifyContext = rdr["InceptID"].ToString();
            model.AddTime = Convert.ToDateTime(rdr["Mcontent"]);
            model.RoomID = Convert.ToInt32(rdr["RestoreID"]);
            model.UserID = Convert.ToInt32(rdr["State"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
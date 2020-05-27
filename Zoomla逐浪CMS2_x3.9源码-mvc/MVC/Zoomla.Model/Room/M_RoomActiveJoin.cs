using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_RoomActiveJoin:M_Base
    {
        #region 构造函数
        public M_RoomActiveJoin()
        {
        }

        public M_RoomActiveJoin
        (
            int ID,
            int ActiveID,
            int UserID,
            string UserName,
            DateTime AddTime
        )
        {
            this.ID = ID;
            this.ActiveID = ActiveID;
            this.UserID = UserID;
            this.UserName = UserName;
            this.AddTime = AddTime;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] RoomActiveJoinList()
        {
            string[] Tablelist = { "ID", "ActiveID", "UserID", "UserName", "AddTime" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 活动ID
        /// </summary>
        public int ActiveID { get; set; }
        /// <summary>
        /// 参与用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        #endregion
        public override string TbName { get { return "ZL_RoomActiveJoin"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"ActiveID","Int","4"},
                                  {"UserID","Int","4"}, 
                                  {"UserName","NChar","255"},
                                  {"AddTime","DateTime","8"}
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

        public SqlParameter[] GetParameters(M_RoomActiveJoin model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.ActiveID;
            sp[2].Value = model.UserID;
            sp[3].Value = model.UserName;
            sp[4].Value = model.AddTime;
            return sp;
        }
        public M_RoomActiveJoin GetModelFromReader(SqlDataReader rdr)
        {
            M_RoomActiveJoin model = new M_RoomActiveJoin();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ActiveID = Convert.ToInt32(rdr["ActiveID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.UserName = rdr["UserName"].ToString();
            model.AddTime = Convert.ToDateTime(rdr["AddTime"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
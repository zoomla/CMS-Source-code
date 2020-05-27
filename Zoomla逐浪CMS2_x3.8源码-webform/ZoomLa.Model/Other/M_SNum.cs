using System;
using System.Data;
using System.Data.SqlClient;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_SNum
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        private int m_s_id;
        /// <summary>
        /// 号码名称
        /// </summary>
        private string m_Sname = string.Empty;
        /// <summary>
        /// 选号者ID
        /// </summary>
        private int m_SUserID;
        /// <summary>
        /// 
        /// </summary>
        private DateTime m_addtime;
        /// <summary>
        /// 
        /// </summary>
        private DateTime m_endtime;
        /// <summary>
        /// 状态0停用，1启用
        /// </summary>
        private int m_states;
        #endregion

        #region 构造函数
        public M_SNum()
        {
        }

        public M_SNum
        (
            int s_id,
            string Sname,
            int SUserID,
            DateTime addtime,
            DateTime endtime,
            int states
        )
        {
            this.m_s_id = s_id;
            this.m_Sname = Sname;
            this.m_SUserID = SUserID;
            this.m_addtime = addtime;
            this.m_endtime = endtime;
            this.m_states = states;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] SNumList()
        {
            string[] Tablelist = { "s_id", "Sname", "SUserID", "addtime", "endtime", "states" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int s_id
        {
            get { return this.m_s_id; }
            set { this.m_s_id = value; }
        }
        /// <summary>
        /// 号码名称
        /// </summary>
        public string Sname
        {
            get { return this.m_Sname; }
            set { this.m_Sname = value; }
        }
        /// <summary>
        /// 选号者ID
        /// </summary>
        public int SUserID
        {
            get { return this.m_SUserID; }
            set { this.m_SUserID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime addtime
        {
            get { return this.m_addtime; }
            set { this.m_addtime = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime endtime
        {
            get { return this.m_endtime; }
            set { this.m_endtime = value; }
        }
        /// <summary>
        /// 状态0停用，1启用
        /// </summary>
        public int states
        {
            get { return this.m_states; }
            set { this.m_states = value; }
        }
        #endregion
        /// <summary>
        /// 表名
        /// </summary>
        public static string TbName = "ZL_SNum";
        /// <summary>
        /// 标识列
        /// </summary>
        public static string PK = "s_id";
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public static string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"s_id","Int","4"},
                                  {"Sname","NVarChar","255"},
                                  {"SUserID","Int","4"},
                                  {"addtime","SmallDateTime","8"}, 
                                  {"endtime","SmallDateTime","8"}, 
                                  {"states","Int","4"}
                                 };
            return Tablelist;
        }

        /// <summary>
        /// 获取字段窜
        /// </summary>
        public static string GetFields()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "],";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 获取参数串
        /// </summary>
        public static string GetParas()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 获取字段=参数
        /// </summary>
        public static string GetFieldAndPara()
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

        public static SqlParameter[] GetParameters(M_SNum model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.s_id;
            sp[1].Value = model.Sname;
            sp[2].Value = model.SUserID;
            sp[3].Value = model.addtime;
            sp[4].Value = model.endtime;
            sp[5].Value = model.states;
            return sp;
        }

        public static M_SNum GetModelFromReader(SqlDataReader rdr)
        {
            M_SNum model = new M_SNum();
            model.s_id = Convert.ToInt32(rdr["s_id"]);
            model.Sname = rdr["Sname"].ToString();
            model.SUserID = Convert.ToInt32(rdr["SUserID"]);
            model.addtime = Convert.ToDateTime(rdr["addtime"]);
            model.endtime = Convert.ToDateTime(rdr["endtime"]);
            model.states = Convert.ToInt32(rdr["states"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }

    }
}



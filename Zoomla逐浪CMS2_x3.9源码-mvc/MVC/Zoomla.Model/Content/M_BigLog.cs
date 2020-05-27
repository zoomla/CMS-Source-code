using System;
using System.Data;
using System.Data.SqlClient;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_BigLog:M_Base
    {
        #region 定义字段
        public int id { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 金钱
        /// </summary>
        public double num { get; set; }
        /// <summary>
        /// 内容ID
        /// </summary>
        public int Cid { get; set; }
        public DateTime Ctime { get; set; }
        public int userID { get; set; }
        #endregion

        #region 构造函数
        public M_BigLog()
        {
            this.title = string.Empty;
            this.type = string.Empty;
        }
        public M_BigLog
        (
            int id,
            string title,
            string type,
            int num,
            int Cid,
            DateTime Ctime,
            int userID
        )
        {
            this.id = id;
            this.title = title;
            this.type = type;
            this.num = num;
            this.Cid = Cid;
            this.Ctime = Ctime;
            this.userID = userID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] BigLogList()
        {
            string[] Tablelist = { "id", "title", "type", "num", "Cid", "Ctime", "userID" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_BigLog"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"title","NVarChar","1000"},
                                  {"type","NVarChar","50"},
                                  {"num","Int","4"}, 
                                  {"Cid","Int","4"},
                                  {"Ctime","DateTime","8"},
                                  {"userID","Int","4"}
                                 };
            return Tablelist;
        }

        /// <summary>
        /// 获取字段窜
        /// </summary>
        public  string GetFields()
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
        public  string GetParas()
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
        public  string GetFieldAndPara()
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

        public override SqlParameter[] GetParameters()
        {
            M_BigLog model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.title;
            sp[2].Value = model.type;
            sp[3].Value = model.num;
            sp[4].Value = model.Cid;
            sp[5].Value = model.Ctime;
            sp[6].Value = model.userID;
            return sp;
        }

        public  M_BigLog GetModelFromReader(SqlDataReader rdr)
        {
            M_BigLog model = new M_BigLog();
            model.id = Convert.ToInt32(rdr["id"]);
            model.title = ConverToStr(rdr["title"]);
            model.type = ConverToStr(rdr["type"]);
            model.num = ConvertToInt(rdr["num"]);
            model.Cid = ConvertToInt(rdr["Cid"]);
            model.Ctime = ConvertToDate(rdr["Ctime"]);
            model.userID = ConvertToInt(rdr["userID"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
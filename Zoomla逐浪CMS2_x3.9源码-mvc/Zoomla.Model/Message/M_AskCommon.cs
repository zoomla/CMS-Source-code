using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace ZoomLa.Model
{
  public class M_AskCommon:M_Base
    {
      public int ID { get; set; }
        /// <summary>
        /// 问题ID
        /// </summary>
      public int AskID { get; set; }
        /// <summary>
        /// 回答ID
        /// </summary>
      public int AswID { get; set; }
        /// <summary>
        /// 评论人
        /// </summary>
      public int UserID { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
      public string Content { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; } 
        public override string TbName { get { return "ZL_AskCommon"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"AskID","Int","4"}, 
                                  {"AswID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"Content","NVarChar","4000"}, 
                                  {"AddTime","DateTime","8"}, 
                                  {"Type","Int","4"}
                                 };
            return Tablelist;
        }
        /// <summary>
        /// 获取字段窜
        /// </summary>
        public string GetFields()
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
        public string GetParas()
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
        public SqlParameter[] GetParameters(M_AskCommon model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                try
                {
                    sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
                }
                catch
                {
                    //throw new Exception(i.ToString());
                    // sp[i] = "@Cwebstatus";
                }
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.AskID;
            sp[2].Value = model.AswID;
            sp[3].Value = model.UserID;
            sp[4].Value = model.Content;
            sp[5].Value = model.AddTime;
            sp[6].Value = model.Type; 
            return sp;
        }
        public M_AskCommon GetModelFromReader(SqlDataReader rdr)
        {
            M_AskCommon model = new M_AskCommon();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.AskID = ConvertToInt(rdr["AskID"]);
            model.AswID = ConvertToInt(rdr["AswID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.Type = ConvertToInt(rdr["Type"]); 
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }  
}

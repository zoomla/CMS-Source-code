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
        public override SqlParameter[] GetParameters()
        {
            M_AskCommon model=this;
            SqlParameter[] sp =GetSP();
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

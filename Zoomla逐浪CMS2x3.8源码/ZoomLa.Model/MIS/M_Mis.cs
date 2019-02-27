using System;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model 
{
    public class M_Mis:M_Base
    {
        #region 定义字段

            public int ID { set; get; }
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { set; get; }
            /// <summary>
            /// 进度
            /// </summary>
            public int Rate { set; get; }
            /// <summary>
            /// 完成日期
            /// </summary>
            public DateTime ComTime { set; get; }
            /// <summary>
            /// 时限，倒数日期
            /// </summary>
            public DateTime limitTime { set; get; }

            /// <summary>
            /// 类型：0事业 ,1财富,2 家庭,3 休闲,4 学习
            /// </summary>
            public int Type { set; get; }

            /// <summary>
            /// 描述
            /// </summary>
            public string Content { set; get; }
            /// 状态：0未启动 ,1进行中,2 已完成
            /// </summary>
            public int Status { set; get; }
            /// 参与人
            /// </summary>
            public string Joiner { set; get; }
            ///创建时间
            /// </summary>
            public DateTime CreateTime { set; get; }
            ///父ID
            /// </summary>
            public int ParentID { set; get; }
            public string Inputer { set; get; }
            public string Pic { set; get; }
        
            #endregion
        #region 构造函数
        public M_Mis()
        {
        }

    
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] Mis()
        {
            string[] Tablelist = { "ID", "Title", "Rate", "ComTime", "limitTime", "Type", "Content", "Status", "Joiner", "CreateTime", "ParentID", "Inputer", "Pic" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_Mis"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Title","NVarChar","255"},
                                  {"Rate","Int","4"},
                                  {"ComTime","DateTime","8"},
                                  {"limitTime","DateTime","8"},
                                  {"Type","Int","4"},
                                  {"Content","NText","4000"},
                                  {"Status","Int","4"},
                                  {"Joiner","NVarChar","255"},
                                  {"CreateTime","DateTime","8"},
                                  {"ParentID","Int","4"},
                                  {"Inputer","NVarChar","255"},
                                  {"Pic","NVarChar","255"}

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

        public override SqlParameter[] GetParameters()
        {
            M_Mis model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Title;
            sp[2].Value = model.Rate;
            sp[3].Value = model.ComTime;
            sp[4].Value = model.limitTime;
            sp[5].Value = model.Type;
            sp[6].Value = model.Content;
            sp[7].Value = model.Status;
            sp[8].Value = model.Joiner;
            sp[9].Value = model.CreateTime;
            sp[10].Value = model.ParentID;
            sp[11].Value = model.Inputer;
            sp[12].Value = model.Pic;
            return sp;
        }
        public M_Mis GetModelFromReader(SqlDataReader rdr)
        {
            M_Mis model = new M_Mis(); 
            model.ID = ConvertToInt(rdr["ID"]);
            model.Title = rdr["Title"].ToString();
            model.Rate = ConvertToInt(rdr["Rate"]);
            model.ComTime = ConvertToDate(rdr["ComTime"].ToString());
            model.limitTime = ConvertToDate(rdr["limitTime"].ToString());
            model.Type = ConvertToInt(rdr["Type"]);
            model.Content = rdr["Content"].ToString();
            model.Status = ConvertToInt(rdr["Status"]);
            model.Joiner = rdr["Joiner"].ToString();
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.ParentID = ConvertToInt(rdr["ParentID"]);
            model.Inputer = ConverToStr(rdr["Inputer"]);
            model.Pic = ConverToStr(rdr["Pic"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
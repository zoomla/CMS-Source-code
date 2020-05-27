using System;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
   public class M_MailSet:M_Base
    {
        #region 定义字段

            public int ID { set; get; }
            ///<summary>
            /// 所属用户ID
            /// </summary>
            public int UserID { set; get; }
            /// <summary>
            /// SMTP
            /// </summary>
            public string Smtp { set; get; }
            /// <summary>
            /// POP3
            /// </summary>
            public string POP3 { set; get; }
            /// <summary>
            /// 邮箱
            /// </summary>
            public string UserMail { set; get; }
            /// <summary>
            /// 密码
            /// </summary>
            public string Password  { set; get; }
            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { set; get; }
             /// <summary>
            /// 状态
            /// </summary>
            public int Status { set; get; } 
             /// <summary>
            /// 默认
            /// </summary>
            public bool IsDefault { set; get; } 
       
            #endregion
        #region 构造函数
        public M_MailSet()
        {
            Smtp = string.Empty;
            POP3 = string.Empty;
            UserMail = string.Empty;
            Password = string.Empty;
        }
    
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] Mis()
        {
            string[] Tablelist = { "ID", "UserID", "Smtp", "POP3", "UserMail", "Password", "CreateTime", "Status", "IsDefault" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_MailSet"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"Smtp","NVarChar","255"},
                                  {"POP3","NVarChar","255"},
                                  {"UserMail","NVarChar","255"},
                                  {"Password","NVarChar","255"}, 
                                  {"CreateTime","DateTime","8"}, 
                                  {"Status","Int","4"},
                                  {"IsDefault","Bit","1"}
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
            M_MailSet model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.Smtp;
            sp[3].Value = model.POP3;
            sp[4].Value = model.UserMail;
            sp[5].Value = model.Password;
            sp[6].Value = model.CreateTime;
            sp[7].Value = model.Status;
            sp[8].Value = model.IsDefault;
            return sp;
        }
        public M_MailSet GetModelFromReader(SqlDataReader rdr)
        {
            M_MailSet model = new M_MailSet(); 
            model.ID = ConvertToInt(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.Smtp = rdr["Smtp"].ToString();
            model.POP3 = rdr["POP3"].ToString();
            model.UserMail = rdr["UserMail"].ToString();
            model.Password = rdr["Password"].ToString(); 
            model.CreateTime = ConvertToDate(rdr["CreateTime"].ToString());
            model.Status = ConvertToInt(rdr["Status"]);
            model.IsDefault = ConverToBool(rdr["IsDefault"].ToString());
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
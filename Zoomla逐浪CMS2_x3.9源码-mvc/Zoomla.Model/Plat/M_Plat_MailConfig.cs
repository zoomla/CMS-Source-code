using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model.Plat
{
    public class M_Plat_MailConfig:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 邮箱POP地址
        /// </summary>
        public string POP { get; set; }
        /// <summary>
        /// 邮箱SMTP地址
        /// </summary>
        public string SMTP { get; set; }
        /// <summary>
        /// 邮箱用户名
        /// </summary>
        public string Acount { get; set; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string Passwd { get; set; }
        public int UserID { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 助记名称
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 接收多少天内的邮件
        /// </summary>
        public int Days { get; set; }
        public override string TbName { get { return "ZL_Plat_MailConfig"; } }

        public override string[,] FieldList()
        {
            string[,] filds ={
                                {"ID","Int","4"},
                                {"POP","NVarChar","200"},
                                {"SMTP","NVarChar","200"},
                                {"Acount","NVarChar","200"},
                                {"Passwd","NVarChar","200"},
                                {"UserID","Int","4"},
                                {"CDate","DateTime","8"},
                                {"Alias","NVarChar","50"},
                                {"Days","Int","4"}
                            };
            return filds;
        }

        public SqlParameter[] GetParameters(M_Plat_MailConfig model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.POP ;
            sp[2].Value = model.SMTP;
            sp[3].Value = model.Acount;
            sp[4].Value = model.Passwd;
            sp[5].Value = model.UserID;
            sp[6].Value = model.CDate;
            sp[7].Value = model.Alias;
            sp[8].Value = model.Days;
            return sp;
        }
        public M_Plat_MailConfig GetModelFromReader(SqlDataReader rdr)
        {
            M_Plat_MailConfig model = new M_Plat_MailConfig();
            model.ID = ConvertToInt(rdr["ID"]);
            model.POP = ConverToStr(rdr["POP"]);
            model.SMTP = ConverToStr(rdr["SMTP"]);
            model.Acount = ConverToStr(rdr["Acount"]);
            model.Passwd = ConverToStr(rdr["Passwd"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Alias = ConverToStr(rdr["Alias"]);
            model.Days = ConvertToInt(rdr["Days"]);
            return model;
        }
    }
}

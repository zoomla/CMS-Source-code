using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    /// <summary>
    /// 邮件订阅用户模型
    /// </summary>
    [Serializable]
    public class M_Mail_BookRead:M_Base
    {
        public int ID { get; set; }

        /// <summary>
        /// 内容ID
        /// </summary>
        public int Gid { get; set; }


        /// <summary>
        /// 用户类型:1为用户,
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime CDate { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        
        /// <summary>
        /// 是否终止订阅:0为否,1为 是
        /// </summary>
        public int IsNotRead
        {
            get; set;
        }

        /// <summary>
        ///状态:0:停用,1:正常,-1退订
        /// </summary>
        public int IsAudit
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许退订:0为否,1为允许
        /// </summary>
        public string Reserve
        {
            get;
            set;
        }
        public string IP { get; set; }
        /// <summary>
        /// 来源网站
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 用户浏览器
        /// </summary>
        public string Browser { get; set; }
        public string EMail { get; set; }
        /// <summary>
        /// 验证代码(用于验证与退订)
        /// </summary>
        public string AuthCode { get; set; }

        public M_Mail_BookRead() { }
        public override string TbName { get { return "ZL_Mail_BookRead"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"title","NVarChar","50"},
                                  {"UserID","Int","4"},
                                  {"CDate","DateTime","8"},
                                  {"endTime","DateTime","8"},
                                  {"IsNotRead","Int","4"}, 
                                  {"isAudit","Int","4"},
                                  {"Reserve","NVarChar","50"},
                                  {"Gid","Int","4"},
                                  {"userType","Int","4"},
                                  {"IP","NVarChar","100" },
                                  {"Source","NVarChar","200"},
                                  {"browser","NVarChar","100" },
                                  {"EMail","NVarChar","100" },
                                  {"AuthCode","NVarChar","100"}
                                 };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Mail_BookRead model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            if (model.EndTime <= DateTime.MinValue) { model.EndTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Title;
            sp[2].Value = model.UserID;
            sp[3].Value = model.CDate;
            sp[4].Value = model.EndTime;
            sp[5].Value = model.IsNotRead;
            sp[6].Value = model.IsAudit;
            sp[7].Value = model.Reserve;
            sp[8].Value = model.Gid;
            sp[9].Value = model.UserType;
            sp[10].Value = model.IP;
            sp[11].Value = model.Source;
            sp[12].Value = model.Browser;
            sp[13].Value = model.EMail;
            sp[14].Value = model.AuthCode;
            return sp;
        }
        public  M_Mail_BookRead GetModelFromReader(SqlDataReader rdr)
        {
            M_Mail_BookRead model = new M_Mail_BookRead();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Title = rdr["Title"].ToString();
            model.UserID = ConvertToInt(rdr["UserId"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.EndTime =ConvertToDate(rdr["EndTime"]);
            model.IsNotRead = ConvertToInt(rdr["IsNotRead"].ToString());
            model.IsAudit = ConvertToInt(rdr["IsAudit"]);
            model.Reserve = rdr["Reserve"].ToString();
            model.Gid = ConvertToInt(rdr["Gid"]);
            model.UserType = ConvertToInt(rdr["UserType"]);
            model.IP = ConverToStr(rdr["IP"]);
            model.Source = ConverToStr(rdr["Source"]);
            model.Browser = ConverToStr(rdr["Browser"]);
            model.EMail = ConverToStr(rdr["EMail"]);
            model.AuthCode = ConverToStr(rdr["AuthCode"]);
            rdr.Close();
            return model;
        }
    }
}
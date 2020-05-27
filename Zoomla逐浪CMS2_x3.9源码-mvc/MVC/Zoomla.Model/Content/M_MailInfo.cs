using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_MailInfo:M_Base
    {
        #region 定义字段
        public int ID { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string MailTitle { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string MailContext { get; set; }
        /// <summary>
        /// 邮件发送时间
        /// </summary>
        public DateTime MailSendTime { get; set; }
        /// <summary>
        /// 邮件添加时间
        /// </summary>
        public DateTime MailAddTime { get; set; }
        /// <summary>
        /// 邮件状态，true:已发送；false:待发送
        /// </summary>
        public bool MailState { get; set; }
        /// <summary>
        /// 收件人邮件地址
        /// </summary>
        public string MailAddRees { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 发件人邮件地址
        /// </summary>
        public string SendMail { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string Files { get; set; }
        /// <summary>
        /// 目标ID
        /// </summary>
        public int MID { get; set; }

        #endregion

        #region 构造函数
        public M_MailInfo()
        {
            this.MailTitle = string.Empty;
            this.MailContext = string.Empty;
            this.MailAddRees = string.Empty;
            this.SendMail = string.Empty;
            this.Files = string.Empty;
        }
        public M_MailInfo
        (
            int ID,
            string MailTitle,
            string MailContext,
            DateTime MailSendTime,
            DateTime MailAddTime,
            bool MailState,
            string MailAddRees,
            int UserID,
            string SendMail,
            string Files

        )
        {
            this.ID = ID;
            this.MailTitle = MailTitle;
            this.MailContext = MailContext;
            this.MailSendTime = MailSendTime;
            this.MailAddTime = MailAddTime;
            this.MailState = MailState;
            this.MailAddRees = MailAddRees;
            this.UserID = UserID;
            this.SendMail = SendMail;
            this.Files = Files;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] MailInfoList()
        {
            string[] Tablelist = { "ID", "MailTitle", "MailContext", "MailSendTime", "MailAddTime", "MailState", "MailAddRees", "UserID", "SendMail", "Files", "MID" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_MailInfo"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"MailTitle","NVarChar","500"},
                                  {"MailContext","Text","4000"},
                                  {"MailSendTime","DateTime","8"}, 
                                  {"MailAddTime","DateTime","8"},
                                  {"MailState","Bit","1"}, 
                                  {"MailAddRees","Text","4000"},
                                  {"UserID","Int","4"},
                                  {"SendMail","NVarChar","255"},
                                  {"Files","NVarChar","500"},
                                  {"MID","Int","4"}

                                 };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_MailInfo model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.MailTitle;
            sp[2].Value = model.MailContext;
            sp[3].Value = model.MailSendTime;
            sp[4].Value = model.MailAddTime;
            sp[5].Value = model.MailState;
            sp[6].Value = model.MailAddRees;
            sp[7].Value = model.UserID;
            sp[8].Value = model.SendMail;
            sp[9].Value = model.Files;
            sp[10].Value = model.MID;
            return sp;
        }

        public  M_MailInfo GetModelFromReader(SqlDataReader rdr)
        {
            M_MailInfo model = new M_MailInfo();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.MailTitle = rdr["MailTitle"].ToString();
            model.MailContext = rdr["Cprovinceno"].ToString();
            model.MailSendTime = Convert.ToDateTime(rdr["MailSendTime"]);
            model.MailAddTime = Convert.ToDateTime(rdr["MailAddTime"]);
            model.MailState = Convert.ToBoolean(rdr["MailState"]);
            model.MailAddRees = rdr["MailAddRees"].ToString();
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.SendMail = rdr["SendMail"].ToString();
            model.Files = rdr["Files"].ToString();
            model.MID = Convert.ToInt32(rdr["MID"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
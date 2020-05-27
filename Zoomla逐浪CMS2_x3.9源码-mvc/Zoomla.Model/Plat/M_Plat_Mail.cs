using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model.Plat
{
    public class M_Plat_Mail:M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 抄送人
        /// </summary>
        public string CCUser { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 附件(虚拟路径)
        /// </summary>
        public string Attach { get; set; }
        /// <summary>
        /// 邮件日期
        /// </summary>
        public DateTime MailDate { get; set; }
        /// <summary>
        /// 创建日期(何时下载的邮件)
        /// </summary>
        public DateTime CDate { get; set; }
        public int Status { get; set; }
        /// <summary>
        /// 邮件所属平台的邮件ID
        /// </summary>
        public string MailID { get; set; }
        /// <summary>
        /// 邮件类型 0:收件,1:发件
        /// </summary>
        public int MailType { get; set; }
        public override string TbName { get { return "ZL_Plat_Mail"; } }
        public override string[,] FieldList()
        {
            string[,] fieds ={
                                {"ID","Int","4"},
                                {"UserID","Int","4"},
                                {"Title","NVarChar","1000"},
                                {"Content","NText","10000"},
                                {"Sender","NVarChar","200"},
                                {"CCUser","NVarChar","500"},
                                {"Receiver","NVarChar","500"},
                                {"Attach","NVarChar","3000"},
                                {"MailDate","DateTime","8"},
                                {"CDate","DateTime","8"},
                                {"MailID","NVarChar","300"},
                                {"Status","Int","4"},
                                {"MailType","Int","4"}
                            };
            return fieds;
        }
        public SqlParameter[] GetParameters(M_Plat_Mail model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.Title;
            sp[3].Value = model.Content;
            sp[4].Value = model.Sender;
            sp[5].Value = model.CCUser;
            sp[6].Value = model.Receiver;
            sp[7].Value = model.Attach;
            sp[8].Value = model.MailDate;
            sp[9].Value = model.CDate;
            sp[10].Value = model.MailID;
            sp[11].Value = model.Status;
            sp[12].Value = model.MailType;
            return sp;
        }
        public M_Plat_Mail GetModelFromReader(SqlDataReader rdr)
        {
            M_Plat_Mail model = new M_Plat_Mail();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.Sender = ConverToStr(rdr["Sender"]);
            model.CCUser = ConverToStr(rdr["CCUser"]);
            model.Receiver = ConverToStr(rdr["Receiver"]);
            model.Attach = ConverToStr(rdr["Attach"]);
            model.MailDate = ConvertToDate(rdr["MailDate"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.MailID = ConverToStr(rdr["MailID"]);
            model.Status = Convert.ToInt32(rdr["Status"]);
            model.MailType = ConvertToInt(rdr["MailType"]);
            return model;
        }

    }
}

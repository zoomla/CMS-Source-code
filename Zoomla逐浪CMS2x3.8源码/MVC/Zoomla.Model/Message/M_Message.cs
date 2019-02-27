namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;
    public class M_Message : M_Base
    {
        /// <summary>
        /// 短消息类型 2:手机短信,3:系统通知信息
        /// </summary>
        public int MsgType { get; set; }
        /// <summary>
        /// 留言ID
        /// </summary>
        public int MsgID
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        ///  内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }
        /// <summary>
        /// 发件人=会员
        /// </summary>
        public string Sender
        {
            get;
            set;
        }
        private string m_Incept = "";
        /// <summary>
        /// 收件人,存储IDS,非用户名
        /// </summary>
        public string Incept
        {
            get
            {
                this.m_Incept = string.IsNullOrEmpty(this.m_Incept) ? "" : "," + (this.m_Incept.Trim(',')) + ",";
                return this.m_Incept;
            }
            set
            {
                this.m_Incept = value;
            }
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime PostDate
        {
            get;
            set;
        }
        /// <summary>
        /// 是否阅读
        /// </summary>
        private bool m_IsNull = false;
        public M_Message(bool value)
        {
            this.m_IsNull = value;
            Sender = ""; Receipt = "";
        }
        public M_Message() { Sender = ""; Receipt = ""; this.m_IsNull = false; }
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
        /// <summary>
        /// 是否发送0未发送,1已发送
        /// </summary>
        public int status
        {
            get;
            set;
        }

        /// <summary>
        /// 是否被删除
        /// </summary>
        public int IsDelInbox
        {
            get;
            set;
        }
        /// <summary>
        ///0：正常,1:草稿
        /// </summary>
        public int Savedata
        {
            get;
            set;
        }
        public int IsDelSendbox
        {
            get;
            set;
        }
        /// <summary>
        /// 已收到
        /// </summary>
        public string Receipt
        {
            get;
            set;
        }
        private string m_CCUser = "";
        /// <summary>
        /// 是否抄送
        /// </summary>
        public string CCUser
        {
            get { this.m_CCUser = string.IsNullOrEmpty(this.m_CCUser) ? "" : "," + (this.m_CCUser.Trim(',')) + ","; return this.m_CCUser; }
            set { this.m_CCUser = value; }
        }
        private string m_ReadUser = "";
        /// <summary>
        /// 阅件人
        /// </summary>
        public string ReadUser
        {
            get
            {
                this.m_ReadUser = string.IsNullOrEmpty(this.m_ReadUser) ? "" : "," + (this.m_ReadUser.Trim(',')) + ",";
                return this.m_ReadUser;
            }
            set
            {
                this.m_ReadUser = value;
            }
        }
        /// <summary>
        /// 邮件附件 多个地址用逗号隔开
        /// </summary>
        public string Attachment
        {
            get;
            set;
        }
        private string _delIDS, _realDelIDS;
        /// <summary>
        /// 已删除该邮件用户列表
        /// </summary>
        public string DelIDS
        {
            get
            {
                if (!string.IsNullOrEmpty(_delIDS))//保持格式为,1,2,3,
                {
                    _delIDS = "," + (_delIDS.Replace(",,", ",").Trim(',')) + ",";
                }
                return _delIDS;
            }
            set { _delIDS = value; }
        }
        /// <summary>
        /// 彻底删除该邮件的用户列表
        /// </summary>
        public string RealDelIDS
        {
            get
            {
                if (!string.IsNullOrEmpty(_realDelIDS))//保持格式为,1,2,3,
                {
                    _realDelIDS = "," + (_realDelIDS.Replace(",,", ",").Trim(',')) + ",";
                }
                return _realDelIDS;
            }
            set { _realDelIDS = value; }
        }
        public override string PK { get { return "MsgID"; } }
        public override string TbName { get { return "ZL_Message"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"MsgID","Int","4"},
                                  {"Title","NVarChar","255"},
                                  {"Content","NText","50000"},
                                  {"Sender","NVarChar","500"}, 
                                  {"Incept","NText","20000"}, 
                                  {"PostDate","DateTime","8"}, 
                                  {"Savedata","Int","4"}, 
                                  {"status","Int","4"}, 
                                  {"IsDelInbox","Int","4"}, 
                                  {"IsDelSendbox","Int","4"}, 
                                  {"receipt","NText","20000"},
                                  {"MsgType","Int","4"},
                                  {"CCUser","NText","20000"},
                                  {"ReadUser","VarChar","8000"},
                                  {"Attachment","NVarChar","4000"},
                                  {"DelIDS","NVarChar","4000"},
                                  {"RealDelIDS","NVarChar","4000"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Message model = this;
            if (model.PostDate <= DateTime.MinValue) { model.PostDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.MsgID;
            sp[1].Value = model.Title;
            sp[2].Value = model.Content;
            sp[3].Value = model.Sender;
            sp[4].Value = model.Incept;
            sp[5].Value = model.PostDate;
            sp[6].Value = model.Savedata;
            sp[7].Value = model.status;
            sp[8].Value = model.IsDelInbox;
            sp[9].Value = model.IsDelSendbox;
            sp[10].Value = model.Receipt;
            sp[11].Value = model.MsgType;
            sp[12].Value = model.CCUser;
            sp[13].Value = model.ReadUser;
            sp[14].Value = model.Attachment;
            sp[15].Value = model.DelIDS;
            sp[16].Value = model.RealDelIDS;
            return sp;
        }
        public M_Message GetModelFromReader(SqlDataReader rdr)
        {
            M_Message model = new M_Message();
            model.MsgID = Convert.ToInt32(rdr["MsgID"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.Sender = ConverToStr(rdr["Sender"]);
            model.Incept = ConverToStr(rdr["Incept"]);
            model.PostDate = ConvertToDate(rdr["PostDate"]);
            model.Savedata = ConvertToInt(rdr["Savedata"]);
            model.status = ConvertToInt(rdr["status"]);
            model.IsDelInbox = ConvertToInt(rdr["IsDelInbox"]);
            model.IsDelSendbox = ConvertToInt(rdr["IsDelSendbox"]);
            model.Receipt = ConverToStr(rdr["Receipt"]);
            model.MsgType = ConvertToInt(rdr["MsgType"]);
            model.CCUser = ConverToStr(rdr["CCUser"]);
            model.ReadUser = ConverToStr(rdr["ReadUser"]);
            model.Attachment = ConverToStr(rdr["Attachment"]);
            model.DelIDS = ConverToStr(rdr["DelIDS"]);
            model.RealDelIDS = ConverToStr(rdr["RealDelIDS"]);
            rdr.Close();
            return model;
        }
    }
}
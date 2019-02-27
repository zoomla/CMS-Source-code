using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    public class M_Common_Notify : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// Guid标识
        /// </summary>
        public string Gid { get; set; }
        /// <summary>
        /// 接收人,UserID字符串
        /// </summary>
        public string ReceUsers { get; set; }
        /// <summary>
        /// 提示类型 2:能力中心消息提示,0:即时信息提示
        /// </summary>
        public int NType { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 指向的内容ID
        /// </summary>
        public int InfoID { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CUser { get; set; }
        public string CUName { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remind { get; set; }
        /// <summary>
        /// 过期时间,0=永久
        /// </summary>
        public int ExpireTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// 开始时间,到了该时间才会加入消息队列
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 0:未执行,99已完成
        /// </summary>
        public int ZStatus { get; set; }
        /// <summary>
        /// 已阅读过的UserIDS(已读列表只用于记录,不用于逻辑判断,读后从ReceUser中移除)
        /// </summary>
        public string ReadedUsers { get; set; }
        /// <summary>
        /// 接收用户的原始字符串,用于展示将提示给哪些用户
        /// </summary>
        public string ReceOrgin { get; set; }
        public M_Common_Notify() { ReceUsers = ""; ReadedUsers = ""; ReceOrgin = ""; ZStatus = 0; NType = 0; ExpireTime = 0; }
        public override string TbName { get { return "ZL_Common_Notify"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Gid","NVarChar","500"},
        		        		{"ReceUsers","VarChar","8000"},
        		        		{"NType","Int","4"},
        		        		{"Title","NVarChar","500"},
        		        		{"Content","NVarChar","4000"},
        		        		{"InfoID","Int","4"},
        		        		{"CUser","Int","4"},
        		        		{"CUName","NVarChar","200"},
        		        		{"Remind","NVarChar","500"},
        		        		{"ExpireTime","Int","4"},
        		        		{"CDate","DateTime","8"},
        		        		{"BeginDate","DateTime","8"},
                                {"ZStatus","Int","4"},
                                {"ReadedUsers","VarChar","8000"},
                                {"ReceOrgin","VarChar","8000"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Common_Notify model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            if (model.BeginDate <= DateTime.MinValue) { model.BeginDate = DateTime.Now; }
            if (string.IsNullOrEmpty(Gid)) { Gid = Guid.NewGuid().ToString(); }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Gid;
            sp[2].Value = model.ReceUsers;
            sp[3].Value = model.NType;
            sp[4].Value = model.Title;
            sp[5].Value = model.Content;
            sp[6].Value = model.InfoID;
            sp[7].Value = model.CUser;
            sp[8].Value = model.CUName;
            sp[9].Value = model.Remind;
            sp[10].Value = model.ExpireTime;
            sp[11].Value = model.CDate;
            sp[12].Value = model.BeginDate;
            sp[13].Value = model.ZStatus;
            sp[14].Value = model.ReadedUsers;
            sp[15].Value = model.ReceOrgin;
            return sp;
        }
        public M_Common_Notify GetModelFromReader(DbDataReader rdr)
        {
            M_Common_Notify model = new M_Common_Notify();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Gid = ConverToStr(rdr["Gid"]);
            model.ReceUsers = ConverToStr(rdr["ReceUsers"]);
            model.NType = ConvertToInt(rdr["NType"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.InfoID = ConvertToInt(rdr["InfoID"]);
            model.CUser = ConvertToInt(rdr["CUser"]);
            model.CUName = ConverToStr(rdr["CUName"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.ExpireTime = ConvertToInt(rdr["ExpireTime"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.BeginDate = ConvertToDate(rdr["BeginDate"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.ReadedUsers = ConverToStr(rdr["ReadedUsers"]);
            model.ReceOrgin = ConverToStr(rdr["ReceOrgin"]);
            rdr.Close();
            return model;
        }
    }

}

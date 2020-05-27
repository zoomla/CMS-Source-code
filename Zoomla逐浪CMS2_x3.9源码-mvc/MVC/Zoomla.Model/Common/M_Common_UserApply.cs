using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    /// <summary>
    /// 通用申请类,用于能力中心与企业认证
    /// </summary>
    public class M_Common_UserApply : M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 管理员审核人ID
        /// </summary>
        public int AdminID { get; set; }
        /// <summary>
        /// 用户审核人ID
        /// </summary>
        public int AdminUserID { get; set; }
        /// <summary>
        /// 申请单类型,中文(plat_joincomp,plat_compcert,plat_applyopen)
        /// </summary>
        public string ZType { get; set; }
        /// <summary>
        /// 处理结果,通过或拒绝原因
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 系统备注
        /// </summary>
        public string Remind { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string UserRemind { get; set; }
        /// <summary>
        /// 管理员备注
        /// </summary>
        public string AdminRemind { get; set; }
        /// <summary>
        /// 状态 0:处理中,99:审核通过，-1:拒绝
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// 审核日期(通过|拒绝)
        /// </summary>
        public string AuditDate { get; set; }
        public string IP { get; set; }
        /// <summary>
        /// 用户的相关信息
        /// </summary>
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string WeChat { get; set; }
        /// <summary>
        /// 多个联系人用|分割
        /// </summary>
        public string Contact { get; set; }
        public string CompName { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// 申请的相关核心内容,五个预留字段
        /// </summary>
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public string Info4 { get; set; }
        public string Info5 { get; set; }
        public override string TbName { get { return "ZL_Common_UserApply"; } }
        public override string PK { get { return "ID"; } }
        public M_Common_UserApply() { Status = 0; }
        public M_Common_UserApply(string type) { ZType = type; Status = 0; }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"UserName","NVarChar","200"},
        		        		{"AdminID","Int","4"},
        		        		{"ZType","NVarChar","100"},
        		        		{"Result","NVarChar","500"},
        		        		{"Remind","NVarChar","4000"},
        		        		{"UserRemind","NVarChar","4000"},
        		        		{"AdminRemind","NVarChar","4000"},
        		        		{"Status","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"AdminUserID","Int","4"},
                                {"AuditDate","NVarChar","200"},
                                {"IP","NVarChar","200"},
                                {"Info1","NVarChar","4000"},
                                {"Info2","NVarChar","4000"},
                                {"Info3","NVarChar","4000"},
                                {"Info4","NVarChar","4000"},
                                {"Info5","NVarChar","4000"},
                                {"Mobile","NVarChar","200"},
                                {"Email","NVarChar","200"},
                                {"QQ","NVarChar","200"},
                                {"WeChat","NVarChar","200"},
                                {"Contact","NVarChar","200"},
                                {"CompName","NVarChar","200"},
                                {"Address","NVarChar","200"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Common_UserApply model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.UserName;
            sp[3].Value = model.AdminID;
            sp[4].Value = model.ZType;
            sp[5].Value = model.Result;
            sp[6].Value = model.Remind;
            sp[7].Value = model.UserRemind;
            sp[8].Value = model.AdminRemind;
            sp[9].Value = model.Status;
            sp[10].Value = model.CDate;
            sp[11].Value = model.AdminUserID;
            sp[12].Value = model.AuditDate;
            sp[13].Value = model.IP;
            sp[14].Value = model.Info1;
            sp[15].Value = model.Info2;
            sp[16].Value = model.Info3;
            sp[17].Value = model.Info4;
            sp[18].Value = model.Info5;
            sp[19].Value = model.Mobile;
            sp[20].Value = model.Email;
            sp[21].Value = model.QQ;
            sp[22].Value = model.WeChat;
            sp[23].Value = model.Contact;
            sp[24].Value = model.CompName;
            sp[25].Value = model.Address;
            return sp;
        }
        public M_Common_UserApply GetModelFromReader(DbDataReader rdr)
        {
            M_Common_UserApply model = new M_Common_UserApply();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.ZType = ConverToStr(rdr["ZType"]);
            model.Result = ConverToStr(rdr["Result"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.UserRemind = ConverToStr(rdr["UserRemind"]);
            model.AdminRemind = ConverToStr(rdr["AdminRemind"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.AdminUserID = ConvertToInt(rdr["AdminUserID"]);
            model.AuditDate = ConverToStr(rdr["AuditDate"]);
            model.IP = ConverToStr(rdr["IP"]);
            model.Info1 = ConverToStr(rdr["Info1"]);
            model.Info2 = ConverToStr(rdr["Info2"]);
            model.Info3 = ConverToStr(rdr["Info3"]);
            model.Info4 = ConverToStr(rdr["Info4"]);
            model.Info5 = ConverToStr(rdr["Info5"]);
            model.Mobile = ConverToStr(rdr["Mobile"]);
            model.Email = ConverToStr(rdr["Email"]);
            model.QQ = ConverToStr(rdr["QQ"]);
            model.WeChat = ConverToStr(rdr["WeChat"]);
            model.Contact = ConverToStr(rdr["Contact"]);
            model.CompName = ConverToStr(rdr["CompName"]);
            model.Address = ConverToStr(rdr["Address"]);
            rdr.Close();
            return model;
        }
        public M_Common_UserApply GetModelFromReader(DataRow rdr)
        {
            M_Common_UserApply model = new M_Common_UserApply();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.ZType = ConverToStr(rdr["ZType"]);
            model.Result = ConverToStr(rdr["Result"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.UserRemind = ConverToStr(rdr["UserRemind"]);
            model.AdminRemind = ConverToStr(rdr["AdminRemind"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.AdminUserID = ConvertToInt(rdr["AdminUserID"]);
            model.AuditDate = ConverToStr(rdr["AuditDate"]);
            model.IP = ConverToStr(rdr["IP"]);
            model.Info1 = ConverToStr(rdr["Info1"]);
            model.Info2 = ConverToStr(rdr["Info2"]);
            model.Info3 = ConverToStr(rdr["Info3"]);
            model.Info4 = ConverToStr(rdr["Info4"]);
            model.Info5 = ConverToStr(rdr["Info5"]);
            model.Mobile = ConverToStr(rdr["Mobile"]);
            model.Email = ConverToStr(rdr["Email"]);
            model.QQ = ConverToStr(rdr["QQ"]);
            model.WeChat = ConverToStr(rdr["WeChat"]);
            model.Contact = ConverToStr(rdr["Contact"]);
            model.CompName = ConverToStr(rdr["CompName"]);
            model.Address = ConverToStr(rdr["Address"]);
            return model;
        }
    }
}

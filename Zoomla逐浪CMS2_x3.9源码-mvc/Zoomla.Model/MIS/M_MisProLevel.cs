using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_MisProLevel:M_Base
    {
        private string _referUser = "", _ccUser = "", _emailAlert = "", _smsAlert = "", _sendMan = "", _referGroup = "", _ccGroup = "", _emailGroup = "", _smsGroup = "";
        public int ID { get; set; }
        /// <summary>
        /// 所属流程ID
        /// </summary>
        public int ProID { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int stepNum { get; set; }
        /// <summary>
        /// 步骤名
        /// </summary>
        public string stepName { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string ReferUser
        {
            get { _referUser =string.IsNullOrEmpty(_referUser)?"": "," + (_referUser.Trim(',')) + ","; return _referUser; }
            set { _referUser = value; }
        }
        /// <summary>
        /// 经办组
        /// </summary>
        public string ReferGroup
        {
            get { _referGroup = string.IsNullOrEmpty(_referGroup) ? "" : "," + (_referGroup.Trim(',')) + ","; return _referGroup; }
            set {  _referGroup = value; }
        }
        /// <summary>
        /// 抄送人
        /// </summary>
        public string CCUser
        {
            get { _ccUser =string.IsNullOrEmpty(_ccUser)?"": "," + (_ccUser.Trim(',')) + ","; return _ccUser; }
            set { _ccUser = value; }
        }
        /// <summary>
        /// 抄送组
        /// </summary>
        public string CCGroup
        {
            get { _ccGroup =string.IsNullOrEmpty(_ccGroup)?"": "," + (_ccGroup.Trim(',')) + ","; return _ccGroup; }
            set { _ccGroup = value; }
        }
        private int _hqoption;
        /// <summary>
        /// 会签选项0:不启用会签,1:启用会签
        /// </summary>
        public int HQoption 
        {
            get { return _hqoption; }
            set { _hqoption=ConvertToInt(value); }
        }
        /// <summary>
        /// 强制转交
        /// </summary>
        public int Qzzjoption { get; set; }
        /// <summary>
        /// 回退选项0:不允许,1:允许回到步一步,2:允许回到之前步骤
        /// </summary>
        public int HToption { get; set; }
        /// <summary>
        /// 转交流程时,需email提醒人
        /// </summary>
        public string EmailAlert
        {
            get { _emailAlert = string.IsNullOrEmpty(_emailAlert) ? "" : "," + (_emailAlert.Trim(',')) + ","; return _emailAlert; }
            set { _emailAlert = value; }
        }
        /// <summary>
        /// 转交流程时,需email提醒组
        /// </summary>
        public string EmailGroup
        {
            get { _emailGroup = string.IsNullOrEmpty(_emailGroup) ? "" : "," + (_emailGroup.Trim(',')) + ","; return _emailGroup; }
            set { _emailGroup = value; }
        }
        /// <summary>
        /// 转交流程时,需短信提醒人
        /// </summary>
        public string SmsAlert
        {
            get { _smsAlert = string.IsNullOrEmpty(_smsAlert) ? "" : "," + (_smsAlert.Trim(',')) + ",";return _smsAlert; }
            set { _smsAlert = value; }
        }
        /// <summary>
        /// 转交流程时,需短信提醒组
        /// </summary>
        public string SmsGroup
        {
            get { _smsGroup = string.IsNullOrEmpty(_smsGroup) ? "" : "," + (_smsGroup.Trim(',')) + ",";return _smsGroup; }
            set { _smsGroup = value; }
        }
        /// <summary>
        /// 原预留,现用于自由流程,存储文档ID
        /// </summary>
        public int BackOption { get; set; }
        /// <summary>
        /// 公共附件选项0:不允许,1:允许
        /// </summary>
        public int PublicAttachOption { get; set; }
        /// <summary>
        /// 私人附件选项
        /// </summary>
        public int PrivateAttachOption { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public string Remind { get; set; }
        /// <summary>
        /// 允许发起人
        /// </summary>
        public string SendMan
        {
            get { _sendMan = "," + (_sendMan.Trim(',')) + ","; return _sendMan; }
            set { _sendMan = value; }
        }
        private string _editfield = "";
        /// <summary>
        /// 当前步骤下,允许主办人修改的字段列表
        /// </summary>
        public string CanEditField 
        {
            get { _editfield = string.IsNullOrEmpty(_editfield) ? "" : "," + (_editfield.Trim(',')) + ","; return _editfield; }
            set { _editfield = value; }
        }
        /// <summary>
        /// 已改为,下一步骤权限,refer:经办人,sender:主办人,all:经办和主办(自由流程为主办人权限)
        /// </summary>
        public string DocAuth { get; set; }
        public override string TbName { get { return "ZL_MisProLevel"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                        {"ID","Int","4"},
        	            {"ProID","Int","4"},            
                        {"stepNum","Int","4"},            
                        {"stepName","NVarChar","50"},            
                        {"ReferUser","NVarChar","255"},
                        {"CCUser","NVarChar","255"}, 
                        {"HQoption","Int","4"},            
                        {"Qzzjoption","Int","4"},            
                        {"HToption","Int","4"},            
                        {"EmailAlert","NVarChar","200"},
                        {"SmsAlert","NVarChar","200"},
                        {"BackOption","Int","4"},            
                        {"PublicAttachOption","Int","4"},            
                        {"PrivateAttachOption","Int","4"},            
                        {"Status","Int","4"},            
                        {"CreateTime","DateTime","8"},            
                        {"Remind","NVarChar","100"} ,
                        {"SendMan","NVarChar","255"},
                        {"ReferGroup","NVarChar","255"},
                        {"CCGroup","NVarChar","255"},
                        {"EmailGroup","NVarChar","200"},
                        {"SmsGroup","NVarChar","200"},
                        {"CanEditField","VarChar","1000"},
                        {"DocAuth","VarChar","500"}
              
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_MisProLevel model = this;
            if (string.IsNullOrEmpty(model.DocAuth)) { model.DocAuth = "refer"; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ProID;
            sp[2].Value = model.stepNum;
            sp[3].Value = model.stepName;
            sp[4].Value = model.ReferUser;
            sp[5].Value = model.CCUser;
            sp[6].Value = model.HQoption;
            sp[7].Value = model.Qzzjoption;
            sp[8].Value = model.HToption;
            sp[9].Value = model.EmailAlert;
            sp[10].Value = model.SmsAlert;
            sp[11].Value = model.BackOption;
            sp[12].Value = model.PublicAttachOption;
            sp[13].Value = model.PrivateAttachOption;
            sp[14].Value = model.Status;
            sp[15].Value = model.CreateTime;
            sp[16].Value = model.Remind;
            sp[17].Value = model.SendMan;
            sp[18].Value = model.ReferGroup;
            sp[19].Value = model.CCGroup;
            sp[20].Value = model.EmailGroup;
            sp[21].Value = model.SmsGroup;
            sp[22].Value = model.CanEditField;
            sp[23].Value = model.DocAuth;
            return sp;
        }
        public M_MisProLevel GetModelFromReader(SqlDataReader rdr)
        {
            M_MisProLevel model = new M_MisProLevel();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ProID = Convert.ToInt32(rdr["ProID"]);
            model.stepNum = ConvertToInt(rdr["stepNum"]);
            model.stepName = ConverToStr(rdr["stepName"]);
            model.ReferUser = ConverToStr(rdr["ReferUser"]);
            model.CCUser = ConverToStr(rdr["CCUser"]);
            model.HQoption = ConvertToInt(rdr["HQoption"]);
            model.Qzzjoption = ConvertToInt(rdr["Qzzjoption"]);
            model.HToption = ConvertToInt(rdr["HToption"]);
            model.EmailAlert = ConverToStr(rdr["EmailAlert"]);
            model.SmsAlert = ConverToStr(rdr["SmsAlert"]);
            model.BackOption = ConvertToInt(rdr["BackOption"]);
            model.PublicAttachOption = ConvertToInt(rdr["PublicAttachOption"]);
            model.PrivateAttachOption = ConvertToInt(rdr["PrivateAttachOption"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.SendMan = ConverToStr(rdr["SendMan"]);
            model.ReferGroup = ConverToStr(rdr["ReferGroup"]);
            model.CCGroup = ConverToStr(rdr["CCGroup"]);
            model.EmailGroup = ConverToStr(rdr["EmailGroup"]);
            model.SmsGroup = ConverToStr(rdr["SmsGroup"]);
            model.CanEditField = ConverToStr(rdr["CanEditField"]);
            model.DocAuth = ConverToStr(rdr["DocAuth"]);
            rdr.Close();
            return model;
        }
        public M_MisProLevel GetModelFromDR(DataRow rdr) 
        {
            M_MisProLevel model = new M_MisProLevel();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ProID = Convert.ToInt32(rdr["ProID"]);
            model.stepNum = Convert.ToInt32(rdr["stepNum"]);
            model.stepName = ConverToStr(rdr["stepName"]);
            model.ReferUser = ConverToStr(rdr["ReferUser"]);
            model.CCUser = ConverToStr(rdr["CCUser"]);
            model.HQoption = Convert.ToInt32(rdr["HQoption"]);
            model.Qzzjoption = Convert.ToInt32(rdr["Qzzjoption"]);
            model.HToption = Convert.ToInt32(rdr["HToption"]);
            model.EmailAlert = ConverToStr(rdr["EmailAlert"]);
            model.SmsAlert = ConverToStr(rdr["SmsAlert"]);
            model.BackOption = Convert.ToInt32(rdr["BackOption"]);
            model.PublicAttachOption = Convert.ToInt32(rdr["PublicAttachOption"]);
            model.PrivateAttachOption = Convert.ToInt32(rdr["PrivateAttachOption"]);
            model.Status = Convert.ToInt32(rdr["Status"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.SendMan = ConverToStr(rdr["SendMan"]);
            model.ReferGroup = ConverToStr(rdr["ReferGroup"]);
            model.CCGroup = ConverToStr(rdr["CCGroup"]);
            model.EmailGroup = ConverToStr(rdr["EmailGroup"]);
            model.SmsGroup = ConverToStr(rdr["SmsGroup"]);
            model.CanEditField = ConverToStr(rdr["CanEditField"]);
            model.DocAuth = ConverToStr(rdr["DocAuth"]);
            return model;
        }
        public bool Auth(AuthEnum e, DataTable userDT)
        {
            switch (e)
            {
                case AuthEnum.Refer:
                    if (ReferGroup.Contains("," + userDT.Rows[0]["GroupID"] + ",") || ReferUser.Contains("," + userDT.Rows[0]["UserID"] + ","))
                        return true;
                    else
                        return false;
                case AuthEnum.CCUser:
                    if (CCGroup.Contains("," + userDT.Rows[0]["GroupID"] + ",") || CCUser.Contains("," + userDT.Rows[0]["UserID"] + ","))
                        return true;
                    else
                        return false;
            }
            return false;
        }
        public enum AuthEnum { Refer, CCUser };
    }
}

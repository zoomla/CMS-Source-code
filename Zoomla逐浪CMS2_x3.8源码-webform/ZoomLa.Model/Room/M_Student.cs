using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    [Serializable]
    //ZL_Student
    public class M_Student:M_Base
    {

        /// <summary>
        /// 记录ID
        /// </summary>	
        public int Noteid
        {
            get;
            set;
        }
        /// <summary>
        /// UserID
        /// </summary>	
        public int UserID
        {
            get;
            set;
        }
        /// <summary>
        /// UserName
        /// </summary>	
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 学校ID
        /// </summary>	
        public int SchoolID
        {
            get;
            set;
        }
        /// <summary>
        /// 班级ID
        /// </summary>	
        public int RoomID
        {
            get;
            set;
        }
        /// <summary>
        /// 加入时间
        /// </summary>	
        public DateTime Addtime
        {
            get;
            set;
        }
        
        public int StatusType
        {
            get;
            set;
        }
        /// <summary>
        /// 学生属性：1、学生 2、教师
        /// </summary>	
        public int StudentType
        {
            get;
            set;
        }
        /// <summary>
        /// 学生属性：是学生或班干部；StatusType属性为1时。此属性值有效
        /// </summary>	
        public string StudentTypeTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 审核用户-1:未申请 0：审核中，1：审核通过
        /// </summary>	
        public int Auditing
        {
            get;
            set;
        }
        /// <summary>
        /// 申请理由
        /// </summary>	
        public string AuditingContext
        {
            get;
            set;
        }
        /// <summary>
        /// 审核用户ID
        /// </summary>	
        public int AuditingUserID
        {
            get;
            set;
        }
        /// <summary>
        /// 审核用户用户名
        /// </summary>	
        public string AuditingUserName
        {
            get;
            set;
        }
        public M_Student()
        {

        }

        public override string TbName { get { return "ZL_Exam_Student"; } }
        public override string PK { get { return "Noteid"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                        {"Noteid","Int","4" },
        	            {"UserID","Int","4"},            
                        {"UserName","NVarChar","255"},            
                        {"SchoolID","Int","4"},            
                        {"RoomID","Int","4"},            
                        {"Addtime","DateTime","8"},            
                        {"StatusType","Int","4"},            
                        {"StudentType","Int","4"},            
                        {"StudentTypeTitle","NVarChar","50"},            
                        {"Auditing","Int","4"},            
                        {"AuditingContext","NVarChar","50"},            
                        {"AuditingUserID","Int","4"},            
                        {"AuditingUserName","NVarChar","255"}            
              
        };
            return Tablelist;
        }
        
        public SqlParameter[] GetParameters(M_Student model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Noteid;
            sp[1].Value = model.UserID;
            sp[2].Value = model.UserName;
            sp[3].Value = model.SchoolID;
            sp[4].Value = model.RoomID;
            sp[5].Value = model.Addtime;
            sp[6].Value = model.StatusType;
            sp[7].Value = model.StudentType;
            sp[8].Value = model.StudentTypeTitle;
            sp[9].Value = model.Auditing;
            sp[10].Value = model.AuditingContext;
            sp[11].Value = model.AuditingUserID;
            sp[12].Value = model.AuditingUserName;
            return sp;
        }
        public M_Student GetModelFromReader(SqlDataReader rdr)
        {
            M_Student model = new M_Student();
            model.Noteid = Convert.ToInt32(rdr["Noteid"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.UserName = rdr["UserName"].ToString();
            model.SchoolID = Convert.ToInt32(rdr["SchoolID"]);
            model.RoomID = ConvertToInt(rdr["RoomID"]);
            model.Addtime = Convert.ToDateTime(rdr["Addtime"]);
            model.StatusType = Convert.ToInt32(rdr["StatusType"]);
            model.StudentType = Convert.ToInt32(rdr["StudentType"]);
            model.StudentTypeTitle = rdr["StudentTypeTitle"].ToString();
            model.Auditing = ConvertToInt(rdr["Auditing"]);
            model.AuditingContext = rdr["AuditingContext"].ToString();
            model.AuditingUserID = Convert.ToInt32(rdr["AuditingUserID"]);
            model.AuditingUserName = rdr["AuditingUserName"].ToString();
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
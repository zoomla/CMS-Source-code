using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Recruitment:M_Base
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 学员姓名
        /// </summary>
        public string Studioname { get; set; }
        /// <summary>
        /// 预设用户名
        /// </summary>
        public string PriorUserName { get; set; }
        /// <summary>
        /// 预设登录密码
        /// </summary>
        public string LogPassWord { get; set; }
        /// <summary>
        /// 招生员ID
        /// </summary>
        public int TechID { get; set; }
        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseID { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        public int ClassID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Addinfo { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string CradNo { get; set; }
        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime WriteTime { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 招生员记录关联字段
        /// </summary>
        public int EnrolllistID { get; set; }
        #endregion

        #region 构造函数
        public M_Recruitment()
        {
        }

        public M_Recruitment
        (
            int id,
            string Studioname,
            string PriorUserName,
            string LogPassWord,
            int TechID,
            int CourseID,
            int ClassID,
            string Remark,
            string Tel,
            string Addinfo,
            string CradNo,
            DateTime WriteTime,
            DateTime AddTime,
            int EnrolllistID
        )
        {
            this.id = id;
            this.Studioname = Studioname;
            this.PriorUserName = PriorUserName;
            this.LogPassWord = LogPassWord;
            this.TechID = TechID;
            this.CourseID = CourseID;
            this.ClassID = ClassID;
            this.Remark = Remark;
            this.Tel = Tel;
            this.Addinfo = Addinfo;
            this.CradNo = CradNo;
            this.WriteTime = WriteTime;
            this.AddTime = AddTime;
            this.EnrolllistID = EnrolllistID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] RecruitmentList()
        {
            string[] Tablelist = { "id", "Studioname", "PriorUserName", "LogPassWord", "TechID", "CourseID", "ClassID", "Remark", "Tel", "Addinfo", "CradNo", "WriteTime", "AddTime", "EnrolllistID" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_Recruitment"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"Studioname","NVarChar","50"},
                                  {"PriorUserName","NVarChar","50"},
                                  {"LogPassWord","NVarChar","50"},
                                  {"TechID","Int","4"},
                                  {"CourseID","Int","4"},
                                  {"ClassID","Int","4"},
                                  {"Remark","NVarChar","1000"},
                                  {"AddTime","DateTime","8"} 
                                 };
            return Tablelist;
        }

        /// <summary>
        /// 获取字段窜
        /// </summary>
        public  string GetFields()
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
        public  string GetParas()
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
        public  string GetFieldAndPara()
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

        public  SqlParameter[] GetParameters(M_Recruitment model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.Studioname;
            sp[2].Value = model.PriorUserName;
            sp[3].Value = model.LogPassWord;
            sp[4].Value = model.TechID;
            sp[5].Value = model.CourseID;
            sp[6].Value = model.ClassID;
            sp[7].Value = model.Remark;
            sp[8].Value = model.AddTime;
            return sp;
        }

        public  M_Recruitment GetModelFromReader(SqlDataReader rdr)
        {
            M_Recruitment model = new M_Recruitment();
            model.id = Convert.ToInt32(rdr["ID"]);
            model.Studioname = ConverToStr(rdr["Studioname"]);
            model.PriorUserName = ConverToStr(rdr["PriorUserName"]);
            model.LogPassWord = ConverToStr(rdr["LogPassWord"]);
            model.TechID = ConvertToInt(rdr["TechID"]);
            model.CourseID = ConvertToInt(rdr["CourseID"]);
            model.ClassID = ConvertToInt(rdr["ClassID"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
	
  }




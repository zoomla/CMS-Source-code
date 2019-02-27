using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ZoomLa.Model 
{
    public  class M_MisApproval:M_Base
    {  
        #region 定义字段
        public int ID { get;set;}
        /// <summary>
        /// 申请人
        /// </summary>
        public string Inputer { get; set; }
        ///<summary>
        ///申请部门
        ///<summary>
        public string department{ get; set; }
        ///<summary>
        ///流程
        ///<summary>
        public int PID { get; set; }
        ///<summary>
        ///申请内容
        ///<summary>
        public string  content { get; set; }
        ///<summary>
        ///审核人
        ///<summary>
        public string Approver { get; set; }
        ///<summary>
        ///创建时间
        ///<summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 审批状态,0未开始审批,1:审批中,-1:不同意,99:流程完成
        /// </summary>
        public int Results { get; set; }
        public string ResultStr 
        {
            get 
            {
                string result = "";
                switch (Results)
                {
                    case -1:
                        result = "不同意";
                        break;
                    case 0:
                        result = "未审批";
                        break;
                    case 1:
                        result = "审批中";
                        break;
                    case 99:
                        result = "同意";
                        break;
                    default:
                        result = "未审批";
                        break; 
                }
                return result;
            }
        }
        /// <summary>
        /// 抄送的用户名
        /// </summary>
        public string Send{ get; set; }
        /// <summary>
        /// 流程
        /// </summary>
        public int ProcedureID{ get ; set;}

        #endregion
        #region 构造函数
        public M_MisApproval()
        {
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] MisApproval()
        {
            string[] Tablelist = { "ID", "Inputer","department", "PID", "content", "Approver", "CreateTime","Results"};
            return Tablelist;
        }
          #endregion
        public override string TbName { get { return "ZL_MisApproval"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Inputer","NVarChar","255"},
                                  {"department","NVarChar","255"},
                                  {"PID","Int","4"},
                                  {"content","NText","4000"},
                                  {"Approver","NVarChar","255"},
                                  {"CreateTime","DateTime","8"},
                                  {"Results","Int","4"},
                                  {"Send","NVarChar","255"},
                                  {"ProcedureID","Int","4"}
                              };
            return Tablelist;
        }
        /// <summary>
        /// 获取字段窜
        /// </summary>
        public string GetFields()
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
        public string GetParas()
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
        public string GetFieldAndPara()
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

        public override SqlParameter[] GetParameters()
        {
            M_MisApproval model = this;
            string[,] strArr = FieldList();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Inputer;
            sp[2].Value = model.department;
            sp[3].Value = model.PID;
            sp[4].Value = model.content;
            sp[5].Value = model.Approver;
            sp[6].Value = model.CreateTime;
            sp[7].Value = model.Results;
            sp[8].Value = model.Send;
            sp[9].Value = model.ProcedureID;
            return sp;
        }
        public M_MisApproval GetModelFromReader(SqlDataReader rdr)
        {
            M_MisApproval model = new M_MisApproval();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Inputer = rdr["Inputer"].ToString();
            model.department = rdr["department"].ToString();
            model.PID = ConvertToInt(rdr["PID"]);
            model.content = rdr["content"].ToString();
            model.Approver = rdr["Approver"].ToString();
            model.CreateTime = ConvertToDate(rdr["CreateTime"].ToString());
            model.Results = ConvertToInt(rdr["Results"].ToString());
            model.Send = rdr["Send"].ToString();
            model.ProcedureID = ConvertToInt(rdr["ProcedureID"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}

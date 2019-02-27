using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ZoomLa.Model
{
    public class M_CrmAuth : M_Base
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>		
        public int RoleID { get; set; }
        /// <summary>
        /// 允许增加自定义选项
        /// </summary>		
        public string AllowOption { get; set; }
        /// <summary>
        /// 允许增加修改选项的值
        /// </summary>		
        public string AllowOptionValue { get; set; }
        /// <summary>
        /// 能否导入Excel
        /// </summary>		
        public string AllowExcel { get; set; }
        /// <summary>
        /// 允许添加客户
        /// </summary>		
        public string AllowAddClient { get; set; }
        /// <summary>
        /// 只能看到自己的客户
        /// </summary>		
        /// <summary>
        /// true可以看到所有人，false只能看到自己:1与0
        /// </summary>
        public string AllCustomer { get; set; }
        /// <summary>
        /// 允许指定跟进人
        /// </summary>		
        public string AssignFPMan { get; set; }
        /// <summary>
        /// 能否回复所有客户的跟进信息
        /// </summary>		
        public string AllowFPAll { get; set; }
        /// <summary>
        /// 是否销售人员,用于Excel导入分配跟进人员
        /// </summary>
        public string IsSalesMan { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>		
        public string Add_Man { get; set; }
        /// <summary>
        /// 添加日期
        /// </summary>		
        public DateTime Add_Date { get; set; }
        /// <summary>
        /// 节点ID(预留)
        /// </summary>		
        public string NodeID { get; set; }
        /// <summary>
        /// 备注(预留)
        /// </summary>		
        public string Remind { get; set; }
        public override string TbName { get { return "ZL_CrmAuthList"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  //{"ID","Int","4"},
                                     {"RoleID","NVarChar","10"},
                                     {"AllowOption","NVarChar","10"},
                                     {"AllowOptionValue","NVarChar","10"},
                                     {"AllowExcel","NVarChar","10"},
                                     {"AllowAddClient","NVarChar","10"},
                                     {"AllCustomer","NVarChar","10"},
                                     {"AssignFPMan","NVarChar","10"},
                                     {"AllowFPAll","NVarChar","10"},
                                     {"IsSalesMan","NVarChar","10"},
                                     {"Add_Man","NVarChar","10"},
                                     {"Add_Date","DateTime","100"},
                                     {"NodeID","NVarChar","255"},
                                     {"Remind","NVarChar","50"}
                                 };
            return Tablelist;
        }
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
            M_CrmAuth model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.RoleID;
            sp[1].Value = model.AllowOption;
            sp[2].Value = model.AllowOptionValue;
            sp[3].Value = model.AllowExcel;
            sp[4].Value = model.AllowAddClient;
            sp[5].Value = model.AllCustomer;
            sp[6].Value = model.AssignFPMan;
            sp[7].Value = model.AllowFPAll;
            sp[8].Value = model.IsSalesMan;
            sp[9].Value = model.Add_Man;
            sp[10].Value = model.Add_Date;
            sp[11].Value = model.NodeID;
            sp[12].Value = model.Remind;
            return sp;
        }
        public M_CrmAuth GetModelFromReader(SqlDataReader rdr)
        {
            M_CrmAuth model = new M_CrmAuth();
            model.RoleID = Convert.ToInt32(rdr["RoleID"]);
            model.AllowOption = ConverToStr(rdr["AllowOption"]);
            model.AllowOptionValue = ConverToStr(rdr["AllowOptionValue"]);
            model.AllowExcel = ConverToStr(rdr["AllowExcel"]);
            model.AllowAddClient = ConverToStr(rdr["AllowAddClient"]);
            model.AllCustomer = ConverToStr(rdr["AllCustomer"]);
            model.AssignFPMan = ConverToStr(rdr["AssignFPMan"]);
            model.AllowFPAll = ConverToStr(rdr["AllowFPAll"]);
            model.IsSalesMan = ConverToStr(rdr["IsSalesMan"]);
            model.Add_Man = ConverToStr(rdr["Add_Man"]);
            model.Add_Date = ConvertToDate(rdr["Add_Date"]);
            model.NodeID = ConverToStr(rdr["NodeID"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}

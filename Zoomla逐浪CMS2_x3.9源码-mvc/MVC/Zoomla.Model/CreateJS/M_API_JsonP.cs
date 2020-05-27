using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

/*
 * 通过配置,开指定表的Json与JsonP调用权限
 */
namespace ZoomLa.Model.CreateJS
{
    public class M_API_JsonP : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 别名,给予前端调用
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 0:全开放,1:需要key验证(主用于手机json调用)
        /// </summary>
        public int AuthType { get; set; }
        public string MyPK { get; set; }
        public string Fields { get; set; }
        /// <summary> 主表,次表
        /// </summary>
        public string T1 { get; set; }
        public string T2 { get; set; }
        public string WhereStr { get; set; }
        public string ONStr { get; set; }
        public string OrderStr { get; set; }
        /// <summary>
        ///     
        /// </summary>
        public string Params { get; set; }
        /// <summary>
        /// 0:关闭,1:启用
        /// </summary>
        public int MyState { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }
        /// <summary>
        /// 添加的管理员ID
        /// </summary>
        public int AdminID { get; set; }
        public DateTime CDate { get; set; }
        public string Remark { get; set; }
        public override string TbName { get { return "ZL_API_JsonP"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Alias","NVarChar","50"},
        		        		{"PK","NVarChar","50"},
        		        		{"Fields","NVarChar","1000"},
        		        		{"T1","NVarChar","2000"},
        		        		{"T2","NVarChar","2000"},
        		        		{"WhereStr","NVarChar","1000"},
        		        		{"ONStr","NVarChar","1000"},
        		        		{"OrderStr","NVarChar","1000"},
        		        		{"Params","VarChar","4000"},
        		        		{"MyState","Int","4"},
        		        		{"Remind","NVarChar","200"},
        		        		{"AdminID","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"Remark","NVarChar","1000"}
        };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_API_JsonP model)
        {
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Alias;
            sp[2].Value = model.MyPK;
            sp[3].Value = model.Fields;
            sp[4].Value = model.T1;
            sp[5].Value = model.T2;
            sp[6].Value = model.WhereStr;
            sp[7].Value = model.ONStr;
            sp[8].Value = model.OrderStr;
            sp[9].Value = model.Params;
            sp[10].Value = model.MyState;
            sp[11].Value = model.Remind;
            sp[12].Value = model.AdminID;
            sp[13].Value = model.CDate;
            sp[14].Value = model.Remark;
            return sp;
        }
        public M_API_JsonP GetModelFromReader(SqlDataReader rdr)
        {
            M_API_JsonP model = new M_API_JsonP();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Alias = ConverToStr(rdr["Alias"]);
            model.MyPK = ConverToStr(rdr["PK"]);
            model.Fields = ConverToStr(rdr["Fields"]);
            model.T1 = ConverToStr(rdr["T1"]);
            model.T2 = ConverToStr(rdr["T2"]);
            model.WhereStr = ConverToStr(rdr["WhereStr"]);
            model.ONStr = ConverToStr(rdr["ONStr"]);
            model.OrderStr = ConverToStr(rdr["OrderStr"]);
            model.Params = ConverToStr(rdr["Params"]);
            model.MyState = ConvertToInt(rdr["MyState"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            rdr.Close();
            return model;
        }
    }
    public class M_API_Param
    {
        public string name = "";
        public string type = "";//like,int,string
        public string defval = "";//不传参时默认值
        public string desc = "";
        //public bool IsRequired = false;//是否必须
    }
}

using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Client_Basic:M_Base
    {
        #region 定义字段
        public int Flow { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string P_name { get; set; }
        /// <summary>
        /// 助记名称
        /// </summary>
        public string P_alias { get; set; }
        /// <summary>
        /// 上级客户
        /// </summary>
        public string Client_Upper { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Add_Date { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 客户类型
        /// </summary>
        public string Client_Type { get; set; }
        /// <summary>
        /// 跟进人
        /// </summary>
        public int FPManID { get; set; }
        /// <summary>
        /// 模型内容id
        /// </summary>
        public int ItemID { get; set; }
        #endregion
        public override string PK { get { return "Flow"; } }
        public override string TbName { get { return "ZL_Client_Basic"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Flow","Int","4"},
                                  {"Code","VarChar","50"},
                                  {"P_name","NVarChar","50"}, 
                                  {"P_alias","VarChar","50"},
                                  {"Client_Upper","NVarChar","50"},
                                  {"Add_Date","DateTime","8"}, 
                                  {"Title","NVarChar","1000"},
                                  {"Client_Type","VarChar","50"},
                                  {"FPManID","Int","4"},
                                  {"ItemID","Int","4" }
                                 }; 	
            return Tablelist;
        }
        public void EmptyData()
        {
            if (Add_Date <= DateTime.MinValue) Add_Date = DateTime.Now;
            if (string.IsNullOrEmpty(Code)) { Code = DateTime.Now.ToString("yyyyMMddHHmmss"); }
        }
        public SqlParameter[] GetParameters(M_Client_Basic model)
        {
            EmptyData();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Flow;
            sp[1].Value = model.Code;
            sp[2].Value = model.P_name;
            sp[3].Value = model.P_alias;
            sp[4].Value = model.Client_Upper;
            sp[5].Value = model.Add_Date;
            sp[6].Value = model.Title;
            sp[7].Value = model.Client_Type;
            sp[8].Value = model.FPManID;
            sp[9].Value = model.ItemID;
            return sp;
        }
        public M_Client_Basic GetModelFromReader(SqlDataReader rdr)
        {
            M_Client_Basic model = new M_Client_Basic();
            model.Flow = Convert.ToInt32(rdr["Flow"]);
            model.Code = ConverToStr(rdr["Code"]);
            model.P_name = ConverToStr(rdr["P_name"]);
            model.P_alias = ConverToStr(rdr["P_alias"]);
            model.Client_Upper = ConverToStr(rdr["Client_Upper"]);
            model.Client_Type = ConverToStr(rdr["Client_Type"]);
            model.Add_Date = ConvertToDate(rdr["Add_Date"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.FPManID = ConvertToInt(rdr["FPManID"]);
            model.ItemID = ConvertToInt(rdr["ItemID"]);
            rdr.Close();
            return model;
        }
    }
}




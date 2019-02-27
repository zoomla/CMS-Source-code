using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Client_Enterprise : M_Base
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        public int Flow { get; set; }
        /// <summary>
        /// 企业编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 开户银行
        /// </summary>
        public string Bank_Open { get; set; }
        /// <summary>
        /// 银行帐号
        /// </summary>
        public string Bank_Code { get; set; }
        /// <summary>
        /// 税号
        /// </summary>
        public string Tax_Code { get; set; }
        /// <summary>
        /// 行业地位
        /// </summary>
        public string Co_Status { get; set; }
        /// <summary>
        /// 公司规模
        /// </summary>
        public string Co_Size { get; set; }
        /// <summary>
        /// 经营状态
        /// </summary>
        public string Co_Management { get; set; }

        /// <summary>
        /// 注册资本
        /// </summary>
        public double Reg_Capital { get; set; }
        /// <summary>
        /// 年销售额
        /// </summary>
        public double Sales { get; set; }
        /// <summary>
        /// 业务范围
        /// </summary>
        public string Operation_Bound { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        public string Homepage { get; set; }
        /// <summary>
        /// 通讯地址
        /// </summary>
        public string Message_Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 传真号码
        /// </summary>
        public string Fax_phone { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCodeW { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 省/市/自治区
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 市/县/区/旗
        /// </summary>
        public string city { get; set; }
        public DateTime Add_Date { get; set; }
        #endregion

        #region 构造函数
        public M_Client_Enterprise()
        {
        }

        public M_Client_Enterprise
        (
            int Flow,
            string Code,
            string Bank_Open,
            string Bank_Code,
            string Tax_Code,
            string Co_Status,
            string Co_Size,
            string Co_Management,
            double Reg_Capital,
            double Sales,
            string Operation_Bound,
            string Homepage,
            string Message_Address,
            string Phone,
            string Fax_phone,
            string ZipCodeW,
            string country,
            string province,
            string city
        )
        {
            this.Flow = Flow;
            this.Code = Code;
            this.Bank_Open = Bank_Open;
            this.Bank_Code = Bank_Code;
            this.Tax_Code = Tax_Code;
            this.Co_Status = Co_Status;
            this.Co_Size = Co_Size;
            this.Co_Management = Co_Management;
            this.Reg_Capital = Reg_Capital;
            this.Sales = Sales;
            this.Operation_Bound = Operation_Bound;
            this.Homepage = Homepage;
            this.Message_Address = Message_Address;
            this.Phone = Phone;
            this.Fax_phone = Fax_phone;
            this.ZipCodeW = ZipCodeW;
            this.country = country;
            this.province = province;
            this.city = city;
        }
        #endregion

        public override string PK { get { return "Flow"; } }
        public override string TbName { get { return "ZL_Client_Enterprise"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Flow","Int","4"},
                                  {"Code","VarChar","50"},
                                  {"Bank_Open","VarChar","50"}, 
                                  {"Bank_Code","VarChar","50"},
                                  {"Tax_Code","VarChar","50"},
                                  {"Co_Status","NChar","50"}, 
                                  {"Co_Size","NChar","50"},
                                  {"Co_Management","NChar","50"},
                                  {"Reg_Capital","Money","100"}, 
                                  {"Sales","Money","100"},
                                  {"Operation_Bound","NChar","50"},
                                  {"Homepage","NChar","50"}, 
                                  {"Message_Address","NChar","50"}, 
                                  {"Phone","VarChar","50"}, 
                                  {"Fax_phone","VarChar","50"}, 
                                  {"Add_Date","DateTime","50"},
                                  {"ZipCodeW","NVarChar","50"}, 
                                  {"country","NVarChar","255"}, 
                                  {"province","NVarChar","255"}, 
                                  {"city","NVarChar","255"}
                                 };
            return Tablelist;
        }
        public void EmptyData()
        {
            if (Add_Date <= DateTime.MinValue) Add_Date = DateTime.Now;
        }
        public SqlParameter[] GetParameters(M_Client_Enterprise model)
        {
            EmptyData();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Flow;
            sp[1].Value = model.Code;
            sp[2].Value = model.Bank_Open;
            sp[3].Value = model.Bank_Code;
            sp[4].Value = model.Tax_Code;
            sp[5].Value = model.Co_Status;
            sp[6].Value = model.Co_Size;
            sp[7].Value = model.Co_Management;
            sp[8].Value = model.Reg_Capital;
            sp[9].Value = model.Sales;
            sp[10].Value = model.Operation_Bound;
            sp[11].Value = model.Homepage;
            sp[12].Value = model.Message_Address;
            sp[13].Value = model.Phone;
            sp[14].Value = model.Fax_phone;
            sp[15].Value = model.Add_Date;
            sp[16].Value = model.ZipCodeW;
            sp[17].Value = model.country;
            sp[18].Value = model.province;
            sp[19].Value = model.city;
            return sp;
        }
        public M_Client_Enterprise GetModelFromReader(SqlDataReader rdr)
        {
            M_Client_Enterprise model = new M_Client_Enterprise();
            model.Flow = Convert.ToInt32(rdr["Flow"]);
            model.Code = ConverToStr(rdr["Code"]);
            model.Bank_Open = ConverToStr(rdr["Bank_Open"]);
            model.Bank_Code = ConverToStr(rdr["Bank_Code"]);
            model.Tax_Code = ConverToStr(rdr["Tax_Code"]);
            model.Co_Status = ConverToStr(rdr["Co_Status"]);
            model.Co_Size = ConverToStr(rdr["Co_Size"]);
            model.Co_Management = ConverToStr(rdr["Co_Management"]);
            model.Reg_Capital = ConverToDouble(Convert.IsDBNull(rdr["Reg_Capital"]) ? 0 : rdr["Reg_Capital"]);
            model.Sales = ConverToDouble(Convert.IsDBNull(rdr["Sales"]) ? 0 : rdr["Sales"]);
            model.Operation_Bound = ConverToStr(rdr["Operation_Bound"]);
            model.Homepage = ConverToStr(rdr["Homepage"]);
            model.Message_Address = ConverToStr(rdr["Message_Address"]);
            model.Phone = ConverToStr(rdr["Phone"]);
            model.Fax_phone = ConverToStr(rdr["Fax_phone"]);
            model.Add_Date = ConvertToDate(Convert.IsDBNull(rdr["Add_Date"]) ? DateTime.Now : rdr["Add_Date"]);
            model.ZipCodeW = ConverToStr(rdr["ZipCodeW"]);
            model.country = ConverToStr(rdr["country"]);
            model.province = ConverToStr(rdr["province"]);
            model.city = ConverToStr(rdr["city"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}




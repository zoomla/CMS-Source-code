using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Client_Penson : M_Base
    {
        #region 构造函数
        public M_Client_Penson()
        {
        }

        public M_Client_Penson
        (
            int Flow,
            string Code,
            string Sex,
            string Native,
            string Id_Code,
            string Nation,
            string Marriage,
            string Finish_School,
            string Cultrue_Love,
            string Sport_Love,
            string Life_Love,
            string FUN_Love,
            string Other_Love,
            DateTime Birthday,
            string Income,
            string Home_Circs,
            string Education,
            string Message_Address,
            string Work_Phone,
            string Home_Phone,
            string Fax_phone,
            string Telephone,
            string Little_Smart,
            string Homepage,
            string QQ_Code,
            string ICQ_Code,
            string UC_Code,
            string Email_Address,
            string MSN_Code,
            string YaoHu_Code,
            string Aim_Code,
            string ZipCodeW,
            string country,
            string province,
            string city
        )
        {
            this.Flow = Flow;
            this.Code = Code;
            this.Sex = Sex;
            this.Native = Native;
            this.Id_Code = Id_Code;
            this.Nation = Nation;
            this.Marriage = Marriage;
            this.Finish_School = Finish_School;
            this.Cultrue_Love = Cultrue_Love;
            this.Sport_Love = Sport_Love;
            this.Life_Love = Life_Love;
            this.FUN_Love = FUN_Love;
            this.Other_Love = Other_Love;
            this.Birthday = Birthday;
            this.Income = Income;
            this.Home_Circs = Home_Circs;
            this.Education = Education;
            this.Message_Address = Message_Address;
            this.Work_Phone = Work_Phone;
            this.Home_Phone = Home_Phone;
            this.Fax_phone = Fax_phone;
            this.Telephone = Telephone;
            this.Little_Smart = Little_Smart;
            this.Homepage = Homepage;
            this.QQ_Code = QQ_Code;
            this.ICQ_Code = ICQ_Code;
            this.UC_Code = UC_Code;
            this.Email_Address = Email_Address;
            this.MSN_Code = MSN_Code;
            this.YaoHu_Code = YaoHu_Code;
            this.Aim_Code = Aim_Code;
            this.ZipCodeW = ZipCodeW;
            this.country = country;
            this.province = province;
            this.city = city;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] Client_PensonList()
        {
            string[] Tablelist = { "Flow", "Code", "Sex", "Native", "Nation", "Marriage", "Finish_School", "Cultrue_Love", "Sport_Love", "Life_Love", "FUN_Love", "Other_Love", "Birthday", "Income", "Home_Circs", "Education", "Message_Address", "Work_Phone", "Home_Phone", "Fax_phone", "Telephone", "Little_Smart", "Homepage", "QQ_Code", "ICQ_Code", "UC_Code", "Email_Address", "MSN_Code", "YaoHu_Code", "Aim_Code", "ZipCodeW", "country", "province", "city" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int Flow { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 性别（0为女，1为男，2为未知）
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string Native { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string Id_Code { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// 婚姻状况(0为未知，1为未婚，2为已婚，3为离异)
        /// </summary>
        public string Marriage { get; set; }
        /// <summary>
        /// 毕业学校
        /// </summary>
        public string Finish_School { get; set; }
        /// <summary>
        /// 文化爱好
        /// </summary>
        public string Cultrue_Love { get; set; }
        /// <summary>
        /// 体育爱好
        /// </summary>
        public string Sport_Love { get; set; }
        /// <summary>
        /// 生活爱好
        /// </summary>
        public string Life_Love { get; set; }
        /// <summary>
        /// 娱乐休闲爱好
        /// </summary>
        public string FUN_Love { get; set; }
        /// <summary>
        /// 其他爱好
        /// </summary>
        public string Other_Love { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Income { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Home_Circs { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message_Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Work_Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Home_Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Fax_phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Little_Smart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Homepage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string QQ_Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ICQ_Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UC_Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email_Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MSN_Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string YaoHu_Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Aim_Code { get; set; }
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
        #endregion

        public override string PK { get { return "Flow"; } }
        public override string TbName { get { return "ZL_Client_Penson"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Flow","Int","4"},
                                  {"Code","VarChar","50"},
                                  {"Sex","Char","1"},
                                  {"Native","NVarChar","50"},
                                  {"Nation","VarChar","12"},
                                  {"Marriage","Char","1"},
                                  {"Finish_School","NVarChar","50"},
                                  {"Cultrue_Love","NVarChar","50"},
                                  {"Sport_Love","NVarChar","50"},
                                  {"Life_Love","NVarChar","50"},
                                  {"FUN_Love","NVarChar","50"},
                                  {"Other_Love","NVarChar","50"},
                                  {"Birthday","DateTime","50"},
                                  {"Income","NVarChar","50"},
                                  {"Home_Circs","NVarChar","100"},
                                  {"Education","NVarChar","50"},
                                  {"Message_Address","NChar","50"},
                                  {"Work_Phone","VarChar","12"},
                                  {"Home_Phone","VarChar","12"},
                                  {"Fax_phone","VarChar","12"},
                                  {"Telephone","VarChar","12"},
                                  {"Little_Smart","VarChar","12"},
                                  {"Homepage","NVarChar","50"},
                                  {"QQ_Code","VarChar","50"},
                                  {"ICQ_Code","VarChar","50"},
                                  {"UC_Code","VarChar","50"},
                                  {"Email_Address","NVarChar","50"},
                                  {"MSN_Code","VarChar","50"},
                                  {"YaoHu_Code","VarChar","50"},
                                  {"Aim_Code","VarChar","1000"},
                                  {"Id_Code","VarChar","50"},
                                  {"ZipCodeW","NVarChar","255"},
                                  {"country","NVarChar","255"},
                                  {"province","NVarChar","255"},
                                  {"city","NVarChar","255"}
                              };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Client_Penson model)
        {
            if (model.Birthday <= DateTime.MinValue) model.Birthday = DateTime.Now;
            if (string.IsNullOrEmpty(Code)) { Code = DateTime.Now.ToString("yyyyMMddHHmmss"); }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Flow;
            sp[1].Value = model.Code;
            sp[2].Value = model.Sex;
            sp[3].Value = model.Native;
            sp[4].Value = model.Nation;
            sp[5].Value = model.Marriage;
            sp[6].Value = model.Finish_School;
            sp[7].Value = model.Cultrue_Love;
            sp[8].Value = model.Sport_Love;
            sp[9].Value = model.Life_Love;
            sp[10].Value = model.FUN_Love;
            sp[11].Value = model.Other_Love;
            sp[12].Value = model.Birthday;
            sp[13].Value = model.Income;
            sp[14].Value = model.Home_Circs;
            sp[15].Value = model.Education;
            sp[16].Value = model.Message_Address;
            sp[17].Value = model.Work_Phone;
            sp[18].Value = model.Home_Phone;
            sp[19].Value = model.Fax_phone;
            sp[20].Value = model.Telephone;
            sp[21].Value = model.Little_Smart;
            sp[22].Value = model.Homepage;
            sp[23].Value = model.QQ_Code;
            sp[24].Value = model.ICQ_Code;
            sp[25].Value = model.UC_Code;
            sp[26].Value = model.Email_Address;
            sp[27].Value = model.MSN_Code;
            sp[28].Value = model.YaoHu_Code;
            sp[29].Value = model.Aim_Code;
            sp[30].Value = model.Id_Code;
            sp[31].Value = model.ZipCodeW;
            sp[32].Value = model.country;
            sp[33].Value = model.province;
            sp[34].Value = model.city;
            return sp;
        }
        public M_Client_Penson GetModelFromReader(SqlDataReader rdr)
        {
            M_Client_Penson model = new M_Client_Penson();
            model.Flow = Convert.ToInt32(rdr["Flow"]);
            model.Code = ConverToStr(rdr["Code"]);
            model.Sex = ConverToStr(rdr["Sex"]);
            model.Id_Code = ConverToStr(rdr["Id_Code"]);
            model.Native = ConverToStr(rdr["Native"]);
            model.Nation = ConverToStr(rdr["Nation"]);
            model.Marriage = ConverToStr(rdr["Marriage"]);
            //model.Cultrue_Love = rdr["Client_Value"].ToString();
            model.Cultrue_Love = ConverToStr(rdr["Cultrue_Love"]);
            model.Sport_Love = ConverToStr(rdr["Sport_Love"]);
            model.Life_Love = ConverToStr(rdr["Life_Love"]);
            model.FUN_Love = ConverToStr(rdr["FUN_Love"]);
            model.Other_Love = ConverToStr(rdr["Other_Love"]);
            model.Birthday = ConvertToDate(rdr["Birthday"]);
            model.Income = ConverToStr(rdr["Income"]);
            model.Home_Circs = ConverToStr(rdr["Home_Circs"]);
            model.Education = ConverToStr(rdr["Education"]);
            model.Message_Address = ConverToStr(rdr["Message_Address"]);
            model.Work_Phone = ConverToStr(rdr["Work_Phone"]);
            model.Home_Phone = ConverToStr(rdr["Home_Phone"]);
            model.Fax_phone = ConverToStr(rdr["Fax_phone"]);
            model.Telephone = ConverToStr(rdr["Telephone"]);
            model.Little_Smart = ConverToStr(rdr["Little_Smart"]);
            model.Homepage = ConverToStr(rdr["Homepage"]);
            model.QQ_Code = ConverToStr(rdr["QQ_Code"]);
            model.ICQ_Code = ConverToStr(rdr["ICQ_Code"]);
            model.UC_Code = ConverToStr(rdr["UC_Code"]);
            model.Email_Address = ConverToStr(rdr["Email_Address"]);
            model.MSN_Code = ConverToStr(rdr["MSN_Code"]);
            model.YaoHu_Code = ConverToStr(rdr["YaoHu_Code"]);
            model.Aim_Code = ConverToStr(rdr["Aim_Code"]);
            model.ZipCodeW = ConverToStr(rdr["ZipCodeW"]);
            model.country = ConverToStr(rdr["country"]);
            model.province = ConverToStr(rdr["province"]);
            model.city = ConverToStr(rdr["city"]);
            rdr.Close();
            return model;
        }

    }
}
using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_BossInfo : M_Base
    {

        #region 构造函数
        public M_BossInfo()
        {
        }


        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int nodeid { get; set; }
        public double CMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int parentid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shoptype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Childs { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 招商名
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 法定代理人
        /// </summary>
        public string Agent { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string license { get; set; }
        /// <summary>
        /// 代理商电话
        /// </summary>
        public string CTel { get; set; }
        /// <summary>
        /// 公司介绍
        /// </summary>
        public string CInfo { get; set; }
        /// <summary>
        /// 合同协议号
        /// </summary>
        public string ContractNum { get; set; }
        /// <summary>
        /// 开户人
        /// </summary>
        public string AccountPeople { get; set; }
        /// <summary>
        /// 开户银行
        /// </summary>
        public string Bank { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountPeople2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Bank2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BankNum2 { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string linkname { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string linksex { get; set; }
        /// <summary>
        /// 联系人职务
        /// </summary>
        public string linkPositions { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string linktel { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string Documents { get; set; }
        /// <summary>
        /// 证件类型号码
        /// </summary>
        public string DocumentsNUm { get; set; }
        #endregion
        public override string PK { get { return "nodeid"; } }
        public override string TbName { get { return "ZL_BossInfo"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"nodeid","Int","4"},
                                  {"parentid","Int","4"},
                                  {"shoptype","Int","4"},
                                  {"userid","Int","4"},
                                  {"Childs","Int","4"},
                                  {"CMoney","Money","10"},
                                  {"Province","NVarChar","250"},
                                  {"city","NVarChar","250"},
                                  {"CName","NVarChar","250"},
                                  {"Address","NChar","250"},
                                  {"Agent","NVarChar","250"},
                                  {"license","NVarChar","250"},
                                  {"CTel","NVarChar","250"},
                                  {"CInfo","NText","400"},
                                  {"ContractNum","NVarChar","250"},
                                  {"AccountPeople","NVarChar","250"},
                                  {"Bank","NVarChar","250"},
                                  {"BankNum","NVarChar","250"},
                                  {"AccountPeople2","NVarChar","250"},
                                  {"Bank2","NVarChar","250"},
                                  {"BankNum2","NVarChar","250"},
                                  {"linkname","NVarChar","250"},
                                  {"linksex","NVarChar","250"},
                                  {"linkPositions","NVarChar","250"},
                                  {"linktel","NVarChar","250"},
                                  {"fax","NVarChar","250"},
                                  {"PostCode","NVarChar","250"},
                                  {"email","NVarChar","250"},
                                  {"Documents","NVarChar","250"},
                                  {"DocumentsNUm","NVarChar","250"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_BossInfo model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.nodeid;
            sp[1].Value = model.parentid;
            sp[2].Value = model.shoptype;
            sp[3].Value = model.userid;
            sp[4].Value = model.Childs;
            sp[5].Value = model.CMoney;
            sp[6].Value = model.Province;
            sp[7].Value = model.city;
            sp[8].Value = model.CName;
            sp[9].Value = model.Address;
            sp[10].Value = model.Agent;
            sp[11].Value = model.license;
            sp[12].Value = model.CTel;
            sp[13].Value = model.CInfo;
            sp[14].Value = model.ContractNum;
            sp[15].Value = model.AccountPeople;
            sp[16].Value = model.Bank;
            sp[17].Value = model.BankNum;
            sp[18].Value = model.AccountPeople2;
            sp[19].Value = model.Bank2;
            sp[20].Value = model.BankNum2;
            sp[21].Value = model.linkname;
            sp[22].Value = model.linksex;
            sp[23].Value = model.linkPositions;
            sp[24].Value = model.linktel;
            sp[25].Value = model.fax;
            sp[26].Value = model.PostCode;
            sp[27].Value = model.email;
            sp[28].Value = model.Documents;
            sp[29].Value = model.DocumentsNUm;
            return sp;
        }

        public M_BossInfo GetModelFromReader(SqlDataReader rdr)
        {
            M_BossInfo model = new M_BossInfo();
            model.nodeid = ConvertToInt(rdr["nodeid"]);
            model.parentid = ConvertToInt(rdr["parentid"]);
            model.shoptype = ConvertToInt(rdr["shoptype"]);
            model.userid = ConvertToInt(rdr["userid"]);
            model.Childs = ConvertToInt(rdr["Childs"]);
            model.CMoney = ConvertToInt(rdr["CMoney"]);
            model.Province = ConverToStr(rdr["Province"]);
            model.city = ConverToStr(rdr["city"]);
            model.CName = ConverToStr(rdr["CName"]);
            model.Address = ConverToStr(rdr["Address"]);
            model.Agent = ConverToStr(rdr["Agent"]);
            model.license = ConverToStr(rdr["license"]);
            model.CTel = ConverToStr(rdr["CTel"]);
            model.CInfo = ConverToStr(rdr["CInfo"]);
            model.ContractNum = ConverToStr(rdr["ContractNum"]);
            model.AccountPeople = ConverToStr(rdr["AccountPeople"]);
            model.Bank = ConverToStr(rdr["Bank"]);
            model.BankNum = ConverToStr(rdr["BankNum"]);
            model.AccountPeople2 = ConverToStr(rdr["AccountPeople2"]);
            model.Bank2 = ConverToStr(rdr["Bank2"]);
            model.BankNum2 = ConverToStr(rdr["BankNum2"]);
            model.linkname = ConverToStr(rdr["linkname"]);
            model.linksex = ConverToStr(rdr["linksex"]);
            model.linkPositions = ConverToStr(rdr["linkPositions"]);
            model.linktel = ConverToStr(rdr["linktel"]);
            model.fax = ConverToStr(rdr["fax"]);
            model.PostCode = ConverToStr(rdr["PostCode"]);
            model.email = ConverToStr(rdr["email"]);
            model.Documents = ConverToStr(rdr["Documents"]);
            model.DocumentsNUm = ConverToStr(rdr["DocumentsNUm"]);
            rdr.Close();
            return model;
        }
    }
}
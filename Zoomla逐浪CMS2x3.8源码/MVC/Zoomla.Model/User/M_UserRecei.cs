using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
	[Serializable]
	public class M_UserRecei:M_Base
	{
		#region 定义字段
		/// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
		/// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceivName { get; set; }
		/// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }
		/// <summary>
        /// 邮政编码
        /// </summary>
        public string Zipcode { get; set; }
		/// <summary>
        /// 电话号码
        /// </summary>
        public string phone { get; set; }
		/// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNum { get; set; }
		/// <summary>
        /// 是否默认:0为是,1为否
        /// </summary>
        public int isDefault { get; set; }
		/// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 省市
        /// </summary>
        public string Provinces { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
		#endregion
        /// <summary>
        /// 以|分隔
        /// </summary>
        public string CityCode = "";
        public override string TbName { get { return "ZL_UserRecei"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"ReceivName","NVarChar","1000"}, 
                                  {"Street","NVarChar","1000"}, 
                                  {"Zipcode","NVarChar","1000"}, 
                                  {"phone","NVarChar","1000"}, 
                                  {"MobileNum","NVarChar","1000"}, 
                                  {"isDefault","Int","4"},
                                  {"UserID","Int","4"},
                                  {"Provinces","NVarChar","1000"}, 
                                  {"Email","NVarChar","1000"},
                                  //{"CityCode","NVarChar","50"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_UserRecei model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ReceivName;
            sp[2].Value = model.Street;
            sp[3].Value = model.Zipcode;
            sp[4].Value = model.phone;
            sp[5].Value = model.MobileNum;
            sp[6].Value = model.isDefault;
            sp[7].Value = model.UserID;
            sp[8].Value = model.Provinces;
            sp[9].Value = model.Email;
            //sp[10].Value = model.CityCode;
            return sp;
        }
        public M_UserRecei GetModelFromReader(SqlDataReader rdr)
        {
            M_UserRecei model = new M_UserRecei(); 
            model.ID = Convert.ToInt32(rdr["ID"].ToString());
            model.ReceivName = ConverToStr(rdr["ReceivName"]);
            model.Street = ConverToStr(rdr["Street"]);
            model.Zipcode = ConverToStr(rdr["Zipcode"]);
            model.phone = ConverToStr(rdr["phone"]);
            model.MobileNum = ConverToStr(rdr["MobileNum"]);
            model.isDefault = ConvertToInt(rdr["isDefault"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.Provinces = ConverToStr(rdr["Provinces"]);
            model.Email = ConverToStr(rdr["Email"]);
            //model.CityCode = rdr["CityCode"] == DBNull.Value ? "" : rdr["CityCode"].ToString();
            rdr.Close();
            return model;
        }
    }
}
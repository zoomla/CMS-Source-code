namespace ZoomLa.Model.User
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class Appinfo:M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        /// <summary>
        /// 平台OpenID
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public string UserInfo { get; set; }
        /// <summary>
        /// 平台Token
        /// </summary>
        public string Token { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 来源平台,小写
        /// qq,sina,wechat
        /// </summary>
        public string SourcePlat { get; set; }
        /// <summary>
        /// 系统备注
        /// </summary>
        public string Remind { get; set; }
        public override string TbName { get { return "ZL_UserApp"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"OpenID","NVarChar","500"},
        		        		{"UserInfo","NVarChar","4000"},
        		        		{"Token","NVarChar","500"},
        		        		{"CDate","DateTime","8"},
        		        		{"SourcePlat","NVarChar","200"},
        		        		{"Remind","NVarChar","500"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            Appinfo model = this;
            model.SourcePlat = (model.SourcePlat ?? "").ToLower();
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.OpenID;
            sp[3].Value = model.UserInfo;
            sp[4].Value = model.Token;
            sp[5].Value = model.CDate;
            sp[6].Value = model.SourcePlat;
            sp[7].Value = model.Remind;
            return sp;
        }
        public Appinfo GetModelFromReader(SqlDataReader rdr)
        {
            Appinfo model = new Appinfo();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.OpenID = ConverToStr(rdr["OpenID"]);
            model.UserInfo = ConverToStr(rdr["UserInfo"]);
            model.Token = ConverToStr(rdr["Token"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.SourcePlat = ConverToStr(rdr["SourcePlat"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
       
    }
}
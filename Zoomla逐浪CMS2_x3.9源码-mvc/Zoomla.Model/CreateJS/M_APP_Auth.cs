using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.CreateJS
{
    public class M_APP_Auth : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 申请网站Url
        /// </summary>
        public string SiteUrl { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MPhone { get; set; }
        public string Email { get; set; }
        public string QQCode { get; set; }
        /// <summary>
        /// 授权Key
        /// </summary>
        public string AuthKey { get; set; }
        public int MyStatus { get; set; }
        public DateTime CDate { get; set; }
        public int UserID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }
        public M_APP_Auth() 
        {
            CDate = DateTime.Now;
            AuthKey = "";
            MyStatus = 0;
        }
        public override string TbName { get { return "ZL_APP_Auth"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"SiteUrl","NVarChar","500"},
        		        		{"Contact","NVarChar","100"},
        		        		{"MPhone","NVarChar","50"},
        		        		{"Email","NVarChar","50"},
        		        		{"QQCode","NVarChar","50"},
        		        		{"AuthKey","NVarChar","50"},
        		        		{"MyStatus","Int","4"},
        		        		{"CDate","DateTime","8"},
        		        		{"UserID","Int","4"},
        		        		{"Remind","NVarChar","500"}
        };
            return Tablelist;
        }

        public SqlParameter[] GetParameters(M_APP_Auth model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.SiteUrl;
            sp[2].Value = model.Contact;
            sp[3].Value = model.MPhone;
            sp[4].Value = model.Email;
            sp[5].Value = model.QQCode;
            sp[6].Value = model.AuthKey;
            sp[7].Value = model.MyStatus;
            sp[8].Value = model.CDate;
            sp[9].Value = model.UserID;
            sp[10].Value = model.Remind;
            return sp;
        }
        public M_APP_Auth GetModelFromReader(SqlDataReader rdr)
        {
            M_APP_Auth model = new M_APP_Auth();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SiteUrl = ConverToStr(rdr["SiteUrl"]);
            model.Contact = ConverToStr(rdr["Contact"]);
            model.MPhone = ConverToStr(rdr["MPhone"]);
            model.Email = ConverToStr(rdr["Email"]);
            model.QQCode = ConverToStr(rdr["QQCode"]);
            model.AuthKey = ConverToStr(rdr["AuthKey"]);
            model.MyStatus = Convert.ToInt32(rdr["MyStatus"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
    }
}

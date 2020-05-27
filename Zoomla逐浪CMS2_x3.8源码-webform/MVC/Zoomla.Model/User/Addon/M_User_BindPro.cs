using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.User
{
    public class M_User_BindPro : M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 商品IDS,其他信息均从主表中取
        /// </summary>
        public string ProIDS { get; set; }
        public string Remind { get; set; }
        public override string TbName { get { return "ZL_User_BindPro"; } }
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","Int","4"},
        		        		{"UserName","NVarChar","100"},
        		        		{"ProIDS","Text","20000"},
        		        		{"Remind","NVarChar","500"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_User_BindPro model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.UserName;
            sp[3].Value = model.ProIDS;
            sp[4].Value = model.Remind;
            return sp;
        }
        public M_User_BindPro GetModelFromReader(DbDataReader rdr)
        {
            M_User_BindPro model = new M_User_BindPro();
            model.ID = ConvertToInt(rdr["ID"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.ProIDS = ConverToStr(rdr["ProIDS"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
    }
}

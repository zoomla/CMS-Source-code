using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_WX_User:M_Base
    {
        public int ID { get; set; }
        public string Name { get; set; }
        //微信用户id
        public string OpenID { get; set; }
        public int Sex { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        //用户头像url
        public string HeadImgUrl { get; set; }
        public DateTime CDate { get; set; }
        public int Groupid { get; set; }
        //公众号id
        public int AppId { get; set; }
        public override string TbName { get { return "ZL_WX_User"; } }
        public override string[,] FieldList()
        {
            string[,] tablelist = { 
                                    {"ID","Int","4"},
                                    {"Name","NVarChar","100"},
                                    {"OpenID","NVarChar","200"},
                                    {"Sex","Int","4"},
                                    {"City","NVarChar","50"},
                                    {"Province","NVarChar","50"},
                                    {"Country","NVarChar","50"},
                                    {"HeadImgUrl","NVarChar","500"},
                                    {"CDate","DateTime","8"},
                                    {"Groupid","Int","4"},
                                    {"AppId","Int","4"}
                                  };
            return tablelist;
        }
        public SqlParameter[] GetParameters(M_WX_User model)
        {
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Name;
            sp[2].Value = model.OpenID;
            sp[3].Value = model.Sex;
            sp[4].Value = model.City;
            sp[5].Value = model.Province;
            sp[6].Value = model.Country;
            sp[7].Value = model.HeadImgUrl;
            sp[8].Value = model.CDate;
            sp[9].Value = model.Groupid;
            sp[10].Value = model.AppId;
            return sp;
        }
        public M_WX_User GetModelFromReader(SqlDataReader rdr)
        {
            M_WX_User model = new M_WX_User();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Name = rdr["Name"].ToString();
            model.OpenID = rdr["OpenID"].ToString();
            model.Sex = ConvertToInt(rdr["Sex"]);
            model.City = rdr["City"].ToString();
            model.Province = rdr["Province"].ToString();
            model.Country = rdr["Country"].ToString();
            model.HeadImgUrl = rdr["HeadImgUrl"].ToString();
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Groupid = ConvertToInt(rdr["Groupid"]);
            model.AppId = ConvertToInt(rdr["AppId"]);
            rdr.Close();
            return model;
        }

    }
}

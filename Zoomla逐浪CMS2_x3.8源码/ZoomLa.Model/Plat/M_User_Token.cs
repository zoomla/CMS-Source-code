using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model.Plat
{
    public class M_User_Token:M_Base
    {
        public int id { get; set; }
        public int uid { get; set; }
        public string SinaToken { get; set; }
        //QQ的OpenID,相当于QQ用户ID(其他的改为用DataTable存储)
        public string QQOpenID { get; set; }
        public string QQToken { get; set; }
        //昵称
        public string QQUName { get; set; }
        //------微信
        public string WXOpenID { get; set; }
        public string WxInfo { get; set; }

        public override string TbName { get { return "ZL_User_Token"; } }
        public override string[,] FieldList()
        {
            string[,] fieldlist ={
                                    {"ID","Int","4"},
                                    {"uid","Int","4"},
                                    {"SinaToken","NVarChar","500"},
                                    {"QQOpenID","NVarChar","500"},
                                    {"QQToken","NVarChar","500"},
                                    {"QQUName","NVarChar","100"},
                                    {"WXOpenID","NVarChar","100"},
                                    {"WXInfo","NVarChar","3000"}
                                };
            return fieldlist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_User_Token model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.uid;
            sp[2].Value = model.SinaToken;
            sp[3].Value = model.QQOpenID;
            sp[4].Value = model.QQToken;
            sp[5].Value = model.QQUName;
            sp[6].Value = model.WXOpenID;
            sp[7].Value = model.WxInfo;
            return sp;
        }
        public M_User_Token GetModelFromReader(SqlDataReader rdr)
        {
            M_User_Token model = new M_User_Token();
            model.id = Convert.ToInt32(rdr["id"]);
            model.uid = Convert.ToInt32(rdr["uid"]);
            model.SinaToken = ConverToStr(rdr["SinaToken"]);
            model.QQOpenID = ConverToStr(rdr["QQOpenID"]);
            model.QQToken = ConverToStr(rdr["QQToken"]);
            model.QQUName = ConverToStr(rdr["QQUName"]);
            model.WXOpenID = ConverToStr(rdr["WXOpenID"]);
            model.WxInfo = ConverToStr(rdr["WxInfo"]);
            rdr.Close();
            return model;
        }
        public DataRow GetStruct()
        {
            DataTable dt = new DataTable();
            //Token
            dt.Columns.Add(new DataColumn("Token", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));//绑定帐户用户名
            dt.Columns.Add(new DataColumn("Alias", typeof(string)));//昵称
            return dt.NewRow();
        }
    }
}

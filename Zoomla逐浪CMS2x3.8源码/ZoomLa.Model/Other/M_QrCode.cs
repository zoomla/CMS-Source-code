using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_QrCode : ZoomLa.Model.M_Base
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 二维码路径
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 二维码类型
        /// </summary>
        public string CodeType { get; set; }
        /// <summary>
        /// 纠错等级
        /// </summary>
        public string CodeLevel { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int CodeVersion { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string CodeContents { get; set; }
        /// <summary>
        /// 用于存该二维码别名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 类型：0文本、1名片 
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 生成url的标识
        /// </summary>
        public int AppID { get; set; }
        /// <summary>
        /// 生成各平台url集合(以,隔开)
        /// 各平台顺序:Android,iPhone,iPad,WPhone,PC
        /// </summary>
        public string Urls { get; set; }
        public M_QrCode()
        {
            this.Type = 0;
            this.CreateTime = DateTime.Now;
        }
        public override string TbName { get { return "ZL_QrCode"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"ImageUrl","NVarChar","255"},
                                  {"CodeType","NVarChar","50"},
                                  {"CodeLevel","NVarChar","50"},
                                  {"CodeVersion","Int","4"},
                                  {"CodeContents","NVarChar","255"},
                                  {"UserName","NVarChar","50"},
                                  {"Type","Int","4"},
                                  {"CreateTime","DateTime","8"},
                                  {"AppID","Int","4"},
                                  {"Urls","NVarChar","500"}
                              };
            return Tablelist;
        }

        public SqlParameter[] GetParameters(M_QrCode model)
        {

            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ImageUrl;
            sp[2].Value = model.CodeType;
            sp[3].Value = model.CodeLevel;
            sp[4].Value = model.CodeVersion;
            sp[5].Value = model.CodeContents;
            sp[6].Value = model.UserName;
            sp[7].Value = model.Type;
            sp[8].Value = model.CreateTime;
            sp[9].Value = model.AppID;
            sp[10].Value = model.Urls;
            return sp;
        }
        void EmptyData()
        {
            if (CreateTime <= DateTime.MinValue) CreateTime = DateTime.Now;
        }
        public M_QrCode GetModelFromReader(SqlDataReader rdr)
        {
            M_QrCode model = new M_QrCode();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ImageUrl = rdr["ImageUrl"].ToString();
            model.CodeType = rdr["CodeType"].ToString();
            model.CodeLevel = rdr["CodeLevel"].ToString();
            model.CodeVersion = ConvertToInt(rdr["CodeVersion"]);
            model.CodeContents = rdr["CodeContents"].ToString();
            model.UserName = rdr["UserName"].ToString();
            model.Type = ConvertToInt(rdr["Type"].ToString());
            model.CreateTime = ConvertToDate(rdr["CreateTime"].ToString());
            model.AppID = ConvertToInt(rdr["AppID"]);
            model.Urls = rdr["Urls"].ToString();
            rdr.Close();
            return model;
        }
    }

}

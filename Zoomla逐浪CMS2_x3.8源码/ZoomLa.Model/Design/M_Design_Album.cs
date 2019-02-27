using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    public class M_Design_Album:M_Base
    {
        public int ID { get; set; }
        public string AlbumName { get; set; }
        public string AlbumDesc { get; set; }
        /// <summary>
        /// 图片地址 |分隔
        /// </summary>
        public string Photos { get; set; }
        /// <summary>
        /// 背景音乐
        /// </summary>
        public string Music { get; set; }
        [JsonIgnore]
        public int UseTlp { get; set; }
        [JsonIgnore]
        public int UserID { get; set; }
        [JsonIgnore]
        public DateTime CDate { get; set; }
        public M_Design_Album() { CDate = DateTime.Now; }
        [JsonIgnore]
        public override string TbName { get { return "ZL_Design_Album"; } }
        [JsonIgnore]
        public override string PK { get { return "ID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4"},
                                {"AlbumName","NVarChar","200"},
                                {"AlbumDesc","NVarChar","4000"},
                                {"Photos","NText","20000"},
                                {"UseTlp","Int","4"},
                                {"UserID","Int","4"},
                                {"CDate","DateTime","8"},
                                {"Music","NVarChar","2000"},
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Design_Album model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.AlbumName;
            sp[2].Value = model.AlbumDesc;
            sp[3].Value = model.Photos;
            sp[4].Value = model.UseTlp;
            sp[5].Value = model.UserID;
            sp[6].Value = model.CDate;
            sp[7].Value = model.Music;
            return sp;
        }
        public M_Design_Album GetModelFromReader(DbDataReader rdr)
        {
            M_Design_Album model = new M_Design_Album();
            model.ID = ConvertToInt(rdr["ID"]);
            model.AlbumName = ConverToStr(rdr["AlbumName"]);
            model.AlbumDesc = ConverToStr(rdr["AlbumDesc"]);
            model.Photos = ConverToStr(rdr["Photos"]);
            model.UseTlp = ConvertToInt(rdr["UseTlp"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Music = ConverToStr(rdr["Music"]);
            rdr.Close();
            return model;
        }

    }
}

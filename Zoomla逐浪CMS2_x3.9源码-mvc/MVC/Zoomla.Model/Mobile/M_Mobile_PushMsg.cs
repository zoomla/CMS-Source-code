using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Mobile
{
    public class M_Mobile_PushMsg : M_Base
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string MsgContent { get; set; }
        public string PushDevice { get; set; }
        public string PushTag { get; set; }
        public string PushAlias { get; set; }
        /// <summary>
        /// 推送平台,1:极光,2:百度
        /// </summary>
        public int PushPlat { get; set; }
        /// <summary>
        /// 推送类型 alter,sms
        /// </summary>
        public string PushType { get; set; }
        /// <summary>
        /// 消息类型,使用文字备注
        /// </summary>
        public string MsgType { get; set; }
        public string Remind { get; set; }
        /// <summary>
        /// 推送结果
        /// </summary>
        public string Result { get; set; }
        public DateTime CDate { get; set; }

        public override string TbName { get { return "ZL_Mobile_PushMsg"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Title","NVarChar","500"},
        		        		{"MsgContent","NVarChar","1000"},
        		        		{"PushDevice","NVarChar","500"},
        		        		{"PushTag","NVarChar","500"},
        		        		{"PushAlias","NVarChar","500"},
        		        		{"PushPlat","Int","4"},
        		        		{"PushType","NVarChar","100"},
        		        		{"MsgType","NVarChar","100"},
        		        		{"Remind","NVarChar","500"},
        		        		{"Result","NVarChar","4000"},
        		        		{"CDate","DateTime","8"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Mobile_PushMsg model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Title;
            sp[2].Value = model.MsgContent;
            sp[3].Value = model.PushDevice;
            sp[4].Value = model.PushTag;
            sp[5].Value = model.PushAlias;
            sp[6].Value = model.PushPlat;
            sp[7].Value = model.PushType;
            sp[8].Value = model.MsgType;
            sp[9].Value = model.Remind;
            sp[10].Value = model.Result;
            sp[11].Value = model.CDate;
            return sp;
        }
        public M_Mobile_PushMsg GetModelFromReader(DbDataReader rdr)
        {
            M_Mobile_PushMsg model = new M_Mobile_PushMsg();
            model.ID = ConvertToInt(rdr["ID"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.MsgContent = ConverToStr(rdr["MsgContent"]);
            model.PushDevice = ConverToStr(rdr["PushDevice"]);
            model.PushTag = ConverToStr(rdr["PushTag"]);
            model.PushAlias = ConverToStr(rdr["PushAlias"]);
            model.PushPlat = ConvertToInt(rdr["PushPlat"]);
            model.PushType = ConverToStr(rdr["PushType"]);
            model.MsgType = ConverToStr(rdr["MsgType"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.Result = ConverToStr(rdr["Result"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            rdr.Close();
            return model;
        }
    }
}

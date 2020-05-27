using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Chat
{
    public class M_UAction : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 用户动作,浏览|单击
        /// </summary>
        public string action { get; set; }
        /// <summary>
        /// 浏览页面标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 页面url
        /// </summary>
        public string pageurl { get; set; }
        public string ip { get; set; }
        public int uid { get; set; }
        public string uname { get; set; }
        /// <summary>
        /// 用于标识,如未登录的情况下依此
        /// </summary>
        public string idflag { get; set; }
        public DateTime cdate { get; set; }
        public override string TbName
        {
            get { return "ZL_UAction"; }
        }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"action","NVarChar","50"},
        		        		{"title","NVarChar","100"},
        		        		{"pageurl","NVarChar","1000"},
        		        		{"ip","NVarChar","200"},
        		        		{"uid","Int","4"},
        		        		{"uname","NVarChar","100"},
        		        		{"cdate","DateTime","8"},
                                {"idflag","NVarChar","50"}
        };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_UAction model)
        {
            if (model.cdate <= DateTime.MinValue) { model.cdate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.action;
            sp[2].Value = model.title;
            sp[3].Value = model.pageurl;
            sp[4].Value = model.ip;
            sp[5].Value = model.uid;
            sp[6].Value = model.uname;
            sp[7].Value = model.cdate;
            sp[8].Value = model.idflag;
            return sp;
        }
        public M_UAction GetModelFromReader(SqlDataReader rdr)
        {
            M_UAction model = new M_UAction();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.action = ConverToStr(rdr["action"]);
            model.title = ConverToStr(rdr["title"]);
            model.pageurl = ConverToStr(rdr["pageurl"]);
            model.ip = ConverToStr(rdr["ip"]);
            model.uid = ConvertToInt(rdr["uid"]);
            model.uname = ConverToStr(rdr["uname"]);
            model.cdate = ConvertToDate(rdr["cdate"]);
            model.idflag = ConverToStr(rdr["idflag"]);
            rdr.Close();
            return model;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Plat
{
    public class M_Plat_Comp : M_Base
    {
        public int ID { get; set; }
        private string _logo = "";
        /// <summary>
        /// 公司Logo
        /// </summary>
        public string CompLogo
        {
            get
            {
                return string.IsNullOrEmpty(_logo) ? "/Images/nopic.gif" : _logo;
            }
            set { _logo = value; }
        }
        /// <summary>
        /// 上传文件的路径
        /// </summary>
        public string UPPath { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 公司简述,500字以内
        /// </summary>
        public string CompDesc { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public int CompType { get; set; }
        /// <summary>
        /// 0:私人,1:认证企业
        /// </summary>
        public int Status { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 公司网址
        /// </summary>
        public string CompHref { get; set; }
        /// <summary>
        /// 公司企业邮箱后缀名,为创始人的邮箱后缀,示例:z01.com,hx008.com
        /// </summary>
        public string Mails { get; set; }
        /// <summary>
        /// 公司电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 公司手机
        /// </summary>
        public string Mobile { get; set; }

        private int shortlen = 4;
        private string _compshort = "";//兼容
        /// <summary>
        /// 公司名称简写,最多四个字符,用于平台显示公司名称
        /// </summary>
        public string CompShort
        {
            get
            {
                if (string.IsNullOrEmpty(_compshort))
                {
                    _compshort = CompName;
                    if (_compshort.Length > shortlen) { _compshort = _compshort.Substring(0, shortlen); }
                }
                return _compshort;
            }
            set { _compshort = value; }
        }
        public override string TbName { get { return "ZL_Plat_Comp"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4"},
                                {"CompLogo","NVarChar","200"},
                                {"CompName","NVarChar","100"},
                                {"CompDesc","NVarChar","500"},
                                {"CompType","Int","4"},
                                {"Status","Int","4"},
                                {"CreateUser","Int","4"},
                                {"CreateTime","DateTime","8"},
                                {"Mails","NVarChar","200"},
                                {"CompHref","NVarChar","100"},
                                {"UPPath","NVarChar","300"},
                                {"CompShort","NVarChar","100"},
                                {"Telephone","NVarChar","50"},
                                {"Mobile","NVarChar","50"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Plat_Comp model = this;
            EmptyDeal();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.CompLogo;
            sp[2].Value = model.CompName;
            sp[3].Value = model.CompDesc;
            sp[4].Value = model.CompType;
            sp[5].Value = model.Status;
            sp[6].Value = model.CreateUser;
            sp[7].Value = model.CreateTime;
            sp[8].Value = model.Mails;
            sp[9].Value = model.CompHref;
            sp[10].Value = model.UPPath;
            sp[11].Value = model.CompShort;
            sp[12].Value = model.Telephone;
            sp[13].Value = model.Mobile;
            return sp;
        }
        public M_Plat_Comp GetModelFromReader(DbDataReader rdr)
        {
            M_Plat_Comp model = new M_Plat_Comp();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.CompLogo = rdr["CompLogo"].ToString();
            model.CompName = rdr["CompName"].ToString();
            model.CompDesc = rdr["CompDesc"].ToString();
            model.CompType = Convert.ToInt32(rdr["CompType"]);
            model.Status = Convert.ToInt32(rdr["Status"]);
            model.CreateUser = Convert.ToInt32(rdr["CreateUser"]);
            model.CreateTime = Convert.ToDateTime(rdr["CreateTime"]);
            model.Mails = ConverToStr(rdr["Mails"]);
            model.CompHref = ConverToStr(rdr["CompHref"]);
            model.UPPath = ConverToStr(rdr["UPPath"]);
            model.CompShort = ConverToStr(rdr["CompShort"]);
            model.Telephone = ConverToStr(rdr["Telephone"]);
            model.Mobile = ConverToStr(rdr["Mobile"]);
            rdr.Close();
            return model;
        }
        public void EmptyDeal()
        {
            if (CreateTime.Year < 1901) CreateTime = DateTime.Now;
            if (string.IsNullOrEmpty(Mails)) Mails = "";
            if (string.IsNullOrEmpty(CompDesc)) CompDesc = "";
        }
    }
}

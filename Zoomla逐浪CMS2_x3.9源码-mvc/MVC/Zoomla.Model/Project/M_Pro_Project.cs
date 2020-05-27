using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Project
{
    public class M_Pro_Project : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        public int ZType { get; set; }
        /// <summary>
        /// 项目价格
        /// </summary>
        public double ProPrice { get; set; }
        /// <summary>
        /// 项目经理
        /// </summary>
        public int ProManageer { get; set; }
        /// <summary>
        /// 技术负责人
        /// </summary>
        public string TecDirector { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 客户所在单位
        /// </summary>
        public string CustomerCompany { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerTele { get; set; }
        /// <summary>
        /// 客户手机
        /// </summary>
        public string CustomerMobile { get; set; }
        /// <summary>
        /// 客户QQ
        /// </summary>
        public string CustomerQQ { get; set; }
        /// <summary>
        /// 客户MSN
        /// </summary>
        public string CustomerMSN { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string CustomerEmail { get; set; }
        /// <summary>
        /// 项目需求
        /// </summary>
        public string Requirements { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public int ZStatus { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }
        public override string TbName { get { return "ZL_Pro_Project"; } }
        public override string PK { get { return "ID"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4" },
                                {"ProName","NVarChar","300" },
                                {"ZType","Int","4" },
                                {"ProPrice","Money","32" },
                                {"ProManageer","Int","4" },
                                {"TecDirector","NVarChar","50" },
                                {"CustomerID","Int","4" },
                                {"CustomerCompany","NVarChar","50" },
                                {"CustomerTele","NVarChar","50" },
                                {"CustomerMobile","NVarChar","50" },
                                {"CustomerQQ","NVarChar","50" },
                                {"CustomerMSN","NVarChar","50" },
                                {"CustomerAddress","NVarChar","200" },
                                {"CustomerEmail","NVarChar","100" },
                                {"Requirements","NVarChar","2000" },
                                {"ZStatus","Int","4" },
                                {"CDate","DateTime","8" },
                                {"Remind","NVarChar","200" }
            };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Pro_Project model = this;
            if (CDate <= DateTime.MinValue) { CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ProName;
            sp[2].Value = model.ZType;
            sp[3].Value = model.ProPrice;
            sp[4].Value = model.ProManageer;
            sp[5].Value = model.TecDirector;
            sp[6].Value = model.CustomerID;
            sp[7].Value = model.CustomerCompany;
            sp[8].Value = model.CustomerTele;
            sp[9].Value = model.CustomerMobile;
            sp[10].Value = model.CustomerQQ;
            sp[11].Value = model.CustomerMSN;
            sp[12].Value = model.CustomerAddress;
            sp[13].Value = model.CustomerEmail;
            sp[14].Value = model.Requirements;
            sp[15].Value = model.ZStatus;
            sp[16].Value = model.CDate;
            sp[17].Value = model.Remind;

            return sp;
        }
        public M_Pro_Project GetModelFromReader(DbDataReader rdr)
        {
            M_Pro_Project model = new M_Pro_Project();
            model.ID = ConvertToInt(rdr["ID"]);
            model.ProName = ConverToStr(rdr["ProName"]);
            model.ZType = ConvertToInt(rdr["ZType"]);
            model.ProPrice = ConverToDouble(rdr["ProPrice"]);
            model.ProManageer = ConvertToInt(rdr["ProManageer"]);
            model.TecDirector = ConverToStr(rdr["TecDirector"]);
            model.CustomerID = ConvertToInt(rdr["CustomerID"]);
            model.CustomerCompany = ConverToStr(rdr["CustomerCompany"]);
            model.CustomerTele = ConverToStr(rdr["CustomerTele"]);
            model.CustomerMobile = ConverToStr(rdr["CustomerMobile"]);
            model.CustomerQQ = ConverToStr(rdr["CustomerQQ"]);
            model.CustomerMSN = ConverToStr(rdr["CustomerMSN"]);
            model.CustomerAddress = ConverToStr(rdr["CustomerAddress"]);
            model.CustomerEmail = ConverToStr(rdr["CustomerEmail"]);
            model.Requirements = ConverToStr(rdr["Requirements"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            return model;
        }
    }
}

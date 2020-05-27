using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Design
{
    public class M_Design_Tlp : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TlpName { get; set; }
        /// <summary>
        /// 预览图路径
        /// </summary>
        public string PreviewImg { get; set; }
        /// <summary>
        /// 模板备注
        /// </summary>
        public string Remind { get; set; }
        /// <summary>
        /// 模板价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 模板类型 0:动力模板,1:H5场景,2:手机模板
        /// </summary>
        public int ZType { get; set; }
        /// <summary>
        /// 模板状态 0:正常,1:推荐,-1:停用(不可选)
        /// </summary>
        public int ZStatus { get; set; }
        public DateTime CDate { get; set; }
        public int ClassID { get; set; }
        /// <summary>
        /// 评分，0-5.0
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// 属于哪种类型的默认模板 mbh5_fast,
        /// </summary>
        public string DefBy { get; set; }
        public override string TbName { get { return "ZL_Design_Tlp"; } }
        public override string PK { get { return "ID"; } }
        public M_Design_Tlp()
        {
            ZStatus = 0;
        }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4"},
                                {"TlpName","NVarChar","50"},
                                {"PreviewImg","NVarChar","500"},
                                {"Remind","NVarChar","500"},
                                {"Price","Money","8"},
                                {"ZType","Int","4"},
                                {"ZStatus","Int","4"},
                                {"CDate","DateTime","8"},
                                {"ClassID","Int","4"},
                                {"Score","Float","8"},
                                {"DefBy","NVarChar","500"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Design_Tlp model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TlpName;
            sp[2].Value = model.PreviewImg;
            sp[3].Value = model.Remind;
            sp[4].Value = model.Price;
            sp[5].Value = model.ZType;
            sp[6].Value = model.ZStatus;
            sp[7].Value = model.CDate;
            sp[8].Value = model.ClassID;
            sp[9].Value = model.Score;
            sp[10].Value = model.DefBy;
            return sp;
        }
        public M_Design_Tlp GetModelFromReader(DbDataReader rdr)
        {
            M_Design_Tlp model = new M_Design_Tlp();
            model.ID = ConvertToInt(rdr["ID"]);
            model.TlpName = ConverToStr(rdr["TlpName"]);
            model.PreviewImg = ConverToStr(rdr["PreviewImg"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.Price = ConverToDouble(rdr["Price"]);
            model.ZType = ConvertToInt(rdr["ZType"]);
            model.ZStatus = ConvertToInt(rdr["ZStatus"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.ClassID = ConvertToInt(rdr["ClassID"]);
            model.Score = ConverToDouble(rdr["Score"]);
            model.DefBy = ConverToStr(rdr["DefBy"]);
            rdr.Close();
            return model;
        }
    }
}

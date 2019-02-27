using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace ZoomLa.Model.Content
{
    public class M_Content_FileBuy : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 所属内容ID
        /// </summary>
        public int Gid { get; set; }
        /// <summary>
        /// 随机码,用于标识文件
        /// </summary>
        public string Ranstr { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string  FName { get; set; }
        /// <summary>
        /// 下载链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 上传用户
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 上传用户名
        /// </summary>
        public string  UserName { get; set; }
        /// <summary>
        /// 购买价格
        /// </summary>
        public string BuyPrice { get; set; }
       /// <summary>
       /// 是否主文件
       /// </summary>
        public int IsMain { get; set; }
        /// <summary>
        /// 字段列表
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// 文件到期时间
        /// </summary>
        public DateTime EndDate { get; set; }
        public override string TbName { get { return "ZL_Content_FileBuy"; } }

        public override string[,] FieldList()
        {
            string[,] fileds = {
                                   {"ID","Int","4"},
                                   {"Gid","Int","4"},
                                   {"Ranstr","NVarChar","200"},
                                   {"FName","NVarChar","200"},
                                   {"Url","NVarChar","200"},
                                   {"UserID","Int","4"},
                                   {"UserName","NVarChar","50"},
                                   {"BuyPrice","NVarChar","200"},
                                   {"IsMain","Int","4"},
                                   {"Field","NVarChar","100"},
                                   {"CDate","DateTime","8"},
                                   {"EndDate","DateTime","8"}
                               };
            return fileds;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Content_FileBuy model = this;
            EmptyDeal();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Gid;
            sp[2].Value = model.Ranstr;
            sp[3].Value = model.FName;
            sp[4].Value = model.Url;
            sp[5].Value = model.UserID;
            sp[6].Value = model.UserName;
            sp[7].Value = model.BuyPrice;
            sp[8].Value = model.IsMain;
            sp[9].Value = model.Field;
            sp[10].Value = model.CDate;
            sp[11].Value = model.EndDate;
            return sp;
        }
        public M_Content_FileBuy GetModelFromReader(SqlDataReader rdr)
        {
            M_Content_FileBuy model = new M_Content_FileBuy();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Gid = Convert.ToInt32(rdr["Gid"]);
            model.Ranstr = ConverToStr(rdr["Ranstr"]);
            model.FName = ConverToStr(rdr["FName"]);
            model.Url = ConverToStr(rdr["Url"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.BuyPrice = ConverToStr(rdr["BuyPrice"]);
            model.IsMain = ConvertToInt(rdr["IsMain"]);
            model.Field = ConverToStr(rdr["Field"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.EndDate = ConvertToDate(rdr["EndDate"]);
            rdr.Close();
            return model;
        }
        public void EmptyDeal()
        {
            if (CDate.Year < 1900) CDate = DateTime.Now;
            if (EndDate.Year < 1900) EndDate = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    [Serializable]
    //ZL_User_Cloud
    public class M_User_Cloud : M_Base
    {

        public int ID { get; set; }
        public int UserID { get; set; }
        //原始文件名或目录名
        public string FileName { get; set; }
        public string Guid { get; set; }
        //所处的虚拟路径
        public string VPath { get; set; }
        //随机文件名(目录名不随机) 
        public string SFileName { get; set; }
        /// <summary>
        /// 1,普通文件,2:目录
        /// </summary>
        public int FileType { get; set; }
        public DateTime CDate { get; set; }
        public string FileSize { get; set; }
        public DateTime UpdateDate { get; set; }

        public override string TbName { get { return "ZL_User_Cloud"; } }
        public override string PK { get { return "Guid"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"Guid","NVarChar","200"},
                                {"UserID","Int","4"},
                                {"FileName","NVarChar","500"},
                                {"VPath","NVarChar","500"},
                                {"SFileName","NVarChar","100"},
                                {"FileType","Int","4"},
                                {"CDate","DateTime","8"},
                                {"FileSize","NVarChar","500"},
                                {"UpdateDate","DateTime","8"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_User_Cloud model = this;
            SqlParameter[] sp = GetSP();
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            if (model.UpdateDate <= DateTime.MinValue) { model.UpdateDate = DateTime.Now; }
            sp[0].Value = model.Guid;
            sp[1].Value = model.UserID;
            sp[2].Value = model.FileName;
            sp[3].Value = model.VPath;
            sp[4].Value = model.SFileName;
            sp[5].Value = model.FileType;
            sp[6].Value = model.CDate;
            sp[7].Value = model.FileSize;
            sp[8].Value = model.UpdateDate;
            return sp;
        }
        public M_User_Cloud GetModelFromReader(SqlDataReader rdr)
        {
            M_User_Cloud model = new M_User_Cloud();
            model.Guid = ConverToStr(rdr["Guid"]);
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.FileName = ConverToStr(rdr["FileName"]);
            model.VPath = ConverToStr(rdr["VPath"]);
            model.SFileName = ConverToStr(rdr["SFileName"]);
            model.FileType = ConvertToInt(rdr["FileType"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.FileSize = ConverToStr(rdr["FileSize"]);
            model.UpdateDate = ConvertToDate(rdr["UpdateDate"]);
            rdr.Close();
            return model;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Plat
{
    public class M_Plat_File : M_Base
    {
        public M_Plat_File() { FileType = 1; CDate = DateTime.Now; UpdateDate = DateTime.Now; Remind = ""; }
        //Guid标识
        public string Guid { get; set; }
        //ID标识
        public int ID { get; set; }
        //原始文件名或目录名
        public string FileName { get; set; }
        //随机文件名(目录名不随机) 
        public string SFileName { get; set; }
        /// <summary>
        /// 用户ID,为以后扩展,使用字符串类型
        /// </summary>
        public string UserID { get; set; }
        //公司ID
        public int CompID { get; set; }
        public string Remind { get; set; }
        //所处的虚拟路径
        public string VPath { get; set; }
        /// <summary>
        /// 1,普通文件,2:目录
        /// </summary>
        public int FileType { get; set; }
        public string FileSize { get; set; }
        public DateTime CDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public override string TbName { get { return "ZL_Plat_File"; } }
        public override string PK { get { return "Guid"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"Guid","NVarChar","200"},
        		        		{"FileName","NVarChar","200"},
        		        		{"SFileName","NVarChar","200"},
        		        		{"UserID","Int","4"},
                                {"CompID","Int","4"},
                                {"Remind","NVarChar","50"},
                                {"VPath","NVarChar","2000"},
                                {"FileType","Int","4"},
                                {"FileSize","NVarChar","100"},
                                {"CDate","DateTime","8"},
                                {"UpdateDate","DateTime","8"}
        };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Plat_File model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.Guid;
            sp[1].Value = model.FileName;
            sp[2].Value = model.SFileName;
            sp[3].Value = model.UserID;
            sp[4].Value = model.CompID;
            sp[5].Value = model.Remind;
            sp[6].Value = model.VPath;
            sp[7].Value = model.FileType;
            sp[8].Value = model.FileSize;
            sp[9].Value = model.CDate;
            sp[10].Value = model.UpdateDate;
            return sp;
        }
        public M_Plat_File GetModelFromReader(SqlDataReader rdr)
        {
            M_Plat_File model = new M_Plat_File();
            model.Guid = rdr["Guid"].ToString();
            model.ID =Convert.ToInt32(rdr["ID"]);
            model.FileName = ConverToStr(rdr["FileName"]);
            model.SFileName = ConverToStr(rdr["SFileName"]);
            model.UserID = ConverToStr(rdr["UserID"]);
            model.CompID = ConvertToInt(rdr["CompID"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            model.VPath = ConverToStr(rdr["VPath"]);
            model.FileType = ConvertToInt(rdr["FileType"]);
            model.FileSize = ConverToStr(rdr["FileSize"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.UpdateDate = ConvertToDate(rdr["UpdateDate"]);
            rdr.Close();
            return model;
        }
    }
}

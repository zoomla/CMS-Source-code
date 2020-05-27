using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    public class M_DocList : M_Base
    {

        public int ID { get; set; }

        public int ModelID { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string AddUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// 
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 表单格式
        /// </summary>
        /// 
        public string FileUrl { get; set; }

        public override string TbName { get { return "ZL_DocList"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"ModelID","Int","4"},
                                  {"FileName","NVarChar","255"},
                                  {"AddUser","NVarChar","50"},
                                  {"CreateTime","DateTime","255"},
                                  {"FileUrl","NVarChar","255"}
              };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_DocList model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ModelID;
            sp[2].Value = model.FileName;
            sp[3].Value = model.AddUser;
            sp[4].Value = model.CreateTime;
            sp[5].Value = model.FileUrl;
            return sp;
        }
        public M_DocList GetModelFromReader(SqlDataReader rdr)
        {
            M_DocList model = new M_DocList();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ModelID = ConvertToInt(rdr["ModelID"]);
            model.FileName = ConverToStr(rdr["FileName"]);
            model.AddUser = rdr["AddUser"].ToString();
            model.CreateTime = ConvertToDate(rdr["CreateTime"].ToString());
            model.FileUrl = ConverToStr(rdr["FileUrl"]);
            rdr.Close();
            return model;
        }
    }
}
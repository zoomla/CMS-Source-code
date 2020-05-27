using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model.FTP
{
    public class M_FtpConfig:M_Base
    {
        /// <summary>
        /// 表ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// FTP服务器
        /// </summary>
        public string FtpServer { get; set; }
        /// <summary>
        /// FTP端口
        /// </summary>
        public string FtpPort { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string FtpUsername { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string FtpPassword { get; set; }
        /// <summary>
        /// 超时时间
        /// </summary>
        public string OutTime { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string SavePath { get; set; }
        /// <summary>
        /// FTP别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; set; }
        public override string TbName { get { return "ZL_FTPConfig"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"FtpServer","NVarChar","255"},
                                  {"FtpPort","NVarChar","50"},
                                  {"FtpUsername","NVarChar","50"},
                                  {"FtpPassword","NVarChar","50"},
                                  {"OutTime","NVarChar","50"},
                                  {"SavePath","NVarChar","50"},
                                  {"Alias","NVarChar","50"},
                                  {"Url","NVarChar","4000"},
                                 
                                 };
            return Tablelist;
        }

        /// <summary>
        /// 获取字段窜
        /// </summary>
        public  string GetFields()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "],";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 获取参数串
        /// </summary>
        public  string GetParas()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 获取字段=参数
        /// </summary>
        public  string GetFieldAndPara()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }

        public  SqlParameter[] GetParameters(M_FtpConfig model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.FtpServer;
            sp[2].Value = model.FtpPort;
            sp[3].Value = model.FtpUsername;
            sp[4].Value = model.FtpPassword;
            sp[5].Value = model.OutTime;
            sp[6].Value = model.SavePath;
            sp[7].Value = model.Alias;
            sp[8].Value = model.Url;
            return sp;
        }

        public static M_FtpConfig GetModelFromReader(SqlDataReader rdr)
        {
            M_FtpConfig model = new M_FtpConfig();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.FtpServer = rdr["FtpServer"].ToString();
            model.FtpPort = rdr["FtpPort"].ToString();
            model.FtpUsername = rdr["FtpUsername"].ToString();
            model.FtpPassword = rdr["FtpPassword"].ToString();
            model.OutTime = rdr["OutTime"].ToString();
            model.SavePath = rdr["SavePath"].ToString();
            model.Alias = rdr["Alias"].ToString();
            model.Url = rdr["Url"].ToString(); 
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
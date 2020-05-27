using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    /// <summary>
    /// DownServer 的摘要说明
    /// </summary>
    public class M_DownServer:M_Base
    {
        public M_DownServer()
        {
            this.Addtime = DateTime.Now;
            this.Encryptime = DateTime.Now;
            this.EncryptKey = string.Empty;
            this.ReadRoot = string.Empty;
            this.ServerLogo = string.Empty;
            this.ServerName = string.Empty;
            this.ServerUrl = string.Empty;
        }
        /// <summary>
        /// 镜像服务器ID
        /// </summary>
        public int ServerID { get; set; }
        /// <summary>
        /// 镜像服务器名
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 镜像服务器地址
        /// </summary>
        public string ServerUrl { get; set; }
        /// <summary>
        /// 镜像服务器Logo
        /// </summary>
        public string ServerLogo { get; set; }
        /// <summary>
        /// 镜像服务器排序ID
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 镜像服务器显示方式
        /// </summary>
        public int ShowType { get; set; }
        /// <summary>
        /// 加密方式
        /// </summary>
        public int UrlEncrypt { get; set; }
        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncryptKey { get; set; }
        /// <summary>
        /// 附加时间戳加密
        /// </summary>
        public int TimeEncrypt { get; set; }
        /// <summary>
        /// 更新时间戳间隔时间
        /// </summary>
        public int UpTimeuti { get; set; }
        /// <summary>
        /// 加密更新时间
        /// </summary>
        public DateTime Encryptime { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
        /// <summary>
        /// //允许访问用户组
        /// </summary>
        public string ReadRoot { get; set; }

        public override string PK { get { return "ServerID"; } }
        public override string TbName { get { return "ZL_DownServer"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ServerID","Int","4"},
                                  {"ServerName","NVarChar","50"},
                                  {"ServerUrl","NVarChar","50"},
                                  {"ServerLogo","NVarChar","255"}, 
                                  {"OrderID","Int","4"},
                                  {"ShowType","Int","4"},
                                  {"UrlEncrypt","Int","4"},
                                  {"EncryptKey","NVarChar","255"},
                                  {"TimeEncrypt","Int","4"}, 
                                  {"UpTimeuti","Int","4"},
                                  {"Addtime","DateTime","8"},
                                  {"Encryptime","DateTime","8"},
                                  {"ReadRoot","NVarChar","1000"}
                                 };
            return Tablelist;
        }
        public string GetFieldAndPara()
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
        public override SqlParameter[] GetParameters()
        {
            M_DownServer model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ServerID;
            sp[1].Value = model.ServerName;
            sp[2].Value = model.ServerUrl;
            sp[3].Value = model.ServerLogo;
            sp[4].Value = model.OrderID;
            sp[5].Value = model.ShowType;
            sp[6].Value = model.UrlEncrypt;
            sp[7].Value = model.UrlEncrypt;
            sp[8].Value = model.TimeEncrypt;
            sp[9].Value = model.UpTimeuti;
            sp[10].Value = model.Addtime;
            sp[11].Value = model.Encryptime;
            sp[12].Value = model.ReadRoot;
            return sp;
        }
        public M_DownServer GetModelFromReader(SqlDataReader rdr)
        {
            M_DownServer model = new M_DownServer();
            model.ServerID = Convert.ToInt32(rdr["ServerID"]);
            model.ServerName = ConverToStr(rdr["ServerName"]);
            model.ServerUrl = ConverToStr(rdr["ServerUrl"]);
            model.ServerLogo = ConverToStr(rdr["ServerLogo"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.ShowType = ConvertToInt(rdr["ShowType"]);
            model.UrlEncrypt = ConvertToInt(rdr["UrlEncrypt"]);
            model.EncryptKey = ConverToStr(rdr["EncryptKey"]);
            model.TimeEncrypt = ConvertToInt(rdr["TimeEncrypt"]);
            model.UpTimeuti = ConvertToInt(rdr["UpTimeuti"]);
            model.Addtime = ConvertToDate(rdr["Addtime"]); ;
            model.Encryptime = ConvertToDate(rdr["Encryptime"]);
            model.ReadRoot = ConverToStr(rdr["ReadRoot"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}

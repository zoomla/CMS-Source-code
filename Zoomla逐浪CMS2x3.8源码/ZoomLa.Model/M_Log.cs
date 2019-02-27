using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    //用于写入DB,文本,SQLite的模型类
    public class M_Log:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UName { get; set; }
        public string IP { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AName { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 来源页面,或自定义
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 操作,如上传等
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 操作详情,如上传文件名,
        /// </summary>
        public string Message { get; set; }
        public string Level { get; set; }
        public override string TbName { get { return "ZL_Log4"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UName","NVarChar","200"},
        		        		{"IP","NVarChar","500"},
        		        		{"AName","NVarChar","200"},
        		        		{"CDate","DateTime","8"},
        		        		{"Source","NVarChar","500"},
        		        		{"Action","NVarChar","100"},
                                {"Message","NVarChar","2000"},
                                {"Level","NVarChar","100"}
        };
            return Tablelist;
        }
        //FOR SQL
        public override SqlParameter[] GetParameters()
        {
            M_Log model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UName;
            sp[2].Value = model.IP;
            sp[3].Value = model.AName;
            sp[4].Value = model.CDate;
            sp[5].Value = model.Source;
            sp[6].Value = model.Action;
            sp[7].Value = model.Message;
            sp[8].Value = model.Level;
            return sp;
        }
        //FOR SQLite
        public Dictionary<string, object> ToDic()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("UName", UName);
            dic.Add("IP", IP);
            dic.Add("AName", AName);
            dic.Add("CDate", CDate);
            dic.Add("Source", Source);
            dic.Add("Action", Action);
            dic.Add("Message", Message);
            dic.Add("Level", Level);
            return dic;
        }
        public M_Log GetModelFromReader(SqlDataReader rdr)
        {
            M_Log model = new M_Log();
            model.ID = Convert.ToInt32(rdr["id"]);
            model.UName = ConverToStr(rdr["UName"]);
            model.IP = ConverToStr(rdr["IP"]);
            model.AName = ConverToStr(rdr["AName"]);
            model.CDate =ConvertToDate(rdr["CDate"]);
            model.Source = ConverToStr(rdr["Source"]);
            model.Action = ConverToStr(rdr["Action"]);
            model.Message = ConverToStr(rdr["Message"]);
            model.Level = ConverToStr(rdr["Level"]);
            rdr.Close();
            return model;
        }
        /// <summary>
        /// 文本格式,后期考虑支持Html格式
        /// </summary>
        public override string ToString()
        {
            string result = "";
            result += "操作人：(用户名:" + UName + ",管理员名:" + AName + ",IP:" + IP + ")\r\n";
            result += "时间：(" + CDate.ToString() + "),操作：(" + Action + "),来源：(" + Source + ")\r\n";
            result += "详情：(" + Message + ")\r\n";
            result += "/*-----------------------------------------------------------------------------------*/";
            return result;
        }
        /// <summary>
        /// 用于Html查看,暂不用实现
        /// </summary>
        /// <returns></returns>
        public string ToHtml() 
        {
            return "";
        }
    }
}

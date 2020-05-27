using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_CreateJS:M_Base
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 代码名称
        /// </summary>
        public string Jsname { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string JsReadme { get; set; }
        /// <summary>
        /// 内容代码格式
        /// </summary>
        public int ContentType { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string JsFileName { get; set; }
        /// <summary>
        /// JS类型
        /// </summary>
        public int JsType { get; set; }
        /// <summary>
        /// 配置XML内容
        /// </summary>
        public string JsXmlContent { get; set; }
        #endregion

        #region 构造函数
        public M_CreateJS()
        {
        }

        public M_CreateJS
        (
            int id,
            string Jsname,
            string JsReadme,
            int ContentType,
            string JsFileName,
            int JsType,
            string JsXmlContent
        )
        {
            this.id = id;
            this.Jsname = Jsname;
            this.JsReadme = JsReadme;
            this.ContentType = ContentType;
            this.JsFileName = JsFileName;
            this.JsType = JsType;
            this.JsXmlContent = JsXmlContent;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] CreateJSList()
        {
            string[] Tablelist = { "id", "Jsname", "JsReadme", "ContentType", "JsFileName", "JsType", "JsXmlContent" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_CreateJS"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"Jsname","NVarChar","255"},
                                  {"JsReadme","NVarChar","1000"}, 
                                  {"ContentType","Int","4"},
                                  {"JsFileName","NVarChar","255"},
                                  {"JsType","Int","4"}, 
                                  {"JsXmlContent","NText","10000"}
                                 };

            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_CreateJS model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.Jsname;
            sp[2].Value = model.JsReadme;
            sp[3].Value = model.ContentType;
            sp[4].Value = model.JsFileName;
            sp[5].Value = model.JsType;
            sp[6].Value = model.JsXmlContent;
            return sp;
        }

        public  M_CreateJS GetModelFromReader(SqlDataReader rdr)
        {
            M_CreateJS model = new M_CreateJS();
            model.id = Convert.ToInt32(rdr["id"]);
            model.Jsname = rdr["CourseName"].ToString();
            model.JsReadme = rdr["CoureseThrun"].ToString();
            model.ContentType = Convert.ToInt32(rdr["Hot"]);
            model.JsFileName = rdr["CoureseCredit"].ToString();
            model.JsType =Convert.ToInt32( rdr["CoureseRemark"]);
            model.JsXmlContent = rdr["Hot"].ToString();
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}



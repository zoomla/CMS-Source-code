using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_App : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Thumbnails { get; set; }
        public string Clearimg { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 绑定节点
        /// </summary>
        public int NodeID { get; set; }
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 起始Url
        /// </summary>
        public string Furl { get; set; }
        /// <summary>
        /// 用于存UserID
        /// </summary>
        public string Colors { get; set; }
        /// <summary>
        /// 打包模式: 0:Url打包,1:Html打包为APK,2:二级域名发布
        /// </summary>
        public int APKMode { get; set; }
        /// <summary>
        /// 压缩文件包名
        /// </summary>
        public string ZipFile { get; set; }
        /// <summary>
        /// 用户所选模板,绑定模板ID
        /// </summary>
        public string Template { get; set; }
        /// <summary>
        /// 所需要的插件功能
        /// </summary>
        public string Feature { get; set; }
        /// <summary>
        /// 用户提交的所有资源以及最后生成的APK,都在这目录之下
        /// </summary>
        public string APPDir { get; set; }
        /// <summary>
        /// 0:未生成,1:已生成
        /// </summary>
        public int MyStatus { get; set; }
        /// <summary>
        /// 现为存UserID,为将来扩展用string
        /// </summary>
        public string UserID { get; set; }
        public override string TbName { get { return "ZL_App"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"AppName","NVarChar","255"},
                                  {"Thumbnails","NVarChar","255"},
                                  {"Clearimg","NVarChar","255"},
                                  {"Description","Text","400"}, 
                                  {"Author","NVarChar","255"}, 
                                  {"NodeID","Int","4"}, 
                                  {"AddTime","DateTime","8"},
                                  {"Furl","NVarChar","255"},
                                  {"Colors","NVarChar","255"},
                                  {"MyStatus","Int","4"},
                                  {"APKMode","Int","4"},
                                  {"ZipFile","NVarChar","500"},
                                  {"Template","NVarChar","500"},
                                  {"Feature","NVarChar","1000"},
                                  {"APPDir","NVarChar","1000"},
                                  {"UserID","NVarChar","100"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_App model = this;
            if (model.AddTime <= DateTime.MinValue) { model.AddTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.AppName;
            sp[2].Value = model.Thumbnails;
            sp[3].Value = model.Clearimg;
            sp[4].Value = model.Description;
            sp[5].Value = model.Author;
            sp[6].Value = model.NodeID;
            sp[7].Value = model.AddTime;
            sp[8].Value = model.Furl;
            sp[9].Value = model.Colors;
            sp[10].Value = model.MyStatus;
            sp[11].Value = model.APKMode;
            sp[12].Value = model.ZipFile;
            sp[13].Value = model.Template;
            sp[14].Value = model.Feature;
            sp[15].Value = model.APPDir;
            sp[16].Value = model.UserID;
            return sp;
        }
        public M_App GetModelFromReader(SqlDataReader rdr)
        {
            M_App model = new M_App();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.AppName = ConverToStr(rdr["AppName"]);
            model.Thumbnails = ConverToStr(rdr["Thumbnails"]);
            model.Clearimg = ConverToStr(rdr["Clearimg"]);
            model.Description = ConverToStr(rdr["Description"]);
            model.Author = ConverToStr(rdr["Author"].ToString());
            model.NodeID = ConvertToInt(rdr["NodeID"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.Furl = ConverToStr(rdr["Furl"]);
            model.Colors = ConverToStr(rdr["Colors"]);
            model.MyStatus = ConvertToInt(rdr["MyStatus"]);
            model.APKMode = ConvertToInt(rdr["APKMode"]);
            model.ZipFile = ConverToStr(rdr["ZipFile"]);
            model.Template = ConverToStr(rdr["Template"]);
            model.Feature = ConverToStr(rdr["Feature"]);
            model.APPDir = ConverToStr(rdr["APPDir"]);
            model.UserID = ConverToStr(rdr["UserID"]);
            rdr.Dispose();
            return model;
        }
    }
}

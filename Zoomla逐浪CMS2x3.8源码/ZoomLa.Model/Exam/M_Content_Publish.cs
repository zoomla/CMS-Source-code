using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Exam
{
    //后期根据需要分表
    public class M_Content_Publish : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 图片虚拟路径
        /// </summary>
        public string ImgPath { get; set; }
        public string Json { get; set; }
        /// <summary>
        /// 版面名:第A02版：要闻
        /// </summary>
        public string Title { get; set; }
        public int CUser { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// Pid为0则为报纸,否则为版面
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 报纸名
        /// </summary>
        public string NewsName { get; set; }
        /// <summary>
        /// 存储PDF,Zip等高清报纸文件
        /// </summary>
        public string AttachFile { get; set; }
        /// <summary>
        ///节点id
        /// </summary>
        public int Nid { get; set; }
        public override string TbName { get { return "ZL_Content_Publish"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"ImgPath","NVarChar","300"},
        		        		{"Json","NVarChar","4000"},
                                {"Title","NVarChar","200"},
                                {"CUser","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"Pid","Int","4"},
                                {"NewsName","NVarChar","200"},
                                {"AttachFile","NVarChar","1000"},
                                {"Nid","Int","4"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Content_Publish model=this;
            if (CDate < DateTime.MinValue) CDate = DateTime.Now;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ImgPath;
            sp[2].Value = model.Json;
            sp[3].Value = model.Title;
            sp[4].Value = model.CUser;
            sp[5].Value = model.CDate;
            sp[6].Value = model.Pid;
            sp[7].Value = model.NewsName;
            sp[8].Value = model.AttachFile;
            sp[9].Value = model.Nid;
            return sp;
        }
        public M_Content_Publish GetModelFromReader(SqlDataReader rdr)
        {
            M_Content_Publish model = new M_Content_Publish();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ImgPath = rdr["ImgPath"].ToString();
            model.Json = rdr["Json"].ToString();
            model.Title = ConverToStr(rdr["Title"]);
            model.CUser = ConvertToInt(rdr["CUser"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Pid = ConvertToInt(rdr["Pid"]);
            model.NewsName = ConverToStr(rdr["NewsName"]);
            model.AttachFile = ConverToStr(rdr["AttachFile"]);
            model.Nid = ConvertToInt(rdr["Nid"]);
            rdr.Close();
            return model;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ZoomLa.Model
{
    public class M_Content_Video:M_Base
    {
        public int ID { get; set; }
        public string VName { get; set; }
        public string VSize { get; set; }
        public string VTime { get; set; }
        public int UserID { get; set; }
        public DateTime CDate { get; set; }
        public string Thumbnail { get; set; } 
        public string Desc { get; set; }
        public string VPath { get; set; }

        public override string TbName { get { return "ZL_Content_Video"; } }
        public M_Content_Video()
        {
            this.CDate = DateTime.Now;
        }
        public override string[,] FieldList()
        {
            string[,] fieldlist ={
                                     {"ID","Int","4"},
                                     {"VName","NVarChar","100"},
                                     {"VSize","NVarChar","4"},
                                     {"VTime","NVarChar","8"},
                                     {"UserID","Int","4"},
                                     {"CDate","DateTime","8"},
                                     {"Thumbnail","NVarChar","1000"},
                                     {"Desc","NVarChar","3000"},
                                     {"VPath","NVarChar","1000"}
                                };
            return fieldlist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Content_Video model = this;
            if (model.CDate <= DateTime.MinValue) { model.CDate = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.VName;
            sp[2].Value = model.VSize;
            sp[3].Value = model.VTime;
            sp[4].Value = model.UserID;
            sp[5].Value = model.CDate;
            sp[6].Value = model.Thumbnail;
            sp[7].Value = model.Desc;
            sp[8].Value = model.VPath;
            return sp;
        }
        public M_Content_Video GetModelFromReader(SqlDataReader rdr)
        {
            M_Content_Video model = new M_Content_Video();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.VName = ConverToStr(rdr["VName"]);
            model.VSize = ConverToStr(rdr["VSize"]);
            model.VTime = ConverToStr(rdr["VTime"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.Thumbnail = ConverToStr(rdr["Thumbnail"]);
            model.Desc = ConverToStr(rdr["Desc"]);
            model.VPath = ConverToStr(rdr["VPath"]);
            return model;
        }
    }
    public class VideoFile
    {
        #region Properties
        public string Path
        {
            get;
            set;
        }
        public TimeSpan Duration { get; set; }
        public double BitRate { get; set; }
        public string AudioFormat { get; set; }
        public string VideoFormat { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string RawInfo { get; set; }
        public bool infoGathered { get; set; }
        #endregion
        public VideoFile(string path)
        {
            Path = path;
            Initialize();
        }
        private void Initialize()
        {
            this.infoGathered = false;
            //first make sure we have a value for the video file setting
            if (string.IsNullOrEmpty(Path))
            {
                throw new Exception("Could not find the location of the video file");
            }
            //Now see if the video file exists
            if (!File.Exists(Path))
            {
                throw new Exception("The video file " + Path + " does not exist.");
            }
        }
    }
    public class OutputPackage
    {
        public MemoryStream VideoStream { get; set; }
        public string VPath { get; set; }
        public string RawOutput { get; set; }
        public bool Success { get; set; }
    }
}

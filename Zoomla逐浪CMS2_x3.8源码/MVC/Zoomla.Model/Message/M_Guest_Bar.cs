using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Message
{
    public class M_Guest_Bar:M_Base
    {
        public int ID { get; set; }
        public string Gid { get; set; }
        /// <summary>
        /// 所属贴吧
        /// </summary>
        public int CateID { get; set; }
        /// <summary>
        /// 所属留言,便于获取
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 要回复哪个贴子(允许子贴)
        /// </summary>
        public int ReplyID { get; set; }
        public int ReplyUserID { get; set; }
        public string Title
        {
            get;
            set;
        }
        public string MsgContent { get; set; }
        public string IP { get; set; }
        private string _ColledIDS;
        /// <summary>
        /// 收藏了该条信息的用户IDS
        /// </summary>
        public string ColledIDS
        {
            get { return string.IsNullOrEmpty(_ColledIDS) ? "" : _ColledIDS; }
            set { _ColledIDS = value; }
        }
        public int MsgType { get; set; }
        public int Status { get; set; }
        /// <summary>
        /// 创建人UserID
        /// </summary>
        public int CUser { get; set; }
        public string CUName{get;set;}
        public DateTime CDate { get; set; }
        public string SubTitle { get; set; }
        public string PostFlag { get; set; }
        public string Style { get; set; }
        //1,版面置顶；2,全局置顶
        public int OrderFlag { get; set; }
        public int HitCount { get; set; }
        /// <summary>
        /// 身份标记,用于区分不同的匿名用户
        /// </summary>
        public string IDCode { get; set; }
        public string LikeIDS { get; set; }
        //最近一次的回贴记录
        public int R_CUser { get; set; }
        public string R_CUName { get; set; }
        public DateTime R_CDate { get; set; }
        ///// <summary>
        ///// 由何处提交: 0:贴吧,1:能力
        ///// </summary>
        //public int AddFrom { get; set; }
        public override string TbName { get { return "ZL_Guest_Bar"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
                                {"CateID","Int","4"},
                                {"Pid","Int","4"},
                                {"ReplyID","Int","4"},
                                {"ReplyUserID","Int","4"},
        		        		{"Title","NVarChar","200"},
        		        		{"MsgContent","NText","50000"},
                                {"IP","NVarChar","200"},
                                {"ColledIDS","NVarChar","4000"},
        		        		{"MsgType","Int","4"},
        		        		{"Status","Int","4"},
        		        		{"CUser","Int","4"},
                                {"CUName","NVarChar","50"},
        		        		{"CDate","DateTime","8"},
                                {"SubTitle","NVarChar","3000"},
                                {"PostFlag","NVarChar","300"},
                                {"Style","NVarChar","500"},
                                {"OrderFlag","Int","4"},
                                {"HitCount","Int","4"},
                                {"IDCode","NVarChar","50"},
                                {"LikeIDS","NVarChar","500"},
                                {"R_CUser","Int","4"},
                                {"R_CUName","NVarChar","50"},
                                {"R_CDate","DateTime","8"},
                                {"Gid","VarChar","50"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Guest_Bar model = this;
            EmptyDeal();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.CateID;
            sp[2].Value = model.Pid;
            sp[3].Value = model.ReplyID;
            sp[4].Value = model.ReplyUserID;
            sp[5].Value = model.Title;
            sp[6].Value = model.MsgContent;
            sp[7].Value = model.IP;
            sp[8].Value = model.ColledIDS;
            sp[9].Value = model.MsgType;
            sp[10].Value = model.Status;
            sp[11].Value = model.CUser;
            sp[12].Value = model.CUName;
            sp[13].Value = model.CDate;
            sp[14].Value = model.SubTitle;
            sp[15].Value = model.PostFlag;
            sp[16].Value = model.Style;
            sp[17].Value = model.OrderFlag;
            sp[18].Value = model.HitCount;
            sp[19].Value = model.IDCode;
            sp[20].Value = ConverToStr(model.LikeIDS);
            sp[21].Value = model.R_CUser;
            sp[22].Value = model.R_CUName;
            sp[23].Value = model.R_CDate;
            sp[24].Value = model.Gid;
            return sp;
        }
        public M_Guest_Bar GetModelFromReader(DbDataReader rdr)
        {
            M_Guest_Bar model = new M_Guest_Bar();
            model.ID = Convert.ToInt32(rdr["id"]);
            model.CateID = Convert.ToInt32(rdr["CateID"]);
            model.Pid = Convert.ToInt32(rdr["Pid"]);
            model.ReplyID = Convert.ToInt32(rdr["ReplyID"]);
            model.ReplyUserID = Convert.ToInt32(rdr["ReplyUserID"]);
            model.Title = rdr["Title"].ToString();
            model.MsgContent = rdr["MsgContent"].ToString();
            model.IP = ConverToStr(rdr["IP"]);
            model.ColledIDS =  ConverToStr(rdr["ColledIDS"]);
            model.MsgType = ConvertToInt(rdr["MsgType"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.CUser = ConvertToInt(rdr["CUser"]);
            model.CUName = ConverToStr(rdr["CUName"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.SubTitle = ConverToStr(rdr["SubTitle"]);
            model.PostFlag = ConverToStr(rdr["PostFlag"]);
            model.Style = ConverToStr(rdr["Style"]);
            model.OrderFlag = ConvertToInt(rdr["OrderFlag"]);
            model.HitCount = ConvertToInt(rdr["HitCount"]);
            model.IDCode = ConverToStr(rdr["IDCode"]);
            model.LikeIDS = ConverToStr(rdr["LikeIDS"]);
            model.R_CUser = ConvertToInt(rdr["R_CUser"]);
            model.R_CUName = ConverToStr(rdr["R_CUName"]);
            model.R_CDate = ConvertToDate(rdr["R_CDate"]);
            model.Gid = ConverToStr(rdr["Gid"]);
            rdr.Close();
            return model;
        }
        public M_Guest_Bar() 
        {
            Gid = Guid.NewGuid().ToString();
        }
        public void EmptyDeal()
        {
            if (string.IsNullOrEmpty(ColledIDS)) ColledIDS = "";
            if (string.IsNullOrEmpty(Title)) Title = "";
            if (string.IsNullOrEmpty(PostFlag)) PostFlag = "";
            if (CDate <= DateTime.MinValue) { CDate = DateTime.Now; }
            //发主贴,自动填充
            if (R_CDate.Year < 1901) { R_CUser = CUser; R_CUName = CUName; R_CDate = CDate; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace ZoomLa.Model.Plat
{
    public class M_Blog_Msg:ZoomLa.Model.M_Base
    {
        public int ID { get; set; }
        public string Gid { get; set; }
        /// <summary>
        /// 所属留言,便于获取
        /// </summary>
        public int pid { get; set; }
        public string Title
        {
            get;
            set;
        }
        public string MsgContent { get; set; }
        /// <summary>
        /// 1:普通,2:投票,3:长文章
        /// </summary>
        public int MsgType { get; set; }
        public int Status { get; set; }
        public int CUser { get; set; }
        public DateTime CDate { get; set; }
        /// <summary>
        /// 回复的留言ID
        /// </summary>
        public int ReplyID { get; set; }
        public string CUName { get; set; }
        public int ReplyUserID { get; set; }
        public string ReplyUName { get; set; }
        /// <summary>
        /// 附件,存路径
        /// </summary>
        public string Attach { get; set; }
        private string _ColledIDS;
        /// <summary>
        /// 收藏了该条信息的用户IDS
        /// </summary>
        public string ColledIDS 
        {
            get { return string.IsNullOrEmpty(_ColledIDS)?"":_ColledIDS; }
            set { _ColledIDS = value; } 
        }
        private string _GroupIDS;
        /// <summary>
        /// 能看到该条信息的组 为空:全部,IDS:组,0:仅自己
        /// </summary>
        public string GroupIDS
        {
            get
            {
                if (!string.IsNullOrEmpty(_GroupIDS))
                {
                    _GroupIDS = "," + _GroupIDS.Trim(',') + ",";
                }
                return _GroupIDS;
            }
            set { _GroupIDS = value; }
        }
        /// <summary>
        /// 投票选项
        /// </summary>
        public string VoteOP { get; set; }
        private string _voteResult="";
        /// <summary>
        /// 投票结果   所选ID,用户ID
        /// </summary>
        public string VoteResult 
        {
            get { if (!string.IsNullOrEmpty(_voteResult)) { _voteResult = "," + _voteResult.Trim(',') + ","; } return _voteResult; }
            set { _voteResult = value; }
        }
        /// <summary>
        /// 到期时间,用于投票等
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 点选人的IDS
        /// </summary>
        public string LikeIDS { get; set; }
        /// <summary>
        /// 转发的目标博客或文章ID
        /// </summary>
        public int ForWardID { get; set; }
        /// <summary>
        /// 该条信息属于哪家公司
        /// </summary>
        public int CompID { get; set; }
        /// <summary>
        /// 所属项目ID,非项目成员不能浏览,0为不属于任何项目
        /// </summary>
        public int ProID { get; set; }
        private string _atuser;
        /// <summary>
        /// 被AT用户列表,用户浏览后移除
        /// </summary>
        public string ATUser
        {
            get
            {
                if (!string.IsNullOrEmpty(_atuser))
                {
                    _atuser = ("," + _atuser.Trim().Trim(',') + ",").Replace(",,",",");
                }
                return _atuser;
            }
            set { _atuser = value; }
        }
        /// <summary>
        /// 地理位置[Json]
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 该信息所拥有话题,以,切割
        /// </summary>
        public string Topic { get; set; }
        public override string TbName { get { return "ZL_Plat_Blog"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"pid","Int","4"},
        		        		{"Title","NVarChar","4000"},
        		        		{"MsgContent","NText","10000"},
        		        		{"MsgType","Int","4"},
        		        		{"Status","Int","4"},
        		        		{"CUser","Int","4"},
        		        		{"CDate","DateTime","8"},
                                {"ReplyID","Int","4"},
                                {"CUName","NVarChar","30"},
                                {"ReplyUserID","Int","4"},
                                {"ReplyUName","NVarChar","30"},
                                {"Attach","NVarChar","3000"},
                                {"ColledIDS","NVarChar","4000"},
                                {"GroupIDS","NVarChar","500"},
                                {"VoteOP","NVarChar","500"},
                                {"VoteResult","NText","4000"},
                                {"EndTime","DateTime","8"},
                                {"LikeIDS","NText","4000"},
                                {"ForwardID","Int","4"},
                                {"ProID","Int","4"},
                                {"CompID","Int","4"},
                                {"ATUser","NVarChar","500"},
                                {"Location","NVarChar","2000"},
                                {"Gid","VarChar","50"},
                                {"Topic","NVarChar","4000"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Blog_Msg model = this;
            EmptyDeal();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.pid;
            sp[2].Value = model.Title;
            sp[3].Value = model.MsgContent;
            sp[4].Value = model.MsgType;
            sp[5].Value = model.Status;
            sp[6].Value = model.CUser;
            sp[7].Value = model.CDate;
            sp[8].Value = model.ReplyID;
            sp[9].Value = model.CUName;
            sp[10].Value = model.ReplyUserID;
            sp[11].Value = model.ReplyUName;
            sp[12].Value = model.Attach;
            sp[13].Value = model.ColledIDS;
            sp[14].Value = model.GroupIDS;
            sp[15].Value = model.VoteOP;
            sp[16].Value = model.VoteResult;
            sp[17].Value = model.EndTime;
            sp[18].Value = model.LikeIDS;
            sp[19].Value = model.ForWardID;
            sp[20].Value = model.ProID;
            sp[21].Value = model.CompID;
            sp[22].Value = model.ATUser;
            sp[23].Value = model.Location;
            sp[24].Value = model.Gid;
            sp[25].Value = model.Topic;
            return sp;
        }
        public M_Blog_Msg GetModelFromReader(DbDataReader rdr)
        {
            M_Blog_Msg model = new M_Blog_Msg();
            model.ID = ConvertToInt(rdr["id"]);
            model.pid = ConvertToInt(rdr["pid"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.MsgContent = ConverToStr(rdr["MsgContent"]);
            model.MsgType = ConvertToInt(rdr["MsgType"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.CUser = ConvertToInt(rdr["CUser"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.ReplyID = ConvertToInt(rdr["ReplyID"]);
            model.CUName = ConverToStr(rdr["CUName"]);
            model.ReplyUserID = ConvertToInt(rdr["ReplyUserID"]);
            model.ReplyUName = ConverToStr(rdr["ReplyUName"]);
            model.Attach = ConverToStr(rdr["Attach"]);
            model.ColledIDS = ConverToStr(rdr["ColledIDS"]);
            model.GroupIDS = ConverToStr(rdr["GroupIDS"]);
            model.VoteOP = ConverToStr(rdr["VoteOP"]);
            model.VoteResult = ConverToStr(rdr["VoteResult"]);
            model.EndTime = ConvertToDate(rdr["EndTime"]);
            model.LikeIDS = ConverToStr(rdr["LikeIDS"]);
            model.ForWardID = ConvertToInt(rdr["ForWardID"]);
            model.ProID = ConvertToInt(rdr["ProID"]);
            model.CompID = ConvertToInt(rdr["CompID"]);
            model.Location = ConverToStr(rdr["Location"]);
            model.Gid = ConverToStr(rdr["Gid"]);
            model.Topic = ConverToStr(rdr["Topic"]);
            rdr.Close();
            return model;
        }
        public M_Blog_Msg()
        {
            Gid = Guid.NewGuid().ToString();
            ATUser = "";
            VoteResult = "";
            LikeIDS = "";
            Location = "";
            Status = (int)ZLEnum.ConStatus.Audited;
            CDate = DateTime.Now;
            EndTime = DateTime.Now;
        }
        public void EmptyDeal() 
        {
            if (string.IsNullOrEmpty(ColledIDS)) ColledIDS = "";
            if (string.IsNullOrEmpty(Title)) Title = "";
            if (string.IsNullOrEmpty(VoteOP)) VoteOP = "";
            else//非空值则移除其中的可能空值
            {
                VoteOP = VoteOP.Replace(" ", "").Replace(",,", ",");
            }
        }
    }
   
}

using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    /// <summary>
    /// 评论信息
    /// </summary>
    public class M_Comment:M_Base
    {
        #region 字段字义
        public int Pid { get; set; }
        public int Status { get; set; }
        public string Files { get; set; }
        /// <summary>
        /// 评论ID
        /// </summary>
        public int CommentID { get; set; }
        /// <summary>
        /// 被评论的内容ID
        /// </summary>
        public int GeneralID { get; set; }
        /// <summary>
        /// 评论标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 是否审核通过
        /// </summary>
        public bool Audited { get; set; }
        /// <summary>
        /// 发表评论的用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 评论发表时间
        /// </summary>
        public DateTime CommentTime { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 是否支持方
        /// </summary>
        public bool PKS { get; set; }
        /// <summary>
        /// 类型 0文章 2目标
        /// </summary>
        public int Type { get; set; }
        public string ReportIDS { get; set; }
        #endregion
        #region 构造函数
        public M_Comment()
        {
            this.Files = string.Empty;
            this.Title = string.Empty;
            this.Contents = string.Empty;
        }
        #endregion
         
        public override string PK { get { return "CommentID"; } }
        public override string TbName { get { return "ZL_Comment"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"CommentID","Int","4"},
                                  {"GeneralID","Int","4"},
                                  {"Title","NVarChar","255"},
                                  {"Contents","NVarChar","300"}, 
                                  {"Audited","Bit","2"},
                                  {"UserID","Int","4"},
                                  {"CommentTime","DateTime","8"},
                                  {"Score","Int","4"}, 
                                  {"PKS","Bit","2"},
                                  {"files","NVarChar","200"},
                                  {"status","Int","4"}, 
                                  {"pid","Int","4"}, 
                                  {"Type","Int","4"},
                                  {"ReprotIDS","NVarChar","1000"}
                                 };
            return Tablelist;
        }
          

        public override SqlParameter[] GetParameters()
        {
            M_Comment model = this;
            if (string.IsNullOrEmpty(ReportIDS)) { ReportIDS = ""; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.CommentID;
            sp[1].Value = model.GeneralID;
            sp[2].Value = model.Title;
            sp[3].Value = model.Contents;
            sp[4].Value = model.Audited;
            sp[5].Value = model.UserID;
            sp[6].Value = model.CommentTime;
            sp[7].Value = model.Score;
            sp[8].Value = model.PKS;
            sp[9].Value = model.Files;
            sp[10].Value = model.Status;
            sp[11].Value = model.Pid;
            sp[12].Value = model.Type;
            sp[13].Value = model.ReportIDS;
            return sp;
        }

        public M_Comment GetModelFromReader(SqlDataReader rdr)
        {
            M_Comment model = new M_Comment();
            model.CommentID = Convert.ToInt32(rdr["CommentID"]);
            model.GeneralID = ConvertToInt(rdr["GeneralID"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Contents = ConverToStr(rdr["Contents"]);
            model.Audited = ConverToBool(rdr["Audited"]);
            model.UserID = ConvertToInt(rdr["UserID"]);
            model.CommentTime = ConvertToDate(rdr["CommentTime"]);
            model.Score = ConvertToInt(rdr["Score"]);
            model.PKS = ConverToBool(rdr["PKS"]);
            model.Files = ConverToStr(rdr["files"]);
            model.Status = ConvertToInt(rdr["status"]);
            model.Pid = ConvertToInt(rdr["pid"]);
            model.Type = ConvertToInt(rdr["Type"]);
            model.ReportIDS = ConverToStr(rdr["ReprotIDS"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Shop
{
    /// <summary>
    /// 用户晒单评论与回复
    /// </summary>
    public class M_Order_Share : M_Base
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 评分1-5
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Labels { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string MsgContent { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Imgs { get; set; }
        /// <summary>
        /// 是否匿名 0:否,1:是
        /// </summary>
        public int IsAnonymous { get; set; }
        public DateTime CDate { get; set; }
        public int OrderID { get; set; }
        public int ProID { get; set; }
        /// <summary>
        /// 0为主列表,其余为回复信息
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 回复的信息ID
        /// </summary>
        public int ReplyID { get; set; }
        /// <summary>
        /// 回复的用户ID,用户名和组动态获取(视图?)
        /// </summary>
        public int ReplyUid { get; set; }
        public M_Order_Share()
        {
            Pid = 0;
            IsAnonymous = 0;
            CDate = DateTime.Now;
            OrderDate = DateTime.Now;
        }
        public override string TbName { get { return "ZL_Order_Share"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                {"ID","Int","4"},
                                {"UserID","Int","4"},
                                {"Title","NVarChar","200"},
                                {"Score","Int","4"},
                                {"Labels","NVarChar","400"},
                                {"MsgContent","NVarChar","500"},
                                {"Imgs","NVarChar","4000"},
                                {"IsAnonymous","Int","4"},
                                {"CDate","DateTime","8"},
                                {"OrderID","Int","4"},//
                                {"ProID","Int","4"},
                                {"Pid","Int","4"},
                                {"OrderDate","DateTime","8"},
                                {"ReplyID","Int","4"},
                                {"ReplyUid","Int","4"}
        };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Order_Share model = this;
            if (model.Score < 1) { model.Score = 1; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.Title;
            sp[3].Value = model.Score;
            sp[4].Value = model.Labels;
            sp[5].Value = model.MsgContent;
            sp[6].Value = model.Imgs;
            sp[7].Value = model.IsAnonymous;
            sp[8].Value = model.CDate;
            sp[9].Value = model.OrderID;
            sp[10].Value = model.ProID;
            sp[11].Value = model.Pid;
            sp[12].Value = model.OrderDate;
            sp[13].Value = model.ReplyID;
            sp[14].Value = model.ReplyUid;
            return sp;
        }
        public M_Order_Share GetModelFromReader(SqlDataReader rdr)
        {
            M_Order_Share model = new M_Order_Share();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.Title = ConverToStr(rdr["Title"]);
            model.Score = Convert.ToInt32(rdr["Score"]);
            model.Labels = ConverToStr(rdr["Labels"]);
            model.MsgContent = ConverToStr(rdr["MsgContent"]);
            model.Imgs = ConverToStr(rdr["Imgs"]);
            model.IsAnonymous = ConvertToInt(rdr["IsAnonymous"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.ProID = ConvertToInt(rdr["ProID"]);
            model.Pid = ConvertToInt(rdr["Pid"]);
            model.OrderDate = ConvertToDate(rdr["OrderDate"]);
            model.ReplyID = ConvertToInt(rdr["ReplyID"]);
            model.ReplyUid = ConvertToInt(rdr["ReplyUid"]);
            rdr.Close();
            return model;
        }
    }
}

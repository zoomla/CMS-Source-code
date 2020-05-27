using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Chat : M_Base
    {
        #region 构造函数
        public M_Chat()
        {
        }

        public M_Chat
        (
            int ID,
            string Content,
            DateTime SendDatetime,
            int SendUserID,
            int AdmitUserId,
            int type,
            int RoomID,
            string All_People
        )
        {
            this.ID = ID;
            this.Content = Content;
            this.SendDatetime = SendDatetime;
            this.SendUserID = SendUserID;
            this.AdmitUserId = AdmitUserId;
            this.type = type;
            this.RoomId = RoomID;
            this.All_People = All_People;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] ChatList()
        {
            string[] Tablelist = { "ID", "Content", "SendDatetime", "SendUserID", "AdmitUserId", "type", "RoomID", "All_People" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendDatetime { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        public int SendUserID { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public int AdmitUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public int RoomId { get; set; }

        public string All_People { get; set; }
        public string ImgUrl { get; set; }
        public int state { get; set; }
        #endregion

        public override string TbName { get { return "ZL_Chat"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Content","NVarChar","1000"},
                                  {"SendDatetime","DateTime","8"},
                                  {"SendUserID","Int","4"},
                                  {"AdmitUserId","Int","4"},
                                  {"type","Int","4"},
                                  {"All_People","VarChar","5000"},
                                  {"ImgUrl","NVarChar","500"},
                                  {"Roomid","Int","4"},
                                  {"state","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Chat model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Content;
            sp[2].Value = model.SendDatetime;
            sp[3].Value = model.SendUserID;
            sp[4].Value = model.AdmitUserId;
            sp[5].Value = model.type;
            sp[6].Value = model.All_People;
            sp[7].Value = model.ImgUrl;
            sp[8].Value = model.RoomId;
            sp[9].Value = model.state;
            return sp;
        }

        public M_Chat GetModelFromReader(SqlDataReader rdr)
        {
            M_Chat model = new M_Chat();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.SendDatetime = ConvertToDate(rdr["SendDatetime"]);
            model.SendUserID = ConvertToInt(rdr["SendUserID"]);
            model.AdmitUserId = ConvertToInt(rdr["AdmitUserId"]);
            model.type = ConvertToInt(rdr["type"]);
            model.All_People = ConverToStr(rdr["All_People"]);
            model.ImgUrl = ConverToStr(rdr["ImgUrl"]);
            model.RoomId = ConvertToInt(rdr["Roomid"]);
            model.state = ConvertToInt(rdr["state"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    public class M_WX_ReplyMsg:M_Base
    {
        public int ID { get; set; }
        //序列化后的图文消息
        public string Content { get; set; }
        /// <summary>
        /// 0:文本(text),1:图文,2:视频,3:语音
        /// </summary>
        public string MsgType = "";
        public string EventType = "";
        //媒体id(微信多媒体标识)
        public string media_id = "";
        //回复条件
        public string fiter { get; set; }
        public int AppId { get; set; }
        /// <summary>
        /// 是否默认1:是(每人公众号可设一个)
        /// </summary>
        public int IsDefault { get; set; }
        public override string TbName { get { return "ZL_WX_ReplyMsg"; } }
        public override string[,] FieldList()
        {
            string[,] tablelist ={
                                     {"ID","Int","4"},
                                     {"MsgType","VarChar","100"},
                                     {"EventType","VarChar","100"},
                                     {"Content","NVarChar","3000"},
                                     {"media_id","NVarChar","500"},
                                     {"Filter","NVarChar","4"},
                                     {"AppId","Int","4"},
                                     {"IsDefault","Int","4"}
                                };
            return tablelist;
        }
        public SqlParameter[] GetParameters(M_WX_ReplyMsg model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.MsgType;
            sp[2].Value = model.EventType;
            sp[3].Value = model.Content;
            sp[4].Value = model.media_id;
            sp[5].Value = model.fiter;
            sp[6].Value = model.AppId;
            sp[7].Value = model.IsDefault;
            return sp;
        }
        public M_WX_ReplyMsg GetModelFromReader(SqlDataReader rdr)
        {
            M_WX_ReplyMsg model = new M_WX_ReplyMsg();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.MsgType = rdr["MsgType"].ToString();
            model.EventType = rdr["EventType"].ToString();
            model.Content = rdr["Content"].ToString();
            model.media_id = rdr["media_id"].ToString();
            model.fiter = rdr["Filter"].ToString();
            model.AppId = Convert.ToInt32(rdr["ID"]);
            model.IsDefault = ConvertToInt(rdr["IsDefault"]);
            rdr.Close();
            return model;
        }
    }
}

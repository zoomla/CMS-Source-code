using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Chat
{
    //先考虑性能,后期再考虑安全
    //每过一段时间将已读过和超时未接收的数据抛弃,批量写入数据库
    public class M_ChatMsg : M_Base
    {
        public M_ChatMsg()
        {
            MsgType = 1;
            CDate = DateTime.Now;
        }
        public int ID { get; set; }
        public string UserID { get; set; }
        //读取真名
        public string UserName { get; set; }
        //发消息的人的头像
        public string UserFace { get; set; }
        //接收人(直接移除吧)
        public string ReceUser { get; set; }
        public string Content { get; set; }
        public DateTime CDate { get; set; }
        public int MsgType { get; set; }
        public override string TbName { get { return "ZL_Chat_Msg"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"UserID","NVarChar","50"},
        		        		{"UserName","NVarChar","200"},
        		        		{"UserFace","NVarChar","100"},
        		        		{"ReceUser","VarChar","500"},
                                {"Content","NText","5000"},
                                {"CDate","DateTime","8"},
                                {"MsgType","Int","4"}
             };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_ChatMsg model = this;
            EmptyDeal();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.UserName;
            sp[3].Value = model.UserFace;
            sp[4].Value = model.ReceUser;
            sp[5].Value = model.Content;
            sp[6].Value = model.CDate;
            sp[7].Value = model.MsgType;
            return sp;
        }
        public M_ChatMsg GetModelFromReader(DbDataReader rdr)
        {
            M_ChatMsg model = new M_ChatMsg();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = ConverToStr(rdr["UserID"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.UserFace = ConverToStr(rdr["UserFace"]);
            model.ReceUser = ConverToStr(rdr["ReceUser"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.CDate = ConvertToDate(rdr["CDate"]);
            model.MsgType = ConvertToInt(rdr["ID"]);
            rdr.Close();
            return model;
        }
        public void EmptyDeal()
        {
            if (CDate <= DateTime.MinValue) CDate = DateTime.Now;
            if (MsgType < 1) MsgType = 1;
        }

    }
}

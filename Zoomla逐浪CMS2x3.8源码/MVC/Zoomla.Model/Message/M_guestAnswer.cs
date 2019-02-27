using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_GuestAnswer:M_Base
    {
        public int ID{get;set;}
        /// <summary>
        /// 问题ID
        /// </summary>
        public int QueId{get;set;}

        /// <summary>
        /// 回答
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 回答时间
        /// </summary>
        public DateTime AddTime{get;set;}

        /// <summary>
        /// 回答人Id
        /// </summary>
        public int UserId{get;set;}

        /// <summary>
        /// 回答人
        /// </summary>
        public string UserName{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///回答追问
        /// </summary> 
        public int supplymentid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int audit { get; set; }

        public int IsNi { get; set; }
        public M_GuestAnswer() { AddTime = DateTime.Now; }
        public override string TbName { get { return "ZL_GuestAnswer"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"}, 
                                  {"QueId","Int","4"}, 
                                  {"Content","NVarChar","4000"}, 
                                  {"AddTime","DateTime","8"}, 
                                  {"UserId","Int","4"}, 
                                  {"UserName","NVarChar","255"}, 
                                  {"Status","Int","4"},
                                  {"supplymentid","Int","4"},
                                  {"audit","Int","4"},
                                  {"IsNi","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_GuestAnswer model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.QueId;
            sp[2].Value = model.Content;
            sp[3].Value = model.AddTime;
            sp[4].Value = model.UserId;
            sp[5].Value = model.UserName;
            sp[6].Value = model.Status;
            sp[7].Value = model.supplymentid;
            sp[8].Value = model.audit;
            sp[9].Value = model.IsNi;
            return sp;
        }

        public M_GuestAnswer GetModelFromReader(SqlDataReader rdr)
        {
            M_GuestAnswer model = new M_GuestAnswer();
            model.ID = Convert.ToInt32(rdr["ID"].ToString());
            model.QueId = Convert.ToInt32(rdr["QueId"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.UserId = Convert.ToInt32(rdr["UserId"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.supplymentid = ConvertToInt(rdr["supplymentid"]);
            model.audit = ConvertToInt(rdr["audit"]);
            model.IsNi = ConvertToInt(rdr["IsNi"]);
            rdr.Close();
            return model;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Shop
{
    public class M_Shop_PrintMessage : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 信息/订单编号
        /// </summary>
        public string MsgNo { get; set; }
        /// <summary>
        /// 发送打印信息的格式类型,1 表示格式化信息，2 表示自定义格式
        /// </summary>
        public int Mode { get; set; }
        /// <summary>
        /// 打印的数据/信息内容
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 打印任务状态
        /// 0:队列中
        /// 1:已完成
        /// 2:请求失败
        /// 9:请求已发送
        /// </summary>
        public string TaskStatus { get; set; }
        /// <summary>
        /// 本次API请求发生的时刻
        /// </summary>
        public DateTime ReqTime { get; set; }
        /// <summary>
        /// 请求返回状态
        /// </summary>
        public string ReqStatus { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DevID { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TlpID { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 打印数量
        /// </summary>
        public int Num { get; set; }
        public override string TbName { get { return "ZL_Shop_PrintMessage"; } }
        public override string[,] FieldList()
        {
            string[,] fields = {
                                  {"ID","Int","4" },
                                  {"MsgNo","NVarChar","50" },
                                  {"Mode","Int","4" },
                                  {"Detail","NVarChar","4000" },
                                  {"TaskStatus","NVarChar","100" },
                                  {"ReqTime","DateTime","8" },
                                  {"ReqStatus","NVarChar","4000" },
                                  {"DevID","Int","4" },
                                  {"TlpID","Int","4" },
                                  {"OrderID","Int","4" },
                                  {"Num","Int","4"}
                               };
            return fields;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Shop_PrintMessage model = this;
            if (ReqTime <= DateTime.MinValue) { ReqTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.MsgNo;
            sp[2].Value = model.Mode;
            sp[3].Value = model.Detail;
            sp[4].Value = model.TaskStatus;
            sp[5].Value = model.ReqTime;
            sp[6].Value = model.ReqStatus;
            sp[7].Value = model.DevID;
            sp[8].Value = model.TlpID;
            sp[9].Value = model.OrderID;
            sp[10].Value = model.Num;
            return sp;
        }
        public M_Shop_PrintMessage GetModelFromReader(DbDataReader rdr)
        {
            M_Shop_PrintMessage model = new M_Shop_PrintMessage();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.MsgNo = ConverToStr(rdr["MsgNo"]);
            model.Mode = ConvertToInt(rdr["Mode"]);
            model.Detail = ConverToStr(rdr["Detail"]);
            model.TaskStatus = ConverToStr(rdr["TaskStatus"]);
            model.ReqTime = ConvertToDate(rdr["ReqTime"]);
            model.ReqStatus = ConverToStr(rdr["ReqStatus"]);
            model.DevID = ConvertToInt(rdr["DevID"]);
            model.TlpID = ConvertToInt(rdr["TlpID"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            model.Num = ConvertToInt(rdr["Num"]);
            return model;
        }
    }
}

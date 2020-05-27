using System;
using System.Data;
using System.Data.SqlClient;
namespace ZoomLa.Model
{
    public class M_RoomMessage:M_Base
    {
        #region 构造函数
        public M_RoomMessage()
        {
        }

        public M_RoomMessage
        (
            int ID,
            int SendID,
            int InceptID,
            string Mcontent,
            DateTime Addtime,
            int RestoreID,
            int State
        )
        {
            this.ID = ID;
            this.SendID = SendID;
            this.InceptID = InceptID;
            this.Mcontent = Mcontent;
            this.Addtime = Addtime;
            this.RestoreID = RestoreID;
            this.State = State;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] RoomMessageList()
        {
            string[] Tablelist = { "ID", "SendID", "InceptID", "Mcontent", "Addtime", "RestoreID", "State" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 留言者
        /// </summary>
        public int SendID { get; set; }
        /// <summary>
        /// 被留言者
        /// </summary>
        public int InceptID { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Mcontent { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Addtime { get; set; }
        /// <summary>
        /// 回复ID
        /// </summary>
        public int RestoreID { get; set; }
        /// <summary>
        /// 0为正常 1为删除
        /// </summary>
        public int State { get; set; }
        #endregion

        public override string TbName { get { return "ZL_RoomMessage"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"SendID","Int","4"},
                                  {"InceptID","Int","4"}, 
                                  {"Mcontent","Text","400"},
                                  {"Addtime","DateTime","8"},
                                  {"RestoreID","Int","4"}, 
                                  {"State","Int","4"}
                              };

            return Tablelist;

        }
        public override SqlParameter[] GetParameters()
        {
            M_RoomMessage model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.SendID;
            sp[2].Value = model.InceptID;
            sp[3].Value = model.Mcontent;
            sp[4].Value = model.Addtime;
            sp[5].Value = model.RestoreID;
            sp[6].Value = model.State;
            return sp;
        }
        public  M_RoomMessage GetModelFromReader(SqlDataReader rdr)
        {
            M_RoomMessage model = new M_RoomMessage();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SendID = ConvertToInt(rdr["SendID"]);
            model.InceptID = ConvertToInt(rdr["InceptID"]);
            model.Mcontent = ConverToStr(rdr["Mcontent"]);
            model.Addtime = ConvertToDate(rdr["Addtime"]);
            model.RestoreID = ConvertToInt(rdr["RestoreID"]);
            model.State = ConvertToInt(rdr["State"]);
            rdr.Close();
            return model;
        }
    }
}
using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model.User
{
    public class M_ViewHistory : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 内容ID
        /// </summary>
        public int InfoId { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        public string type { get; set; }
        public int UserID { get; set; }
        public DateTime addtime { get; set; }
        public M_ViewHistory()
        {
            type = string.Empty;
        }
        public override string TbName { get { return "ZL_ViewHistory"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Id","Int","4"},
                                  {"InfoId","Int","4"},
                                  {"type","NVarChar","1000"},
                                  {"UserID","Int","4"},
                                  {"addtime","DateTime","8"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_ViewHistory model = this;
            if (model.addtime <= DateTime.MinValue) { model.addtime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.InfoId;
            sp[2].Value = model.type;
            sp[3].Value = model.UserID;
            sp[4].Value = model.addtime;
            return sp;
        }
        public M_ViewHistory GetModelFromReader(SqlDataReader rdr)
        {
            M_ViewHistory model = new M_ViewHistory();
            model.ID = Convert.ToInt32(rdr["Id"]);
            model.InfoId = ConvertToInt(rdr["InfoId"]);
            model.type = ConverToStr(rdr["type"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.addtime = ConvertToDate(rdr["addtime"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
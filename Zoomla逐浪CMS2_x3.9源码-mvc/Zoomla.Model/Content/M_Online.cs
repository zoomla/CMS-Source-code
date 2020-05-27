namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;

    /// <summary>
    /// 在线记录
    /// </summary>
    public class M_Online:M_Base
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ChangeTime { get; set; }
        public bool IsNull { get; set; }

        public M_Online()
        {
            this.UserID = 0;
            this.UserName = "";
            this.StartTime = DateTime.Now;
            this.ChangeTime = DateTime.Now;
        }
        public M_Online(bool value)
        {
            this.IsNull = value;
        }
        public override string PK { get { return ""; } }
        public override string TbName { get { return "ZL_Online"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"UserID","Int","4"},
                                  {"UserName","NVarChar","1000"},
                                  {"StartTime","DateTime","8"},
                                  {"ChangeTime","DateTime","8"},
                                  {"IsNull","NVarChar","50"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Online model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.UserID;
            sp[1].Value = model.UserName;
            sp[2].Value = model.StartTime;
            sp[3].Value = model.ChangeTime;
            sp[4].Value = model.IsNull; 
            return sp;
        }
        public  M_Online GetModelFromReader(SqlDataReader rdr)
        {
            M_Online model = new M_Online();
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.UserName =rdr["UserName"].ToString();
            model.StartTime = Convert.ToDateTime(rdr["DownUrl"]);
            model.ChangeTime =  Convert.ToDateTime(rdr["ExtractionCode"]);
            model.IsNull = Convert.ToBoolean(rdr["IsNull"]); 
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
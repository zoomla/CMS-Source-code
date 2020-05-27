namespace ZoomLa.Model
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// 会员积分历史备注
    /// </summary>
    public class M_UserExpHis:M_Base
    {
        /// <summary>
        /// 余额,银币,积分(3)|信誉点(6),点券(4)|虚拟币(5)
        /// </summary>
        public enum SType { Purse = 1, SIcon = 2, Point = 3, UserPoint = 4, DummyPoint = 5, Credit = 6 };
        public int ExpHisID { get; set; }
        public int UserID { get; set; }
        public string detail { get; set; }
        //虚拟币类型
        public int ScoreType { get; set; }
        public double score { get; set; }
        //添加时间
        public DateTime HisTime { get; set; }
        //AdminID
        public int Operator { get; set; }
        public string OperatorIP { get; set; }
        public string Remark { get; set; }
        public int ComeType { get; set; }
        public M_UserExpHis()
        {
            this.HisTime = DateTime.Now;
        }
        public M_UserExpHis(M_UserInfo mu,int sco,string detailStr)
        {
            this.UserID = mu.UserID;
            this.Operator = mu.UserID;
            this.detail = detailStr;
            this.score = 0 - sco;
            this.HisTime = DateTime.Now;
        }
        private string _tbname = "ZL_UserExpHis";
        public override string TbName { get { return _tbname; }set { _tbname = value; } }
        public override string PK { get { return "ExpHisID"; } }
        public override string[,] FieldList() { return M_UserExpHis.GetFieldList(); }
        public static string[,] GetFieldList()
        {
            string[,] Tablelist = {
                                  {"ExpHisID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"Operator","Int","4"},
                                  {"detail","NVarChar","1000"},
                                  {"score","Money","16"},
                                  {"HisTime","DateTime","8"},
                                  {"OperatorIP","NVarChar","200"},
                                  {"ScoreType","Int","4"},
                                  {"Remark","NVarChar","200"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_UserExpHis model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ExpHisID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.Operator;
            sp[3].Value = model.detail;
            sp[4].Value = model.score;
            sp[5].Value = model.HisTime;
            sp[6].Value = model.OperatorIP;
            sp[7].Value = model.ScoreType;
            sp[8].Value = model.Remark;
            return sp;
        }
        public M_UserExpHis GetModelFromReader(SqlDataReader rdr)
        {
            M_UserExpHis model = new M_UserExpHis();
            model.ExpHisID = Convert.ToInt32(rdr["ExpHisID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.Operator =ConvertToInt(rdr["Operator"]);
            model.detail = ConverToStr(rdr["detail"]);
            model.score =  Convert.ToDouble(rdr["score"]);
            model.HisTime = ConvertToDate(rdr["HisTime"]);
            model.OperatorIP = ConverToStr(rdr["OperatorIP"]);
            model.ScoreType = Convert.ToInt32(rdr["ScoreType"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            rdr.Close();
            return model;
        }
    }
}
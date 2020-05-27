using System;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    public class M_Shopconfig : M_Base
    {
        #region 公开属性
        public bool IsOpen { get; set; }
        public string BankInfo { get; set; }
        public bool Anonymity { get; set; }
        public bool Pointcard { get; set; }
        public bool Dummymoney { get; set; }
        public bool Comment { get; set; }
        public int Goodpl { get; set; }
        public int Centerpl { get; set; }
        public int Badpl { get; set; }
        public int Auditing { get; set; }
        public int ScorePoint { get; set; }
        //是否允许修改订单价格
        public bool ChangeOrder { get; set; }
        #endregion
        public M_Shopconfig()
        {
            this.BankInfo = string.Empty;
        }
        public override string PK { get { return ""; } }
        public override string TbName { get { return "ZL_Shopconfig"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"IsOpen","Bit","4"},
                                  {"BankInfo","NText","400"},
                                  {"Anonymity","Bit","4"},
                                  {"Pointcard","Bit","4"}, 
                                  {"Dummymoney","Bit","4"},
                                  {"Comment","Bit","4"},
                                  {"Goodpl","Int","4"},
                                  {"Centerpl","Int","4"},
                                  {"Badpl","Int","4"}, 
                                  {"Auditing","Int","4"},
                                  {"ScorePoint","Int","4"},
                                  {"ChangeOrder","Bit","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Shopconfig model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.IsOpen;
            sp[1].Value = model.BankInfo;
            sp[2].Value = model.Anonymity;
            sp[3].Value = model.Pointcard;
            sp[4].Value = model.Dummymoney;
            sp[5].Value = model.Comment;
            sp[6].Value = model.Goodpl;
            sp[7].Value = model.Centerpl;
            sp[8].Value = model.Badpl;
            sp[9].Value = model.Auditing;
            sp[10].Value = model.ScorePoint;
            sp[11].Value = model.ChangeOrder;
            return sp;
        }

        public M_Shopconfig GetModelFromReader(SqlDataReader rdr)
        {
            M_Shopconfig model = new M_Shopconfig();
            model.IsOpen = ConverToBool(rdr["IsOpen"]);
            model.BankInfo = ConverToStr(rdr["BankInfo"]);
            model.Anonymity = ConverToBool(rdr["Anonymity"]);
            model.Pointcard = ConverToBool(rdr["Pointcard"]);
            model.Dummymoney = ConverToBool(rdr["Dummymoney"]);
            model.Comment = ConverToBool(rdr["Comment"]);
            model.Goodpl = ConvertToInt(rdr["Goodpl"]);
            model.Centerpl = ConvertToInt(rdr["Centerpl"]);
            model.Badpl = ConvertToInt(rdr["Badpl"]);
            model.Auditing = ConvertToInt(rdr["Auditing"]);
            model.ScorePoint = ConvertToInt(rdr["ScorePoint"]);
            model.ChangeOrder = ConverToBool(rdr["ChangeOrder"]);
            rdr.Close();
            return model;
        }
    }
}
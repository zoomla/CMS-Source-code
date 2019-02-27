using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
  public  class M_Answer_Recode:M_Base
  {
      public int Rid { get; set; } //答案ID
      public int Sid { get; set; } //问卷ID
      public int Userid { get; set; }//用户ID        
      public string Submitip { get; set; }//IP
      public int Status { get; set; }//状态：0未审核锁定，1审核锁定,2已录用锁定，-1已保存未提交，-2取消锁定
        public DateTime Submitdate { get; set; } //提交答案时间 
        public M_Answer_Recode()
        {
            this.Rid = 0;
            this.Sid = 0;
            this.Userid = 0;
            this.Submitdate = DateTime.Now;
            this.Submitip = "";
            this.Status = 0;
        }
        public override string PK { get { return "Rid"; } }
        public override string TbName { get { return "ZL_Answer_Recode"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Rid","Int","4"},
                                  {"Sid","Int","4"},
                                  {"Userid","Int","4"},
                                  {"Submitip","VarChar","50"},
                                  {"Submitdate","DateTime","4"},
                                  {"Status","Int","4"}

                                 };
            return Tablelist;
        }
        public  override SqlParameter[] GetParameters()
        {
            M_Answer_Recode model=this;
            SqlParameter[] sp = GetSP();
            //sp[0].Value = model.AnswerID;
            sp[0].Value = model.Rid;
            sp[1].Value = model.Sid;
            sp[2].Value = model.Userid;
            sp[3].Value = model.Submitip;
            sp[5].Value = model.Submitdate;
            sp[6].Value = model.Status;
            return sp;
        }
        public  M_Answer_Recode GetModelFromReader(SqlDataReader rdr)
        {
            M_Answer_Recode model = new M_Answer_Recode();
            model.Rid = ConvertToInt(rdr["Rid"]);
            model.Sid = ConvertToInt(rdr["Sid"]);
            model.Userid = ConvertToInt(rdr["Userid"]);
            model.Submitip = ConverToStr(rdr["Submitip"]);
            model.Submitdate = ConvertToDate(rdr["Submitdate"]);
            model.Status = ConvertToInt(rdr["Status"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
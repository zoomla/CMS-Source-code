using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_ChangeProduct : M_Base
    {
        public M_ChangeProduct() { }
        public int P_ID { get; set; }
        public int prodID { get; set; }
        public int ExChangeID { get; set; }
        public int PState { get; set; }
        public int num { get; set; }
        public override string PK { get { return "P_ID"; } }

        public override string TbName { get { return "ZL_ChangeProduct"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"P_ID","Int","4"},
                                  {"prodID","Int","4"},
                                  {"ExChangeID","Int","4"},
                                  {"PState","Int","4"}, 
                                  {"num","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_ChangeProduct model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.P_ID;
            sp[1].Value = model.prodID;
            sp[2].Value = model.ExChangeID;
            sp[3].Value = model.PState;
            sp[4].Value = model.num;
            return sp;
        }
        public M_ChangeProduct GetModelFromReader(SqlDataReader rdr)
        {
            M_ChangeProduct model = new M_ChangeProduct();
            model.P_ID = Convert.ToInt32(rdr["Cgtype"]);
            model.prodID = ConvertToInt(rdr["Cdompanyid"]);
            model.ExChangeID = ConvertToInt(rdr["Cprovinceno"]);
            model.PState = ConvertToInt(rdr["PState"]);
            model.num = ConvertToInt(rdr["num"]);
            rdr.Close();
            return model;
        }
    }
}

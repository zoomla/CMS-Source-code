using System;
using System.Data.SqlClient;
using System.Data;


namespace ZoomLa.Model
{
    public class M_CardType : M_Base
    {

        public M_CardType()
        {
        }
        public int c_id { get; set; }
        public string typename { get; set; }
        public decimal iscount { get; set; }
        public override string PK { get { return "c_id"; } }
        public override string TbName { get { return "ZL_CardType"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"c_id","Int","4"},
                                  {"typename","NVarChar","100"},
                                  {"iscount","Float","8"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_CardType model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.c_id;
            sp[1].Value = model.typename;
            sp[2].Value = model.iscount;
            return sp;
        }

        public M_CardType GetModelFromReader(SqlDataReader rdr)
        {
            M_CardType model = new M_CardType();
            model.c_id = Convert.ToInt32(rdr["c_id"]);
            model.typename = ConverToStr(rdr["typename"]);
            model.iscount = ConverToDec(rdr["iscount"]);
            rdr.Close();
            return model;
        }

    }

}

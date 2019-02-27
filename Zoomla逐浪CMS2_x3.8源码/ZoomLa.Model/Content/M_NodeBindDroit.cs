using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    public class M_NodeBindDroit : M_Base
    {
        public int Id { get; set; }
        public int NodeId { get; set; }
        public int FID { get; set; }
        public override string TbName { get { return "ZL_NodeBindDroit"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"NodeID","Int","4"},
                                  {"FID","Int","4"}
                                 };
            return Tablelist;
        }


        public override SqlParameter[] GetParameters()
        {
            M_NodeBindDroit model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Id;
            sp[1].Value = model.NodeId;
            sp[2].Value = model.FID;
            return sp;
        }

        public M_NodeBindDroit GetModelFromReader(SqlDataReader rdr)
        {
            M_NodeBindDroit model = new M_NodeBindDroit();
            model.Id = Convert.ToInt32(rdr["ID"]);
            model.NodeId = Convert.ToInt32(rdr["NodeID"]);
            model.FID = Convert.ToInt32(rdr["FID"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    public class M_Hits:M_Base
    {

        public int ID
        {
            get;
            set;
        }

        public int UID
        {
            get;
            set;
        }
        public int GID
        {
            get;
            set;
        }
        public DateTime UpdateTime
        {
            get;
            set;
        }
        public string IP
        {
            get;
            set;
        }
        public int Status
        {
            get;
            set;
        }
        public override string TbName { get { return "ZL_Hits"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"GID","Int","4"},
                                  {"Uid","Int","4"},
                                  {"UpdateTime","DateTime","8"}, 
                                  {"Ip","NVarChar","50"},
                                  {"status","Int","4"}
                                 };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_Hits model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.GID;
            sp[2].Value = model.UID;
            sp[3].Value = model.UpdateTime;
            sp[4].Value = model.IP;
            sp[5].Value = model.Status;
            return sp;
        }

        public  M_Hits GetModelFromReader(SqlDataReader rdr)
        {
            M_Hits model = new M_Hits();
            model.ID = Convert.ToInt32(rdr["id"]);
            model.GID = Convert.ToInt32(rdr["GID"]);
            model.UID = Convert.ToInt32(rdr["Uid"]);
            model.UpdateTime = Convert.ToDateTime(rdr["UpdateTime"]);
            model.IP = rdr["Ip"].ToString();
            model.Status = Convert.ToInt32(rdr["status"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
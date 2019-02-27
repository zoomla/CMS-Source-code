using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Message
{
    public class M_Guest_BarAuth:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Uid { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UName { get; set; }
        public int BarID { get; set; }
        /// <summary>
        /// 浏览权限
        /// </summary>
        public int Look { get; set; }
        /// <summary>
        /// 发贴权限
        /// </summary>
        public int Send { get; set; }
        /// <summary>
        /// 回复贴权
        /// </summary>
        public int Reply { get; set; }
        public override string TbName { get { return "ZL_Guest_BarAuth"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Uid","Int","4"},
        		        		{"UName","NVarChar","200"},
                                {"BarID","Int","4"},
                                {"Look","Int","4"},
                                {"Send","Int","4"},
                                {"Reply","Int","4"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Guest_BarAuth model = this;
            EmptyDeal();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Uid;
            sp[2].Value = model.UName;
            sp[3].Value = model.BarID;
            sp[4].Value = model.Look;
            sp[5].Value = model.Send;
            sp[6].Value = model.Reply;
            return sp;
        }
        public M_Guest_BarAuth GetModelFromReader(DbDataReader rdr)
        {
            M_Guest_BarAuth model = new M_Guest_BarAuth();
            model.ID = Convert.ToInt32(rdr["id"]);
            model.Uid = Convert.ToInt32(rdr["Uid"]);
            model.UName = rdr["UName"].ToString();
            model.BarID = Convert.ToInt32(rdr["BarID"]);
            model.Look = ConvertToInt(rdr["Look"]);
            model.Send = ConvertToInt(rdr["Send"]);
            model.Reply = ConvertToInt(rdr["Reply"]);
            rdr.Close();
            return model;
        }
        public void EmptyDeal()
        {
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Plat
{
    public class M_Plat_UserRole:M_Base
    {
        public int ID { get; set; }

        public int CompID { get; set; }

        public string RoleName { get; set; }

        public string RoleDesc { get; set; }
        private string _roleauth;
        public string RoleAuth
        {
            get
            {
                if (!string.IsNullOrEmpty(_roleauth))
                {
                    _roleauth = "," + _roleauth.Trim(',') + ",";
                }
                return _roleauth;
            }
            set { _roleauth = value; }
        }

        public int UserID { get; set; }

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 为1,则为当前公司的网络管理员
        /// </summary>
        public int IsSuper { get; set; }
        public override string TbName { get { return "ZL_Plat_UserRole"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
                                {"CompID","Int","4"},
        		        		{"RoleName","NVarChar","50"},
                                {"RoleDesc","VarChar","100"},
        		        		{"RoleAuth","NVarChar","4000"},
        		        		{"UserID","Int","4"},
                                {"CreateTime","DateTime","8"},
                                {"IsSuper","Int","4"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Plat_UserRole model = this;
            if (model.CreateTime <= DateTime.MinValue) { model.CreateTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.CompID;
            sp[2].Value = model.RoleName;
            sp[3].Value = model.RoleDesc;
            sp[4].Value = model.RoleAuth;
            sp[5].Value = model.UserID;
            sp[6].Value = model.CreateTime;
            sp[7].Value = model.IsSuper;
            return sp;
        }
        public M_Plat_UserRole GetModelFromReader(DbDataReader rdr)
        {
            M_Plat_UserRole model = new M_Plat_UserRole();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.CompID =  Convert.ToInt32(rdr["CompID"]);
            model.RoleName = rdr["RoleName"].ToString();
            model.RoleDesc = rdr["RoleDesc"].ToString();
            model.RoleAuth = rdr["RoleAuth"].ToString();
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.CreateTime = Convert.ToDateTime(rdr["CreateTime"]);
            model.IsSuper = ConvertToInt(rdr["IsSuper"]);
            rdr.Close();
            return model;
        }
    }
}

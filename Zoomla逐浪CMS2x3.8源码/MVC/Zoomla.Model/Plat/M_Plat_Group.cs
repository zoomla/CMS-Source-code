using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Plat
{
    public class M_Plat_Group:M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 父组ID
        /// </summary>
        public int Pid { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        string _manage="", _member = "";
        /// <summary>
        /// 管理人员IDS
        /// </summary>
        public string ManageIDS
        {
            get
            {
                return ("," + (_manage.Replace(",,", ",").Trim(',')) + ",");
            }
            set { _manage = value; }
        }
        /// <summary>
        /// 成员IDS
        /// </summary>
        public string MemberIDS
        {
            get
            {
                return ("," + (_member.Replace(",,", ",").Trim(',')) + ",");
            }
            set { _member = value; }
        }
        public int GroupType { get; set; }
        public int Status { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public int CompID { get; set; }
        /// <summary>
        /// 起始的部门
        /// </summary>
        public int FirstID { get; set; }
        /// <summary>
        /// 深度
        /// </summary>
        public int Depth { get; set; }
        public int OrderID { get; set; }
        public override string TbName { get { return "ZL_Plat_Group"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"ID","Int","4"},
        		        		{"Pid","Int","4"},
        		        		{"GroupName","NVarChar","200"},
        		        		{"GroupDesc","NVarChar","500"},
        		        		{"ManageIDS","NText","20000"},
        		        		{"MemberIDS","NText","20000"},
        		        		{"GroupType","Int","4"},
        		        		{"Status","Int","4"},
        		        		{"CreateUser","Int","4"},
        		        		{"CreateTime","DateTime","8"},
                                {"CompID","Int","4"},
                                {"FirstID","Int","4"},
                                {"Depth","Int","4"},
                                {"OrderID","Int","4"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Plat_Group model = this;
            EmptyDeal();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Pid;
            sp[2].Value = model.GroupName;
            sp[3].Value = model.GroupDesc;
            sp[4].Value = model.ManageIDS;
            sp[5].Value = model.MemberIDS;
            sp[6].Value = model.GroupType;
            sp[7].Value = model.Status;
            sp[8].Value = model.CreateUser;
            sp[9].Value = model.CreateTime;
            sp[10].Value = model.CompID;
            sp[11].Value = model.FirstID;
            sp[12].Value = model.Depth;
            sp[13].Value = model.OrderID;
            return sp;
        }
        public M_Plat_Group GetModelFromReader(DbDataReader rdr)
        {
            M_Plat_Group model = new M_Plat_Group();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Pid = Convert.ToInt32(rdr["Pid"]);
            model.GroupName = ConverToStr(rdr["GroupName"]);
            model.GroupDesc = ConverToStr(rdr["GroupDesc"]);
            model.ManageIDS = ConverToStr(rdr["ManageIDS"]);
            model.MemberIDS = ConverToStr(rdr["MemberIDS"]);
            model.GroupType = ConvertToInt(rdr["GroupType"]);
            model.Status = ConvertToInt(rdr["Status"]);
            model.CreateUser = ConvertToInt(rdr["CreateUser"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.CompID = ConvertToInt(rdr["CompID"]);
            model.FirstID = ConvertToInt(rdr["FirstID"]);
            model.Depth = ConvertToInt(rdr["Depth"]);
            model.OrderID = ConvertToInt(rdr["OrderID"]);
            rdr.Close();
            return model;
        }
        public void EmptyDeal() 
        {
            if (CreateTime.Year <= 1901) CreateTime = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZoomLa.Model
{
    public class M_User_Plat:M_Base
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        /// <summary>
        /// 无用,保留
        /// </summary>
        public string Plat_Group { get; set; }
        private string _platRole;
        /// <summary>
        /// 能力中心,角色
        /// </summary>
        public string Plat_Role { get { if (!string.IsNullOrEmpty(_platRole))_platRole = "," + _platRole.Trim(',') + ","; return _platRole; } set { _platRole = value; } }
        /// <summary>
        /// -1:禁用,0:未审核,1:已审核
        /// </summary>
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public int CompID { get; set; }
        public string CompName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Post { get; set; }
        private string _uface = "";
        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserFace
        {
            get
            {
                if (string.IsNullOrEmpty(_uface)) { return "/Images/userface/noface.png"; }
                else { return _uface; }
            }
            set { _uface = value; }
        }
        /// <summary>
        /// 所属会员组ID
        /// </summary>
        public string Gid { get; set; }
        /// <summary>
        /// 所属会员组
        /// </summary>
        public string GroupName { get; set; }
        public int AtCount { get; set; }
        public override string TbName { get { return "ZL_User_Plat"; } }
        private string _pk="UserID";
        public M_User_Plat() { Status = 0; }
        public override string PK
        {
            get
            {
               return _pk;
            }
            set { _pk = value; }
        }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        		        		{"UserID","Int","4"},
        		        		{"Plat_Group","NVarChar","500"},
        		        		{"Plat_Role","NVarChar","200"},
        		        		{"Status","Int","4"},
        		        		{"CreateTime","DateTime","8"},
                                {"CompID","Int","4"},
                                {"Mobile","VarChar","20"},
                                {"TrueName","NVarChar","50"},
                                {"Post","NVarChar","50"},
                                {"UserFace","NVarChar","150"},
                                {"ATCount","Int","4"}
             };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_User_Plat model = this;
            if (model.CreateTime <= DateTime.MinValue) { model.CreateTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.UserID;
            sp[1].Value = model.Plat_Group ?? "";
            sp[2].Value = model.Plat_Role ?? "";
            sp[3].Value = model.Status;
            sp[4].Value = model.CreateTime;
            sp[5].Value = model.CompID;
            sp[6].Value = model.Mobile;
            sp[7].Value = model.TrueName;
            sp[8].Value = model.Post;
            sp[9].Value = model.UserFace;
            sp[10].Value = model.AtCount;
            return sp;
        }
        public M_User_Plat GetModelFromReader(SqlDataReader rdr)
        {
            M_User_Plat model = new M_User_Plat();
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.Plat_Group = ConverToStr(rdr["Plat_Group"]);
            model.Plat_Role = ConverToStr(rdr["Plat_Role"]);
            model.Status = ConvertToInt((rdr["Status"]));
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.UserName = ConverToStr(rdr["UserName"]);
            model.CompID = Convert.ToInt32(rdr["CompID"]);
            model.CompName = ConverToStr(rdr["CompName"]);
            model.Post = ConverToStr(rdr["Position"]);
            model.Mobile = ConverToStr(rdr["Mobile"]);
            model.TrueName = ConverToStr(rdr["TrueName"]);
            model.Post = ConverToStr(rdr["Post"]);
            model.UserFace = ConverToStr(rdr["UserFace"]);
            model.Gid = ConverToStr(rdr["Gid"]); 
            model.GroupName = ConverToStr(rdr["GroupName"]);
            model.AtCount = ConvertToInt(rdr["ATCount"]);
            rdr.Close();
            return model;
        }
    }
}

using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Permission:M_Base
    {
        #region 定义字段
        public int ID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色图片
        /// </summary>
        public string RoleImg { get; set; }
        /// <summary>
        /// 角色说明
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 权限详细列表
        /// </summary>
        public string Perlist { get; set; }
        /// <summary>
        /// 用户组绑定
        /// </summary>
        public string UserGroup { get; set; }
        /// <summary>
        /// 优先级别,用户组多次绑定
        /// </summary>
        public int Precedence { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsTrue { get; set; }
        public string Auth_OA { get; set; }
        #endregion
        public override string TbName { get { return "ZL_Permission"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"RoleName","NVarChar","255"},
                                  {"RoleImg","NVarChar","255"},
                                  {"Info","NVarChar","255"}, 
                                  {"Perlist","NText","4000"},
                                  {"UserGroup","NText","4000"},
                                  {"Precedence","Int","4"}, 
                                  {"IsTrue","Int","4"},
                                  {"Auth_OA","VarChar","8000"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Permission model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.RoleName;
            sp[2].Value = model.RoleImg;
            sp[3].Value = model.Info;
            sp[4].Value = model.Perlist;
            sp[5].Value = model.UserGroup;
            sp[6].Value = model.Precedence;
            sp[7].Value = model.IsTrue;
            sp[8].Value = model.Auth_OA;
            return sp;
        }

        public M_Permission GetModelFromReader(SqlDataReader rdr)
        {
            M_Permission model = new M_Permission();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.RoleName = rdr["RoleName"].ToString();
            model.RoleImg = rdr["RoleImg"].ToString();
            model.Info = rdr["Info"].ToString();
            model.Perlist = rdr["Perlist"].ToString();
            model.UserGroup = rdr["UserGroup"].ToString();
            model.Precedence = Convert.ToInt32(rdr["Precedence"]);
            model.IsTrue = Convert.ToBoolean(rdr["IsTrue"]);
            model.Auth_OA = rdr["Auth_OA"].ToString();
            rdr.Close();
            return model;
        }
    }
}
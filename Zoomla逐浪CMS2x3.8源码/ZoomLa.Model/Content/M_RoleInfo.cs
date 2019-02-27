using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    /// <summary>
    /// M_RoleInfo 的摘要说明
    /// 角色信息
    /// </summary>
    public class M_RoleInfo:M_Base
    {
        #region 字段定义
        /// <summary>
        /// 角色ＩＤ
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 操作代码
        /// （具有权限）
        /// </summary>
        public string OperateCode { get; set; }
        /// <summary>
        /// 添加角色用户ID
        /// </summary>
        public int NodeID { get; set; }
        /// <summary>
        /// 是否为系统角色，1：是，0：否
        /// </summary>
        public int IsSystem { get; set; }
        /// <summary>
        ///对象是否空对象
        /// </summary>
        public bool IsNull { get; private set; }
        #endregion
        #region 构造函数
        public M_RoleInfo()
        {
            this.RoleName = string.Empty;
            this.Description = string.Empty;
            this.OperateCode = string.Empty;
        }
        #endregion

        public override string PK { get { return "RoleID"; } }
        public override string TbName { get { return "ZL_Role"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"RoleID","Int","4"},
                                  {"RoleName","NVarChar","50"},
                                  {"Description","NVarChar","255"},
                                  {"NodeID","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_RoleInfo model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.RoleID;
            sp[1].Value = model.RoleName;
            sp[2].Value = model.Description;
            sp[3].Value = model.NodeID;
            return sp;
        }

        public  M_RoleInfo GetModelFromReader(SqlDataReader rdr)
        {
            M_RoleInfo model = new M_RoleInfo();
            model.RoleID = Convert.ToInt32(rdr["RoleID"]);
            model.RoleName = rdr["RoleName"].ToString();
            model.Description = rdr["Description"].ToString();
            model.NodeID = Convert.ToInt32(rdr["NodeID"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
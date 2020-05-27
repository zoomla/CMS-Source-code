using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_UserPurview:M_Base
    {
        #region 定义字段
        /// <summary>
        /// ID
        /// </summary>	
        public int ID{ get;set; }
        /// <summary>
        /// 用户角色ID
        /// </summary>	
        public int RoleID { get; set; }
        /// <summary>
        /// 角色权限
        /// </summary>	
        public string PurviewCode { get; set; }
        private string _nodeID;
        /// <summary>
        /// 预留节点ID
        /// </summary>	
        public string NodeID
        {
            get
            {
                _nodeID = string.IsNullOrEmpty(_nodeID) ? "" : "," + (_nodeID.Trim(',')) + ",";
                return _nodeID;
            }
            set
            {
                _nodeID = value;
            }
        }
        #endregion
        #region 构造函数
        public M_UserPurview() { }
        #endregion

        public override string TbName { get { return "ZL_UserPurview"; } }

        
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        	            {"ID","Int","4"},            
                        {"RoleID","Int","4"},            
                        {"PurviewCode","NVarChar","255"},            
                        {"NodeID","NVarChar","500"}
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_UserPurview model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.RoleID;
            sp[2].Value = model.PurviewCode;
            sp[3].Value = model.NodeID;
            return sp;
        }
        public M_UserPurview GetModelFromReader(SqlDataReader rdr)
        {
            M_UserPurview model = new M_UserPurview();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.RoleID = ConvertToInt(rdr["RoleID"]);
            model.PurviewCode = ConverToStr(rdr["PurviewCode"]);
            model.NodeID = ConverToStr(rdr["NodeID"]);
            rdr.Close();
            return model;
        }
    }
}
using System;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    /// <summary>
    ///ZL_UserFriendGroup业务实体
    /// </summary>
    [Serializable]
    public class M_UserFriendGroup:M_Base
    {
        #region 字段定义

        ///<summary>
        ///编号
        ///</summary>
        public int iD { get; set; }

        ///<summary>
        ///所属用户
        ///</summary>
        public int userID { get; set; }

        ///<summary>
        ///分组名
        ///</summary>
        public string groupName { get; set; }

        ///<summary>
        ///是否是黑名单(1为黑名单)
        ///</summary>
        public int blackGroup { get; set; }

        ///<summary>
        ///排序
        ///</summary>
        public int orderID { get; set; }
        #endregion
        public override string TbName { get { return "ZL_User_FriendGroup"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"groupName","NVarChar","1000"}, 
                                  {"blackGroup","Int","4"},
                                  {"orderID","Int","4"} 
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_UserFriendGroup model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.iD;
            sp[1].Value = model.userID;
            sp[2].Value = model.groupName;
            sp[3].Value = model.blackGroup;
            sp[4].Value = model.orderID; 
            return sp;
        }
        public M_UserFriendGroup GetModelFromReader(SqlDataReader rdr)
        {
            M_UserFriendGroup model = new M_UserFriendGroup();
            model.iD = Convert.ToInt32(rdr["ID"]);
            model.userID = Convert.ToInt32(rdr["userID"]);
            model.groupName = ConverToStr(rdr["groupName"]);
            model.blackGroup =  ConvertToInt(rdr["blackGroup"]);
            model.orderID =  ConvertToInt(rdr["orderID"]); 
            rdr.Close();
            return model;
        }
    }
}
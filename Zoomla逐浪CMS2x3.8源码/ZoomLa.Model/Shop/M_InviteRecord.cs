using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_InviteRecord : M_Base
    {
        #region 构造函数
        public M_InviteRecord()
        {
        }

        public M_InviteRecord
        (
            int id,
            int userid,
            int RecommUserId,
            DateTime RegData,
            int isValid,
            int isReset
        )
        {
            this.id = id;
            this.userid = userid;
            this.RecommUserId = RecommUserId;
            this.RegData = RegData;
            this.isValid = isValid;
            this.isReset = isReset;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] InviteRecordList()
        {
            string[] Tablelist = { "id", "userid", "RecommUserId", "RegData", "isValid", "isReset" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 是否已重置:0为否,1为是
        /// </summary>
        public int isReset { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 推荐注册用户
        /// </summary>
        public int RecommUserId { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime RegData { get; set; }
        /// <summary>
        /// 是否是有效用户:0为否,1为是
        /// </summary>
        public int isValid { get; set; }
        #endregion

        public override string TbName { get { return "ZL_InviteRecord"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"userid","Int","4"},
                                  {"RecommUserId","Int","4"},
                                  {"RegData","DateTime","8"}, 
                                  {"isValid","Int","4"}, 
                                  {"isReset","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_InviteRecord model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.userid;
            sp[2].Value = model.RecommUserId;
            sp[3].Value = model.RegData;
            sp[4].Value = model.isValid;
            sp[5].Value = model.isReset;
            return sp;
        }

        public M_InviteRecord GetModelFromReader(SqlDataReader rdr)
        {
            M_InviteRecord model = new M_InviteRecord();
            model.id = Convert.ToInt32(rdr["id"]);
            model.userid = Convert.ToInt32(rdr["userid"]);
            model.RecommUserId = ConvertToInt(rdr["RecommUserId"]);
            model.RegData = ConvertToDate(rdr["RegData"]);
            model.isValid = ConvertToInt(rdr["isValid"]);
            model.isReset = ConvertToInt(rdr["isReset"]);
            rdr.Close();
            return model;
        }
    }
}



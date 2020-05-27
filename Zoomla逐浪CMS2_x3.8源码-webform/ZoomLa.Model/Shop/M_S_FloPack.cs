using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_S_FloPack : M_Base
    {
        #region 构造函数
        public M_S_FloPack()
        {
            this.PackName = string.Empty;
            this.Remark = string.Empty;
        }
        public M_S_FloPack
        (
            int ID,
            int UserID,
            string PackName,
            double PackPrice,
            string Remark,
            int SID
        )
        {
            this.ID = ID;
            this.UserID = UserID;
            this.PackName = PackName;
            this.PackPrice = PackPrice;
            this.Remark = Remark;
            this.SID = SID;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] S_FloPackList()
        {
            string[] Tablelist = { "ID", "UserID", "PackName", "PackPrice", "Remark", "SID" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        public int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 包装名
        /// </summary>
        public string PackName { get; set; }
        /// <summary>
        /// 包装费用(含包装、设计及服务费)
        /// </summary>
        public double PackPrice { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int SID { get; set; }
        #endregion
        public override string TbName { get { return "ZL_S_FloPack"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"PackName","NVarChar","50"},
                                  {"PackPrice","Money","8"}, 
                                  {"Remark","NVarChar","255"},
                                  {"SID","Int","4"}
                                };

            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_S_FloPack model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.UserID;
            sp[2].Value = model.PackName;
            sp[3].Value = model.PackPrice;
            sp[4].Value = model.Remark;
            sp[5].Value = model.SID;
            return sp;
        }

        public M_S_FloPack GetModelFromReader(SqlDataReader rdr)
        {
            M_S_FloPack model = new M_S_FloPack();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.PackName = ConverToStr(rdr["PackName"]);
            model.PackPrice = ConverToDouble(rdr["PackPrice"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            model.SID = ConvertToInt(rdr["SID"]);
            rdr.Close();
            return model;
        }
    }
}
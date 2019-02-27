using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_InvtoType : M_Base
    {
        #region 构造函数
        public M_InvtoType()
        {
        }

        public M_InvtoType
        (
            int ID,
            string InvtoType,
            float Invto,
            DateTime Addtime,
            int AdminID,
            string Remark
        )
        {
            this.ID = ID;
            this.InvtoType = InvtoType;
            this.Invto = Invto;
            this.Addtime = Addtime;
            this.AdminID = AdminID;
            this.Remark = Remark;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] InvtoTypeList()
        {
            string[] Tablelist = { "ID", "InvtoType", "Invto", "Addtime", "AdminID", "Remark" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 发票类型名称
        /// </summary>
        public string InvtoType { get; set; }
        /// <summary>
        /// 发票税率
        /// </summary>
        public float Invto { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        #endregion

        public override string TbName { get { return "ZL_InvtoType"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"InvtoType","NVarChar","50"},
                                  {"Invto","Float","4"},
                                  {"Addtime","DateTime","8"}, 
                                  {"AdminID","Int","4"}, 
                                  {"Remark","NVarChar","255"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_InvtoType model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.InvtoType;
            sp[2].Value = model.Invto;
            sp[3].Value = model.Addtime;
            sp[4].Value = model.AdminID;
            sp[5].Value = model.Remark;
            return sp;
        }

        public M_InvtoType GetModelFromReader(SqlDataReader rdr)
        {
            M_InvtoType model = new M_InvtoType();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.InvtoType = ConverToStr(rdr["InvtoType"]);
            model.Invto = ConvertToInt(rdr["Invto"]);
            model.Addtime = ConvertToDate(rdr["Addtime"]);
            model.AdminID = ConvertToInt(rdr["AdminID"]);
            model.Remark = ConverToStr(rdr["Remark"]);
            rdr.Close();
            return model;
        }
    }
}



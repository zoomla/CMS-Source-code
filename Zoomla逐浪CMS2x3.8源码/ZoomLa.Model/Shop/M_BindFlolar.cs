using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_BindFlolar:M_Base
    {
        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int Shopid { get; set; }
        /// <summary>
        /// 商品绑定花材id编号
        /// </summary>
        public int BindSid { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
        /// <summary>
        /// 用户ID,添加商品用户
        /// </summary>
        public int Userid { get; set; }
        /// <summary>
        /// 花材数量
        /// </summary>
        public int SNum { get; set; }
        #endregion
       
        #region 构造函数
        public M_BindFlolar(){}
        public M_BindFlolar
        (
            int ID,
            int Shopid,
            int BindSid,
            DateTime Addtime,
            int Userid,
            int SNum
        )
        {
            this.ID = ID;
            this.Shopid = Shopid;
            this.BindSid = BindSid;
            this.Addtime = Addtime;
            this.Userid = Userid;
            this.SNum = SNum;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] BindFlolarList()
        {
            string[] Tablelist = { "ID", "Shopid", "BindSid", "Addtime", "Userid", "SNum" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_BindFlolar"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Shopid","Int","4"},
                                  {"BindSid","Int","4"},
                                  {"Addtime","DateTime","8"}, 
                                  {"Userid","Int","4"},
                                  {"SNum","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_BindFlolar model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Shopid;
            sp[2].Value = model.BindSid;
            sp[3].Value = model.Addtime;
            sp[4].Value = model.Userid;
            sp[5].Value = model.SNum;
            return sp;
        }

        public  M_BindFlolar GetModelFromReader(SqlDataReader rdr)
        {
            M_BindFlolar model = new M_BindFlolar();
            model.ID = Convert.ToInt32(rdr["Cgtype"]);
            model.Shopid = ConvertToInt(rdr["Cdompanyid"]);
            model.BindSid = ConvertToInt(rdr["Cprovinceno"]);
            model.Addtime = ConvertToDate(rdr["Ccitycode"]);
            model.Userid = ConvertToInt(rdr["Cprovinceno"]);
            model.SNum = ConvertToInt(rdr["Cprovinceno"]);
            rdr.Close();
            return model;
        }  
    }
}



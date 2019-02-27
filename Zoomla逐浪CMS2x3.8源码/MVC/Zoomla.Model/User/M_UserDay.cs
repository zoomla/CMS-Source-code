namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;
    [Serializable]
    public class M_UserDay:M_Base
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 节日名称
        /// </summary>
        public string D_name  { get; set; }
        /// <summary>
        /// 节日日期
        /// </summary>
        public DateTime D_date { get; set; }
        /// <summary>
        /// 提醒内容
        /// </summary>
        public string D_Content  { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int D_UserID { get; set; }
        /// <summary>
        /// 邮件发送状态
        /// </summary>
        public int D_mail { get; set; }
        /// <summary>
        /// 手机发送状态
        /// </summary>
        public int D_mobile { get; set; }
        /// <summary>
        /// 发送次数
        /// </summary>
        public int D_SendNum { get; set; }
        #endregion
        public override string TbName { get { return "ZL_UserDay"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"D_name","NVarChar","100"},
                                  {"D_date","DateTime","8"}, 
                                  {"D_Content","NVarChar","200"},
                                  {"D_UserID","Int","4"},
                                  {"D_mail","Int","4"},
                                  {"D_mobile","Int","4"},
                                  {"D_SendNum","Int","4"},
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_UserDay model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.D_name;
            sp[2].Value = model.D_date;
            sp[3].Value = model.D_Content;
            sp[4].Value = model.D_UserID;
            sp[5].Value = model.D_mail;
            sp[6].Value = model.D_mobile;
            sp[7].Value = model.D_SendNum;
            return sp;
        }
        public M_UserDay GetModelFromReader(SqlDataReader rdr)
        {
            M_UserDay model = new M_UserDay();

            model.id =Convert.ToInt32(rdr["id"]);
            model.D_name = ConverToStr(rdr["D_name"]);
            model.D_date = ConvertToDate(rdr["D_date"]);
            model.D_Content = ConverToStr(rdr["D_Content"]);
            model.D_UserID = Convert.ToInt32(rdr["D_UserID"]);
            model.D_mail = ConvertToInt(rdr["D_mail"]);
            model.D_mobile = ConvertToInt(rdr["D_mobile"]);
            model.D_SendNum = ConvertToInt(rdr["D_SendNum"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
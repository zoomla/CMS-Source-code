namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;
    /// <summary>
    /// M_MailTemp 的摘要说明
    /// </summary>
    public class M_MailTemp : M_Base
    {
        public int ID { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TempName { get; set; }
        /// <summary>
        ///  邮箱模板类型
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string AddUser { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public override string TbName { get { return "ZL_MailTemp"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"TempName","NVarChar","50"},
                                  {"Pic","NVarChar","255"},
                                  {"Type","Int","4"},
                                  {"AddUser","NVarChar","50"},
                                  {"CreateTime","DateTime","8"},
                                  {"Content","NText","10000"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_MailTemp model=this;
            if (model.CreateTime <= DateTime.MinValue) { model.CreateTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.TempName;
            sp[2].Value = model.Pic;
            sp[3].Value = model.Type;
            sp[4].Value = model.AddUser;
            sp[5].Value = model.CreateTime;
            sp[6].Value = model.Content;
            return sp;
        }
        public M_MailTemp GetModelFromReader(SqlDataReader rdr)
        {
            M_MailTemp model = new M_MailTemp();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.TempName = rdr["TempName"].ToString();
            model.Pic = rdr["Pic"].ToString();
            model.Type = ConvertToInt(rdr["Type"]);
            model.AddUser = rdr["AddUser"].ToString();
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.Content = rdr["Content"].ToString();
            rdr.Close();
            return model;
        }
    }
}
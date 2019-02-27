using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_MailIdiograph:M_Base
    {
        #region 定义字段
        public int ID { get; set; }
        /// <summary>
        /// 签名名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 签名内容
        /// </summary>
        public string Context { get; set; }
        #endregion

        #region 构造函数
        public M_MailIdiograph()
        {
            this.Name = string.Empty;
            this.Context = string.Empty;
        }
        public M_MailIdiograph
        (
            int ID,
            string Name,
            DateTime AddTime,
            bool State,
            string Context
        )
        {
            this.ID = ID;
            this.Name = Name;
            this.AddTime = AddTime;
            this.State = State;
            this.Context = Context;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] MailIdiographList()
        {
            string[] Tablelist = { "ID", "Name", "AddTime", "State", "Context" };
            return Tablelist;
        }
        #endregion

        public override string TbName { get { return "ZL_MailIdiograph"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Name","NVarChar","50"},
                                  {"AddTime","DateTime","8"},
                                  {"State","Bit","2"}, 
                                  {"Context","NVarChar","100"}
                                 };
            return Tablelist;
        }
       

        public override SqlParameter[] GetParameters()
        {
            M_MailIdiograph model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Name;
            sp[2].Value = model.AddTime;
            sp[3].Value = model.State;
            sp[4].Value = model.Context;
            return sp;
        }
        public M_MailIdiograph GetModelFromReader(SqlDataReader rdr)
        {
            M_MailIdiograph model = new M_MailIdiograph();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Name = rdr["Name"].ToString();
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.State = ConverToBool(rdr["State"]);
            model.Context = ConverToStr(rdr["Context"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
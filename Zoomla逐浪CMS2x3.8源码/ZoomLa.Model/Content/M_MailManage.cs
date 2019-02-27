using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_MailManage : M_Base
    {
        #region 构造函数
        public M_MailManage()
        {
        }

        public M_MailManage
        (
            int ID,
            string Email,
            string Postfix,
            DateTime AddTime,
            DateTime BackMostTime,
            string SubscribeToType,
            int UserID,
            bool State
        )
        {
            this.ID = ID;
            this.Email = Email;
            this.Postfix = Postfix;
            this.AddTime = AddTime;
            this.BackMostTime = BackMostTime;
            this.SubscribeToType = SubscribeToType;
            this.UserID = UserID;
            this.State = State;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] MailManageList()
        {
            string[] Tablelist = { "ID", "Email", "Postfix", "AddTime", "BackMostTime", "SubscribeToType", "UserID", "State" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 邮件后缀
        /// </summary>
        public string Postfix { get; set; }
        /// <summary>
        /// 邮箱地址添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 最后邮件发送时间
        /// </summary>
        public DateTime BackMostTime { get; set; }
        /// <summary>
        /// 订阅类型
        /// </summary>
        public string SubscribeToType { get; set; }
        /// <summary>
        /// 订阅用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 邮件状态 0：未认证，1已认证
        /// </summary>
        public bool State { get; set; }
        #endregion
        public override string TbName { get { return "ZL_MailManage"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Email","NVarChar","100"},
                                  {"Postfix","NVarChar","50"},
                                  {"AddTime","DateTime","8"},
                                  {"BackMostTime","DateTime","8"},
                                  {"SubscribeToType","NVarChar","50"},
                                  {"UserID","Int","4"},
                                  {"State","Bit","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_MailManage model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.Email;
            sp[2].Value = model.Postfix;
            sp[3].Value = model.AddTime;
            sp[4].Value = model.BackMostTime;
            sp[5].Value = model.SubscribeToType;
            sp[6].Value = model.UserID;
            sp[7].Value = model.State;
            return sp;
        }

        public M_MailManage GetModelFromReader(SqlDataReader rdr)
        {
            M_MailManage model = new M_MailManage();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Email = rdr["Email"].ToString();
            model.Postfix = rdr["Postfix"].ToString();
            model.AddTime = Convert.ToDateTime(rdr["AddTime"]);
            model.BackMostTime = Convert.ToDateTime(rdr["BackMostTime"]);
            model.SubscribeToType = rdr["SubscribeToType"].ToString();
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.State = Convert.ToBoolean(rdr["State"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
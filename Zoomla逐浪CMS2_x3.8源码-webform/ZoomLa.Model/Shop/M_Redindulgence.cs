using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Redindulgence : M_Base
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double Account { get; set; }
        /// <summary>
        /// 所属用户
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 发送邮箱
        /// </summary>
        public string mail { get; set; }
        /// <summary>
        /// 邀请人
        /// </summary>
        public string InvitePeople { get; set; }
        /// <summary>
        /// 朋友姓名
        /// </summary>
        public string FriendName { get; set; }
        /// <summary>
        ///注册链接地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 是否已使用:0为未使用,1为已使用
        /// </summary>
        public int isUse { get; set; }
        /// <summary>
        /// 类型:0为返利邀请,1为微博邀请，2为sns邀请
        /// </summary>
        public int Type { get; set; }
        #endregion

        #region 构造函数
        public M_Redindulgence()
        {
        }

        public M_Redindulgence
        (
            int id,
            double Account,
            int UserId,
            string mail,
            string InvitePeople,
            string FriendName,
            int isUse,
            string url,
            int type
        )
        {
            this.id = id;
            this.Account = Account;
            this.UserId = UserId;
            this.mail = mail;
            this.InvitePeople = InvitePeople;
            this.FriendName = FriendName;
            this.isUse = isUse;
            this.Url = url;
            this.Type = type;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] RedindulgenceList()
        {
            string[] Tablelist = { "id", "Account", "UserId", "mail", "InvitePeople", "FriendName", "isUse", "Type" };
            return Tablelist;
        }
        #endregion
        public override string TbName { get { return "ZL_Redindulgence"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"Account","Money","8"},
                                  {"UserId","Int","4"},
                                  {"mail","NVarChar","50"}, 
                                  {"InvitePeople","NVarChar","50"},
                                  {"FriendName","NVarChar","50"},
                                  {"Url","NText","400"},
                                  {"isUse","Int","4"},
                                  {"Type","Int","4"}
                                };

            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Redindulgence model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.Account;
            sp[2].Value = model.UserId;
            sp[3].Value = model.mail;
            sp[4].Value = model.InvitePeople;
            sp[5].Value = model.FriendName;
            sp[6].Value = model.Url;
            sp[7].Value = model.isUse;
            sp[8].Value = model.Type;
            return sp;
        }

        public M_Redindulgence GetModelFromReader(SqlDataReader rdr)
        {
            M_Redindulgence model = new M_Redindulgence();
            model.id = Convert.ToInt32(rdr["id"]);
            model.Account = Convert.ToDouble(rdr["Account"]);
            model.UserId = Convert.ToInt32(rdr["UserId"]);
            model.mail = ConverToStr(rdr["mail"]);
            model.InvitePeople = ConverToStr(rdr["InvitePeople"]);
            model.FriendName = ConverToStr(rdr["FriendName"]);
            model.Url = ConverToStr(rdr["Url"]);
            model.isUse = ConvertToInt(rdr["isUse"]);
            model.Type = ConvertToInt(rdr["Type"]);
            rdr.Close();
            return model;
        }

    }
}



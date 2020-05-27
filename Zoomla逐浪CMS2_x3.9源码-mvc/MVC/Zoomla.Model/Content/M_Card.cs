using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Card : M_Base
    {
        #region 定义字段
        public int Card_ID { get; set; }
        /// <summary>
        /// 卡片编号
        /// </summary>
        public string CardNum { get; set; }
        /// <summary>
        /// 卡片密码
        /// </summary>
        public string CardPwd { get; set; }
        /// <summary>
        /// 发放卡片加盟商
        /// </summary>
        public int PutUserID { get; set; }
        /// <summary>
        /// 关联用户
        /// </summary>
        public int AssociateUserID { get; set; }
        /// <summary>
        /// VIP类型
        /// </summary>
        public int VIPType { get; set; }
        /// <summary>
        /// 第一次使用时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 使用年限,到期时间
        /// </summary>
        public DateTime CircumscribeTime { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public int PlyNum { get; set; }
        /// <summary>
        /// 卡片创建时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 卡片状态 1：未开启，2：开启，3：停用
        /// </summary>
        public int CardState { get; set; }
        /// <summary>
        /// 激活状态， 0：未使用，1：已使用
        /// </summary>
        public int ActivateState { get; set; }
        /// <summary>
        /// 激活用户ID
        /// </summary>
        public int ActivateUserID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remind { get; set; }
        #endregion

        #region 构造函数
        public M_Card()
        {
            this.CardNum = string.Empty;
            this.CardPwd = string.Empty;
        }
        public M_Card
        (
            int Card_ID,
            string CardNum,
            string CardPwd,
            int PutUserID,
            int AssociateUserID,
            int VIPType,
            string StartTime,
            DateTime CircumscribeTime,
            int PlyNum,
            DateTime AddTime,
            int CardState
        )
        {
            this.Card_ID = Card_ID;
            this.CardNum = CardNum;
            this.CardPwd = CardPwd;
            this.PutUserID = PutUserID;
            this.AssociateUserID = AssociateUserID;
            this.VIPType = VIPType;
            this.StartTime = StartTime;
            this.CircumscribeTime = CircumscribeTime;
            this.PlyNum = PlyNum;
            this.AddTime = AddTime;
            this.CardState = CardState;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] CardList()
        {
            string[] Tablelist = { "Card_ID", "CardNum", "CardPwd", "PutUserID", "AssociateUserID", "VIPType", "StartTime", "CircumscribeTime", "PlyNum", "AddTime", "CardState", "ActivateState", "ActivateUserID", "Remind" };
            return Tablelist;
        }
        #endregion
        public override string PK { get { return "Card_ID"; } }
        public override string TbName { get { return "ZL_Card"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"Card_ID","Int","4"},
                                  {"CardNum","NVarChar","50"},
                                  {"CardPwd","NVarChar","50"},
                                  {"PutUserID","Int","4"},
                                  {"AssociateUserID","Int","4"},
                                  {"VIPType","Int","4"},
                                  {"StartTime","NVarChar","50"},
                                  {"CircumscribeTime","DateTime","8"},
                                  {"PlyNum","Int","4"},
                                  {"AddTime","DateTime","8"},
                                  {"CardState","Int","4"},
                                  {"ActivateState","Int","4"},
                                  {"ActivateUserID","Int","4" },
                                  {"Remind","NVarChar","200" }
                                 };
            return Tablelist;
        }
        public string GetFieldAndPara()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }
        public override SqlParameter[] GetParameters()
        {
            M_Card model = this;
            if (CircumscribeTime <= DateTime.MinValue) { CircumscribeTime = DateTime.Now; }
            if (AddTime <= DateTime.MinValue) { AddTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Card_ID;
            sp[1].Value = model.CardNum;
            sp[2].Value = model.CardPwd;
            sp[3].Value = model.PutUserID;
            sp[4].Value = model.AssociateUserID;
            sp[5].Value = model.VIPType;
            sp[6].Value = model.StartTime;
            sp[7].Value = model.CircumscribeTime;
            sp[8].Value = model.PlyNum;
            sp[9].Value = model.AddTime;
            sp[10].Value = model.CardState;
            sp[11].Value = model.ActivateState;
            sp[12].Value = model.ActivateUserID;
            sp[13].Value = model.Remind;
            return sp;
        }
        public SqlParameter[] GetParameters(M_Card Cardinfo, int type)
        {
            SqlParameter[] parameter = new SqlParameter[] { };
            switch (type)
            {
                case 1:
                    parameter = new SqlParameter[6];
                    parameter[0] = new SqlParameter("@CardNum", SqlDbType.NVarChar, 50);
                    parameter[0].Value = Cardinfo.CardNum;
                    parameter[1] = new SqlParameter("@CardPwd", SqlDbType.NVarChar, 50);
                    parameter[1].Value = Cardinfo.CardPwd;
                    parameter[2] = new SqlParameter("@PutUserID", SqlDbType.Int, 4);
                    parameter[2].Value = Cardinfo.PutUserID;
                    parameter[3] = new SqlParameter("@VIPType", SqlDbType.Int, 4);
                    parameter[3].Value = Cardinfo.VIPType;
                    parameter[4] = new SqlParameter("@CircumscribeTime", SqlDbType.DateTime, 8);
                    parameter[4].Value = Cardinfo.CircumscribeTime;
                    parameter[5] = new SqlParameter("@CardState", SqlDbType.Int, 4);
                    parameter[5].Value = Cardinfo.CardState;
                    break;
                case 2:
                    break;
                case 3:
                    parameter = new SqlParameter[11];
                    parameter[0] = new SqlParameter("@Card_ID", SqlDbType.Int, 4);
                    parameter[0].Value = Cardinfo.Card_ID;
                    parameter[1] = new SqlParameter("@CardNum", SqlDbType.NVarChar, 50);
                    parameter[1].Value = Cardinfo.CardNum;
                    parameter[2] = new SqlParameter("@CardPwd", SqlDbType.NVarChar, 50);
                    parameter[2].Value = Cardinfo.CardPwd;
                    parameter[3] = new SqlParameter("@PutUserID", SqlDbType.Int, 4);
                    parameter[3].Value = Cardinfo.PutUserID;
                    parameter[4] = new SqlParameter("@AssociateUserID", SqlDbType.Int, 4);
                    parameter[4].Value = Cardinfo.AssociateUserID;
                    parameter[5] = new SqlParameter("@VIPType", SqlDbType.Int, 4);
                    parameter[5].Value = Cardinfo.VIPType;
                    parameter[6] = new SqlParameter("@StartTime", SqlDbType.NVarChar, 50);
                    parameter[6].Value = Cardinfo.StartTime;
                    parameter[7] = new SqlParameter("@CircumscribeTime", SqlDbType.Int, 4);
                    parameter[7].Value = Cardinfo.CircumscribeTime;
                    parameter[8] = new SqlParameter("@PlyNum", SqlDbType.Int, 4);
                    parameter[8].Value = Cardinfo.PlyNum;
                    parameter[9] = new SqlParameter("@AddTime", SqlDbType.DateTime, 8);
                    parameter[9].Value = Cardinfo.AddTime;
                    parameter[10] = new SqlParameter("@CardState", SqlDbType.Int, 4);
                    parameter[10].Value = Cardinfo.CardState;
                    break;
            }
            return parameter;
        }
        public M_Card GetModelFromReader(SqlDataReader rdr)
        {
            M_Card model = new M_Card();
            model.Card_ID = Convert.ToInt32(rdr["Card_ID"]);
            model.CardNum = ConverToStr(rdr["CardNum"]);
            model.CardPwd = ConverToStr(rdr["CardPwd"]);
            model.PutUserID = ConvertToInt(rdr["PutUserID"]);
            model.AssociateUserID = ConvertToInt(rdr["AssociateUserID"]);
            model.VIPType = ConvertToInt(rdr["VIPType"]);
            model.StartTime = ConverToStr(rdr["StartTime"]);
            model.CircumscribeTime = ConvertToDate(rdr["CircumscribeTime"]);
            model.PlyNum = ConvertToInt(rdr["PlyNum"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            model.CardState = ConvertToInt(rdr["CardState"]);
            model.ActivateState = ConvertToInt(rdr["ActivateState"]);
            model.ActivateUserID = ConvertToInt(rdr["ActivateUserID"]);
            model.Remind = ConverToStr(rdr["Remind"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
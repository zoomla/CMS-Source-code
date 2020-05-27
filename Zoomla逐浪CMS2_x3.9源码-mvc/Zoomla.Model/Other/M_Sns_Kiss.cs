using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_Sns_Kiss : M_Base
    {
        #region 构造函数
        public M_Sns_Kiss()
        {
        }

        public M_Sns_Kiss
        (
            int id,
            string Title,
            string Inputer,
            string Sendto,
            int Otherdel,
            string Content,
            int InputerID,
            int SendtoID,
            DateTime SendTime,
            DateTime ReadTime,
            int IsRead
        )
        {
            this.id = id;
            this.Title = Title;
            this.Inputer = Inputer;
            this.Sendto = Sendto;
            this.Otherdel = Otherdel;
            this.Content = Content;
            this.InputerID = InputerID;
            this.SendtoID = SendtoID;
            this.SendTime = SendTime;
            this.ReadTime = ReadTime;
            this.IsRead = IsRead;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] Sns_KissList()
        {
            string[] Tablelist = { "id", "Title", "Inputer", "Sendto", "Otherdel", "Content", "InputerID", "SendtoID", "SendTime", "ReadTime", "IsRead" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 秋波标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Inputer { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        public string Sendto { get; set; }
        /// <summary>
        /// 对方删除
        /// </summary>
        public int Otherdel { get; set; }
        /// <summary>
        /// 秋波内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int InputerID { get; set; }
        /// <summary>
        /// 对方ID
        /// </summary>
        public int SendtoID { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime ReadTime { get; set; }
        /// <summary>
        /// 是否打开
        /// </summary>
        public int IsRead { get; set; }
        #endregion

        public override string TbName { get { return "ZL_Sns_Kiss"; } }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"Title","NVarChar","1000"},
                                  {"Inputer","NVarChar","255"},
                                  {"Sendto","NVarChar","255"},
                                  {"Otherdel","Int","4"},
                                  {"Content","NText","400"},
                                  {"InputerID","Int","4"},
                                  {"SendtoID","Int","4"},
                                  {"SendTime","DateTime","8"},
                                  {"ReadTime","DateTime","8"},
                                  {"IsRead","Int","4"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Sns_Kiss model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.Title;
            sp[2].Value = model.Inputer;
            sp[3].Value = model.Sendto;
            sp[4].Value = model.Otherdel;
            sp[5].Value = model.Content;
            sp[6].Value = model.Inputer;
            sp[7].Value = model.SendtoID;
            sp[8].Value = model.SendTime;
            sp[9].Value = model.ReadTime;
            sp[10].Value = model.IsRead;
            return sp;
        }

        public M_Sns_Kiss GetModelFromReader(SqlDataReader rdr)
        {
            M_Sns_Kiss model = new M_Sns_Kiss();
            model.id = Convert.ToInt32(rdr["id"]);
            model.Title = rdr["Title"].ToString();
            model.Inputer = rdr["Inputer"].ToString();
            model.Sendto = rdr["Sendto"].ToString();
            model.Otherdel = Convert.ToInt32(rdr["Otherdel"]);
            model.Content = rdr["Content"].ToString();
            model.Inputer = rdr["InputerID"].ToString();
            model.SendtoID = Convert.ToInt32(rdr["SendtoID"]);
            model.SendTime = Convert.ToDateTime(rdr["SendTime"]);
            model.ReadTime = Convert.ToDateTime(rdr["ReadTime"]);
            model.IsRead = Convert.ToInt32(rdr["IsRead"]);
            rdr.Close();
            return model;
        }
    }
}



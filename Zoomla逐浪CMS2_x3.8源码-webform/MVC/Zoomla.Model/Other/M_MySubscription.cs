using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_MySubscription : M_Base
    {
        #region 构造方法
        public M_MySubscription() { }
        public M_MySubscription(int m_Id, int m_Reference_ID, DateTime m_Order_Time, int m_Period, DateTime m_Start_Date, int m_Order_Type, string m_User_Name, string m_Tel, string m_Email)
        {
            this.Id = m_Id;
            this.Reference_ID = m_Reference_ID;
            this.Order_Time = m_Order_Time;
            this.Period = m_Period;
            this.Start_Date = m_Start_Date;
            this.Order_Type = m_Order_Type;
            this.User_Name = m_User_Name;
            this.Tel = m_Tel;
            this.Email = m_Email;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 主键，自动增长
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 订阅的信息编号
        /// </summary>
        public int Reference_ID { get; set; }


        /// <summary>
        /// 本次订阅操作时间
        /// </summary>
        public DateTime Order_Time { get; set; }

        /// <summary>
        /// //订阅时间（以月为单位计算）
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// 订阅起始日期
        /// </summary>
        public DateTime Start_Date { get; set; }

        /// <summary>
        /// 信息订购状态
        /// </summary>
        public int Order_Type { get; set; }

        /// <summary>
        /// 订阅人
        /// </summary>
        public string User_Name { get; set; }

        /// <summary>
        /// 订阅人联系电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 订阅内容接受邮箱
        /// </summary>
        public string Email { get; set; }
        #endregion
        public override string PK { get { return ""; } }
        public override string TbName { get { return "ZL_MySubscription"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"Reference_ID","Int","4"},
                                  {"Order_Time","DateTime","8"},
                                  {"Period","Int","4"},
                                  {"Start_Date","DateTime","8"},
                                  {"Order_Type","Int","4"},
                                  {"User_Name","NVarChar","50"},
                                  {"Tel","NVarChar","50"},
                                  {"Email","NVarChar","50"}
                                 };
            return Tablelist;
        }

        public SqlParameter[] GetParameters(M_MySubscription model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.Id;
            sp[1].Value = model.Reference_ID;
            sp[2].Value = model.Order_Time;
            sp[3].Value = model.Period;
            sp[4].Value = model.Start_Date;
            sp[5].Value = model.Order_Type;
            sp[6].Value = model.User_Name;
            sp[7].Value = model.Tel;
            sp[8].Value = model.Email;
            return sp;
        }

        public M_MySubscription GetModelFromReader(SqlDataReader rdr)
        {
            M_MySubscription model = new M_MySubscription();
            model.Id = Convert.ToInt32(rdr["id"]);
            model.Reference_ID = Convert.ToInt32(rdr["Reference_ID"]);
            model.Order_Time = Convert.ToDateTime(rdr["Order_Time"]);
            model.Period = Convert.ToInt32(rdr["Period"]);
            model.Start_Date = Convert.ToDateTime(rdr["Start_Date"]);
            model.Order_Type = Convert.ToInt32(rdr["Order_Type"]);
            model.User_Name = rdr["User_Name"].ToString();
            model.Tel = rdr["Tel"].ToString();
            model.Email = rdr["Email"].ToString();
            rdr.Close();
            return model;
        }

    }
}
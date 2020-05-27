using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace ZoomLa.Model.Plat
{
    public class M_Plat_Pro : ZoomLa.Model.M_Base
    {
        public override string TbName { get { return "ZL_Plat_Project"; } }

        /// <summary>
        ///项目ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目经理
        /// </summary>
        public string LeaderIDS { get; set; }

        /// <summary>
        /// 项目成员
        /// </summary>
        public string ParterIDS { get; set; }

        /// <summary>
        /// 旁观人员
        /// </summary>
        public string LookIDS { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 是否公开
        /// 1=公开；0=不公开
        /// </summary>
        public int IsOpen { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string Attach { get; set; }

        /// <summary>
        /// 立即启动
        /// 1=勾选，0=不勾选
        /// </summary>
        public int Status { get; set; }
        public int UserID { get; set; }
        public string Desc { get; set; }

        public override string[,] FieldList()
        {
            string[,] TableList ={
                                    {"ID","Int","4"},
                                    {"Name","NVarChar","100"},
                                    {"LeaderIDS","VarChar","200"},
                                    {"ParterIDS","VarChar","4000"},
                                    {"LookIDS","VarChar","4000"},
                                    {"StartDate","DateTime","8"},
                                    {"EndDate","DateTime","8"},
                                    {"IsOpen","Int","4"},
                                    {"Status","Int","4"},
                                    {"UserID","Int","4"},
                                    {"Desc","NVarChar","500"},
                                    {"Attach","NVarChar","500"}
                                 };
            return TableList;
        }

        public SqlParameter[] GetParameters(M_Plat_Pro model)
        {
            EmptyDeal(); 
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.ID;
            sp[1].Value = model.Name;
            sp[2].Value = model.LeaderIDS;
            sp[3].Value = model.ParterIDS;
            sp[4].Value = model.LookIDS;
            sp[5].Value = model.StartDate;
            sp[6].Value = model.EndDate;
            sp[7].Value = model.IsOpen;
            sp[8].Value = model.Status;
            sp[9].Value = model.UserID;
            sp[10].Value = model.Desc;
            sp[11].Value = model.Attach;
            return sp;
        }
        public M_Plat_Pro GetModelFromReader(SqlDataReader rdr)
        {
            M_Plat_Pro model = new M_Plat_Pro();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.Name = rdr["Name"].ToString();
            model.LeaderIDS = rdr["LeaderIDS"].ToString();
            model.ParterIDS = rdr["ParterIDS"].ToString();
            model.LookIDS = rdr["LookIDS"].ToString();
            model.StartDate = Convert.ToDateTime(rdr["StartDate"]);
            model.EndDate = Convert.ToDateTime(rdr["EndDate"]);
            model.IsOpen = Convert.ToInt32(rdr["IsOpen"]);
            model.Status = Convert.ToInt32(rdr["Status"]);
            model.UserID = Convert.ToInt32(rdr["UserID"]);
            model.Desc = rdr["Desc"].ToString();
            model.Attach = rdr["Attach"].ToString();
            return model;
        }
        public void EmptyDeal() 
        {
            if (string.IsNullOrEmpty(Desc)) Desc = "";
            if (StartDate.Year < 1901) StartDate = DateTime.Now;
        }
    }
}

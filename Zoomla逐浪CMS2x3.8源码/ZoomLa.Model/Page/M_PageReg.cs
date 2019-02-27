using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model.Page
{
    public class M_PageReg : M_Base
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string PageTitle { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string LOGO { get; set; }
        public string Domain { get; set; }
        public int Style { get; set; }
        /// <summary>
        /// 绑定的黄页样式
        /// </summary>
        public int NodeStyle { get; set; }
        public string NavHeight { get; set; }
        public string NavColor { get; set; }
        public string NavBackground { get; set; }
        public string TableName { get; set; }
        public int Hits { get; set; }
        public int InfoID { get; set; }
        public string Recommendation { get; set; }
        public int Status { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int ParentPageID { get; set; }
        public int ParentPageUserID { get; set; }
        public string PageInfo { get; set; }
        public string TopWords { get; set; }
        public string BottonWords { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Template { get; set; }
        public int ModelID { get; set; }

        public M_PageReg()
        {
            this.CreationTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }

        public M_PageReg(bool flag)
        {
            this.m_IsNull = flag;
        }

        private bool m_IsNull;
        public bool IsNull
        {
            get { return this.m_IsNull; }
        }
        public override string TbName { get { return "ZL_PageReg"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"CompanyName","NVarChar","255"},
                                  {"PageTitle","NVarChar","255"},
                                  {"Keyword","NVarChar","255"},
                                  {"Description","NVarChar","255"},
                                  {"LOGO","NVarChar","255"},
                                  {"Domain","NVarChar","255"},
                                  {"Style","Int","4"},
                                  {"NodeStyle","Int","4"},
                                  {"NavHeight","NVarChar","255"},
                                  {"NavColor","NVarChar","255"},
                                  {"NavBackground","NVarChar","255"},
                                  {"TableName","NVarChar","255"},
                                  {"Hits","Int","4"},
                                  {"InfoID","Int","4"},
                                  {"Recommendation","NVarChar","255"},
                                  {"Status","Int","4"},
                                  {"UserID","Int","4"},
                                  {"UserName","NVarChar","255"},
                                  {"ParentPageID","Int","4"},
                                  {"ParentPageUserID","Int","4"},
                                  {"PageInfo","NText","20000"},
                                  {"TopWords","NVarChar","255"},
                                  {"BottonWords","NVarChar","255"},
                                  {"CreationTime","DateTime","8"},
                                  {"UpdateTime","DateTime","8"},
                                  {"Template","NVarChar","255"},
                                  {"ModelID","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_PageReg model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.CompanyName;
            sp[2].Value = model.PageTitle;
            sp[3].Value = model.Keyword;
            sp[4].Value = model.Description;
            sp[5].Value = model.LOGO;
            sp[6].Value = model.Domain;
            sp[7].Value = model.Style;
            sp[8].Value = model.NodeStyle;
            sp[9].Value = model.NavHeight;
            sp[10].Value = model.NavColor;
            sp[11].Value = model.NavBackground;
            sp[12].Value = model.TableName;
            sp[13].Value = model.Hits;
            sp[14].Value = model.InfoID;
            sp[15].Value = model.Recommendation;
            sp[16].Value = model.Status;
            sp[17].Value = model.UserID;
            sp[18].Value = model.UserName;
            sp[19].Value = model.ParentPageID;
            sp[20].Value = model.ParentPageUserID;
            sp[21].Value = model.PageInfo;
            sp[22].Value = model.TopWords;
            sp[23].Value = model.BottonWords;
            sp[24].Value = model.CreationTime;
            sp[25].Value = model.UpdateTime;
            sp[26].Value = model.Template;
            sp[27].Value = model.ModelID;
            return sp;
        }

        public M_PageReg GetModelFromReader(SqlDataReader rdr)
        {
            M_PageReg model = new M_PageReg();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.CompanyName = rdr["CompanyName"].ToString();
            model.PageTitle = rdr["PageTitle"].ToString();
            model.Keyword = rdr["Keyword"].ToString();
            model.Description = rdr["Description"].ToString();
            model.LOGO = rdr["LOGO"].ToString();
            model.Domain = rdr["Domain"].ToString();
            model.Style = Convert.ToInt32(rdr["Style"].ToString());
            model.NodeStyle = Convert.ToInt32(rdr["NodeStyle"].ToString());
            model.NavHeight = rdr["NavHeight"].ToString();
            model.NavColor = rdr["NavColor"].ToString();
            model.NavBackground = rdr["NavBackground"].ToString();
            model.TableName = rdr["TableName"].ToString();
            model.Hits = Convert.ToInt32(rdr["Hits"].ToString());
            model.InfoID = Convert.ToInt32(rdr["InfoID"].ToString());
            model.Recommendation = rdr["Recommendation"].ToString();
            model.Status = Convert.ToInt32(rdr["Status"].ToString());
            model.UserID = Convert.ToInt32(rdr["UserID"].ToString());
            model.UserName = rdr["UserName"].ToString();
            model.ParentPageID = Convert.ToInt32(rdr["ParentPageID"].ToString());
            model.ParentPageUserID = Convert.ToInt32(rdr["ParentPageUserID"].ToString());
            model.PageInfo = rdr["PageInfo"].ToString();
            model.TopWords = rdr["TopWords"].ToString();
            model.BottonWords = rdr["BottonWords"].ToString();
            model.CreationTime = Convert.ToDateTime(rdr["CreationTime"]);
            model.UpdateTime = Convert.ToDateTime(rdr["UpdateTime"]);
            model.Template = rdr["Template"].ToString();
            model.ModelID = Convert.ToInt32(rdr["ModelID"]);
            rdr.Close();
            return model;
        }
    }
}
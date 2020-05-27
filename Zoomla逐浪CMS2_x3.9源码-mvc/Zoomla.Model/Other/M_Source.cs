namespace ZoomLa.Model
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    /// <summary>
    /// M_Source 的摘要说明
    /// </summary>
    public class M_Source : M_Base
    {
        private string m_Photo;//来源图片
        public M_Source()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int SourceID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public bool Passed { get; set; }
        public bool onTop { get; set; }
        public bool IsElite { get; set; }
        public int Hits { get; set; }
        public DateTime LastUseTime { get; set; }
        public string Photo
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_Photo) || m_Photo.Equals("/App"))
                    m_Photo = "/App_Themes/AdminDefaultTheme/Images/default.gif";
                return this.m_Photo;
            }
            set
            {
                this.m_Photo = value;
            }
        }
        public string Intro { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public string Email { get; set; }
        //------------ZipCode
        public int ZipCode { get; set; }
        public string HomePage { get; set; }
        public string Im { get; set; }
        public string Contacter { get; set; }
        public override string TbName { get { return "ZL_Source"; } }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"Type","NVarChar","50"},
                                  {"Name","NVarChar","50"},
                                  {"Passed","Bit","4"}, 
                                  {"onTop","Bit","4"}, 
                                  {"IsElite","Bit","4"}, 
                                  {"Hits","Int","4"}, 
                                  {"LastUseTime","DateTime","50"}, 
                                  {"Photo","NVarChar","50"}, 
                                  {"Intro","NVarChar","50"}, 
                                  {"Address","NVarChar","50"}, 
                                  {"Tel","NVarChar","50"}, 
                                  {"Fax","NVarChar","50"}, 
                                  {"Mail","NVarChar","50"}, 
                                  {"Email","NVarChar","50"}, 
                                  {"ZipCode","Int","6"}, 
                                  {"HomePage","NVarChar","50"}, 
                                  {"Im","NVarChar","50"}, 
                                  {"Contacter","NVarChar","50"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Source model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.SourceID;
            sp[1].Value = model.Type;
            sp[2].Value = model.Name;
            sp[3].Value = model.Passed;
            sp[4].Value = model.onTop;
            sp[5].Value = model.IsElite;
            sp[6].Value = model.Hits;
            sp[7].Value = model.LastUseTime;
            sp[8].Value = model.Photo;
            sp[9].Value = model.Intro;
            sp[10].Value = model.Address;
            sp[11].Value = model.Tel;
            sp[12].Value = model.Fax;
            sp[13].Value = model.Mail;
            sp[14].Value = model.Email;
            sp[15].Value = model.ZipCode;
            sp[16].Value = model.HomePage;
            sp[17].Value = model.Im;
            sp[18].Value = model.Contacter;
            return sp;
        }

        public M_Source GetModelFromReader(SqlDataReader rdr)
        {
            M_Source model = new M_Source();
            model.SourceID = Convert.ToInt32(rdr["ID"]);
            model.Type = rdr["Type"].ToString();
            model.Name = rdr["Name"].ToString();
            model.Passed = Convert.ToBoolean(rdr["Passed"]);
            model.onTop = Convert.ToBoolean(rdr["onTop"]);
            model.IsElite = Convert.ToBoolean(rdr["IsElite"]);
            model.Hits = Convert.ToInt32(rdr["Hits"]);
            model.LastUseTime = Convert.ToDateTime(rdr["LastUseTime"]);
            model.Photo = rdr["Photo"].ToString();
            model.Intro = rdr["Intro"].ToString();
            model.Address = rdr["Address"].ToString();
            model.Tel = rdr["Tel"].ToString();
            model.Fax = rdr["Fax"].ToString();
            model.Mail = rdr["Mail"].ToString();
            model.Email = rdr["Email"].ToString();
            model.ZipCode = Convert.ToInt32(rdr["ZipCode"]);
            model.HomePage = rdr["HomePage"].ToString();
            model.Im = rdr["Im"].ToString();
            model.Contacter = rdr["Contacter"].ToString();
            rdr.Close();
            return model;
        }
    }
}
namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;
    /// <summary>
    /// M_Author 的摘要说明
    /// </summary>
    public class M_Author : M_Base
    {

        public M_Author()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public int ID { get; set; }
        public int AuthorID { get; set; }
        public string AuthorType { get; set; }
        public string AuthorName { get; set; }
        public bool AuthorPassed { get; set; }
        public bool AuthoronTop { get; set; }
        public bool AuthorIsElite { get; set; }
        public int AuthorHits { get; set; }
        public DateTime AuthorLastUseTime { get; set; }
        public int AuthorTemplateID { get; set; }
        public string AuthorPhoto { get; set; }
        public string AuthorIntro { get; set; }
        public string AuthorAddress { get; set; }
        public string AuthorTel { get; set; }
        public string AuthorFax { get; set; }
        public string AuthorMail { get; set; }
        public string AuthorEmail { get; set; }
        public int AuthorZipCode { get; set; }
        public string AuthorHomePage { get; set; }
        public string AuthorIm { get; set; }
        public int AuthorSex { get; set; }
        public DateTime AuthorBirthDay { get; set; }
        public string AuthorCompany { get; set; }
        public string AuthorDepartment { get; set; }
        public override string TbName { get { return "ZL_Author"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"UserID","Int","4"},
                                  {"Type","NVarChar","50"},
                                  {"Name","NVarChar","50"}, 
                                  {"Passed","Bit","4"}, 
                                  {"onTop","Bit","4"}, 
                                  {"IsElite","Bit","4"}, 
                                  {"Hits","Int","4"}, 
                                  {"LastUseTime","DateTime","8"}, 
                                  {"TemplateID","Int","4"}, 
                                  {"Photo","NVarChar","255"}, 
                                  {"Intro","NText","400"}, 
                                  {"Address","NVarChar","50"}, 
                                  {"Tel","NVarChar","50"}, 
                                  {"Fax","NVarChar","50"}, 
                                  {"Mail","NVarChar","50"}, 
                                  {"Email","NVarChar","50"}, 
                                  {"ZipCode","Int","4"}, 
                                  {"HomePage","NVarChar","50"}, 
                                  {"Im","NVarChar","50"}, 
                                  {"Sex","SmallInt","4"}, 
                                  {"BirthDay","DateTime","8"}, 
                                  {"Company","NVarChar","50"}, 
                                  {"Department","NVarChar","50"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_Author model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.AuthorID;
            sp[2].Value = model.AuthorType;
            sp[3].Value = model.AuthorName;
            sp[4].Value = model.AuthorPassed;
            sp[5].Value = model.AuthoronTop;
            sp[6].Value = model.AuthorIsElite;
            sp[7].Value = model.AuthorHits;
            sp[8].Value = model.AuthorLastUseTime;
            sp[9].Value = model.AuthorTemplateID;
            sp[10].Value = model.AuthorPhoto;
            sp[11].Value = model.AuthorIntro;
            sp[12].Value = model.AuthorAddress;
            sp[13].Value = model.AuthorTel;
            sp[14].Value = model.AuthorFax;
            sp[15].Value = model.AuthorMail;
            sp[16].Value = model.AuthorEmail;
            sp[17].Value = model.AuthorZipCode;
            sp[18].Value = model.AuthorHomePage;
            sp[19].Value = model.AuthorIm;
            sp[20].Value = model.AuthorSex;
            sp[21].Value = model.AuthorBirthDay;
            sp[22].Value = model.AuthorCompany;
            sp[23].Value = model.AuthorDepartment;
            return sp;
        }

        public M_Author GetModelFromReader(SqlDataReader rdr)
        {
            M_Author model = new M_Author();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.AuthorID = Convert.ToInt32(rdr["UserID"]);
            model.AuthorType = rdr["Type"].ToString();
            model.AuthorName = rdr["Name"].ToString();
            model.AuthorPassed = Convert.ToBoolean(rdr["Passed"]);
            model.AuthoronTop = Convert.ToBoolean(rdr["onTop"]);
            model.AuthorIsElite = Convert.ToBoolean(rdr["IsElite"]);
            model.AuthorHits = Convert.ToInt32(rdr["Hits"]);
            model.AuthorLastUseTime = Convert.ToDateTime(rdr["LastUseTime"]);
            model.AuthorTemplateID = Convert.ToInt32(rdr["TemplateID"]);
            model.AuthorPhoto = rdr["Photo"].ToString();
            model.AuthorIntro = rdr["Intro"].ToString();
            model.AuthorAddress = rdr["Address"].ToString();
            model.AuthorTel = rdr["Tel"].ToString();
            model.AuthorFax = rdr["Fax"].ToString();
            model.AuthorMail = rdr["Mail"].ToString();
            model.AuthorEmail = rdr["Email"].ToString();
            model.AuthorZipCode = Convert.ToInt32(rdr["ZipCode"]);
            model.AuthorHomePage = rdr["HomePage"].ToString();
            model.AuthorIm = rdr["Im"].ToString();
            model.AuthorSex = Convert.ToInt32(rdr["Sex"]);
            model.AuthorBirthDay = Convert.ToDateTime(rdr["BirthDay"]);
            model.AuthorCompany = rdr["Company"].ToString();
            model.AuthorDepartment = rdr["Department"].ToString();
            rdr.Close();
            return model;
        }
    }
}

using System;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    public class M_Manufacturers:M_Base
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 厂商分类
        /// </summary>
        public string CoClass { get; set; }
        /// <summary>
        /// 厂商照片
        /// </summary>
        public string CoPhoto { get; set; }
        /// <summary>
        /// 厂商简介
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 固顶
        /// </summary>
        public int Istop { get; set; }
        /// <summary>
        /// 禁用
        /// </summary>
        public int Disable { get; set; }
        /// <summary>
        /// 推荐
        /// </summary>
        public int Isbest { get; set; }
        /// <summary>
        /// 厂商名称
        /// </summary>
        public string Producername { get; set; }
        /// <summary>
        /// 厂商缩写
        /// </summary>
        public string Smallname { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string Coadd { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telpho { get; set; }
        /// <summary>
        /// 传真号码
        /// </summary>
        public string FaxCode { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 厂商主页
        /// </summary>
        public string CoWebsite { get; set; }
        public override string TbName { get { return "ZL_Manufacturers"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"Producername","VarChar","255"},
                                  {"Smallname","VarChar","255"},
                                  {"CreateTime","DateTime","50"}, 
                                  {"Coadd","VarChar","255"}, 
                                  {"Telpho","VarChar","255"}, 
                                  {"FaxCode","VarChar","50"}, 
                                  {"PostCode","VarChar","50"}, 
                                  {"CoWebsite","VarChar","255"}, 
                                  {"Email","VarChar","50"}, 
                                  {"CoClass","VarChar","50"}, 
                                  {"CoPhoto","VarChar","255"}, 
                                  {"Content","Text","1000"}, 
                                  {"Istop","Int","4"}, 
                                  {"Disable","Int","4"}, 
                                  {"Isbest","Int","4"} 
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

        public SqlParameter[] GetParameters(M_Manufacturers model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.id;
            sp[1].Value = model.Producername;
            sp[2].Value = model.Smallname;
            sp[3].Value = model.CreateTime;
            sp[4].Value = model.Coadd;
            sp[5].Value = model.Telpho;
            sp[6].Value = model.FaxCode;
            sp[7].Value = model.PostCode;
            sp[8].Value = model.CoWebsite;
            sp[9].Value = model.Email;
            sp[10].Value = model.CoClass;
            sp[11].Value = model.CoPhoto;
            sp[12].Value = model.Content;
            sp[13].Value = model.Istop;
            sp[14].Value = model.Disable;
            sp[15].Value = model.Isbest;
            return sp;
        }

        public M_Manufacturers GetModelFromReader(SqlDataReader rdr)
        {
            M_Manufacturers model = new M_Manufacturers();
            model.id = Convert.ToInt32(rdr["id"]);
            model.Producername = rdr["Producername"].ToString();
            model.Smallname = rdr["Smallname"].ToString();
            model.CreateTime = Convert.ToDateTime(rdr["CreateTime"]);
            model.Coadd = rdr["Coadd"].ToString();
            model.Telpho = rdr["Telpho"].ToString();
            model.FaxCode = rdr["FaxCode"].ToString();
            model.PostCode = rdr["PostCode"].ToString();
            model.CoWebsite = rdr["CoWebsite"].ToString();
            model.Email = rdr["Email"].ToString();
            model.CoClass = rdr["CoClass"].ToString();
            model.CoPhoto = rdr["CoPhoto"].ToString();
            model.Content = rdr["Content"].ToString();
            model.Istop = Convert.ToInt32(rdr["Istop"]);
            model.Disable = Convert.ToInt32(rdr["Disable"]);
            model.Isbest = Convert.ToInt32(rdr["Isbest"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}

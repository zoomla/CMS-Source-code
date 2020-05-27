using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_School:M_Base
    {
        #region 构造函数
        public M_School()
        {
        }

        public M_School
        (
            int ID,
            string SchoolName,
            string Country,
            string Province,
            int SchoolType,
            int Visage,
            DateTime Addtime,
            string SchoolInfo
        )
        {
            this.ID = ID;
            this.SchoolName = SchoolName;
            this.Country = Country;
            this.Province = Province;
            this.SchoolType = SchoolType;
            this.Visage = Visage;
            this.Addtime = Addtime;
            this.SchoolInfo = SchoolInfo;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] SchoolList()
        {
            string[] Tablelist = { "ID", "SchoolName", "Country", "Province", "SchoolType", "Visage", "Addtime", "SchoolInfo" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        public int ID { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        /// <summary>
        /// 学校类型(1-小学 2-中学 3-大学 4-其他)
        /// </summary>
        public int SchoolType { get; set; }
        /// <summary>
        /// 学校面貌(1-公办 2-私立)
        /// </summary>
        public int Visage { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
        /// <summary>
        /// 学校说明
        /// </summary>
        public string SchoolInfo { get; set; }
        #endregion
        public string GetScoolType(int type)
        {
            switch (type)
            {
                case 1:
                    return "小学";
                case 2:
                    return "中学";
                case 3:
                    return "高中";
                case 4:
                    return "中专";
                case 5:
                    return "大学";
                default:
                    return "其它";
            }
        }
        public override string TbName { get { return "ZL_School"; } }
        public override string[,] FieldList()
        {

            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"SchoolName","NVarChar","255"},
                                  {"Country","NVarChar","255"}, 
                                  {"Province","NVarChar","255"},
                                  {"SchoolType","Int","4"},
                                  {"Visage","Int","4"}, 
                                  {"Addtime","DateTime","8"},
                                  {"SchoolInfo","NText","400"},
                                  {"City","NVarChar","255"},
                                  {"County","NVarChar","255"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_School model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.SchoolName;
            sp[2].Value = model.Country;
            sp[3].Value = model.Province;
            sp[4].Value = model.SchoolType;
            sp[5].Value = model.Visage;
            sp[6].Value = model.Addtime;
            sp[7].Value = model.SchoolInfo;
            sp[8].Value = model.City;
            sp[9].Value = model.County;
            return sp;
        }
        public M_School GetModelFromReader(SqlDataReader rdr)
        {
            M_School model = new M_School();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.SchoolName = ConverToStr(rdr["SchoolName"]);
            model.Country = ConverToStr(rdr["Country"]);
            model.Province = ConverToStr(rdr["Province"]);
            model.SchoolType = ConvertToInt(rdr["SchoolType"]);
            model.Visage = ConvertToInt(rdr["Visage"]);
            model.Addtime =ConvertToDate(rdr["Addtime"]);
            model.SchoolInfo = ConverToStr(rdr["SchoolInfo"]);
            model.City = ConverToStr(rdr["City"]);
            model.County = ConverToStr(rdr["County"]);
            rdr.Close();
            return model;
        }
    }
}

namespace ZoomLa.Model
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class M_Trademark:M_Base
    {
        public int id { get; set; }
        public DateTime Addtime { get; set; }
        /// <summary>
        /// 商标名
        /// </summary>
        public string Trname { get; set; }
        /// <summary>
        /// 所属厂商
        /// </summary>
        public int Producer { get; set; }
        /// <summary>
        /// 商标类别
        /// </summary>
        public string TrClass { get; set; }
        /// <summary>
        /// 是否使用
        /// </summary>
        public int Isuse { get; set; }
        /// <summary>
        /// 是否品牌
        /// </summary>
        public int Istop { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public int Isbest { get; set; }
        /// <summary>
        /// 品牌照片
        /// </summary>
        public string TrPhoto { get; set; }
        /// <summary>
        /// 品牌简介
        /// </summary>
        public string TrContent { get; set; }
        public override string TbName { get { return "ZL_Trademark"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"Trname","NVarChar","255"},
                                  {"Producer","Int","4"},
                                  {"TrClass","NVarChar","50"}, 
                                  {"Isuse","Int","4"},
                                  {"Istop","Int","4"},
                                  {"Isbest","Int","4"},
                                  {"TrPhoto","NVarChar","50"},
                                  {"TrContent","Text","400"}, 
                                  {"Addtime","DateTime","8"}
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
        public SqlParameter[] GetParameters(M_Trademark model)
        {
            string[,] strArr = FieldList();
            SqlParameter[] sp = new SqlParameter[strArr.Length];

            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                sp[i] = new SqlParameter("@" + strArr[i, 0], (SqlDbType)Enum.Parse(typeof(SqlDbType), strArr[i, 1]), Convert.ToInt32(strArr[i, 2]));
            }
            sp[0].Value = model.id;
            sp[1].Value = model.Trname;
            sp[2].Value = model.Producer;
            sp[3].Value = model.TrClass;
            sp[4].Value = model.Isuse;
            sp[5].Value = model.Istop;
            sp[6].Value = model.Isbest;
            sp[7].Value = model.TrPhoto;
            sp[8].Value = model.TrContent;
            sp[9].Value = model.Addtime;
            return sp;
        }

        public M_Trademark GetModelFromReader(SqlDataReader rdr)
        {
            M_Trademark model = new M_Trademark();
            model.id = Convert.ToInt32(rdr["id"]);
            model.Trname = rdr["Trname"].ToString();
            model.Producer = Convert.ToInt32(rdr["Producer"]);
            model.TrClass = rdr["TrClass"].ToString();
            model.Isuse = Convert.ToInt32(rdr["Isuse"]);
            model.Istop = Convert.ToInt32(rdr["Istop"]);
            model.Isbest = Convert.ToInt32(rdr["Isbest"]);
            model.TrPhoto = rdr["TrPhoto"].ToString();
            model.TrContent = rdr["TrContent"].ToString();
            model.Addtime = Convert.ToDateTime(rdr["Addtime"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
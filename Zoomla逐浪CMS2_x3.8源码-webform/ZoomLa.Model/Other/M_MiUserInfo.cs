using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_MiUserInfo : M_Base
    {
        #region 构造函数
        public M_MiUserInfo()
        {
        }


        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>

        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        public int M_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolID { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        public string banji { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string prov { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 学科
        /// </summary>
        public string daike { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string realname { get; set; }
        #endregion
        public override string PK { get { return "M_id"; } }
        public override string TbName { get { return "ZL_MiUserInfo"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"M_id","Int","4"},
                                  {"userid","Int","4"},
                                  {"schoolID","Int","4"},
                                  {"banji","NVarChar","255"},
                                  {"prov","NVarChar","50"},
                                  {"city","NVarChar","50"},
                                  {"daike","NVarChar","255"},
                                  {"realname","NVarChar","255"},
                                  {"type","Int","4"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_MiUserInfo model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.M_id;
            sp[1].Value = model.userid;
            sp[2].Value = model.schoolID;
            sp[3].Value = model.banji;
            sp[4].Value = model.prov;
            sp[5].Value = model.city;
            sp[6].Value = model.daike;
            sp[7].Value = model.realname;
            sp[8].Value = model.type;
            return sp;
        }

        public M_MiUserInfo GetModelFromReader(SqlDataReader rdr)
        {
            M_MiUserInfo model = new M_MiUserInfo();
            model.M_id = Convert.ToInt32(rdr["M_id"]);
            model.userid = Convert.ToInt32(rdr["userid"]);
            model.schoolID = rdr["schoolID"].ToString();
            model.banji = rdr["banji"].ToString();
            model.prov = rdr["prov"].ToString();
            model.city = rdr["city"].ToString();
            model.daike = rdr["daike"].ToString();
            model.realname = rdr["realname"].ToString();
            model.type = Convert.ToInt32(rdr["type"]);
            rdr.Close();
            return model;
        }

    }
}



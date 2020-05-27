using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_DMusic:M_Base
    {
        #region 属性定义
        public int id
        {
            get;
            set;
        }
        /// <summary>
        /// 音乐名称
        /// </summary>
        public string MusicName
        {
            get;
            set;
        }
        /// <summary>
        /// 音乐地址
        /// </summary>
        public string MusicUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsTrue
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get;
            set;
        }
        #endregion
        public override string TbName { get { return "ZL_3DMusic"; } }
        public override string GetPK()
        {
            return PK;
        }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"MusicName","NVarChar","255"},
                                  {"MusicUrl","NVarChar","1000"},
                                  {"IsTrue","Int","4"}, 
                                  {"AddTime","DateTime","4000"}
                                 };
            return Tablelist;
        }

        public override SqlParameter[] GetParameters()
        {
            M_DMusic model = this;
            if (model.AddTime <= DateTime.MinValue) { model.AddTime = DateTime.Now; }
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.MusicName;
            sp[2].Value = model.MusicUrl;
            sp[3].Value = model.IsTrue;
            sp[4].Value = model.AddTime;
            return sp;
        }

        public M_DMusic GetModelFromReader(SqlDataReader rdr)
        {
            M_DMusic model = new M_DMusic();
            model.id = Convert.ToInt32(rdr["id"]);
            model.MusicName = ConverToStr(rdr["MusicName"]);
            model.MusicUrl = ConverToStr(rdr["MusicUrl"]);
            model.IsTrue = ConvertToInt(rdr["IsTrue"]);
            model.AddTime = ConvertToDate(rdr["AddTime"]);
            rdr.Close();
            return model;
        }
    }
}
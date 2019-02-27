using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_EnrollList:M_Base
    {
        #region 定义字段
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 招生人员ID
        /// </summary>
        public int UesrID { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string infos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        #endregion

        #region 构造函数
        public M_EnrollList()
        {
        }

        public M_EnrollList
        (
            int id,
            int UesrID,
            DateTime CreateTime,
            string infos
        )
        {
            this.id = id;
            this.UesrID = UesrID;
            this.CreateTime = CreateTime;
            this.infos = infos;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] EnrollListList()
        {
            string[] Tablelist = { "id", "UesrID", "CreateTime", "infos" };
            return Tablelist;
        }
        #endregion

        public override string TbName { get { return "ZL_EnrollList"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"UesrID","Int","4"},
                                  {"CreateTime","DateTime","8"}, 
                                  {"infos","NVarChar","255"}
                                 };
         
            return Tablelist;
        }
        public  override SqlParameter[] GetParameters()
        {
            M_EnrollList model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.UesrID;
            sp[2].Value = model.CreateTime;
            sp[3].Value = model.infos;     
            return sp;
        }

        public  M_EnrollList GetModelFromReader(SqlDataReader rdr)
        {
            M_EnrollList model = new M_EnrollList();
            model.id = Convert.ToInt32(rdr["id"]);
            model.UesrID = Convert.ToInt32(rdr["UesrID"]);
            model.CreateTime = Convert.ToDateTime(rdr["CreateTime"]);
            model.infos = rdr["infos"].ToString();
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
   
}



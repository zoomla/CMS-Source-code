using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_DPanoramic : M_Base
    {
        #region 构造函数
        public M_DPanoramic()
        {
        }

        public M_DPanoramic
        (
            int id,
            string PanoramicName,
            int Istrue,
            string Callabel,
            string PanoramicViewList,
            int OrderBy,
            DateTime Addtime
        )
        {
            this.id = id;
            this.PanoramicName = PanoramicName;
            this.Istrue = Istrue;
            this.Callabel = Callabel;
            this.PanoramicViewList = PanoramicViewList;
            this.OrderBy = OrderBy;
            this.Addtime = Addtime;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] DPanoramicList()
        {
            string[] Tablelist = { "id", "PanoramicName", "Istrue", "Callabel", "PanoramicViewList", "OrderBy", "Addtime" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 全景名称
        /// </summary>
        public string PanoramicName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int Istrue { get; set; }
        /// <summary>
        /// 调用标签
        /// </summary>
        public string Callabel { get; set; }
        /// <summary>
        /// 全景地图
        /// </summary>
        public string PanoramicViewList { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
        #endregion
        public override string TbName { get { return "ZL_3DPanoramic"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"PanoramicName","NVarChar","255"},
                                  {"Istrue","Int","4"},
                                  {"Callabel","NVarChar","255"}, 
                                  {"PanoramicViewList","NText","400"},
                                  {"OrderBy","Int","4"}, 
                                  {"Addtime","DateTime","8"}
                                 };
            return Tablelist;
        }
        public override string GetPK()
        {
            return PK;
        }

        public SqlParameter[] GetParameters(M_DPanoramic model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.PanoramicName;
            sp[2].Value = model.Istrue;
            sp[3].Value = model.Callabel;
            sp[4].Value = model.PanoramicViewList;
            sp[5].Value = model.OrderBy;
            sp[6].Value = model.Addtime;
            return sp;
        }

        public M_DPanoramic GetModelFromReader(SqlDataReader rdr)
        {
            M_DPanoramic model = new M_DPanoramic();
            model.id = Convert.ToInt32(rdr["id"]);
            model.PanoramicName = ConverToStr(rdr["PanoramicName"]);
            model.Istrue = ConvertToInt(rdr["Istrue"]);
            model.Callabel = ConverToStr(rdr["Callabel"]);
            model.PanoramicViewList = ConverToStr(rdr["PanoramicViewList"]);
            model.OrderBy = ConvertToInt(rdr["OrderBy"]);
            model.Addtime = ConvertToDate(rdr["Addtime"]);
            rdr.Close();
            return model;
        }
    }
}
using System;
using System.Data.SqlClient;
using System.Data;

namespace ZoomLa.Model
{
    /// <summary>
    ///ZL_PageStyle业务实体
    /// </summary>
    [Serializable]
    public class M_PageStyle : M_Base
    {
        #region 构造函数

        ///<summary>
        ///
        ///</summary>
        public M_PageStyle()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public M_PageStyle
        (
            int pageNodeid,
            string pageNodeName,
            int istrue,
            int orderid,
            DateTime addtime,
            int isDefault
        )
        {
            this.PageNodeid = pageNodeid;
            this.PageNodeName = pageNodeName;
            this.Istrue = istrue;
            this.Orderid = orderid;
            this.Addtime = addtime;
            this.IsDefault = isDefault;

        }

        #endregion

        #region 属性定义

        ///<summary>
        ///
        ///</summary>
        public int PageNodeid { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string PageNodeName { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Istrue { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int Orderid { get; set; }

        ///<summary>
        ///
        ///</summary>
        public DateTime Addtime { get; set; }

        ///<summary>
        ///
        ///</summary>
        public int IsDefault { get; set; }
        /// <summary>
        /// 首页模板
        /// </summary>
        public string TemplateIndex { get; set; }
        /// <summary>
        /// 模板图片
        /// </summary>
        public string TemplateIndexPic { get; set; }

        private string _stylePath = "";
        public string StylePath
        {
            get
            {
                if (string.IsNullOrEmpty(_stylePath)) _stylePath = "";
                else
                {
                    _stylePath = _stylePath.Trim();
                    if (!_stylePath.EndsWith("/")) _stylePath += "/";
                    if (!_stylePath.StartsWith("/")) _stylePath += "/";
                }
                return _stylePath;
            }
            set
            {
                _stylePath = value;
            }
        }
        #endregion
        public override string PK { get { return "PageNodeid"; } }
        public override string TbName { get { return "ZL_PageStyle"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"PageNodeid","Int","4"},
                                  {"PageNodeName","NVarChar","255"},
                                  {"Istrue","Int","4"},
                                  {"Orderid","Int","4"}, 
                                  {"Addtime","DateTime","8"},
                                  {"IsDefault","Int","4"},
                                  {"StylePath","NVarChar","255"},
                                  {"TemplateIndex","NVarChar","255"},
                                  {"TemplateIndexPic","NVarChar","255"}
                                 };
            return Tablelist;
        }
        public SqlParameter[] GetParameters(M_PageStyle model)
        {
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.PageNodeid;
            sp[1].Value = model.PageNodeName;
            sp[2].Value = model.Istrue;
            sp[3].Value = model.Orderid;
            sp[4].Value = model.Addtime;
            sp[5].Value = model.IsDefault;
            sp[6].Value = model.StylePath;
            sp[7].Value = model.TemplateIndex;
            sp[8].Value = model.TemplateIndexPic;
            return sp;
        }

        public M_PageStyle GetModelFromReader(SqlDataReader rdr)
        {
            M_PageStyle model = new M_PageStyle();
            model.PageNodeid = Convert.ToInt32(rdr["PageNodeid"]);
            model.PageNodeName = rdr["PageNodeName"].ToString();
            model.Istrue = Convert.ToInt32(rdr["Istrue"]);
            model.Orderid = Convert.ToInt32(rdr["Orderid"]);
            model.Addtime = Convert.ToDateTime(rdr["Addtime"]);
            model.IsDefault = Convert.ToInt32(rdr["IsDefault"]);
            model.StylePath = rdr["StylePath"] as string;
            model.TemplateIndex = rdr["TemplateIndex"].ToString();
            model.TemplateIndexPic = rdr["TemplateIndexPic"].ToString();
            rdr.Close();
            return model;
        }
    }
}
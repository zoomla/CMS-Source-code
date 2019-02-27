using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    [Serializable]
    public class M_PageSkins : M_Base
    {
        #region 构造函数
        public M_PageSkins()
        {
        }

        public M_PageSkins
        (
            int id,
            string Stylename,
            string ShowImg,
            string StyleUrl,
            int IsDefault,
            string DefaultTemplate,
            string NodeTemplate

        )
        {
            this.id = id;
            this.Stylename = Stylename;
            this.ShowImg = ShowImg;
            this.StyleUrl = StyleUrl;
            this.IsDefault = IsDefault;
            this.DefaultTemplate = DefaultTemplate;
            this.NodeTemplate = NodeTemplate;
        }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public string[] PageSkinsList()
        {
            string[] Tablelist = { "id", "Stylename", "ShowImg", "StyleUrl", "IsDefault", "DefaultTemplate", "NodeTemplate" };
            return Tablelist;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 风格名称
        /// </summary>
        public string Stylename { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string ShowImg { get; set; }
        /// <summary>
        /// 风格路径
        /// </summary>
        public string StyleUrl { get; set; }
        /// <summary>
        /// 是否为默认
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 默认首页模板
        /// </summary>
        public string DefaultTemplate { get; set; }

        /// <summary>
        /// 默认节点模板
        /// </summary>
        public string NodeTemplate { get; set; }
        #endregion
        public override string TbName { get { return "ZL_PageSkins"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"id","Int","4"},
                                  {"Stylename","NVarChar","1000"},
                                  {"ShowImg","NVarChar","1000"},
                                  {"StyleUrl","NVarChar","1000"}, 
                                  {"IsDefault","Int","4"}, 
                                  {"DefaultTemplate","NVarChar","1000"}, 
                                  {"NodeTemplate","NVarChar","1000"}, 
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_PageSkins model=this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.id;
            sp[1].Value = model.Stylename;
            sp[2].Value = model.ShowImg;
            sp[3].Value = model.StyleUrl;
            sp[4].Value = model.IsDefault;
            sp[5].Value = model.DefaultTemplate;
            sp[6].Value = model.NodeTemplate;
            return sp;
        }

        public M_PageSkins GetModelFromReader(SqlDataReader rdr)
        {
            M_PageSkins model = new M_PageSkins();
            model.id = Convert.ToInt32(rdr["id"]);
            model.Stylename = rdr["Stylename"].ToString();
            model.ShowImg = rdr["ShowImg"].ToString();
            model.StyleUrl = rdr["StyleUrl"].ToString();
            model.IsDefault = Convert.ToInt32(rdr["IsDefault"]);
            model.DefaultTemplate = rdr["DefaultTemplate"].ToString();
            model.NodeTemplate = rdr["NodeTemplate"].ToString();
            rdr.Close();
            return model;
        }
    }
}



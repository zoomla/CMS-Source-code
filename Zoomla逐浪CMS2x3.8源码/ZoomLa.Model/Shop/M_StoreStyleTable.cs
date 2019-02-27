using System;
using System.Data.SqlClient;
using System.Data;
namespace ZoomLa.Model
{
    public class M_StoreStyleTable : M_Base
    {
        #region 构造函数
        public M_StoreStyleTable()
        {
            this.StyleName = string.Empty;
            this.StylePic = string.Empty;
            this.StyleUrl = string.Empty;
            this.ListStyle = string.Empty;
            this.ContentStyle = string.Empty;
        }
        #endregion
        #region 属性定义
        public int ID { get; set; }
        ///<summary>
        ///模板名
        ///</summary>
        public string StyleName { get; set; }
        ///<summary>
        ///显示缩图
        ///</summary>
        public string StylePic { get; set; }
        ///<summary>
        ///模板路径
        ///</summary>
        public string StyleUrl { get; set; }
        ///<summary>
        ///列表页模板路径
        ///</summary>
        public string ListStyle { get; set; }
        ///<summary>
        ///内容页模板路径
        ///</summary>
        public string ContentStyle { get; set; }
        ///<summary>
        ///所属模型
        ///</summary>
        public int ModelID { get; set; }
        ///<summary>
        ///是否启用(0为停用 1为使用)
        ///</summary>
        public int IsTrue { get; set; }
        #endregion

        public override string TbName { get { return "ZL_StoreStyleTable"; } }
        /// <summary>
        /// 返回实体列表数组
        /// </summary>
        /// <returns>String[]</returns>
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"ID","Int","4"},
                                  {"StyleName","NVarChar","50"},
                                  {"StylePic","NVarChar","100"},
                                  {"StyleUrl","NVarChar","200"}, 
                                  {"ListStyle","NVarChar","255"},
                                  {"ContentStyle","NVarChar","255"},
                                  {"ModelID","Int","4"}, 
                                  {"IsTrue","Int","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_StoreStyleTable model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.StyleName;
            sp[2].Value = model.StylePic;
            sp[3].Value = model.StyleUrl;
            sp[4].Value = model.ListStyle;
            sp[5].Value = model.ContentStyle;
            sp[6].Value = model.ModelID;
            sp[7].Value = model.IsTrue;
            return sp;
        }

        public M_StoreStyleTable GetModelFromReader(SqlDataReader rdr)
        {
            M_StoreStyleTable model = new M_StoreStyleTable();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.StyleName = ConverToStr(rdr["StyleName"]);
            model.StylePic = ConverToStr(rdr["StylePic"]);
            model.StyleUrl = ConverToStr(rdr["StyleUrl"]);
            model.ListStyle = ConverToStr(rdr["ListStyle"]);
            model.ContentStyle = ConverToStr(rdr["ContentStyle"]);
            model.ModelID = ConvertToInt(rdr["ModelID"]);
            model.IsTrue = ConvertToInt(rdr["IsTrue"]);
            rdr.Close();
            return model;
        }
    }
}
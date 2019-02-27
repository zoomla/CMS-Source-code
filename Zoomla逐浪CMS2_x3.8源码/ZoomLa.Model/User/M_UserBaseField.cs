namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;
    using System.Data.Common;
    [Serializable]
    public class M_UserBaseField:M_Base
    {
        #region 定义字段
        /// <summary>
        /// 
        /// </summary>
        public int FieldID { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段别名
        /// </summary>
        public string FieldAlias { get; set; }
        /// <summary>
        /// 字段说明
        /// </summary>
        public string FieldTips { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 字段允许空，1：允许，2：不允许
        /// </summary>
        public int IsNotNull;
        /// <summary>
        /// 字段类型
        /// </summary>
        public string FieldType { get; set; }
        /// <summary>
        /// 字段内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 列表显示。1：显示，2不显示
        /// </summary>
        public int ShowList { get; set; }
        /// <summary>
        /// 显示列宽度
        /// </summary>
        public int ShowWidth { get; set; }
        /// <summary>
        /// 是否允许修改1:是,0否
        /// </summary>
        public int NoEdit { get; set; }
        public int IsCopy { get; set; }
        #endregion
        public override string TbName { get { return "ZL_UserBaseField"; } }
        public override string PK { get { return "FieldID"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                                  {"FieldID","Int","4"},  
                                  {"FieldName","NVarChar","1000"}, 
                                  {"FieldAlias","NVarChar","1000"},
                                  {"FieldTips","NVarChar","1000"}, 
                                  {"Description","NVarChar","1000"},
                                  {"IsNotNull","Int","4"},
                                  {"FieldType","NVarChar","1000"},
                                  {"Content","NVarChar","4000"},
                                  {"OrderId","Int","4"}, 
                                  {"ShowList","Int","4"}, 
                                  {"ShowWidth","Int","4"}, 
                                  {"NoEdit","Int","4"},
                                  {"IsCopy","Int","4" }
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_UserBaseField model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.FieldID;
            sp[1].Value = model.FieldName;
            sp[2].Value = model.FieldAlias;
            sp[3].Value = model.FieldTips;
            sp[4].Value = model.Description;
            sp[5].Value = model.IsNotNull;
            sp[6].Value = model.FieldType;
            sp[7].Value = model.Content;
            sp[8].Value = model.OrderId;
            sp[9].Value = model.ShowList;
            sp[10].Value = model.ShowWidth;
            sp[11].Value = model.NoEdit;
            sp[12].Value = model.IsCopy;
            return sp;
        }
        public M_UserBaseField GetModelFromReader(DbDataReader rdr)
        {
            M_UserBaseField model = new M_UserBaseField();
            model.FieldID = Convert.ToInt32(rdr["FieldID"]);
            model.FieldName = ConverToStr(rdr["FieldName"]);
            model.FieldAlias = ConverToStr(rdr["FieldAlias"]);
            model.FieldTips = ConverToStr(rdr["FieldTips"]);
            model.Description = ConverToStr(rdr["Description"]);
            model.IsNotNull = ConvertToInt(rdr["IsNotNull"]);
            model.FieldType = ConverToStr(rdr["FieldType"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.OrderId = ConvertToInt(rdr["OrderId"]);
            model.ShowList = ConvertToInt(rdr["ShowList"]);
            model.ShowWidth = ConvertToInt(rdr["ShowWidth"]);
            model.NoEdit = ConvertToInt(rdr["NoEdit"]);
            model.IsCopy = ConvertToInt(rdr["IsCopy"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
        /// <summary>
        /// GetInfoFromDataTable
        /// </summary>
        /// <param name="Rowsinfo">DataTable</param>
        public M_UserBaseField GetInfoFromDataTable(DataTable Rowsinfo)
        {
            M_UserBaseField info = new M_UserBaseField();
            if (Rowsinfo.Rows.Count > 0)
            {
                info.FieldID = Convert.ToInt32(Rowsinfo.Rows[0]["FieldID"]);
                info.FieldName = ConverToStr(Rowsinfo.Rows[0]["FieldName"]);
                info.FieldAlias = ConverToStr(Rowsinfo.Rows[0]["FieldAlias"]);
                info.FieldTips = ConverToStr(Rowsinfo.Rows[0]["FieldTips"]);
                info.Description = ConverToStr(Rowsinfo.Rows[0]["Description"]);
                info.IsNotNull = ConvertToInt(Rowsinfo.Rows[0]["IsNotNull"]);
                info.FieldType = ConverToStr(Rowsinfo.Rows[0]["FieldType"]);
                info.Content = ConverToStr(Rowsinfo.Rows[0]["Content"]);
                info.OrderId = ConvertToInt(Rowsinfo.Rows[0]["OrderId"]);
                info.ShowList = ConvertToInt(Rowsinfo.Rows[0]["ShowList"]);
                info.ShowWidth = ConvertToInt(Rowsinfo.Rows[0]["ShowWidth"]);
                info.NoEdit = ConvertToInt(Rowsinfo.Rows[0]["NoEdit"]);
                info.IsCopy = ConvertToInt(Rowsinfo.Rows[0]["IsCopy"]);
            }
            return info;
        }
    }
}
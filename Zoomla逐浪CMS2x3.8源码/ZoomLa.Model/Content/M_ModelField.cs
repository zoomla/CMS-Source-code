namespace ZoomLa.Model
{
    using System;
    using System.Data.SqlClient;
    using System.Data;
    using System.Data.Common;
    [Serializable]
    public class M_ModelField : M_Base
    {
        public M_ModelField()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //


        }
        public M_ModelField(bool flag)
        {

            this.IsNull = flag;
        }
        public int FieldID { get; set; }
        public bool IsView { get; set; }
        public bool Sys_type { get; set; }
        public int ModelID { get; set; }
        /// <summary>
        /// 字段名,实际调用
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 别名,仅用于显示
        /// </summary>
        public string FieldAlias { get; set; }
        public string FieldTips { get; set; }
        public string Description { get; set; }
        public bool IsNotNull { get; set; }
        public bool IsSearchForm { get; set; }
        public string FieldType { get; set; }
        public string Content { get; set; }
        public int OrderID { get; set; }
        public bool ShowList { get; set; }
        public int ShowWidth { get; set; }
        public bool IsNull { get; private set; }

        public bool IsShow { get; set; }
        /// <summary>
        /// 是否为下载专用字段
        /// </summary>
        public int IsDownField { get; set; }
        /// <summary>
        /// 关联下载服务器
        /// </summary>
        public int DownServerID { get; set; }
        /// <summary>
        /// 回复字段
        /// </summary>
        public int RestoreField { get; set; }
        /// <summary>
        /// 改为是否禁用 0:启用,-1禁用(兼容之前)
        /// </summary>
        public int IsCopy
        {
            get;
            set;
        }
        public int Unfurl { get; set; }
        /// <summary>
        /// 是否批量添加
        /// </summary>
        public bool Islotsize { get; set; }
        /// <summary>
        /// 注册时填写
        /// </summary>
        public int RegShow { get; set; }
        /// <summary>
        /// 是否允许内链，true为是
        /// </summary>
        public bool IsChain { get; set; }
        public override string PK { get { return "FieldID"; } }
        public override string TbName { get { return "ZL_ModelField"; } }
        public static string[,] FieldList2()
        {
            string[,] Tablelist = {
                                  {"FieldID","Int","4"},
                                  {"ModelId","Int","4"},
                                  {"FieldName","NVarChar","50"},
                                  {"FieldAlias","NVarChar","50"},
                                  {"FieldTips","NVarChar","50"},
                                  {"Description","NVarChar","255"},
                                  {"IsNotNull","Bit","4"},
                                  {"IsSearchForm","Bit","4"},
                                  {"FieldType","NVarChar","50"},
                                  {"Content","NText","400"},
                                  {"OrderId","Int","4"},
                                  {"ShowList","Bit","4"},
                                  {"ShowWidth","Int","4"},
                                  {"IsShow","Bit","4"},
                                  {"IsView","Bit","4"},
                                  {"IsDownField","Int","4"},
                                  {"DownServerID","Int","4"},
                                  {"RestoreField","Int","4"},
                                  {"Sys_type","Bit","4"},
                                  {"IsCopy","Int","4"},
                                  {"Islotsize","Bit","4"},
                                  {"IsChain","Bit","4"}
                                 };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_ModelField model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.FieldID;
            sp[1].Value = model.ModelID;
            sp[2].Value = model.FieldName;
            sp[3].Value = model.FieldAlias;
            sp[4].Value = model.FieldTips;
            sp[5].Value = model.Description;
            sp[6].Value = model.IsNotNull;
            sp[7].Value = model.IsSearchForm;
            sp[8].Value = model.FieldType;
            sp[9].Value = model.Content;
            sp[10].Value = model.OrderID;
            sp[11].Value = model.ShowList;
            sp[12].Value = model.ShowWidth;
            sp[13].Value = model.IsShow;
            sp[14].Value = model.IsView;
            sp[15].Value = model.IsDownField;
            sp[16].Value = model.DownServerID;
            sp[17].Value = model.RestoreField;
            sp[18].Value = model.Sys_type;
            sp[19].Value = model.IsCopy;
            sp[20].Value = model.Islotsize;
            sp[21].Value = model.IsChain;
            return sp;
        }
        public M_ModelField GetModelFromReader(DbDataReader rdr)
        {
            M_ModelField model = new M_ModelField();
            model.FieldID = Convert.ToInt32(rdr["FieldID"]);
            model.ModelID = Convert.ToInt32(rdr["ModelId"]);
            model.FieldName = ConverToStr(rdr["FieldName"]);
            model.FieldAlias = ConverToStr(rdr["FieldAlias"]);
            model.FieldTips = ConverToStr(rdr["FieldTips"]);
            model.Description = ConverToStr(rdr["Description"]);
            model.IsNotNull = ConverToBool(rdr["IsNotNull"]);
            model.IsSearchForm = ConverToBool(rdr["IsSearchForm"]);
            model.FieldType = ConverToStr(rdr["FieldType"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.OrderID = ConvertToInt(rdr["OrderId"]);
            model.ShowList = ConverToBool(rdr["ShowList"]);
            model.ShowWidth = ConvertToInt(rdr["ShowWidth"]);
            model.IsShow = ConverToBool(rdr["IsShow"]);
            model.IsView = ConverToBool(rdr["IsView"]);
            model.IsDownField = ConvertToInt(rdr["IsDownField"]);
            model.DownServerID = ConvertToInt(rdr["DownServerID"]);
            model.RestoreField = ConvertToInt(rdr["RestoreField"]);
            model.Sys_type = ConverToBool(rdr["Sys_type"]);
            model.IsCopy = ConvertToInt(rdr["IsCopy"]);
            model.Islotsize = ConverToBool(rdr["Islotsize"]);
            model.IsChain = ConverToBool(rdr["IsChain"]);
            rdr.Close();
            return model;
        }
        public M_ModelField GetModelFromReader(DataRow rdr)
        {
            M_ModelField model = new M_ModelField();
            model.FieldID = Convert.ToInt32(rdr["FieldID"]);
            model.ModelID = Convert.ToInt32(rdr["ModelId"]);
            model.FieldName = ConverToStr(rdr["FieldName"]);
            model.FieldAlias = ConverToStr(rdr["FieldAlias"]);
            model.FieldTips = ConverToStr(rdr["FieldTips"]);
            model.Description = ConverToStr(rdr["Description"]);
            model.IsNotNull = ConverToBool(rdr["IsNotNull"]);
            model.IsSearchForm = ConverToBool(rdr["IsSearchForm"]);
            model.FieldType = ConverToStr(rdr["FieldType"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.OrderID = ConvertToInt(rdr["OrderId"]);
            model.ShowList = ConverToBool(rdr["ShowList"]);
            model.ShowWidth = ConvertToInt(rdr["ShowWidth"]);
            model.IsShow = ConverToBool(rdr["IsShow"]);
            model.IsView = ConverToBool(rdr["IsView"]);
            model.IsDownField = ConvertToInt(rdr["IsDownField"]);
            model.DownServerID = ConvertToInt(rdr["DownServerID"]);
            model.RestoreField = ConvertToInt(rdr["RestoreField"]);
            model.Sys_type = ConverToBool(rdr["Sys_type"]);
            model.IsCopy = ConvertToInt(rdr["IsCopy"]);
            model.Islotsize = ConverToBool(rdr["Islotsize"]);
            model.IsChain = ConverToBool(rdr["IsChain"]);
            return model;
        }
        public override string[,] FieldList()
        {
            return FieldList2();
        }
    }
}
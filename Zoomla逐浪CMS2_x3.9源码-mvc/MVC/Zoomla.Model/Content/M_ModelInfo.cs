namespace ZoomLa.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    [Serializable]
    public class M_ModelInfo : M_Base
    {
        //模型ID
        public int ModelID { get; set; }
        //模型名称
        public string ModelName { get; set; }
        //模型描述
        public string Description { get; set; }
        //模型内容存储表名
        public string TableName { get; set; }
        //项目名称：如文章、新闻
        public string ItemName { get; set; }
        //项目单位：如篇、条
        public string ItemUnit { get; set; }
        //项目图标
        public string ItemIcon { get; set; }
        /// <summary>
        /// 1:内容模型,2:商城模型,3:用户模型(人才招聘等非注册字段),4:黄页模型,5:店铺模型,6:店铺申请,7:互动,8,9:用户注册字段,10:OA流程模型
        /// </summary>
        public int ModelType { get; set; }
        /// <summary>
        /// 内容模板
        /// </summary>
        public string ContentModule { get; set; }
        /// <summary>
        /// 是否多条记录，只对用户模型有效true时允许一个用户输入多条此模型信息
        /// </summary>
        public bool MultiFlag { get; set; }
        public bool IsNull { get; set; }
        public int NodeID { get; set; }
        /// <summary>
        /// 识别系统模型字段，1：系统生成模型，2：用户自定义模型
        /// </summary>
        public int SysModel { get; set; }
        /// <summary>
        /// 复制来源模型ID
        /// </summary>
        public int FromModel { get; set; }
        public string Thumbnail { get; set; }
        public bool Islotsize { get; set; }
        public List<M_ModelField> m_ModelInfo = new List<M_ModelField>();
        public List<M_ModelField> ModelField
        {
            get { return this.m_ModelInfo; }
            set { this.m_ModelInfo = value; }
        }
        public M_ModelInfo()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public M_ModelInfo(bool s)
        {
            this.IsNull = s;
        }
        public override string PK { get { return "ModelID"; } }
        public override string TbName { get { return "ZL_Model"; } }
        public static string[,] FieldList2()
        {
            string[,] Tablelist = {
                                  {"ModelID","Int","4"},
                                  {"ModelName","NVarChar","50"},
                                  {"Description","NVarChar","1000"},
                                  {"TableName","NVarChar","255"},
                                  {"ItemName","NVarChar","255"},
                                  {"ItemUnit","NVarChar","255"},
                                  {"ItemIcon","NVarChar","255"},
                                  {"ContentTemplate","NVarChar","255"},
                                  {"ModelType","Int","4"},
                                  {"MultiFlag","Int","4"},
                                  {"NodeID","Int","4"},
                                  {"SysModel","Int","4"},
                                  {"FromModel","Int","4"},
                                  {"Thumbnail","NVarChar","500"},
                                  {"Islotsize","Int","4"}
                                 };
            return Tablelist;
        }
        public override string[,] FieldList()
        {
            return FieldList2();
        }
        public override SqlParameter[] GetParameters()
        {
            M_ModelInfo model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ModelID;
            sp[1].Value = model.ModelName;
            sp[2].Value = model.Description;
            sp[3].Value = model.TableName;
            sp[4].Value = model.ItemName;
            sp[5].Value = model.ItemUnit;
            sp[6].Value = model.ItemIcon;
            sp[7].Value = model.ContentModule;
            sp[8].Value = model.ModelType;
            sp[9].Value = model.MultiFlag;
            sp[10].Value = model.NodeID;
            sp[11].Value = model.SysModel;
            sp[12].Value = model.FromModel;
            sp[13].Value = model.Thumbnail;
            sp[14].Value = model.Islotsize;
            return sp;
        }
        public M_ModelInfo GetModelFromReader(DbDataReader rdr)
        {
            M_ModelInfo model = new M_ModelInfo();
            model.ModelID = ConvertToInt(rdr["ModelID"]);
            model.ModelName = ConverToStr(rdr["ModelName"]);
            model.Description = ConverToStr(rdr["Description"]);
            model.TableName = ConverToStr(rdr["TableName"]);
            model.ItemName = ConverToStr(rdr["ItemName"]);
            model.ItemUnit = ConverToStr(rdr["ItemUnit"]);
            model.ItemIcon = ConverToStr(rdr["ItemIcon"]);
            model.ModelType = ConvertToInt(rdr["ModelType"]);
            model.ContentModule = ConverToStr(rdr["ContentTemplate"]);
            model.MultiFlag = ConverToBool(rdr["MultiFlag"]);
            model.NodeID = ConvertToInt(rdr["NodeID"]);
            model.SysModel = ConvertToInt(rdr["SysModel"]);
            model.FromModel = ConvertToInt(rdr["FromModel"]);
            model.Thumbnail = ConverToStr(rdr["Thumbnail"]);
            model.Islotsize = ConverToBool(rdr["Islotsize"]);
            rdr.Close();
            return model;
        }
        public M_ModelInfo GetModelFromReader(DataRow rdr)
        {
            M_ModelInfo model = new M_ModelInfo();
            model.ModelID = ConvertToInt(rdr["ModelID"]);
            model.ModelName = ConverToStr(rdr["ModelName"]);
            model.Description = ConverToStr(rdr["Description"]);
            model.TableName = ConverToStr(rdr["TableName"]);
            model.ItemName = ConverToStr(rdr["ItemName"]);
            model.ItemUnit = ConverToStr(rdr["ItemUnit"]);
            model.ItemIcon = ConverToStr(rdr["ItemIcon"]);
            model.ModelType = ConvertToInt(rdr["ModelType"]);
            model.ContentModule = ConverToStr(rdr["ContentTemplate"]);
            model.MultiFlag = ConverToBool(rdr["MultiFlag"]);
            model.NodeID = ConvertToInt(rdr["NodeID"]);
            model.SysModel = ConvertToInt(rdr["SysModel"]);
            model.FromModel = ConvertToInt(rdr["FromModel"]);
            model.Thumbnail = ConverToStr(rdr["Thumbnail"]);
            model.Islotsize = ConverToBool(rdr["Islotsize"]);
            return model;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    [Serializable]
    //ZL_OrderBaseField
    public class M_OrderBaseField : M_Base
    {

        /// <summary>
        /// FieldID
        /// </summary>	
        public int FieldID { get; set; }
        /// <summary>
        /// FieldName
        /// </summary>	
        public string FieldName { get; set; }
        /// <summary>
        /// FieldAlias
        /// </summary>	
        public string FieldAlias { get; set; }
        /// <summary>
        /// FieldTips
        /// </summary>	
        public string FieldTips { get; set; }
        /// <summary>
        /// Description
        /// </summary>	
        public string Description { get; set; }
        /// <summary>
        /// IsNotNull
        /// </summary>	
        public bool IsNotNull { get; set; }
        /// <summary>
        /// FieldType
        /// </summary>	
        public string FieldType { get; set; }
        /// <summary>
        /// Content
        /// </summary>	
        public string Content { get; set; }
        /// <summary>
        /// OrderId
        /// </summary>	
        public int OrderId { get; set; }
        /// <summary>
        /// ShowList
        /// </summary>	
        public bool ShowList { get; set; }
        /// <summary>
        /// ShowWidth
        /// </summary>	
        public int ShowWidth { get; set; }
        /// <summary>
        /// NoEdit
        /// </summary>	
        public int NoEdit { get; set; }
        /// <summary>
        /// Type
        /// </summary>	
        public int Type { get; set; }
        public M_OrderBaseField()
        {

        }
        public override string PK { get { return "FieldID"; } }
        public override string TbName { get { return "ZL_OrderBaseField"; } }
        public override string[,] FieldList()
        {
            string[,] Tablelist = {
                        {"FieldName","NVarChar","50"},
                        {"FieldAlias","NVarChar","50"},
                        {"FieldTips","NVarChar","50"},
                        {"Description","NVarChar","255"},
                        {"IsNotNull","Bit","1"},
                        {"FieldType","NVarChar","50"},
                        {"Content","NText","16"},
                        {"OrderId","Int","4"},
                        {"ShowList","Bit","1"},
                        {"ShowWidth","Int","4"},
                        {"NoEdit","Int","4"},
                        {"Type","Int","4"}

        };
            return Tablelist;
        }
        public string GetFieldAndPara()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "]=@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
        }
        public override SqlParameter[] GetParameters()
        {
            M_OrderBaseField model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.FieldName;
            sp[1].Value = model.FieldAlias;
            sp[2].Value = model.FieldTips;
            sp[3].Value = model.Description;
            sp[4].Value = model.IsNotNull;
            sp[5].Value = model.FieldType;
            sp[6].Value = model.Content;
            sp[7].Value = model.OrderId;
            sp[8].Value = model.ShowList;
            sp[9].Value = model.ShowWidth;
            sp[10].Value = model.NoEdit;
            sp[11].Value = model.Type;
            return sp;
        }
        public M_OrderBaseField GetModelFromReader(SqlDataReader rdr)
        {
            M_OrderBaseField model = new M_OrderBaseField();
            model.FieldID = Convert.ToInt32(rdr["FieldID"]);
            model.FieldName = ConverToStr(rdr["FieldName"]);
            model.FieldAlias = ConverToStr(rdr["FieldAlias"]);
            model.FieldTips = ConverToStr(rdr["FieldTips"]);
            model.Description = ConverToStr(rdr["Description"]);
            model.IsNotNull = ConverToBool(rdr["IsNotNull"]);
            model.FieldType = ConverToStr(rdr["FieldType"]);
            model.Content = ConverToStr(rdr["Content"]);
            model.OrderId = ConvertToInt(rdr["OrderId"]);
            model.ShowList = ConverToBool(rdr["ShowList"]);
            model.ShowWidth = ConvertToInt(rdr["ShowWidth"]);
            model.NoEdit = ConvertToInt(rdr["NoEdit"]);
            model.Type = ConvertToInt(rdr["Type"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZoomLa.Model
{
    [Serializable]
    //ZL_Mis_Model
    public class M_Mis_Model:M_Base
    {

        /// <summary>
        /// 模型ID
        /// </summary>	
        public int ID { get; set; }
        /// <summary>
        /// 模型名称
        /// </summary>	
        public string ModelName { get; set; }
        /// <summary>
        /// 模型内容
        /// </summary>	
        public string ModelContent { get; set; }
        /// <summary>
        /// 生成时间
        /// </summary>	
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 绑定节点,支持绑定多个节点
        /// </summary>
        public string BindNode { get; set; }
        public int DocType { get; set; }
        /// <summary>
        /// 套红Word路径
        /// </summary>
        public string WordPath { get; set; }
        public M_Mis_Model()
        {

        }
        public override string TbName { get { return "ZL_Mis_Model"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        	            {"ID","Int","4"},            
                        {"ModelName","NVarChar","255"},            
                        {"ModelContent","NText","20000"},            
                        {"CreateTime","DateTime","8"},            
                        {"BindNode","NVarChar","200"},
                        {"DocType","Int","4"},
                        {"WordPath","NVarChar","300"}
        };
            return Tablelist;
        }
        public string GetFields()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "[" + strArr[i, 0] + "],";
                }
            }
            return str.Substring(0, str.Length - 1);
        }
        public string GetParas()
        {
            string str = string.Empty;
            string[,] strArr = FieldList();
            for (int i = 0; i < strArr.GetLength(0); i++)
            {
                if (strArr[i, 0] != PK)
                {
                    str += "@" + strArr[i, 0] + ",";
                }
            }
            return str.Substring(0, str.Length - 1);
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
            M_Mis_Model model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.ModelName;
            sp[2].Value = model.ModelContent;
            sp[3].Value = model.CreateTime;
            sp[4].Value = model.BindNode;
            sp[5].Value = model.DocType;
            sp[6].Value = model.WordPath;
            return sp;
        }
        public M_Mis_Model GetModelFromReader(SqlDataReader rdr)
        {
            M_Mis_Model model = new M_Mis_Model();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.ModelName = ConverToStr(rdr["ModelName"]);
            model.ModelContent = ConverToStr(rdr["ModelContent"]);
            model.CreateTime = ConvertToDate(rdr["CreateTime"]);
            model.BindNode = ConverToStr(rdr["BindNode"]);
            model.DocType = ConvertToInt(rdr["DocType"]);
            model.WordPath = ConverToStr(rdr["WordPath"]);
            rdr.Close();
            return model;
        }
    }
}
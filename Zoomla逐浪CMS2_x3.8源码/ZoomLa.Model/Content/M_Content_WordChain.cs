using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ZoomLa.Model
{
    [Serializable]
    //ZL_Content_WordChain
    public class M_Content_WordChain:M_Base
    {

        /// <summary>
        /// ID
        /// </summary>	
        public int ID { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>	
        public string KeyWord { get; set; }
        /// <summary>
        /// 要替换的值
        /// </summary>	
        public string KeyValue { get; set; }
        /// <summary>
        /// 权值，越大优先替换
        /// </summary>	
        public int Priority { get; set; }
        /// <summary>
        /// 正则，扩展功能
        /// </summary>	
        public string Regex { get; set; }
        /// <summary>
        /// RegexValue
        /// </summary>	
        public string RegexValue { get; set; }
        public string Target{
            get
            {
                if (!string.IsNullOrEmpty(KeyValue))
                    return GetValue(KeyValue, "target=\"", "\"");
                else
                    return "";
            }
        }
        public string Href
        {
            get
            {
                if (!string.IsNullOrEmpty(KeyValue))
                    return GetValue(KeyValue, "href=\"", "\"");
                else return "";
            }
        }
        public M_Content_WordChain()
        {

        }     
        public override string TbName { get { return "ZL_Content_WordChain"; } }

        public override string[,] FieldList()
        {
            string[,] Tablelist = {
        	            {"ID","Int","4"},            
                        {"KeyWord","NVarChar","255"},            
                        {"KeyValue","NVarChar","255"},            
                        {"Priority","Int","4"},            
                        {"Regex","NVarChar","255"},            
                        {"RegexValue","NVarChar","255"}            
              
        };
            return Tablelist;
        }
        public override SqlParameter[] GetParameters()
        {
            M_Content_WordChain model = this;
            SqlParameter[] sp = GetSP();
            sp[0].Value = model.ID;
            sp[1].Value = model.KeyWord;
            sp[2].Value = model.KeyValue;
            sp[3].Value = model.Priority;
            sp[4].Value = model.Regex;
            sp[5].Value = model.RegexValue;
            return sp;
        }
        public M_Content_WordChain GetModelFromReader(SqlDataReader rdr)
        {
            M_Content_WordChain model = new M_Content_WordChain();
            model.ID = Convert.ToInt32(rdr["ID"]);
            model.KeyWord = ConverToStr(rdr["KeyWord"]);
            model.KeyValue = ConverToStr(rdr["KeyValue"]);
            model.Priority = ConvertToInt(rdr["Priority"]);
            model.Regex = ConverToStr(rdr["Regex"]);
            model.RegexValue = ConverToStr(rdr["RegexValue"]);
            rdr.Close();
            rdr.Dispose();
            return model;
        }
        public static string GetValue(string str, string s, string e)
        {
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }
    }
}
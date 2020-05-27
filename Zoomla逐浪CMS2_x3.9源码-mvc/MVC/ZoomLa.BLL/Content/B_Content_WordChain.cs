using System;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL
{
    public class B_Content_WordChain
    {
        public M_Content_WordChain model = new M_Content_WordChain();
        public string strTableName = "";
        public string PK = "";
        public B_Content_WordChain()
        {
            strTableName = model.TbName;
            PK = model.PK;
        }
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public DataTable Sel(int ID)
        {
            return Sql.Sel(strTableName, PK, ID);
        }

        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        public M_Content_WordChain SelReturnModel(int ID)
        {
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, PK, ID))
            {
                if (reader.Read())
                {
                    return model.GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public DataTable Sel()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize, int id = 0)
        {
            string where = " 1=1";
            if (id > 0) { where += " AND " + PK + "=" + id; }
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, where);
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable SelByPriority()
        {
            string strSql = "Select * from " + strTableName + " Order by Priority DESC";
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public bool Del(int ID)
        {
            return Sql.Del(strTableName, ID);
        }
        /// <summary>
        /// 插入数据返回新ID
        /// </summary>
        public int insert(M_Content_WordChain model)
        {
            return Sql.insertID(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        public bool UpdateByID(M_Content_WordChain model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        /// <summary>
        /// 替换关键词
        /// </summary>
        public string RePlaceKeyWord(string content)
        {
            //用占位符，防止keyvalue里包含keyword
            List<string> strList = new List<string>();
            DataTable keyDT = SelByPriority();
            for (int i = 0; i < keyDT.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(keyDT.Rows[i]["Regex"].ToString()))
                {
                    if (!string.IsNullOrEmpty(keyDT.Rows[i]["KeyValue"].ToString()))//用于修改
                        content = content.Replace(keyDT.Rows[i]["KeyValue"].ToString(), "{" + i + "}");
                    content = content.Replace(keyDT.Rows[i]["KeyWord"].ToString(), "{" + i + "}");//{0},{1}
                    strList.Add(keyDT.Rows[i]["KeyValue"].ToString());
                }
                else
                {
                    content = Regex.Replace(content, keyDT.Rows[i]["Regex"].ToString(), keyDT.Rows[i]["RegexValue"].ToString());
                }
            }
            content = string.Format(content, strList.ToArray());
            return content;
        }
        public string ReturnBack(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                List<string> strList = new List<string>();
                DataTable keyDT = SelByPriority();
                for (int i = 0; i < keyDT.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(keyDT.Rows[i]["Regex"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(keyDT.Rows[i]["KeyValue"].ToString()))//用于修改
                            content = content.Replace(keyDT.Rows[i]["KeyValue"].ToString(), "{" + i + "}");
                        strList.Add(keyDT.Rows[i]["KeyWord"].ToString());
                    }
                }
                content = string.Format(content, strList.ToArray());
            }
            return content;
        }
    }
}

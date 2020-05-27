using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
/*
 * 下载字段解析
 */ 
namespace ZoomLa.BLL.HtmlLabel
{
    public class DownField
    {
        B_Content conBll = new B_Content();
        public List<M_Field_Down> list = new List<M_Field_Down>();
        public List<M_Field_Down> GetListByGid(int gid, string field)
        {
            DataTable dt = new DataTable();
            dt = conBll.GetContentByItems(gid);
            if (dt.Rows.Count < 1) { return null; }
            string json = dt.Rows[0][field].ToString();
            return JsonConvert.DeserializeObject<List<M_Field_Down>>(json);
        }
        //将数据更新回去
        public void UpdateByList(string tname, string field, int id)
        {
            string json = JsonConvert.SerializeObject(list);
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("json", json) };
            string sql = string.Format("UPDATE {0} SET {1}=@json WHERE ID=" + id, tname, field);
            SqlHelper.ExecuteSql(sql, sp);
        }
        //将前台的字符串转为链接，仅用于新建文档时,以|分隔
        public string ConverStrToJson(string str)
        {
            string[] arr = str.Split('|');
            if (arr.Length < 1) return "";
            for (int i = 0; i < arr.Length; i++)
            {
                M_Field_Down model = new M_Field_Down();
                model.url = arr[i];
                model.fname = Path.GetFileName(model.url);
                model.ranstr = function.GetRandomString(10);
                list.Add(model);
            }
            return JsonConvert.SerializeObject(list);
        }
        //根据身份字符串，返回模型
        public M_Field_Down GetModel(string json, string ranstr)
        {
            if (string.IsNullOrEmpty(json) || string.IsNullOrEmpty(ranstr)) return null;
            list = JsonConvert.DeserializeObject<List<M_Field_Down>>(json);

            foreach (M_Field_Down model in list)
            {
                if (model.ranstr.Equals(ranstr))
                {
                    return model;
                }
            }
            return null;
        }
    }
    public class M_Field_Down
    {
        //显示名
        public string fname = "";
        public string url = "";
        public string ranstr = "";//用于下载和定位的十位字符串
        public int count = 0;//下载数
        public string ptype = "sicon";
        public int price;
        public int hour;
    }
}

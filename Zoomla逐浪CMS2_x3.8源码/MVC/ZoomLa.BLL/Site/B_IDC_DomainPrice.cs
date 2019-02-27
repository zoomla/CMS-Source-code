using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.Site
{
    public class B_IDC_DomainPrice
    {
        public string strTableName,PK;
        public M_IDC_DomPrice model = new M_IDC_DomPrice();
        private string[] suffix = ".com,.net,.cn,.com.cn,.net.cn,.org.cn,.org,.asia,.cc,.biz,.info,.tv,.tw,.in,.me,.pw,.jl.cn,.sh.cn,.qh.cn,.gx.cn,.ah.cn,.sx.cn,.hk.cn,.fj.cn,.xz.cn,.hb.cn,.hl.cn,.tj.cn,.nx.cn,.hi.cn,.jx.cn,.nm.cn,.mo.cn,.ac.cn,.sn.cn,.hn.cn,.js.cn,.cq.cn,.xj.cn,.sc.cn,.sd.cn,.ln.cn,.yn.cn,.bj.cn,.gs.cn,.gd.cn,.zj.cn,.he.cn,.tw.cn,.gz.cn,.ha.cn,.中国,.公司,.网络".Split(',');
        public B_IDC_DomainPrice() 
        {
            strTableName = model.TbName;
            PK = model.PK;
        }
        //-----------------Retrieve
        public DataTable Sel()
        {
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
        public DataTable Sel(string domName) 
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("domName","%"+domName+"%")
            };
            return SqlHelper.ExecuteTable(CommandType.Text, "Select * From " + strTableName+" Where DomName Like @domName",sp);
        }
        public M_IDC_DomPrice SelByID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] {new SqlParameter("@id",id) };
            using (SqlDataReader reader = Sql.SelReturnReader(strTableName, " Where ID = @id",sp))
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
        /// 从表中获取指定数据,优先匹配全名，无则匹配后缀名,域名需无www.
        /// </summary>
        public string GetPrice(string domName,DataTable dt) 
        {
            if (dt == null) { dt = Sel(); }
            string result = "";
            dt.DefaultView.RowFilter = "DomName = '"+domName+"'";
            if (dt.DefaultView.ToTable().Rows.Count > 0)//无域名匹配，则匹配后缀名
            {
                result = dt.DefaultView.ToTable().Rows[0]["DomPrice"].ToString();
            }
            else
            {
               string suffix="."+domName.Split('.')[domName.Split('.').Length - 1];
               dt.DefaultView.RowFilter = "DomName = '" + suffix + "'";
               if (dt.DefaultView.ToTable().Rows.Count > 0)
               {
                   result = dt.DefaultView.ToTable().Rows[0]["DomPrice"].ToString();
               }
            }
            return result;
        }
        //-----------------Insert
        /// <summary>
        /// 如有同名返回-1,否则返回被插入ID;
        /// </summary>
        public int Insert(M_IDC_DomPrice model)
        {
            if (CheckIsExist(model.DomName) != null)
            {
                return -1;
            }
            return Sql.insert(strTableName, model.GetParameters(), BLLCommon.GetParas(model), BLLCommon.GetFields(model));
        }
        //-----------------Update
        public bool UpdateByID(M_IDC_DomPrice model)
        {
            return Sql.UpdateByIDs(strTableName, PK, model.ID.ToString(), BLLCommon.GetFieldAndPara(model), model.GetParameters());
        }
        public void UpdatePriceByID(string id,string domPrice) 
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("domPrice",domPrice),
                new SqlParameter("id",id)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, "Update " + strTableName + " Set DomPrice=@domPrice Where ID=@id", sp);
        }

        //------------Tool
        /// <summary>
        /// 移去www.,适用于单个,避免www.123123www.com这样的也被删
        /// </summary>
        public string RemoveWWW(string domName) 
        {
            domName = domName.Replace(" ", "");
            if (domName.IndexOf("www.")==0)
            {
                domName = domName.Remove(0,4);
            }
            return domName;
        }
        /// <summary>
        /// 移除www.,用于IDC
        /// </summary>
        public static string RemoveW(string domName)
        {
            domName = domName.Replace(" ", "");
            if (domName.IndexOf("www.") == 0)
            {
                domName = domName.Remove(0, 4);
            }
            return domName;
        }
        /// <summary>
        /// 是否有同名的域名存在,为空不存在
        /// </summary>
        public object CheckIsExist(string domName) 
        {
            SqlParameter[] sp = new SqlParameter[] 
            {
                new SqlParameter("DomName",domName)
            };
            return SqlHelper.ExecuteScalar(CommandType.Text,"Select * From "+strTableName+" Where DomName = @DomName",sp);
        }
        /// <summary>
        /// 修改检测是否有同名的域名存在,为空不存在
        /// </summary>
        public object CheckIsExist1(string domName,string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("domName", domName), new SqlParameter("id", id) };
            return SqlHelper.ExecuteScalar(CommandType.Text, "Select * From " + strTableName + " Where DomName = @DomName And id!=@id", sp);
        }
        public bool DelByID(string id)
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("id", id) };
            return SqlHelper.ExecuteSql("Delete From " + strTableName + " Where ID = @id", sp);
        }
        //选取未写入数据库的后缀名,用于下拉列表选择定价
        public DataTable GetNotDefindSuffix() 
        {
            DataTable suffixDT = new DataTable();
            suffixDT.Columns.Add("Index",typeof(int));
            suffixDT.Columns.Add("DomName", typeof(string));
            for (int i = 0; i < suffix.Length; i++)
            {
               DataRow dr= suffixDT.NewRow();
               dr["DomName"] = suffix[i];
               suffixDT.Rows.Add(dr);
            }
            string d = GetSuffixFromDB();//已添加了价格的域名
            if (!string.IsNullOrEmpty(d))
            suffixDT.DefaultView.RowFilter = "DomName not in(" + GetSuffixFromDB()+ ")";
            return suffixDT.DefaultView.ToTable();
        }
        //选取数据库中已定义的后缀名,格式'.com','.cn','.net'
        private string GetSuffixFromDB() 
        {
            string result = "";
           DataTable dt= SqlHelper.ExecuteTable(CommandType.Text, "Select DomName From " + strTableName);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               result += "'" + dt.Rows[i]["DomName"].ToString() + "',";
           }
           result = result.TrimEnd(',');
           return result;
        }
    }
}

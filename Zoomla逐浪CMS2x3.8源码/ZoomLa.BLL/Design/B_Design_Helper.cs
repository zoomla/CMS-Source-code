using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using ZoomLa.BLL.CreateJS;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Design
{
    //用于新建站点,备份,私有化布署
    public class B_Design_Helper
    {
        /// <summary>
        /// 返回节点,内容,内容附表数据
        /// </summary>
        public DataSet PackSiteToDS(int siteID)
        {
            B_Design_Node nodeBll = new B_Design_Node();
            DataSet ds = new DataSet();
            string nids = nodeBll.SelToIDS(siteID, "all");
            DataTable nodeDT = nodeBll.SelBy(siteID);
            DataTable conDT = DBCenter.Sel("ZL_CommonModel", "NodeID IN(" + nids + ")");
            DataTable artDT = DBCenter.Sel("ZL_C_Article", "ID IN (SELECT ItemID FROM ZL_CommonModel WHERE NodeID IN (" + nids + "))");
            DataTable sfDT = DBCenter.Sel("ZL_Design_SiteInfo", "ID=" + siteID);
            nodeDT.TableName = "ZL_Node";
            conDT.TableName = "ZL_CommonModel";
            artDT.TableName = "ZL_C_Article";
            sfDT.TableName = "ZL_Design_SiteInfo";
            ds.Tables.Add(nodeDT.Copy());
            ds.Tables.Add(conDT.Copy());
            ds.Tables.Add(artDT.Copy());
            ds.Tables.Add(sfDT.Copy());
            return ds;
        }
        /// <summary>
        /// 交信息导入数据库,将绑定好其之间的对应关系
        /// 需要在外部完成赋值等操作
        /// </summary>
        /// <param name="nodeDT">节点表</param>
        /// <param name="conDT">内容表</param>
        /// <param name="artDT">附加内容表(后期扩展为支持多个)</param>
        public bool ImportContentFromDT(DataTable nodeDT, DataTable conDT, DataTable artDT)
        {
            if (string.IsNullOrEmpty(nodeDT.TableName) || nodeDT.TableName.Equals("Result")) { nodeDT.TableName = "ZL_Node"; }
            if (string.IsNullOrEmpty(conDT.TableName) || conDT.TableName.Equals("Result")) { conDT.TableName = "ZL_CommonModel"; }
            if (string.IsNullOrEmpty(artDT.TableName) || artDT.TableName.Equals("Result")) { artDT.TableName = "ZL_C_Article"; }
            B_CodeModel nodeBll = new B_CodeModel(nodeDT.TableName);
            B_CodeModel conBll = new B_CodeModel(conDT.TableName);
            B_CodeModel artBll = new B_CodeModel(artDT.TableName);
            for (int i = 0; i < nodeDT.Rows.Count; i++)
            {
                DataRow dr = nodeDT.Rows[i];
                dr["CDate"] = DateTime.Now;
                dr["EditDate"] = DateTime.Now;
                int oldnid = Convert.ToInt32(dr["NodeID"]);
                int newnid = nodeBll.Insert(dr, "NodeID");
                //将文章添加入该节点下(根目录下不放文章,在新建的站点的时候必须规范)
                DataRow[] cons = conDT.Select("NodeID='" + oldnid + "'");
                for (int j = 0; j < cons.Length; j++)
                {
                    cons[j]["NodeID"] = newnid;
                }
            }
            //-------------将内容与附表导入(内容与文章的数据可不删,只要关联好ItemID与NodeID即可)
            if (artDT.Columns.Contains("ppics")) { artDT.Columns.Remove("ppics"); }
            if (artDT.Columns.Contains("tpic")) { artDT.Columns.Remove("tpic"); }
            for (int i = 0; i < conDT.Rows.Count; i++)
            {
                DataRow dr = conDT.Rows[i];
                dr["CreateTime"] = DateTime.Now;
                dr["UPDateTime"] = DateTime.Now;
                int itemID = Convert.ToInt32(dr["ItemID"]);
                if (artDT.Select("ID='" + itemID + "'").Length > 0)
                {
                    //其中可能包含没有的字段,需要一个方法,将其导入(根据站站迁移扩展)
                    DataRow artdr = artDT.Select("ID='" + itemID + "'")[0];
                    itemID = artBll.Insert(artdr);
                }
                else { itemID = 0; }
                dr["ItemID"] = itemID;
                conBll.Insert(dr, "GeneralID");
            }
            return true;
        }
        //----------------------用于运行
        /// <summary>
        /// 获取路径,为空则取首页
        /// </summary>
        public static M_Design_Page SelByPath(string path = "")
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            string where = "ZType=0 ";
            if (string.IsNullOrEmpty(path))
            {
                where += " AND (Path='' OR Path='/index' OR Path='/')";
            }
            else
            {
                path = "/" + (path.TrimStart('/').Replace(" ", ""));
                sp.Add(new SqlParameter("path", path));
                where += " AND Path=@path";
            }
            using (DbDataReader reader = DBCenter.SelReturnReader("ZL_Design_Page", where, "", sp))
            {
                if (reader.Read())
                {
                    return new M_Design_Page().GetModelFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        private static XmlDocument pcbkXml = new XmlDocument();
        public static string GetPCViewBK()
        {
            string result = "";//配合前端img检测使用
            try
            {
                if (pcbkXml.ChildNodes.Count < 1) { pcbkXml.Load("http://code.z01.com/web/h5/BackGround.xml"); }
                Random ra = new Random();
                int index = ra.Next(pcbkXml.DocumentElement.ChildNodes.Count);
                result = pcbkXml.DocumentElement.ChildNodes[index].Attributes["Path"].Value;
            }
            catch { result = "/UploadFiles/bg_pcview.jpg"; }
            return result;
        }
        //-----------------------------用户块
        public static string H5_AccessPwd { get { return DataConvert.CStr(HttpContext.Current.Session["H5_AccessPwd"]); } set { HttpContext.Current.Session["H5_AccessPwd"] = value; } }
        public static bool Se_CheckAccessPwd(M_Design_Page pageMod, M_UserInfo mu)
        {
             //是否需要输入密码,创建者访问不需要验证密码
            if (!string.IsNullOrEmpty(pageMod.AccessPwd) && !H5_AccessPwd.Contains("," + pageMod.ID + ","))//&&mu.UserID!=pageMod.UserID
            {
                return false;
            }
            return true;
        }
    }
}

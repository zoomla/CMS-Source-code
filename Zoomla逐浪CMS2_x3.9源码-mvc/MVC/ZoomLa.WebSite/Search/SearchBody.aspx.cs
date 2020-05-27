using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Search
{
    public partial class SearchBody : System.Web.UI.Page
    {
        B_Product pll = new B_Product();
        B_Content bll = new B_Content();
        B_Guest_Bar barBll = new B_Guest_Bar();
        DataTableHelper dtHelper = new DataTableHelper();
        public int count = 0, pageCount = 1, PageSize = 20;
        public string KeyWord { get { return HttpUtility.UrlDecode(Request.QueryString["KeyWord"] ?? ""); } }
        public int CPage
        {
            get
            {
                return PageCommon.GetCPage();
            }
        }
        public int NodeID { get { return DataConvert.CLng(Request.QueryString["node"]); } }
        public int Order { get { return DataConvert.CLng(Request.QueryString["order"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            if (string.IsNullOrEmpty(KeyWord) || KeyWord.Equals("请输入关键字"))
            {
                nonemsg.Visible = true;
                return;
            }
            //DataTable dt = GetDT();
            //count = dt.Rows.Count;
            //dt = dtHelper.PageDT(dt, PageSize, CPage);
            DataTable dt = SelPage(out count);
            if (count > 0)
            {
                this.PShow.Visible = false;
                string hrefTlp = "<a href='javascript:;' onclick='LoadByAjax(\"@query\",@page);' title=''>@text</a>";
                RPT.DataSource = dt;
                RPT.DataBind();
                Page_Lit.Text = PageCommon.CreatePageHtml(PageHelper.GetPageCount(count, PageSize), CPage, 10, hrefTlp);
            }
            else
            {
                this.PShow.Visible = true;
            }
        }
        // 跳转页面地址
        public string GetUrl()
        {
            string itemid = Eval("ID").ToString();
            if (!string.IsNullOrEmpty(Eval("PageUrl").ToString()))//如为子站数据,则直接返回链接
            {
                return Eval("PageUrl").ToString();
            }
            else if (!string.IsNullOrEmpty(Eval("HtmlLink").ToString()))//有静态链接
            {
                return SiteConfig.SiteInfo.SiteUrl.TrimEnd('/') + Eval("HtmlLink");
            }
            else
            {
                string url = string.Empty;
                string tbname = Eval("TableName").ToString();
                if (tbname.Contains("ZL_C_"))
                {
                    url = "/Item/" + itemid + ".aspx";
                }
                else if (tbname.Contains("ZL_P_"))
                {
                    DataTable protable = pll.SelectProByCmdID(DataConverter.CLng(itemid));
                    if (protable != null && protable.Rows.Count > 0)//当内容表与商品表同步时，取商品地址
                        url = "/Shop/" + protable.Rows[0]["ID"] + ".aspx";
                    else//否则取内容地址
                        url = "/Item/" + itemid + ".aspx";
                }
                return url;
            }
        }
        public string ToRed(string de)
        {
            return Regex.Replace(de, Regex.Escape(KeyWord), "<span style='color:red;'>" + KeyWord + "</span>", RegexOptions.IgnoreCase);
        }
        //站点整合数据读取
        public DataTable GetDTFromXml()
        {
            DataTable dt = new DataTable();
            string xmlPath = Server.MapPath("/Config/SitesContent.config");
            if (File.Exists(xmlPath))//子站数据
            {
                DataTable siteDT = dtHelper.DeserializeDataTable(xmlPath, true);
                if (siteDT != null && siteDT.Rows.Count > 0)
                {
                    dt = siteDT;
                }
            }
            return dt;
        }
        public DataTable GetDT()
        {
            string filter = "1=1";
            List<SqlParameter> sp = new List<SqlParameter>() { new SqlParameter("key", "%" + KeyWord + "%") };
            filter += " AND Title Like @key";
            filter = filter + " And (Tablename like 'ZL_P_%' OR Tablename like 'ZL_U_%' OR Tablename like 'ZL_C_%' ) And status=99";
            if (NodeID > 0)
            {
                filter = filter + " And NodeID=" + NodeID;
            }
            //-----整合数据
            DataTable dt = bll.ContentSearch(filter, sp);//主站数据
            dt.Columns.Add(new DataColumn("PageUrl", typeof(string)));
            string sortstr = "";
            switch (Order)
            {
                case 0:
                    sortstr = "CreateTime DESC";
                    break;
                case 1:
                    sortstr = "Hits DESC";
                    break;
            }
            dt.DefaultView.Sort = sortstr;
            dt = dt.DefaultView.ToTable();
            return dt;
        }
        public DataTable SelPage(out int itemCount)
        {
            //*节点ID可能有重复,解决:加一个来源参数,S=0:内容表,1:贴吧
            //ZL_CommonModel status=99,表名ZL_C_,或ZL_P,或ZL_S
            string where = " Title Like @key ";
            string order = "";
            if (NodeID > 0)
            {
                where += " AND NodeID=" + NodeID;
            }
            switch (Order)
            {
                case 0:
                    order = "CreateTime DESC";
                    break;
                case 1:
                    order = "Hits DESC";
                    break;
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("key", "%" + KeyWord + "%") };
            return DBCenter.SelPage(PageSize, CPage, out itemCount, "ID", "*", "ZL_SearchView", where, order, sp);
        }
    }
}
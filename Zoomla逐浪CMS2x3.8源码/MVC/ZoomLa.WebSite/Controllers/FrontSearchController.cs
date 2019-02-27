using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Controllers
{
    public class FrontSearchController : Controller
    {
        B_Node nodeBll = new B_Node();
        B_Content conBll = new B_Content();
        B_Product proBll = new B_Product();
        private int NodeID { get { return DataConvert.CLng(Request.QueryString["node"]); } }
        private int Order { get { return DataConvert.CLng(Request.QueryString["order"]); } }
        private int CPage
        {
            get
            {
                return PageCommon.GetCPage();
            }
        }
        private string KeyWord { get { return HttpUtility.UrlDecode(Request.QueryString["KeyWord"] ?? ""); } }
        public ActionResult Default()
        {
            DataTable dt = nodeBll.GetNodeListContainXML(0);
            ViewBag.nodehtml =nodeBll.CreateDP(dt);
            return View();
        }
        public ActionResult SearchList()
        {
            DataTable dt = nodeBll.GetNodeListContainXML(0);
            ViewBag.nodehtml = nodeBll.CreateDP(dt);
            DataTable dt1 = DBCenter.Sel("ZL_Node", "ParentID=0");
            ViewBag.KeyWord=KeyWord;
            return View(dt1);
        }
        public ActionResult SearchBody()
        {
            int count = 0;
            DataTable dt = SelPage(out count);
            string hrefTlp = "<a href='javascript:;' onclick='LoadByAjax(\"@query\",@page);'>@text</a>";
            ViewBag.pagehtml = PageCommon.CreatePageHtml(PageHelper.GetPageCount(count, 20), CPage, 10, hrefTlp);
            ViewBag.count = count;
            return View(dt);
        }
        public ActionResult SearchShop() {return View();}
        public void ShopList() { Response.Redirect("SearchList?" + Request.QueryString);return; }

        private DataTable SelPage(out int itemCount)
        {
            //0:内容,1:商品,2:论坛,3:问答,4:留言,5:店铺
            int s = DataConvert.CLng(Request["s"]);
            //*节点ID可能有重复,解决:加一个来源参数,S=0:内容表,1:贴吧
            //ZL_CommonModel status=99,表名ZL_C_,或ZL_P,或ZL_S
            string where = " Title Like @key ";
            if (s != -100) { where += " AND S=" + s; }
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
            return DBCenter.SelPage(20, CPage, out itemCount, "ID", "*", "ZL_SearchView", where, order, sp);
        }
        //SearchClear,SearchDetail,SearchJob
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
namespace ZoomLa.WebSite.Manage
{
    public partial class CreateHtml : System.Web.UI.Page
    {
        protected string type;
        protected B_Create CreateBll = new B_Create();
        private int m_CreateCount;
        protected B_Model bmodel = new B_Model();
        protected B_Node bnode = new B_Node();
        protected B_Content bContent = new B_Content();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            if (!badmin.ChkPermissions("CreateHtmL"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            string s = "<html><head><link href=\"../../App_Themes/AdminDefaultTheme/Guide.css\" type=\"text/css\" rel=\"stylesheet\" />";
            s = s + "<link href=\"../../App_Themes/AdminDefaultTheme/index.css\" type=\"text/css\" rel=\"stylesheet\" />";
            s = s + "<link href=\"../../App_Themes/AdminDefaultTheme/Main.css\" type=\"text/css\" rel=\"stylesheet\" />";
            s = s + "</head>";
            s = s + "<body>\r\n<br/><br/>\r\n<div align='center'><div style='width:500px;border:solid 1px gray;height:24px' align='left'>";
            s = s + "<img id='tp' height='22px' src='../images/loading.gif' width='0px'></div>\r\n</div></div>\r\n";
            s = s + "<table width='500px' align='center'><tr><td align='center' width='250px'>";
            s = s + "<span id='tn'>0%</span></td><td align='center' width='250px' id='finallytd'>共<span id='total'>0</span>条</td></tr></table>";
            s = s + "<script>\r\nfunction SetPr(val,curr){ \r\n document.getElementById('tp').style.width = val;document.getElementById('tn').innerText=val; \r\n}\r\nfunction SetTotal(val){ document.getElementById('total').innerText=val;}</script>\r\n</body>\r\n";

            base.Response.Write(s);
            base.Response.Flush();
            string InfoId = "";
            if (!string.IsNullOrEmpty(base.Request.QueryString["Type"]))
            {
                this.type = base.Request.QueryString["Type"].ToLower();
            }
            if (!string.IsNullOrEmpty(base.Request.QueryString["InfoID"]))
            {
                InfoId = base.Request.QueryString["InfoID"];
            }
            switch (this.type)
            {
                case "index":
                    this.CreateIndex();
                    break;

                case "infoall":
                    this.CreateInfo();
                    break;

                case "infoid":
                    this.CreateInfoByIdStr(InfoId);
                    break;

                case "lastinfocount":
                    this.CreateLastInfoRecord(InfoId);
                    break;

                case "infodate":
                    this.CreateInfoDate(InfoId);
                    break;

                case "infocolumn":
                    this.CreateInfoColumn(InfoId);
                    break;

                case "columnbyid":
                    this.CreateColumnByID(InfoId);
                    break;

                case "columnall":
                    this.CreateColumnAll();
                    break;

                case "special":
                    this.CreateSpecial(InfoId);
                    break;
                case "single":
                    this.CreateSingle();
                    break;
                case "singlebyid":
                    this.CreateSingleByID(InfoId);
                    break;
            }
            base.Response.Write(string.Concat(new object[] { "<table align='center' width='500px'><tr><td align='left'><a href='CreateHtmlContent.aspx", "'>继续发布</a></td>", "<td align='right'><a href='javascript:history.back()'>返回</a></td></tr></table>" }));
            base.Response.Flush();
        }
        /// <summary>
        /// 发布选定的单页
        /// </summary>
        /// <param name="InfoId"></param>
        private void CreateSingleByID(string InfoId)
        {
            DataTable dt = this.bnode.GetCreateSingleByID(InfoId);
            this.m_CreateCount = dt.Rows.Count;
            base.Response.Write("<script>SetTotal(" + this.m_CreateCount + ")</script>\r\n");
            base.Response.Flush();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string str2 = ((i * 100) / this.m_CreateCount).ToString("F1");
                this.CreateBll.CreateNodePage(DataConverter.CLng(dr["NodeID"]));
                base.Response.Write(string.Concat(new object[] { "<script>SetPr('", str2, "%',", i, ")</script>\r\n" }));
                base.Response.Flush();
                i++;
            }
            base.Response.Write("<script>SetPr('100.0%'," + this.m_CreateCount + ")</script>");
            base.Response.Write("<script>document.getElementById('finallytd').innerText = '生成完毕'</script>");
            base.Response.Flush();
        }
        /// <summary>
        /// 发布所有单页
        /// </summary>
        private void CreateSingle()
        {
            DataTable dt = this.bnode.GetSingleList();
            this.m_CreateCount = dt.Rows.Count;
            base.Response.Write("<script>SetTotal(" + this.m_CreateCount + ")</script>\r\n");
            base.Response.Flush();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string str2 = ((i * 100) / this.m_CreateCount).ToString("F1");
                this.CreateBll.CreateNodePage(DataConverter.CLng(dr["NodeID"]));
                base.Response.Write(string.Concat(new object[] { "<script>SetPr('", str2, "%',", i, ")</script>\r\n" }));
                base.Response.Flush();
                i++;
            }
            base.Response.Write("<script>SetPr('100.0%'," + this.m_CreateCount + ")</script>");
            base.Response.Write("<script>document.getElementById('finallytd').innerText = '生成完毕'</script>");
            base.Response.Flush();
        }
        /// <summary>
        /// 发布选定的专题页
        /// </summary>
        private void CreateSpecial(string InfoId)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        /// <summary>
        /// 发布所有栏目
        /// </summary>
        private void CreateColumnAll()
        {
            DataTable dt = this.bnode.GetCreateAllList();
            this.m_CreateCount = dt.Rows.Count;
            base.Response.Write("<script>SetTotal(" + this.m_CreateCount + ")</script>\r\n");
            base.Response.Flush();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string str2 = ((i * 100) / this.m_CreateCount).ToString("F1");
                this.CreateBll.CreateNodePage(DataConverter.CLng(dr["NodeID"]));
                base.Response.Write(string.Concat(new object[] { "<script>SetPr('", str2, "%',", i, ")</script>\r\n" }));
                base.Response.Flush();
                i++;
            }
            base.Response.Write("<script>SetPr('100.0%'," + this.m_CreateCount + ")</script>");
            base.Response.Write("<script>document.getElementById('finallytd').innerText = '生成完毕'</script>");
            base.Response.Flush();
        }
        /// <summary>
        /// 发布选定的栏目页
        /// </summary>
        /// <param name="InfoId"></param>
        private void CreateColumnByID(string InfoId)
        {
            DataTable dt = this.bnode.GetCreateListByID(InfoId);
            this.m_CreateCount = dt.Rows.Count;
            base.Response.Write("<script>SetTotal(" + this.m_CreateCount + ")</script>\r\n");
            base.Response.Flush();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string str2 = ((i * 100) / this.m_CreateCount).ToString("F1");
                this.CreateBll.CreateNodePage(DataConverter.CLng(dr["NodeID"]));
                base.Response.Write(string.Concat(new object[] { "<script>SetPr('", str2, "%',", i, ")</script>\r\n" }));
                base.Response.Flush();
                i++;
            }
            base.Response.Write("<script>SetPr('100.0%'," + this.m_CreateCount + ")</script>");
            base.Response.Write("<script>document.getElementById('finallytd').innerText = '生成完毕'</script>");
            base.Response.Flush();
        }
        /// <summary>
        /// 发布选定的栏目的内容页
        /// </summary>
        /// <param name="InfoId"></param>
        private void CreateInfoColumn(string InfoId)
        {
            DataTable dt = this.bContent.GetCreateNodeList(InfoId);
            this.m_CreateCount = dt.Rows.Count;
            base.Response.Write("<script>SetTotal(" + this.m_CreateCount + ")</script>\r\n");
            base.Response.Flush();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string str2 = ((i * 100) / this.m_CreateCount).ToString("F1");
                this.CreateBll.CreateInfo(DataConverter.CLng(dr["GeneralID"]), DataConverter.CLng(dr["NodeID"]), DataConverter.CLng(dr["ModelID"]));
                base.Response.Write(string.Concat(new object[] { "<script>SetPr('", str2, "%',", i, ")</script>\r\n" }));
                base.Response.Flush();
                i++;
            }
            base.Response.Write("<script>SetPr('100.0%'," + this.m_CreateCount + ")</script>");
            base.Response.Write("<script>document.getElementById('finallytd').innerText = '生成完毕'</script>");
            base.Response.Flush();
        }
        /// <summary>
        /// 按日期发布内容页
        /// </summary>
        /// <param name="modelId"></param>
        private void CreateInfoDate(string InfoId)
        {
            DateTime ID1 = DataConverter.CDate(InfoId.Split(new char[] { ',' })[0]);
            DateTime ID2 = DataConverter.CDate(InfoId.Split(new char[] { ',' })[1]);
            DataTable dt = this.bContent.GetCreateDateList(ID1, ID2);
            this.m_CreateCount = dt.Rows.Count;
            base.Response.Write("<script>SetTotal(" + this.m_CreateCount + ")</script>\r\n");
            base.Response.Flush();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string str2 = ((i * 100) / this.m_CreateCount).ToString("F1");
                this.CreateBll.CreateInfo(DataConverter.CLng(dr["GeneralID"]), DataConverter.CLng(dr["NodeID"]), DataConverter.CLng(dr["ModelID"]));
                base.Response.Write(string.Concat(new object[] { "<script>SetPr('", str2, "%',", i, ")</script>\r\n" }));
                base.Response.Flush();
                i++;
            }
            base.Response.Write("<script>SetPr('100.0%'," + this.m_CreateCount + ")</script>");
            base.Response.Write("<script>document.getElementById('finallytd').innerText = '生成完毕'</script>");
            base.Response.Flush();
        }
        /// <summary>
        /// 发布最新个数的内容页
        /// </summary>
        /// <param name="InfoId"></param>
        private void CreateLastInfoRecord(string InfoId)
        {
            DataTable dt = this.bContent.GetCreateCountList(DataConverter.CLng(InfoId));
            this.m_CreateCount = dt.Rows.Count;
            base.Response.Write("<script>SetTotal(" + this.m_CreateCount + ")</script>\r\n");
            base.Response.Flush();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string str2 = ((i * 100) / this.m_CreateCount).ToString("F1");
                this.CreateBll.CreateInfo(DataConverter.CLng(dr["GeneralID"]), DataConverter.CLng(dr["NodeID"]), DataConverter.CLng(dr["ModelID"]));
                base.Response.Write(string.Concat(new object[] { "<script>SetPr('", str2, "%',", i, ")</script>\r\n" }));
                base.Response.Flush();
                i++;
            }
            base.Response.Write("<script>SetPr('100.0%'," + this.m_CreateCount + ")</script>");
            base.Response.Write("<script>document.getElementById('finallytd').innerText = '生成完毕'</script>");
            base.Response.Flush();
        }
        /// <summary>
        /// 按ID范围发布内容页
        /// </summary>
        /// <param name="InfoId"></param>
        private void CreateInfoByIdStr(string InfoId)
        {
            int ID1 = DataConverter.CLng(InfoId.Split(new char[] { ',' })[0]);
            int ID2 = DataConverter.CLng(InfoId.Split(new char[] { ',' })[1]);
            DataTable dt = this.bContent.GetCreateIDList(ID1, ID2);
            this.m_CreateCount = dt.Rows.Count;
            base.Response.Write("<script>SetTotal(" + this.m_CreateCount + ")</script>\r\n");
            base.Response.Flush();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string str2 = ((i * 100) / this.m_CreateCount).ToString("F1");
                this.CreateBll.CreateInfo(DataConverter.CLng(dr["GeneralID"]), DataConverter.CLng(dr["NodeID"]), DataConverter.CLng(dr["ModelID"]));
                base.Response.Write(string.Concat(new object[] { "<script>SetPr('", str2, "%',", i, ")</script>\r\n" }));
                base.Response.Flush();
                i++;
            }
            base.Response.Write("<script>SetPr('100.0%'," + this.m_CreateCount + ")</script>");
            base.Response.Write("<script>document.getElementById('finallytd').innerText = '生成完毕'</script>");
            base.Response.Flush();
        }
        /// <summary>
        /// 发布所有内容页
        /// </summary>
        private void CreateInfo()
        {
            DataTable dt = this.bContent.GetCreateAllList();
            this.m_CreateCount = dt.Rows.Count;
            base.Response.Write("<script>SetTotal(" + this.m_CreateCount + ")</script>\r\n");
            base.Response.Flush();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string str2 = ((i * 100) / this.m_CreateCount).ToString("F1");
                this.CreateBll.CreateInfo(DataConverter.CLng(dr["GeneralID"]), DataConverter.CLng(dr["NodeID"]), DataConverter.CLng(dr["ModelID"]));
                base.Response.Write(string.Concat(new object[] { "<script>SetPr('", str2, "%',", i, ")</script>\r\n" }));
                base.Response.Flush();
                i++;
            }
            base.Response.Write("<script>SetPr('100.0%'," + this.m_CreateCount + ")</script>");
            base.Response.Write("<script>document.getElementById('finallytd').innerText = '生成完毕'</script>");
            base.Response.Flush();
        }
        /// <summary>
        /// 发布主页
        /// </summary>
        private void CreateIndex()
        {
            base.Response.Write("<script>SetTotal(1)</script>\r\n");
            base.Response.Flush();
            this.CreateBll.CreateIndex();
            base.Response.Write("<script>SetPr('100.0%','1')</script>\r\n");
            base.Response.Write("<script>document.getElementById('finallytd').innerText = '生成完毕'</script>");
            base.Response.Flush();
        }
    }
}
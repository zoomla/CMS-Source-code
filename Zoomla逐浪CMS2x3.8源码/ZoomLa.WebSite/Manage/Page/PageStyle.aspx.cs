namespace manage.Page
{
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
    using ZoomLa.Model;
    using System.Text;

    public partial class PageStyle : CustomerPageAction
    {
        protected B_PageStyle pll = new B_PageStyle();
        protected int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.page, "PageStyle"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            MyBind();
            string menu = DataSecurity.FilterBadChar(Request.QueryString["menu"]);
            this.id = DataConverter.CLng(Request.QueryString["id"]);
            int userid = DataConverter.CLng(Request.QueryString["userid"]);
            if (menu == "del")
            {
                if (this.id > 0) { pll.DelPagestyle(id); Response.Redirect("PageStyle.aspx"); }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li><li class='active'>样式管理<a href='AddPageStyle.aspx'>[添加样式]</a></li>");
        }
        public void MyBind()
        {
            DataTable Cll = pll.GetPagestylelist();
            EGV.DataSource = Cll;
            EGV.DataBind();
        }
        public string getistrue(string istrue)
        {
            return (istrue == "1") ? "<font color=blue>√</font>" : "<font color=red>×</font>";
        }

       
        public string getuserid()
        {
            this.id = DataConverter.CLng(Request.QueryString["id"]);
            return this.id.ToString();
        }

        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
        }
        protected void ExGridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Enable_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                pll.SetEnableByIds(Request.Form["idchk"], 1);
                MyBind();
            }
        }
        protected void Disble_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                pll.SetEnableByIds(Request.Form["idchk"], 0);
                MyBind();
            }
        }

        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType==DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes["ondblclick"] = "location.href='AddPageStyle.aspx?menu=edit&sid="+dr["PageNodeid"] +"';";
            }
        }
    }
}
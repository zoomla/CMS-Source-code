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
using ZoomLa.Components;
using ZoomLa.BLL.Shop;
using ZoomLa.Model.Shop;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class DeliverType : CustomerPageAction
    {

        B_Admin badmin = new B_Admin();
        B_Shop_FareTlp fareBll = new B_Shop_FareTlp();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "DeliverType"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='ProductManage.aspx'>商城管理</a><li class='active'>运费模板列表<a href='AddDeliverType.aspx'>[添加运费模板]</a></li>");
            }
        }
        public void MyBind()
        {
            DataTable dt = fareBll.Sel();
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = "javascript:location.href='AddDeliverType.aspx?menu=edit&id=" + Egv.DataKeys[e.Row.RowIndex].Value + "';";
                e.Row.Attributes["title"] = "双击修改";
            }
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            M_Shop_FareTlp fareMod = fareBll.SelReturnModel(id);
            switch (e.CommandName.ToLower())
            {
                case "del":
                    fareBll.Del(id);
                    break;
            }
            MyBind();
        }
        public string GetMode()
        {
            switch (Eval("PriceMode").ToString())
            {
                case "2":
                    return "按重量";
                default:
                    return "按件数";
            }
        }
    }
}
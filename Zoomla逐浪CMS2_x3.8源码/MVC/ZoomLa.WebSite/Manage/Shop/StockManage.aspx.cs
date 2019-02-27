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

namespace ZoomLaCMS.Manage.Shop
{
    public partial class StockManage : CustomerPageAction
    {
        private B_Stock bll = new B_Stock();
        private B_Node bNode = new B_Node();
        private B_Model bmode = new B_Model();
        protected int Stocktype;
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();

            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "AddStock"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                Stocktype = DataConverter.CLng(base.Request.QueryString["Stocktype"]);
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li class='active'><a href='" + Request.RawUrl + "'>库存管理</a> / <a href='Stock.aspx'>[添加入库出库记录]</a></li>");
        }
        public void DataBind(string key = "")
        {
            EGV.DataSource = bll.AllStock();
            EGV.DataBind();
        }
        protected void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = EGV.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = EGV.PageSize;
            }
            EGV.PageSize = pageSize;
            EGV.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        public string stocktype(string cc)
        {
            int cc1;
            string dd1;
            dd1 = "";
            cc1 = DataConverter.CLng(cc);
            if (cc1 == 0)
            {
                dd1 = "入库";
            }
            else if (cc1 == 1)
            {
                dd1 = "出库";
            }
            return dd1;
        }
        protected void Dels_B_Click(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];
            if (!String.IsNullOrEmpty(CID) && bll.delstock(CID))
            {
                Response.Write("<script language=javascript>alert('批量删除成功!');location.href='StockManage.aspx';</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('批量删除失败!请选择您要删除的数据');location.href='StockManage.aspx';</script>");
            }
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ondblclick", "window.location.href = Stock.aspx?menu=edit&id=" + (e.Row.DataItem as DataRowView)["ID"] + "';");
            }
        }

        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }

        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del1":
                    bll.DeleteByID(Convert.ToInt32(e.CommandArgument.ToString()));
                    break;
                default:
                    break;
            }
            DataBind();
        }
    }
}
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
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Plus
{
    public partial class JSTemplate :CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Egv.txtFunc = txtPageFunc;
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.other, "ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Plus/ADManage.aspx'>广告管理</a></li><li class='active'><a href='JSTemplate.aspx'>广告模板管理</a></li>" + Call.GetHelp(30));
        }
        public void DataBind(string key = "")
        {
            DataTable dt = new DataTable("JSTemplate");
            DataColumn myDataColumn;
            DataRow myDataRow;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "JSTemplateID";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "JSTemplateName";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "JSTemplatePath";
            dt.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "JSTemplateSize";
            dt.Columns.Add(myDataColumn);

            int num = 5;
            B_ADZoneJs adjs = new B_ADZoneJs();
            string[] fileSize = adjs.GetFileSize();
            string[] tname = new string[] { "矩形横幅", "弹出窗口", "随屏移动", "固定位置", "漂浮移动", "文字代码" };
            for (int i = 0; i <= num; i++)
            {
                myDataRow = dt.NewRow();
                myDataRow["JSTemplateID"] = i;
                myDataRow["JSTemplateName"] = tname[i];
                myDataRow["JSTemplatePath"] = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.AdvertisementDir) + "ADTemplate/" + adjs.GetTemplateName(i);
                myDataRow["JSTemplateSize"] = fileSize[i];
                dt.Rows.Add(myDataRow);
            }
            this.Egv.DataSource = dt;
            this.Egv.DataBind();
        }
        protected void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = Egv.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = Egv.PageSize;
            }
            Egv.PageSize = pageSize;
            Egv.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("style", "cursor:pointer");
                e.Row.Attributes.Add("title", "双击修改");
                e.Row.Attributes.Add("ondblclick", "location.href('EditJSTemplate.aspx?ZoneType=" + Egv.DataKeys[e.Row.RowIndex].Value.ToString() + "');");
            }
        }
    }
}
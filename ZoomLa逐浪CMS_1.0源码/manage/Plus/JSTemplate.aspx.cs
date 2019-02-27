namespace ZoomLa.WebSite.Manage.AddOn
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
    using ZoomLa.Common;
    using ZoomLa.BLL;
    using ZoomLa.Components;

    public partial class JSTemplate : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
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
                string[] tname=new string[]{"矩形横幅","弹出窗口","随屏移动","固定位置","漂浮移动","文字代码"};
                for (int i = 0; i <= num; i++)
                {
                    myDataRow = dt.NewRow();
                    myDataRow["JSTemplateID"] = i;
                    myDataRow["JSTemplateName"] = tname[i];
                    myDataRow["JSTemplatePath"] = VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.AdvertisementDir) + "ADTemplate/" + adjs.GetTemplateName(i);
                    myDataRow["JSTemplateSize"] = fileSize[i];
                    dt.Rows.Add(myDataRow);
                }

                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
            }

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Modify")
            {
                Page.Response.Redirect("EditJSTemplate.aspx?ZoneType=" + e.CommandArgument.ToString());
            }
        }
    }
}
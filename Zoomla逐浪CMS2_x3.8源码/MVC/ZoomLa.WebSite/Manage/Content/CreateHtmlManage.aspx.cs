using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Content
{
    public partial class CreateHtmlManage :CustomerPageAction
    {
        B_Node bn = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            if (!IsPostBack)
            {
                showDropDownList2();
            }
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Title", typeof(string)));
            table.Columns.Add(new DataColumn("ID", typeof(string)));
            table.Columns.Add(new DataColumn("Url", typeof(string)));
            ///type 0首页，1节点
            table.Columns.Add(new DataColumn("Type", typeof(int)));

            if (FileSystemObject.IsExist(Request.PhysicalApplicationPath + "index.html", FsoMethod.File))
            {
                DataRow row1 = table.NewRow();
                row1[0] = "首页";
                row1[1] = "0";
                row1[2] = Request.ApplicationPath + "index.html";
                row1[3] = 0;
                table.Rows.Add(row1);
            }
            DataTable nn = bn.SelectNodeHtmlXML();
            if (nn.Rows.Count > 0)
            {
                for (int i = 0; i < nn.Rows.Count; i++)
                {
                    if (FileSystemObject.IsExist(Request.PhysicalApplicationPath + nn.Rows[i]["NodeListUrl"], FsoMethod.File))
                    {
                        DataRow newrow = table.NewRow();
                        newrow[0] = nn.Rows[i]["NodeName"];
                        newrow[1] = nn.Rows[i]["NodeID"];
                        newrow[2] = nn.Rows[i]["NodeListUrl"];
                        newrow[3] = 1;
                        table.Rows.Add(newrow);
                    }
                }
            }
            gvCard.DataSource = table;
            gvCard.DataBind();
        }

        private void showDropDownList2()
        {
            DataTable dColumn = this.bn.GetNodeListContainXML(0);
            this.DropDownList2.DataSource = dColumn;
            this.DropDownList2.DataTextField = "NodeName";
            this.DropDownList2.DataValueField = "NodeID";
            this.DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("首页与栏目生成", "0"));
            DropDownList2.Items.Insert(0, new ListItem("选择生成内容", "-1"));

        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Button3_Click(object sender, EventArgs e)
        {

        }
    }
}
namespace ZoomLa.WebSite.Manage.Content
{
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

    public partial class CreateHtmlManage : CustomerPageAction
    {
        B_Node bn = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
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

            Bind(table);
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
        private void Bind(DataTable dd)
        {
            int CPage, temppage;

            if (Request.Form["DropDownList1"] != null)
            {
                temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
            }
            else
            {
                temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
            }
            CPage = temppage;
            if (CPage <= 0)
            {
                CPage = 1;
            }

            DataTable Cll = dd;

            PagedDataSource cc = new PagedDataSource();
            cc.DataSource = Cll.DefaultView;
            cc.AllowPaging = true;
            cc.PageSize = 12;
            cc.CurrentPageIndex = CPage - 1;
            gvCard.DataSource = cc;
            gvCard.DataBind();

            Allnum.Text = Cll.DefaultView.Count.ToString();
            int thispagenull = cc.PageCount;//总页数
            int CurrentPage = cc.CurrentPageIndex;
            int nextpagenum = CPage - 1;//上一页
            int downpagenum = CPage + 1;//下一页
            int Endpagenum = thispagenull;
            if (thispagenull <= CPage)
            {
                downpagenum = thispagenull;
                Downpage.Enabled = false;
            }
            else
            {
                Downpage.Enabled = true;
            }
            if (nextpagenum <= 0)
            {
                nextpagenum = 0;
                Nextpage.Enabled = false;
            }
            else
            {
                Nextpage.Enabled = true;
            }
            Toppage.Text = "<a href=?Currentpage=0>首页</a>";
            Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
            Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
            Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
            Nowpage.Text = CPage.ToString();
            PageSize.Text = thispagenull.ToString();
            pagess.Text = cc.PageSize.ToString();


            if (!this.Page.IsPostBack)
            {
                for (int i = 1; i <= thispagenull; i++)
                {
                    DropDownList1.Items.Add(i.ToString());
                }
            }

        }
        protected void Button3_Click(object sender, EventArgs e)
        {

        }

    }
}
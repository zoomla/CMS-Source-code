using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.Common;
using ZoomLa.Model.Content;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Manage.Template.ExternDS
{
    public partial class DSAdd : System.Web.UI.Page
    {
        M_DataSource dsModel = new M_DataSource();
        B_DataSource dsBll = new B_DataSource();
        B_Admin badmin = new B_Admin();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax(Request))
            {
                string[] s = Request.Form["data"].Split(',');
                foreach (string i in s)
                {
                    dsBll.DeleteByID(Convert.ToInt32(i));
                    Response.Write("1"); Response.Flush();
                    Response.End();
                }
            }
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='DSList.aspx'>数据源列表</a></li><li class='active'>数据源管理</li>");
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                M_DataSource dsMod = dsBll.SelReturnModel(Mid);
                DSName.Text = dsMod.DSName;
                DataSource_DP.SelectedValue = dsMod.Type;
                Remind_T.Text = dsMod.Remind;
                DBConnectText.Text = dsMod.ConnectionString;
            }
        }
        protected void DataSource_DP_SIChanged(object sender, EventArgs e)
        {
            switch (DataSource_DP.SelectedValue)
            {
                case "mssql":
                    DBConnectText.Text = "Data Source=(local);Initial Catalog=test;User ID=test;Password=test";
                    break;
                case "mysql":
                    DBConnectText.Text = "Data Source=;Provider=Microsoft.JET.OLEDB.4.0;";
                    break;
                case "oracle":
                    DBConnectText.Text = "Data Source=;User Id=;Password=;Integrated Security=no;";
                    break;
                case "access":
                    DBConnectText.Text = "/access/test.mdb";
                    break;
                case "xml":
                    DBConnectText.Text = "/xml/test.xml";
                    break;
                case "excel":
                    DBConnectText.Text = "/temp/test.xlsx";
                    break;
                default:
                    break;
            }
        }
        // 判断多个字符串，如有任意一个为空返回true
        protected bool CheckIsEmpty(string a, string b, string c)
        {
            return (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b) || string.IsNullOrEmpty(c));
        }
        protected void Test_Btn_Click(object sender, EventArgs e)
        {
            DBConnectText.Text = DBConnectText.Text.Trim();
            TestResult_L.Text = "";
            string success = "<span style='color:green'>测试成功</span>";
            SqlBase db = SqlBase.CreateHelper(DataSource_DP.SelectedValue);
            try
            {
                db.ConnectionString = DBConnectText.Text;
                db.Table_List();
                TestResult_L.Text = success;
            }
            catch (Exception ex) { TestResult_L.Text = "<span style='color:red;'>连接失败,原因:" + ex.Message + "</span>"; }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            if (Mid > 0) { dsModel = dsBll.SelReturnModel(Mid); }
            dsModel.DSName = DSName.Text.Trim();
            dsModel.ConnectionString = DBConnectText.Text;
            dsModel.CreateMan = badmin.GetAdminLogin().AdminName;
            dsModel.CreateTime = DateTime.Now;
            dsModel.Type = DataSource_DP.SelectedValue;
            dsModel.Remind = Remind_T.Text.Trim();
            if (Mid > 0) { dsBll.UpdateModel(dsModel); }
            else { dsBll.Insert(dsModel); }
            function.WriteSuccessMsg("操作成功", "DSList.aspx");
        }
    }
}
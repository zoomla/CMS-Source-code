using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Config
{
    public partial class ViewList : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "DevCenter")) { function.WriteErrMsg("你没有对该模块的访问权限"); }
                if (!badmin.CheckSPwd(Session["Spwd"] as string))
                    SPwd.Visible = true;
                else
                    maindiv.Visible = true;
                if (Request.UrlReferrer == null || string.IsNullOrEmpty(Request.UrlReferrer.AbsoluteUri))
                    function.WriteErrMsg("错误的Url或非法的请求！", "");
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>扩展功能</a></li> <li><a href='RunSql.aspx'>开发中心</a></li><li><a href='ViewList.aspx'>视图列表</a> <a href='CreateView.aspx'>[创建视图]</a></li>");
            }
        }
        public void MyBind()
        {
            DataTable dt1 = new DataTable();
            dt1 = SqlHelper.ExecuteTable(CommandType.Text, "select v.[name] as viewname from sys.views as v,sys.schemas as s where v.schema_id = s.schema_id and s.[name] = 'dbo' ", null);
            EGV.DataSource = dt1;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del2")
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, "DROP VIEW " + e.CommandArgument.ToString(), null);
                function.WriteErrMsg("移除视图成功");
            }
            MyBind();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Request.Form["Item"] != null)
            {

                string CID = Request.Form["Item"];
                if (CID.IndexOf(',') > -1)
                {
                    string[] arrcity = CID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < arrcity.Length; i++)
                    {
                        SqlHelper.ExecuteNonQuery(CommandType.Text, "DROP VIEW " + arrcity[i].Trim().ToString(), null);
                    }
                    ClientScript.RegisterStartupScript(this.GetType(), "warning", "<script language='javascript'>alert('批量删除成功!');location.href='ViewList.aspx';</script>");
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(CommandType.Text, "DROP VIEW " + CID.Trim().ToString(), null);
                    ClientScript.RegisterStartupScript(this.GetType(), "warning", "<script language='javascript'>location.href='ViewList.aspx';alert('批量删除成功!');</script>");
                }
            }
        }
    }
}
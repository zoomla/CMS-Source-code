using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.SQLDAL;
using System.Data;
using ZoomLa.Common;
using ZoomLa.BLL;
using System.Data.SqlClient;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.Config
{
    public partial class EmptyData : CustomerPageAction
    {

        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "DevCenter")) { function.WriteErrMsg("你没有对该模块的访问权限"); }
            if (!IsPostBack)
            {
                if (!badmin.CheckSPwd(Session["Spwd"] as string))
                    SPwd.Visible = true;
                else
                    maindiv.Visible = true;
                if (Request.UrlReferrer == null || string.IsNullOrEmpty(Request.UrlReferrer.AbsoluteUri))
                    function.WriteErrMsg("错误的Url或非法的请求！", "");
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>扩展功能</a></li> <li><a href='RunSql.aspx'>开发中心</a></li><li><a href='EmptyData.aspx'>清空测试数据</a></li>" + Call.GetHelp(71));
            }

        }

        protected void ConfirmClr_Click(object sender, EventArgs e)
        {
            clearSql();
        }

        protected void clearSql()
        {
            string strSql = @"select name from  sysobjects ";
            string[] tbnames = Request.Form["tbname_chk"].Split(',');
            foreach (string tbname in tbnames)
            {
                if (strSql.ToLower().Contains("where"))
                {
                    strSql += @"  or name like '" + tbname + "' ";
                }
                else
                {
                    strSql += @"  where name like '" + tbname + "'";
                }
                if (tbname.ToString().Contains("\\"))
                    strSql += "  escape '\\' ";
            }
            string str = string.Empty;
            DataTable dts = SqlHelper.ExecuteTable(SqlHelper.ConnectionString, CommandType.Text, strSql);
            for (int i = 0; i < dts.Rows.Count; i++)
            {
                if (dts.Rows[i]["name"].ToString().ToLower() == "zl_user")
                {
                    str = "delete from ZL_User where UserName not in('admin')";
                }
                else
                {
                    str = "TRUNCATE TABLE " + dts.Rows[i]["name"].ToString();
                }
                SqlHelper.ExecuteSql(str);
            }
            function.WriteSuccessMsg("清除成功", "../Content/NodeManage.aspx");
        }
    }
}
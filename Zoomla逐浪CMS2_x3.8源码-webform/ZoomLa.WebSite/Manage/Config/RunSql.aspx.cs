using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.SQLDAL;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using ZoomLa.BLL;
using System.Configuration;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;
public partial class manage_Config_Sql : CustomerPageAction
{
    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_AdminInfo adminMod = B_Admin.GetLogin();
            if (Request.UrlReferrer == null || string.IsNullOrEmpty(Request.UrlReferrer.AbsoluteUri)) { function.WriteErrMsg("错误的Url或非法的请求!", ""); }
            if (!adminMod.IsSuperAdmin()) { function.WriteErrMsg("只有超级管理员才可访问该模块"); }
            if (!badmin.CheckSPwd(Session["Spwd"] as string)) { SPwd.Visible = true; }
            else {
                maindiv.Visible = true;
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='DatalistProfile.aspx'>扩展功能</a></li> <li><a href='RunSql.aspx'>开发中心</a></li><li><a href='RunSql.aspx'>执行SQL语句</a></li>" + Call.GetHelp(65));
        }
    }

    private void MyBind()
    {
        string pdir = function.VToP(SiteConfig.SiteOption.UploadDir + "/T-sql/");
        if (!Directory.Exists(pdir)) { Directory.CreateDirectory(pdir); }
        DataTable dt = FileSystemObject.SearchFiles2(pdir, "*.sql");
        DataView dv = dt.AsDataView();
        dv.Sort = "CreateTime desc";
        EGV.DataSource = dv;
        EGV.DataBind();
    }
    protected void creatfile(string filenames, string writ, string path)
    {
        StreamWriter sw = new StreamWriter(File.Create(MapPath(path) + filenames + ".sql"));
        sw.Write(writ);
        sw.Close();
    }
    // 将Sql语句保存到脚本
    protected void SaveSql_B_Click(object sender, EventArgs e)
    {
        string sql = Sql_T.Value;
        if (string.IsNullOrEmpty(sql)) { function.WriteErrMsg("脚本内容不能为空！"); }
        string filename = "";
        DateTime dt = DateTime.Now;
        filename = dt.ToString("yyyyMMddHHmmss");
        string path = "/" + SiteConfig.SiteOption.UploadDir + "/T-sql/";
        try
        {
            creatfile(filename, sql, path);
        }
        catch
        {
            Directory.CreateDirectory(MapPath(path));
            creatfile(filename, sql, path);
        }
        function.WriteSuccessMsg("脚本保存成功!");
    }
    // 执行SQL语句
    protected void RunSql_B_Click(object sender, EventArgs e)
    {
        string sql = Sql_T.Value.ToLower();
        if (string.IsNullOrEmpty(sql)) { function.WriteErrMsg("请输入sql语句！"); }
        if (sql.Contains("select "))//查询
        {
            DataTable dt = SqlHelper.ExecuteTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                sql_div.Visible = false;
                result_div.Visible = true;
                //Sql_L.Text = "执行 " + sql + " ,查询结果:";
                Result_EGV.DataSource = dt;
                Result_EGV.DataBind();
            }
            else
            {
                function.WriteErrMsg("查询失败或表中无数据");
            }
        }
        else if (sql.Contains("insert "))//添加
        {
            if (SqlHelper.ExecuteSql(sql))
            {
                function.WriteSuccessMsg("数据插入成功！");
            }
            else { function.WriteErrMsg("数据插入失败！"); }
        }
        else if (sql.Contains("update "))//修改
        {
            if (SqlHelper.ExecuteSql(sql))
            {
                function.WriteSuccessMsg("修改成功！");
            }
            else { function.WriteErrMsg("修改失败！"); }
        }
        else if (sql.Contains("delete "))//删除
        {
            if (SqlHelper.ExecuteSql(sql))
            {
                function.WriteSuccessMsg("删除成功！");
            }
            else { function.WriteErrMsg("删除失败！"); }
        }
        else
        {
            function.WriteErrMsg("您输入的SQL语句可能存在语法错误，请检查后再进行操作");
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string path = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "del2":
                FileSystemObject.Delete(function.VToP(path), FsoMethod.File);
                MyBind();
                break;
            case "execute":
                if (DBHelper.ExecuteSqlScript(DBCenter.DB.ConnectionString, function.VToP(path)))
                {
                    function.WriteSuccessMsg("操作成功!");
                }
                else { function.WriteErrMsg("操作失败!"); }

                break;
        }
    }

    protected void Return_B_Click(object sender, EventArgs e)
    {
        result_div.Visible = false;
        sql_div.Visible = true;
    }
}
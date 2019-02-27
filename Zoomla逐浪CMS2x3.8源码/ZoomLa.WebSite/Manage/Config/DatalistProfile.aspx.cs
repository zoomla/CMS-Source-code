using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net;
using ZoomLa.BLL.Helper;

public partial class Manage_Config_DatalistProfile : System.Web.UI.Page
{
    B_DataList dtlist = new B_DataList();
    DataTable dt = new DataTable();
    B_Admin badmin = new B_Admin();
    HttpHelper httpHelper = new HttpHelper();
    private string Type { get { return Request.QueryString["type"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Call.SetBreadCrumb(Master, "<li><a href='RunSql.aspx'>开发中心</a></li><li><a href='DatalistProfile.aspx'>系统全库概况</a></li>" + Call.GetHelp(68));
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "DevCenter")) { function.WriteErrMsg("你没有对该模块的访问权限"); }
            if (!badmin.CheckSPwd(Session["Spwd"] as string))
            {
                SPwd.Visible = true;
                return;
            }
            else
            {
                maindiv.Visible = true;
            }
            if (Request.UrlReferrer == null || string.IsNullOrEmpty(Request.UrlReferrer.AbsoluteUri))
                function.WriteErrMsg("错误的Url或非法的请求！", "");
            Call.HideBread(Master);
            if (Type.Equals("1"))
            {
                downloadData();
            }
            else
            {
                string script = "if(confirm('使用此功能需要从远程云台下载脚本初始化，否则仅此支持静默模式（不支持高级数据管理）,是否继续?')){location.href='DatalistProfile.aspx?type=1'; }else{location.href=\"DatabaseProfile.aspx\"}";
                if (!ZoomLa.SQLDAL.DBHelper.Table_IsExist("ZL_DataList"))
                {
                    function.Script(this, script);
                    EGV.Visible = false;
                    Link2.Visible = false;
                    return;
                }
                else
                {
                    MyBind();
                    ShowDatas();
                }
            }
        }
    }
    protected void MyBind()
    {
        dt = dtlist.Sel();
        if (dt != null && dt.Rows.Count > 0)
        {
            EGV.Visible = true;
            Link2.Visible = true;
        }
        else
        {
            this.EGV.Visible = false;
            Link2.Visible = false;
        }
        string skey = Search_T.Text.Replace(" ", "");
        SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("type", Request["type"]),
            new SqlParameter("skey","%"+skey+"%")
        };
        string where = "";
        //if (!string.IsNullOrEmpty(Request["type"]))
        //{
        //    sqlstr += " And Type=@type";
        //}
        if (!string.IsNullOrEmpty(skey))
        {
            where += " And Explain LIKE @skey OR TableName LIKE @skey";
        }
        dt = SqlHelper.ExecuteTable("SELECT * FROM ZL_DataList WHERE 1=1 " + where, sp);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        MyBind();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        MyBind();
    }
    protected string GetType(string type)
    {
        switch (type)
        {
            case "0":
                return "系统表";
            case "1":
                return "自定义表";
            case "2":
                return "临时表";
            case "3":
                return "视图";
            default:
                return "系统表";
        }
    }
    protected void downloadData()
    {
        string fileUrl = "http://update.z01.com/ZL_DataList.txt";//template + 
        string saveDir = "/UploadFiles/TSql/";
        string savePath = saveDir + "ZL_DataList.sql";
        if (File.Exists(Server.MapPath(savePath))) { SafeSC.DelFile(savePath); }
        if (!Directory.Exists(Server.MapPath(saveDir))) { SafeSC.CreateDir(saveDir); }
        httpHelper.DownFile(fileUrl, savePath);
        if (!ExecutionSql(Server.MapPath(savePath), SqlHelper.ConnectionString))
        {
            function.WriteErrMsg("操作失败");
        }
        else
        {
            function.WriteSuccessMsg("初始化成功!", "DatalistProfile.aspx");
        }
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
            Page.Response.Redirect("EditDataList.aspx?ID=" + e.CommandArgument.ToString());

        if (e.CommandName == "View")
            Page.Response.Redirect("DataListView.aspx?ID=" + e.CommandArgument.ToString());
        if (e.CommandName == "ViewData")
            Page.Response.Redirect("DataListView.aspx?ID=" + e.CommandArgument.ToString() + "&type=data");

    }
    protected bool checktrue(string url)
    {
        if (File.Exists(Server.MapPath(url)))
        {
            //存在文件
            return true;
        }
        else
        {
            //不存在文件
            File.Create(MapPath(url)).Close();//创建该文件
            if (File.Exists(Server.MapPath(url)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    protected void Link2_Click(object sender, EventArgs e)
    {
        Response.Redirect("DatabaseProfile.aspx");
    }
    /// <summary>
    ///  执行sql脚本写入数据库至新建的数据库中
    /// </summary>
    /// <param name="path">sql脚本路径</param>
    /// <param name="connectString"></param>
    /// <returns></returns>
    public bool ExecutionSql(string path, string connectString)
    {
        SqlConnection connection = new SqlConnection(connectString);
        SqlCommand command = new SqlCommand();
        connection.Open();
        command.Connection = connection;
        using (StreamReader reader = new StreamReader(path, System.Text.Encoding.Default)) //Encoding.UTF8
        {
            try
            {
                while (!reader.EndOfStream)
                {
                    StringBuilder builder = new StringBuilder();
                    while (!reader.EndOfStream)
                    {
                        string str = reader.ReadLine();
                        if (!string.IsNullOrEmpty(str) && str.ToUpper().Trim().Equals("GO"))
                        {
                            break;
                        }
                        builder.AppendLine(str);
                    }
                    command.CommandType = CommandType.Text;
                    command.CommandText = builder.ToString();
                    command.CommandTimeout = 300;
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }
        }
        return true;
    }
    protected void ShowDatas()
    {
        B_DataList bll = new B_DataList();
        DataTable dt = bll.Sel();
        string str = "";
        if (dt.Rows.Count > 0)
        {
            string[,] arr = new string[dt.Rows.Count, 4];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arr[i, 0] = dt.Rows[i]["ThrShort"].ToString();
                arr[i, 1] = dt.Rows[i]["Explain"].ToString();
                arr[i, 2] = dt.Rows[i]["TableName"].ToString();
                arr[i, 3] = dt.Rows[i]["SecShort"].ToString();
                str += "citys[" + i + "]=new Array('" + arr[i, 0] + "','" + arr[i, 1] + "','" + arr[i, 2] + "','" + arr[i, 3] + "');";
            }
        }
        ViewState["dataArr"] = "var citys=new Array();" + str;
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    //重下载数据脚本,并删表和更新
    protected void Link2_Click1(object sender, EventArgs e)
    {

    }
    protected void Search_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
}
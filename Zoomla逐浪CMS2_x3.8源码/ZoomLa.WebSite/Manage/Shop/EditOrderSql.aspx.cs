using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;
using System.Text;


public partial class manage_Shop_EditOrderSql : CustomerPageAction
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ID"]))
        {
           
        }
        Call.SetBreadCrumb(Master, "<li>商城管理</li><li>订单管理</li><li><a href='OrderSql.aspx'>账单管理</a></li><li>查看账单</li>");
    }
    /// <summary>
    /// 生成SQL文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
       
    }
    // 导出SQL文件
    protected void Save1_Click(object sender, EventArgs e)
    {

        
    }
    protected void creatfile(string filenames, string writ, string path)
    {
      
    }
    // 运行SQL
    protected void Run_Click(object sender, EventArgs e)
    {
        
     }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
    //  执行sql脚本写入数据库至新建的数据库中
    public static bool ExecutionSql(string path, string connectString)
    {
        return true;
    }
}
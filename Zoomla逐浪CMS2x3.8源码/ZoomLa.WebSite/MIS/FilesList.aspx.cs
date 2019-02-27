using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.IO;

public partial class MIS_FilesList : System.Web.UI.Page
{
    B_MisInfo bll = new B_MisInfo();
    M_MisInfo model = new M_MisInfo();
    DataTable dt = new DataTable();
    B_User buser = new B_User();
    int ProID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin("/MIS/MisInfo.aspx");
        M_UserInfo info = buser.GetLogin();
        if (!IsPostBack)
        {
            if (function.IsNumeric(Request.QueryString["ProID"]))
            {
                ProID = DataConverter.CLng(Request.QueryString["ProID"]);
            }
        }
    }
    protected string GetStatus(int stu)
    {
        if (stu == 1)
        {
            return "结束";
        }
        else
        {
            return "未结束";
        }
    }
    protected void Repeater1_ItemCommand(object obj, RepeaterCommandEventArgs e)
    {
       
    }
    protected void Repeater1_ItemBind(object sender, RepeaterItemEventArgs e)
    {
       
    }

    public long FileSize(string filePath)
    {
        long temp = 0;
        filePath=base.Request.PhysicalApplicationPath+filePath;
        //判断当前路径所指向的是否为文件
        if (File.Exists(filePath) == false)
        {
            string[] str1 = Directory.GetFileSystemEntries(filePath);
            foreach (string s1 in str1)
            {
                temp += FileSize(s1);
            }
        }
        else
        {

            //定义一个FileInfo对象,使之与filePath所指向的文件向关联,

            //以获取其大小
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }
        return temp;
    }

}
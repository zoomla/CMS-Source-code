using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using ZoomLa.Common;

//支持词条名搜索,支持词条标签搜索
public partial class Guestbook_BkSearch : System.Web.UI.Page
{
    B_Baike bkBll = new B_Baike();
    B_User buser = new B_User();
    public string Tittle { get { return Server.UrlDecode(Request.QueryString["tittle"]); } }
    public string BType { get { return Server.UrlDecode(Request.QueryString["BType"]); } }
    public int UserID { get { return DataConverter.CLng(Request.QueryString["UserID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        DataTable dt = bkBll.SelByInfo(Tittle, BType, UserID);
        if (string.IsNullOrEmpty(Tittle + BType) && UserID == 0) { lblTittle.Text = "<div class='alert alert-warning'>未输入查询条件</div>"; return; }
        if (dt.Rows.Count < 1)
        {
            if (!string.IsNullOrEmpty(Tittle))
            {
                lblTittle.Text = "<div class='alert alert-info'>" + Call.SiteName + "百科尚未收录该词条\"<font color='red'>" + Tittle + "</font>\"，也未找到相关词条。<br/>欢迎您来创建，与广大网友分享关于该词条的信息。<a href='BKEditor.aspx?tittle=" + Tittle + "'><font color='blue'>我来创建</font></a></div>";
            }
            else
            {
                lblTittle.Text = "<div class='alert alert-info'>" + Call.SiteName + "百科尚未收录该词条<br/>欢迎您来创建,与广大网友分享各种词条的信息</div>";
            }
            return;
        }
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    protected string getstyle()
    {
        if (buser.CheckLogin())
        {
            return "display:inline-table";
        }
        else return "display:none";
    }
    protected string getstyles()
    {
        if (buser.CheckLogin())
        {
            return "display:none";
        }
        else return "display:inline-table";
    }
}
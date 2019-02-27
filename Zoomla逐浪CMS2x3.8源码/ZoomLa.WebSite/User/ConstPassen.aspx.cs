using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Data;

public partial class User_ConstPassen : System.Web.UI.Page
{
    public int type { get { return DataConverter.CLng(Request.QueryString["type"]); } }
    B_User user = new B_User();
    private B_Client_Basic bll = new B_Client_Basic();
    //private string xmlPath2 = "~/Config/CRM_Dictionary.xml";
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        user.CheckIsLogin();
        if (Request.QueryString["menu"] != null && Request.QueryString["menu"] == "delete")
        {
            string code = Request.QueryString["code"];
            bll.GetDeleteByCode(code);//以code为标准删除多表数据
        }
        if (!IsPostBack)
        {   
            
            MyBind();
        } 
    }
    public void MyBind()
    {
        string group = Request.QueryString["group"]; 
        dt = bll.SelByType(type,group);
        dt.DefaultView.RowFilter = " FPManID =" + user.GetLogin().UserID;
        dt.DefaultView.Sort = "Flow desc";
        dt = dt.DefaultView.ToTable(); 
        EGV.DataSource=dt;
        EGV.DataBind(); 
    } 
    protected void Button1_Click(object sender, EventArgs e)
    {
        string[] codeids = Request.Form["idchk"].Split(',');
        for (int i = 0; i < codeids.Length; i++)
        {
            bll.GetDeleteByCode(codeids[i]);
        }
        function.WriteSuccessMsg("批量删除成功!", "ConstPassen.aspx"); 
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}

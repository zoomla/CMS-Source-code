using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

public partial class Manage_Design_AddTlpClass : CustomerPageAction
{
    B_Design_TlpClass classBll = new B_Design_TlpClass();
    M_Design_TlpClass classMod = new M_Design_TlpClass();
    private int Pid { get { return DataConvert.CLng(ViewState["Pid"]); } set { ViewState["Pid"] = value; } }
    private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Pid = DataConvert.CLng(Request.QueryString["Pid"]);
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>动力模块</a></li><li><a href='TlpClass.aspx'>模板分类</a></li><li class='active'>分类管理</li>");
        }
    }
    private void MyBind()
    {
        PName_T.Text = SiteConfig.SiteInfo.SiteName;
        if (Mid > 0) 
        {
            classMod = classBll.SelReturnModel(Mid);
            Name_T.Text = classMod.Name;
            Remind_T.Text = classMod.Remind;
            CDate_L.Text = classMod.CDate.ToString();
        }
        if (Pid > 0) 
        {
            M_Design_TlpClass pmod = classBll.SelReturnModel(Pid);
            PName_T.Text = pmod.Name;
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        if (Mid > 0)
        {
            classMod = classBll.SelReturnModel(Mid);
        }
        classMod.Name = Name_T.Text;
        classMod.Remind = Remind_T.Text;
        if (classMod.ID > 0) { classBll.UpdateByID(classMod); }
        else
        {
            classMod.Pid = Pid;
            classMod.Depth = classBll.GetDepth(Pid);
            classMod.AdminID = B_Admin.GetLogin().AdminId;
            classBll.Insert(classMod);
        }
        function.WriteSuccessMsg("操作成功", "TlpClass.aspx");
    }
}
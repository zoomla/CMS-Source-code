using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.Data;

public partial class Manage_I_Content_TemplateSet : CustomerPageAction
{
    protected B_Admin badmin = new B_Admin();
    private string serverdomain = SiteConfig.SiteOption.ProjectServer;// 
    DataSet newtempset = new DataSet("NewDataSet");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        RPT.DataSource = FileSystemObject.GetDirectorySmall(SiteConfig.SiteMapath() + "Template");
        RPT.DataBind();
    }

    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del2":
                string deletepath = @"/Template/" + e.CommandArgument.ToString() + "/";
                SafeSC.DelFile(deletepath);
                break;
            case "set":
                string tempdir = @"/Template/" + e.CommandArgument;                
                SiteConfig.SiteOption.TemplateDir = tempdir;
                SiteConfig.SiteOption.CssDir = tempdir + "/style";
                SiteConfig.Update();               
                break;
        }
        MyBind();
    }
    public String GetTlpName(string name)
    {
        string ppath = Server.MapPath(@"/Template/" + Eval("Name").ToString() + @"/Info.config");
        if (FileSystemObject.IsExist(ppath, FsoMethod.File))
        {
            DataSet newtempset = new DataSet();
            newtempset.ReadXml(ppath);
            return newtempset.Tables[0].Rows[0][name].ToString();//Project
        }
        else
        {
            return Eval("name").ToString();
        }
    }
    public string IsDefaultTlp()
    {
        if (SiteConfig.SiteOption.TemplateDir.Equals("/template/" + Eval("name").ToString(), StringComparison.OrdinalIgnoreCase))
        {
            return "<span style='color:#f00;'>已选用</span>";
        }         
        
        return "待选用";              
    }
}
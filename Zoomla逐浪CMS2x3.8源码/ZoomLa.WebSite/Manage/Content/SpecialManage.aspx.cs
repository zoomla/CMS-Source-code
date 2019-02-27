using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_I_Content_SpecialManage : CustomerPageAction
{
    protected B_Spec bll = new B_Spec();
    public int Mid
    {
        get { return DataConverter.CLng(Request.QueryString["id"]); }
    }
    public string SKey { get { return Request.QueryString["skey"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string result="";
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            switch (action)
            {
                case "GetChild":
                    if (!string.IsNullOrEmpty(SKey))//搜索
                    {
                        result = JsonHelper.JsonSerialDataTable(bll.GetSpecAll(SKey));
                    }
                    else
                    {
                        DataTable dt = bll.GetSpecList(DataConverter.CLng(value));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["SpecDesc"] = StringHelper.SubStr(dt.Rows[i]["SpecDesc"], 40);
                        }
                        result = JsonHelper.JsonSerialDataTable(dt);
                    }
                    
                    break;
                case "Del":
                    bll.DelBySpecID(DataConverter.CLng(value));
                    result = "1";
                    break;
                default:
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        //B_Admin badmin = new B_Admin();
        //if (!B_ARoleAuth.Check(ZLEnum.Auth.model, "SpecManage"))
        //    function.WriteErrMsg("没有权限进行此项操作");
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "AddSpec":
                Response.Redirect("AddSpec.aspx?Action=AddCh&ID=" + e.CommandArgument.ToString());
                break;
            case "SpecList":
                Response.Redirect("SpecialManage.aspx?id=" + DataConverter.CLng(e.CommandArgument));
                break;
            case "Modify":
                Response.Redirect("AddSpec.aspx?Action=Modify&ID=" + e.CommandArgument.ToString());
                break;
            case "Delete":
                bll.DelBySpecID(DataConverter.CLng(e.CommandArgument));
                MyBind();
                break;
        }   
    }
    private void MyBind()
    {
        //DataTable dt = new DataTable();
        //dt = bll.GetSpecList(Mid);
        //RPT.DataSource = dt;
        //RPT.DataBind();
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li class=\"active\"><a href='"+Request.RawUrl+"'>专题管理</a></li>" + Call.GetHelp(58));
    }
    public string GetIcon()
    {
        return DataConverter.CLng(Eval("ChildCount")) > 0 ? "fa fa-folder" : "fa fa-file";
    }
    public string GetDesc() { return StringHelper.SubStr(Eval("SpecDesc"), 40); }
}
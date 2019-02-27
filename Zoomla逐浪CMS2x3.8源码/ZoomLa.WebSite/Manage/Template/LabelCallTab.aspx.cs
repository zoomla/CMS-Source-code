using System;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Content;
using ZoomLa.SQLDAL;


public partial class Manage_I_Content_LabelCallTab : CustomerPageAction
{
    public string param;
    public string ShowPath = string.Empty;
    B_Admin badmin = new B_Admin();
    B_DataSource dsBll = new B_DataSource();
    B_Label labelBll = new B_Label();
    B_FunLabel bfun = new B_FunLabel();
    DataTable dsTable = new DataTable();
    M_DataSource dsModel = new M_DataSource();
    private string TemplateDir
    {
        get
        {
            string _dir = SiteConfig.SiteOption.TemplateDir;
            if (!string.IsNullOrEmpty(Request["setTemplate"])) { _dir = Request["setTemplate"]; }
            return _dir;
        }
    }
    public string LabelName { get { return Request.QueryString["labelName"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(LabelName))
        {
            M_Label labelinfo = labelBll.GetLabelXML(LabelName);
            param = labelinfo.Param;
        }
        if (!IsPostBack)
        {
            CustomLabel_DP.DataSource = labelBll.GetLabelCateListXML();
            CustomLabel_DP.DataTextField = "name";
            CustomLabel_DP.DataValueField = "name";
            CustomLabel_DP.DataBind();
            CustomLabel_DP.Items.Insert(0, new ListItem("选择标签类型", ""));
            Field_DP.DataSource = labelBll.GetSourceLabelXML();//LabelType
            Field_DP.DataTextField = "LabelName";
            Field_DP.DataValueField = "LabelID";
            Field_DP.DataBind();
            Field_DP.Items.Insert(0, new ListItem("选择数据源标签", ""));
            lblSys.Text = bfun.GetSysLabel();
            lblFun.Text = bfun.GetFunLabel();
            DealInvoke();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li><a href='LabelManage.aspx'>标签管理</a></li><li class='active'>标签调用</li>");
        }
    }
    protected string getExternalConnStr(M_Label label)
    {
        try
        {
            string id = label.DataSourceType;
            dsTable = ViewState["dsTable"] == null ? dsBll.Sel() : ViewState["dsTable"] as DataTable;
            return dsTable.Select("ID='" + id + "'")[0][dsTable.Columns.IndexOf("ConnectionString")] as string;
        }
        catch { return SqlHelper.ConnectionString; }
    }
    // 处理从LabelManage中跳过来的引用调用,!ispostBack中调用
    private void DealInvoke() 
    {
        M_Label labelinfo = labelBll.GetLabelXML(LabelName);
        function.Script(this, "cit2('" + labelinfo.LableType + "','" + labelinfo.LableName + "');");
    }
    public string GetParam()
    {
        param = param.Replace("'", "\"");
        return param;
    }
 }
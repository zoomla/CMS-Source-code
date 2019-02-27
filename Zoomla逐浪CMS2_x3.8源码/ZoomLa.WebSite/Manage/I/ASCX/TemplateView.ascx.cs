using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.SQLDAL;
using System.Data;
using ZoomLa.Common;

public partial class Manage_I_ASCX_TemplateView : System.Web.UI.UserControl
{
    [Browsable(true)]
    public string TemplateID { get; set; }
    [Browsable(true)]
    public string TemplateUrl { get; set; }
    [Browsable(true)]
    public string TemplatePic { get; set; }
    [Browsable(true)]
    public string TemplateTitle { get; set; }
    private bool _isfirstselect = true;
    [Browsable(true)]
    public bool IsFirstSelect { get { return _isfirstselect; } set { _isfirstselect = value; } }
    /// <summary>
    /// 模板数量
    /// </summary>
    public int Count
    {
        get { return Convert.ToInt32(ViewState["TemplateView_Count"]); }
        set { ViewState["TemplateView_Count"] = value; }
    }
    //选中模板的id
    public string SelectedID
    {
        get
        {
            return TempleID_Hid.Value.ToString();
        }
        set
        {
            TempleID_Hid.Value = value.ToString();
        } 
    }
    //选中模板的Url值
    public string SelectedValue
    {
        get
        {
            return TempleUrl_Hid.Value;
        }
        set
        {
            TempleUrl_Hid.Value = value;
        }
    }
    public DataTable DataSource { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public new void DataBind()
    {
        if (DataSource == null || DataSource.Rows.Count <= 0)
            return;
        ReplaceFild();
        this.Count = DataSource.Rows.Count;
        Temp_RPT.DataSource = DataSource;
        Temp_RPT.DataBind();
    }
    //替换字段
    public void ReplaceFild()
    {
        if (!string.IsNullOrEmpty(TemplateID))
            DataSource.Columns[TemplateID].ColumnName = "TemplateID";
        if (!string.IsNullOrEmpty(TemplateUrl))
            DataSource.Columns[TemplateUrl].ColumnName = "TemplateUrl";
        if (!string.IsNullOrEmpty(TemplatePic))
            DataSource.Columns[TemplatePic].ColumnName = "TemplatePic";
        if (!string.IsNullOrEmpty(TemplateTitle))
            DataSource.Columns[TemplateTitle].ColumnName = "TemplateTitle";
    }

}
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
using ZoomLa.SQLDAL;
using ZoomLa.Web;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Text;

public partial class Manage_I_Content_ModelManage : CustomerPageAction
{
    protected B_Model bll = new B_Model();
    private B_ModelField cc = new B_ModelField();
    private B_Admin badmin = new B_Admin();
    private M_ModelInfo m_ModelInfo = new M_ModelInfo();
    //7:互动,
    public string ModelType { get { return Request.QueryString["ModelType"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        string str = "";
        string help = "";
        if (!IsPostBack)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.pub, "ModelManage");
            if (!string.IsNullOrEmpty(ModelType))
            {
                switch (ModelType)
                {
                    case "1":
                        str = Resources.L.内容;
                        help = Call.GetHelp(10);
                        break;
                    case "2":
                        str = Resources.L.商城;
                        help = Call.GetHelp(11);
                        break;
                    case "3":
                        str = Resources.L.用户;
                        help = Call.GetHelp(13);
                        break;
                    case "4":
                        str = Resources.L.黄页;
                        break;
                    case "5":
                        str = Resources.L.店铺;
                        help = Call.GetHelp(12);
                        break;
                    case "6":
                        str = Resources.L.店铺申请;
                        break;
                    case "7":
                        str = Resources.L.互动;
                        break;
                    case "8":
                        str = Resources.L.功能;
                        break;
                    case "11":
                        str = "CRM";
                        break;
                    case "12":
                        str = "OA";
                        break;
                    default:
                        function.WriteErrMsg("未指定正确的模型ID");
                        break;
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>"+Resources.L.工作台+"</a></li><li><a href='ModelManage.aspx?ModelType=" + ModelType + "'>"+ Resources.L.模型管理 + "</a></li><li class='active'><a href='ModelManage.aspx?ModelType=" + ModelType + "'>" + str + Resources.L.模型管理+ "</a> <a href='AddEditModel.aspx?ModelType=" + ModelType + "' class='reds'>["+ Resources.L.添加 + str + Resources.L.模型 + "]</a></li>" + help);
                DataBind();
            }
        }
    }
    public void DataBind(string key="")
    {
        DataTable dt = this.bll.GetModel("'" + bll.GetModelType(Convert.ToInt32(ModelType)) + "'", "");
        //dt.DefaultView.Sort = "ModelIDS ASC";
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    public string GetModelIcon()
    {
        return StringHelper.GetItemIcon(Eval("ItemIcon").ToString());
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row!=null&&e.Row.RowType==DataControlRowType.DataRow)
        {
            e.Row.Attributes["ondblclick"] = "javascript:getinfo('" + EGV.DataKeys[e.Row.RowIndex].Value + "');";
            e.Row.Attributes["style"] = "cursor:pointer;";
            e.Row.Attributes["title"] = "双击修改";
        }
    }
    protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Id = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "Copy":
                {
                    B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "ModelEdit");
                    M_ModelInfo model = bll.GetModelById(Convert.ToInt32(Id));
                    model.ModelID = 0;
                    model.ModelName = model.ModelName + "_拷贝";
                    model.TableName = model.TableName + "_" + function.GetRandomString(4);
                    bll.AddModel(model);
                    function.WriteSuccessMsg("拷贝模型成功", "ModelManage.aspx?ModelType=" + ModelType);
                }
                break;
            case "Edit1":
                Response.Redirect("AddEditModel.aspx?ModelID=" + Id + "&ModelType=" + Request.QueryString["ModelType"]);
                break;
            case "Del2":
                B_ARoleAuth.CheckEx(ZLEnum.Auth.model, "ModelEdit");
                if (this.bll.DelModel(int.Parse(Id)))
                    function.WriteSuccessMsg("删除成功");
                else
                    function.WriteSuccessMsg("删除失败");
                break;
            case "Field":
                Response.Redirect("ModelField.aspx?ModelID=" + Id + "&ModelType=" + Request.QueryString["ModelType"]);
                break;
            default :
                break;
        }
        DataBind();
    }
    public string isExistTableName(string TableName)
    {
        if (bll.isExistTableName(TableName))
        {
            Random ro = new Random();
            TableName = TableName + ro.Next(100, 999).ToString();
            return TableName = isExistTableName(TableName);
        }
        else
        {
            return TableName;
        }
    }
    protected bool GetEnabled(string str)
    {
        if (str == "1")
        {
            return true;
        }
        return false;
    }
    protected string getHelpID()
    {
        switch (Request["ModelType"])
        {
            case "1":
                return "10";
            case "2": 
                return "11"; 
            case "5":
                return "12"; 
			case "3":
				return "13";
			case "8":
				return "14";
            default:
                return "";
        }
    }
    public DataTable GetTable(DataTable dt, string PowerInfo)
    {
        string names = "";
        string[] PowerInfoArr = PowerInfo.Split(',');
        for (int i = 0; i < PowerInfoArr.Length; i++)
        {
            names += "'" + PowerInfoArr[i] + "',";
        }
        names = names.Trim(',');
        dt.DefaultView.RowFilter = "TableName in (" + names + ")";
        dt = dt.DefaultView.ToTable();
        return dt;
    }
    //public string GetIcon(string IconPath)
    //{
    //    return "/Images/ModelIcon/" + (string.IsNullOrEmpty(IconPath) ? "Default.gif" : IconPath);
    //}
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class MIS_OA_Struct_StructList : System.Web.UI.Page
{
    B_Structure strBll = new B_Structure();
    B_User buser = new B_User();
    //----节点
    string path = CustomerPageAction.customPath2 + "AddOn/";
    string hasChild, noChild;
    //--------------会员相关
    private string preAction
    {
        get { return ViewState["preAction"] as string; }
        set { ViewState["preAction"] = value; }
    }
    public string preValue
    {
        get { return ViewState["preValue"] as string; }
        set { ViewState["preValue"] = value; }
    }
    public string orderType
    {
        get { return ViewState["orderType"] as string; }
        set { ViewState["orderType"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        hasChild = "<a href='StructList.aspx?action=struct&value={0}' id='a{0}' class='list1'>{2}<span class='list_span'>{1}</span></a>";
        noChild =  "<a href='StructList.aspx?action=struct&value={0}'>{2}{1}</a>";
        if (!IsPostBack)
        {
            preAction = Request.QueryString["action"];
            preValue = Request.QueryString["value"];
            BindNode();
            MyBind();
        }
    }
    private void MyBind()
    {
        M_Structure strMod = strBll.SelReturnModel(DataConvert.CLng(preValue));
        if (strMod == null) return;
        DataTable dt = buser.SelectUserByIds(strMod.UserIDS); //buser.SelPage(pageSize, pageIndex, out itemCount, preAction, preValue, orderType);
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    protected void BindNode()
    {
        DataTable dt = strBll.Sel();
        nodeHtml.Text = "<ul class='tvNav'><li><a  class='list1' id='a0' href='StructList.aspx' ><span class='list_span'>全部成员</span><span class='fa fa-list'></span></a>" +  B_Node.GetLI(dt,hasChild,noChild) + "</li></ul>";
    }
    protected void OrderBtn_Click(object sender, EventArgs e)
    {
        LinkButton linkbt = ((LinkButton)sender);
        orderType = linkbt.CommandArgument;
        RPT.DataBind();
        if (orderType.IndexOf("Asc") > -1)//判断是否倒序
        {
            linkbt.CommandArgument = orderType.Replace("Asc", "");
            linkbt.Text = linkbt.Text.Replace("fa-arrow-up", "fa-arrow-down");
        }
        else
        {
            linkbt.CommandArgument = "Asc" + orderType;
            linkbt.Text = linkbt.Text.Replace("fa-arrow-down", "fa-arrow-up");
        }
        function.Script(this, "ShowOrderIcon('" + linkbt.ID + "')");
    }
}
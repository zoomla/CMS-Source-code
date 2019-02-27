using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class test_RegionEdit : CustomerPageAction
{
    //1:内容列表,2:调整(用于顶部条等),3:
    B_Label labelBll = new B_Label();
    B_Content bll = new B_Content();
    B_Model bmodel = new B_Model();
    B_Node bNode = new B_Node();
    B_Admin badmin = new B_Admin();
    B_Create createBll = new B_Create();
    ContentHelper conHelper = new ContentHelper();
    private string Label { get { return Request.QueryString["label"].Trim(); } }
    private string IDS { get { return Request.QueryString["ids"]; } }
    private int FirstID{get{return DataConvert.CLng(IDS.Split(',')[0]);}}
    public int CNodeID { get { return DataConvert.CLng(ViewState["CNodeID"]); } set { ViewState["CNodeID"] = value; } }
    //1,无值的情况下,如何获取到其节点和模型信息?
    //2,支付商城商品列表管理,贴吧内容管理等
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Label)) { function.WriteErrMsg("未指定标签名"); }
            string labelHtml = "<a href='" + customPath2 + "Template/LabelSql.aspx?LabelName=" + HttpUtility.UrlEncode(Label) + "' title='查看标签源码' style='color:red;'>[" + Label + "]</a>";
            Empty_Div.InnerHtml = "区块" + labelHtml + "没有可供维护的数据,";
            Call.HideBread(Master);
            if (FirstID == 0)
            {
                opdiv.Visible = false;
                Empty_Div.Visible = true; return;
            }
            else
            {
                MyBind();
            }
        }
    }
    public void MyBind() 
    {
        M_CommonData contentMod = bll.SelReturnModel(FirstID);
        M_ModelInfo modelMod = bmodel.GetModelById(contentMod.ModelID);
        CNodeID = contentMod.NodeID;
        Add_A.NavigateUrl = CustomerPageAction.customPath2 + "Content/AddContent.aspx?ModelID=" + contentMod.ModelID + "&NodeID=" + contentMod.NodeID;
        Add_A.Text = "添加" + modelMod.ItemName;
        DataTable dt = SelByIDS(IDS);
        if (dt.Rows.Count < 1) { Empty_Div.Visible = true; return; }
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item != null && e.Item.ItemType != ListItemType.Footer)
        {
            DataRowView dr = e.Item.DataItem as DataRowView;
            int generalId = DataConverter.CLng(dr["GeneralID"]);
            int isCreate, status;
            isCreate = DataConvert.CLng(dr["IsCreate"]);
            status = DataConvert.CLng(dr["Status"]);

            LinkButton lbHtml = e.Item.FindControl("lbHtml") as LinkButton;//删除||生成Html
            LinkButton lbCheck = e.Item.FindControl("lbCheck") as LinkButton;//浏览
            LinkButton lbDelete = e.Item.FindControl("lbDelete") as LinkButton;//删除内容

            HiddenField hfstatus = e.Item.FindControl("hfstatus") as HiddenField;
            if (isCreate == 1) //判断是否已生成,1.为已生成
            {
                lbHtml.CommandName = "DelHtml";
                lbHtml.CommandArgument = generalId.ToString();
                lbHtml.Text = "删除HTML";
            }
            else if (isCreate == 0) //判断是否已生成,0.为未生成
            {
                lbHtml.CommandName = "CreateHtml";
                lbHtml.CommandArgument = generalId.ToString();
                lbHtml.Text = "生成HTML";
            }
        }
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
       
    }
    public bool GetRole(int nid, string authType)
    {
        bool result = false;
        M_AdminInfo ad = badmin.GetAdminLogin();
        if (ad.IsSuperAdmin())
        {
            result = true;
        }
        else
        {
            DataTable dt = badmin.GetNodeAuthList();
            dt.DefaultView.RowFilter = "NID=" + nid + " And " + authType + " = 1";
            if (dt.DefaultView.ToTable().Rows.Count > 0) result = true;
        }
        return result;
    }
    public DataTable SelByIDS(string ids) 
    {
        ids = StrHelper.IdsFormat(ids, "");
        if (string.IsNullOrEmpty(ids)) { return new DataTable(); }
        SafeSC.CheckIDSEx(ids);
        string sql = "SELECT * FROM ZL_CommonModel WHERE GeneralID IN("+ids+")";
        return SqlHelper.ExecuteTable(CommandType.Text,sql);
    }
    public string GetElite(string elite)
    {
        return conHelper.GetElite(elite);
    }
    public string GetPic(object modeid)
    {
        return conHelper.GetPic(modeid);
    }
    //显示标题
    public string GetTitle()
    {
        //string ItemID, string NID, string Title, string modeid, string style
        string title = Eval("Title").ToString();
        title = title.Length > 55 ? title.Substring(0, 55) + "..." : title;
        string n = "<a style=\"" + Eval("TitleStyle") + "\" href=\"" + CustomerPageAction.customPath2 + "/Content/EditContent.aspx?GeneralID=" + Eval("GeneralID") + "\">" + title + "</a>";
        return n;
    }
}
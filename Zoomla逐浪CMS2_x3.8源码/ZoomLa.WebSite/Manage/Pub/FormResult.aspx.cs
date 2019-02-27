using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

public partial class Manage_Pub_FormResult : System.Web.UI.Page
{
    M_Pub pubMod = new M_Pub();
    B_Pub pubBll = new B_Pub();
    public int Mid { get { return  Convert.ToInt32(Request.QueryString["ID"]); } }
    public int CPage { get { return PageCommon.GetCPage(); } }
    public int PageSize = 10;
    public DataTable DTStruct { get { return (DataTable)ViewState["DTStruct"]; } set { ViewState["DTStruct"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        pubMod = pubBll.SelReturnModel(Mid);
        DataTable dt = pubBll.SelByTbName(pubMod.PubTableName);
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li class='active'><a href='FormManage.aspx'>互动表单管理</a></li><li>当前表单:<a href='" + Request.RawUrl + "'>" + pubMod.PubName + "</a></li>");
        if (dt.Rows.Count < 1) { EmptyDiv.Visible = true; return; }
        //--------------------------------------------------
        DataTable ResultDT = GetStruct(HttpUtility.HtmlDecode(dt.Rows[0]["PubContent"].ToString()));
        foreach (DataRow dr in dt.Rows)
        {
            string json = HttpUtility.HtmlDecode(dr["PubContent"].ToString());
            JArray array = (JArray)JsonConvert.DeserializeObject(json);
            DataRow resultDR = ResultDT.NewRow();
            foreach (JObject model in array)
            {
                string ctype = model["ctype"].ToString().ToLower();
                if (ctype.Equals("img") || ctype.Equals("str")) continue;
                resultDR[model["title"].ToString()] = model["value"].ToString();
            }
            resultDR["EndChar"] = "end";
            resultDR["ID"] = dr["ID"];
            resultDR["IP地址"] = dr["PubIP"];
            ResultDT.Rows.Add(resultDR);
        }
        int pageCount = 0;
        RPT.DataSource = PageCommon.GetPageDT(PageSize,CPage,ResultDT,out pageCount);
        Page_Lit.Text = PageCommon.CreatePageHtml(pageCount, CPage);
        RPT.DataBind();
    }
    //互动的值在最前,EndChar之后的为系统值
    public DataTable GetStruct(string json) 
    {
        DataTable dt = new DataTable();
        JArray array = (JArray)JsonConvert.DeserializeObject(json);
        foreach (JObject model in array)
        {
            string ctype = model["ctype"].ToString().ToLower();
            if (ctype.Equals("img") || ctype.Equals("str")) continue;
            dt.Columns.Add(new DataColumn(model["title"].ToString(), typeof(string)));//结构
        }
        TableHead_Lit.Text = GetTableHead(dt);
        dt.Columns.Add(new DataColumn("EndChar", typeof(string)));
        dt.Columns.Add(new DataColumn("ID", typeof(int)));
        dt.Columns.Add(new DataColumn("IP地址",typeof(string)));
        //dt.Columns.Add(new DataColumn("提交人", typeof(string)));
        DTStruct = dt.Clone();
        return dt;
    }
    public string GetTableHead(DataTable dt) 
    {
        string tdhtml = "";
        foreach (DataColumn dc in dt.Columns)
        {
            tdhtml += "<th>"+dc.ColumnName+"</th>";
        }
        return tdhtml;
    }
    protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = e.Item.DataItem as DataRowView;
            Literal lit = e.Item.FindControl("TableValue_Lit") as Literal;
            foreach (DataColumn dc in DTStruct.Columns)
            {
                string value = dr[dc.ColumnName].ToString() ;
                if (value.Equals("end")) break;
                lit.Text += "<td>" + value + "</td>";
            }
        }
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del2":
                pubMod = pubBll.SelReturnModel(Mid);
                int id = Convert.ToInt32(e.CommandArgument);
                pubBll.DelComments(pubMod.PubTableName, id);
                break;
        }
        MyBind();
    }
}